using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using PhoneBookWinMobileApp.Resources;
using System.Net;
using Newtonsoft.Json;
using PhoneBookWinMobileApp.Models;

namespace PhoneBookWinMobileApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        const string apiUrl = @"http://studentphonebook.azurewebsites.net/api/StudentWebApi";
        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }



        /// <summary>
        /// Sample property that returns a localized string
        /// </summary>
        public string LocalizedSampleProperty
        {
            get
            {
                return AppResources.SampleProperty;
            }
        }

        public bool IsDataLoaded
        {
            get;
            set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            if (this.IsDataLoaded == false)
            {
                this.Items.Clear();
                this.Items.Add(new ItemViewModel() { IdFromList = "0", ID = "0", Surname = "Please Wait...", Name = "Please wait while the list of Student records are downloaded from the server.", Age = null });
                WebClient webClient = new WebClient();
                webClient.Headers["Accept"] = "application/json";
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStudentsCompleted);
                webClient.DownloadStringAsync(new Uri(apiUrl));
            }
        }

        private void webClient_DownloadStudentsCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                this.Items.Clear();
                if (e.Result != null)
                {
                    var students = JsonConvert.DeserializeObject<Student[]>(e.Result);
                    int id = 0;
                    foreach (Student student in students)
                    {
                        this.Items.Add(new ItemViewModel()
                        {
                            IdFromList = (id++).ToString(),
                            ID = student.id,
                            Surname = student.Surname,
                            Name = student.Name,
                            Age = student.Age.ToString(),
                            PhoneNumber = student.PhoneNumber.ToString(),
                            Location = student.Location
                        });
                    }
                    this.IsDataLoaded = true;
                }
            }
            catch (Exception ex)
            {
                this.Items.Add(new ItemViewModel()
                {
                    IdFromList = "0",
                    ID = "0",
                    Surname = "An Error Occurred",
                    Name = String.Format("The following exception occured: {0}", ex.Message),
                    Age = String.Format("Additional inner exception information: {0}", ex.InnerException.Message),
                    PhoneNumber = String.Format("Additional inner exception information: {0}", ex.InnerException.Message),
                    Location = String.Format("Additional inner exception information: {0}", ex.InnerException.Message)
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}