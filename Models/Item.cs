using System.Runtime.Serialization;
using Newtonsoft.Json;
using ShoppingCartWebApiClientLib.Models.Interfaces;

namespace ShoppingCartWebApiClientLib.Models
{
    /// <summary>
    ///     an item available on the e-commerce website
    /// </summary>
    [DataContract]
    public class Item : IModel
    {
        /// <summary>
        ///     Inventory id of the item
        /// </summary>
        /// <value>Inventory id of the item</value>
        [DataMember(Name = "Id", EmitDefaultValue = false, IsRequired = true)]
        [JsonProperty(PropertyName = "Id")]
        public long Id { get; set; }
        
        /// <summary>
        ///     Name of the item
        /// </summary>
        /// <value>Name of the item</value>
        [DataMember(Name = "Name", EmitDefaultValue = false, IsRequired = true)]
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        
        /// <summary>
        ///     A short description about the item
        /// </summary>
        /// <value>A short description about the item</value>
        [DataMember(Name = "Description", EmitDefaultValue = false, IsRequired = false)]
        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }
        
        /// <summary>
        ///     Cost of the item (in EUR)
        /// </summary>
        /// <value>Cost of the item (in EUR)</value>
        [DataMember(Name = "Value", EmitDefaultValue = false, IsRequired = true)]
        [JsonProperty(PropertyName = "Value")]
        public decimal Value { get; set; }
        
        /// <summary>
        ///     Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        [DataMember(Name = "InventoryCount", EmitDefaultValue = false, IsRequired = true)]
        [JsonProperty(PropertyName = "InventoryCount")]
        public int InventoryCount { get; set; }

        public override string ToString()
        {
            return ToJson();
        }
        
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}