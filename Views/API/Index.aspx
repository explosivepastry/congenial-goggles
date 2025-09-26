<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    API
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <div class="rule_container" style="margin-top:2rem">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <%: Html.TranslateTag("API","API")%>
                </div>
            </div>

            <div class="rule-sets ">
            <a href="/API/RestAPI/" title=" <%: Html.TranslateTag("API/Index|RestAPI","RestAPI")%>" class="btn-lg  newActionBtn" style="position: relative;">
                <span class="api-icon" style="position: absolute; left: 25px;"><%=Html.GetThemedSVG("code") %></span>
                <%: Html.TranslateTag("API/Index|Rest API","Rest API")%>
            </a>
                </div>

            <div class="rule-sets ">
            <a href="/API/ApiKeys/" title=" <%: Html.TranslateTag("API/Index|APIKeys","APIKeys")%>" class="btn-lg  newActionBtn" style="position: relative;">
                <span class="api-icon" style="position: absolute; left: 25px;"><%=Html.GetThemedSVG("lock") %></span>
                <%: Html.TranslateTag("API/Index|API Keys","API Keys")%>
            </a>
                </div>

            <div class="rule-sets ">
            <a href="/Export/DataWebhook/" title=" <%: Html.TranslateTag("API/Index|DataWebhook","DataWebhook")%>" class="btn-lg  newActionBtn" style="position: relative;">
                <span class="api-icon" style="position: absolute; left: 25px;"><%=Html.GetThemedSVG("api") %></span>
                 <%: Html.TranslateTag("API/Index|Data Webhook","Data Webhook")%>
            </a>
                </div>

            <div class="rule-sets ">
            <a href="/Export/NotificationWebhook/" title="<%: Html.TranslateTag("API/Index|NotificationWebhook","Notification Webhook")%>" class="btn-lg  newActionBtn" style="position: relative;">
                <span class="api-icon" style="position: absolute; left: 25px;"><%=Html.GetThemedSVG("rules") %></span>
                <%: Html.TranslateTag("API/Index|Rule Webhook","Rule Webhook")%>
            </a>
                </div>

            	<% if (MonnitSession.CurrentCustomer != null && MonnitSession.CurrentCustomer.Account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
                    { %>
              <div class="rule-sets ">
             <a href="/Export/WebhookHealth/" title="<%: Html.TranslateTag("API/Index|WebhookHealth","WebhookHealth")%>" class="btn-lg  newActionBtn" style="position: relative;">
                <span class="api-icon" style="position: absolute; left: 25px;"><%=Html.GetThemedSVG("notifications") %></span>
                <%: Html.TranslateTag("API/Index|Webhook Health","Webhook Health")%>
            </a>
                  </div>
            <% } %>
        </div>
    </div>

    <style type="text/css">
        svg {
            height: 100px;
      /*      fill: #FFF;*/
        }

        #svg_api  path {
          /*  fill: white!important;*/
        }

        .x_panel .svg_icon {
			height: 25px;
			width: 25px;
		}
    </style>

</asp:Content>
