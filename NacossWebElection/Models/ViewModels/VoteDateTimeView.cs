using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace NacossWebElection.Models.ViewModels
{
    public class VoteDateTimeView
    {
       public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }

        public int Hour { get; set; }
        public int minute { get; set; }

        public string period { get; set; }


        public int sday { get; set; }
        public int smonth { get; set; }
        public int syear { get; set; }

        public int sHour { get; set; }
        public int sminute { get; set; }

        public string speriod { get; set; }
    }
}