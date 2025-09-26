<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: CreateAccount</b><br />
    <b>*Special Case: No Authorization Token Required*</b><br />
    Creates a new account<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>CompanyName:</td>
            <td>string</td>
            <td>Company Name / Account Number  used to lookup account</td>
        </tr>
        <tr>
            <td>TimeZoneID:</td>
            <td>integer</td>
            <td>Identifier of the timezone to view sensor data</td>
        </tr>
        <tr>
            <td>Address:</td>
            <td>string</td>
            <td>Street address for account</td>
        </tr>
        <tr>
            <td>Address2:</td>
            <td>string (optional)</td>
            <td>Second line of street address for account</td>
        </tr>
        <tr>
            <td>City:</td>
            <td>string</td>
            <td>City for account</td>
        </tr>
        <tr>
            <td>State:</td>
            <td>string</td>
            <td>State or Region for account</td>
        </tr>
        <tr>
            <td>PostalCode:</td>
            <td>string</td>
            <td>Postal Code for account</td>
        </tr>
        <tr>
            <td>Country:</td>
            <td>string</td>
            <td>Country for account</td>
        </tr>
        <tr>
            <td>ResellerID:</td>
            <td>Integer (optional)</td>
            <td>Identifier of reseller where hardware was purchased</td>
        </tr>
        <tr>
            <td>UserName:</td>
            <td>string</td>
            <td>Username for primary contact to log into system</td>
        </tr>
        <tr>
            <td>Password:</td>
            <td>string</td>
            <td>Password for primary contact to log into system</td>
        </tr>
        <tr>
            <td>ConfirmPassword:</td>
            <td>string</td>
            <td>Confirmation of password for primary contact to log into system</td>
        </tr>
        <tr>
            <td>FirstName:</td>
            <td>string</td>
            <td>First name of primary contact</td>
        </tr>
        <tr>
            <td>LastName:</td>
            <td>string</td>
            <td>Last name of primary contact</td>
        </tr>
        <tr>
            <td>NotificationEmail:</td>
            <td>string</td>
            <td>Email address of primary contact</td>
        </tr>
        <tr>
            <td>NotificationPhone:</td>
            <td>string (optional)</td>
            <td>Cell phone number to receive sms notifications</td>
        </tr>
        <tr>
            <td>SMSCarrierID:</td>
            <td>integer (optional)</td>
            <td>Identifier of cell phone carrier to receive sms notifications</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/CreateAccount?companyName=Monnit+Test&timeZoneID=1&address=7304+South+Cottonwood&address2=Suite+204&city=SLC&state=Utah&postalCode=74047&country=USA&userName=API.Tester&password=password&confirmPassword=password&firstName=API&lastName=Tester&notificationEmail=api.tester@monnit.com" target="_blank">https://<%=Request.Url.Host %>/xml/CreateAccount?companyName=Monnit+Test&amp;timeZoneID=1&amp;address=7304+South+Cottonwood&amp;address2=Suite+204&amp;city=SLC&amp; state=Utah&amp;postalCode=74047&amp;country=USA&amp;userName=API.Tester&amp;password=password&amp;confirmPassword=password&amp;firstName=API&amp; lastName=Tester&amp;notificationEmail=api.tester@monnit.com</a>
                
    <h4>Example Outputs</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;CreateAccount&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
------------------------------------------------------------------------------------------------------------------
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;CreateAccount&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIValidationErrorList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIValidationError Parameter="CompanyName" Error="Company name already exists"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIValidationError Parameter="UserName" Error="Username not available"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIValidationErrorList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>

</div>