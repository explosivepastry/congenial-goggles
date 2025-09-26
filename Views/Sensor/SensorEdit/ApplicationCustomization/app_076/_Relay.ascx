<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    BasicControl Application = new BasicControl();
    Application.SetSensorAttributes(Model.SensorID);
    //If some validation failed and the page is re-displayed than the ViewData will be set for these attributes
    if (ViewData["Relay1Visibility"] != null)
        Application.Relay1VisibliityAttribute.Value = ViewData["Relay1Visibility"].ToString();
    if (ViewData["Relay1Name"] != null)
        Application.Relay1NameAttribute.Value = ViewData["Relay1Name"].ToString();

    string relayName = Application.Relay1NameAttribute.Value;
%>

<br />
<h4>Relay</h4>
<%--Relay1Name (SensorAttribute "Relay1Name")--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_076|Relay Title","Relay Title")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="text" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="Relay1Name" id="Relay1Name" value="<%=Application.Relay1NameAttribute.Value %>" />
    </div>
</div>

<%--Default State (Calibration1)--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_076|Default State","Default State")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label id="relayOff" class="form-check-label"><%: Html.TranslateTag("Off","Off")%>  </label>
            <label id="relayOn" class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
            <input value="<%=Model.Calibration1 > 0 ? "checked" : "" %>" class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="Cal1Chk" id="Cal1Chk" onclick="relayToggle()" <%=Model.Calibration1 > 0 ? "checked" : "" %>>
        </div>
        <div id="Cal1" style="display: none;"><%: Html.TextBoxFor(model => Model.Calibration1, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
    </div>
</div>

<%--Led Mode (Calibration4)--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_076|LED Mode","LED Mode")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="Cal4" class="form-select" name="Cal4">
            <option value="0" <%= Model.Calibration4 == 0 ?"selected='Selected'":"" %>>Off</option>
            <option value="1" <%= Model.Calibration4 == 1 ?"selected='Selected'":"" %>>On</option>
            <option value="2" <%= Model.Calibration4 == 2 ?"selected='Selected'":"" %>>On after Polling</option>
        </select>
    </div>
</div>

<script type="text/javascript">
    $("#Relay1Name").addClass('editField editFieldLarge');
    $("#Cal1Chk").addClass('editField editFieldMedium');
    $("#Cal4").addClass('editField editFieldMedium');

    //Delay because if not visible yet sizing gets all screwed up
    /* setTimeout("$('#Cal1Chk').iButton();", 500);*/
    <% if (Model.CanUpdate)
    { %>
    $(document).ready(function () {
        $('#Cal1Chk').change(function () {
            if ($('#Cal1Chk').prop('checked')) {
                $('#Calibration1').val(1);
            } else {
                $('#Calibration1').val(0);
            }
        });
    });
        <%}%>

    function relayToggle() {
        if (document.getElementById("Cal1Chk").checked) {
            document.getElementById("relayOff").style.display = "none";
        } else {
            document.getElementById("relayOff").style.display = "block";
        }
        if (document.getElementById("Cal1Chk").checked === false) {
            document.getElementById("relayOn").style.display = "none";
        } else {
            document.getElementById("relayOn").style.display = "block";
        }

    }
    relayToggle()
</script>
