<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<MonnitApplication>>" %>

<%List<MonnitApplication> applist = new List<MonnitApplication>();
	applist = (Model.Count > 0) ? Model :  MonnitApplication.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID);
%>

<select id="applicationFilter" class="profileSelect form-select" style="width: 175px;">
	<option value="-1"><%: Html.TranslateTag("Overview/Index|All Sensor Profiles", "All Sensor Profiles")%></option>
	<%foreach (MonnitApplication App in applist)
		{%>
	<option value='<%: App.ApplicationID%>'><%:App.ApplicationName %></option>
	<%}%>
</select>
<script>
	$('.dropdown-menu option, .dropdown-menu select').click(function (e) {
		e.stopPropagation();
	});

	$('.profileSelect').change(function () {
		selectApplication();
	});

</script>

