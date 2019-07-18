using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace NacossWebElection.Models.ViewModels
{
    public class voters
    {
        [Required (ErrorMessage ="Required")]
        [Display(Name ="Matriculation Number")]
        public string MatNo { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Current Level")]
        public string CurrentLevel { get; set; }
    }
}