using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using RestSharp;

namespace ShoppingCartWebApiClientLib.Client.Extensions
{
    public static class ApiClientExtensions
    {
         /// <summary>
        ///     If parameter is DateTime, output in a formatted string (default ISO 8601), customizable with
        ///     Configuration.DateTime.
        ///     If parameter is a list of string, join the list with ",".
        ///     Otherwise just return the string.
        /// </summary>
        /// <param name="obj">The parameter (header, path, query, form).</param>
        /// <returns>Formatted string.</returns>
        public static string ParameterToString(this object obj)
        {
            switch (obj)
            {
                case DateTime _:
                    return ((DateTime) obj).ToString(Configuration.DateTimeFormat);
                case List<string> _:
                    return string.Join(",", (obj as List<string>)?.ToArray());
            }

            return Convert.ToString(obj);
        }

        /// <summary>
        ///     Deserialize the JSON string into a proper object.
        /// </summary>
        /// <param name="content">HTTP body (e.g. string, JSON).</param>
        /// <param name="type">Object type.</param>
        /// <param name="headers">HTTP headers.</param>
        /// <returns>Object representation of the JSON string.</returns>
        public static object Deserialize(this string content, Type type, IList<Parameter> headers = null)
        {
            if (type == typeof(object)) // return an object
                return content;

            if (type == typeof(Stream))
            {
                var filePath = string.IsNullOrEmpty(Configuration.TempFolderPath)
                    ? Path.GetTempPath()
                    : Configuration.TempFolderPath;

                var fileName = filePath + Guid.NewGuid();
                if (headers != null)
                {
                    var regex = new Regex(@"Content-Disposition:.*filename=['""]?([^'""\s]+)['""]?$");
                    var match = regex.Match(headers.ToString());
                    if (match.Success)
                        fileName = filePath + match.Value.Replace("\"", "").Replace("'", "");
                }

                File.WriteAllText(fileName, content);
                return new FileStream(fileName, FileMode.Open);
            }

            if (type.Name.StartsWith("System.Nullable`1[[System.DateTime")) // return a datetime object
                return DateTime.Parse(content, null, DateTimeStyles.RoundtripKind);

            if (type == typeof(string) || type.Name.StartsWith("System.Nullable")) // return primitive type
                return content.ConvertType(type);

            // at this point, it must be a model (json)
            try
            {
                return JsonConvert.DeserializeObject(content, type);
            }
            catch (IOException e)
            {
                throw new ApiException(500, e.Message);
            }
        }

        /// <summary>
        ///     Serialize an object into JSON string.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>JSON string.</returns>
        public static string Serialize(this object obj)
        {
            try
            {
                return obj != null ? JsonConvert.SerializeObject(obj) : null;
            }
            catch (Exception e)
            {
                throw new ApiException(500, e.Message);
            }
        }
        
        /// <summary>
        ///     Encode string in base64 format.
        /// </summary>
        /// <param name="text">String to be encoded.</param>
        /// <returns>Encoded string.</returns>
        public static string Base64Encode(this string text)
        {
            var textByte = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textByte);
        }

        /// <summary>
        ///     Dynamically cast the object into target type.
        ///     Ref: http://stackoverflow.com/questions/4925718/c-dynamic-runtime-cast
        /// </summary>
        /// <param name="source">Object to be casted</param>
        /// <param name="dest">Target type</param>
        /// <returns>Casted object</returns>
        public static object ConvertType(this object source, Type dest)
        {
            return Convert.ChangeType(source, dest);
        }
        
    }
}