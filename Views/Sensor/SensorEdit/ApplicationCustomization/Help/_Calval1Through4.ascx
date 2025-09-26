<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Baud Rate","Baud Rate")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Number of data bits.", "Number of data bits.")%> 
        <hr />
    </div>
</div>
<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Data Bits","Data Bits")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Delay between powering the transducer on and taking the measurement.", "Delay between powering the transducer on and taking the measurement.")%> 
        <hr />
    </div>
</div>
<%--<div class="row">
    <div class="bold col-md-12 col-sm-12 col-xs-12">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Parity","Parity")%>
    </div>
     <div class="col-md-12 col-sm-12 col-xs-12" style="font-size: 0.8em;">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Determines which reading of the sensor will trigger the Aware State.","Determines which reading of the sensor will trigger the Aware State.")%> 
        <hr />
    </div>
</div>--%>
<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Stop Bits","Stop Bits")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Delay between powering the transducer on and taking the measurement.", "Delay between powering the transducer on and taking the measurement.")%> 
        <hr />
    </div>
</div>
<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Packet Size","Packet Size")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Number of bytes in a radio transmission packet.", "Number of bytes in a radio transmission packet.")%> 
        <hr />
    </div>
</div>


