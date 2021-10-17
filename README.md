# Email Sender
Send Email with on DeliveryConfirmation and OnReadConfirmation

To send an email

1. Build the project
2. Create your email file at the root with the EmailSender.exe. file name is example.xml 
```` 
```
<?xml version="1.0"?>
<Settings xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" Log="true" DeliveryConfirmation="true" OnReadConfirmation="true">
  <From Host="Replace with your host smtp details" Port="587" useSSL="false">
    <Email><![CDATA[Replace with your email address]]></Email>
    <User><![CDATA[Replace with your email username. usually your email address]]></User>
    <Pass><![CDATA[Replace with your email account password]]></Pass>
  </From>
  <To><![CDATA[Replace with your email send to address]]></To>
  <Subject>Replace with your email subject</Subject>
  <Body><![CDATA[your message body in html. for example<br><strong>Hello World</strong>]]></Body>
</Settings>
```
````
3. send your message with "EmailSender.exe example.xml"
