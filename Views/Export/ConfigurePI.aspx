<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<ExternalDataSubscription>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Configure PI OMF
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <div class="col-12">
            <%Html.RenderPartial("_APILink", Model); %>
        </div>

        <div class="col-12">
            <div class="x_panel shadow-sm rounded mt-2">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <%:Html.TranslateTag("Export/ConfigurePI|Configure PI Integration","Configure PI Integration")%>
                    </div>
                </div>

                <div class="x_content">

                    <div class="col-12">
                        <div class="col-lg-10 col-12">
                            <h2><%:Html.TranslateTag("Export/ConfigurePI|How Data is passed to PI OMF platform","How Data is passed to PI OMF platform")%></h2>
                            <span style="font-size: 1.1em; align-content: center;"><%:Html.TranslateTag("Export/ConfigurePI|This passes data to your PI OMF endpoint when data is received at the server.","This passes data to your PI OMF endpoint when data is received at the server.")%></span>
                        </div>
                        <div style="clear: both;"></div>

                        <hr />
                    </div>

                    <form id="prefForm" action="/Export/ConfigurePI/<%= Model.eExternalDataSubscriptionClass.ToString().ToCharArray() %>" method="post" class="form-horizontal form-label-left">
                        <div class="form-group">
                            <%: Html.ValidationMessage("main") %>

                            <div class="clearfix"></div>
                            <input type="hidden" class="form-control" name="eEDSClass" value="<%=Model.eExternalDataSubscriptionClass.ToInt() %>" />
                        </div>

                        <%
                            List<ExternalDataSubscriptionProperty> propertyList = ExternalDataSubscriptionProperty.LoadByExternalDataSubscriptionID(Model.ExternalDataSubscriptionID);

                            bool ValidateSSL = !Model.IgnoreSSLErrors;
                            string Username = "";
                            bool PasswordRecorded = false;

                            if (propertyList.Count > 0)
                            {

                                ExternalDataSubscriptionProperty UsernameProp = propertyList.Where(m => m.Name == "username").ToList().FirstOrDefault();
                                if (UsernameProp != null)
                                    Username = UsernameProp.StringValue;

                                ExternalDataSubscriptionProperty PasswordProp = propertyList.Where(m => m.Name == "password").ToList().FirstOrDefault();
                                if (PasswordProp != null)
                                    PasswordRecorded = !string.IsNullOrEmpty(PasswordProp.StringValue);

                            }
                        %>

                        <div class="form-group">
                            <div class="bold col-lg-3 col-12">
                                <%: Html.TranslateTag("Base URL","Base URL")%>
                            </div>
                            <div class="col-lg-9 col-12 lgbox">
                                <input type="text" class="form-control" name="ConnectionInfo1" value="<%=Model.ConnectionInfo1 %>" style="width: 250px;" />
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-3 col-12">
                                <%: Html.TranslateTag("Username","Username")%>
                            </div>
                            <div class="col-lg-9 col-12">
                                <input class="basicAuth form-control" type="text" name="Username" id="username" value="<%=Username %>" style="width: 250px;" />
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="col-lg-3 col-12">
                                <%: Html.TranslateTag("Password","Password")%>
                            </div>
                            <div class="col-lg-9 col-12">
                                <input class="basicAuth form-control" autocomplete="off" type="password" name="Password" id="password" value="" placeholder="<%=PasswordRecorded ? "Use Existing Password" : "" %>" style="width: 250px;" />
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-lg-2  col-12">
                                <%:Html.TranslateTag("Export/ConfigurePI|Validate SSL","Validate SSL")%>
                            </div>
                            <div class="col-lg-8 col-12">
                                <div class="form-check form-switch d-flex align-items-center ps-0">
                                    <label class="form-check-label"><%: Html.TranslateTag("Ignore Error","Ignore Error")%></label>
                                    <input class="form-check-input my-0 y-0 mx-2" type="checkbox" name="ValidateSSL" value="<%: Html.TranslateTag("True","True")%>" id="ValidateSSL" <%=ValidateSSL ? "checked" : "" %> />
                                    <label class="form-check-label"><%: Html.TranslateTag("Validate","Validate")%></label>
                                </div>
                                <%: Html.ValidationMessage("ValidateSSL") %>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div class="bold col-12">
                                <%if (Model != null && Model.ExternalDataSubscriptionID > 0)
                                    { %>
                                <div class="BrokenCount" style="float: left; margin: 10px 5px 10px 5px; color: #2D4780;">
                                    <%if (Model.DoSend)
                                        {%>
                                    <%: Html.TranslateTag("Export/ConfigurePI|Sending: Enabled","Sending: Enabled")%>
                                    <%}%>
                                    <%else
                                        {%>
                                    <%: Html.TranslateTag("Export/ConfigurePI|Sending: Disabled","Sending: Disabled")%>
                                    <%}%>
                                    <%if (Model.DoRetry)
                                        {%><br />
                                    <%: Html.TranslateTag("Export/ConfigurePI|Retries: Enabled","Retries: Enabled")%>
                                    <%}%>
                                    <%else
                                        {%><br />
                                    <%: Html.TranslateTag("Export/ConfigurePI|Retries: Disabled","Retries: Disabled")%>
                                    <%}%>
                                </div>
                                <% } %>
                            </div>
                            <div class="col-12 text-end">

                                <input type="submit" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary" />
                                <% if (Model != null && Model.ExternalDataSubscriptionID > 0 && Model.ExternalDataSubscriptionID > long.MinValue)
                                    { %>
                                <input type="button" onclick="checkDelete()" value="<%: Html.TranslateTag("Delete","Delete")%>" class="btn btn-secondary" />
                                <%if (Model != null && Model.ExternalDataSubscriptionID > 0 && Model.BrokenCount > ExternalDataSubscription.killSendLimit)
                                    {%>
                                <input type="button" id="resetBrokenSend" onclick="resetBroken();" value="<%: Html.TranslateTag("Export/ConfigurePI|Reset Broken Send","Reset Broken Send")%>" class="btn btn-default" />
                                <%}
                                    }%>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <%--<input type="hidden" value="<%=Model.ExternalDataSubscriptionID %>" name="id" />--%>
                    </form>

                    <div class="clearfix"></div>

                    <% if (Model != null && Model.ExternalDataSubscriptionID > 0 && Model.ExternalDataSubscriptionID > long.MinValue)
                        { %>
                    <div class="col-12">
                        <div class="col-8 col-md-4">

                            <%:Html.TranslateTag("Export/ConfigurePI|Register Type and Sensor to Pi Server","Register Type and Sensor to Pi Server")%>
                            <div class="input-group mb-0">
                                <input id="sensorid" name="sensorid" type="text" class="form-control me-0" placeholder="<%:Html.TranslateTag("Sensor ID","Sensor ID")%>">
                                <button class="btn btn-primary" id="regSensor" style="cursor: pointer;"><%:Html.TranslateTag("Register","Register")%></button>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>

                    <div class="col-12">
                        <div class="col-8 col-md-4">
                            <%:Html.TranslateTag("Export/ConfigurePI|Register All Sensors to Pi Server","Register All Sensors to Pi Server")%>
                            <div class="input-group mb-0">
                                <button class="btn btn-primary" id="regSensorMultiple" style="cursor: pointer;"><%:Html.TranslateTag("Export/ConfigurePI|Register All","Register All")%></button>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div class="col-12" id="RegisterResult">
                    </div>

                    <%}%>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">

        function resetBroken() {
            if (confirm('Reset broken count to 20?')) {
                $.get('/Export/ResetBrokenExternalDataSubscription/<%:Model.ExternalDataSubscriptionID%>', function (data) {
                    if (data != 'Success') {
                        console.log(data);
                        let values = {};
                    <%--values.redirect = '/Ack/<%:Model.NotificationRecordedID%>/<%:Model.NotificationGUID%>';--%>
                        showSimpleMessageModal("<%=Html.TranslateTag("Oops! That did not work, please refresh your page. If this error continues, contact support.")%>");
                    }
                    else {
                        $('#resetBrokenSend').hide();
                        window.location.href = window.location.href;
                    }
                });
            }
        }

        function checkDelete() {
            if (confirm('Are you sure you want to delete this?')) {
                $.get('/Export/WebHookDelete/<%: Model.ExternalDataSubscriptionID %>', function (data) {
                    if (data == 'Success') {
                        window.location.href = '/Export/DataWebhook/';
                    }
                    else {
                        showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                    }
                    alert(data);
                });
            }
        }

        $(function () {
            $('#regSensor').click(function () {
                $.post('/Export/ConfigurePISensor/', { id: <%: Model.ExternalDataSubscriptionID %>, sensorID: $('#sensorid').val() }, function (data) {
                    if (data == 'Success') {
                        //
                    }
                    $('#RegisterResult').html(data);
                });
            });

            $('#regSensorMultiple').click(function () {
                $.post('/Export/ConfigurePISensorMultiple/', { id: <%: Model.ExternalDataSubscriptionID %> }, function (data) {
                    $('#RegisterResult').html(data);
                });
            });
        });

    </script>

</asp:Content>
