<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.ExternalDataSubscription>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ConfigureAWS
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% 
        char[] WebhookClass = Model.eExternalDataSubscriptionClass.ToString().ToCharArray(); // either "webhook" or "notification"
        WebhookClass[0] = Char.ToUpperInvariant(WebhookClass[0]);
        string WebhookClassName = Model.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.webhook ? "DataWebhook" : "NotificationWebhook";
    %>

    <%
        List<ExternalDataSubscriptionProperty> propertyList = ExternalDataSubscriptionProperty.LoadByExternalDataSubscriptionID(Model.ExternalDataSubscriptionID);
        string AccessKey = "";
        string SecretKey = "";
        string APIKey = "";
        string method = "";

        if (propertyList.Count > 0)
        {
            ExternalDataSubscriptionProperty accessProp = propertyList.Where(m => m.Name == "accessKey").ToList().FirstOrDefault();
            ExternalDataSubscriptionProperty secretProp = propertyList.Where(m => m.Name == "secretKey").ToList().FirstOrDefault();
            ExternalDataSubscriptionProperty apiKeyProp = propertyList.Where(m => m.Name == "apiKey").ToList().FirstOrDefault();
            ExternalDataSubscriptionProperty methodProp = propertyList.Where(m => m.Name == "callMethod").ToList().FirstOrDefault();

            if (accessProp != null)
                AccessKey = accessProp.StringValue;
            if (secretProp != null)
                SecretKey = secretProp.StringValue;
            if (apiKeyProp != null)
                APIKey = apiKeyProp.StringValue;
            if (methodProp != null)
                method = methodProp.StringValue;
        }
    %>

    <div class="container-fluid">
        <div class="col-12">
            <%Html.RenderPartial("_APILink"); %>
        </div>

        <div class="col-12">
            <div class="x_panel shadow-sm rounded mt-2">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <%:Html.TranslateTag("Export/ConfigureAWS|Configure Amazon AWS","Configure Amazon AWS")%>
                    </div>
                </div>

                <div class="x_content">
                    <div class="col-12">
                        <div class="col-lg-10 col-12">
                            <h2><%:Html.TranslateTag("Export/ConfigureAWS|How Data is passed to Amazon API Gateway","How Data is passed to Amazon API Gateway")%></h2>
                            <span style="font-size: 1.1em; align-content: center;"><%:Html.TranslateTag("Export/ConfigureAWS|This pushes data to your Amazon API Gateway endpoint when data is received at the server. Create an Amazon API Gateway in your AWS console to represent this connection. The \"GET\" method is not compatible with this data push system. AWS Signature Version 4 is required for authentication.")%><a target="_blank" href="https://docs.aws.amazon.com/apigateway/latest/developerguide/welcome.html"> <b style="text-decoration: underline; color: #4a71cc;"><%:Html.TranslateTag("More Info","More Info")%></b></a> <%:Html.TranslateTag("All sensor and gateway message data will be pushed to this endpoint. Data is sent as JSON. Amazon Web Services, AWS, and  API Gateway are trademarks of Amazon.com, Inc. or its affiliates in the United States and/or other countries.")%> </span>
                        </div>
                        <div style="clear: both;"></div>
                        <hr />
                    </div>

                    <%: Html.ValidationSummary() %>
                    <form autocomplete="off" id="prefForm" action="/Export/ConfigureAWS/<%= WebhookClass %>" method="post" class="form-horizontal form-label-left">
                        <div class="form-group">
                            <%: Html.ValidationMessage("main") %>
                            <%: Html.ValidationMessageFor(m => m.ConnectionInfo1) %>

                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group" style="display: none">
                            <div class="bold col-lg-3 col-12">
                                <%: Html.TranslateTag("Export/ConfigureAWS|Webhook Class","Webhook Class")%>
                            </div>
                            <div class="col-lg-9 col-12 lgbox">
                                <input type="text" class="form-control" name="eEDSClass" value="<%=Model.eExternalDataSubscriptionClass.ToInt() %>" style="width: 250px;" />
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-2 col-12">
                                <%: Html.TranslateTag("Export/ConfigureAWS|Invoke URL","Invoke URL")%>
                            </div>
                            <div class="col-lg-8 col-12 lgbox">
                                <input type="text" name="ConnectionInfo1" class="form-control" value="<%=Model.ConnectionInfo1 %>" style="width: 100%;" placeholder="https://{api-id}.execute-api.{region}.amazonaws.com/{stage_name}/" />
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-2 col-12">
                                <%: Html.TranslateTag("Export/ConfigureAWS|Http Method","Http Method")%>
                            </div>
                            <div class="col-lg-8 col-12">
                                <select name="callMethod" class="form-select" id="callMethod" style="width: 250px;">
                                    <option value="<%:Html.TranslateTag("Export/ConfigureAWS|POST","POST")%>" <%:  method == "POST" ?"selected":""%>><%:Html.TranslateTag("Export/ConfigureAWS|POST","POST")%></option>
                                    <option value="<%:Html.TranslateTag("Export/ConfigureAWS|PUT","PUT")%>" <%:  method == "PUT" ?"selected":""%>><%:Html.TranslateTag("Export/ConfigureAWS|PUT","PUT")%></option>
                                    <option value="<%:Html.TranslateTag("Export/ConfigureAWS|DELETE","DELETE")%>" <%:  method == "DELETE" ?"selected":""%>><%:Html.TranslateTag("Export/ConfigureAWS|DELETE","DELETE")%></option>
                                    <option value="<%:Html.TranslateTag("Export/ConfigureAWS|PATCH","PATCH")%>" <%:  method == "PATCH" ?"selected":""%>><%:Html.TranslateTag("Export/ConfigureAWS|PATCH","PATCH")%></option>
                                    <option value="<%:Html.TranslateTag("Export/ConfigureAWS|HEAD","HEAD")%>" <%:  method == "HEAD" ?"selected":""%>><%:Html.TranslateTag("Export/ConfigureAWS|HEAD","HEAD")%></option>
                                    <option value="<%:Html.TranslateTag("Export/ConfigureAWS|OPTIONS","OPTIONS")%>" <%:  method == "OPTIONS" ?"selected":""%>><%:Html.TranslateTag("Export/ConfigureAWS|OPTIONS","OPTIONS")%></option>
                                </select>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-2 col-12">
                                <%:Html.TranslateTag("Export/ConfigureAWS|Amazon IAM Access Key","Amazon IAM Access Key")%>
                            </div>
                            <div class="col-lg-8 col-12 lgbox">
                                <input type="text" class="form-control" value="<%=MonnitSession.UseEncryption ? AccessKey.Decrypt() : AccessKey %>" style="width: 250px;" required name="AccessKey" id="AccessKey" />
                                <%: Html.ValidationMessage("AccessKey") %>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-2 col-12">
                                <%:Html.TranslateTag("Export/ConfigureAWS|Amazon IAM Secret Key","Amazon IAM Secret Key")%>
                            </div>
                            <div class="col-lg-8 col-12 lgbox">
                                <input type="text" autocomplete="off" class="form-control" value="<%=MonnitSession.UseEncryption ? SecretKey.Decrypt() : SecretKey %>" style="width: 250px;" required name="SecretKey" id="SecretKey" />
                                <%: Html.ValidationMessage("SecretKey") %>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-2 col-12">
                                <%:Html.TranslateTag("Export/ConfigureAWS|Amazon API Key","Amazon API Key")%>
                            </div>

                            <div class="col-lg-8 col-12 lgbox">
                                <input type="text" class="form-control" value="<%=APIKey %>" style="width: 250px;" required name="APIKey" id="APIKey" />
                                <%: Html.ValidationMessage("APIKey") %>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group" <%=Model.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.notification ? "style=\"display: none;\"" : "" %>>
                            <div class="bold col-lg-2 col-12">
                                <%:Html.TranslateTag("Export/ConfigureAWS|Send gateway message","Send gateway message")%>
                            </div>

                            <div class="col-lg-8 col-12 lgbox">
                                <select class="form-select" name="sendWithoutDataMessage" id="sendWithoutDataMessage" style="width: 250px;">
                                    <option value="true" <%:  Model.SendWithoutDataMessage == true?"selected":""%>><%:Html.TranslateTag("Export/ConfigureAWS|Always","Always")%></option>
                                    <option value="false" <%:  Model.SendWithoutDataMessage == false?"selected":""%>><%:Html.TranslateTag("Export/ConfigureAWS|Only with sensor messages","Only with sensor messages")%></option>
                                </select>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-2  col-12">
                                <%: Html.TranslateTag("Export/ConfigureMQTT|Send Raw Data","Send Raw Data")%>
                            </div>

                            <div class="col-lg-9 col-12">
                                <div class="form-check form-switch d-flex align-items-center ps-0">
                                    <label class="form-check-label"><%: Html.TranslateTag("No","No")%></label>
                                    <input class="form-check-input my-0 y-0 mx-2" type="checkbox" id="sendRawData" <%:Model.SendRawData ? "checked='checked'" : ""%>>
                                    <label class="form-check-label"><%: Html.TranslateTag("Yes","Yes")%></label>
                                    <input type="hidden" name="sendRawData" id="sendRawDataHidden" value="<%:Model.SendRawData %>">
                                </div>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-2 col-12">
                                <%if (Model != null && Model.ExternalDataSubscriptionID > 0)
                                    { %>
                                <div class="BrokenCount" style="float: left; margin: 10px 5px 10px 5px; color: #2D4780;">
                                    <%if (Model.DoSend)
                                        {%>
                                    <%:Html.TranslateTag("Export/ConfigureAWS|Sending: Enabled","Sending: Enabled")%>
                                    <%}%><%else
                                             {%>
                                    <%:Html.TranslateTag("Export/ConfigureAWS|Sending: Disabled","Sending: Disabled")%>
                                    <%}%>

                                    <%if (Model.DoRetry)
                                        {%><br />
                                    <%:Html.TranslateTag("Export/ConfigureAWS|Retries: Enabled","Retries: Enabled")%>
                                    <%}%>
                                    <%else
                                        {%><br />
                                    <%:Html.TranslateTag("Export/ConfigureAWS|Retries: Disabled","Retries: Disabled")%>
                                    <%}%>
                                </div>
                                <% } %>
                            </div>
                            <div class="col-12 text-end">
                                <input type="submit" value="<%:Html.TranslateTag("Export/ConfigureAWS|Save","Save")%>" class="btn btn-primary" />
                                <% if (Model != null && Model.ExternalDataSubscriptionID > 0)
                                    { %>
                                <input type="button" onclick="checkDelete()" value="<%:Html.TranslateTag("Delete", "Delete")%>" class="btn btn-secondary" />
                                <%} %>
                                <input type="button" id="resetBrokenSend" onclick="resetBroken();" value="<%:Html.TranslateTag("Export/ConfigureAWS|Reset Broken Send","Reset Broken Send")%>" class="btn btn-secondary" <%= Model.DoSend ? "disabled" : "" %> />
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <%--<input type="hidden" value="<%=Model.ExternalDataSubscriptionID %>" name="externalDataSubscriptionID" />--%>
                    </form>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        var areyousure = "<%: Html.TranslateTag("Export/ConfigureAWS|Are you sure you want to delete this?","Are you sure you want to delete this ?")%>";
        var resetBrokenString = "<%: Html.TranslateTag("Export/ConfigureAWS|Reset broken count to 0 ?","Reset broken count to 0 ?")%>";

        function checkDelete() {
            if (confirm(areyousure)) {
                $.get('/Export/WebHookDelete/<%: Model.ExternalDataSubscriptionID %>', function (data) {

                    if (data == 'Success') {
                        showSimpleMessageModal("<%=Html.TranslateTag("Success")%>");
                        window.location.href = '/Export/<%=WebhookClassName%>/';
                    } else {
                        console.log(data);
                        let values = {};
                    <%--values.redirect = '/Ack/<%:Model.NotificationRecordedID%>/<%:Model.NotificationGUID%>';--%>
                        showSimpleMessageModal("<%=Html.TranslateTag("Oops! That did not work, please refresh your page. If this error continues, contact support.")%>");
                    }
                });
            }
        }

        $('#sendRawData').change(function () {
            let sendRawData = $(this).prop('checked')
            $('#sendRawDataHidden').val(sendRawData)
        });

        function resetBroken() {
            if (confirm(resetBrokenString)) {
                $.get('/Export/ResetBrokenExternalDataSubscription/<%:Model.ExternalDataSubscriptionID%>', function (data) {
                    if (data == 'Success') {
                        showSimpleMessageModal("<%=Html.TranslateTag("Success")%>");
                        $('#resetBrokenSend').prop('disabled', true);
                        //window.location.href = window.location.href;
                        //window.location.reload();
                        //$('#resetBrokenSend').hide();
                    }
                    else {
                        console.log(data);
                        let values = {};
                    <%--values.redirect = '/Ack/<%:Model.NotificationRecordedID%>/<%:Model.NotificationGUID%>';--%>
                        showSimpleMessageModal("<%=Html.TranslateTag("Oops! That did not work, please refresh your page. If this error continues, contact support.")%>");
                    }
                });
            }
        }

    </script>

</asp:Content>
