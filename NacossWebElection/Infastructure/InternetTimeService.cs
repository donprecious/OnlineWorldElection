//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Net.Sockets;
//using System.Web;

//namespace NacossWebElection.Infastructure
//{
//    public class InternetTimeService
//    {
//        public static DateTime GetNistTime()
//        {
//            var client = new TcpClient("time.nist.gov", 13);
//            using (var streamReader = new StreamReader(client.GetStream()))
//            {
//                var response = streamReader.ReadToEnd();
//                var utcDateTimeString = response.Substring(7, 17);
//                return DateTime.ParseExact(utcDateTimeString, "MM/dd/yy HH:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
//            }
//        }
//    }
//}