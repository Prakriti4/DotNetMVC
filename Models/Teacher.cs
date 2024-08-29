﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModelTableFormation.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Gmail { get; set; }  

        public string PhoneNumber { get; set; } 

        public string Address { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        public string ImageUrl { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }


    }
}
