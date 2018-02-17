using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ShoppingCartWebApiClientLib.Models.Interfaces;

namespace ShoppingCartWebApiClientLib.Models
{
    public class ShoppingCart : IModel
    {
        /// <summary>
        ///     A list of all the items present in the shopping cart
        /// </summary>
        /// <value>A list of all the items present in the shopping cart</value>
        [DataMember(Name = "ItemList", EmitDefaultValue = false, IsRequired = true)]
        [JsonProperty(PropertyName = "ItemList")]
        public IList<Item> ItemList { get; set; }

        /// <summary>
        ///     A dictionary of Items and their respective quantities where the ItemId is the key Quantity is the value
        /// </summary>
        /// <value>A dictionary of Items and their respective quantities where the ItemId is the key Quantity is the value</value>
        [DataMember(Name = "ItemCountMap", EmitDefaultValue = false, IsRequired = true)]
        [JsonProperty(PropertyName = "ItemCountMap")]
        public Dictionary<int, int> ItemCountMap { get; set; }

        /// <summary>
        ///     Total number of items in the shopping cart
        /// </summary>
        /// <value>Total number of items in the shopping cart</value>
        [DataMember(Name = "TotalItemCount", EmitDefaultValue = false, IsRequired = true)]
        [JsonProperty(PropertyName = "TotalItemCount")]
        public int TotalItemCount { get; set; }

        /// <summary>
        ///     Total value of all the items in the shopping cart
        /// </summary>
        /// <value>Total value of all the items in the shopping cart</value>
        [DataMember(Name = "TotalValue", EmitDefaultValue = false, IsRequired = true)]
        [JsonProperty(PropertyName = "TotalValue")]
        public decimal TotalValue { get; set; }


        /// <summary>
        ///     Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            return ToJson();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Get the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}