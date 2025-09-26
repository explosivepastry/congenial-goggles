<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%  
	Dictionary<string, SensorAttribute.SensorScaleBadge> DropdownOptions = Monnit.ZeroToTwentyMilliamp.GetApplicationScaleOptions(Model.AccountID);
	string badgeText = Monnit.ZeroToTwentyMilliamp.ScaleBadgeText(Model.SensorID);

	string currentLow = Monnit.ZeroToTwentyMilliamp.Get4maValue(Model.SensorID).ToString();
	string currentHigh = Monnit.ZeroToTwentyMilliamp.GetHighValue(Model.SensorID).ToString();
	string currentLabel = Monnit.ZeroToTwentyMilliamp.GetLabel(Model.SensorID).ToString();
%>

<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post" class="form-group">
	<%: Html.ValidationSummary(false) %>
	<input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />

    <!-- Do not show dropdown, if it doesn't have any options-->
    <%if (DropdownOptions.Count > 0)
        { %><div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <b><%: Html.TranslateTag("Would you like to use a previous scale?", "Would you like to use a previous scale?")%></b>
            </div>

            <div class="col sensorEditFormInput">
                <select id="badgeSelector" onchange="SetScaleValues()" class="form-select"\>
                    <option style="display: flex; justify-content: space-between;" data-low="<%=currentLow%>" data-high="<%=currentHigh%>" data-lbl="<%=currentLabel%>" selected><%=Html.TranslateTag("Current Scale") %></option>

                    <%foreach (var b in DropdownOptions)
                        {%>
                    <option style="display: flex; justify-content: space-between;" data-low="<%=b.Value.Attribute1%>" data-high="<%=b.Value.Attribute2%>" data-lbl="<%=b.Value.Label%>">
                        <%=b.Value.BadgeText%>
                    </option>
                    <%}%>
                    <option style="display: flex; justify-content: space-between; font-weight: bold;">New Scale</option>
                </select>
            </div>
        </div>
    <br />
	<%} %>

	<div class="formtitle">

		<b><%: Html.TranslateTag("Overview/SensorScale|Current Scale","Current Scale")%></b>
		&nbsp;&nbsp;&nbsp;

		<!-- Do not show badge if badge is empty-->
		<%if (!string.IsNullOrEmpty(badgeText))
			{ %>
		<div class="badge badgeSelected" id="actionBadge">
			<%= badgeText%>
		</div>
		<%} %>
	</div>
	<div class="formBody">
		<!--LowValue-->
		<div class="row sensorEditForm">
			<div class="col-12 col-md-3"><%: Html.TranslateTag("Overview/SensorScale|4 mA Value","4 mA Value")%></div>
			<div class="col sensorEditFormInput">
				<input class="form-control" type="text" id="lowValue" name="lowValue" value="<%:currentLow%>" />
			</div>
			<br />

		</div>
		<!--HighValue-->
		<div class="row sensorEditForm">
			<div class="col-12 col-md-3"><%: Html.TranslateTag("Overview/SensorScale|20 mA Value","20 mA Value")%></div>
			<div class="col sensorEditFormInput">
				<input class="form-control" type="text" id="highValue" name="highValue" value="<%:currentHigh%>" />
			</div>
			<br />
		</div>
		<!--Label-->
		<div class="row sensorEditForm">
			<div class="col-12 col-md-3"><%: Html.TranslateTag("Overview/SensorScale|Label","Label")%></div>
			<div class="col sensorEditFormInput">
				<input class="form-control" type="text" id="label" name="label" value="<%:currentLabel%>" />
			</div>
		</div>
	</div>

	<div class="col-md-12 col-xs-12">
		<span style="color: red;">
			<%: ViewBag.ErrorMessage == null ? "": ViewBag.ErrorMessage %>
		</span>
		<span style="color: black;">
			<%: ViewBag.Message == null ? "":ViewBag.Message %>
		</span>
	</div>

	<div class="clearfix"></div>
	<div class="ln_solid"></div>

	<div class="" style="text-align: right;">
		<input class="btn btn-primary" type="button" id="save" value="Save" />
		<div style="clear: both;"></div>
	</div>

	<script type="text/javascript">
		var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';

		function SetScaleValues() {
			var optionval = $("#badgeSelector option:selected")

			$('#lowValue').val($(optionval).data('low'));
			$('#highValue').val($(optionval).data('high'));
			$('#label').val($(optionval).data('lbl'));
		}

		$(document).ready(function () {

			//$('#TempScale').mobiscroll().select({
			//	theme: 'ios',
			//	display: popLocation,
			//	onSet: function (event, inst) {
			//		$('#save').click();
			//	}

			//});

			$('#save').click(function () {
				postForm($('#SensorScale_<%: Model.SensorID%>'));
			});

		});

	</script>
</form>
