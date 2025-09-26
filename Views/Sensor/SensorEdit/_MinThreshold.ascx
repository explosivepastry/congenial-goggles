<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string Min = "";
        string Max = "";
        string label = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);
        
        
        
%>

<td style="width: 250px;">Below</td>
<td><%: Html.TextBox("MinimumThreshold_Manual", Min, (Dictionary<string,object>)ViewData["HtmlAttributes"])%> <%: label %>
    <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
</td>
<td>
    <img alt="help" class="helpIcon" title="Any assessments below this value will cause the sensor to enter the Aware State." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>
 <script>
     $('#MinimumThreshold_Manual').addClass('editField editFieldMedium');
     $("#MinimumThreshold_Manual").change(function () {
         if (!isANumber($("#MinimumThreshold_Manual").val()))
             $("#MinimumThreshold_Manual").val(<%: Min%>);
     });
 </script>
<%} %>