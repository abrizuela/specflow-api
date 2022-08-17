using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SpecFlowAPI
{
    public static class RestResponseExtensions
    {
        private static readonly JsonSerializerOptions Options;

        public struct SpotifyValidationErrorDetail
        {
            public SpotifyError Error { get; set; }
        }

        public struct SpotifyError
        {
            public int Status { get; set; }
            public string Message { get; set; }
        }

        static RestResponseExtensions()
        {
            Options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            var enumConverter = new JsonStringEnumConverter();
            Options.Converters.Add(enumConverter);
        }

        /// <summary>
        /// Deserialeze a Json Response to the given Entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public static TEntity? GetContent<TEntity>(this IRestResponse response)
        {
            return JsonSerializer.Deserialize<TEntity?>(response.Content, Options);
        }

        /// <summary>
        /// Deserialize the Spotify error response
        /// </summary>
        /// <param name="response"></param>
        /// <returns>The info about the endpoint, the status and the message</returns>
        public static string GetProblemDetails(this IRestResponse response)
        {
            try
            {
                var problemDetails = GetContent<SpotifyValidationErrorDetail>(response);

                return $"{response.Request.Method} {response.Request.Resource}\n   Status: {response.StatusCode}.\n   Message: {problemDetails.Error.Message}";
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
