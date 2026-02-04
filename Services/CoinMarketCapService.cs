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
	}
}

