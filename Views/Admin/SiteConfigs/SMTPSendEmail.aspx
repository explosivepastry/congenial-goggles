<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SMTPSendEmail
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%AccountTheme acctTheme = MonnitSession.CurrentTheme; %>
    <div class="formtitle">SMTP (Sending Email)</div>
    <form action="/Admin/EditSiteConfigs" method="post">

    <%: Html.Hidden("formName", "SMTPSendEmail")  %>
        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
    <%if(MonnitSession.IsCurrentCustomerMonnitAdmin){ %>
    
    <div class="formBody">

    <table style="width: 100%;">
        <tr>
            <td>SMTP Host</td>
            <td>
                <input type="text" name="SMTP" value="<%: acctTheme.SMTP %>" required="required"/></td>
        </tr>
        <tr>
            <td>Port</td>
            <td>
                <input type="text" name="SMTPPort" value="<%:  acctTheme.SMTPPort %>" required="required"/></td>
        </tr>
        <tr>
            <td>User</td>
            <td>
                <input type="text" name="SMTPUser" value="<%:  acctTheme.SMTPUser %>" required="required"/></td>
        </tr>
        <tr>
             <%string password = string.Empty;
               if (acctTheme.SMTPPassword.Length > 0)
              {
                  try
                  {
                      password = acctTheme.SMTPPassword.Decrypt();
                  }
                  catch
                  {
                      password = acctTheme.SMTPPassword;
                  }
              }%>
            <td>Password</td>
            <td>
                <input type="text" name="SMTPPassword" value="<%: password %>" required="required"/></td>
        </tr>
        <tr>
            <td>From Address</td>
            <td>
                <input type="text" name="SMTPDefaultFrom" value="<%: acctTheme.SMTPDefaultFrom %>" required="required"/></td>
        </tr>
        <tr>
            <td>From Name</td>
            <td>
                <input type="text" name="SMTPFriendlyName" value="<%: !string.IsNullOrWhiteSpace(acctTheme.SMTPFriendlyName) ? acctTheme.SMTPFriendlyName : "" %>" required="required"/></td>
        </tr>
        <tr>
            <td>Use SSL</td>
            <td>
                <select name="SMTPUseSSL">
                    <option value="False">False</option>
                    <option value="True" <%: acctTheme.SMTPUseSSL.ToBool()? "selected=selected" : "" %>>True</option>
                </select>
            </td>
        </tr>
        <%if(MonnitSession.IsCurrentCustomerMonnitSuperAdmin){ %>
        <tr>
            <td>Return Path</td>
            <td>
                <input type="text" name="SMTPReturnPath" value="<%: acctTheme.SMTPReturnPath %>"/></td>
        </tr>

        <%--<tr>
            <td>Bcc User</td>
            <td>
                <input type="text" name="SMTPBccUser" value="<%: ConfigData.FindValue("SMTPBccUser") %>" /></td>
        </tr>--%>

        <tr>
            <td colspan="2"></td>
        </tr>
        <%} %>
    </table>
    </div>
    <%} %>

    <div class="buttons">
        <input type="submit" value="Save" class="bluebutton"/>
        <span style="color: red; padding: 15px; display: inline-block;"><%:ViewBag.Result ?? "" %></span>
        <div style="clear: both;"></div>
    </div>
    </form>

</asp:Content>
