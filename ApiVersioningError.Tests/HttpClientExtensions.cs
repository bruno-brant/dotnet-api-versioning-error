// Copyright (c) QSaúde LTDA. Todos direitos reservados.

using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ApiVersioningError.Tests
{
	public static class HttpClientExtensions
	{
		private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
		{
			ContractResolver = new DefaultContractResolver
			{
				NamingStrategy = new CamelCaseNamingStrategy()
			}
		};

		public static async Task<T> ReadAsJsonAsync<T>(this HttpContent @this)
		{
			if (@this is null)
			{
				throw new ArgumentNullException(nameof(@this));
			}

			var str = await @this.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<T>(str);
		}

		public static async Task<HttpResponseMessage> PutAsJsonAsync(this HttpClient @this, string uri, object json)
		{
			if (@this is null)
			{
				throw new ArgumentNullException(nameof(@this));
			}

			using var str = new StringContent(JsonConvert.SerializeObject(json, _serializerSettings), Encoding.UTF8, "application/json");

			return await @this.PutAsync(uri, str);
		}

		public static async Task<HttpResponseMessage> PutAsJsonAsync(this HttpClient @this, Uri uri, object json)
		{
			if (@this is null)
			{
				throw new ArgumentNullException(nameof(@this));
			}

			using var str = new StringContent(JsonConvert.SerializeObject(json, _serializerSettings), Encoding.UTF8, "application/json");

			return await @this.PutAsync(uri, str);
		}

		public static async Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient @this, string uri, object json)
		{
			if (@this is null)
			{
				throw new ArgumentNullException(nameof(@this));
			}

			using var str = new StringContent(JsonConvert.SerializeObject(json, _serializerSettings), Encoding.UTF8, "application/json");

			return await @this.PostAsync(uri, str);
		}

		public static async Task<HttpResponseMessage> PostAsJsonAsync(HttpClient @this, Uri uri, object json)
		{
			if (@this is null)
			{
				throw new ArgumentNullException(nameof(@this));
			}

			using var str = new StringContent(JsonConvert.SerializeObject(json, _serializerSettings), Encoding.UTF8, "application/json");

			return await @this.PostAsync(uri, str);
		}
	}
}
