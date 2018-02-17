namespace ShoppingCartWebApiClientLib.Models.Interfaces
{
    public interface IModel
    {
        /// <summary>
        ///     Get the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        string ToJson();
    }
}