using WeaviateNET;
using WeaviateNET.Query;
using WeaviateNET.Query.AdditionalProperty;
using WeaviateNET.Query.ConditionalOperator;

public class WeaviateRepository<T> : IRepository<T> where T : WeaviateEntity, new()
{

    public WeaviateRepository(WeaviateDB db)
    {
        _db = db;
    }

    protected WeaviateClass<T>? _TClass;
    protected WeaviateDB _db;

    protected void ensureConnection()
    {
        if (_db == null)
            throw new Exception("Weaviate not connected");
    }

    protected async Task InitializeClass()
    {
        if (_TClass == null)
        {
            await _db.Schema.Update();
            if (_db.Schema.Classes.Where(c => c.Name == typeof(T).Name).Any())
            {
                _TClass = _db.Schema.GetClass<T>(typeof(T).Name);
                foreach (var propertyInfo in typeof(T).GetFields())
                {
                    if (propertyInfo.Name == "id" || propertyInfo.Name == "ClassName") continue;
                    if (!_TClass!.Properties.Any(p => p.Name == propertyInfo.Name))
                    {
                        await _TClass.AddProperty(Property.Create(propertyInfo.FieldType, propertyInfo.Name));
                    }
                }
            }
            else
            {
                _TClass = await _db.Schema.NewClass<T>(typeof(T).Name);
                await _TClass.Update();
            }
        }
    }

    protected virtual async Task EnsureClassAsync()
    {
        if (_TClass == null)
            await InitializeClass();
    }

    public virtual async Task<Guid?> Add(T T)
    {
        ensureConnection();
        await EnsureClassAsync();

        var obj = _TClass!.Create();
        obj.Properties = T;

        await _TClass.Add(obj);
        T.id = obj.Id ?? Guid.Empty; // Assumption: Guid is generated by Create()
        return obj.Id;
    }

    public virtual async Task<T?> Get(Guid id)
    {
        ensureConnection();
        await EnsureClassAsync();

        WeaviateObject<T>? obj = null;

        try
        {
            obj = await _TClass!.Get(id);
        }
        catch (ApiException)
        {
            return null;
        }

        if (obj == null) return null;

        obj.Properties.id = obj?.Id ?? Guid.Empty;
        return obj?.Properties;
    }

    public virtual async Task<ICollection<T>> List(long limit = 1024, long offset = 0, string? sort = null, string? order = null)
    {
        ensureConnection();
        await EnsureClassAsync();

        var objs = await _TClass!.ListObjects(limit, offset: offset, sort: sort, order: order);
        if (objs == null) return new List<T>();
        var ret = new List<T>();
        foreach (var obj in objs.Objects)
        {
            obj.Properties.id = obj.Id ?? Guid.Empty;
            ret.Add(obj.Properties);
        }
        return ret;
    }

    public virtual async Task<bool> Update(T T)
    {
        ensureConnection();
        await EnsureClassAsync();

        WeaviateObject<T>? obj = null;

        try
        {
            obj = await _TClass!.Get(T.id);
        }
        catch (ApiException)
        {
            return false;
        }

        if (obj == null)
            throw new Exception($"Cannot find T with id {T.id}");

        obj.Properties = T;
        await obj.Save();
        return true;
    }

    public virtual async Task<bool> Delete(Guid id)
    {
        ensureConnection();
        await EnsureClassAsync();

        WeaviateObject<T>? obj = null;

        try
        {
            obj = await _TClass!.Get(id);
        }
        catch (ApiException)
        {
            return false;
        }

        if (obj == null)
            throw new Exception($"Cannot find T with id {id}");

        await obj.Delete();
        return true;
    }

    public virtual async Task<ICollection<T>> GetByProperty<K>(string propertyName, K propertyValue, long limit = 1024, long offset = 0, string? sort = null, string? order = null)
    {
        ensureConnection();
        await EnsureClassAsync();

        if (_TClass.Properties.Where(p => p.Name == propertyName).Count() == 0)
            throw new Exception($"Property {propertyName} does not exist in WeaviateClass {_TClass.Name}");

        if (!_TClass.Properties.Where(p => p.Name == propertyName).First().DataType.Any(s => s == WeaviateDataType.MapType<K>()))
            throw new Exception($"WeaviateClass's Property {propertyName} is not of type {typeof(K).Name}");

        var query = _TClass.CreateGetQuery(selectall: true);
        var cond = new List<ConditionalAtom<T>>() {
                When<T, K>.Equal(propertyName, propertyValue)
            };

        query.Filter.Where(Conditional<T>.And(cond.ToArray()));
        query.Fields.Additional.Add(Additional.Id);
        var graphQLQuery = new GraphQLQuery();
        graphQLQuery.Query = query.ToString();

        var res = await _db.Schema.RawQuery(graphQLQuery);

        if (res.Errors != null && res.Errors.Count > 0)
        {
            throw new Exception($"Error querying Weaviate");
        }

        var ret = new List<T>();

#pragma warning disable CS8602
        foreach (var TJToken in res.Data["Get"][_TClass.Name])
        {
            Guid id = TJToken["_additional"]["id"] == null ? new Guid() : Guid.Parse(TJToken["_additional"]["id"].ToString());
            var o = TJToken.ToObject<T>();
            o.id = id;
            ret.Add(o);
        }
#pragma warning restore CS8602

        return ret;
    }

}


