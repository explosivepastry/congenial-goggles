<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|SRX Time Out","SRX Time Out")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Maximum amount of time to wait after receiving data on the serial port to transmit the data to the gateway.", "Maximum amount of time to wait after receiving data on the serial port to transmit the data to the gateway.")%> 
        <hr />
    </div>
</div>


