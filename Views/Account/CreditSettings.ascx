<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CreditSetting>" %>
<div class="formtitle">Credit Notification Settings</div>
<form id="CreditPreferenceSettingsForm" action="/Account/CreditSetting/<%:Model.CreditSettingID%>" class="ESPForm" method="post">
    <%: Html.Hidden("accountID",MonnitSession.CurrentCustomer.AccountID)%>
   
    <table width="100%">
        <tr>
            <td colspan="2">These settings allow you to customize when a credit notification value should be sent out.</td>
        </tr>
        <tr>
            <td><div class="editor-label">Last Email Date</div></td>
            <td><%: Model.LastEmailDate%></td>
        </tr>
        <tr>
            <td><div class="editor-label">Credits Available</div></td>
            <td><%: Model.CreditsAvailable < 0 ? "0": Model.CreditsAvailable.ToString() %></td>
        </tr>
        <tr>
            <td>
                <div class="editor-label">
                 if available credits fall below:
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.CreditCompareValue)%>
                </div>
                <div class="editor-error"><%:Html.ValidationMessageFor(model => model.CreditCompareValue) %></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="editor-label">
                    User to be notified:
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%List<Customer> custlist = Customer.LoadAllByAccount(Model.AccountID);%>
                    <%:Html.DropDownListFor(model => model.UserId, custlist, "FullName") %>
                </div>
                <div class="editor-error">
                    
                </div>
            </td>
        </tr>
        <tr>
            <td id="espResponse"><script type="text/javascript">
                                     $(function () {
                                         $('#submitCreditSettings').click(function ()
                                         {
                                             var Form = $('#CreditPreferenceSettingsForm');
                                             var formCollection = Form.serialize();
                                             $.post("/Account/CreditSetting/<%:Model.CreditSettingID%>", formCollection , function (data)
                                             {
                                                 if (data == "Success") {
                                                     window.location.href = "/Account/NotificationCreditList";
                                                 }
                                                 else {
                                                     showSimpleMessageModal("<%=Html.TranslateTag("Failed to save credit settings")%>");
                                                 }
                                             });
                                         });
                                     });
            </script></td>
            <td><input id="submitCreditSettings" class="bluebutton preferenceSubmit" type="button" value="Save" style="float: none;"/></td>
        </tr>    
    </table>
</form>

