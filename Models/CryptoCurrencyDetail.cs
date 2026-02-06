using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskApi.Models
{
	public class CryptoCurrencyDetail
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Symbol { get; set; }
		public decimal PriceUsd { get; set; }
		public decimal Volume24h { get; set; }
		public decimal PercentChange1h { get; set; }
		public decimal PercentChange24h { get; set; }
		public decimal PercentChange7d { get; set; }

		public List<decimal> PriceHistory { get; set; } = new List<decimal>();
	}

	

}
