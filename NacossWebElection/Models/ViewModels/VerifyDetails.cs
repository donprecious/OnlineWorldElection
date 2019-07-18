using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace NacossWebElection.Models.ViewModels
{
    public class VerifyDetails
    {
        [Display(Name = "Matriculation Number ")]
        [Required(ErrorMessage ="Matriculation Number is Required")]
        public string matno { get; set; }

        [Required(ErrorMessage = "First Name (Surname) is Required")]
        [Display (Name ="First Name ")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name (Surname) ")]
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number used in registration ")]
        [Required(ErrorMessage = "Phone number is Required")]
        public string PhoneNo { get; set; }
    }
}