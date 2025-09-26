<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<ExternalDataSubscription>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ConfigureWatson
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% 
        char[] WebhookClass = Model.eExternalDataSubscriptionClass.ToString().ToCharArray(); // either "webhook" or "notification"
        WebhookClass[0] = Char.ToUpperInvariant(WebhookClass[0]);
        string WebhookClassName = Model.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.webhook ? "DataWebhook" : "NotificationWebhook";
    %>

    <div class="container-fluid">
        <div class="col-12">
            <%Html.RenderPartial("_APILink"); %>
        </div>

        <div class="col-12">
            <div class="x_panel shadow-sm rounded mt-2">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <%:Html.TranslateTag("Export/ConfigureWatson|Configure Watson IoT","Configure Watson IoT")%>
                    </div>
                </div>

                <div class="x_content">

                    <div class="col-12">
                        <div class="col-lg-10 col-12">
                            <h2><%:Html.TranslateTag("Export/ConfigureWatson|How Data is passed to IBM Watson IoT platform","How Data is passed to IBM Watson IoT platform")%></h2>
                            <span style="font-size: 1.1em; align-content: center;"><%:Html.TranslateTag("Export/ConfigureWatson|This passes data to your IBM Watson endpoint when data is received at the server. Create a device in your IBM Cloud dashboard to represent this connection. All sensor and gateway message data will be sent to this device's endpoint.  Data is sent as  JSON. IBM, Watson, Watson IoT,  and IBM Cloud are trademarks of International Business Machines Corporation, registered in many jurisdictions worldwide. ")%></span>
                        </div>

                        <div style="clear: both;"></div>
                        <hr />
                    </div>

                    <%: Html.ValidationSummary() %>
                    <form id="prefForm" action="/Export/ConfigureWatson/<%= WebhookClass %>" method="post" class="form-horizontal form-label-left">
                        <div class="form-group">
                            <%: Html.ValidationMessage("main") %>
                            <%: Html.ValidationMessageFor(m => m.ConnectionInfo1) %>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group" style="display: none">
                            <div class="bold col-lg-3 col-12">
                                <%: Html.TranslateTag("Export/ConfigureWatson|Webhook Class","Webhook Class")%>
                            </div>

                            <div class="col-lg-9 col-12 lgbox">
                                <input type="text" class="form-control" name="eEDSClass" value="<%=Model.eExternalDataSubscriptionClass.ToInt() %>" style="width: 250px;" />
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <%
                            List<ExternalDataSubscriptionProperty> propertyList = ExternalDataSubscriptionProperty.LoadByExternalDataSubscriptionID(Model.ExternalDataSubscriptionID);

                            string OrgID = "";
                            string TypeID = "";
                            string DeviceID = "";
                            string EventName = "";
                            string APIKey = "";
                            string AuthToken = "";

                            if (propertyList.Count > 0)
                            {

                                ExternalDataSubscriptionProperty orgProp = propertyList.Where(m => m.Name == "OrgID").ToList().FirstOrDefault();
                                ExternalDataSubscriptionProperty typeProp = propertyList.Where(m => m.Name == "TypeID").ToList().FirstOrDefault();
                                ExternalDataSubscriptionProperty deviceProp = propertyList.Where(m => m.Name == "DeviceID").ToList().FirstOrDefault();
                                ExternalDataSubscriptionProperty eventProp = propertyList.Where(m => m.Name == "EventName").ToList().FirstOrDefault();
                                ExternalDataSubscriptionProperty APIKeyProp = propertyList.Where(m => m.Name == "username").ToList().FirstOrDefault();
                                ExternalDataSubscriptionProperty authTokenProp = propertyList.Where(m => m.Name == "password").ToList().FirstOrDefault();

                                if (orgProp != null)
                                    OrgID = orgProp.StringValue;
                                if (typeProp != null)
                                    TypeID = typeProp.StringValue;
                                if (deviceProp != null)
                                    DeviceID = deviceProp.StringValue;
                                if (eventProp != null)
                                    EventName = eventProp.StringValue;
                                if (APIKeyProp != null)
                                    APIKey = APIKeyProp.StringValue;
                                if (authTokenProp != null)
                                    AuthToken = authTokenProp.StringValue;
                            }
                        %>

                        <div class="form-group">
                            <div class="bold col-lg-2  col-12">
                                <%:Html.TranslateTag("Export/ConfigureWatson|Organization ID","Organization ID")%>
                            </div>

                            <div class="col-lg-8 col-12">
                                <input type="text" class="form-control" style="width: 250px;" value="<%=OrgID %>" required name="OrgID" id="OrgID" />
                                <%: Html.ValidationMessage("OrgID") %>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-2  col-12">
                                <%:Html.TranslateTag("Export/ConfigureWatson|Type ID","Type ID")%>
                            </div>
                            <div class="col-lg-8 col-12">

                                <input type="text" class="form-control" style="width: 250px;" value="<%=TypeID %>" required name="TypeID" id="TypeID" />
                                <%: Html.ValidationMessage("TypeID") %>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-2  col-12">
                                <%:Html.TranslateTag("Export/ConfigureWatson|Device ID","DeviceID")%>
                            </div>
                            <div class="col-lg-8 col-12">

                                <input type="text" class="form-control" style="width: 250px;" value="<%=DeviceID %>" required name="DeviceID" id="DeviceID" />
                                <%: Html.ValidationMessage("DeviceID") %>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-2  col-12">
                                <%:Html.TranslateTag("Export/ConfigureWatson|Event Name","EventName")%>
                            </div>
                            <div class="col-lg-8 col-12">

                                <input type="text" class="form-control" style="width: 250px;" value="<%=EventName %>" required name="EventName" id="EventName" />
                                <%: Html.ValidationMessage("EventName") %>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-2  col-12">
                                <%:Html.TranslateTag("Export/ConfigureWatson|API Key","API Key")%>
                            </div>

                            <div class="col-lg-8 col-12">
                                <input type="text" class="form-control" style="width: 250px;" value="<%=APIKey %>" required name="APIKey" id="APIKey" />
                                <%: Html.ValidationMessage("APIKey") %>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-2  col-12">
                                <%:Html.TranslateTag("Export/ConfigureWatson|Auth Token","Auth Token")%>
                            </div>

                            <div class="col-lg-8 col-12">
                                <input type="text" class="form-control" style="width: 250px;" value="<%=MonnitSession.UseEncryption ? AuthToken.Decrypt() : AuthToken %>" required name="AuthToken" id="AuthToken" />
                                <%: Html.ValidationMessage("AuthToken") %>
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
                                    <input class="form-check-input my-0 y-0 mx-2" type="checkbox" id="SendRawData" <%:Model.SendRawData ? "checked='checked'" : ""%>>
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
                                    <%:Html.TranslateTag("Export/ConfigureWatson|Sending: Enabled","Sending: Enabled")%>
                                    <%}%>
                                    <%else
                                        {%>
                                    <%:Html.TranslateTag("Export/ConfigureWatson|Sending: Disabled","Sending: Disabled")%>
                                    <%}%>
                                    <%if (Model.DoRetry)
                                        {%><br />
                                    <%:Html.TranslateTag("Export/ConfigureWatson|Retries: Enabled","Retries: Enabled")%>
                                    <%}%>
                                    <%else
                                        {%><br />
                                    <%:Html.TranslateTag("Export/ConfigureWatson|Retries: Disabled","Retries: Disabled")%>
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
                                <input type="button" id="resetBrokenSend" onclick="resetBroken();" value="<%:Html.TranslateTag("Export/ConfigureWatson|Reset Broken Send","Reset Broken Send")%>" class="btn btn-secondary" <%= Model.DoSend ? "disabled" : "" %> />
                            </div>

                            <div class="clearfix"></div>
                        </div>
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
                        showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
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
                        showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                    }
                });
            }
        }

    </script>

</asp:Content>
