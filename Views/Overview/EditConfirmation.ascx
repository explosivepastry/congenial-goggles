<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div id="fTitle" style="font-weight:bold;" class="formtitle"><%: Html.TranslateTag("Overview/EditConfirmation|Update Confirmation","Update Confirmation")%>:</div>
<div class="formbody">
    <div style="padding: 10px 30px 0px 30px;font-weight:bold;">
        <%
            string temp = ViewBag.Response.ToString();
            switch (temp)
            {
                case "Sensor Edit Success":
        %>
        <div>
            <%: Html.TranslateTag("Overview/EditConfirmation|Sensor has been successfully updated","Sensor has been successfully updated")%>.<br />
        </div>
        <%
                    break;
                case "Sensor Edit Pending":
        %>
        <div>
            <%: Html.TranslateTag("Overview/EditConfirmation|Your settings were saved and will be applied when the sensor checks in","Your settings were saved and will be applied when the sensor checks in")%>.<br />
        </div>
        <%
                    break;
                case "Multi Sensor Edit Pending":
        %>
        <div>
            <%: Html.TranslateTag("Overview/EditConfirmation|Your settings were saved and will be applied as the sensors check in","Your settings were saved and will be applied as the sensors check in")%>.<br />
        </div>
        <%
                    break;
                case "Sensor Reset Defaults Pending":
        %>
        <div>
            <%: Html.TranslateTag("Overview/EditConfirmation|Sensor reset has been saved and will be applied when the sensor checks in","Sensor reset has been saved and will be applied when the sensor checks in")%>.<br />
        </div>
        <%
                    break;
                case "Sensor Reset Counter Pending":
        %>
        <div>
            <%: Html.TranslateTag("Overview/EditConfirmation|Reset Counter has been saved and will be applied when the sensor checks in","Reset Counter has been saved and will be applied when the sensor checks in")%>.<br />
        </div>
        <%
                    break;
                case "Sensor Calibration Pending":
        %>
        <div>
            <%: Html.TranslateTag("Overview/EditConfirmation|Calibration has been set and will be applied when the sensor checks in","Calibration has been set and will be applied when the sensor checks in")%>.<br />
        </div>
        <%
                    break;
                case "Sensor Calibration Success":
        %>
        <div>
            <%: Html.TranslateTag("Overview/EditConfirmation|Sensor has been successfully calibrated","Sensor has been successfully calibrated")%>.<br />
        </div>
        <%
                break;
            case "Sensor Calibration Reset to Defaults Pending":
        %>
        <div>
            <%: Html.TranslateTag("Overview/EditConfirmation|Default calibrations have been set and will be applied when the sensor checks in","Default calibrations have been set and will be applied when the sensor checks in")%>.<br />
        </div>
        <%
                break;
            case "Sensor Scale Change Pending":
        %>
        <div>
            <%: Html.TranslateTag("Overview/EditConfirmation|Scale settings have been saved and will be applied when the sensor checks in","Scale settings have been saved and will be applied when the sensor checks in")%>.<br />
        </div>
        <%
                break;
            case "Sensor Scale Change Success":
        %>
        <div>
            <%: Html.TranslateTag("Overview/EditConfirmation|Sensor Scale settings have been successfully updated","Sensor Scale settings have been successfully updated")%>.<br />
        </div>
        <%
                break;
            case "Gateway Reset Pending":
        %>
        <div>
            <%: Html.TranslateTag("Overview/EditConfirmation|Gateway reset has been saved and will update on the next heartbeat","Gateway reset has been saved and will update on the next heartbeat")%>.<br />
        </div>
        <%
                break;
            default:
        %>
        <div>
            <%:temp %><br />
        </div>
        <%
                break;
            }
    
        %>
    </div>
</div>
<div class="form-group">
    <div class="col-12 text-end">
        <%if (!string.IsNullOrWhiteSpace(ViewBag.SensorID)) 
          { %>
            <a href="/Overview/SensorChart/<%:ViewBag.SensorID %>" class="btn btn-secondary"><%:Html.TranslateTag("Go to Details","Go to Details")%></a>
        <%} %>
        <a href="<%= ViewBag.returnConfirmationURL != null ? ViewBag.returnConfirmationURL.ToString() : "" %>" onclick="$(this).hide();$('#saving').show();" id="reloadEdit" class="btn btn-primary"><%: Html.TranslateTag("Continue","Continue")%></a>
        <button class="btn btn-primary" id="saving" style="display:none;" type="button" disabled >
            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true" ></span>
            <%:Html.TranslateTag("Overview/EditConfirmation|Loading","Loading")%>
        </button>
        <div style="clear: both"></div>
    </div>

</div>


<script type="text/javascript">

<%= ExtensionMethods.LabelPartialIfDebug("Overview_EditConfirmation.ascx")  %>

    $(document).ready(function () {

<%   
    if(temp.ToLower().Contains("pending"))
    { 
%>
        $('.pendingsvg').show();
<%
    }
%>

        $('#reloadEdit').click(function (e) {
            var href = $(this).attr("href");

            if (href.indexOf("reload") !== -1) {
                $(this).attr('href', goBack());

            }
            if (href.indexOf("?networkID=") !== -1) {
                window.location.href = $(this).attr("href");
            }
            else {
                e.preventDefault();
                window.location.href = $(this).attr("href");
            }
        });
    });
</script>
