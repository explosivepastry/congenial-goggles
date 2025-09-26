<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<% if (MonnitSession.IsCurrentCustomerMonnitAdmin)
   {%>
<li><a href="/Admin/Administrative">Administrative Settings</a></li>
<%}%>

<% if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Support_Advanced"))
   {%>
<li><a href="/Admin/InboundDataPacket">InboundPackets</a></li>
<%}%>

<% if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Support_Advanced"))
   {%>
<li><a href="/Admin/OutboundDataPacket">OutBoundPackets</a></li>
<%}%>

<% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
   {%>
<li><a href="/Admin/SensorMessageAuditLookUp">Sensor Message Audit Tool</a></li>
<%}%>

<% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
   {%>
<li><a class="sf-with-ul" href="/Report/AdminManagement">Report Builder</a></li>
<%}%>

<% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
   {%>
<li><a href="/Admin/AuthorizeNet">Authorize.Net Settings</a></li>
<%}%>

<% if (MonnitSession.IsCurrentCustomerMonnitAdmin)
   {%>
<li><a href="/Admin/AutomatedEmails">Automated Email Settings</a></li>
<%}%>

<% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
   {%>
<li><a href="/Admin/CellularUsage">Cellular Usage Settings</a></li>
<%}%>

<% if (MonnitSession.IsCurrentCustomerMonnitAdmin)
   {%>
<li><a href="/Admin/SMTPSendEmail">SMTP Send Mail Settings</a></li>
<%}%>

<% if (MonnitSession.IsCurrentCustomerMonnitAdmin)
   {%>
<li><a href="/Admin/ExceptionLogging">Exception Logging</a></li>
<%}%>

<% if ((/*MonnitSession.IsCurrentCustomerReseller &&*/ MonnitSession.CurrentCustomer.AccountID == MonnitSession.CurrentTheme.AccountID) || MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
    {%>
<li><a href="/Admin/MaintenanceWindows">Maintenance</a></li>
<%}%>

<% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
   {%>
<li><a href="/Admin/MassEmail">Mass Email</a></li>
<%}%>


<% if ((/*MonnitSession.IsCurrentCustomerReseller &&*/ MonnitSession.CurrentCustomer.AccountID == MonnitSession.CurrentTheme.AccountID) || MonnitSession.IsCurrentCustomerMonnitSuperAdmin || MonnitSession.IsCurrentCustomerAccountThemeAdmin)
    { %>
<li><a class="sf-with-ul" href="/Admin/CreateChangeAccountTheme">White Label Settings</a></li>
<%}%>

<% if ((/*MonnitSession.IsCurrentCustomerReseller &&*/ MonnitSession.CurrentCustomer.AccountID == MonnitSession.CurrentTheme.AccountID) || MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
    { %>
<li><a class="sf-with-ul" href="/Admin/EditSensorApplication">Sensor Application List</a></li>
<li><a class="sf-with-ul" href="/Admin/EditSMSCarriersList">SMS Carriers List</a></li>
<%}%>

<% if (MonnitSession.IsCurrentCustomerMonnitAdmin /*|| MonnitSession.IsCurrentCustomerReseller*/ || MonnitSession.IsCurrentCustomerAccountThemeAdmin)
    {%>
<li><a class="sf-with-ul" href="/Email/EmailTemplateOverview">Email Templates</a></li>

<%}%>

<% if ((/*MonnitSession.IsCurrentCustomerReseller &&*/ MonnitSession.CurrentCustomer.AccountID == MonnitSession.CurrentTheme.AccountID) || MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
    { %>
<li><a class="sf-with-ul" href="/Admin/ReleaseNote">Release Note</a></li>
<%}%>


<% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin || MonnitSession.CustomerCan("Support_Advanced"))
   { %>
<li><a class="sf-with-ul" href="/Settings/SensorEdit">Admin Edit Sensor</a></li>
<%}%>


<% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin || MonnitSession.CustomerCan("Support_Advanced"))
   { %>
<li><a class="sf-with-ul" href="/Settings/GatewayEdit">Admin Edit Gateway</a></li>
<%}%>

<%--<% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
   {%>
<li><a href="/Admin/FirmwareUpload">Sensor Firmware Upload</a></li>
<%}%>--%>

<%-- if superadmin Monnit only --%>
<%-- if monnitadmin Enterprise admin --%>
<%-- White Label Reselers have same rights as monnitadmin but can have access to superadmin specific li --%>
<%-- Standard Resellers have access only to edit email templates --%>