<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.ExternalSubscriptionPreference>" %>
<div class="formtitle"> Notifications</div>
<form id="ExternalDataPreferenceSettingsForm" action="/Account/ExternalDataPreferenceSettings/<%:Model.ExternalSubscriptionPreferenceID%>" class="ESPForm" method="post" style="padding-left: 20px;padding-right: 20px;">
    <%: Html.HiddenFor(model => model.AccountID)%>
  <%--<% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>--%>

    <table>
       
        <tr>
            <td colspan="2">These settings allow you to customize when and to whom emails are sent when your External Data Push fails multiple times in a row. You will only be notified once every 24 hours, unless your data push failures exceed 100, in which case you will be notified of your data push being disabled.</td>
        </tr>
        <tr>
            <td><div class="editor-label">Last Email Date </div></td>
            <td><%: Model.LastEmailDate.ToLocalDateTimeShort()%></td>
        </tr>
        <tr>
            <td><div class="editor-label">Current Broken Count</div></td>
            <td><%: Model.LargestBrokenCount()%></td>
        </tr>
        <tr>
            <td>
                <div class="editor-label">
                Broken Count Threshold
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.UsersBrokenCountLimit)%>
                </div>
                <div class="editor-error"><%:Html.ValidationMessageFor(model => model.UsersBrokenCountLimit) %></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="editor-label">
                    User to be notified
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%List<Customer> custlist = Customer.LoadAllByAccount(Model.AccountID);%>

                    <%=Html.DropDownListFor(model => model.UserId, custlist, "FullName") %>
                </div>
                <div class="editor-error">
                    
                </div>
            </td>
        </tr>
        <tr>
            <td id="espResponse"><script type="text/javascript">
                                     $(function () {
                                         $('#submitExternalData').click(function () {
                                             var Form = $('#ExternalDataPreferenceSettingsForm');
                                             var formCollection = Form.serialize();
                                             $.post("/Account/ExternalDataPreferenceSettings/<%:Model.ExternalSubscriptionPreferenceID%>", formCollection, function (data) {
                                                 if (data == "Success") {
                                                     window.location.href = "/RestAPI/NotificationSettings/<%:Model.ExternalSubscriptionPreferenceID%>";
                                                 }
                                                 else {
                                                     showSimpleMessageModal("<%=Html.TranslateTag("Failed to save credit settings")%>");
                                                 }
                                             });
                                         });
                                    });
            </script></td>
            <td><input id="submitExternalData" class="bluebutton preferenceSubmit" type="button" value="Save" style="float: none;"/></td>
        </tr>    
    </table>
    
</form>

