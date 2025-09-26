<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.CreditSetting>" %>


<form method="post" id="CreditPreferenceSettingsForm" class="form-horizontal form-label-left">

    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>

    <%: Html.Hidden("accountID",MonnitSession.CurrentCustomer.AccountID)%>

    <div class="row">
        <div class="bold" style="font-weight:bold;">
            <div class="form-group" style="margin-left:20px;">
                <%: Html.TranslateTag("Retail/CreditNotificationSettings|Credits Available","Credits Available")%>:
            </div>
        </div>
        <div class="col-8" style="margin-left:25px;">
            <div class="form-group">
                <%: Model.CreditsAvailable < 0 ? "0": Model.CreditsAvailable.ToString() %>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="bold" style="font-weight:bold;">
            <div class="form-group" style="padding-top:5px;margin-left:20px;">
                <%: Html.TranslateTag("Retail/CreditNotificationSettings|Credit Threshold","Credit Threshold")%>: 
            </div>
        </div>
        <div class="col-8" style="margin-left:25px;">
            <div class="form-group">
                <input class="aSettings__input_input" style="width:152px; height:25px; border: none;border: 1px solid #e3e3e3;border-radius: 4px;" id="CreditCompareValue" name="CreditCompareValue" type="text" value="<%=Model.CreditCompareValue %>">
            </div>
        </div>
    </div>
    <div class="row">
        <div class="bold" style="font-weight:bold;">
            <div class="form-group" style="padding-top:5px;margin-left:20px;">
                <%: Html.TranslateTag("Retail/CreditNotificationSettings|User to be notified","User to be notified")%>: 
            </div>
        </div>
        <div class="col-8" style="margin-left:25px;">
            <div class="form-group">
<%--                <%List<Customer> custlist = Customer.LoadAllByAccount(Model.AccountID);%>--%>
                    <%List<Customer> custlist = Customer.LoadAllForReseller(MonnitSession.CurrentCustomer.CustomerID, Model.AccountID); %>

                <select class="tzSelect" id="UserId" name="UserId" style="width:152px;height:25px;">
                    <%foreach(Customer cust in custlist ){ %>
                    <option value="<%=cust.CustomerID %>" <%=cust.CustomerID == Model.UserId ? "selected='selected'" : "" %> ><%=cust.FullName %></option>
                    <%} %>
                </select>
		</div></div>
        <div class="bold col-4"></div>
		<div class="col-8 media-btn-right">
				<button type="button" id="submitCreditSettings"  value="<%:Html.TranslateTag("Save","Save")%>" class="btn btn-primary">
                    <%:Html.TranslateTag("Save","Save")%>
                </button>
		</div>
	</div>

</form>

<script type="text/javascript">
    $(function () {
        var failedCredit = '<%: Html.TranslateTag("Retail/CreditNotificationSettings|Failed to save credit settings","Failed to save credit settings")%>';


        $('#submitCreditSettings').click(function () {
            var creditform = $('#CreditPreferenceSettingsForm').serialize();;
           
            $.post("/Retail/CreditSetting/<%:Model.CreditSettingID%>", creditform, function (data) {
                if (data == "Success") {
                    window.location.href = "/Retail/NotificationCredit/<%=Model.AccountID%>";
                }
                else {
                    showSimpleMessageModal("<%=Html.TranslateTag("Failed to save credit settings")%>");
                }
            });
        });


    });
</script>


