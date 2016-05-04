using System;
using System.Net;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Threading;

namespace PhoneBookWinMobileApp
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        private string selectedIndex = "";
        // Constructor
        public DetailsPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DataContext == null)
            {
                if (NavigationContext.QueryString.TryGetValue("selectedItem", out this.selectedIndex))
                {
                    int index = int.Parse(selectedIndex);
                    DataContext = App.ViewModel.Items[index];
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (!selectedIndex.Equals(""))
            {
                int index = int.Parse(selectedIndex);
                string id = App.ViewModel.Items[index].ID;
                string URL = "http://studentphonebook.azurewebsites.net/api/StudentWebApi/" + id;
                WebRequest request = WebRequest.Create(URL);
                request.Method = "DELETE";
                request.GetResponseAsync();
                Thread.Sleep(1000);
                App.ViewModel.IsDataLoaded = false;
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

            }
        }

    }
}