<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%if (!Model.IsWiFiSensor)
  {%>

<tr>
    <td style="width: 250px;">Synchronize</td>
    <td>
       

        <%: Html.CheckBox("TransmitOffsetChk", Model.TransmitOffset > 0, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%--<input type="checkbox" id="TransmitOffsetChk" name="TransmitOffsetChk" <%:Model.TransmitOffset > 0 ? "checked='checked'" : ""%> />--%>
        <div style="display: none;"><%: Html.TextBoxFor(model => model.TransmitOffset, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="In small sensor networks, the sensors can be set to synchronize their communications. The default setting off allows the sensors to randomize their communications therefore maximizing communication robustness.  Setting this will synchronize the communication of the sensors. <br /><br />Example: Turning synchronization on all sensors indicates that they all try to communicate within 5 seconds of each other, recommended to limit this to gateways with 10 or fewer sensors." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>
<script type="text/javascript">
    //Delay because if not visible yet sizing gets all screwed up
    setTimeout("$('#TransmitOffsetChk').iButton();", 500);
    $('#TransmitOffsetChk').change(function () {
        if ($('#TransmitOffsetChk').prop('checked')) {
            $('#TransmitOffset').val(7);
        } else {
            $('#TransmitOffset').val(0);
        }
    });

    $(document).ready(function () {
        if (<%= Model.Calibration1 %> <= 1 || <%= Model.Calibration2 %> <= 1) {
                $('#TransmitOffsetChk').prop('disabled', true);
            showSimpleMessageModal("<%=Html.TranslateTag("Assessment Interval must be greater than 1 to use 'Synchronize' setting")%>");
            }
        });

</script>
<%}%>