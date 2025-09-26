<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.SensorInformation>" %>

<% 
    string imagePath = "";
    if (Model.Sensor != null)
    {
        switch (Model.Sensor.Status)
        {
            case Monnit.eSensorStatus.OK:
                imagePath = Html.GetThemedContent("/images/good.png");
                break;
            case Monnit.eSensorStatus.Warning:
                imagePath = Html.GetThemedContent("/images/Alert.png");
                break;
            case Monnit.eSensorStatus.Alert:
                imagePath = Html.GetThemedContent("/images/alarm.png");
                break;
            //case Monnit.eSensorStatus.Inactive:
            //    imagePath = Html.GetThemedContent("/images/inactive.png");
            //    break;
            //case Monnit.eSensorStatus.Sleeping:
            //    imagePath = Html.GetThemedContent("/images/sleeping.png");
            //    break;
            case Monnit.eSensorStatus.Offline:
                imagePath = Html.GetThemedContent("/images/sleeping.png");
                break;
        }
    }
    %>
        
        <div class="sectionTitle">
            Sensor Information<br />&nbsp;
        </div>
        <div class="editor-label">
            Name (SensorID)
        </div>
        <div class="editor-field">
            <%= Model.Name %> (<%:Model.SensorID %>)
        </div>
        
    <%if (Model.Sensor != null) {%>
        <div class="editor-label">
            Application
        </div>
        <div class="editor-field">
            <%:Model.Sensor.MonnitApplication.ApplicationName%>
        </div>
        
        <div class="editor-label">
            Firmware Version
        </div>
        <div class="editor-field">
            <%:Model.Sensor.FirmwareVersion%>
        </div>
        
        <div class="editor-label">
            Radio Band
        </div>
        <div class="editor-field">
            <%:Model.Sensor.RadioBand%>
        </div>
        
        <div class="editor-label">
            Power Source
        </div>
        <div class="editor-field">
            <%:Model.Sensor.PowerSource.Name%>
        </div>
        
        <%if (Model.Sensor.IsDeleted || Model.Sensor.CSNetID == null)
          {%>
        <div class="editor-label">
        </div>
        <div class="editor-field" style="color:Red;">
            Deleted!
        </div>
        <%}%>
        <% if(Model.Network!=null) { %>
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
            <%=Model.Account.CompanyName%> 
            <a href="/Account/NetworkSettings/<%: Model.Account.AccountID %>?networkID=<%:Model.Network.CSNetID%>">Manage</a>
            <%if (MonnitSession.CustomerCan("Proxy_Login")){%>
                <a href="/Account/ProxyCustomer/<%:Model.Account.PrimaryContactID %>" onclick="proxyCustomer(this, '/Overview/Index/<%:Model.Network.CSNetID%>'); return false;"><img src="../..<%:Html.GetThemedContent("/images/proxy.png")%>" alt="Proxy Login" title="Proxy Login" /></a>
            <%} %>
            <%if (MonnitSession.CustomerCan("Account_View")){%>
           <a class="viewAccount" href="/Account/ProxySubAccount/" onclick="viewAccount(this); return false;" data-accountid="<%=Model.Account.AccountID %>">
            <img style="width: 25px;" src="../..<%=Html.GetThemedContent("/images/view.png")%>" alt="View Account" title="View Account" />
           </a>
             <%} %>
        </div>
        
        <div class="editor-label">
            Network
        </div>
        <div class="editor-field">
            <%= Model.Network.Name%> (<%:Model.Network.CSNetID%>)
        </div>
        

        <div class="editor-label">
            Pending Transaction
        </div>
        <div class="editor-field">
            <%:(!Model.Sensor.CanUpdate).ToString()%>
        </div>
        
        <div class="editor-label">
            Status
        </div>
        <div class="editor-field">
            <img src="<%: imagePath %>" /> <%:Model.Sensor.Status.ToString()%>
        </div>
         
        <div class="editor-label">
            Last Check In
        </div>
        <div class="editor-field">
            <% if (Model.Sensor.LastCommunicationDate < DateTime.UtcNow.AddMinutes(5))
               { %>
            <%: Monnit.TimeZone.GetLocalTimeById(Model.Sensor.LastCommunicationDate, Model.Account.TimeZoneID).ToShortDateString()%>
            <%: Monnit.TimeZone.GetLocalTimeById(Model.Sensor.LastCommunicationDate, Model.Account.TimeZoneID).ToShortTimeString()%>
            <% } %>
        </div>
    <%} %>
    
        <% if (MonnitSession.CustomerCan("Support_Advanced"))
           { %>
        <div class="editor-label">
            Edit
        </div>
        <div class="editor-field">
            <a href="/Settings/SensorEdit/<%: Model.SensorID %>" >Admin Edit</a>
        </div>
        <%} %>
<%} %>
    <div style="clear:both;"></div> 

<% Html.RenderPartial("DeviceLookup", null, new ViewDataDictionary()); %>
<script>
    function viewAccount(lnk){
        var anchor = $(lnk);
        var acctID = anchor.data('accountid');
        var href = anchor.attr('href');

        $.post(href, { id: acctID }, function (data) {
            if (data == "Success")
                window.location.href = "/";
            else
                showSimpleMessageModal("<%=Html.TranslateTag("View account failed")%>");
        });
    }
</script>
    