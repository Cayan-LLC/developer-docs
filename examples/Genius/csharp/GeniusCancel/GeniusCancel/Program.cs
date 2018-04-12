using System;
using System.IO;
using System.Net;
using System.Xml.Serialization;

namespace GeniusCancel
{
    public class Program
    {
        private static void Main(string[] args)
        {
            string ipAddress = "192.168.0.123";

            // Initiate transaction with TransportKey
            WebRequest webReq = WebRequest.Create($"http://{ipAddress}:8080/v2/pos?Action=Cancel&Format=XML");
            using (WebResponse webResp = webReq.GetResponse())
            {
                using (Stream responseStream = webResp.GetResponseStream())
                {
                    // Validate XML to Genius XSD class
                    StreamReader streamReader = new StreamReader(responseStream);
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(CancelResult));
                    CancelResult transactionResult = (CancelResult)xmlSerializer.Deserialize(streamReader);
                    Console.WriteLine("Cancel Result: {0}", transactionResult.Status);
                    Console.WriteLine("Response Message: {0}", transactionResult.ResponseMessage);
                }
            }

            // Close application
            Console.WriteLine("Press Any Key to Close");
            Console.ReadKey();
        }
    }
}
