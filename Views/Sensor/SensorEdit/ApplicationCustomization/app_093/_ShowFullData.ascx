<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
        bool FullNotiString = CurrentZeroToTwentyAmp.GetShowFullDataValue(Model.SensorID);          
%>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3 1111">
       <%: Html.TranslateTag("Show Full Data Value","Show Full Data Value")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label id="dataOff" class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <label id="dataOn" class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
            <input class="form-check-input my-0 y-0 mx-2"  onclick="onOffToggleIt()" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="fullChk" id="fullChk" <%= FullNotiString ? "checked" : "" %>>
        </div>
        <div style="display: none;">
            <%: Html.TextBoxFor(model => FullNotiString, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        </div>
    </div>
</div>

<script type="text/javascript">
    let offD = document.getElementById("dataOff");
    let onD = document.getElementById("dataOn");
    let dataToggle = document.getElementById("fullChk");
   
    //setTimeout("$('#fullChk').iButton();", 500);
    $('#fullChk').change(function () {
        if ($('#fullChk').prop('checked')) {
            $('#FullNotiString').val(1);
        } else {
            $('#FullNotiString').val(0);
        }
    });
    function onOffToggleIt() {
        if (dataToggle.checked == true) {
            offD.style.display = "none";
            onD.style.display = "block";
        } else {
            onD.style.display = "none";
            offD.style.display = "block";
        }

    };
    onOffToggleIt()
</script>
