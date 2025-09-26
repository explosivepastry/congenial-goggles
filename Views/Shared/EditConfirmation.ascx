<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div id="fTitle" class="formtitle">Update Confirmation</div>
<div class="formbody">
    <div style="padding: 10px 30px 0px 30px;">
        <%
            string temp = ViewBag.Response.ToString();
            switch (temp)
            {
                case "Sensor Edit Success":
        %>
        <div>Sensor has been successfully updated.</div>
        <%
                break;
            case "Sensor Edit Pending":
        %>
        <div>Your settings were saved and will be applied when the sensor checks in.</div>
        <%
                break;
            case "Sensor Reset Defaults Pending":
        %>
        <div>Sensor reset has been saved and will be applied when the sensor checks in.</div>
        <%
                break;
            case "Sensor Reset Counter Pending":
        %>
        <div>The reset command has been queued and will be applied when the sensor checks in.</div>
        <%
                break;
            case "Sensor Calibration Pending":
        %>
        <div>Calibration has been set and will be applied when the sensor checks in.</div>
        <%
                break;
            case "Sensor Calibration Success":
        %>
        <div>Sensor has been successfully calibrated.</div>
        <%
                break;
            case "Sensor Calibration Reset to Defaults Pending":
        %>
        <div>Default calibrations have been set and will be applied when the sensor checks in.</div>
        <%
                break;
            case "Sensor Scale Change Pending":
        %>
        <div>Scale settings have been saved and will be applied when the sensor checks in.</div>
        <%
                break;
            case "Sensor Scale Change Success":
                %>
                <div>Sensor Scale settings have been successfully updated.</div>
                <%
                break;
            case "Gateway Reset Pending":
                %>
                <div>Gateway reset has been saved and will update on the next heartbeat.</div>
                <%
                break;
            default:
                %>
                <div><%:temp %></div>
                <%
                break;
        }
    
        %>
    </div>
</div>
<div class="text-end">
    <a href="<%= ViewBag.returnConfirmationURL.ToString() %>" onclick="$(this).hide();$('#saving').show();" id="reloadEdit" class="btn btn-primary">Continue</a>
    <button class="btn btn-primary" id="saving" style="display:none;" type="button" disabled >
        <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true" ></span>
        Loading...
    </button>
    <div style="clear: both"></div>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $('#reloadEdit').click(function (e) {
            var href = $(this).attr("href");
            
            if (href.indexOf("Overview/SensorEdit") !== -1) {
                location.reload();
            }
            if (href.indexOf("reload") !== -1) {
                $(this).attr('href', goBack());

            }
            if (href.indexOf("?networkID=") !== -1) {
                window.location.href = $(this).attr("href");
            }
            else {
                e.preventDefault();
                var url = $(this).attr("href");
                ajaxDiv($('#fTitle').parent().attr('id'), url);
            }
        });
    });
</script>
