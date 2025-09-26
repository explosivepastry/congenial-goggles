<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>

<%        double intervalVal = (Model.Calibration3 / 1000d);
    if (new Version(Model.FirmwareVersion) >= new Version("14.26.17.10"))
    {
        intervalVal = Model.Calibration3;
    }
%>


<%: Html.Hidden("Offset_Hidden", unchecked((sbyte)(CurrentZeroToOneFiftyAmp.GetHystThirdByte(Model))) / 100d, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>

<input type="hidden" <%=Model.CanUpdate ? "" : "disabled" %> name="avgInterval" id="avgInterval" value="<%=intervalVal%>" />


<input type="hidden" <%=Model.CanUpdate ? "" : "disabled" %> name="AccumChk" id="AccumChk" <%= CurrentZeroToOneFiftyAmp.GetHystFourthByte(Model) == 1 ? "checked" : "" %> data-toggle="toggle" data-on="<%: Html.TranslateTag("On","On")%>" data-off="<%: Html.TranslateTag("Off","Off")%>" />
<!-- Check remove? --><div style="display: none;"><%: Html.Hidden("Accum",CurrentZeroToOneFiftyAmp.GetHystFourthByte(Model))%></div>


<input type="hidden" <%=Model.CanUpdate ? "" : "disabled" %> name="fullChk" id="fullChk" <%= CurrentZeroToOneFiftyAmp.GetShowFullDataValue(Model.SensorID) ? "checked" : "" %> data-toggle="toggle" data-on="<%: Html.TranslateTag("On","On")%>" data-off="<%: Html.TranslateTag("Off","Off")%>" />
<!-- Check remove? --><div style="display: none;"><%: Html.Hidden("FullNotiString",CurrentZeroToOneFiftyAmp.GetShowFullDataValue(Model.SensorID))%></div>

<script type="text/javascript">

    //check if this logic needs to be removed

    $('#AccumChk').change(function () {
        if ($('#AccumChk').prop('checked')) {
            $('#Accum').val(1);
        } else {
            $('#Accum').val(0);
        }
    });

    $('#fullChk').change(function () {
        if ($('#fullChk').prop('checked')) {
            $('#FullNotiString').val(1);
        } else {
            $('#FullNotiString').val(0);
        }
    });
</script>
