using System;
using MWRefundKeyed.MWCredit;

namespace MWRefundKeyed
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create Soap Client
            CreditSoapClient soapClient = new CreditSoapClient("CreditSoap");
            //Create MerchantCredentails object
            MerchantCredentials merchantCredentials = new MerchantCredentials
            {
                MerchantName = "TEST MERCHANT",
                MerchantSiteId = "XXXXXXXX",
                MerchantKey = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
            };
            //Create PaymentData object
            PaymentData paymentData = new PaymentData
            {
                Source = "Keyed",
                CardNumber = "4012000033330026",
                ExpirationDate = "1220",
                CardHolder = "John Doe",
                AvsStreetAddress = "1 Federal St",
                AvsZipCode = "02110",
                CardVerificationValue = "123"
            };
            //Create RefundRequest Object
            RefundRequest refundRequest = new RefundRequest
            {
                Amount = "1.01",
                InvoiceNumber = "1234",
                CardAcceptorTerminalId = "01"
            };

            //Run Refund
            TransactionResponse45 refundResponse = soapClient.Refund(merchantCredentials, paymentData, refundRequest);
            //Print Results
            Console.WriteLine(" Sale Response: {0}{3} Token: {1}{3} Amount: ${2}{3}", refundResponse.ApprovalStatus, refundResponse.Token, refundResponse.Amount, Environment.NewLine);
            Console.WriteLine("Press Any Key to Close");
            Console.ReadKey();
        }
    }
}
