<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AccountLocationSearchModel>" %>
<!-- CorporateLeafCards.ascx BEGIN  -->

<% 
    string sensorStatus = "sensorStatus";
    if (Model.DevicesAlerting > 0)
        sensorStatus += "Alert";
    else if (Model.DevicesWarning > 0)
        sensorStatus += "Warning";
    else if (Model.DevicesOffline > 0)
        sensorStatus += "Offline";
    else
        sensorStatus += "OK";

%>
<div class="searchCardDiv corp-card" style="width: 18rem;" data-search="<%=Model.AccountNumber %>">
    <a style="width: 100%; height: 100%; display: flex;" href="/Account/ProxySubAccount/<%=Model.AccountID %>" onclick="viewAccountQuick(this); return false;" data-accountid="<%=Model.AccountID %>" title="<%: Html.TranslateTag("Settings/LocationOverviewDetails|Account Sensors", "Account Sensors")%>">
        <div class="corp-status <%= sensorStatus %>"></div>

        <div class="corp-top-title" title="">

        
         <div> <%=Model.AccountNumber %></div>  
          
           

            <div class="corp-grid-boxes">
                <div title="Alerting" class="corp-circle leaf-alert"><%=Html.GetThemedSVG("single-alert-red") %><div class="db-leaf-number"><%=Model.DevicesAlerting %></div>
                </div>
                <div title="Warning" class="corp-circle leaf-battery"><%=Html.GetThemedSVG("single-batlow-y") %><div class="db-leaf-number"><%=Model.DevicesWarning %></div>
                </div>
                <div title="Offline" class="corp-circle leaf-wifi"><%=Html.GetThemedSVG("single-wifioff") %><div class="db-leaf-number"><%=Model.DevicesOffline %></div>
                </div>
                <div title="Online" class="corp-circle leaf-check"><%=Html.GetThemedSVG("single-check") %><div class="db-leaf-number"><%=Model.DevicesOK %></div>
                </div>
            </div>
        </div>

    </a>
    <%: Html.Partial("_LocationCardMenu") %>
</div>
<script>
  
</script>

<!-- CorporateLeafCards.ascx END  -->