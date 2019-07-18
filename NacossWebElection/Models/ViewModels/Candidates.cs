using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Infrastructure;
namespace NacossWebElection.Models.ViewModels
{
    public class Candidates
    {
       public List<Models.DBModel.Position> _positions { get; set; }

        [CheckMatNo (ErrorMessage ="Matriculation number Already Exist")]
        [MaxLength(30,ErrorMessage ="Maximum character is 30")]
        [MinLength(4, ErrorMessage ="Character must be more than 4")]
        [Required (ErrorMessage = "Matriculation Required")]
        [Display(Name ="Matriculation Number")]
        public string MatNo { get; set; }


        [MaxLength(30, ErrorMessage = "Maximum character is 30")]
        [MinLength(4, ErrorMessage = "Character must be more than 4")]
        [Required(ErrorMessage = "First Name Required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [MaxLength(30, ErrorMessage = "Maximum character is 30")]
        [MinLength(4, ErrorMessage = "Character must be more than 4")]
        [Required(ErrorMessage = "Last Name Required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Gender Required")]
        [Display(Name = "Sex")]
        public string Sex { get; set; }


        [Required(ErrorMessage = "Select position")]
        [Display(Name = "Position")]
        public int PositionID { get; set; }
        
        public IEnumerable<SelectListItem> Positions {
            //get { return new SelectList(_positions,"PositionID","Position1"); }
            get;set;
        }


        [Required(ErrorMessage = " Required")]
        [Display(Name = "Current Level")]
        public string CurrentLevel { get; set; }

       
        [Required(ErrorMessage = " Required")]
        [Display(Name = "Gpa")]
        public decimal Gpa { get; set; }

       
        [MinLength(10, ErrorMessage = "Character must be more than 10")]
        [Required(ErrorMessage = " Required")]
        [Display(Name = "Manifestor")]
        public string Manifestor { get; set; }
    }
}