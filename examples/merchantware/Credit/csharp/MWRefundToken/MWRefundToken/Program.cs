using System;
using MWRefundToken.MWCredit;

namespace MWRefundToken
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // Create Soap Client
            CreditSoapClient soapClient = new CreditSoapClient("CreditSoap");
            // Create MerchantCredentails object
            MerchantCredentials merchantCredentials = new MerchantCredentials
            {
                MerchantName = "TEST MERCHANT",
                MerchantSiteId = "XXXXXXXX",
                MerchantKey = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
            };
            // Create PaymentData object
            PaymentData paymentData = new PaymentData
            {
                Source = "PreviousTransaction",
                Token = "1234567890"
            };
            // Create RefundRequest Object
            RefundRequest refundRequest = new RefundRequest
            {
                Amount = "1.01",
                InvoiceNumber = "INV1234",
                CardAcceptorTerminalId = "01"
            };
            // Run Refund
            TransactionResponse45 refundResponse = soapClient.Refund(merchantCredentials, paymentData, refundRequest);
            // Print Results
            Console.WriteLine("Refund Response: {0} Token: {1} Amount: ${2}", refundResponse.ApprovalStatus, refundResponse.Token, refundResponse.Amount);
            Console.WriteLine("Press Any Key to Close");
            Console.ReadKey();
        }
    }
}
