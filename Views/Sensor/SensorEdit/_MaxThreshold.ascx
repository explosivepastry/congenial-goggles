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

<td style="width: 250px;">Maximum Threshold</td>
<td>  <%: Html.TextBox("MaximumThreshold_Manual", Max, (Dictionary<string,object>)ViewData["HtmlAttributes"])%> <%: label %>
           <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
</td>
<td>
    <img alt="help" class="helpIcon" title="Any assessments above this value will cause the sensor to enter the Aware State." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>
  <script>
      $('#MaximumThreshold_Manual').addClass('editField editFieldMedium');
      $("#MaximumThreshold_Manual").change(function () {
          if (!isANumber($("#MaximumThreshold_Manual").val()))
              $("#MaximumThreshold_Manual").val(<%: Max%>);
      });
 </script>
<%} %>