using System;
using System.Collections.Generic;
using System.Linq;
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
using TestTaskApi.ViewModels;

namespace TestTaskApi.Pages
{
	/// <summary>
	/// Логика взаимодействия для Exchange.xaml
	/// </summary>
	public partial class Exchange : Page
	{
		public Exchange()
		{
			InitializeComponent();
			ExchangeViewModel vm = new ExchangeViewModel();
			DataContext = vm;
		}
	}
}
