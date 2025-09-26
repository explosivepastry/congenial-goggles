<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    int awareMode = TriggeredTilt.GetAwareMode(Model);
    int dataMode = TriggeredTilt.GetDataMode(Model);
    int reArmTime = TriggeredTilt.GetReArmTime(Model);
    double GetOffset = TriggeredTilt.GetOffset(Model);

%>


<div class="row sensorEditForm" style="display: none">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_130|Data Mode","Data Mode")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="DataMode_Manual" name="DataMode_Manual" class=form-select" <%:ViewData["disabled"].ToBool() ? "disabled" : ""%>>
            <option value="0" <%: dataMode == 0 ? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_130|Delta Mode","Delta Mode")%></option>
            <option value="1" <%: dataMode == 1 ? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_130|Up/Down","Up/Down")%></option>
        </select>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_130|Aware Mode","Aware Mode")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="AwareMode_Manual" name="AwareMode_Manual" class=form-select" <%:ViewData["disabled"].ToBool() ? "disabled" : ""%>>
            <option class="updownMode deltaMode" value="0" <%: awareMode == 0 ? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_130|None","None")%></option>
            <option class="deltaMode" value="1" <%: (dataMode == 0 &&  awareMode == 1) ? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_130|Delta Exceeded","Delta Exceeded")%></option>
            <option class="updownMode" value="1" <%: (dataMode == 1 &&  awareMode == 1)  ? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_130|On Change","On Change")%></option>
            <option class="updownMode" value="2" <%: awareMode == 2 ? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_130|Not Down","Not Down")%></option>
            <option class="updownMode" value="3" <%: awareMode == 3 ? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_130|Not Up","Not Up")%></option>
            <option class="updownMode" value="4" <%: awareMode == 4 ? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_130|Stuck","Stuck")%></option>
        </select>
    </div>
</div>

<%--Rearm Time--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Time to Re-Arm (seconds)","Time to Re-Arm (seconds)")%>
    </div>
    <div class="col sensorEditFormInput">

        <input class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> id="RearmTime_Manual" name="RearmTime_Manual" value="<%=reArmTime%>" />
        <a id="reArmNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<script>

    $(function () {
        setDataModeOptions();

        var rearm_array = [0, 1, 5, 10, 12, 15, 20, 30, 60];

        <% if (Model.CanUpdate) { %>

        createSpinnerModal("reArmNum", "Seconds", "RearmTime_Manual", rearm_array);

        <%}%>

        $("#RearmTime_Manual").change(function () {
            if (isANumber($("#RearmTime_Manual").val())) {
                if ($("#RearmTime_Manual").val() < 0)
                    $("#RearmTime_Manual").val(0);
                if ($("#RearmTime_Manual").val() > 60)
                    $("#RearmTime_Manual").val(60)
            }
            else {
                $("#RearmTime_Manual").val(10);

            }
        });
    });

    function setDataModeOptions() {
        var dataMode = $("#DataMode_Manual").val();
        $(".deltaMode").hide();
        $(".updownMode").hide();
        $("#AwareMode_Manual").children().hide(); // hide all the options
        switch (dataMode) {
            case "0":
                $(".deltaMode").show();
                break;
            case "1":
                $(".updownMode").show();
                break;
        }
    }

</script>
