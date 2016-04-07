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
                //if (Regex.IsMatch(value, "^[a-zA-Z][a-zA-Z0-9-_\\.]{1,20}$"))
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
                //if (Regex.IsMatch(value, "^[a-zA-Z][a-zA-Z0-9-_\\.]{1,20}$"))
                    name = value;
            }
        }

        public ushort Age
        {
            get
            {
                return age;
            }

            set
            {
                //if (value > 15 && value < 40)
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
                //if (Regex.IsMatch(value.ToString(), "^((8|\\+7)[\\- ]?)?(\\(?\\d{3}\\)?[\\- ]?)?[\\d\\- ]{7,10}$"))
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
                //if (Regex.IsMatch(value, "^[a-zA-Z][a-zA-Z0-9-_\\.]{1,60}$"))
                    location = value;
            }
        }

        [JsonProperty(PropertyName = "surname")]
        private string surname;

        [JsonProperty(PropertyName = "name")]
        private string name;

        [JsonProperty(PropertyName = "age")]
        private UInt16 age;

        [JsonProperty(PropertyName = "phoneNumber")]
        private UInt64 phoneNumber;

        [JsonProperty(PropertyName = "location")]
        private string location;
    }
}