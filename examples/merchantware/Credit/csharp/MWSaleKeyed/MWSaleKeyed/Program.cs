using System;
using MWSaleKeyed.MWCredit;

namespace MWSaleKeyed
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
            //Create SaleRequest Object
            SaleRequest saleRequest = new SaleRequest
            {
                Amount = "1.01",
                TaxAmount = "0.10",
                InvoiceNumber = "1234",
                CardAcceptorTerminalId = "01",
                CustomerCode = "1234",
                PurchaseOrderNumber = "1234",
                EnablePartialAuthorization = "true"

            };
            //Run Sale
            TransactionResponse45 saleResponse = soapClient.Sale(merchantCredentials, paymentData, saleRequest);
            //Print Results
            Console.WriteLine("Sale Response: {0} Token: {1} Amount: ${2}", saleResponse.ApprovalStatus, saleResponse.Token, saleResponse.Amount);
            Console.WriteLine("Press Any Key to Close");
            Console.ReadKey();
        }
    }
}
