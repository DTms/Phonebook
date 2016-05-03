using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace PhoneBookWinMobileApp.Models
{
    class Student
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

        public override bool Equals(Object obj)
        {

            if (obj is Student)
            {
                var s = obj as Student;
                return (s.age == age) && (s.id == id) && (s.location == location) && (s.name == name) &&
            (s.surname == surname) && (s.phoneNumber == phoneNumber);
            }
            return false;
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
