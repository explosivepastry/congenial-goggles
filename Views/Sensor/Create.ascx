<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<% 
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();
%>
<script type="text/javascript">
    $(document).ready(function () {
        $('#SensorTypeID').change(setSensorType);
        setSensorType();
    });

    function setSensorType() {
        if ($('#SensorTypeID').val() == "4") {
            $('#divWiFi').show();
            $('#divStandard').hide();
            $('#RadioBand').val("WIFI");
        }
        else {
            $('#divStandard').show();
            $('#divWiFi').hide();
        }
    }
</script>
<% using (Html.BeginForm())
   {%>
<%: Html.ValidationSummary(false) %>

<div class="formBody">
    <%: Html.HiddenFor(model => model.AccountID)%>
    <%: Html.HiddenFor(model => model.CSNetID)%>

    <div class="editor-label">
        <%: Html.LabelFor(model => model.ApplicationID) %>
    </div>
    <div class="editor-field">
        <%: Html.DropDownList("ApplicationID", (SelectList)ViewData["Applications"], "Select One") %>
    </div>
    <div class="editor-error">
        <%: Html.ValidationMessageFor(model => model.ApplicationID) %>
    </div>

    <div class="editor-label">
        <%: Html.LabelFor(model => model.SensorID) %>
    </div>
    <div class="editor-field">
        <%: Html.TextBoxFor(model => model.SensorID)%>
    </div>
    <div class="editor-error">
        <%: Html.ValidationMessageFor(model => model.SensorID)%>
    </div>

    <div class="editor-label">
        <%: Html.LabelFor(model => model.SensorName) %>
    </div>
    <div class="editor-field">
        <%: Html.TextBoxFor(model => model.SensorName) %>
    </div>
    <div class="editor-error">
        <%: Html.ValidationMessageFor(model => model.SensorName) %>
    </div>

    <div class="editor-label">
        <%: Html.LabelFor(model => model.SensorTypeID) %>
    </div>
    <div class="editor-field">
        <%: Html.DropDownList("SensorTypeID", (SelectList)ViewData["SensorType"], null, null)%>
    </div>
    <div class="editor-error">
        <%: Html.ValidationMessageFor(model => model.SensorTypeID)%>
    </div>

    <%: Html.HiddenFor(model => model.ReportInterval)%>
    <%: Html.HiddenFor(model => model.ActiveStateInterval)%>
    <%: Html.HiddenFor(model => model.MinimumCommunicationFrequency)%>


    <div class="editor-label">
        <%: Html.LabelFor(model => model.GenerationType) %>
    </div>
    <div class="editor-field">
        <select class="tzSelect" id="GenerationType" name="GenerationType">
            <option <%=Model.GenerationType == "Gen1" ? "Selected='selected'" : "" %> value="Gen1">Generation 1</option>
            <option <%=Model.GenerationType.Contains("Gen2") ? "Selected='selected'" : "" %> value="Gen2">Alta</option>
        </select>
    </div>
    <div class="editor-error">
        <%: Html.ValidationMessageFor(model => model.GenerationType)%>
    </div>

    <div class="editor-label">
        <%: Html.LabelFor(model => model.FirmwareVersion) %>
    </div>
    <div class="editor-field">
        <%: Html.TextBoxFor(model => model.FirmwareVersion)%>
    </div>
    <div class="editor-error">
        <%: Html.ValidationMessageFor(model => model.FirmwareVersion)%>
    </div>

    <div class="editor-label">
        <%: Html.LabelFor(model => model.PowerSourceID) %>
    </div>
    <div class="editor-field">
        <%: Html.DropDownList("PowerSourceID", (SelectList)ViewData["PowerSource"], null, null)%>
    </div>
    <div class="editor-error">
        <%: Html.ValidationMessageFor(model => model.PowerSourceID)%>
    </div>

    <div id="divStandard">

        <div class="editor-label">
            <%: Html.LabelFor(model => model.RadioBand) %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownList("RadioBand", (SelectList)ViewData["RadioBand"], null, null)%>
        </div>

    </div>

    <div id="divWiFi" style="display: none;">

        <div class="editor-label">
            Gateway ID
        </div>
        <div class="editor-field">
            <%: Html.TextBox("GatewayID")%>
        </div>
        <div class="editor-error">
            <%: Html.ValidationMessage("GatewayID")%>
        </div>

        <div class="editor-label">
            MAC Address
        </div>
        <div class="editor-field">
            <%: Html.TextBox("MacAddress")%>
        </div>
        <div class="editor-error">
            <%: Html.ValidationMessage("MacAddress")%>
        </div>

        <div class="editor-label">
            Gateway Firmware Version
        </div>
        <div class="editor-field">
            <%: Html.TextBox("GatewayFirmwareVersion")%>
        </div>
        <div class="editor-error">
            <%: Html.ValidationMessage("GatewayFirmwareVersion")%>
        </div>
    </div>
    <div style="clear: both;"></div>

</div>
<div class="buttons">
    <a href="" onclick="window.location.href = window.location.href;" class="greybutton">Cancel</a>
    <input type="button" onclick="postMain();" value="Save" class="bluebutton" />
    <% if (ViewData["LastID"] != null)
       { %>
    <div style="float: right;">
        Success - SensorID: <%:ViewData["LastID"] %>
    </div>
    <%} %>
    <div style="clear: both;"></div>
</div>

<script type="text/javascript">
    $(document).ready(function () {

        $('#SensorName').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        $('#SensorID').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        $('#MonnitApplicationID').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        $('#GatewayFirmwareVersion').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        $('#MacAddress').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        $('#GatewayID').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        $('#RadioBand').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        $('#PowerSourceID').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        $('#FirmwareVersion').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        $('#RadioBand').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        $('#SensorTypeID').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });



        //$(window).keydown(function (event) {
        //    if ($("*:focus").attr("id") != "savebtn") {
        //        if (event.keyCode == 13) {
        //            event.preventDefault();
        //            return false;
        //        }
        //    }
        //});
    });
</script>
<% } %>