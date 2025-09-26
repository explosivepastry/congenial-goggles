<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.AccountTheme>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AccountTheme
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- //purgeclassic --%>
    <div class="formtitle">AccountTheme</div>

    <% using (Html.BeginForm())
       { %>
    <%: Html.ValidationSummary(false) %>
    <%: Html.Hidden("AccountThemeID",Model.AccountThemeID) %>
    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
    <div class="formBody">
        <table style="width: 100%">
            <tr>
                <td>AccountID:
                </td>
                <td>
                    <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
                      {%>
                    <%: Html.TextBox("AccountID", Model.AccountID != long.MinValue && Model.AccountID != null ? Model.AccountID.ToString()  : " " )%>
                    <%}
                      else
                      {%>
                    <%: Html.Hidden("WhiteLabelReseller",string.Format("Present")) %>
                    <%: Model.AccountID  %>
                    <%} %>
                </td>
                <td><%: Html.ValidationMessageFor(d => d.AccountID) %></td>
            </tr>
            <tr>
                <td>Theme Domain:
                </td>
                <td>
                    <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
                      { %>
                    <%: Html.TextBoxFor(d => d.Domain)%>
                    <%}
                      else
                      {%>
                    <%: Model.Domain  %>
                    <%} %>
                </td>
                <td><%: Html.ValidationMessageFor(d => d.Domain) %></td>
            </tr>


            <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
              { %>
            <tr>
                <td>Theme Name:</td>
                <td><%: Html.TextBoxFor(t=>t.Theme) %>
                </td>
                <td><%: Html.ValidationMessageFor(d => d.Theme) %></td>
            </tr>
            <tr>
                <td>SMTP Domain:</td>
                <td><%: Html.TextBoxFor(smtp=>smtp.SMTP) %>
                </td>
                <td><%: Html.ValidationMessageFor(d => d.SMTP) %></td>
            </tr>
            <tr>
                <td>SMTP Port:</td>
                <td><%: Html.TextBoxFor(sport=>sport.SMTPPort) %>
                </td>
                <td><%: Html.ValidationMessageFor(d => d.SMTPPort) %></td>
            </tr>
            <tr>
                <td>SMTP User:</td>
                <td><%: Html.TextBoxFor(suser=>suser.SMTPUser) %>
                </td>
                <td><%: Html.ValidationMessageFor(d => d.SMTPUser) %></td>
            </tr>
            <tr>
                <td>SMTP Password:</td>
                <td><%: Html.TextBoxFor(spass=>spass.SMTPPasswordPlainText) %>
                </td>
                <td><%: Html.ValidationMessageFor(d => d.SMTPPasswordPlainText) %></td>
            </tr>
            <tr>
                <td>SMTP Use SSL:</td>
                <td>
                    <%: Html.CheckBoxFor(suSSL=> suSSL.SMTPUseSSL) %></td>
                <td></td>
            </tr>
            <%} %>
            <tr>
                <td>From Email:</td>
                <td><%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
                      { %>
                    <%: Html.TextBoxFor(sdf=>sdf.SMTPDefaultFrom) %>
                    <%}
                      else
                      { %>
                    <%: Model.SMTPDefaultFrom %>
                    <%} %>
                </td>
                <td><%: Html.ValidationMessageFor(d => d.SMTPDefaultFrom) %></td>
            </tr>
            <tr>
                <td>From Name:</td>
                <td><%: Html.TextBoxFor(sfn=>sfn.SMTPFriendlyName) %>
                </td>
                <td><%: Html.ValidationMessageFor(d => d.SMTPFriendlyName) %></td>
            </tr>

            <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
              {%>
            <tr>
                <td>SMTP Return Path:</td>
                <td><%: Html.TextBoxFor(theme => theme.SMTPReturnPath)%></td>
                <td><%: Html.ValidationMessageFor(d => d.SMTPReturnPath) %></td>
            </tr>
            <%} %>

            <tr>
                <td>Send Subscription Notifications</td>
                <td><%: Html.CheckBoxFor(ssn=>ssn.SendSubscriptionNotification) %> </td>
                <td></td>
            </tr>
            <tr>
                <td>Send Maintenance Notifications</td>
                <td><%: Html.CheckBoxFor(smn=>smn.SendMaintenanceNotification) %> </td>
                <td></td>
            </tr>
            <tr>
                <td>Send Webhook Notifications</td>
                <td><%: Html.CheckBoxFor(swn=>swn.SendWebhookNotification) %> </td>
                <td></td>
            </tr>
            <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)// until mobile push is full set up 
              { %>
            <tr>
                <td>Send Mobile Push Notifications</td>
                <td><%: Html.CheckBoxFor(swn=>swn.AllowPushNotification) %> </td>
                <td></td>
            </tr>
            <%} %>
            <tr>
                <td>Minimum Password Length</td>
                <td><%: Html.TextBoxFor(mpl=>mpl.MinPasswordLength) %></td>
                <td><%: Html.ValidationMessageFor(d => d.MinPasswordLength) %></td>
            </tr>
            <tr>
                <td>Password Requires Mixed</td>
                <td><%: Html.CheckBoxFor(prm=>prm.PasswordRequiresMixed) %> </td>
                <td></td>
            </tr>
            <tr>
                <td>Password Requires Special</td>
                <td><%: Html.CheckBoxFor(prs=>prs.PasswordRequiresSpecial) %> </td>
                <td></td>
            </tr>
            <tr>
                <td>Password Requires Number</td>
                <td><%: Html.CheckBoxFor(prn=>prn.PasswordRequiresNumber) %> </td>
                <td></td>
            </tr>
            <tr>
                <td>Password Valid for(in days)</td>
                <td><%: Html.TextBoxFor(ped=>ped.PasswordLifetime) %></td>
                <td><%: Html.ValidationMessageFor(d => d.PasswordLifetime) %></td>
            </tr>
            <%--          
        <tr>
            <td>Show SMS Subject Feild</td>
            <td><%: Html.CheckBoxFor(sssf=>sssf.ShowSMSSubject) %> </td>
              <td></td>
        </tr>
            --%>
            <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
              { %>
            <tr>
                <td>From Phone:</td>
                <td><%: Html.TextBoxFor(sfn=>sfn.FromPhone) %>
                </td>
                <td><%: Html.ValidationMessageFor(d => d.FromPhone) %></td>
            </tr>
            <tr>
                <td>Current EULA:</td>
                <td><%: Html.TextBoxFor(ce=>ce.CurrentEULA) %>
                </td>
                <td><%: Html.ValidationMessageFor(d => d.CurrentEULA) %></td>
            </tr>
            <tr>
                <td>Default Premium Length (Days):</td>
                <td><%: Html.TextBoxFor(t=>t.DefaultPremiumDays) %>
                </td>
                <td><%: Html.ValidationMessageFor(d => d.DefaultPremiumDays) %></td>
            </tr>
            <tr>
                <td>Default Subscription Type ID:</td>
                <td><%: Html.TextBoxFor(t=>t.DefaultAccountSubscriptionTypeID) %>
                </td>
                <td><%: Html.ValidationMessageFor(d => d.DefaultAccountSubscriptionTypeID) %></td>
            </tr>
            <%} %>
            <%if (MonnitSession.CustomerCan("Unlock_User"))
              { %>
            <tr>
                <td>Maximum Login Attempts before lock:</td>
                <td><%: Html.TextBoxFor(sfn=>sfn.MaxFailedLogins) %>
                </td>
                <td><%: Html.ValidationMessageFor(d => d.MaxFailedLogins) %></td>
            </tr>
            <tr>
                <td>IsActive</td>
                <td><%: Html.CheckBoxFor(d=>d.IsActive) %> </td>
                <td></td>
            </tr>
            <tr>
                <td>Enable Dashboard</td>
                <td><%: Html.CheckBoxFor(d=>d.EnableDashboard) %> </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <input class="bluebutton" type="submit" title="save" style="margin: -5px -513px -17px 0px;" />
                    <span><%: string.IsNullOrWhiteSpace(ViewBag.Result) ?"" :ViewBag.Result  %></span>
                    <div style="clear: both;"></div>
                </td>
            </tr>



            <%}
       }%>
        </table>
    </div>

    <% Html.RenderPartial("~/Views/Admin/AccountThemeContactList.ascx"); %>
</asp:Content>
