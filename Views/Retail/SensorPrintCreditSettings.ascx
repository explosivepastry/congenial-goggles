<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.CreditSetting>" %>


<%--<form method="post" id="CreditPreferenceSettingsForm" class="form-horizontal form-label-left">--%>

    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>

    <%: Html.Hidden("accountID",MonnitSession.CurrentCustomer.AccountID)%>
    <input type="hidden" name="creditClassification" value="<%=eCreditClassification.SensorPrint.ToInt() %>" />

    <div class="row" style="margin: 0px 0px 15px 5px;">
        <table class="table table-hover" style="background-color: white; margin: 0px; padding: 0px;">
            <thead>
                <tr style="background-color: #e3e3e3;">
                    <th></th>
                    <th style="color: black; padding: 6px;"><%: Html.TranslateTag("Retail/NotificationCredit|Used","Used")%></th>
                    <th style="color: black; padding: 6px;"><%: Html.TranslateTag("Retail/NotificationCredit|Remaining","Remaining")%></th>
                    <th style="color: black; padding: 6px;"><%: Html.TranslateTag("Expiration","Expiration")%></th>
                    <th style="color: black; padding: 6px;"><%: Html.TranslateTag("Activation Date","Activation Date")%></th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <%foreach (Credit creditgroup in Credit.LoadAvailable(Model.AccountID,eCreditClassification.SensorPrint))
                    {%>
                <tr>
                    <th style="color: black; padding: 6px;"><%:creditgroup.CreditType.Name %></th>
                    <td style="padding: 6px;"><%:creditgroup.UsedCredits %></td>
                    <td style="padding: 6px;"><%:creditgroup.RemainingCredits %></td>
                    <td style="padding: 6px;"><%:creditgroup.ExpirationDate > DateTime.UtcNow.AddYears(10) ? "N/A" : creditgroup.ExpirationDate.ToShortDateString() %></td>
                    <td style="padding: 6px;"><%:creditgroup.ActivationDate > DateTime.UtcNow.AddYears(10) ? "N/A" : creditgroup.ActivationDate.ToShortDateString() %></td>
                    <td style="padding: 0px; padding-top: 6px;"><%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
                                                                    {%>
                        <a title="<%: Html.TranslateTag("Retail/NotificationCredit|Remove Credits","Remove Credits")%>" href="/Retail/RemoveCredits/<%:creditgroup.CreditID %>" onclick="removeCredits(this); return false;">
                            <%=Html.GetThemedSVG("delete") %></a></td>
                    <%} %>
                </tr>
                <%} %>
                 <tr style="background-color: #e3e3e3; ">
                    <th style="color:black;"><%: Html.TranslateTag("Retail/NotificationCredit|Total","Total")%></th>
                    <th style="color: black; padding: 6px;"></th>
                    <th style="font-weight:500; padding: 6px;"><%: Model.CreditsAvailable < 0 ? "0": Model.CreditsAvailable.ToString() %></th>
                    <th style="color: black; padding: 6px;"></th>
                    <th></th>
                </tr>
            </tbody>
        </table>
    </div>




<script type="text/javascript">
    $(function () {
        var failedCredit = '<%: Html.TranslateTag("Retail/CreditNotificationSettings|Failed to save settings","Failed to save settings")%>';


        $('#submitCreditSettings').click(function () {
            var creditform = $('#CreditPreferenceSettingsForm').serialize();;

            $.post("/Retail/CreditSetting/<%:Model.CreditSettingID%>", creditform, function (data) {
                if (data == "Success") {
                    window.location.href = "/Retail/SensorPrints/<%=Model.AccountID%>";
                }
                else {
                    showSimpleMessageModal("<%=Html.TranslateTag("Failed to save settings")%>");
                }
            });
        });


    });
</script>


