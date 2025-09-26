<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CT Size","CT Size")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|This value is defined by the physical setting on the current transducer. There is a slide switch on the bottom of the transducer, the value entered in this field must match what the transducer switch is set to. Changing only the transducer or only this value will cause the sensor to report incorrect data.","This value is defined by the physical setting on the current transducer. There is a slide switch on the bottom of the transducer, the value entered in this field must match what the transducer switch is set to. Changing only the transducer or only this value will cause the sensor to report incorrect data.")%> 
        <hr />
    </div>
</div>


