﻿using System;
using MWSaleSwiped.MWCredit;

namespace MWSaleSwiped
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
                Source = "Reader",
                TrackData = "%B4012000033330026^TEST CARD/GENIUS^181210054321000000000000000 150 A?;4012000033330026=18121011000012345678?",
            };
            // Create SaleRequest Object
            SaleRequest saleRequest = new SaleRequest
            {
                Amount = "1.01",
                TaxAmount = "0.10",
                InvoiceNumber = "INV1234",
                CardAcceptorTerminalId = "01",
                CustomerCode = "1234",
                PurchaseOrderNumber = "PO1234",
                EnablePartialAuthorization = "true"

            };
            // Run Sale
            TransactionResponse45 saleResponse = soapClient.Sale(merchantCredentials, paymentData, saleRequest);
            // Print Results
            Console.WriteLine("Sale Response: {0} Token: {1} Amount: ${2}", saleResponse.ApprovalStatus, saleResponse.Token, saleResponse.Amount);
            Console.WriteLine("Press Any Key to Close");
            Console.ReadKey();
        }
    }
}
