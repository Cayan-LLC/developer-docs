import urllib3
import lxml.etree as etree
import lxml.objectify as objectify

# Declare IP Address
ip_address = "192.168.0.123"
# Generate XML and XSD Validation
genius_schema = etree.XMLSchema(file='Genius.xsd')
xml_parser = objectify.makeparser(schema=genius_schema)
# Send Status Request
genius_comm = urllib3.PoolManager()
genius_request_url = "http://%s:8080/v2/pos?Action=Status&Format=XML" % ip_address
genius_response = genius_comm.request("GET", genius_request_url).data
# Validate the response with the Genius XSD
genius_response_data = objectify.fromstring(genius_response, xml_parser)
print("Status: %s" % genius_response_data.Status)
print("Current Screen: %s" % genius_response_data.CurrentScreen)
print("Response Message: %s" % genius_response_data.ResponseMessage)
print("Serial Number: %s" % genius_response_data.SerialNumber)
print("Application Version: %s" % genius_response_data.ApplicationVersion)

input("Press Enter to close")