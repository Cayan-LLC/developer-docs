<?php
    header("Content-Type: application/json");
    $client = new SoapClient("https://ps1.merchantware.net/Merchantware/ws/RetailTransaction/v45/Credit.asmx?WSDL");
    $response = $client->Refund(
        array (
            "Credentials" => array (
                "MerchantName"   => "TEST MERCHANT",
                "MerchantSiteId" => "XXXXXXXX",
                "MerchantKey"    => "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
                ),
            "PaymentData" => array (
                "Source"                => "Keyed",
                "CardNumber"            => "4012000033330026",
                "ExpirationDate"        => "1220",
                "CardHolder"            => "John Doe",
                "AvsStreetAddress"      => "1 Federal St",
                "AvsZipCode"            => "02110",
                "CardVerificationValue" => "123",
                ),
            "Request" => array (
                "Amount"                     => "1.01",
                "InvoiceNumber"              => "INV1234",
                "CardAcceptorTerminalId"     => "01",
                )
            )
        );
    print_r(json_encode($response->RefundResult));
?>