<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Account>" %>

<div class="formtitle">
    Notification Credits
            
</div>
<div class="formBody">

    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
    <table style="float: left; width: auto;">
        <tr>
            <td></td>
            <td>
                <div class="credText center">Used</div>
            </td>
            <td>
                <div class="credText center">Remaining</div>
            </td>
            <td>
                <div class="credText center">Expiration</div>
            </td>
        </tr>
        <%foreach (NotificationCredit creditgroup in NotificationCredit.LoadAvailable(Model.AccountID))
            {%>
        <tr style="border-top: solid #888888 1px;" title="<%:creditgroup.ActivatedCredits %> Credits, Activated <%:creditgroup.ActivationDate.ToShortDateString() %>">
            <td>
                <div class="credText c1 left" style="width: 150px;"><%:creditgroup.NotificationCreditType.Name %></div>
            </td>
            <td>
                <div class="credBox"><%:creditgroup.UsedCredits %></div>
            </td>
            <td>
                <div class="credBox"><%:creditgroup.RemainingCredits %></div>
            </td>
            <td>
                <div class="credText c1"><%:creditgroup.ExpirationDate > DateTime.UtcNow.AddYears(10) ? "" : creditgroup.ExpirationDate.ToShortDateString() %> </div>
            </td>
            <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
                {%>
            <td><a href="/Notification/RemoveCredits/<%:creditgroup.NotificationCreditID %>" onclick="removeCredits(this); return false;">
                <img src="/Content/images/delete.png" alt="Delete"></a></td>
            <%} %>
        </tr>
        <%} %>
    </table>

    <div class="credDesc">
        <div><span class="credText c1">Email Notification -</span> No credits required</div>
        <div><span class="credText c1">External Provider SMS Notification -</span> No credits required (<i>Not available in all areas</i>)</div>
        <div><span class="credText c1">Direct SMS notification -</span> Credits based on recipients country</div>
        <div><span class="credText c1">Voice Notification -</span> Credits based on recipients country</div>
    </div>

    <div style="clear: both;">&nbsp;</div>

    <div style="margin-top: 20px; float: right;">
        <%:Html.Partial("../Account/NotificationCreditsLink", Model) %>
        <table>
            <tr>
                <td align="right">
                    <%--<input type="text" id="NotificationCreditActivation" />--%>
                    <input id="KeyActivation" placeholder="<%: Html.TranslateTag("e.g. xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")%>" class="aSettings__input_input" type="text" style="height: 25px; width: 100%; max-width: 300px; border: none; border: 1px solid #e3e3e3; border-radius: 4px 0px 0px 4px;" />
                </td>
                <td>
                    <%--  <a href="/Checkout/ActivateNotificationCredits/<%:Model.AccountID %>?activationCode=" class="bluebutton" onclick="activateCredits(this); return false;">Redeem Code</a>--%>
                    <a style="right: 90px;position: relative;" href="/Retail/ActivateCredits/<%:Model.AccountID %>?activationCode=" class="bluebutton" onclick="activateKey(this, 1); return false;">Redeem Code</a> <span id="codeSubmitError" style="font: 0.8em; padding: 5px;"></span>
                </td>
            </tr>
        </table>
    </div>
    <div style="clear: both;">&nbsp;</div>
    <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
        {%>
    <div style="clear: both; margin-bottom: 10px; width: 100%; border-top: 1px solid #888;"></div>
    <div class="credAssign">
        <span class="credText c1" style="margin-right: 10px;">Assign Credits:</span>
        <input type="text" id="NotificationCreditsToAssign" style="margin-right: 20px;" />
        <span class="credText c1" style="margin-right: 10px;">Expiration Date:</span>
        <input type="text" id="AssignedExpiration" class="datepicker" style="margin-right: 20px;" />
        <a href="/Notification/AssignCredits/<%:Model.AccountID %>?notificationCreditsToAssign=" class="bluebutton" style="margin-top: -2px;" onclick="assignCredits(this); return false;">Assign</a>
    </div>
    <%} %>
    <div style="clear: both;"></div>
</div>
<%if (MonnitSession.CustomerCan("Account_Edit"))
    {

        CreditSetting cs = Monnit.CreditSetting.LoadByAccountID(Model.AccountID);
        if (cs == null)
        {
            cs = new CreditSetting();
            cs.AccountID = Model.AccountID;
            cs.CreditCompareValue = 0;
            cs.LastEmailDate = DateTime.MinValue;
            cs.CreditClassification = eCreditClassification.Notification;
            cs.UserId = MonnitSession.CurrentCustomer.CustomerID;

        }
%>
<%:Html.Partial("~/Views/Account/CreditSettings.ascx",cs) %>
<%}%>

<div class="buttons">
    <%:Html.Partial("AccountButtons") %>
    <div style="clear: both;"></div>
</div>

<script type="text/javascript">
    $(function () {
        $('.datepicker').datepicker();
        $('#NotificationCreditActivation').watermark('Have a credit code');
    });
    //function activateCredits(a) {
    //    var url = $(a).attr("href") + $('#NotificationCreditActivation').val();
    //    $.post(url, "", function (data) {
    //        if (data == "Success")
    //            window.location.href = window.location.href;
    //        else
    //            alert(data);
    //    }, "text");
    //}
    function activateKey(a) {
        if ($('#KeyActivation').val().length === 0) {
            $('#KeyActivation').attr('placeholder', 'Code Required');
            return;
        }

        $('#codeSubmitError').html(`
        <div id="loadingGIF" class="text-center" style="display: none;">
            <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
                <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
            </div>
        </div>
    `);
}
        
        var url = $(a).attr("href") + $('#KeyActivation').val() + '&creditClassification=1';
        $.post(url, "", function (data) {
            if (data.match("^Success")) {
                var SKU = data.split('_')[1];
                var parts = SKU.split('-');
                switch (parts[1]) {
                    case "NC":
                        showSimpleMessageModal("<%=Html.TranslateTag("Success: Purchased Notifications Credits")%>");
                        break;
                    case "HX":
                        showSimpleMessageModal("<%=Html.TranslateTag("Success: Purchased HX Message Credits")%>");
                        break;
                    case "SP":
                        showSimpleMessageModal("<%=Html.TranslateTag("Success: Purchased SensorPrint Credits")%>");
                        break;
                    case "IP":
                        showSimpleMessageModal("<%=Html.TranslateTag("Success: Account Subscription")%>");
                        break;
                    default:
                        showSimpleMessageModal("<%=Html.TranslateTag("Success")%>");
                        break;
                }
                window.location.href = window.location.href;
            }
            else {
                console.log(data);
                $('#codeSubmitError').html('');
                let values = {};
<%--values.redirect = '/Ack/<%:Model.NotificationRecordedID%>/<%:Model.NotificationGUID%>';--%>
                values.text = "<%=Html.TranslateTag("Oops! That did not work, please refresh your page. If this error continues, contact support.")%>";
                openConfirm(values);
                $('#modalCancel').hide();

            }
        });
    }

<%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
    {%>
    function assignCredits(a) {
        var url = $(a).attr("href") + $('#NotificationCreditsToAssign').val();
        var expiration = $('#AssignedExpiration').val()
        if (expiration.length > 0)
            url += "&expiration=" + expiration;

        $.post(url, "", function (data) {
            if (data.includes("Success"))
                window.location.href = window.location.href;
            else
                console.log(data);
            let values = {};
<%--values.redirect = '/Ack/<%:Model.NotificationRecordedID%>/<%:Model.NotificationGUID%>';--%>
            values.text = "<%=Html.TranslateTag("Oops! That did not work, please refresh your page. If this error continues, contact support.")%>";
            openConfirm(values);
            $('#modalCancel').hide();
        }, "text");
    }

    function removeCredits(a) {
        $.post($(a).attr("href"), "", function (data) {
            if (data == "Success")
                window.location.href = window.location.href;
            else
                console.log(data);
            let values = {};
<%--values.redirect = '/Ack/<%:Model.NotificationRecordedID%>/<%:Model.NotificationGUID%>';--%>
            values.text = "<%=Html.TranslateTag("Oops! That did not work, please refresh your page. If this error continues, contact support.")%>";
            openConfirm(values);
            $('#modalCancel').hide();
        }, "text");
    }

    <%}%>
</script>
