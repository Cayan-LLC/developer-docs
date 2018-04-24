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
                "Source"    => "Reader",
                "TrackData" => "%B4012000033330026^TEST CARD/GENIUS^181210054321000000000000000 150 A?;4012000033330026=18121011000012345678?"
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