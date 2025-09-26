<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Account>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminSettings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%--<%Html.RenderPartial("AdminLink", Model); %>--%>

    <div class="x_panel">
        <div class="x_title">
            <h2><%: Html.TranslateTag("Settings/AdminSettingsList|Admin Settings List","Admin Settings List")%></h2>
            <div class="clearfix"></div>
        </div>
        <div class="x_content"></div>
        <!--  This is the Overview equivalant of the "/Admin/Settings/" view. -->
        <% if (MonnitSession.IsCurrentCustomerMonnitAdmin)
   {%>
<li><a href="/Admin/Administrative"><%: Html.TranslateTag("Settings/AdminSettingsList|Administrative Settings","Administrative Settings")%></a></li>
<%}%>

<% if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Support_Advanced"))
   {%>
<li><a href="/Admin/InboundDataPacket"><%: Html.TranslateTag("Settings/AdminSettingsList|InboundPackets","InboundPackets")%></a></li>
<%}%>

<% if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Support_Advanced"))
   {%>
<li><a href="/Admin/OutboundDataPacket"><%: Html.TranslateTag("Settings/AdminSettingsList|OutBoundPackets","OutBoundPackets")%></a></li>
<%}%>

<% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
   {%>
<li><a href="/Admin/SensorMessageAuditLookUp"><%: Html.TranslateTag("Settings/AdminSettingsList|Sensor Message Audit Tool","Sensor Message Audit Tool")%></a></li>
<%}%>

<% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
   {%>
<li><a class="sf-with-ul" href="/Report/AdminManagement"><%: Html.TranslateTag("Settings/AdminSettingsList|Report Builder","Report Builder")%></a></li>
<%}%>

<% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
   {%>
<li><a href="/Admin/AuthorizeNet"><%: Html.TranslateTag("Settings/AdminSettingsList|Authorize.Net Settings","Authorize.Net Settings")%></a></li>
<%}%>

<% if (MonnitSession.IsCurrentCustomerMonnitAdmin)
   {%>
<li><a href="/Admin/AutomatedEmails"><%: Html.TranslateTag("Settings/AdminSettingsList|Automated Email Settings","Automated Email Settings")%></a></li>
<%}%>

<% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
   {%>
<li><a href="/Admin/CellularUsage"><%: Html.TranslateTag("Settings/AdminSettingsList|Cellular Usage Settings","Cellular Usage Settings")%></a></li>
<%}%>

<% if (MonnitSession.IsCurrentCustomerMonnitAdmin)
   {%>
<li><a href="/Admin/SMTPSendEmail"><%: Html.TranslateTag("Settings/AdminSettingsList|SMTP Send Mail Settings","SMTP Send Mail Settings")%></a></li>
<%}%>

<% if (MonnitSession.IsCurrentCustomerMonnitAdmin)
   {%>
<li><a href="/Admin/ExceptionLogging"><%: Html.TranslateTag("Settings/AdminSettingsList|Exception Logging","Exception Logging")%></a></li>
<%}%>

<% if ((/*MonnitSession.IsCurrentCustomerReseller &&*/ MonnitSession.CurrentCustomer.AccountID == MonnitSession.CurrentTheme.AccountID) || MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
    {%>
<li><a href="/Admin/MaintenanceWindows"><%: Html.TranslateTag("Settings/AdminSettingsList|Maintenance","Maintenance")%></a></li>
<%}%>

<% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
   {%>
<li><a href="/Admin/MassEmail"><%: Html.TranslateTag("Settings/AdminSettingsList|Mass Email","Mass Email")%></a></li>
<%}%>


<% if ((/*MonnitSession.IsCurrentCustomerReseller &&*/ MonnitSession.CurrentCustomer.AccountID == MonnitSession.CurrentTheme.AccountID) || MonnitSession.IsCurrentCustomerMonnitSuperAdmin || MonnitSession.IsCurrentCustomerAccountThemeAdmin)
    { %>
<li><a class="sf-with-ul" href="/Admin/CreateChangeAccountTheme"><%: Html.TranslateTag("Settings/AdminSettingsList|White Label Settings","White Label Settings")%></a></li>
<%}%>

<% if ((/*MonnitSession.IsCurrentCustomerReseller &&*/ MonnitSession.CurrentCustomer.AccountID == MonnitSession.CurrentTheme.AccountID) || MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
    { %>
<li><a class="sf-with-ul" href="/Admin/EditSensorApplication"><%: Html.TranslateTag("Settings/AdminSettingsList|Sensor Application List","Sensor Application List")%></a></li>
<li><a class="sf-with-ul" href="/Admin/EditSMSCarriersList"><%: Html.TranslateTag("Settings/AdminSettingsList|SMS Carriers List","SMS Carriers List")%></a></li>
<%}%>

<% if (MonnitSession.IsCurrentCustomerMonnitAdmin /*|| MonnitSession.IsCurrentCustomerReseller*/ || MonnitSession.IsCurrentCustomerAccountThemeAdmin)
    {%>
<li><a class="sf-with-ul" href="/Email/EmailTemplateOverview"><%: Html.TranslateTag("Settings/AdminSettingsList|Email Templates","Email Templates")%></a></li>

<%}%>

<% if ((/*MonnitSession.IsCurrentCustomerReseller &&*/ MonnitSession.CurrentCustomer.AccountID == MonnitSession.CurrentTheme.AccountID) || MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
    { %>
<li><a class="sf-with-ul" href="/Admin/ReleaseNote"><%: Html.TranslateTag("Settings/AdminSettingsList|Release Note","Release Note")%></a></li>
<%}%>


<% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin || MonnitSession.CustomerCan("Support_Advanced"))
   { %>
<li><a class="sf-with-ul" href="/Settings/SensorEdit"><%: Html.TranslateTag("Settings/AdminSettingsList|Admin Edit Sensor","Admin Edit Sensor")%></a></li>
<%}%>


<% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin || MonnitSession.CustomerCan("Support_Advanced"))
   { %>
<li><a class="sf-with-ul" href="/Settings/GatewayEdit"><%: Html.TranslateTag("Settings/AdminSettingsList|Admin Edit Gateway","Admin Edit Gateway")%></a></li>
<%}%>

<%--<% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
   {%>
<li><a href="/Admin/FirmwareUpload">Sensor Firmware Upload</a></li>
<%}%>--%>

<%-- if superadmin Monnit only --%>
<%-- if monnitadmin Enterprise admin --%>
<%-- White Label Reselers have same rights as monnitadmin but can have access to superadmin specific li --%>
<%-- Standard Resellers have access only to edit email templates --%>

    </div>

</asp:Content>
