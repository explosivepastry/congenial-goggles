<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.ExternalDataSubscription>" %>
<% 
    char[] WebhookClass = Model.eExternalDataSubscriptionClass.ToString().ToCharArray(); // either "webhook" or "notification"
        WebhookClass[0] = Char.ToUpperInvariant(WebhookClass[0]);                            // now "Webhook" or "Notification"
        string WebhookClassName = Model.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.webhook ? "DataWebhook" : "NotificationWebhook";
    %>

    <div class="container-fluid">
        <div class="col-12">
            <div class="x_panel shadow-sm rounded mt-md-2">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <%:Html.TranslateTag("Export/_ChooseWebhookType|Choose Webhook Type","Choose Webhook Type")%>
                    </div>
                </div>

                <div class="x_content">
                    <div class="col-12">
                        <%--<label><%:Html.TranslateTag("Export/_ChooseWebhookType|Choose Webhook Type","Choose Webhook Type")%></label>--%>
                        <select class="form-select" id="newWebhook" style="width: 250px;">
                            <option><%:Html.TranslateTag("Export/_ChooseWebhookType|Choose Webhook Type")%></option>
                            <option value="webhook"><%: Html.TranslateTag("Export/_ChooseWebhookType|HTTP Webhook")%></option>
                            <option value="mqtt"><%: Html.TranslateTag("Export/_ChooseWebhookType|MQTT Webhook")%></option>
                            <option value="mqttcert"><%: Html.TranslateTag("Export/_ChooseWebhookType|MQTT Webhook w/ Certificate")%></option>
                            <option value="aws"><%: Html.TranslateTag("Export/_ChooseWebhookType|Amazon Gateway")%></option>
                            <option value="azure"><%: Html.TranslateTag("Export/_ChooseWebhookType|Azure IOT")%></option>
                            <option value="azuremqtt"><%: Html.TranslateTag("Export/_ChooseWebhookType|Azure IOT Hub MQTT")%></option>
                            <option value="watson"><%: Html.TranslateTag("Export/_ChooseWebhookType|Watson IOT")%></option>
                            <option value="pi_omf"><%: Html.TranslateTag("Export/_ChooseWebhookType|PI Integration")%></option>
                        </select>
                    </div>
                    <div class="clearfix"></div>
                    <div style="clear: both;"></div>
                </div>
            </div>
        </div>
    </div>



    <script type="text/javascript">

        $(function () {
            $("#newWebhook").change(function () {
               
                var eventType = $("#newWebhook").val()
                switch (eventType) {
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
                    case "pi_omf":
                        window.location.href = "/Export/ConfigurePI/<%= WebhookClass %>";
                        break;
                }

            });

        });

    </script>