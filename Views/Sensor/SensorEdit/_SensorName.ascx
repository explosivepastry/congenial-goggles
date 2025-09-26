<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<tr>
    <td style="width: 250px;">Sensor Name</td>
    <td>
        
          <%--<%: Html.TextBox("SensorName",)%>--%>
		<input type="text" id="SensorName" name="SensorName" value="<%= Model.SensorName%>" />
      <%--  <%: Html.TextBoxFor(model => model.SensorName)%>--%>
        <%: Html.ValidationMessageFor(model => model.SensorName)%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="Default value <%: Model.MonnitApplication.ApplicationName%>-<%: Model.SensorID%><br/><br/>The unique name you give the sensor to easily identify it in a list and in any notifications." src="<%:Html.GetThemedContent("/images/help.png")%>" />
		
		<script>


		    $('#SensorName').addClass('editField editFieldLarge');

		    $(function () {
                $('.helpIcon').tipTip();
		    });


		</script>
		  
		    
		    
		    
		    

		   
		
	</td>
</tr>
