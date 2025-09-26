<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>



<%
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string Hyst = "";
        string label = "";
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);
        
%>

<tr>
    <td style="width: 250px;">Hysteresis</td>
    <td>
        <%: Html.TextBox("Hysteresis_Manual", Hyst, (Dictionary<string,object>)ViewData["HtmlAttributes"])%> <%: label %>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="A buffer to prevent the sensor from bouncing between Standard Operation and Aware State when the assessments are very close to a threshold.  For example, if a Maximum Threshold is set to 90° and a Hysteresis of 1°, then once the sensor takes an assessment of 90.0° and enters the Aware State it will remain in the Aware State until the temperature readings drop to 89.0°.  Similarly, at the Minimum Threshold the temperature will have to rise a degree above the threshold to return to Standard Operation." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>
 <script>
     $('#Hysteresis_Manual').addClass('editField editFieldMedium');

     $("#Hysteresis_Manual").change(function () {

         if (!isANumber($("#Hysteresis_Manual").val()))
             $("#Hysteresis_Manual").val(<%: Hyst%>);

         if ($("#Hysteresis_Manual").val() < 0)
             $("#Hysteresis_Manual").val(0);

         if ($("#Hysteresis_Manual").val() > 10)
             $("#Hysteresis_Manual").val(10);
     });
 </script>
<%} %>