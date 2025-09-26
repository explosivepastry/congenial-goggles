<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<ExternalDataSubscription>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ConfigureMQTT
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% 
        string WebhookClass = Model.eExternalDataSubscriptionClass.ToString();
        string WebhookClassName = Model.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.webhook ? "DataWebhook" : "NotificationWebhook";
    %>
    <div class="container-fluid">
        <div class="col-12">
            <%Html.RenderPartial("_APILink", Model); %>
        </div>

        <div class="col-12">
            <div class="x_panel shadow-sm rounded mt-2">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <%:Html.TranslateTag("Configure MQTT", "Configure MQTT")%>
                    </div>
                </div>

                <div class="x_content">
                    <div class="col-12">
                        <div class="col-lg-10 col-12">
                            <h2><%:Html.TranslateTag("Export/ConfigureMQTTwithCerticate|How MQTT Webhooks Pass Data to Your Application","How MQTT Webhooks Pass Data to Your Application")%></h2>
                            <span style="font-size: 1.1em; align-content: center;"><%:Html.TranslateTag("Export/ConfigureMQTTwithCerticate|A webhook sends data to your application when data is received at the server. You can configure the parameters used to route the request. Data is compiled as a JSON body and sent via MQTT publish","A webhook sends data to your application when data is received at the server. You can configure the parameters used to route the request. Data is compiled as a JSON body and sent via MQTT publish")%></span>
                        </div>
                        <div style="clear: both;"></div>
                        <hr />
                    </div>

                    <%: Html.ValidationSummary() %>
                    <form id="prefForm" action="/Export/ConfigureMQTTwithCertificate/<%= WebhookClass %>" method="post" enctype="multipart/form-data" class="form-horizontal form-label-left">
                        <div class="form-group">
                            <%: Html.ValidationMessage("main") %>
                            <%: Html.ValidationMessageFor(m => m.ConnectionInfo1) %>
                            <div class="clearfix"></div>
                        </div>

                        <% 
                            string host = "";
                            string port = "";
                            string clientID = "";
                            string topic = "";
                            List<ExternalDataSubscriptionProperty> propertyList = ExternalDataSubscriptionProperty.LoadByExternalDataSubscriptionID(Model.ExternalDataSubscriptionID);

                            if (propertyList.Count > 0)
                            {
                                ExternalDataSubscriptionProperty clientIDProp = propertyList.Where(m => m.Name == "clientID").ToList().FirstOrDefault();
                                ExternalDataSubscriptionProperty topicProp = propertyList.Where(m => m.Name == "topic").ToList().FirstOrDefault();

                                if (clientIDProp != null)
                                    clientID = clientIDProp.StringValue;
                                if (topicProp != null)
                                    topic = topicProp.StringValue;


                                host = Model.ConnectionInfo1;
                                port = Model.ConnectionInfo2;

                            }
                        %>

                        <div class="form-group" style="display: none;">
                            <div class="bold col-lg-3 col-12">
                                <%: Html.TranslateTag("Webhook Class","Webhook Class")%>
                            </div>
                            <div class="col-lg-9 col-12 lgbox">
                                <input class="" type="hidden" name="useSSL" id="useSSL" value="<%: Html.TranslateTag("True","True")%>">
                                <input type="text" class="form-control" name="eEDSClass" value="<%=Model.eExternalDataSubscriptionClass.ToInt() %>" style="width: 250px;" />
                            </div>

                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-2  col-12">
                                <%: Html.TranslateTag("Host","Host")%> <span style="color: red;">*</span>
                            </div>

                            <div class="col-lg-8 col-12">
                                <input type="text" class="form-control" style="width: 450px;" value="<%=host %>" required name="Host" id="host" />
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-2  col-12">
                                <%: Html.TranslateTag("Port","Port")%> <span style="color: red;">*</span>
                            </div>
                            <div class="col-lg-9 col-12">

                                <input type="number" class="form-control" style="width: 100px;" value="<%=port %>" required name="Port" id="port" />
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-2  col-12">
                                <%: Html.TranslateTag("Client ID","ClientID")%> <span style="color: red;">*</span>
                            </div>

                            <div class="col-lg-8 col-12 lgbox text-break">
                                <input type="text" class="form-control" style="width: 450px;" value="<%=clientID %>" required name="ClientID" id="clientID" />
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-2  col-12">
                                <%: Html.TranslateTag("Topic","Topic")%> <span style="color: red;">*</span>
                            </div>

                            <div class="col-lg-9 col-12">
                                <input type="text" class="form-control" style="width: 450px;" value="<%=topic %>" required name="Topic" id="topic" />
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-2  col-12">
                                <%: Html.TranslateTag("X509 Certificate","X509 Certificate")%> <span style="color: red;">*</span>
                            </div>

                            <div class="col-lg-9 col-12">
                                <input type="file" class="form-control" style="width: 250px;" onchange="checkCertificateType(this);" required name="Certificate" />
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-3 col-12">
                                <%: Html.TranslateTag("Send Raw Data","Send Raw Data")%>
                            </div>

                            <div class="col-lg-9 col-12">
                                <div class="form-check form-switch d-flex align-items-center ps-0">
                                    <label class="form-check-label"><%: Html.TranslateTag("No","No")%></label>
                                    <input class="form-check-input my-0 y-0 mx-2" type="checkbox" id="SendRawData" <%:Model.SendRawData ? "checked='checked'" : ""%>>
                                    <label class="form-check-label"><%: Html.TranslateTag("Yes","Yes")%></label>
                                    <input type="hidden" name="sendRawData" id="sendRawDataHidden" value="<%:Model.SendRawData %>">
                                </div>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-3 col-12">
                                <div id="errMessages" style="float: left; margin: 10px 5px 10px 5px; color: #2D4780;">
                                </div>

                            </div>
                            <div class="col-12 text-end">
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-3 col-12">
                                <%if (Model != null && Model.ExternalDataSubscriptionID > 0)
                                    { %>
                                <div class="BrokenCount" style="float: left; margin: 10px 5px 10px 5px; color: #2D4780;">
                                    <%if (Model.DoSend)
                                        {%>
                                    <%: Html.TranslateTag("Export/ConfigureMQTTwithCerticate|Sending: Enabled","Sending: Enabled")%>
                                    <%}%>
                                    <%else
                                        {%>
                                    <%: Html.TranslateTag("Export/ConfigureMQTTwithCerticate|Sending: Disabled","Sending: Disabled")%>
                                    <%}%>
                                    <%if (Model.DoRetry)
                                        {%><br />
                                    <%: Html.TranslateTag("Export/ConfigureMQTTwithCerticate|Retries: Enabled","Retries: Enabled")%>
                                    <%}%>
                                    <%else
                                        {%><br />
                                    <%: Html.TranslateTag("Export/ConfigureMQTTwithCerticate|Retries: Disabled","Retries: Disabled")%>
                                    <%}%>
                                </div>
                                <% } %>
                            </div>

                            <div class="col-12 text-end">
                                <input type="submit" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary" />
                                <% if (Model != null && Model.ExternalDataSubscriptionID > 0)
                                    { %>
                                <input type="button" onclick="checkDelete()" value="<%:Html.TranslateTag("Delete", "Delete")%>" class="btn btn-secondary" />
                                <%} %>
                                <input type="button" id="resetBrokenSend" onclick="resetBroken();" value="<%: Html.TranslateTag("Reset Broken Send","Reset Broken Send")%>" class="btn btn-secondary" <%= Model.DoSend ? "disabled" : "" %> />
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <%--<input type="hidden" value="<%=Model.ExternalDataSubscriptionID %>" name="id" />--%>
                    </form>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        var areyousure = "<%: Html.TranslateTag("Export/ConfigureAWS|Are you sure you want to delete this?","Are you sure you want to delete this ?")%>";
        var resetBrokenString = "<%: Html.TranslateTag("Export/ConfigureAWS|Reset broken count to 0 ?","Reset broken count to 0 ?")%>";
        var certTooBigString = "<%: Html.TranslateTag("Certificate size larger/smaller than expected.","Certificate size larger/smaller than expected.")%>";

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

        function checkCertificateType(data) {
            var filesize = data.files[0].size;
            if (filesize > 16384 || filesize < 128) {
                $('#errMessages').html(certTooBigString + " " + Math.round((filesize / 10)) + " kb")
            }
        }

    </script>

</asp:Content>
