using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;

namespace Reusable
{
  
    public class Generators
    {
        public string infoMsg="";
        private static Random random = new Random();
 private static Random rand = new Random(1);
       
        public static int RandomNumber()
        {
          int rnd_number = rand.Next(19012, 99900);
            return rnd_number;
        }
        public  int WordGen1(int length)
        {
            string chars = "0123456789";
            return Convert.ToInt32(new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray()));
        }
        public string WordGen(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

   
    }
    
}