﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Bigschool_TH_11.Models;

namespace Bigschool_TH_11.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Place { get; set; }
        [Required]
        [FutureDate]
        public string Date { get; set; }
        [Required]
        [ValidTime]
        public string Time { get; set; }
        [Required]
        public byte Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public DateTime GetDateTime() 
        { 
            return DateTime.Parse(string.Format("{0} {1}", Date, Time)); 
        }
        public String Heading { get; set; }
        public String Action 
        {
            get { return Id != 0 ? "Update" : "Create"; } 
        }
    }
}