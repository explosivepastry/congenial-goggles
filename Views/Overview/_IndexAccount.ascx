<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%-- 
    <!----------------------------------------------------------------------------------------------
                                    Top Grid Container 
                        1. SensorPie / 2. GatewayPie / 3. *Profile* / 4. Reports
    --------------------------------------------------------------------------------------------------> 
--%>

<!-- ----------------------------------
                Profile Card 3
    ------------------------------->
<% AccountSubscription subscription = MonnitSession.CurrentCustomer.Account.Subscriptions.Where(c=>c.AccountSubscriptionTypeID != AccountSubscriptionType.BasicID).OrderByDescending(c => c.ExpirationDate).FirstOrDefault();
    if (subscription == null)
        subscription = MonnitSession.CurrentCustomer.Account.CurrentSubscription;  %>

<%--<div class="userGrid  top_card" style="min-width: 351px; min-height: 160px; overflow-y: scroll;">--%>
    <div class="userheader-top headerCard"">
        <div class="l-corner-hug userx" style="width: 100px;"><%:Html.GetThemedSVG("user-settings") %> </div>
        <%--  <div class="userheader-icon">  <%:Html.GetThemedSVG("user") %></div>--%>
        <div class="user-name-home">
            <span class="homeName" title=" <%=MonnitSession.CurrentCustomer.Account.CompanyName %>">
                <%:Html.TranslateTag("Account Info") %>
            </span>

            <%if (MonnitSession.CustomerCan("Account_Edit"))
                {%>
            <span>
                <a title="Account Edit" href="/Settings/AccountEdit/<%:MonnitSession.CurrentCustomer.AccountID %>" data-short="<%=MonnitSession.CurrentCustomer.Account.AccountNumber %>">
                    <%=MonnitSession.CurrentCustomer.Account.AccountNumber %>
                </a>
            </span>
            <%} %>
        </div>

        <%if (MonnitSession.CustomerCan("Can_Access_Billing"))
            {%>
        <div class="premium-box d-flex">
            <a href="/Retail/PremiereCredit/<%:MonnitSession.CurrentCustomer.AccountID %>">
                <div class="user-premium" title="Premiere Credit">
                    <%:Html.GetThemedSVG("premium") %>
                    <span><%=subscription.AccountSubscriptionType.Name %></span>
                    <span></span>
                </div>
                <%if (subscription.AccountSubscriptionTypeID != AccountSubscriptionType.BasicID)
                    { %>
                <div class="user-premium">Exp: <%:MonnitSession.CurrentCustomer.Account.PremiumValidUntil.ToShortDateString() %></div>
                <%} %>
            </a>
        </div>
        <%} %>
    </div>

    <div class="user-updates" style="margin-top:12px;">
        <%--                <div class="user-name-home" style="align-items: flex-start; margin-left: 25px; gap: 8px;">
            <span class="homeName">
                <a title="Account Edit" href="/Settings/AccountEdit/<%:MonnitSession.CurrentCustomer.AccountID %>" data-short="<%=MonnitSession.CurrentCustomer.Account.AccountNumber %>">
                    <b><%:Html.TranslateTag("Account Number") %> </b>: <%=MonnitSession.CurrentCustomer.Account.AccountNumber %>
                </a>
            </span>
        </div>--%>

        <script type="text/javascript">
            var systemHelpID = '';
        </script>
        <%Customer customer = MonnitSession.CurrentCustomer;

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

            List<CertificationAcknowledgementModel> CertsExpiring = CertificateNotification.LoadByCustomerID(customer.CustomerID);
            Boolean profileNotComplete = false;
            int networkCount = CSNet.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Count;
            int gatewayCount = MonnitSession.OverviewHomeModel.TotalGateways;
            int sensorCount = MonnitSession.OverviewHomeModel.TotalSensors;

            if (customer.FirstName.IsEmpty()
                || customer.LastName.IsEmpty())
                profileNotComplete = true;

            if (systemHelps.Count == 0 && networkCount > 0 && gatewayCount > 0 && sensorCount > 0 && !profileNotComplete)
            {%>
        <div class="acc-name-profile-card">
            <div class="anpc"><%:customer.FirstName %> <%=customer.LastName %></div>
            <a href="/Settings/UserNotification/<%:customer.CustomerID %>" class="anpc"><%=customer.NotificationEmail %></a>
        </div>
        <%}%>


<%--Webhook Health--%>
        <%
            List<ExternalDataSubscription> Webhooks = ExternalDataSubscription.LoadAllByAccountID(customer.AccountID);
            ExternalDataSubscription DataEDS = Webhooks.Where(w => w.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.webhook).FirstOrDefault();
            ExternalDataSubscription NtfcEDS = Webhooks.Where(w => w.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.notification).FirstOrDefault();

            if (Webhooks.Count > 0 && MonnitSession.CustomerCan("Navigation_View_API") && MonnitSession.CurrentCustomer.Account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
            {
                if ((DataEDS != null && DataEDS.BrokenCount > 0) || (NtfcEDS != null && NtfcEDS.BrokenCount > 0))
                {
                    foreach (ExternalDataSubscription webhook in Webhooks)
                    {%>
        <div class="d-flex linex ">
            <div class="redDot"></div>
            <div class="icon-update"><%:Html.GetThemedSVG("notifications") %></div>
            <div style="width: 258px;">
                <a class="viewWebhookHealth" href="/Export/WebhookHealth/">
                    <%:Html.TranslateTag("Webhook error, click here for details")%>
                </a>
            </div>
        </div>
                  <%}
                }
            }%>


        <%
            if (systemHelps.Count > 0 || CertsExpiring.Count > 0)
            {
                if (systemHelps.Count == 0 && CertsExpiring.Count > 0)
                {
                    foreach (CertificationAcknowledgementModel certAckModel in CertsExpiring)
                    {%>
        <div class="d-flex linex ">
            <div class="redDot"></div>
            <div class="icon-update"><%:Html.GetThemedSVG("certificate") %></div>
            <div style="width: 258px;">
            <span style="cursor:pointer" title="Acknowledge Cert" class="clearSingleCertAck" id="clearCertNoti_<%=certAckModel.CertificateAcknowledgementID %>">
                <%:Html.TranslateTag("Acknowledge that the Calibration Certificate is Expiring Soon for Sensor ID: ") + certAckModel.SensorID%>
                </span>
            </div>
        </div>
        <%}
            }
            else
            { %>
        <div class="welcome-profile-card " id="hideWPC">
            <div class="welcome-animation-text">Welcome to your account</div>
            <div id="animatedText" class="animation-bounce  bounce-help" data-bs-toggle="modal" data-bs-target=".pageHelp">
                <%=Html.GetThemedSVG("help") %>
            </div>
        </div>



        <div class="modal fade pageHelp" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Welcome to your Account!</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>

                    <div class="modal-body">
                        <div class="row">
                            <div class="word-choice">
                                <div class="modal-welcome"><%=Html.TranslateTag("We're excited to have you on board. To begin using the system and unlock its full potential, please take a moment to complete the following fields in the categories below.")%></div>
                            </div>

                            <div class="word-def steps-index">
                                <%=Html.TranslateTag("To get started, let's begin by") %><strong> <%=Html.TranslateTag("completing your profile.")%></strong> <%=Html.TranslateTag("This will help us tailor your experience and provide you with relevant information.")%>  <strong><%=Html.TranslateTag(" Next, set up your gateway,")%></strong><%=Html.TranslateTag(" which will serve as the central hub for your devices. Once your gateway is up and running, you can easily")%> <strong><%=Html.TranslateTag("add sensors")%></strong> <%=Html.TranslateTag(" to monitor various aspects.")%>
                            </div>

                            <div class="word-def steps-index"><%=Html.GetThemedSVG("heart-fav") %> <%=Html.TranslateTag("      But that's not all! We want to make sure you make the most of our latest and newest features. By adding your favorite sensors, you can keep a close eye on the areas that matter to you the most. This will provide you with peace of mind, knowing that your investments are safe and secure.") %></div>
                            <div class="word-def steps-index"><%=Html.GetThemedSVG("circle-rules") %><%=Html.TranslateTag("     Finally, don't forget to customize your experience by setting up rules. By doing so, you'll receive notifications specifically tailored to your gateway and sensors. This way, you'll always stay informed about important events and updates.") %></div>
                            <div class="word-def steps-index thankyou-msg"><%=Html.TranslateTag("     Thank you for choosing our platform. We're here to support you every step of the way as you make the most of our innovative system. Let's get started and enjoy a seamless and secure experience!") %></div>
                        </div>
                    </div>
                    <div class="modal-footer" style="justify-content: center;">
                        <a class="btn btn-primary user-dets " href='/Setup/AccountWelcome'>Setup your account!</a>
                    </div>
                </div>
            </div>
        </div>

        <script type="text/javascript">
            /*    Animated Text*/
            const animatedText = document.getElementById("animatedText");
            let isBouncing = true;

            animatedText.addEventListener("click", () => {
                if (isBouncing) {
                    animatedText.classList.remove("animation-bounce");
                    isBouncing = false;
                } else {
                    animatedText.classList.add("animation-bounce");
                    isBouncing = true;
                }
            });

        </script>

        <%}

            foreach (SystemHelp s in systemHelps)
            {%>
        <script type="text/javascript">
            systemHelpID = '<%=s.SystemHelpID%>';
        </script>
        <%if (gatewayCount == 0 || sensorCount == 0 || networkCount == 0 || profileNotComplete || CertsExpiring.Count > 0)
            {
                if (networkCount == 0)
                { %>
        <a href="/Network/Create/<%:MonnitSession.CurrentCustomer.AccountID%>">
            <div class="d-flex ">
                <div class="redDot"></div>
                <div class="icon-update"><%:Html.GetThemedSVG("network") %></div>
                <div><%: Html.TranslateTag("Add a Network", "Add a Network")%></div>
            </div>
        </a>
        <%}
            if (profileNotComplete)
            { %>
        <a href="/Setup/NewCustomer/<%:customer.CustomerID %>">
            <div class="d-flex linex">
                <div class="redDot"></div>
                <div class="icon-update">
                    <%=Html.GetThemedSVG("user-settings") %>
                </div>
                Complete Your <span style="color: var(--primary-color); font-weight: bold;">Profile</span>
            </div>
        </a>
        <%}
                }
            }

            if (CertsExpiring.Count > 0)
            {%>
        <a href="/Network/SensorCertificateExpiring/<%=customer.AccountID %>">
            <div class="d-flex linex">
                <div class="redDot"></div>
                <div class="icon-update"><%:Html.GetThemedSVG("certificate") %></div>
                <div><%: Html.TranslateTag("Calibration Certificates Expiring Soon")%></div>
            </div>
        </a>
        <%}
            }%>
    </div>
<%--</div>--%>

<script>
    function handleScreenSize() {
        var screenWidth = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;
        var waM = document.getElementById("WA-Mobile");
        var waP = document.getElementById("hideWPC")

        if (waM && waP) {
            if (screenWidth <= 767) {
                waM.style.display = "flex";
                waP.style.display = "none";
            } else {
                waM.style.display = "none";
                waP.style.display = "flex";
            }
        }
    }

    $(function () {
        $('.clearSingleCertAck').click(function () {
            var certId = $(this).attr('id').split('_')[1];

            $.post("/Overview/ClearCertificationAcknowledgement/", { id: certId }, function (data) {
                if (data == "Success") {
                    toastBuilder(data)

                    $.get('/Overview/_IndexAccount', function (data) {
                        $('.userGrid').html(data)
                    })
                }
                else {
                    toastBuilder(data)
                }
            });
        });
    });

    window.addEventListener("load", function () {
        handleScreenSize();
    });

    window.addEventListener("resize", function () {
        handleScreenSize();
    });


</script>
<!-- End User Card -->
