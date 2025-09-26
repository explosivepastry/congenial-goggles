<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
        bool FullNotiString = SiteSurvey.GetShowFullDataValue(Model.SensorID);          
%>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
       <%: Html.TranslateTag("SensorEdit/app_143|Show Advanced Data")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label id="AdataOff" class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <label id="AdataOn" class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
            <input onclick="onOffToggle3()" class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="fullChk" id="fullChk" <%= FullNotiString ? "checked" : "" %>>
        </div>
          <div style="display: none;"><%: Html.TextBoxFor(model => FullNotiString, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
    </div>
</div>

<script type="text/javascript">
    let dataOff = document.getElementById("AdataOff");
    let dataOn = document.getElementById("AdataOn");
    let DataToggle = document.getElementById("fullChk");
   
    //setTimeout("$('#fullChk').iButton();", 500);
    $('#fullChk').change(function () {
        if ($('#fullChk').prop('checked')) {
            $('#FullNotiString').val(1);
        } else {
            $('#FullNotiString').val(0);
        }
    });
    function onOffToggle3() {
        if (DataToggle.checked == true) {
            dataOff.style.display = "none";
            dataOn.style.display = "block";
        } else {
           dataOn.style.display = "none";
            dataOff.style.display = "block";
        }
        /* console.log("value", accuToggle22.checked)*/
    };
    onOffToggle3()
</script>
