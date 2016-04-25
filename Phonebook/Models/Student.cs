using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace Phonebook.Models
{
    public class Student
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [Required]
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

        [Required]
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

        [Required]
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

        [Required]
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

        [Required]
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

        [Required]
        [JsonProperty(PropertyName = "surname")]
        private string surname;

        [Required]
        [JsonProperty(PropertyName = "name")]
        private string name;

        [Required]
        [JsonProperty(PropertyName = "age")]
        private int age;

        [Required]
        [JsonProperty(PropertyName = "phoneNumber")]
        private UInt64 phoneNumber;

        [Required]
        [JsonProperty(PropertyName = "location")]
        private string location;
    }
}