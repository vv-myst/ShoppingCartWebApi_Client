using System.Collections.Generic;
using System.IO;
using RestSharp;
using RestSharp.Contrib;
using RestSharp.Extensions;
using ShoppingCartWebApiClientLib.Client.Interfaces;

namespace ShoppingCartWebApiClientLib.Client
{
    /// <summary>
    ///     API client is mainly responible for making the HTTP call to the API backend.
    /// </summary>
    public class ApiClient : IApiClient
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ApiClient" /> class.
        /// </summary>
        /// <param name="basePath">The base path.</param>
        public ApiClient(string basePath = "https://localhost")
        {
            BasePath = basePath;
            RestClient = new RestClient(BasePath);
        }

        /// <summary>
        ///     Gets or sets the base path.
        /// </summary>
        /// <value>The base path</value>
        public string BasePath { get; }

        /// <summary>
        ///     Gets or sets the RestClient.
        /// </summary>
        /// <value>An instance of the RestClient</value>
        public RestClient RestClient { get; }

        /// <summary>
        ///     Gets the default header.
        /// </summary>
        public Dictionary<string, string> DefaultHeader { get; } = new Dictionary<string, string>();

        /// <inheritdoc />
        /// <summary>
        ///     Makes the HTTP request (Sync).
        /// </summary>
        /// <param name="path">URL path.</param>
        /// <param name="method">HTTP method.</param>
        /// <param name="queryParams">Query parameters.</param>
        /// <param name="postBody">HTTP body (POST request).</param>
        /// <param name="headerParams">Header parameters.</param>
        /// <param name="formParams">Form parameters.</param>
        /// <param name="fileParams">File parameters.</param>
        /// <returns>Object</returns>
        public object CallApi(string path, Method method, Dictionary<string, string> queryParams, string postBody,
            Dictionary<string, string> headerParams, Dictionary<string, string> formParams,
            Dictionary<string, FileParameter> fileParams)
        {
            var request = new RestRequest(path, method);            

            // add default header, if any
            foreach (var defaultHeader in DefaultHeader)
                request.AddHeader(defaultHeader.Key, defaultHeader.Value);

            // add header parameter, if any
            foreach (var param in headerParams)
                request.AddHeader(param.Key, param.Value);

            // add query parameter, if any
            foreach (var param in queryParams)
                request.AddParameter(param.Key, param.Value, ParameterType.GetOrPost);

            // add form parameter, if any
            foreach (var param in formParams)
                request.AddParameter(param.Key, param.Value, ParameterType.GetOrPost);

            // add file parameter, if any
            foreach (var param in fileParams)
                request.AddFile(param.Value.Name, param.Value.Writer, param.Value.FileName, param.Value.ContentType);

            if (postBody != null) // http body (model) parameter
                request.AddParameter("application/json", postBody, ParameterType.RequestBody);

            return RestClient.Execute(request);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Add default header.
        /// </summary>
        /// <param name="key">Header field name.</param>
        /// <param name="value">Header field value.</param>
        /// <returns></returns>
        public void AddDefaultHeader(string key, string value)
        {
            DefaultHeader.Add(key, value);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Escape string (url-encoded).
        /// </summary>
        /// <param name="str">String to be escaped.</param>
        /// <returns>Escaped string.</returns>
        public string EscapeString(string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Create FileParameter based on Stream.
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <param name="stream">Input stream.</param>
        /// <returns>FileParameter.</returns>
        public FileParameter ParameterToFile(string name, Stream stream)
        {
            return FileParameter.Create(name, stream.ReadAsBytes(),
                stream is FileStream ? Path.GetFileName(((FileStream) stream).Name) : "no_file_name_provided");
        }

        /// <inheritdoc />
        /// <summary>
        ///     Get the API key with prefix.
        /// </summary>
        /// <param name="apiKeyIdentifier">API key identifier (authentication scheme).</param>
        /// <returns>API key with prefix.</returns>
        public string GetApiKeyWithPrefix(string apiKeyIdentifier)
        {
            Configuration.ApiKey.TryGetValue(apiKeyIdentifier, out var apiKeyValue);
            if (Configuration.ApiKeyPrefix.TryGetValue(apiKeyIdentifier, out var apiKeyPrefix))
                return apiKeyPrefix + " " + apiKeyValue;
            return apiKeyValue;
        }
    }
}