<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.ExternalDataSubscription>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    NotificationWebhook
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <% 
        char[] WebhookClass = Model.eExternalDataSubscriptionClass.ToString().ToCharArray(); // either "webhook" or "notification"
        WebhookClass[0] = Char.ToUpperInvariant(WebhookClass[0]);                            // now "Webhook" or "Notification"
        string WebhookClassName = Model.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.webhook ? "DataWebhook" : "NotificationWebhook";
        Account account = Account.Load(Model.AccountID);
    %>

    <div class="container-fluid">
        <div class="col-12">
            <%Html.RenderPartial("_APILink"); %>
            <div class="text-end">
                <% if (Model != null && Model.ExternalDataSubscriptionID > 0 && Model.ExternalDataSubscriptionID > long.MinValue)
                    { %>
                <button type="button" value="<%: Html.TranslateTag("Export/NotificationWebhook|Configure","Configure")%>" class="btn btn-primary" onclick="configure('<%=Model.eExternalDataSubscriptionType %>')">
                    <%: Html.TranslateTag("Export/NotificationWebhook|Configure","Configure")%>
                </button>
                <% }
                else
                { %>
                <button type="button" value="<%: Html.TranslateTag("Export/NotificationWebhook|Create","Create")%>" class="btn btn-primary" onclick="chooseType()">
                    <%: Html.TranslateTag("Export/NotificationWebhook|Create","Create")%>
                </button>
                <%}%>
            </div>

            <div id="chooseType" style="display: none;" >
                <% Html.RenderPartial("_ChooseWebhookType"); %>
            </div>

            <%Html.RenderPartial("_NotificationWebhookInfo", Model); %>
             <% if (Model != null && Model.ExternalDataSubscriptionID > 0 && Model.ExternalDataSubscriptionID > long.MinValue)
                 { Html.RenderPartial("_WebhookHistory", Model); }%> 
        </div>
    </div>

    <script type="text/javascript">

        function premiumRequired() {
            $('#messageModal .modal-header').text("<%: Html.TranslateTag("Alert","Alert")%>");
            $('#messageModal .modal-body .message').text("<%: Html.TranslateTag("Premium subscription required.","Premium subscription required.")%>");
            $('#messageModal .modal-body .error').text("<%: Html.TranslateTag("Premium subscription required.","Premium subscription required.")%>");
            $('#messageModal .modal-body .alert').show();
            $('#messageModal').modal('show');
        }

        function chooseType() {
        <% if (!MonnitSession.CurrentCustomer.Account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
        { %>
            premiumRequired();
        <% }
        else { %>
            $('#chooseType').toggle();
        <% } %>
        }

        function configure(type) {
        <% if (!MonnitSession.CurrentCustomer.Account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
        { %>
            premiumRequired();
        <% }
        else { %>     
            switch (type) {
                case "webhook":
                    window.location.href = "/Export/ConfigureWebhook/<%= WebhookClass %>";
                    break;
                case "mqtt":
                    window.location.href = "/Export/ConfigureMqtt/<%= WebhookClass %>";
                    break;
                case "mqttcert":
                    window.location.href = "/Export/ConfigureMQTTwithCertificate/<%= WebhookClass %>";
                    break;
                case "watson":
                    window.location.href = "/Export/ConfigureWatson/<%= WebhookClass %>";
                    break;
                case "aws":
                    window.location.href = "/Export/ConfigureAWS/<%= WebhookClass %>";
                    break;
                case "google":
                    window.location.href = "/Export/ConfigureGoogle/<%= WebhookClass %>";
                    break;
                case "azure":
                    window.location.href = "/Export/ConfigureAzure/<%= WebhookClass %>";
                    break;
                case "azuremqtt":
                    window.location.href = "/Export/ConfigureAzureMQTT/<%= WebhookClass %>";
                    break;
                case "omf_pi":
                    window.location.href = "/Export/ConfigurePI/<%= WebhookClass %>";
                    break;
            }
        <% } %>  
        };
    </script>

</asp:Content>
