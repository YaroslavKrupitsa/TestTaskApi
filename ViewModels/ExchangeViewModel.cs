using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TestTaskApi.Models;
using TestTaskApi.Services;

namespace TestTaskApi.ViewModels
{
	public class ExchangeViewModel : INotifyPropertyChanged
	{
		private readonly CoinMarketCapService _coinMarketCap = new CoinMarketCapService();

		public ObservableCollection<CryptoCurrency> Currencies { get; } = new ObservableCollection<CryptoCurrency>();

		private CryptoCurrency _fromCurrency;
		public CryptoCurrency FromCurrency
		{
			get => _fromCurrency;
			set
			{
				_fromCurrency = value;
				OnPropertyChanged(nameof(FromCurrency));
				UpdateConvertedAmount();
			}
		}

		private CryptoCurrency _toCurrency;
		public CryptoCurrency ToCurrency
		{
			get => _toCurrency;
			set
			{
				_toCurrency = value;
				OnPropertyChanged(nameof(ToCurrency));
				UpdateConvertedAmount();
			}
		}

		private decimal _amount;
		public decimal Amount
		{
			get => _amount;
			set
			{
				_amount = value;
				OnPropertyChanged(nameof(Amount));
				UpdateConvertedAmount();
			}
		}

		private decimal _convertedAmount;
		public decimal ConvertedAmount
		{
			get => _convertedAmount;
			set
			{
				_convertedAmount = value;
				OnPropertyChanged(nameof(ConvertedAmount));
			}
		}

		public ExchangeViewModel()
		{
			LoadCurrenciesAsync();
		}

		private async void LoadCurrenciesAsync()
		{
			try
			{
				var data = await _coinMarketCap.GetTopAsync(100);

				Currencies.Clear();
				foreach (var c in data)
					Currencies.Add(c);

				FromCurrency = Currencies.FirstOrDefault(c => c.Symbol == "BTC");
				ToCurrency = Currencies.FirstOrDefault(c => c.Symbol == "USD");
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"Currency download failure: {ex.Message}");
			}
		}

		private void UpdateConvertedAmount()
		{
			if (FromCurrency == null || ToCurrency == null || Amount <= 0)
			{
				ConvertedAmount = 0;
				return;
			}

			ConvertedAmount = Amount * (FromCurrency.PriceUsd / ToCurrency.PriceUsd);
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string name) =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
}