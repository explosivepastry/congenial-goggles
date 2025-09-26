<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string label = "";
    MonnitApplicationBase.ProfileLabelForScale(Model, out label);

    Dictionary<string, object> dic = new Dictionary<string, object>();
    if (!Model.CanUpdate)
    {
        dic.Add("disabled", "disabled");
        ViewData["disabled"] = true;
    }

    ViewData["HtmlAttributes"] = dic;

    Control_1 Application = new Control_1();
    Application.SetSensorAttributes(Model.SensorID);
    //If some validation failed and the page is re-displayed than the ViewData will be set for these attributes
    if (ViewData["Relay1Visibility"] != null)
        Application.Relay1VisibliityAttribute.Value = ViewData["Relay1Visibility"].ToString();
    if (ViewData["Relay1Name"] != null)
        Application.Relay1NameAttribute.Value = ViewData["Relay1Name"].ToString();
    if (ViewData["Relay2Visibility"] != null)
        Application.Relay2VisibliityAttribute.Value = ViewData["Relay2Visibility"].ToString();
    if (ViewData["Relay2Name"] != null)
        Application.Relay2NameAttribute.Value = ViewData["Relay2Name"].ToString();

    string getDatumName0 = Model.GetDatumName(0) == "RelayState1" ? (Application.Relay1NameAttribute.Value) : (Model.GetDatumName(0));
    string getDatumName1 = Model.GetDatumName(1) == "RelayState2" ? (Application.Relay2NameAttribute.Value) : (Model.GetDatumName(1));

%>

<br />
<p class="useAwareState">Relay 1</p>
<%--Relay1Name (SensorAttribute "Relay1Name")--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_012|Relay 1 Title","Relay 1 Title")%>
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input type="text" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="datumName0" id="datumName0" value="<%=getDatumName0 %>" />
        <%: label %>
        <%: Html.ValidationMessageFor(model => getDatumName0)%>
    </div>
</div>

<%--Relay 1 Default State (Calibration1)--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_012|Default State","Default State")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label id="dualRelayOff" class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <label id="dualRelayOn" class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
            <input class="form-check-input my-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="Cal1Chk" id="Cal1Chk" <%=Model.Calibration1 > 0 ? "checked" : "" %> onclick="toggleDualRelay(R1toggle, dRoff, dRon)">
        </div>
        <div style="display: none;"><%: Html.TextBox("Cal1",Model.Calibration1, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
    </div>
</div>
<script type="text/javascript">
    $("#Datum0").addClass('editField editFieldLarge');
    $("#Cal3").addClass('editField editFieldMedium');
    $('#Cal1Chk').change(function () {
        if ($('#Cal1Chk').prop('checked')) {
            $('#Cal1').val(1);
        } else {
            $('#Cal1').val(0);
        }
    });


</script>

<% if (String.IsNullOrWhiteSpace(Model.GenerationType) || Model.GenerationType.ToUpper().Contains("GEN1"))
    { %>
<%--SensorID1 (Calibration3)--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_012|Paired Sensor ID","Paired Sensor ID")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="text" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="Cal3" id="Cal3" value="<%=Model.Calibration3 < 4294967295 && Model.Calibration3 > 0 ?  (Model.Calibration3.ToString()) : ""%>" />
        <%: label %>
        <%: Html.ValidationMessageFor(model => model.Calibration3)%>
    </div>
</div>
<%} %>
<br />

<%--Relay 2--%>
<p class="useAwareState">Relay 2</p>

<%--Relay2Visibility (SensorAttribute "Relay2Visibility")--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_012|Relay 2 Visible","Relay 2 Visible")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Show","Show")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="Relay2VisibilityChkBx" id="Relay2VisibilityChkBx" <%=Application.Relay2VisibliityAttribute.Value == "True"  ? "" : "checked" %>>
            <label class="form-check-label"><%: Html.TranslateTag("Hide","Hide")%></label>
        </div>
        <div style="display: none;">
            <input value="<%=Application.Relay2VisibliityAttribute.Value %>" type="hidden" id="Relay2Visibility" name="Relay2Visibility" /><%: Html.TextBoxFor(model => Model.Calibration1, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        </div>
    </div>
</div>


<div class="relay2">
    <%--Relay2Name (SensorAttribute "Relay2Name")--%>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_012|Relay 2 Title","Relay 2 Title")%>
        </div>
        <div class="col sensorEditFormInput" id="Div2">
            <input type="text" <%=Model.CanUpdate ? "" : "disabled" %> name="datumName1" id="Text1" class="form-control" value="<%=getDatumName1 %>" />
            <%: label %>
            <%: Html.ValidationMessageFor(model => getDatumName1)%>
        </div>
    </div>

    <%--Relay 2 Default State (Calibration2)--%>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_012|Default State","Default State")%>
        </div>
        <div class="col sensorEditFormInput">
            <div class="form-check form-switch d-flex align-items-center ps-0">
                <label id="dualOff" class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
                <label id="dualOn" class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
                <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="Cal2Chk" id="Cal2Chk" <%=Model.Calibration2 > 0 ? "checked" : "" %> onclick=" toggleDualRelay(R2toggle, dual2off, dual2on)">
            </div>
            <div style="display: none;"><%: Html.TextBox("Cal2",Model.Calibration2 ,(Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
        </div>
    </div>
    <script type="text/javascript">
        $("#Datum1").addClass('editField editFieldLarge');
        $("#Cal4").addClass('editField editFieldMedium');

        $('#Cal2Chk').change(function () {
            if ($('#Cal2Chk').prop('checked')) {
                $('#Cal2').val(1);
            } else {
                $('#Cal2').val(0);
            }
        });

        var R2toggle = document.getElementById("Cal2Chk");
        var dual2off = document.getElementById("dualOff");
        var dual2on = document.getElementById("dualOn");

        var R1toggle = document.getElementById("Cal1Chk");
        var dRoff = document.getElementById("dualRelayOff");
        var dRon = document.getElementById("dualRelayOn");

        function toggleDualRelay(relayToggle, dualRelayOff, dualRelayOn) {
            if (relayToggle.checked) {
                dualRelayOff.style.display = "none";
            } else {

                dualRelayOff.style.display = "block";
            }
            if (relayToggle.checked === false) {

                dualRelayOn.style.display = "none";
            } else {

                dualRelayOn.style.display = "block";
            }
        }
        toggleDualRelay(R2toggle, dual2off, dual2on);
        toggleDualRelay(R1toggle, dRoff, dRon);


    </script>

    <% if (String.IsNullOrWhiteSpace(Model.GenerationType) || Model.GenerationType.ToUpper().Contains("GEN1"))
        { %>
    <%--SensorID2 (Calibration4)--%>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_012|Paired Sensor ID","Paired Sensor ID")%>
        </div>
        <div class="col sensorEditFormInput">
            <input type="text" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="Cal4" id="Cal4" value="<%=Model.Calibration4 < 4294967295 && Model.Calibration4 > 0 ?  (Model.Calibration4.ToString()) : ""%>" />
            <%: label %>
            <%: Html.ValidationMessageFor(model => model.Calibration4)%>
        </div>
    </div>
    <%} %>
</div>

<script type="text/javascript">

    if ('<%=Application.Relay2VisibliityAttribute.Value == "True" ? "true" : "false" %>' == 'true') {
        $('.relay2').show();
    }
    else {
        $('.relay2').hide();
    }

    $("#Relay2VisibilityChkBx").on("change", function () {

        if ($("#Relay2VisibilityChkBx").prop('checked')) {
            $('.relay2').hide();
            $('#Relay2Visibility').val("off");

        }
        else {
            $('.relay2').show();
            $('#Relay2Visibility').val("on");
        }
    });


</script>
