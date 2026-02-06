using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using TestTaskApi.Models;

namespace TestTaskApi.Services
{
	public class CoinMarketCapService
	{
		private const string ApiKey = "faae69641070461aa3d3750b99f2d7cc";
		private const string Url =
			"https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest?start=1&limit=10&convert=USD";

		public async Task<List<CryptoCurrency>> GetTop10Async()
		{
			 var client = new HttpClient();
			client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", ApiKey);

			var response = await client.GetAsync(Url);
			response.EnsureSuccessStatusCode();

			var json = await response.Content.ReadAsStringAsync();
			 var doc = JsonDocument.Parse(json);

			var list = new List<CryptoCurrency>();

			foreach (var item in doc.RootElement.GetProperty("data").EnumerateArray())
			{
				list.Add(new CryptoCurrency
				{
					Name = item.GetProperty("name").GetString(),
					Symbol = item.GetProperty("symbol").GetString(),
					PriceUsd = item.GetProperty("quote").GetProperty("USD").GetProperty("price").GetDecimal(),
					MarketCap = item.GetProperty("quote").GetProperty("USD").GetProperty("market_cap").GetDecimal()
				});
			}

			return list;
		}

		public async Task<List<CryptoCurrency>> GetTopAsync(int limit)
		{
			var url =
				"https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest" +
				"?start=1&limit=" + limit + "&convert=USD";

			 var client = new HttpClient();
			client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", ApiKey);

			var response = await client.GetAsync(url);
			response.EnsureSuccessStatusCode();

			var json = await response.Content.ReadAsStringAsync();
			 var doc = JsonDocument.Parse(json);

			var list = new List<CryptoCurrency>();

			foreach (var item in doc.RootElement.GetProperty("data").EnumerateArray())
			{
				list.Add(new CryptoCurrency
				{
					Name = item.GetProperty("name").GetString(),
					Symbol = item.GetProperty("symbol").GetString(),
					PriceUsd = item.GetProperty("quote")
								   .GetProperty("USD")
								   .GetProperty("price")
								   .GetDecimal(),
					MarketCap = item.GetProperty("quote")
									.GetProperty("USD")
									.GetProperty("market_cap")
									.GetDecimal()
				});
			}

			return list;
		}

		public async Task<CryptoCurrencyDetail> GetCurrencyInfoAsync(int id)
		{
			var url = $"https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest?id={id}&convert=USD";

			var client = new HttpClient();
			client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", ApiKey);

			var response = await client.GetAsync(url);
			response.EnsureSuccessStatusCode();

			var json = await response.Content.ReadAsStringAsync();
			 var doc = JsonDocument.Parse(json);

			var data = doc.RootElement.GetProperty("data").GetProperty(id.ToString());

			var quote = data.GetProperty("quote").GetProperty("USD");

			return new CryptoCurrencyDetail
			{
				Name = data.GetProperty("name").GetString(),
				Symbol = data.GetProperty("symbol").GetString(),
				PriceUsd = quote.GetProperty("price").GetDecimal(),
				Volume24h = quote.GetProperty("volume_24h").GetDecimal(),
				PercentChange1h = quote.GetProperty("percent_change_1h").GetDecimal(),
				PercentChange24h = quote.GetProperty("percent_change_24h").GetDecimal(),
				PercentChange7d = quote.GetProperty("percent_change_7d").GetDecimal()
			};
		}


	}
}

