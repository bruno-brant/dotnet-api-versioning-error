using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using Xunit;

namespace ApiVersioningError.Tests
{
	public class MainTest : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly HttpClient _client;

		public MainTest(WebApplicationFactory<Startup> testContext)
		{
			_client = testContext.CreateDefaultClient();
		}

		[Fact]
		public async System.Threading.Tasks.Task PostAsync()
		{
			var result = await _client.PostAsJsonAsync("api/v1/pets", new
			{
				name = "MyDog",
				type = 0,
			});

			var message = $"{result.ReasonPhrase}\n{result.Content.ReadAsStringAsync()}";

			Assert.True(result.IsSuccessStatusCode, message);
		}
	}
}
