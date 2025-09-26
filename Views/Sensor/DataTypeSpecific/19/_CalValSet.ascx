<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

        <tr>
            <td style="width: 250px;">Minor Interval</td>
            <td>
                <%: Html.TextBox("Calval1_Manual", Model.Calibration1, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
                <%: Html.ValidationMessageFor(model => model.Calibration1)%>
            </td>
            <td>
                <img alt="help" class="helpIcon" title="Number of miliseconds that represents the sub-sample period. Range is 20 to 30000" src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
        </tr>

        <tr>
            <td style="width: 250px;">Maximum Reading</td>
            <td>
                <%: Html.TextBox("Calval3_Manual", Model.Calibration3, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
                <%: Html.ValidationMessageFor(model => model.Calibration3)%>
            </td>
            <td>
                <img alt="help" class="helpIcon" title="Number of miliseconds that represents the sub-sample period. Range is 100 to 250" src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
        </tr>
      
       
          
        <tr>
            <td style="width: 250px;">Sensitivity</td>
            <td>
                <%: Html.TextBox("Calval2_Manual", Model.Calibration2, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
                <%: Html.ValidationMessageFor(model => model.Calibration2)%>
            </td>
            <td>
                <img alt="help" class="helpIcon" title="Number of pulsed in every Minor Interval needed to qualify occupancy. Range 1 to 10" src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
        </tr>
   <script>
       $('#Calval1_Manual').addClass("editField editFieldMedium aSettings__input_input");
       $('#Calval3_Manual').addClass("editField editFieldMedium aSettings__input_input");
       $('#Calval2_Manual').addClass("editField editFieldMedium aSettings__input_input");
</script>