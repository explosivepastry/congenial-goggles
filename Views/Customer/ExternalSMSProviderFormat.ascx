<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%if(Model != null && Model is SMSCarrier){ %>
       <%-- Click this link to send a test message to <a href="#" onclick="testSMS(); return false;"><%=string.Format(((SMSCarrier)Model).SMSFormatString,"<span class='displayNotificationPhone'>" + ViewData["Phone"].ToStringSafe().RemoveNonNumeric() + "</span>") %></a>--%>
       <%-- <br />--%>
        <span style="color:red;" class="expectedDigits"><%:((SMSCarrier)Model).SMSCarrierName %> requires a <%:((SMSCarrier)Model).ExpectedPhoneDigits %> digit format.</span>
        <script>
            var expectedDigits = <%:((SMSCarrier)Model).ExpectedPhoneDigits%>;
        </script>
<%} else { %>
<%--      <%:Html.TranslateTag("Customer/ExternalSMSProvidorFormat|SMS Provider","Number formats vary depending on SMS provider")%>.--%>
        <script>
            var expectedDigits = 0;
        </script>
<%}%>