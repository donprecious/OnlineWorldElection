using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace NacossWebElection.Models.ViewModels
{
    public class UploadView
    {
        [Display(Name = "ID")]
        [Required(ErrorMessage ="No ID Found")]
        public string ID { get; set; }
        public string photoUrl { get; set; }
    }
}