using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PrismApp.EvlSink
{
	/// <summary>
	/// Helper class to aid calling the web api
	/// </summary>
	/// <remarks>
	/// @TODO: Not a great name.
	/// </remarks>
	static class Api
	{
		public static string ApiKey { get; set; }
		public static string Endpoint { get; set; }
		public static string Source { get; set; }
		public static string SourceVersion { get; set; }

		// Pinched from Swampnet.EvL
		public static async Task PostAsync(Event e, string apiKey, string endpoint)
		{
			Validate(apiKey, endpoint);

			using (var client = new HttpClient()) // @TODO: Not recommended. Should use a static / singleton instance
			{
				client.DefaultRequestHeaders.Add("x-api-key", apiKey);

				var rs = await client
					.PostAsync(
						endpoint,
						new StringContent(
							JsonConvert.SerializeObject(e),
							Encoding.UTF8,
							"application/json"))
					.ConfigureAwait(false);

				rs.EnsureSuccessStatusCode();
			}
		}


		public static Task PostAsync(Event e)
		{
			return PostAsync(e, ApiKey, Endpoint);
		}


		public static async Task PostAsync(IEnumerable<Event> e, string apiKey, string endpoint)
		{
			Validate(apiKey, endpoint);

			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("x-api-key", apiKey);

				var rs = await client
					.PostAsync(
						endpoint + "/bulk",
						new StringContent(
							JsonConvert.SerializeObject(e),
							Encoding.UTF8,
							"application/json"))
					.ConfigureAwait(false);

				rs.EnsureSuccessStatusCode();
			}
		}

		public static Task PostAsync(IEnumerable<Event> e)
		{
			return PostAsync(e, ApiKey, Endpoint);
		}


		private static void Validate(string apiKey, string endpoint)
		{
			if (string.IsNullOrEmpty(apiKey))
			{
				throw new ArgumentNullException("ApiKey");
			}
			if (string.IsNullOrEmpty(endpoint))
			{
				throw new ArgumentNullException("Endpoint");
			}
		}
	}
}
