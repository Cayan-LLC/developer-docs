using System;
using System.IO;
using System.Net;
using System.Xml.Serialization;

namespace GeniusStatus
{
    public class Program
    {
        private static void Main(string[] args)
        {
            string ipAddress = "192.168.0.123";

            // Request Status of Device
            WebRequest webReq = WebRequest.Create($"http://{ipAddress}:8080/v2/pos?Action=Status&Format=XML");
            using (WebResponse webResp = webReq.GetResponse())
            {
                using (Stream responseStream = webResp.GetResponseStream())
                {
                    // Validate XML to Genius XSD class
                    StreamReader streamReader = new StreamReader(responseStream);
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(StatusResult));
                    StatusResult transactionResult = (StatusResult)xmlSerializer.Deserialize(streamReader);
                    Console.WriteLine("Status: {0}", transactionResult.Status);
                    Console.WriteLine("Current Screen: {0}", transactionResult.CurrentScreen);
                    Console.WriteLine("Response Message: {0}", transactionResult.ResponseMessage);
                    Console.WriteLine("Serial Number: {0}", transactionResult.SerialNumber);
                    Console.WriteLine("Application Version: {0}", transactionResult.ApplicationVersion);
                }
            }

            // Close application
            Console.WriteLine("Press Any Key to Close");
            Console.ReadKey();
        }
    }
}
