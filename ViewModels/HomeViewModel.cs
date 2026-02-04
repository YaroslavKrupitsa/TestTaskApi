using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TestTaskApi.Models;
using System.ComponentModel;
using TestTaskApi.Services;

namespace TestTaskApi.ViewModels
{
	public class HomeViewModel : INotifyPropertyChanged
	{
		private readonly CoinMarketCapService _service = new CoinMarketCapService();

		public ObservableCollection<CryptoCurrency> TopCurrencies { get; }
			= new ObservableCollection<CryptoCurrency>();

		public HomeViewModel()
		{
			_ = LoadAsync();
		}

		private async Task LoadAsync()
		{
			var data = await _service.GetTop10Async();

			TopCurrencies.Clear();
			foreach (var item in data)
				TopCurrencies.Add(item);
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
