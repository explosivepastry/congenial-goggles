<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
        string label = "";
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);
        bool FullNotiString = CurrentZeroToTwentyAmp.GetShowFullDataValue(Model.SensorID);          
%>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3 111">
       <%: Html.TranslateTag("Show Full Data Value","Show Full Data Value")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label id="FDOff"  class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <label id="FDOn" class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
            <input class="form-check-input my-0 y-0 mx-2"onclick="onOffToggle3()"  type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="fullChk" id="fullChk" <%= CurrentZeroToTwentyAmp.GetShowFullDataValue(Model.SensorID) ? "checked" : "" %>>
        </div>
          <div style="display: none;"><%: Html.TextBoxFor(model => FullNotiString, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
    </div>
</div>

<script type="text/javascript">
    let off1 = document.getElementById("FDOff");
    let on1 = document.getElementById("FDOn");
    let accuToggle22 = document.getElementById("fullChk");

    //setTimeout("$('#fullChk').iButton();", 500);
    $('#fullChk').change(function () {
        if ($('#fullChk').prop('checked')) {
            $('#FullNotiString').val(1);
        } else {
            $('#FullNotiString').val(0);
        }
    });
    function onOffToggle3() {
        if (accuToggle22.checked == true) {
            off1.style.display = "none";
            on1.style.display = "block";
        } else {
            on1.style.display = "none";
            off1.style.display = "block";
        }
        /* console.log("value", accuToggle22.checked)*/
    };
    onOffToggle3()
</script>
