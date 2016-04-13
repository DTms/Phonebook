using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Phonebook.Models
{
    public class Student
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        public string Surname
        {
            get
            {
                return surname;
            }

            set
            {
                    surname = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                    name = value;
            }
        }

        public int Age
        {
            get
            {
                return age;
            }

            set
            {
                    age = value;
            }
        }

        public ulong PhoneNumber
        {
            get
            {
                return phoneNumber;
            }

            set
            {
                    phoneNumber = value;
            }
        }

        public string Location
        {
            get
            {
                return location;
            }

            set
            {
                    location = value;
            }
        }

        [JsonProperty(PropertyName = "surname")]
        private string surname;

        [JsonProperty(PropertyName = "name")]
        private string name;

        [JsonProperty(PropertyName = "age")]
        private int age;

        [JsonProperty(PropertyName = "phoneNumber")]
        private UInt64 phoneNumber;

        [JsonProperty(PropertyName = "location")]
        private string location;
    }
}