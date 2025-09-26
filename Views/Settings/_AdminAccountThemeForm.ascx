<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.AccountTheme>" %>
<%bool isEnterprise = MonnitSession.IsEnterprise;%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("AccountID", "AccountID")%>
    </div>
    <div class="col sensorEditFormInput">
        <%
            if ((MonnitSession.IsCurrentCustomerMonnitSuperAdmin || MonnitSession.IsCurrentCustomerAccountThemeAdmin) && !isEnterprise)
            {%>

        <input id="AccountID" name="AccountID" type="text" value="<%=Model.AccountID %>"  class="form-control " >
        <%}
            else
            {%>
        <%: Html.Hidden("WhiteLabelReseller", string.Format("Present")) %>
        <%: Model.AccountID  %>
        <%} %>
    </div>
    <div class="clearfix"></div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Domain","Domain")%>
    </div>
    <div class="col sensorEditFormInput">
        <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin || (isEnterprise && MonnitSession.CurrentCustomer.IsAdmin) || MonnitSession.IsCurrentCustomerAccountThemeAdmin)
            { %>
        <%--        <%: Html.TextBoxFor(d => d.Domain)%>--%>
        <input id="Domain" name="Domain" type="text" value="<%=Model.Domain %>" class="form-control" >
        <%}
            else
            {%>
        <%: Model.Domain  %>
        <%} %>
        <%: Html.ValidationMessageFor(d => d.Domain) %>
    </div>
    <div class="clearfix"></div>
</div>

<%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin || (isEnterprise && MonnitSession.CurrentCustomer.IsAdmin) || MonnitSession.IsCurrentCustomerAccountThemeAdmin)
    { %>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|Theme Name","Theme Name")%>
    </div>
    <div class="col sensorEditFormInput">
        <%--        <%: Html.TextBoxFor(t=>t.Theme) %>--%>
        <input id="Theme" name="Theme" type="text" value="<%=Model.Theme %>" class="form-control " >
        <%: Html.ValidationMessageFor(d => d.Theme) %>
    </div>
    <div class="clearfix"></div>
</div>

<%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin || MonnitSession.IsCurrentCustomerAccountThemeAdmin)
    { %>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|SMTP User","SMTP User")%>
    </div>
    <div class="col sensorEditFormInput">
        <%--        <%: Html.TextBoxFor(t=>t.SMTPUser) %>--%>
        <input id="SMTPUser" name="SMTPUser" type="text" value="<%=Model.SMTPUser %>" class="form-control" required aria-required="true">
        <%: Html.ValidationMessageFor(d => d.SMTPUser) %>
    </div>
    <div class="clearfix"></div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|SMTP Password","SMTP Password")%>
    </div>
    <div class="col sensorEditFormInput">
        <%--        <%: Html.TextBoxFor(t=>t.SMTPPasswordPlainText) %>--%>
        <input id="SMTPPasswordPlainText" name="SMTPPasswordPlainText" type="password" value="<%=Model.SMTPPasswordPlainText %>" class="form-control " required aria-required="true">
        <%: Html.ValidationMessageFor(d => d.SMTPPasswordPlainText) %>
    </div>
    <div class="clearfix"></div>
</div>
<%} %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|SMTP Domain","SMTP Domain")%>
    </div>
    <div class="col sensorEditFormInput">
        <%--        <%: Html.TextBoxFor(t=>t.SMTP) %>--%>
        <input id="SMTP" name="SMTP" type="text" value="<%=Model.SMTP %>" class="form-control " required aria-required="true">
        <%: Html.ValidationMessageFor(d => d.SMTP) %>
    </div>
    <div class="clearfix"></div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|SMTP Port","SMTP Port")%>
    </div>
    <div class="col sensorEditFormInput">
        <%--        <%: Html.TextBoxFor(t=>t.SMTPPort) %>--%>
        <input id="SMTPPort" name="SMTPPort" type="text" value="<%=Model.SMTPPort %>" class="form-control " required aria-required="true">
        <%: Html.ValidationMessageFor(d => d.SMTPPort) %>
    </div>
    <div class="clearfix"></div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|SMTP Use SSL","SMTP Use SSL")%>
    </div>
    <div class="col sensorEditFormInput">
        <%--        <%: Html.TextBoxFor(t=>t.SMTPUseSSL) %>--%>
        <input id="SMTPUseSSL" name="SMTPUseSSL" type="text" value="<%=Model.SMTPUseSSL %>" class="form-control " required aria-required="true">
        <%: Html.ValidationMessageFor(d => d.SMTPUseSSL) %>
    </div>
    <div class="clearfix"></div>
</div>
<%} %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|From Email","From Email")%>
    </div>
    <div class="col sensorEditFormInput">
        <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin || (isEnterprise && MonnitSession.CurrentCustomer.IsAdmin) || MonnitSession.IsCurrentCustomerAccountThemeAdmin)
            { %>
        <%--        <%: Html.TextBoxFor(sdf=>sdf.SMTPDefaultFrom) %>--%>
        <input id="SMTPDefaultFrom" name="SMTPDefaultFrom" type="text" value="<%=Model.SMTPDefaultFrom %>" class="form-control "required aria-required="true">
        <%}
            else
            { %>
        <%: Model.SMTPDefaultFrom %>
        <%} %>
        <%: Html.ValidationMessageFor(d => d.SMTPDefaultFrom) %>
    </div>
    <div class="clearfix"></div>
</div>
<div class="row sensorEditForm" >
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|From Name","From Name")%>
    </div>
    <div class="col sensorEditFormInput">
        <%--        <%: Html.TextBoxFor(sfn=>sfn.SMTPFriendlyName) %>--%>
        <input id="SMTPFriendlyName" name="SMTPFriendlyName" type="text" value="<%=Model.SMTPFriendlyName %>" class="form-control " required aria-required="true">
        <%: Html.ValidationMessageFor(d => d.SMTPFriendlyName) %>
    </div>
    <div class="clearfix"></div>
</div>
<%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin || (isEnterprise && MonnitSession.CurrentCustomer.IsAdmin) || MonnitSession.IsCurrentCustomerAccountThemeAdmin)
    {%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|SMTP Return Path","SMTP Return Path")%>
    </div>
    <div class="col sensorEditFormInput">
        <%--        <%: Html.TextBoxFor(sfn=>sfn.SMTPReturnPath) %>--%>
        <input id="SMTPReturnPath" name="SMTPReturnPath" type="text" value="<%=Model.SMTPReturnPath %>" class="form-control " >
        <%: Html.ValidationMessageFor(d => d.SMTPReturnPath) %>
    </div>
    <div class="clearfix"></div>
</div>
<%} %>
<%if (!isEnterprise)
    { %>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|Send Subscription Notifications","Send Subscription Notifications")%>
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.CheckBoxFor(ssn => ssn.SendSubscriptionNotification) %>
    </div>
    <div class="clearfix"></div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|Send Maintenance Notifications","Send Maintenance Notifications")%>
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.CheckBoxFor(smn => smn.SendMaintenanceNotification) %>
    </div>
    <div class="clearfix"></div>
</div>
<%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)// until mobile push is full set up 
    { %>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|Send Mobile Push Notifications","Send Mobile Push Notifications")%>
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.CheckBoxFor(swn => swn.AllowPushNotification) %>
    </div>
    <div class="clearfix"></div>
</div>
<%} %>
<%} %>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|Send Webhook Notifications","Send Webhook Notifications")%>
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.CheckBoxFor(swn=>swn.SendWebhookNotification) %>
    </div>
    <div class="clearfix"></div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|Minimum Password Length","Minimum Password Length")%>
    </div>
    <div class="col sensorEditFormInput">
        <%--        <%: Html.TextBoxFor(mpl=>mpl.MinPasswordLength) %>--%>
        <input id="MinPasswordLength" name="MinPasswordLength" type="text" value="<%=Model.MinPasswordLength %>" class="form-control ">
        <%: Html.ValidationMessageFor(d => d.MinPasswordLength) %>
    </div>
    <div class="clearfix"></div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|Password Requires Mixed","Password Requires Mixed")%>
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.CheckBoxFor(swn=>swn.PasswordRequiresMixed) %>
    </div>
    <div class="clearfix"></div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|Password Requires Special","Password Requires Special")%>
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.CheckBoxFor(swn=>swn.PasswordRequiresSpecial) %>
    </div>
    <div class="clearfix"></div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|Password Requires Number","Password Requires Number")%>
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.CheckBoxFor(swn=>swn.PasswordRequiresNumber) %>
    </div>
    <div class="clearfix"></div>
</div>
<!-- TODO: For White Label only -->
<% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
    { %>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|Enable Two-Factor Authentication","Enable Two-Factor Authentication")%>
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.CheckBoxFor(swn => swn.IsTFAEnabled) %>
    </div>
    <div class="clearfix"></div>
</div>
<% } %>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|Password Valid for(in days)","Password Valid for(in days)")%>
    </div>
    <div class="col sensorEditFormInput">
        <%--        <%: Html.TextBoxFor(swn=>swn.PasswordLifetime) %>--%>
        <input id="PasswordLifetime" name="PasswordLifetime" type="text" value="<%=Model.PasswordLifetime %>" class="form-control " >
        <%: Html.ValidationMessageFor(d => d.PasswordLifetime) %>
    </div>
    <div class="clearfix"></div>
</div>
<%if ((MonnitSession.IsCurrentCustomerMonnitSuperAdmin || MonnitSession.IsCurrentCustomerAccountThemeAdmin) && !isEnterprise)
    { %>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|From Phone","From Phone")%>
    </div>
    <div class="col sensorEditFormInput">
        <%--        <%: Html.TextBoxFor(sfn=>sfn.FromPhone) %>--%>
        <input id="FromPhone" name="FromPhone" type="text" value="<%=Model.FromPhone %>" class="form-control ">
        <%: Html.ValidationMessageFor(d => d.FromPhone) %>
    </div>
    <div class="clearfix"></div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|From Alphanumeric","From Alphanumeric")%> [a-zA-Z0-9 +-_&]
    </div>
    <div class="col sensorEditFormInput">
        <input id="AlphanumericSenderID" name="AlphanumericSenderID" type="text" title="" pattern="[a-zA-Z0-9 +\-_&]+" value="<%=Model.AlphanumericSenderID %>" class="form-control ">
        <%: Html.ValidationMessageFor(d => d.AlphanumericSenderID) %>
    </div>
    <div class="clearfix"></div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|Current EULA","Current EULA")%>
    </div>
    <div class="col sensorEditFormInput">
        <%--        <%: Html.TextBoxFor(t=>t.CurrentEULA) %>--%>
        <input id="CurrentEULA" name="CurrentEULA" type="text" value="<%=Model.CurrentEULA %>" class="form-control ">
        <%: Html.ValidationMessageFor(d => d.CurrentEULA) %>
    </div>
    <div class="clearfix"></div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|Default Premium Length (Days)","Default Premium Length (Days)")%>
    </div>
    <div class="col sensorEditFormInput">
        <%--        <%: Html.TextBoxFor(t=>t.DefaultPremiumDays) %>--%>
        <input id="DefaultPremiumDays" name="DefaultPremiumDays" type="text" value="<%=Model.DefaultPremiumDays %>" class="form-control ">
        <%: Html.ValidationMessageFor(d => d.DefaultPremiumDays) %>
    </div>
    <div class="clearfix"></div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|Default Account Subscription Type ID","Default Account Subscription Type ID")%>
    </div>
    <div class="col sensorEditFormInput">
        <%--        <%: Html.TextBoxFor(t=>t.DefaultAccountSubscriptionTypeID) %>--%>
        <input id="DefaultAccountSubscriptionTypeID" name="DefaultAccountSubscriptionTypeID" type="text" value="<%=Model.DefaultAccountSubscriptionTypeID %>" class="form-control ">
        <%: Html.ValidationMessageFor(d => d.DefaultAccountSubscriptionTypeID) %>
    </div>
    <div class="clearfix"></div>
</div>
<%} %>
<%if (MonnitSession.CustomerCan("Unlock_User") && !isEnterprise)
    { %>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|Maximum Login Attempts before lock","Maximum Login Attempts before lock")%>
    </div>
    <div class="col sensorEditFormInput">
        <%--  <%: Html.TextBoxFor(sfn=>sfn.MaxFailedLogins) %>--%>
        <input id="MaxFailedLogins" name="MaxFailedLogins" type="text" value="<%=Model.MaxFailedLogins %>" class="form-control ">
        <%: Html.ValidationMessageFor(d => d.MaxFailedLogins) %>
    </div>
    <div class="clearfix"></div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("IsActive","IsActive")%>
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.CheckBoxFor(d=>d.IsActive) %>
    </div>
    <div class="clearfix"></div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|Enable Dashboard","Enable Dashboard")%>
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.CheckBoxFor(d=>d.EnableDashboard) %>
    </div>
    <div class="clearfix"></div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|Enable Classic","Enable Classic")%>
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.CheckBoxFor(d=>d.EnableClassic) %>
    </div>
    <div class="clearfix"></div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|Allow Account Creation","Allow Account Creation")%>
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.CheckBoxFor(d=>d.AllowAccountCreation) %>
    </div>
    <div class="clearfix"></div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|Supports SAML","Supports SAML")%>
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.CheckBoxFor(d=>d.SupportsSaml) %>
    </div>
    <div class="clearfix"></div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminAccountThemeForm|Allow PWA","Allow PWA")%>
    </div>

    <div class="col sensorEditFormInput">
        <%: Html.CheckBoxFor(d=>d.AllowPWA) %>
    </div>
    <div class="clearfix"></div>
</div>
<%}%>

<div class="clearfix"></div>
<div class="text-end">
    <a href="/Settings/AdminAccountTheme/" class="btn btn-light me-2"><%: Html.TranslateTag("Cancel","Cancel")%></a>
    <button id="prefSave" type="submit" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary">
        <%: Html.TranslateTag("Save","Save")%>
    </button>
</div>

<script type="text/javascript">

    $(function () {
        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
        $('select').addClass("form-control");
        $("input").addClass("form-control");
        $(":checkbox").removeClass("form-control");

        $('.ajax').click(function (e) {
            e.preventDefault();
            $.get($(this).attr("href"), function (data) {
                if (data != "Success") {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
                window.location.href = window.location.href;
            });
        });

        $('#AlphanumericSenderID').keyup(function (e) {
            var value = this.value.replaceAll(/[^a-zA-Z0-9 +\-_&]/g, '');
            this.value = value.substring(0,11);
        });
    });

</script>

<style>

    input[type=checkbox] {
        margin-top: 10px;
    }

</style>
