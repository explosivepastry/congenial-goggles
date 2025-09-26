<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Sensor Overview
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #circle-checkmark svg path {
            fill:white;
        }
    </style>

    <%="" %>
    <script>
        var systemHelpID = '';
    </script>
    <div class="container-fluid">
        <% 
            Customer customer = Customer.Load(MonnitSession.CurrentCustomer.CustomerID);
            List<SystemHelp> systemHelps = SystemHelp.LoadByAccount(customer.AccountID);
            bool ReloadHelps = false;
            foreach (SystemHelp s in systemHelps)
            {
                if (s.Type == "Customer_Setup" && s.CustomerID == customer.CustomerID)
                {
                    if (!string.IsNullOrEmpty(customer.FirstName)
                    && !string.IsNullOrEmpty(customer.LastName)
                    && !customer.NotificationPhone.Equals(""))
                    {
                        s.Delete(); //Auto Remove if nothing left to do
                        ReloadHelps = true;
                    }
                }
            }
            if (ReloadHelps) systemHelps = SystemHelp.LoadByAccount(customer.AccountID);

            List<CertificationAcknowledgementModel> CertsExpiring = CertificateNotification.LoadByCustomerID(MonnitSession.CurrentCustomer.CustomerID);
            Boolean profileNotComplete = false;

            if (customer.FirstName.IsEmpty()
                || customer.LastName.IsEmpty()
                //|| (customer.SendMaintenanceNotificationToEmail.Equals(false) /*&& customer.SendMaintenanceNotificationToPhone.Equals(false)*/)
                || customer.NotificationPhone.Equals(""))
                profileNotComplete = true;
        %>
        <div id="fullForm" class="col-12">

            <% if (systemHelps.Count == 0 && ViewBag.gwCount == 0 && ViewBag.sensCount == 0 && CertsExpiring.Count == 0)
                { %>

            <div style="display: flex; flex-direction: column; justify-content: center; align-items: center; height: 100vh">
                <h4>You have no Devices added to your account.</h4>
                <% if (MonnitSession.CustomerCan("Network_Edit"))
                    { %>
                <a class="btn btn-primary" href="/Network/NetworkSelect?AccountID=<%=MonnitSession.CurrentCustomer.AccountID %> ">
                    <% } %>
                    <%=Html.GetThemedSVG("add") %>
                        &nbsp; <%: Html.TranslateTag("Add Device", "Add Device")%>
                </a>
            </div>

            <%} %>
            <%if (systemHelps.Count > 0 || CertsExpiring.Count > 0)
                { %>
            <div class="alert alert-success mt-4" role="alert" id="alertBox">
                <div style="display: flex; justify-content: space-between;">
                    <%if (systemHelps.Count == 0 && CertsExpiring.Count > 0)
                        { %>
                    <h2><%:Html.TranslateTag("Your Account has Sensor Calibration Certificates that Expire Soon.")%></h2>
                    <%}
                        else
                        { %>
                    <h2><%:Html.TranslateTag("Welcome to your account. In order to begin using the system, please complete the fields from the categories below.")%>
                    </h2>
                    <%} %>
                    <svg onclick="removeSystemHelp()" xmlns="http://www.w3.org/2000/svg" class="closeBtn" viewBox="0 0 79.175 79.175">
                        <path id="Union_1" data-name="Union 1" d="M9.9,79.9,4.243,74.246a6,6,0,0,1,0-8.485L27.931,42.073,4.95,19.092a7,7,0,0,1,0-9.9L9.193,4.95a7,7,0,0,1,9.9,0L42.073,27.931,65.761,4.243a6,6,0,0,1,8.485,0L79.9,9.9a6,6,0,0,1,0,8.486L56.215,42.073,79.2,65.054a7,7,0,0,1,0,9.9L74.953,79.2a7,7,0,0,1-9.9,0L42.073,56.215,18.385,79.9a6,6,0,0,1-8.486,0Z" transform="translate(-2.486 -2.486)" />
                    </svg>
                </div>
                <div class="x_content" style="display: flex; flex-wrap: wrap;">
                    <% foreach (SystemHelp s in systemHelps)
                        {
                    %>
                    <script>
                        systemHelpID = '<%=s.SystemHelpID%>';
                    </script>
                    <%if (ViewBag.gwCount == 0 || ViewBag.sensCount == 0 || CSNet.LoadByAccountID(ViewBag.AccountID).Count == 0 || profileNotComplete || CertsExpiring.Count > 0)
                        {%>



                    <%if (CSNet.LoadByAccountID(ViewBag.AccountID).Count == 0)
                        { %>
                    <div class="gridPanel eventsList-small mini-card rounded mx-1 shadow-sm">
                        <div style="width: 100%;">
                            <a href="/Network/Create/<%:ViewBag.accountID %>">
                                <div class="viewSensorDetails eventsList__tr innerCard-holder" style="height: 60px; width: 200px;">
                                    <div class="col-xs-3" style="display: flex; justify-content: space-around; align-items: center; flex-direction: row;">
                                        <%=Html.GetThemedSVG("network") %>
                                    </div>
                                    <div class="col-xs-6" style="display: flex; align-items: center;">
                                        <%: Html.TranslateTag("Add a Network", "Add a Network")%>
                                    </div>
                                </div>
                            </a>
                        </div>

                    </div>
                    <%} %>
                    <%if (profileNotComplete)
                        { %>
                    <div class="gridPanel eventsList mini-card rounded mx-1 shadow-sm">
                        <div style="width: 100%;">
                            <a href="/Settings/UserDetail/<%:customer.CustomerID %>">
                                <div class="viewSensorDetails eventsList__tr innerCard-holder" style="height: 60px;">
                                    <div class="col-xs-3 px-2" style="display: flex; justify-content: space-around; align-items: center; flex-direction: row;">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" viewBox="0 0 85.294 68.294">
                                            <g transform="translate(-203.095 -441.077)">
                                                <path style="fill: #666;" id="Path_9" d="M261.506,506.57a5.746,5.746,0,0,1-5.01,2.756q-23.471.06-46.94.008a6.294,6.294,0,0,1-6.434-6.566,57.76,57.76,0,0,1,.22-7.924c.952-6.732,4.792-11.368,10.986-14.074a17.469,17.469,0,0,1,8.608-1.257,22.8,22.8,0,0,1,4.018,1.231,21.428,21.428,0,0,0,14.005-.651c2.874-1.123,5.672-.629,8.486.072.239.06.5.623.5.956.039,2.86.024,5.721.022,8.582a12.563,12.563,0,0,0,3.907,9.5C256.423,501.641,258.952,504.1,261.506,506.57Z" />
                                                <path style="fill: #666;" id="Path_10" d="M249.968,458.138a17.044,17.044,0,1,1-17.025-17.061A17.028,17.028,0,0,1,249.968,458.138Z" />
                                                <path style="fill: #666;" id="Path_11" d="M264.007,475.412c2.254,0,4.516-.114,6.761.037a6.772,6.772,0,0,1,4.244,2.2q5.9,5.862,11.772,11.753c2.108,2.115,2.153,4.481.056,6.6q-5.957,6.03-11.987,11.987a4.142,4.142,0,0,1-6.143.035q-6.229-6.131-12.359-12.365a7.648,7.648,0,0,1-2.121-5.4c-.037-3.531-.031-7.063-.006-10.6a4.256,4.256,0,0,1,4.484-4.448c1.766-.02,3.532,0,5.3,0Zm-1.2,11.484a3.173,3.173,0,0,0,3.135-3.164,3.242,3.242,0,0,0-3.233-3.212,3.327,3.327,0,0,0-3.158,3.21A3.2,3.2,0,0,0,262.806,486.9Z" />
                                            </g>
                                        </svg>
                                    </div>
                                    <div class="col-xs-6" style="display: flex; align-items: center;">
                                        <%: Html.TranslateTag("Complete Your Profile", "Complete Your Profile")%>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                    <%} %>
                    <%if (ViewBag.gwCount == 0 && CSNet.LoadByAccountID(ViewBag.AccountID).Count > 0)
                        { %>
                    <div class="gridPanel eventsList mini-card rounded mx-1 shadow-sm">
                        <div style="width: 100%;">
                            <a href="<%:ViewBag.simpleSetupAssignDevice %>">
                                <div class="viewSensorDetails eventsList__tr innerCard-holder" style="height: 60px;">
                                    <div class="col-xs-3 px-2" style="display: flex; justify-content: space-around; align-items: center; flex-direction: row;">
                                        <%=Html.GetThemedSVG("gateway") %>
                                    </div>
                                    <div class="col-xs-6" style="display: flex; align-items: center;">
                                        <%: Html.TranslateTag("Add Your First Gateway", "Add Your First Gateway")%>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                    <%} %>
                    <%if (ViewBag.sensCount == 0 && CSNet.LoadByAccountID(ViewBag.AccountID).Count > 0)
                        { %>
                    <div class="gridPanel eventsList-sm mini-card rounded mx-1 shadow-sm">
                        <div style="width: 100%;">
                            <a href="<%:ViewBag.simpleSetupAssignDevice %>">
                                <div class="viewSensorDetails eventsList__tr innerCard-holder" style="height: 60px;">
                                    <div class="col-xs-3 px-2" style="display: flex; justify-content: space-around; align-items: center; flex-direction: row;">
                                        <%=Html.GetThemedSVG("sensor") %>
                                    </div>
                                    <div class="col-xs-6" style="display: flex; align-items: center;">
                                        <%: Html.TranslateTag("Add Your First Sensor", "Add Your First Sensor")%>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                    <%} %>


                    <%}
                        }%>
                    <%  if (CertsExpiring.Count > 0)
                        {%>
                    <div class="gridPanel eventsList-small mini-card rounded mx-1 shadow-sm">
                        <div style="width: 100%;">
                            <a href="/Network/SensorCertificateExpiring/<%=customer.AccountID %>">
                                <div class="viewSensorDetails eventsList__tr innerCard-holder" style="height: 60px;">
                                    <div class="col-xs-3 px-2" style="display: flex; justify-content: space-around; align-items: center; flex-direction: row;">
                                        <%=Html.GetThemedSVG("certificate") %>
                                    </div>
                                    <div class="col-xs-6" style="display: flex; align-items: center;">
                                        <%: Html.TranslateTag("Calibration Certificates Expiring Soon")%>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                    <%}%>
                </div>
                <div style="clear: both;"></div>
            </div>
            <%  }%>



            <% if (ViewBag.gwCount > 0 || ViewBag.sensCount > 0 || CSNet.LoadByAccountID(ViewBag.AccountID).Count > 0)
                {%>
            <div class="formtitle ">
                <div id="MainTitle" class="dashboardCard_row">

                    <%if (ViewBag.sensCount > 0)
                        { %>

    <%--------------   Active Sensors--%>
                    <a class="top-location-card  db-home-0" href="/Overview/SensorIndex">
                         <div class=" dashboardCard__icon ">
      <?xml version="1.0" encoding="UTF-8"?>
<svg id="uuid-822c0a17-d37c-4e5b-9b15-c140067e1cee" data-name="Layer 1" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 437 437">
  <path d="m426.5,218.5c0,114.9-93.1,208-208,208S10.5,333.4,10.5,218.5,103.6,10.5,218.5,10.5s208,93.1,208,208Zm-416,0c0,114.9,93.1,208,208,208s208-93.1,208-208S333.4,10.5,218.5,10.5,10.5,103.6,10.5,218.5Zm107.69,5.63l70.15,62.65,130.48-136.54" fill="none" stroke="#fff" stroke-linecap="round" stroke-linejoin="round" stroke-width="21"/>
</svg>
                                <%--      <%=Html.GetThemedSVG("activeSensors") %>--%>
<%--<svg id="uuid-f884e5bf-27c2-4d60-b3e7-090ded7ee0fb" data-name="Layer 1" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 446 446">
  <path d="m431,223c0,114.9-93.1,208-208,208S15,337.9,15,223,108.1,15,223,15s208,93.1,208,208Zm-416,0c0,114.9,93.1,208,208,208s208-93.1,208-208S337.9,15,223,15,15,108.1,15,223Zm107.69,5.63l70.15,62.65,130.48-136.54" fill="none" stroke="#fff" stroke-linecap="round" stroke-linejoin="round" stroke-width="30"/>
</svg>--%>
            </div>
                        <div class="dbCard_title">
                            <div class="dashboardCard__title__type">
                       <%--            <%=Html.GetThemedSVG("sensor") %>--%>
   
                                <%: Html.TranslateTag("Active Sensors", "Active Sensors")%>
                            </div>
                            <div class="dashboardCard__title__number">
                                <%= ViewBag.sensActiveCount %> / <%= ViewBag.sensCount %>
                            </div>
                        </div>                  
                    </a>

     <%-- ---------  Alerting Sensors--%>
                    <a class="top-location-card  db-home-1" href="/Events">
                        <div class=" dashboardCard__icon ">
                <%=Html.GetThemedSVG("db-white-alert") %>
            </div>
                         <div class="dbCard_title">
                        <div class="dashboardCard__title__type">
                  <%: Html.TranslateTag("Alerting Sensors", "Alerting Sensors")%>
                        </div>
                           <div class="dashboardCard__title__number">
                                <%= ViewBag.sensAlertCount %> 
                            </div>
                             </div>
       <%-- <a class="col-lg-3 dashboardCard shadow-sm rounded dc_alertingSensors dashboardCard-center" href="/Rule/Index">
                        <div class="col-lg-6 dashboardCard__title">
                       <div class="dashboardCard__title__type">
                                     <%=Html.GetThemedSVG("sensor") %>
                                <%: Html.TranslateTag("Alerting Sensors", "Alerting Sensors")%>
                            </div>
                            <div class="dashboardCard__title__number">
                                <%= ViewBag.sensAlertCount %>
                            </div>
                        </div>        --%>         
                    </a>


                    <%} %>

                    <%if (ViewBag.gwCount > 0)
                        { %>

          <%--  ------------        Active Gateways--%>
                    <a class="top-location-card  db-home-0" href="/Overview/GatewayIndex">
                           <div class=" dashboardCard__icon">
                               <?xml version="1.0" encoding="UTF-8"?>
<svg id="uuid-dae1082e-0bcc-485e-a757-5b53e85c592c" data-name="Layer 1" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 437 437">
  <path d="m211.2,119.49c2.29,2.25,5.9,2.45,8.43.47,25.99-21.69,63.78-21.69,89.77,0,2.54,1.94,6.12,1.74,8.43-.47l.47-.58c2.49-2.58,2.42-6.69-.15-9.18-.06-.06-.13-.12-.2-.18-30.72-26.53-76.25-26.53-106.97,0-2.67,2.27-3,6.28-.73,8.95.12.14.25.28.38.41l.58.58h-.01Zm19.9,8.9c-2.7,2.23-3.08,6.24-.85,8.94.12.15.25.29.38.42l.47.47c2.3,2.24,5.9,2.44,8.43.47,7.19-5.37,15.95-8.21,24.93-8.08,8.95-.12,17.7,2.68,24.93,7.96,2.51,2.04,6.16,1.84,8.43-.47l.47-.47c2.47-2.42,2.52-6.38.1-8.85-.18-.18-.37-.35-.57-.51-9.53-7.46-21.26-11.56-33.36-11.69-12.12.1-23.86,4.24-33.36,11.77v.04Zm68.47,78.3h-23.38v-35.07c0-6.46-5.23-11.69-11.69-11.69s-11.69,5.23-11.69,11.69v35.07h-117.12c-12.89.05-23.33,10.49-23.38,23.38v46.76c.05,12.89,10.49,23.33,23.38,23.38h163.88c12.89-.05,23.33-10.49,23.38-23.38v-46.76c-.03-12.91-10.47-23.37-23.38-23.42v.04Zm-128.81,58.48h-23.38v-23.38h23.38v23.38Zm40.96,0h-23.38v-23.38h23.38v23.38Zm40.96,0h-23.38v-23.38h23.38v23.38Z" fill="#fff"/>
  <path d="m426.5,218.5c0,114.9-93.1,208-208,208S10.5,333.4,10.5,218.5,103.6,10.5,218.5,10.5s208,93.1,208,208Zm-416,0c0,114.9,93.1,208,208,208s208-93.1,208-208S333.4,10.5,218.5,10.5,10.5,103.6,10.5,218.5Z" fill="none" stroke="#fff" stroke-miterlimit="10" stroke-width="21"/>
</svg>
           <%--                 <%=Html.GetThemedSVG("activeGateways") %>--%>
                        </div>
                        <div class="dbCard_title">
                            <div class="dashboardCard__title__type">
                                <%: Html.TranslateTag("Active Gateways", "Active Gateways")%>
                            </div>
                            <div class="dashboardCard__title__number">
                                <%= ViewBag.gwActiveCount %> / <%= ViewBag.gwCount %>
                            </div>
                        </div>
                     
                    </a>

   <%--  ------------      Alerting Gateways--%>
                    <a class="top-location-card  db-home-1" href="/Events">
                          <div class=" dashboardCard__icon">
                            <%=Html.GetThemedSVG("db-white-alert") %>
                        </div>
                                <div class="dbCard_title">
                            <div class="dashboardCard__title__type">
                                <%: Html.TranslateTag("Alerting Gateways", "Alerting Gateways")%>
                            </div>
                            <div class="dashboardCard__title__number">
                                <%= ViewBag.gwAlertCount %>
                            </div>
                        </div>


             </a>
             
                    <%} %>
              
            </div>
            <%} %>



            <!-- End Form Title -->
            <div class="formtitle dfac my-4">
                <%
                    List<CSNet> CSNetList = iMonnit.Controllers.CSNetController.GetListOfNetworksUserCanSee(null);
                    CSNet selectedNet = CSNetList.FirstOrDefault();
                    if (CSNetList.Count > 1)
                    {%>
                <a class="col-1 col-md-2 col-xl-1" href="/Network/List" style="cursor: pointer;">
                    <div class="dfac" style="margin-bottom: 20px;">
                        <div class="dfjccac">
                            <%=Html.GetThemedSVG("network") %>
                        </div>
                        <div class="dfjccac media_desktop ms-2" style="padding-top: 3px; font-size: 15px; font-weight: 600;"><%: Html.TranslateTag("Overview/Index|Networks","Networks")%></div>
                    </div>
                </a>
                <div class="col-11 col-md-10 col-xl-11 networkList horizontalScroll" id="networkSelect" onchange="loadIndexDetails(this)">
                    <% 
                        long csnetID = MonnitSession.SensorListFilters.CSNetID;
                        foreach (CSNet Network in CSNetList)
                        {
                            if (csnetID == long.MinValue) csnetID = CSNetList.FirstOrDefault().CSNetID;  %>
                    <p class="networkList__network shadow-sm network_btn_white <%:csnetID == Network.CSNetID ? "network_btn_selected" : "" %>" value="<%:Network.CSNetID%>" <%:csnetID == Network.CSNetID ? "selected=selected" : "" %> title=" <%:Network.Name.Length > 0 ? System.Net.WebUtility.HtmlDecode(Network.Name) : Network.CSNetID.ToString() %>">
                        <span class="networkList__network__inner" onclick="selectNetwork(this)" data-networkname="<%:Network.Name%>" data-csnetid="<%:Network.CSNetID%>">
                            <%:Network.Name.Length > 0 ? System.Net.WebUtility.HtmlDecode(Network.Name) : Network.CSNetID.ToString() %>
                        </span>
                    </p>

                    <%if (MonnitSession.SensorListFilters.CSNetID == Network.CSNetID)
                            {
                                selectedNet = Network;
                            }
                        }%>
                </div>
                <% } %>
                <div id="newRefresh">
                    <a href="#" onclick="$('#settings').toggle(); return false;" class="icon icon-filter"></a>
                    <a href="/m/Home/" onclick="getMain(); return false;" class="icon icon-arrows-cw"></a>
                </div>
                <!-- End Main Refresh -->
            </div>

            <!-- Button trigger modal -->

            <!-- End Form Title -->
            <div id="indexdetails">
                <%if (selectedNet != null)
                    { %>
                <%Html.RenderPartial("_IndexDetails", selectedNet); %>
                <%} %>
            </div>
            <!--End of Main Dashboard Area-->
        </div>
        <!-- End of fullform -->
    </div>

    <%--<div class="row">
        Endpoint: <input type="text" id="endpoint" value="" />
        p256: <input type="text" id="p256dhKey" value="" />
        auth: <input type="text" id="authKey" value="" />
        <textarea id="pushMsg" rows="10" cols="50"></textarea>
        <button type="button" class="btn btn-primary" id="sendPushMsg">Send Push Message</button>
        <h3 id="sendPushMsgResult"></h3>
    </div>--%>

    <script>

        // #region PWA test code
        //$(function () {
        //    $('#sendPushMsg').click(function (e) {
        //        e.preventDefault();

        //        var msg = $('#pushMsg').val();
        //        var endpoint = $('#endpoint').val();
        //        var p256dhKey = $('#p256dhKey').val();
        //        var auth = $('#authKey').val();

        //        $.post('/Setup/SendPushMessage/', { msg: msg, endpoint: endpoint, p256dhKey: p256dhKey, auth: auth }, function (data) {
        //            $('#sendPushMsgResult').html(data);
        //        });
        //    });

        //    // Check if PWA App, if so refresh subscription
        //    if ('Notification' in window && platform.isStandalone) {
        //        console.log('push noti start');

        //        navigator.serviceWorker.ready.then(reg => {
        //            reg.pushManager.getSubscription().then(sub => {
        //                if (sub == undefined) {
        //                    //// Below to be implemented once all browsers support 'pushsubscriptionchange' event for PWA
        //                    //// only supported on Firefox as of 11 / 18 / 2022
        //                    //// https://developer.mozilla.org/en-US/docs/Web/API/ServiceWorkerGlobalScope/pushsubscriptionchange_event

        //                    //// Check indexedDB, or localstorage, for existing endpointUrl
        //                    //var oldEndpoint = '';

        //                    //// if entry found, call deletePushMessageSub
        //                    //if (oldEndpoint.length > 0) {
        //                    //    sub = new Object();
        //                    //    sub.endpoint = oldEndpoint;
        //                    //    deletePushMessageSub(sub);
        //                    //}

        //                } else {
        //                    // You have subscription, update the database with latest sub info
        //                    var subscriptionJson = JSON.stringify(sub);
        //                    console.log('sub found: ' + subscriptionJson);

        //                    if (window.Notification.permission == 'granted') {
        //                        updatePushMessageSub(subscriptionJson);
        //                    } else {
        //                        sub.unsubscribe();
        //                        deletePushMessageSub(subscriptionJson);
        //                    }
        //                }
        //            });
        //        });
        //    } else {
        //        console.log('push - not standalone');
        //    }
        //});

        //function updatePushMessageSub(subscriptionJson) {
        //    $.post('/Setup/EnablePushMessageSubscription/', { subJson: subscriptionJson }, function (data) {
        //        if (data == "Success") {
        //            console.log("Push notification subscribed");
        //        } else {
        //            console.log("AllowPushNotification - else - " + data);
        //        }
        //    });
        //}
        //function deletePushMessageSub(subscriptionJson) {
        //    $.post('/Setup/DeletePushMessageSubscription/', { subJson: subscriptionJson }, function (data) {
        //        if (data == "Success") {
        //            console.log("Push notification unsubscribed");
        //        } else {
        //            console.log("DeletePushMessageSubscription - else - " + data);
        //        }
        //    });
        //}
        //function urlB64ToUint8Array(base64String) {
        //    var padding = '='.repeat((4 - (base64String.length % 4)) % 4);
        //    var base64 = (base64String + padding).replace(/\-/g, '+').replace(/_/g, '/');
        //    var rawData = atob(base64);
        //    var outputArray = new Uint8Array(rawData.length);
        //    for (let i = 0; i < rawData.length; ++i) {
        //        outputArray[i] = rawData.charCodeAt(i);
        //    }
        //    return outputArray;
        //}
        //#endregion

        function loadIndexDetails(id) {

            $.get('/Overview/LoadIndexDetails', 'csnetid=' + id, function (data) {
                $('#indexdetails').html(data);
                console.log(data);
            });
        }

        function selectNetwork(n) {
            var name = $(n).data("networkname");
            var id = $(n).data("csnetid");

            removeBtnClass();
            $(n).parent().addClass("network_btn_selected");

            loadIndexDetails(id);
        }

        function removeBtnClass() {
            $('.networkList__network__inner').parent().removeClass("network_btn_selected");
        }

        function removeSystemHelp() {
            $('#alertBox').hide();
            if (systemHelpID.length > 0) {
                var s = systemHelpID;
                $.post("/Overview/ClearSystemHelp", { id: s }, function (data) {
                    if (data == "Success") {
                        removeAllCertificationAcknowledgement();
                    }
                    else {

                    }
                });
            } else {
                removeAllCertificationAcknowledgement();
            }
        }

        function removeAllCertificationAcknowledgement() {
            var custID = '<%=MonnitSession.CurrentCustomer.CustomerID%>';
            $.post("/Overview/ClearAllCertificationAcknowledgementForCustomer", { id: custID }, function (data) {
                if (data == "Success") {

                }
                else {

                }
            });
        }


    </script>
    <style>
        .network_btn_selected {
            background-color: var(--primary-color);
        }

        #svg_alerting, #svg_activeGateways, #svg_activeSensors {
            height: 60px;
            width: 60px;
            fill: #fff;
        }
    </style>
</asp:Content>
