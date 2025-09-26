<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<ReportRecipientData>>" %>

<% List<long> reportRecipientIDs = ViewBag.RecipientIDs;
   ReportSchedule report = ViewBag.Report;

    List<ReportRecipientData> recipients = ViewBag.ReportUsers as List<ReportRecipientData>;

   foreach (ReportRecipientData cust in Model.OrderBy(c => c.CompanyName).ThenBy(c => c.FullName))
   {
       bool IsActive = false;
       if (reportRecipientIDs.Contains(cust.CustomerID))
           IsActive = true;
%>
    <a style="cursor: pointer;" title="<%: Html.TranslateTag("Export/ReportRecipient|Click Recipient to enable/disable","Click Recipient to enable/disable")%>" onclick="toggleRecipient(<%: cust.CustomerID %>);">
		<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 repors-list-row"  style="font-size: 1.4em; display:flex; margin:1rem;">
            <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 divCellCenter holder holderInactive recipients-circle">
                <div class=" event-p ">
                    <%--<div class="sensor eventIcon eventIconStatus ListBorder<%:IsActive ? "Active":"NotActive" %> reportCustomer<%:cust.CustomerID%>"></div>--%>
                   <div class=" ListBorder<%:IsActive ? "Active":"NotActive" %> reportCustomer<%:cust.CustomerID%>" ><%=Html.GetThemedSVG("circle-profile") %></div> 
                </div>
            </div>

			<div class="recipients-place" >
				<div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 dfac" >
					<%= cust.FirstName%> <%= cust.LastName%> - <%:cust.CompanyName %>
				</div>

			<%--<%=cust.NotificationEmail %>--%>
				<div style="text-align: center;" class="">
				<div class="dfac" style="font-size: 1rem;align-content:center;padding-top:2px;"><%=cust.NotificationEmail %></div>
				</div>
			</div>

			<div class="col-lg-2 col-md-2 col-sm-2 col-xs-2" style="font-size: 1.2em;padding-top:2px;padding-right:10px;float:right;">
<%--				<i class="fa fa-envelope-o" style="float:right;"></i>--%>
			</div>
		</div>
    </a>

<div class="clearfix"></div>
<% } %>

<script type="text/javascript">

    function toggleRecipient(custID) {
        var addRecip = $('.reportCustomer' + custID).hasClass('ListBorderNotActive');
        var url = "/Export/ToggleReportRecipient/<%=report.ReportScheduleID%>";
        $.post(url, { customerID: custID, add: addRecip }, function (data) {
            if (data == 'Success') {
                if (addRecip) {
                    $('.reportCustomer' + custID).removeClass('ListBorderNotActive').addClass('ListBorderActive');
                }
                else {
                    $('.reportCustomer' + custID).removeClass('ListBorderActive').addClass('ListBorderNotActive');
                }
            }
            else {
                console.log(data);
                let values = {};
                    <%--values.redirect = '/Ack/<%:Model.NotificationRecordedID%>/<%:Model.NotificationGUID%>';--%>
                values.text = "<%=Html.TranslateTag("Oops! That did not work, please refresh your page. If this error continues, contact support.")%>";
                openConfirm(values);
                $('#modalCancel').hide();
            }
        });
    }

</script>