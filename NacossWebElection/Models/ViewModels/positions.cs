using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NacossWebElection.Models.ViewModels
{
    public class positions
    {
        [Required(ErrorMessage ="Required")]
        [Display(Name ="Postition Name")]
        public string position { get; set; }
        
    }
}