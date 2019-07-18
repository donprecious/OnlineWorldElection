using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NacossWebElection.Models.DBModel;
using System.Web.Mvc;

namespace Infrastructure
{
    //Custom attribute validations 
    //VALIDATE IF AN EMAIL ALREADY EXIST
    public class CheckMatNo : ValidationAttribute, IClientValidatable
    {
        public CheckMatNo() : base("{0} Already Exit")
        {

        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var db = new NacossVotingDBEntities();
               
                var result = db.Candidates.Find(value.ToString());
                if (result != null)
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    //return error that user already exit with that email
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage =
            FormatErrorMessage(metadata.GetDisplayName());

            rule.ValidationType = "checkemail";
            yield return rule;
        }
        //  private readonly string _email;


    }

}