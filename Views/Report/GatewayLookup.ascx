<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.GatewayInformation>" %>

<% 

    string imagePath = "";
    if (Model.Gateway != null)
    {
        if (Model.Gateway.LastCommunicationDate == DateTime.MinValue)
            imagePath = Html.GetThemedContent("/images/sleeping.png");
        else if (Model.Gateway.ReportInterval != double.MinValue && Model.Gateway.LastCommunicationDate.AddMinutes(Model.Gateway.ReportInterval * 2 + 1) < DateTime.UtcNow)//Missed more than one heartbeat + one minute to take drift into account
            imagePath = Html.GetThemedContent("/images/Alert.png");

        if (Model.Gateway.IsDirty)
            imagePath = imagePath.Replace(".png", "-dirty.png");
    }
%>

<div class="sectionTitle">
    Gateway Information<br />&nbsp;
</div>
<div class="editor-label">
    Name (GatewayID)
</div>
<div class="editor-field">
    <%= Model.Name %> (<%:Model.GatewayID %>)
</div>

<%if (Model.Gateway != null)
  {%>
<div class="editor-label">
    Gateway Type
</div>
<div class="editor-field">
    <%:Model.Gateway.GatewayType.Name.Replace("_", " ")%>
</div>

<div class="editor-label">
    Gateway Firmware Version
</div>
<div class="editor-field">
    <%:Model.Gateway.GatewayFirmwareVersion%>
</div>

<div class="editor-label">
    Radio Firmware Version
</div>
<div class="editor-field">
    <%:Model.Gateway.APNFirmwareVersion%>
</div>

<div class="editor-label">
    Radio Band
</div>
<div class="editor-field">
    <%:Model.Gateway.RadioBand%>
</div>

<% if (Model.Gateway.GatewayTypeID == 17 || Model.Gateway.GatewayTypeID == 18 || Model.Gateway.GatewayTypeID == 22 || Model.Gateway.GatewayTypeID == 23)
   {%>
<div class="editor-label">
    MEID
</div>
<div class="editor-field">
    <% try
       { %>
    <%: Convert.ToInt64(Model.Gateway.MacAddress.Split('|')[0]).ToString("X")%>
    <% }
       catch { } %>
</div>
<div class="editor-label">
    Phone
</div>
<div class="editor-field">
    <% try
       { %>
    <%:Model.Gateway.MacAddress.Split('|')[1].Insert(6,"-").Insert(3,") ").Insert(0, "(") %>
      <% }
       catch { } %>
</div>


<% }
   else if (string.IsNullOrWhiteSpace(Model.Gateway.MacAddress))
   {  %>
<div class="editor-label">
    MacAddress
</div>
<div class="editor-field">
    <%:Model.Gateway.MacAddress %>
</div>
<% } %>

<%if (Model.Gateway.IsDeleted || Model.Network == null)
  {%>
<div class="editor-label">
</div>
<div class="editor-field" style="color: Red;">
    Deleted!
</div>
<%}%>
<% if (Model.Network != null) { %>
<div class="editor-label">
    Parent
</div>
<div class="editor-field">
    <%
        Account Parent = Account.Load(Model.Account.RetailAccountID);
        AccountTheme Theme = null;
        if (Parent != null)
            Theme = AccountTheme.Find(Parent);
                
        if(Theme != null){ %>
        <a href="http://<%: Theme.Domain %>"><%= Parent != null ? Parent.CompanyName : "" %></a>
    <%} else { %>
        <%= Parent != null ? Parent.CompanyName : "" %>
    <%} %>
</div>
<div class="editor-label">
    Account
</div>
<div class="editor-field">
    <%= Model.Account.CompanyName%>
    <a href="/Account/NetworkSettings/<%: Model.Account.AccountID %>?networkID=<%:Model.Network.CSNetID%>">Manage</a>
    <%if (MonnitSession.CustomerCan("Proxy_Login"))
      {%>
    <a href="/Account/ProxyCustomer/<%:Model.Account.PrimaryContactID %>" onclick="proxyCustomer(this); return false;">
        <img src="<%:Html.GetThemedContent("/images/proxy.png")%>" alt="Proxy Login" title="Proxy Login" /></a>
    <%} %>
</div>

<div class="editor-label">
    Network
</div>
<div class="editor-field">
    <%= Model.Network.Name%> (<%:Model.Network.CSNetID%>)
</div>

<div class="editor-label">
    Status
</div>
<div class="editor-field">
    <img src="<%= imagePath %>" />
</div>

<div class="editor-label">
    Last Check In
</div>
<div class="editor-field">
    <% if (Model.Gateway.LastCommunicationDate < DateTime.UtcNow)
       { %>
    <%: Monnit.TimeZone.GetLocalTimeById(Model.Gateway.LastCommunicationDate, Model.Account.TimeZoneID).ToShortDateString()%>
    <%: Monnit.TimeZone.GetLocalTimeById(Model.Gateway.LastCommunicationDate, Model.Account.TimeZoneID).ToShortTimeString()%>
    <% } %>
</div>
<%} %>

<% if (MonnitSession.CustomerCan("Support_Advanced"))
   { %>
<div class="editor-label">
    Edit
</div>
<div class="editor-field">
    <a href="/Settings/GatewayEdit/<%: Model.GatewayID %>">Admin Edit</a>
</div>
<%} %>

<%} %>

<div style="clear: both;"></div>


<% Html.RenderPartial("DeviceLookup", null, new ViewDataDictionary()); %>