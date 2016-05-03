using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using PhoneBookWinMobileApp.Models;

namespace PhoneBookWinMobileApp
{
    public partial class CreatePage : PhoneApplicationPage
    {
        public CreatePage()
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            string URL = "http://studentphonebook.azurewebsites.net/api/StudentWebApi";
            Student newStudent = new Student
            {
                id = "",
                Surname = this.Surname.Text,
                Name = this.Name.Text,
                Age = int.Parse(this.Age.Text),
                PhoneNumber = ulong.Parse(this.PhoneNumber.Text),
                Location = this.Location.Text
            };

            string studentJson = JsonConvert.SerializeObject(newStudent);
            WebClient webClient = new WebClient();
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            webClient.UploadStringAsync(new Uri(URL), "POST", studentJson);
            App.ViewModel.IsDataLoaded = false;
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}