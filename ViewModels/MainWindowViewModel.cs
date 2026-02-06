using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestTaskApi.ViewModels
{
	internal class MainWindowViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;


		private void OnPropertyChanged([CallerMemberName] string prop = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		private Page Home;
		private Page Exchange;
		private Page Search;
		
		private Page _currentPage;

		public Page CurrentPage
		{
			get { return _currentPage; }
			set
			{
				_currentPage = value;
				OnPropertyChanged();
			}
		}

		public MainWindowViewModel()
		{
			Home = new Pages.Home();
			Exchange = new Pages.Exchange();
			Search = new Pages.Search();
			
			_currentPage = Home;
		}

		public ICommand HomeCommand => new RelayCommand(_ => CurrentPage = Home);
		public ICommand ExchangeCommand => new RelayCommand(_ => CurrentPage = Exchange);
		public ICommand SearchCommand => new RelayCommand(_ => CurrentPage = Search);



	}
}
