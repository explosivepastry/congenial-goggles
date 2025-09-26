<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%=""%>

<% DateTime killDate = MonnitSession.EnterpriseKillDate();

    string message = string.Empty;
    string sensorCount = string.Empty;
    string accountLink = string.Empty;
    string brandSpecificAlertColor = MonnitSession.CurrentStyle("AlertingSensors");

    if (MonnitSession.CurrentCustomer != null)
    {

        if (MonnitSession.IsServerHosted)//hosted
        {
            //accountLink = "/Settings/AccountEdit/" + MonnitSession.CurrentCustomer.AccountID;
            accountLink = "/Retail/PremiereCredit/" + MonnitSession.CurrentCustomer.AccountID + "/?returnAfterAdd=true";
            if (MonnitSession.CurrentSubscription.AccountSubscriptionTypeID == AccountSubscriptionType.BasicID)
            {
                message = "Premiere features disabled, to upgrade your account visit: ";
            }
            else if (MonnitSession.CurrentSubscription.AccountSubscriptionTypeID == AccountSubscriptionType.TrialID && MonnitSession.CurrentCustomer.Account.CreateDate < DateTime.UtcNow.AddDays(-1))
            {
                message = string.Format("Your trial of the Premiere features will expire on {0}, to upgrade your account visit: ", MonnitSession.CurrentSubscription.ExpirationDate.OVToLocalDateTimeShort());
            }
            else if (MonnitSession.CurrentSubscription.ExpirationDate < DateTime.UtcNow.AddDays(30))
            {
                DateTime FurthestDate = MonnitSession.CurrentSubscription.ExpirationDate;
                //Check for another subscription...
                foreach (AccountSubscription sub in MonnitSession.CurrentCustomer.Account.Subscriptions)
                {
                    //ignore basic or trial subscriptions
                    if (sub.AccountSubscriptionTypeID == AccountSubscriptionType.BasicID || sub.AccountSubscriptionTypeID == AccountSubscriptionType.TrialID)
                        continue;

                    if (sub.ExpirationDate > FurthestDate)
                        FurthestDate = sub.ExpirationDate;
                }

                if (FurthestDate < DateTime.UtcNow.AddDays(30))
                    message = string.Format("Premiere features will expire on {0}, to extend your subscription visit: ", FurthestDate.OVToLocalDateTimeShort());
            }
        }
        else//Enterprise
        {
            eProgramLevel level = MonnitSession.ProgramLevel();
            int count = ThemeController.SensorCount();
            if (count > level.ToInt())
                sensorCount = "Only " + level.ToInt() + " sensors allowed for this installation.";

            if (level == eProgramLevel.EnterpriseTrial)
            {
                message = "This is a trial installation";
            }


            if (level != eProgramLevel.EnterpriseTrial && killDate.AddDays(-15) < DateTime.UtcNow)
            {
                message = "This installation will expire soon, contact support to update subscription.";
            }

            if (killDate < DateTime.UtcNow)
            {
                message = "This installation is expired, contact support to update subscription.";
            }
        }

    }%>

<div>
    <div>
        <% if (!string.IsNullOrEmpty(message))
            { %>
        <div id="expirationMessage" class="banner-wrapper-ab">
            <div class="warning-wrapper-ab"><%=Html.GetThemedSVG("app13")%></div>
            <div class="warning-text-ab"><%= message %> <%= sensorCount %> </div>
            <% if (!string.IsNullOrEmpty(accountLink))
                { %>
            <a class="btn buy-button-ab" href="<%= accountLink %>">
                <%= Html.TranslateTag("Click Here", "Click Here") %>
            </a>
            <% }%>
        </div>
        <% }%>
    </div>
    <div>
        <%
            if (MonnitSession.CurrentCustomer != null
                && !MonnitSession.IsServerHosted)
            {
                Account acct = Account.Load(MonnitSession.CurrentCustomer.AccountID);
                if (acct != null && acct.CurrentSubscription != null && Sensor.LoadByAccountID(acct.AccountID).Count >= acct.CurrentSubscription.AccountSubscriptionType.AllowedSensors)
                {
        %>

        <div class="banner-wrapper-ab">
            <div class="warning-wrapper-ab"><%=Html.GetThemedSVG("app13")%></div>
            <div class="warning-text-ab"><%:Html.TranslateTag("Sensor limit has been reached for your account, to update your account.") %></div>
            <a class="btn buy-button-ab" href="<%:accountLink %>"><%:Html.TranslateTag("Click Here") %></a>
        </div>

        <%      }
            } %>
    </div>
    <div>
        <%  if (MonnitSession.CurrentCustomer != null && MonnitSession.CurrentCustomer.Account.HideData)
            { %>
        <div class="banner-wrapper-ab">
            <div class="warning-wrapper-ab"><%= Html.GetThemedSVG("app13") %></div>
            <div class="warning-text-ab"><%:Html.TranslateTag("You are out of HX Credits! Buy more to see your data.") %></div>
            <a class="btn buy-button-ab" href="/Retail/MessageCredit/<%=MonnitSession.CurrentCustomer.AccountID%>"><%:Html.TranslateTag("Buy Credits") %></a>
        </div>
        <% } %>
    </div>
</div>


<style>
    :root {
        --brandAlertColor: <%= brandSpecificAlertColor %>;
    }

    .warning-wrapper-ab svg {
        fill: var(--brandAlertColor);
        width: 3.5rem;
        height: 3rem;
    }

    .warning-text-ab {
        font-size: 1rem;
        font-weight: 600;
    }

    .buy-button-ab {
        background: var(--brandAlertColor);
        transition: filter 0.3s ease-in-out;
        color: white;
    }

        .buy-button-ab:hover {
            filter: brightness(1.2);
            color: white;
        }

    .banner-wrapper-ab {
        display: flex;
        width: 100vw;
        justify-content: center;
        gap: 4rem;
        align-items: baseline;
        border-bottom: solid 1px lightgrey;
        padding: 0.25rem;
        background:white;
    }

    @media screen and (max-width:720px) {

        .banner-wrapper-ab {
            display: flex;
            flex-direction: column;
            gap: .25rem;
            align-items: center;
        }
    }
</style>

