/*
 * Oraculum API
 *
 * API for managing facts using Oraculum.
 *
 * OpenAPI spec version: 1.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace OraculumApi.Models.BackOffice
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class SearchCriteria : IEquatable<SearchCriteria>
    {
        /// <summary>
        /// Search query to find relevant facts.
        /// </summary>
        /// <value>Search query to find relevant facts.</value>
        [Required]

        [DataMember(Name = "query")]
        public required string Query { get; set; }

        /// <summary>
        /// Distance criterion for search.
        /// </summary>
        /// <value>Distance criterion for search.</value>

        [DataMember(Name = "distance")]
        public float? Distance { get; set; }

        /// <summary>
        /// Limit the number of facts returned.
        /// </summary>
        /// <value>Limit the number of facts returned.</value>

        [DataMember(Name = "limit")]
        public int? Limit { get; set; }

        /// <summary>
        /// Auto-cut criterion for search.
        /// </summary>
        /// <value>Auto-cut criterion for search.</value>

        [DataMember(Name = "autoCut")]
        public int? AutoCut { get; set; }

        /// <summary>
        /// Filter facts by type.
        /// </summary>
        /// <value>Filter facts by type.</value>

        [DataMember(Name = "factTypeFilter")]
        public string[]? FactTypeFilter { get; set; }

        /// <summary>
        /// Filter facts by category.
        /// </summary>
        /// <value>Filter facts by category.</value>

        [DataMember(Name = "categoryFilter")]
        public string[]? CategoryFilter { get; set; }

        /// <summary>
        /// Filter facts by tags.
        /// </summary>
        /// <value>Filter facts by tags.</value>

        [DataMember(Name = "tagsFilter")]
        public string[]? TagsFilter { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SearchCriteria {\n");
            sb.Append("  Query: ").Append(Query).Append("\n");
            sb.Append("  Distance: ").Append(Distance).Append("\n");
            sb.Append("  Limit: ").Append(Limit).Append("\n");
            sb.Append("  AutoCut: ").Append(AutoCut).Append("\n");
            sb.Append("  FactTypeFilter: ").Append(FactTypeFilter).Append("\n");
            sb.Append("  CategoryFilter: ").Append(CategoryFilter).Append("\n");
            sb.Append("  TagsFilter: ").Append(TagsFilter).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((SearchCriteria)obj);
        }

        /// <summary>
        /// Returns true if SearchCriteria instances are equal
        /// </summary>
        /// <param name="other">Instance of SearchCriteria to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SearchCriteria other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Query == other.Query ||
                    Query != null &&
                    Query.Equals(other.Query)
                ) &&
                (
                    Distance == other.Distance ||
                    Distance != null &&
                    Distance.Equals(other.Distance)
                ) &&
                (
                    Limit == other.Limit ||
                    Limit != null &&
                    Limit.Equals(other.Limit)
                ) &&
                (
                    AutoCut == other.AutoCut ||
                    AutoCut != null &&
                    AutoCut.Equals(other.AutoCut)
                ) &&
                (
                    FactTypeFilter == other.FactTypeFilter ||
                    FactTypeFilter != null &&
                    FactTypeFilter.SequenceEqual(other.FactTypeFilter)
                ) &&
                (
                    CategoryFilter == other.CategoryFilter ||
                    CategoryFilter != null &&
                    CategoryFilter.SequenceEqual(other.CategoryFilter)
                ) &&
                (
                    TagsFilter == other.TagsFilter ||
                    TagsFilter != null &&
                    TagsFilter.SequenceEqual(other.TagsFilter)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                if (Query != null)
                    hashCode = hashCode * 59 + Query.GetHashCode();
                if (Distance != null)
                    hashCode = hashCode * 59 + Distance.GetHashCode();
                if (Limit != null)
                    hashCode = hashCode * 59 + Limit.GetHashCode();
                if (AutoCut != null)
                    hashCode = hashCode * 59 + AutoCut.GetHashCode();
                if (FactTypeFilter != null)
                    hashCode = hashCode * 59 + FactTypeFilter.GetHashCode();
                if (CategoryFilter != null)
                    hashCode = hashCode * 59 + CategoryFilter.GetHashCode();
                if (TagsFilter != null)
                    hashCode = hashCode * 59 + TagsFilter.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(SearchCriteria left, SearchCriteria right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SearchCriteria left, SearchCriteria right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
