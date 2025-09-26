<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<ExternalDataSubscription>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ConfigureAzure
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
                        <%: Html.TranslateTag("Export/ConfigureAzure|Configure Azure IoT","Configure Azure IoT")%>
                    </div>
                </div>

                <div class="x_content">

                    <div class="col-12">
                        <div class="col-lg-10 col-12">
                            <h2><%: Html.TranslateTag("Export/ConfigureAzure|How Data is passed to Microsoft Azure IoT Hub","How Data is passed to Microsoft Azure IoT Hub")%></h2>
                            <%: Html.TranslateTag("Export/ConfigureAzure|This passes data to your Azure IoT Hub endpoint when data is received at the server. Create a device in your Azure IoT Hub dashboard to represent this connection. All sensor and gateway message data will be sent to this device's endpoint. Use the Azure Device explorer tool to generate your token or view","This passes data to your Azure IoT Hub endpoint when data is received at the server. Create a device in your Azure IoT Hub dashboard to represent this connection. All sensor and gateway message data will be sent to this device's endpoint. Use the Azure Device explorer tool to generate your token or view")%>
                            <a target="_blank" role="link" style="color: blue;" href="https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-devguide-security"><u><b><%: Html.TranslateTag("Export/ConfigureAzure|Here","Here")%></b></u></a> <%:Html.TranslateTag("Export/ConfigureAzure|to generate it yourself. Microsoft, Azure, Iot Hub, and Azure IoT are trademarks of Microsoft Corporation, registered in many jurisdictions worldwide.","to generate it yourself. Microsoft, Azure, Iot Hub, and Azure IoT are trademarks of Microsoft Corporation, registered in many jurisdictions worldwide.")%>
                        </div>
                        <div style="clear: both;"></div>

                        <hr />
                    </div>

                    <%: Html.ValidationSummary() %>
                    <form id="prefForm" action="/Export/ConfigureAzure/<%= WebhookClass %>" method="post" class="form-horizontal form-label-left">
                        <div class="form-group">
                            <%: Html.ValidationMessage("main") %>
                            <%: Html.ValidationMessageFor(m => m.ConnectionInfo1) %>
                            <div class="clearfix"></div>
                        </div>

                        <% //stamdard passwprd works x509 does not

                            List<ExternalDataSubscriptionProperty> propertyList = ExternalDataSubscriptionProperty.LoadByExternalDataSubscriptionID(Model.ExternalDataSubscriptionID);

                            string IOTHubName = "";
                            //string apiVersion = "";
                            string deviceID = "";
                            string sasToken = "";

                            if (propertyList.Count > 0)
                            {

                                ExternalDataSubscriptionProperty hubProp = propertyList.Where(m => m.Name == "iotHub").ToList().FirstOrDefault();
                                // ExternalDataSubscriptionProperty versionProp = propertyList.Where(m => m.Name == "apiVersion").ToList().FirstOrDefault();
                                ExternalDataSubscriptionProperty deviceProp = propertyList.Where(m => m.Name == "deviceID").ToList().FirstOrDefault();
                                ExternalDataSubscriptionProperty sasProp = propertyList.Where(m => m.Name == "sasToken").ToList().FirstOrDefault();

                                if (hubProp != null)
                                    IOTHubName = hubProp.StringValue;
                                //if (versionProp != null)
                                //    apiVersion = versionProp.StringValue;
                                if (deviceProp != null)
                                    deviceID = deviceProp.StringValue;
                                if (sasProp != null)
                                    sasToken = sasProp.StringValue;
                            }
                        %>

                        <div class="form-group" style="display: none">
                            <div class="bold col-lg-3 col-12">
                                <%: Html.TranslateTag("Export/ConfigureAzure|Webhook Class","Webhook Class")%>
                            </div>

                            <div class="col-lg-9 col-12 lgbox">
                                <input type="text" class="form-control" name="eEDSClass" value="<%=Model.eExternalDataSubscriptionClass.ToInt() %>" style="width: 250px;" />
                            </div>

                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-2  col-12">
                                <%: Html.TranslateTag("Export/ConfigureAzure|Iot Hub Name","Iot Hub Name")%>
                            </div>

                            <div class="col-lg-8 col-12">
                                <input type="text" class="form-control" style="width: 250px;" value="<%=IOTHubName %>" required name="IOTHub" id="IOTHub" />
                                <%: Html.ValidationMessage("IOTHub") %>
                            </div>

                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-2  col-12">
                                <%: Html.TranslateTag("Device ID","Device ID")%>
                            </div>
                            <div class="col-lg-9 col-12">

                                <input type="text" class="form-control" style="width: 250px;" value="<%=deviceID %>" required name="deviceID" id="deviceID" />
                                <%: Html.ValidationMessage("DeviceID") %>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-2  col-12">
                                <%: Html.TranslateTag("Export/ConfigureAzure|Shared Access Signature","Shared Access Signature")%>
                            </div>

                            <div class="col-lg-8 col-12 lgbox text-break">
                                <input type="text" class="form-control" style="width: 100%;" value="<%=MonnitSession.UseEncryption ? sasToken.Decrypt() : sasToken %>" placeholder="SharedAccessSignature sr=HostName.azure-devices.net%2fdevices%2fDeviceName&sig=RxxxxxxxtE%3d&se=1555372034" required name="sasToken" id="sasToken" style="width: 50%;" />
                                <br />
                                <br />
                                <b><%: Html.TranslateTag("example","example")%>:</b>  SharedAccessSignature sr=HostName.azure-devices.net%2fdevices%2fDeviceName&sig=RxxxxxxxtE%3d&se=1234567890
                                    <%: Html.ValidationMessage("sasToken") %>
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
                            <div class="bold col-lg-3 col-12">
                                <%if (Model != null && Model.ExternalDataSubscriptionID > 0)
                                    { %>
                                <div class="BrokenCount" style="float: left; margin: 10px 5px 10px 5px; color: #2D4780;">
                                    <%if (Model.DoSend)
                                        {%>
                                    <%: Html.TranslateTag("Export/ConfigureAzure|Sending: Enabled","Sending: Enabled")%>
                                    <%}%>
                                    <%else
                                        {%>
                                    <%: Html.TranslateTag("Export/ConfigureAzure|Sending: Disabled","Sending: Disabled")%>
                                    <%}%>
                                    <%if (Model.DoRetry)
                                        {%><br />
                                    <%: Html.TranslateTag("Export/ConfigureAzure|Retries: Enabled","Retries: Enabled")%>
                                    <%}%>
                                    <%else
                                        {%><br />
                                    <%: Html.TranslateTag("Export/ConfigureAzure|Retries: Disabled","Retries: Disabled")%>
                                    <%}%>
                                </div>
                                <% } %>
                            </div>

                            <div class="col-12 text-end">
                                <input type="submit" value="<%: Html.TranslateTag("Export/ConfigureAzure|Save","Save")%>" class="btn btn-primary" />
                                <% if (Model != null && Model.ExternalDataSubscriptionID > 0)
                                    { %>
                                <input type="button" onclick="checkDelete()" value="<%:Html.TranslateTag("Delete", "Delete")%>" class="btn btn-secondary" />
                                <%} %>
                                <input type="button" id="resetBrokenSend" onclick="resetBroken();" value="<%: Html.TranslateTag("Export/ConfigureAzure|Reset Broken Send","Reset Broken Send")%>" class="btn btn-secondary" <%= Model.DoSend ? "disabled" : "" %> />
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
        var areyousure = "<%: Html.TranslateTag("Export/ConfigureAWS|Are you sure you want to delete this ?","Are you sure you want to delete this ?")%>";
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
