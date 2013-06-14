using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MvcDeganitSite.Models
{
    public class NavigationWord
    {
        [Key]
        [Required(ErrorMessage="נא הכנס שם למילת ניווט")]
        [MaxLength(100)]
        public string Name { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}