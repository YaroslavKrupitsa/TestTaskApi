using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TestTaskApi.Models;
using TestTaskApi.Services;

namespace TestTaskApi.ViewModels
{
	public class SearchViewModel : INotifyPropertyChanged
	{
		private readonly CoinMarketCapService _service =
			new CoinMarketCapService();

		private string _searchText;

		public string SearchText
		{
			get => _searchText;
			set
			{
				_searchText = value;
				OnPropertyChanged(nameof(SearchText));
				ApplyFilter();
			}
		}

		public ObservableCollection<CryptoCurrency> AllCurrencies { get; }
			= new ObservableCollection<CryptoCurrency>();

		public ObservableCollection<CryptoCurrency> FilteredCurrencies { get; }
			= new ObservableCollection<CryptoCurrency>();

		public SearchViewModel()
		{
			LoadAsync();
		}

		private async void LoadAsync()
		{
			var data = await _service.GetTopAsync(100);

			AllCurrencies.Clear();
			FilteredCurrencies.Clear();

			foreach (var item in data)
			{
				AllCurrencies.Add(item);
				FilteredCurrencies.Add(item);
			}
		}

		private void ApplyFilter()
		{
			FilteredCurrencies.Clear();

			if (string.IsNullOrWhiteSpace(SearchText))
			{
				foreach (var item in AllCurrencies)
					FilteredCurrencies.Add(item);
				return;
			}

			var text = SearchText.ToLower();

			var result = AllCurrencies.Where(c =>
				c.Name.ToLower().Contains(text) ||
				c.Symbol.ToLower().Contains(text));

			foreach (var item in result)
				FilteredCurrencies.Add(item);
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string name) =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
}
