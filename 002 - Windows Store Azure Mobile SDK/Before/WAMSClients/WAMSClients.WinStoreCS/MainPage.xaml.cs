using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WAMSClients.WinStoreCS
{
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class MainPage : Page
  {
    public MainPage()
    {
      this.InitializeComponent();
    }

    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
      RefreshData();
    }

    async private void RefreshData()
    {
      LoadingDataRing.IsActive = true;
      var demoTable = App.MobileService.GetTable<DemoTable>();
      var query = demoTable.CreateQuery();
      DemoTableListView.ItemsSource = await query.ToListAsync();
      LoadingDataRing.IsActive = false;
    }

    private async void InsertButton_Click(object sender, RoutedEventArgs e)
    {
      DemoTable item = new DemoTable
      {
        FirstName = FirstNameTextBox.Text,
        LastName = LastNameTextBox.Text
      };

      LoadingDataRing.IsActive = true;

      await App.MobileService.GetTable<DemoTable>().InsertAsync(item);

      FirstNameTextBox.Text = "";
      LastNameTextBox.Text = "";

      RefreshData();
    }

  }
}
