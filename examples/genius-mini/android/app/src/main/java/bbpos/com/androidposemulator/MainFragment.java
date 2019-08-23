package bbpos.com.androidposemulator;

import android.app.Fragment;
import android.content.Intent;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.support.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.Toast;

import org.ksoap2.serialization.PropertyInfo;
import org.ksoap2.serialization.SoapObject;

import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import bbpos.com.androidposemulator.webservice.CallWebServiceAsyncTask;
import bbpos.com.androidposemulator.webservice.PostWebServerResult;

public class MainFragment extends Fragment implements View.OnClickListener, PostWebServerResult, AdapterView.OnItemSelectedListener {
    SoapObject soapObject;
    EditText et_amount;
    Defines.KeyedEntryPaymentType keyedEntryPaymentType = Defines.KeyedEntryPaymentType.NIL;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_main, container, false);

    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        et_amount = view.findViewById(R.id.amount);
        Button btn_send = view.findViewById(R.id.sale);
        Button btn_keyed_entry = view.findViewById(R.id.keyed_entry);
        Button btn_cancel = view.findViewById(R.id.cancel);

        btn_send.setOnClickListener(this);
        btn_keyed_entry.setOnClickListener(this);
        btn_cancel.setOnClickListener(this);

        // Spinner element
        Spinner spinner = view.findViewById(R.id.keyed_entry_payment_type_spinner);

        // Spinner click listener
        spinner.setOnItemSelectedListener(this);

        // Spinner Drop down elements
        List<String> categories = new ArrayList<String>();
        categories.add(Defines.KeyedEntryPaymentType.NIL.name());
        categories.add(Defines.KeyedEntryPaymentType.GIFT.name());

        // Creating adapter for spinner
        ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>(getActivity(), android.R.layout.simple_spinner_item, categories);

        // Drop down layout style - list view with radio button
        dataAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);

        // attaching data adapter to spinner
        spinner.setAdapter(dataAdapter);
    }

    private SoapObject createSaleSoapObject(){
        String METHOD_NAME = "CreateTransaction";
        String NAMESPACE = "http://transport.merchantware.net/v4/";

        SoapObject Request = new SoapObject(NAMESPACE, METHOD_NAME);
        Request.addProperty("merchantName", PreferenceManager.getDefaultSharedPreferences(getActivity().getApplicationContext()).getString("merchantName", "XXX Test MerchantDBA XXX"));
        Request.addProperty("merchantSiteId", PreferenceManager.getDefaultSharedPreferences(getActivity().getApplicationContext()).getString("merchantSiteId", "XXXXXX"));
        Request.addProperty("merchantKey", PreferenceManager.getDefaultSharedPreferences(getActivity().getApplicationContext()).getString("merchantKey", "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"));

        PropertyInfo HealthCareAmountDetails = new PropertyInfo();
        HealthCareAmountDetails.setName("HealthCareAmountDetails");
        HealthCareAmountDetails.setValue(new SoapObject(NAMESPACE, "HealthCareAmountDetails")
                .addProperty("CopayAmount", "0")
                .addProperty("ClinicalAmount", "0")
                .addProperty("DentalAmount", "0")
                .addProperty("HealthCareTotalAmount", "0")
                .addProperty("PrescriptionAmount", "0")
                .addProperty("VisionAmount", "0"));

        PropertyInfo pi = new PropertyInfo();
        pi.setName("request");
        pi.setValue(new SoapObject(NAMESPACE, "TransportRequest")
                .addProperty("TransactionType", "SALE")
                .addProperty("Amount", et_amount.getText().toString())
                .addProperty("ClerkId", "User1")
                .addProperty("OrderNumber", "1234")
                .addProperty("Dba", "POS Demo")
                .addProperty("SoftwareName", "POS Demo")
                .addProperty("SoftwareVersion", "1.0.0")
                .addProperty("TransactionId", "")
                .addProperty("ForceDuplicate", true)
                .addProperty("TaxAmount", "0")
                .addProperty("EntryMode", "Undefined")
                .addProperty("AuthorizationCode", "")
                .addProperty(HealthCareAmountDetails)
                .addProperty("EnablePartialAuthorization", true));

        Request.addProperty(pi);

        return Request;
    }

    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.sale:
                try
                {
                    String x = et_amount.getText().toString();
                    BigDecimal tmp = new BigDecimal(x);
                    if (tmp.compareTo(BigDecimal.ZERO)>0) {
                        soapObject = createSaleSoapObject();
                        CallWebServiceAsyncTask task = new CallWebServiceAsyncTask(this, soapObject);
                        task.execute();
                    } else
                        Toast.makeText(getActivity().getApplicationContext(), "Invalid Amount", Toast.LENGTH_LONG).show();
                }
                catch (Exception e)
                {
                    Toast.makeText(getActivity().getApplicationContext(), "Invalid Amount", Toast.LENGTH_LONG).show();
                }
                break;
            case R.id.keyed_entry:
                sendKeyedEntry(keyedEntryPaymentType);
                break;
            case R.id.cancel:
                sendCancel();
                break;
        }
    }

    private void sendKeyedEntry(Defines.KeyedEntryPaymentType paymentType) {
        String URI = PreferenceManager.getDefaultSharedPreferences(getActivity().getApplicationContext()).getString("keyedEntryVer", getString(R.string.version1)) + "/pos";

        Intent intent = new Intent(MainActivity.REQUEST_ACTION);
        intent.putExtra("ResultReceiverID",MainActivity.RECEIVERID);
        intent.putExtra("Action", "InitiateKeyedEntry");
        intent.putExtra("PaymentType", paymentType.name());
        intent.putExtra("Format", PreferenceManager.getDefaultSharedPreferences(getActivity().getApplicationContext()).getString("format", getString(R.string.XML)));
        intent.putExtra("URI", URI);
        getActivity().sendBroadcast(intent);
    }

    private void sendCancel() {
        String URI = PreferenceManager.getDefaultSharedPreferences(getActivity().getApplicationContext()).getString("cancelVer", getString(R.string.version1)) + "/pos";

        Intent intent = new Intent(MainActivity.REQUEST_ACTION);
        intent.putExtra("ResultReceiverID",MainActivity.RECEIVERID);
        intent.putExtra("Action", "Cancel");
        intent.putExtra("Format", PreferenceManager.getDefaultSharedPreferences(getActivity().getApplicationContext()).getString("format", getString(R.string.XML)));
        intent.putExtra("URI", URI);
        getActivity().sendBroadcast(intent);
    }


    @Override
    public void onResult(HashMap<String, String> params) {
        String URI = PreferenceManager.getDefaultSharedPreferences(getActivity().getApplicationContext()).getString("saleVer", getString(R.string.version2)) + "/pos";

        Intent intent = new Intent(MainActivity.REQUEST_ACTION);
        intent.putExtra("ResultReceiverID",MainActivity.RECEIVERID);
        intent.putExtra("TransportKey", params.get("TransportKey"));
        intent.putExtra("ValidationKey", params.get("ValidationKey"));
        intent.putExtra("Format", PreferenceManager.getDefaultSharedPreferences(getActivity().getApplicationContext()).getString("format", getString(R.string.XML)));
        intent.putExtra("URI", URI);
        getActivity().sendBroadcast(intent);
    }

    @Override
    public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
        String tmp = adapterView.getItemAtPosition(i).toString();
        keyedEntryPaymentType = Defines.KeyedEntryPaymentType.valueOf(tmp);
    }

    @Override
    public void onNothingSelected(AdapterView<?> adapterView) {
        keyedEntryPaymentType = Defines.KeyedEntryPaymentType.NIL;
    }
}
