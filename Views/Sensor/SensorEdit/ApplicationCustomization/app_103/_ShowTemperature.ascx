<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%bool FullNotiString = CurrentZeroToOneFiftyAmp.GetShowFullDataValue(Model.SensorID);%>

<%if (!Model.IsWiFiSensor)
  {%>
<hr style="color:lightgray; opacity: 0.4;"/>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3 454545">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Show Temperature","Show Temperature")%>
    </div>
    <div  class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label id="off" class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <label id="on" class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
            <input class="form-check-input my-0 y-0 mx-2" onclick="onOffToggle2()" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="fullChk" id="fullChk" <%=CurrentZeroToOneFiftyAmp.GetShowFullDataValue(Model.SensorID) ? "checked" : "" %>>
        </div>
        <div style="display: none;"><%: Html.TextBoxFor(model => FullNotiString, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
    </div>
</div>

<script type="text/javascript">
    let toggleit = document.getElementById("fullChk");
    let off1 = document.getElementById("off");
    let on1 = document.getElementById("on");

    $(document).ready(function () {

    //setTimeout("$('#fullChk').iButton();", 500);
    $('#fullChk').change(function () {
        if ($('#fullChk').prop('checked')) {
            $('#FullNotiString').val(1);
        } else {
            $('#FullNotiString').val(0);
        }
    });

    });
    function onOffToggle2() {
        if (toggleit.checked == true) {
            off1.style.display = "none";
            on1.style.display = "block";
        } else {
            on1.style.display = "none";
            off1.style.display = "block";
        }
        console.log('value', toggleit.checked);
    };
    onOffToggle2()
</script>
<%}%>