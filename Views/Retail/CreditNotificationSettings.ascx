<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.CreditSetting>" %>


<form method="post" id="CreditPreferenceSettingsForm" class="form-horizontal form-label-left">

    <%--    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>--%>

    <%: Html.Hidden("accountID",MonnitSession.CurrentCustomer.AccountID)%>
    <input type="hidden" id="CreditClassification" name="CreditClassification" value="<%=eCreditClassification.Notification.ToInt() %>">
    <!-- Hardcode to eCreditClassification.Notification-->

    <div class="row">
        <div class="bold" style="font-weight: bold;">
            <div class="form-group" style="padding-top: 5px; margin-left: 20px;">
                <%: Html.TranslateTag("Retail/CreditNotificationSettings|Notify when credits remaining is below:")%>: 
            </div>
        </div>
        <div class="col-xs-8 col-sm-8 col-md-8" style="">
            <div class="form-group">
                <input class="aSettings__input_input" id="CreditCompareValue" name="CreditCompareValue" type="text" value="<%=Model.CreditCompareValue %>">
            </div>
        </div>
    </div>
    <div class="row">
        <div class="bold" style="font-weight: bold;">
            <div class="form-group" style="padding-top: 5px; margin-left: 20px;">
                <%: Html.TranslateTag("Retail/CreditNotificationSettings|User to be notified","User to be notified")%>: 
            </div>
        </div>
        <div class="col-xs-8 col-sm-8 col-md-8" style="margin-left: 25px;">
            <div class="form-group">
                <%--                <%List<Customer> custlist = Customer.LoadAllByAccount(Model.AccountID);%>--%>
                <%List<Customer> custlist = Customer.LoadAllForReseller(MonnitSession.CurrentCustomer.CustomerID, Model.AccountID); %>
                <select class="tzSelect" id="UserId" name="UserId" style="width: 152px; height: 25px;">
                    <%foreach (Customer cust in custlist)
                        { %>
                    <option value="<%=cust.CustomerID %>" <%=cust.CustomerID == Model.UserId ? "selected='selected'" : "" %>><%=cust.FullName %></option>
                    <%} %>
                </select>
            </div>
        </div>
        <div class="bold col-xs-4 col-sm-4 col-md-4"></div>
        <div class="col-xs-8 col-sm-8 col-md-8 media-btn-right">
            <button type="button" id="submitCreditSettings" value="<%:Html.TranslateTag("Save","Save")%>" class="gen-btn">
                <%:Html.TranslateTag("Save","Save")%>
                    &nbsp;
                    <svg xmlns="http://www.w3.org/2000/svg" width="13" height="13" viewBox="0 0 18 18">
                        <path id="ic_save_24px" d="M17,3H5A2,2,0,0,0,3,5V19a2,2,0,0,0,2,2H19a2.006,2.006,0,0,0,2-2V7ZM12,19a3,3,0,1,1,3-3A3,3,0,0,1,12,19ZM15,9H5V5H15Z" transform="translate(-3 -3)" fill="#fff" />
                    </svg>
            </button>
        </div>
    </div>



    <!--
    <div class="row">
        <div class="form-group row">
            <div id="notimessage" class="bold col-xs-12 col-sm-3 col-md-9" style="color: red;">
            </div>
            <div class="col-xs-12 col-sm-3 col-md-3">
                <input type="button" id="submitCreditSettings"  value="<%:Html.TranslateTag("Save","Save")%>" class="btn btn-primary" />
            </div>
        </div>
    </div>
-->

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


