<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<ExternalDataSubscription>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ConfigureAzureMQTT
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% 
        char[] WebhookClass = Model.eExternalDataSubscriptionClass.ToString().ToCharArray(); // either "webhook" or "notification"
        WebhookClass[0] = Char.ToUpperInvariant(WebhookClass[0]);
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
                        <%:Html.TranslateTag("Export/ConfigureAzureMQTT|Configure Azure IoT MQTT","Configure Azure IoT MQTT")%>
                    </div>
                </div>

                <div class="x_content">

                    <div class="col-12">
                        <div class="col-lg-10 col-12">
                            <h2><%: Html.TranslateTag("Export/ConfigureAzureMQTT|How MQTT Data is passed to Microsoft Azure IoT Hub","How MQTT Data is passed to Microsoft Azure IoT Hub")%></h2>
                            <%: Html.TranslateTag("Export/ConfigureAzureMQTT|This passes data to your Azure IoT Hub endpoint when data is received at the server.Create a device in your Azure IoT Hub dashboard to represent this connection. All sensor and gateway message data will be sent to this device's endpoint.From the Azure Iot Hub grab the device's Primary Connection string and paste in the top box to parse the needed parameters.Microsoft, Azure, Iot Hub,  and Azure IoT are trademarks of Microsoft Corporation, registered in many jurisdictions worldwide.","This passes data to your Azure IoT Hub endpoint when data is received at the server.Create a device in your Azure IoT Hub dashboard to represent this connection. All sensor and gateway message data will be sent to this device's endpoint.From the Azure Iot Hub grab the device's Primary Connection string and paste in the top box to parse the needed parameters.Microsoft, Azure, Iot Hub,  and Azure IoT are trademarks of Microsoft Corporation, registered in many jurisdictions worldwide.")%>
                        </div>

                        <div style="clear: both;"></div>
                        <hr />
                    </div>

                    <%: Html.ValidationSummary() %>
                    <form id="prefForm" action="/Export/ConfigureAzureMQTT/<%= WebhookClass %>" method="post" class="form-horizontal form-label-left">
                        <div class="form-group">
                            <%: Html.ValidationMessage("main") %>
                            <%: Html.ValidationMessageFor(m => m.ConnectionInfo1) %>
                            <div class="clearfix"></div>
                        </div>

                        <% 
                            //Primary Key (Security)
                            //Hub Address (URL)
                            //DeviceID (Identifier of which device/account is sending data)

                            List<ExternalDataSubscriptionProperty> propertyList = ExternalDataSubscriptionProperty.LoadByExternalDataSubscriptionID(Model.ExternalDataSubscriptionID);

                            string IOTHubAddress = "";
                            string primaryKey = "";
                            //string policyName = "";
                            //string topic = "";
                            string deviceID = "";

                            if (propertyList.Count > 0)
                            {
                                ExternalDataSubscriptionProperty hubProp = propertyList.Where(m => m.Name == "iotHub").ToList().FirstOrDefault();
                                ExternalDataSubscriptionProperty keyProp = propertyList.Where(m => m.Name == "primaryKey").ToList().FirstOrDefault();
                                //ExternalDataSubscriptionProperty policyProp = propertyList.Where(m => m.Name == "policyName").ToList().FirstOrDefault();
                                //ExternalDataSubscriptionProperty topicProp = propertyList.Where(m => m.Name == "topic").ToList().FirstOrDefault();
                                ExternalDataSubscriptionProperty deviceProp = propertyList.Where(m => m.Name == "deviceID").ToList().FirstOrDefault();

                                if (hubProp != null)
                                    IOTHubAddress = hubProp.StringValue;
                                if (deviceProp != null)
                                    deviceID = deviceProp.StringValue;
                                if (keyProp != null)
                                    primaryKey = keyProp.StringValue;
                                //if (policyProp != null)
                                //    policyName = policyProp.StringValue;
                                //if (topicProp != null)
                                //    topic = topicProp.StringValue;
                            }
                        %>

                        <div class="form-group" style="display: none">
                            <div class="bold col-lg-3 col-12">
                                <%:Html.TranslateTag("Export/ConfigureAzureMQTT|Webhook Class","Webhook Class")%>
                            </div>

                            <div class="col-lg-9 col-12 lgbox">
                                <input type="text" class="form-control" name="eEDSClass" value="<%=Model.eExternalDataSubscriptionClass.ToInt() %>" style="width: 250px;" />
                            </div>

                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-2  col-12">
                                <%:Html.TranslateTag("Export/ConfigureAzureMQTT|Primary Connection String","Primary Connection String")%>
                            </div>

                            <div class="col-lg-8 col-12">
                                <div class="col-lg-10 col-12">
                                    <input type="text" class="form-control" style="width: 100%" value="" id="connectionString" />
                                </div>

                                <div class="col-lg-2 col-12">
                                    <input class="btn btn-secondary" type="button" id="parseStringBtn" onclick="parseConnectionString();" value="<%:Html.TranslateTag("Parse","Parse")%>" />
                                </div>
                            </div>

                            <div class="clearfix"></div>
                        </div>
                        <hr />

                        <div class="form-group">
                            <div class="bold col-lg-2  col-12">
                                <%:Html.TranslateTag("Export/ConfigureAzureMQTT|Host Name","Host Name")%> <span style="color: red;">*</span>
                            </div>

                            <div class="col-lg-8 col-12">
                                <input type="text" class="form-control" style="width: 300px;" value="<%=IOTHubAddress%>" required name="IOTHub" id="IOTHub" />
                            </div>

                            <div class="clearfix"></div>
                        </div>

                        <%--<div class="form-group">
<div class="bold col-lg-2  col-12">
Policy Name *
</div>
<div class="col-lg-9 col-12">

<input type="text" class="form-control" style="width: 250px;" value="<%=String.IsNullOrEmpty(policyName) ? "iothubowner" : policyName %>" required name="policyName" id="policyName" />
</div>
<div class="clearfix"></div>
</div>--%>

                        <div class="form-group">
                            <div class="bold col-lg-2  col-12">
                                <%:Html.TranslateTag("Export/ConfigureAzureMQTT|Primary Key","Primary Key")%> <span style="color: red;">*</span>
                            </div>

                            <div class="col-lg-8 col-12 lgbox text-break">
                                <input type="text" class="form-control" style="width: 450px;" value="<%=primaryKey%>" required name="primaryKey" id="primaryKey" />
                            </div>

                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-2  col-12">
                                <%:Html.TranslateTag("Export/ConfigureAzureMQTT|Device ID","Device ID")%> <span style="color: red;">*</span>
                            </div>

                            <div class="col-lg-9 col-12">
                                <input type="text" class="form-control" style="width: 250px;" value="<%=deviceID%>" required name="deviceID" id="deviceID" />
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

                        <%--<div class="form-group">
<div class="bold col-lg-2  col-12">
Topic *
</div>
<div class="col-lg-9 col-12">

<input type="text" class="form-control" style="width: 450px;" value="<%=topic %>" required name="topic" id="topic" />
</div>
<div class="clearfix"></div>
</div>--%>

                        <div class="form-group">
                            <div class="bold col-lg-3 col-12">
                                <%if (Model != null && Model.ExternalDataSubscriptionID > 0)
                                    { %>

                                <div class="BrokenCount" style="float: left; margin: 10px 5px 10px 5px; color: #2D4780;">
                                    <%if (Model.DoSend)
                                        {%>
                                    <%:Html.TranslateTag("Export/ConfigureAzureMQTT|Sending: Enabled","Sending: Enabled")%>
                                    <%}%>
                                    <%else
                                        {%>
                                    <%:Html.TranslateTag("Export/ConfigureAzureMQTT|Sending: Disabled","Sending: Disabled")%>
                                    <%}%>
                                    <%if (Model.DoRetry)
                                        {%><br />
                                    <%:Html.TranslateTag("Export/ConfigureAzureMQTT|Retries: Enabled","Retries: Enabled")%>
                                    <%}%>
                                    <%else
                                        {%><br />
                                    <%:Html.TranslateTag("Export/ConfigureAzureMQTT|Retries: Disabled","Retries: Disabled")%>
                                    <%}%>
                                </div>
                                <% } %>
                            </div>

                            <div class="col-12 text-end">
                                <input type="submit" value="<%:Html.TranslateTag("Save","Save")%>" class="btn btn-primary" />
                                <% if (Model != null && Model.ExternalDataSubscriptionID > 0)
                                    { %>
                                <input type="button" onclick="checkDelete()" value="<%:Html.TranslateTag("Delete", "Delete")%>" class="btn btn-secondary" />
                                <%} %>
                                <input type="button" id="resetBrokenSend" onclick="resetBroken();" value="<%:Html.TranslateTag("Export/ConfigureAzureMQTT|Reset Broken Send","Reset Broken Send")%>" class="btn btn-secondary" <%= Model.DoSend ? "disabled" : "" %> />
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

        function parseConnectionString() {
            var fullString = $('#connectionString').val().trim();
            if (fullString.indexOf("SharedAccessKeyName") >= 0) {
                showSimpleMessageModal("<%=Html.TranslateTag("This looks like the Policy connection string, Please use the Device's Connection string")%>");
                return;
            }

            var params = fullString.split(";");
            if (params.length > 2) {
                var hostName = params[0];
                if (hostName.startsWith("HostName=")) {
                    hostName = hostName.slice(9, hostName.length);
                    if (hostName.indexOf(".azure-devices.net") == -1) {
                        showSimpleMessageModal("<%=Html.TranslateTag("Host Name in incorrect format: try adding .azure-device.net to the end")%>");
                        return;
                    }
                    $('#IOTHub').val(hostName);
                } else {
                    showSimpleMessageModal("<%=Html.TranslateTag("Host Name in incorrect format")%>");
                }

                var deviceID = params[1];
                if (deviceID.startsWith("DeviceId=")) {
                    deviceID = deviceID.slice(9, deviceID.length);
                    $('#deviceID').val(deviceID);
                } else {
                    showSimpleMessageModal("<%=Html.TranslateTag("Device ID in incorrect format")%>");
                }

                var primaryKey = params[2];
                if (primaryKey.startsWith("SharedAccessKey=")) {
                    primaryKey = primaryKey.slice(16, primaryKey.length);
                    $('#primaryKey').val(primaryKey);
                } else {
                    showSimpleMessageModal("<%=Html.TranslateTag("Primary Key in incorrect format")%>");
                }
            } else {
                showSimpleMessageModal("<%=Html.TranslateTag("Primary Key in incorrect format")%>");
            }
        }
    </script>

</asp:Content>
