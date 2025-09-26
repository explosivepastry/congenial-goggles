<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.ExternalDataSubscription>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ConfigureWebhook
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
                        <%:Html.TranslateTag("Export/ConfigureWebhook|Configure Webhook","Configure Webhook")%>
                    </div>
                </div>

                <div class="x_content">
                    <div class="col-12">
                        <div class="col-lg-10 col-12">
                            <h2><%:Html.TranslateTag("Export/ConfigureWebhook|How Webhooks Pass Data to Your Application","How Webhooks Pass Data to Your Application")%></h2>
                            <span style="font-size: 1.1em; align-content: center;"><%:Html.TranslateTag("Export/ConfigureWebhook|A webhook sends data to your application when data is received at the server. You can configure the URL, Headers, Cookies,  and query parameters used to route the request. Headers and cookies will be available to add after saving your connection string.  Data is compiled as a JSON body and sent via HTTP POST.","A webhook sends data to your application when data is received at the server. You can configure the URL, Headers, Cookies,  and query parameters used to route the request. Headers and cookies will be available to add after saving your connection string.  Data is compiled as a JSON body and sent via HTTP POST.")%></span>
                        </div>
                        <div style="clear: both;"></div>
                        <hr />
                    </div>

                    <%: Html.ValidationSummary() %>
                    <form method="post" action="/Export/ConfigureWebhook/<%= WebhookClass %>">
                        <div style="display: none;">
                            <%-- Dummy fields to prevent browser (Chrome) from identifying 'ConnectionInfo1' as a login field --%>
                            <input type="text">
                            <input type="password">
                        </div>
                        <div class="form-group">
                            <%: Html.ValidationMessage("main") %>
                            <%: Html.ValidationMessageFor(m => m.ConnectionInfo1) %>
                            <div class="clearfix"></div>
                        </div>
                        <div class="form-group" style="display: none">
                            <div class="bold col-lg-3 col-12">
                                <%: Html.TranslateTag("Webhook Class","Webhook Class")%>
                            </div>
                            <div class="col-lg-9 col-12 lgbox">
                                <input type="text" class="form-control" name="eEDSClass" value="<%=Model.eExternalDataSubscriptionClass.ToInt() %>" style="width: 250px;" />
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-3 col-12">
                                <%: Html.TranslateTag("Base URL","Base URL")%>   <%-- autocomplete = "off" works well on Chrome Browser without browser to autofill/autocomplete text input fields --%>
                            </div>
                            <div class="col-lg-9 col-12 lgbox">
                                <%-- it works with autocomplete = off for all browsers in the locahost w/o htpps://  I should add https:// configure IIS or webconfig for my localhost with same replica production https:--%>
                                <input type="text" class="form-control" name="ConnectionInfo1" value="<%=Model.ConnectionInfo1 %>" style="width: 250px;" autocomplete="off" />
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group" <%=Model.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.notification ? "style=\"display: none;\"" : "" %>>
                            <div class="bold col-lg-3 col-12">
                                <%: Html.TranslateTag("Export/ConfigureWebhook|Send gateway message","Send gateway message")%>
                            </div>
                            <div class="col-lg-9 col-12">
                                <select class="form-select" name="sendWithoutDataMessage" id="sendWithoutDataMessage" style="width: 250px;">
                                    <option value="true" <%:  Model.SendWithoutDataMessage == true?"selected":""%>><%: Html.TranslateTag("Always","Always")%></option>
                                    <option value="false" <%:  Model.SendWithoutDataMessage == false?"selected":""%>><%: Html.TranslateTag("Export/ConfigureWebhook|Only with sensor messages","Only with sensor messages")%></option>
                                </select>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-3 col-12">
                                <%: Html.TranslateTag("Authentication","Authentication")%>
                            </div>
                            <div class="col-lg-9 col-12">
                                <select class="form-select" name="authenticationType" onchange="checkAuthType()" id="authenticationType" style="width: 250px;">
                                    <option value="none" <%:  string.IsNullOrEmpty(Model.Username)?"selected":""%>><%: Html.TranslateTag("None","None")%></option>
                                    <option value="basic" <%:  !string.IsNullOrEmpty(Model.Username) ?"selected":""%>><%: Html.TranslateTag("Basic","Basic")%></option>
                                </select>
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
                                    <input class="form-check-input my-0 y-0 mx-2" type="checkbox" id="sendRawData" <%:Model.SendRawData ? "checked='checked'" : ""%>>
                                    <label class="form-check-label"><%: Html.TranslateTag("Yes","Yes")%></label>
                                    <input type="hidden" name="sendRawData" id="sendRawDataHidden" value="<%:Model.SendRawData %>">
                                </div>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class=" form-group basicAuth" style="display: <%: string.IsNullOrEmpty(Model.Username) ?"none":"block"%>">
                            <div class="form-group">
                                <div class="col-lg-3 col-12">
                                    <%: Html.TranslateTag("Username","Username")%>
                                </div>
                                <div class="col-lg-9 col-12">
                                    <input class="basicAuth form-control" type="text" name="username" id="username" value="<%=Model.Username %>" style="width: 250px;" />
                                </div>
                                <div class="clearfix"></div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 col-12">
                                    <%: Html.TranslateTag("Password","Password")%>
                                </div>
                                <div class="col-lg-9 col-12">
                                    <input class="basicAuth form-control" autocomplete="off" type="password" name="password" id="password" value="<%=MonnitSession.UseEncryption ? Model.Password.Decrypt() : Model.Password %>" style="width: 250px;" />
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-3 col-12">
                                <%if (Model != null && Model.ExternalDataSubscriptionID > 0)
                                    { %>
                                <div class="BrokenCount" style="float: left; margin: 10px 5px 10px 5px; color: #2D4780;">
                                    <%if (Model.DoSend)
                                        {%>
                                    <%: Html.TranslateTag("Export/ConfigureWebhook|Sending: Enabled","Sending: Enabled")%>
                                    <%}%>
                                    <%else
                                        {%>
                                    <%: Html.TranslateTag("Export/ConfigureWebhook|Sending: Disabled","Sending: Disabled")%>
                                    <%}%>
                                    <br />
                                    <%if (Model.DoRetry)
                                        {%>
                                    <%: Html.TranslateTag("Export/ConfigureWebhook|Retries: Enabled","Retries: Enabled")%>
                                    <%}%>
                                    <%else
                                        {%>
                                    <%: Html.TranslateTag("Export/ConfigureWebhook|Retries: Disabled","Retries: Disabled")%>
                                    <%}%>
                                </div>
                                <% } %>
                            </div>

                            <div class="col-12 text-end">
                                <input type="submit" value="<%:Html.TranslateTag("Save","Save")%>" class="btn btn-primary" />
                                <% if (Model != null && Model.ExternalDataSubscriptionID > 0)
                                    { %>
                                <input type="button" onclick="checkDelete()" value="<%:Html.TranslateTag("Delete","Delete")%>" class="btn btn-secondary" />
                                <%} %>
                                <input type="button" id="resetBrokenSend" onclick="resetBroken();" value="<%:Html.TranslateTag("Export/ConfigureWebhook|Reset Broken Send","Reset Broken Send")%>" class="btn btn-secondary" <%= Model.DoSend ? "disabled" : "" %> />
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </form>

                    <%   
                        if (!string.IsNullOrEmpty(Model.ConnectionInfo1))
                        {
                            List<ExternalDataSubscriptionProperty> propertyList = ExternalDataSubscriptionProperty.LoadByExternalDataSubscriptionID(Model.ExternalDataSubscriptionID);
                            List<ExternalDataSubscriptionProperty> cookiePropList = propertyList.Where(m => m.Name == "cookie").ToList();
                            List<ExternalDataSubscriptionProperty> headerPropList = propertyList.Where(m => m.Name == "header").ToList();
                    %>
                    <div class="col-md-7 col-12">
                        <h2><%: Html.TranslateTag("Headers","Headers")%></h2>

                        <div class="row bold" style="font-size: 1.2em;">
                            <div class="col-5" style="text-decoration: underline;">
                                <%: Html.TranslateTag("Key","Key")%>
                            </div>

                            <div class="col-5" style="text-decoration: underline;">
                                <%: Html.TranslateTag("Value","Value")%>
                            </div>
                            <div class="col-2">
                            </div>
                        </div>
                        <br />
                        <div id="headerList">

                            <% Html.RenderPartial("WebhookHeader", headerPropList);%>
                        </div>

                        <div class="row">
                            <div class="col-md-5 col-12">
                                <input type="text" class="form-control" name="headerKey" id="headerKey" placeholder="<%: Html.TranslateTag("Key","Key")%> " />
                            </div>

                            <div class="col-md-5 col-9">
                                <input type="text" class="form-control" name="headerValue" id="headerValue" placeholder="<%: Html.TranslateTag("Value","Value")%>" />
                            </div>
                            <div class="col-md-2 col-3">
                                <input onclick="assignProperty('header');" class="btn btn-primary" id="headerAdd" type="button" value="<%: Html.TranslateTag("Add","Add")%>" />
                            </div>
                        </div>
                        <br />
                    </div>

                    <div class="col-md-7 col-12">
                        <h2><%: Html.TranslateTag("Cookies","Cookies")%></h2>
                        <div class="row bold" style="font-size: 1.2em;">
                            <div class="col-5" style="text-decoration: underline;">
                                <%: Html.TranslateTag("Key","Key")%>
                            </div>

                            <div class="col-5" style="text-decoration: underline;">
                                <%: Html.TranslateTag("Value","Value")%>
                            </div>
                            <div class="col-md-2 col-2">
                            </div>
                        </div>
                        <div id="cookieList">
                            <% Html.RenderPartial("WebhookCookie", cookiePropList);%>
                        </div>

                        <br />
                        <div class="row">
                            <div class="col-md-5 col-12">
                                <input type="text" name="cookieKey" class="form-control" id="cookieKey" placeholder="<%: Html.TranslateTag("Key","Key")%> " />
                            </div>

                            <div class="col-md-5 col-9">
                                <input type="text" name="cookieValue" class="form-control" id="cookieValue" placeholder="<%: Html.TranslateTag("Value","Value")%>" />
                            </div>
                            <div class="col-md-2 col-3">

                                <input onclick="assignProperty('cookie');" class="btn btn-primary" id="cookieAdd" type="button" value="<%: Html.TranslateTag("Add","Add")%>" />
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                    </div>
                    <%} %>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>

    <script type="text/javascript">

        var areyousure = "<%: Html.TranslateTag("Export/ConfigureAWS|Are you sure you want to delete this?","Are you sure you want to delete this ?")%>";
        var resetBrokenString = "<%: Html.TranslateTag("Export/ConfigureAWS|Reset broken count to 0 ?","Reset broken count to 0 ?")%>";
        var keyRequired = "<%: Html.TranslateTag("Key Required","Key Required")%>";

        function checkDelete() {

            let values = {};
            values.url = '/Export/WebHookDelete/<%:Model.ExternalDataSubscriptionID %>';
            values.text = areyousure;
            values.callback = function (data) {
                if (data == 'Success') {
                    showSimpleMessageModal("<%=Html.TranslateTag("Success")%>");
                    window.location.href = '/Export/<%=WebhookClassName%>/';
                } else {
                    console.log(data);
                    <%--let values = {};
                    values.redirect = '/Ack/<%:Model.NotificationRecordedID%>/<%:Model.NotificationGUID%>';--%>
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That did not work, please refresh your page. If this error continues, contact support.")%>");
                }
            };
            openConfirm(values);
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

        function checkAuthType() {
            var authType = $('#authenticationType').val();

            if (authType == "basic") {
                $('.basicAuth').show();
            } else {
                $('#username').val("");
                $('#password').val("");
                $('.basicAuth').hide();
            }
        }

        function assignProperty(propertyType) {
            var propertyKey = $('#' + propertyType + 'Key').val();
            var propertyVal = $('#' + propertyType + 'Value').val();

            if (propertyKey == '') {
                showSimpleMessageModal("<%=Html.TranslateTag("Key Required")%>");
            } else {
                $.post("/Export/AssignWebhookProperty/<%=Model.ExternalDataSubscriptionID%>", { key: propertyKey, value: propertyVal, type: propertyType }, function (data) {
                    if (data != 'Failed') {
                        $('#' + propertyType + 'Key').val("");
                        $('#' + propertyType + 'Value').val("");
                        $('#' + propertyType + 'List').html(data);
                    } else {
                        console.log(data);
                        let values = {};
                    <%--values.redirect = '/Ack/<%:Model.NotificationRecordedID%>/<%:Model.NotificationGUID%>';--%>
                        showSimpleMessageModal("<%=Html.TranslateTag("Oops! That did not work, please refresh your page. If this error continues, contact support.")%>");
                    }
                });
            }
        }

        function removeProperty(propertyID, propertyType) {
            $.post("/Export/RemoveWebhookProperty/<%=Model.ExternalDataSubscriptionID%>", { ExtnDtaSbscPropertyID: propertyID, type: propertyType }, function (data) {
                if (data != 'Failed') {
                    $('#' + propertyType + 'List').html(data);
                } else {
                    console.log(data);
                    let values = {};
                    <%--values.redirect = '/Ack/<%:Model.NotificationRecordedID%>/<%:Model.NotificationGUID%>';--%>
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That did not work, please refresh your page. If this error continues, contact support.")%>");
                }
            });
        }
    </script>

</asp:Content>
