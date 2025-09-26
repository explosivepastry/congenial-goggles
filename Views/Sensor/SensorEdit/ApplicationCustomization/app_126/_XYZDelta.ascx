<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<%---X-Delta Value---%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("X-Delta Value","X-Delta Value")%> (<%: Html.Label("g") %>)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="XDeltaValue" id="XDeltaValue" value="<%:  (Model.MaximumThreshold / 1000.0) %>" />
        <a id="XDeltaValueNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<%---Y-Delta Value---%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Y-Delta Value","Y-Delta Value")%> (<%: Html.Label("g") %>)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="YDeltaValue" id="YDeltaValue" value="<%:  (Model.MinimumThreshold / 1000.0) %>" />
        <a id="YDeltaValueNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<%---Z-Delta Value---%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Z-Delta Value","Z-Delta Value")%> (<%: Html.Label("g") %>)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="ZDeltaValue" id="ZDeltaValue" value="<%:  (Model.Hysteresis / 1000.0) %>" />
        <a id="ZDeltaValueNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>
<p class="useAwareState"></p>
<%---Magnitude-Delta Value---%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Magnitude Delta Value","Magnitude Delta Value")%> (<%: Html.Label("g") %>)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MagDeltaValue" id="MagDeltaValue" value="<%:  (Model.Calibration4 / 1000.0) %>" />
        <a id="MagDeltaValueNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>


<%--Output Data Rate--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Output Data Rate","Output Data Rate")%>
    </div>
    <div class="col sensorEditFormInput">
        <select name="OutputDataRate" id="OutputDataRate" class="form-select ms-0">
            <option value="49" <%:Model.Calibration1 == 49 ? "selected='selected'" : "" %>><%: Html.TranslateTag("6 Hz","6 Hz")%></option>
            <option value="41" <%:Model.Calibration1 == 41 ? "selected='selected'" : "" %>><%: Html.TranslateTag("12 Hz","12 Hz")%></option>
            <option value="33" <%:Model.Calibration1 == 33 ? "selected='selected'" : "" %>><%: Html.TranslateTag("50 Hz","50 Hz")%></option>
            <option value="25" <%:Model.Calibration1 == 25 ? "selected='selected'" : "" %>><%: Html.TranslateTag("100 Hz","100 Hz")%></option>
        </select>
    </div>
</div>

<%--Operating Mode--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Accelerometer Mode","Accelerometer Mode")%>
    </div>
    <div class="col sensorEditFormInput">
        <select name="Mode" id="Mode" class="form-select ms-0">
            <option value="0" <%:Model.Calibration2 == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_020|2G Normal","2G Normal")%></option>
            <option value="16" <%:Model.Calibration2 == 16 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_020|2G High Pass Filter","2G High Pass Filter")%></option>
            <option value="1" <%:Model.Calibration2 == 1 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_020|4G Normal","4G Normal")%></option>
            <option value="17" <%:Model.Calibration2 == 17 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_020|4G High Pass Filter","4G High Pass Filter")%></option>
            <option value="2" <%:Model.Calibration2 == 2 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_020|8G Normal","8G Normal")%></option>
            <option value="18" <%:Model.Calibration2 == 18 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_020|8G High Pass Filter","8G High Pass Filter")%></option>
        </select>
    </div>
</div>

<script type="text/javascript">

    $(function () {

        //X-Delta
        <% if (Model.CanUpdate)
           { %>

        createSpinnerModal("XDeltaValueNum", "g", "XDeltaValue", [1, 2, 3, 4, 5, 6, 7, 8], null, [".00", ".10", ".20", ".30", ".40", ".50", ".60", ".70", ".80", ".90"]);

        <%}%>

        $("#XDeltaValue").change(function () {
            if (isANumber($("#XDeltaValue").val())) {
                if ($("#XDeltaValue").val() < 0)
                    $("#XDeltaValue").val(0);
                if ($("#XDeltaValue").val() > 8)
                    $("#XDeltaValue").val(8);
            }
            else {
                $("#XDeltaValue").val(<%=(Model.MaximumThreshold / 1000.0)%>);
            }

        });


        //Y-Delta
        <% if (Model.CanUpdate)
           { %>

        createSpinnerModal("YDeltaValueNum", "g", "YDeltaValue", [1, 2, 3, 4, 5, 6, 7, 8], null, [".00", ".10", ".20", ".30", ".40", ".50", ".60", ".70", ".80", ".90"]);

        <%}%>

        $("#YDeltaValue").change(function () {
            if (isANumber($("#YDeltaValue").val())) {
                if ($("#YDeltaValue").val() < 0)
                    $("#YDeltaValue").val(0);
                if ($("#YDeltaValue").val() > 8)
                    $("#YDeltaValue").val(8);
            }
            else {
                $("#YDeltaValue").val(<%=(Model.MinimumThreshold / 1000.0)%>);
            }

        });

        //Z-Delta
        <% if (Model.CanUpdate)
           { %>

        createSpinnerModal("ZDeltaValueNum", "g", "ZDeltaValue", [1, 2, 3, 4, 5, 6, 7, 8], null, [".00", ".10", ".20", ".30", ".40", ".50", ".60", ".70", ".80", ".90"]);

        <%}%>

        $("#ZDeltaValue").change(function () {
            if (isANumber($("#ZDeltaValue").val())) {
                if ($("#ZDeltaValue").val() < 0)
                    $("#ZDeltaValue").val(0);
                if ($("#ZDeltaValue").val() > 8)
                    $("#ZDeltaValue").val(8);
            }
            else {
                $("#ZDeltaValue").val(<%=(Model.Hysteresis / 1000.0)%>);
            }
        });

        //Mag-Delta
        <% if (Model.CanUpdate)
           { %>

        createSpinnerModal("MagDeltaValueNum", "g", "MagDeltaValue", [1, 2, 3, 4, 5, 6, 7, 8], null, [".00", ".10", ".20", ".30", ".40", ".50", ".60", ".70", ".80", ".90"]);

        <%}%>

        $("#MagDeltaValue").change(function () {
            if (isANumber($("#MagDeltaValue").val())) {
                if ($("#MagDeltaValue").val() < 0)
                    $("#MagDeltaValue").val(0);
                if ($("#MagDeltaValue").val() > 8)
                    $("#MagDeltaValue").val(8);
            }
            else {
                $("#MagDeltaValue").val(<%=(Model.Calibration4 / 1000.0)%>);
            }

        });

                  <% if (Model.CanUpdate)
             { %>

        const arrayForSpinner = arrayBuilder(0, 600, 1);
        createSpinnerModal("timeNum", "Seconds", "ReArmTime", arrayForSpinner);

        <%}%>


        $("#ReArmTime").change(function () {
            if (isANumber($("#ReArmTime").val())) {
                if ($("#ReArmTime").val() < 0)
                    $("#ReArmTime").val(0);
                if ($("#ReArmTime").val() > 600)
                    $("#ReArmTime").val(600);
            }
            else {
                $("#ReArmTime").val(<%=Model.Calibration3%>);;
            }
        });
    });
</script>
