<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>
<!--DatumType 08 MilliAmps-->
<%  
	List<Sensor> senslist = Sensor.LoadByApplicationID(22, 1);


	Dictionary<string, SensorAttribute.SensorScaleBadge> DropdownOptions = Monnit.ZeroToTwentyMilliamp.GetApplicationScaleOptions(Model.AccountID);
	string currentLow = Monnit.ZeroToTwentyMilliamp.GetLowValue(Model.SensorID).ToString();
	string currentHigh = Monnit.ZeroToTwentyMilliamp.GetHighValue(Model.SensorID).ToString();
	string currentLabel = Monnit.ZeroToTwentyMilliamp.GetLabel(Model.SensorID).ToString();
	string badgeText = Monnit.ZeroToTwentyMilliamp.ScaleBadgeText(Model.SensorID);

	double compareVal = Model.CompareValue.ToDouble().LinearInterpolation(0, currentLow.ToDouble(), 20, currentHigh.ToDouble());

%>


<%if (DropdownOptions.Count > 0)
	{ %>
<div class="aSettings__title">
	<%: Html.TranslateTag("Would you like to use a assign a scale value?", "Would you like to assign a scale value?")%>
</div>

<div>
	<select id="badgeSelector" onchange="SetScaleValues()" class="form-select">
		<option style="display: flex; justify-content: space-between;" data-low="<%=currentLow%>" data-high="<%=currentHigh%>" data-lbl="<%=currentLabel%>" selected><%=Html.TranslateTag("Current Scale") %></option>

		<%foreach (var b in DropdownOptions)
			{%>
		<option style="display: flex; justify-content: space-between;" data-low="<%=b.Value.Attribute1%>" data-high="<%=b.Value.Attribute2%>" data-lbl="<%=b.Value.Label%>">
			<%=b.Value.BadgeText%>
		</option>
		<%}%>
	</select>
</div>

<%} %>

<!--DatumType 8 - MilliAmps-->
<div class="aSettings__title">
	Notify when reading is 
</div> 
<div>
    <select class="form-select mb-1" id="CompareType" name="CompareType">
        <option value="Greater_Than" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Greater_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Greater Than","Greater Than")%></option>
        <option value="Less_Than" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Less_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Less Than","Less Than")%></option>        
    </select>
</div>
<div class="d-flex align-items-center">
    <input id="CompareValue" class="form-control" name="CompareValue" type="text" value="<%:compareVal %>">
	<span id="scalelbl" style="margin-left:5px;"><%=currentLabel%></span>
</div>
<%: Html.ValidationMessageFor(model => model.CompareValue)%>
<div>
	<input type="hidden" id="Scale" class="form-control" name="Scale" value="<%:Model.Scale%>" />
	<%: Html.ValidationMessageFor(model => model.Scale)%>
</div>

<script type="text/javascript">

	//alert($('#ApplicationID').val());

    function datumConfigs() {
        var settings = "compareType=" + $('#CompareType').val();
		settings += "&compareValue=" + $('#CompareValue').val();
		settings += "&scale=" + $('#Scale').val();
        return settings;
	}

	function SetScaleValues() {
		var optionval = $("#badgeSelector option:selected")
			
		$('#scalelbl').html($(optionval).data('lbl'));
		$('#Scale').val($(optionval).val());
	}
</script>