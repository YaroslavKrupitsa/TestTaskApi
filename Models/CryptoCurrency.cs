using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskApi.Models
{
	 public class CryptoCurrency
	{
		public string Name { get; set; }
		public string Symbol { get; set; }
		public decimal PriceUsd { get; set; }
		public decimal MarketCap { get; set; }
	}
}
