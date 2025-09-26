<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<ExternalDataSubscriptionAttempt>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    WebhookAttempt
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <div class="col-12">
            <%Html.RenderPartial("_APILink"); %>
        </div>

        <div class="row">
            <div class="col-12">
                <div class="x_panel shadow-sm rounded">
                    <div class="x_title">
                        <h2><%:Html.TranslateTag("Export/WebhookAttempt|Attempt Details","Attempt Details")%></h2>

                        <div class="clearfix"></div>
                    </div>

                    <div class="x_content">
                        <div class="col-12">
                            <h2><%: Html.TranslateTag("Export/WebhookAttempt|Attempts & Retries","Attempts & Retries")%></h2>
                            <span style="font-size: 1.1em; align-content: center;"><%:Html.TranslateTag("Export/WebhookAttempt|Each individual message is only attempted 4 times until the total count for all attempts hits 20, then each message is only attempted 1 time up to 100 attempts at which time it stops sending until it is restarted. First attempt queued immediately (typically delivered within about a minute) Second attempt queued after 2 minutes Third attempt queued after 15 minutes Fourth attempt queued after 60 minutes Subsequent attempts only sent after manually being manually re-queued. There is a second limit of 10 total attempts that can be sent. After 10 failed attempts of a particular message (4 auto attempts + 6 manual resend commands) you will need to contact us to release that particular message.","Each individual message is only attempted 4 times until the total count for all attempts hits 20, then each message is only attempted 1 time up to 100 attempts at which time it stops sending until it is restarted. First attempt queued immediately (typically delivered within about a minute) Second attempt queued after 2 minutes Third attempt queued after 15 minutes Fourth attempt queued after 60 minutes Subsequent attempts only sent after manually being manually re-queued. There is a second limit of 10 total attempts that can be sent. After 10 failed attempts of a particular message (4 auto attempts + 6 manual resend commands) you will need to contact us to release that particular message.")%></span>

                            <div style="clear: both;"></div>

                            <div style="float: right;">
                                <br />

                                <%if (Model.Status != eExternalDataSubscriptionStatus.Success)
                                    {
                                        if (Model.Status == eExternalDataSubscriptionStatus.Retry)
                                        { %>
                                <span style="position: relative; font-weight: bold;"><%:Html.TranslateTag("Export/WebhookAttempt|Attempt retry queued.","Attempt retry queued.")%></span>
                                <%}
                                    else
                                    {
                                        if (Model.AttemptCount < 10)
                                        { %>
                                <span style="position: relative; font-weight: bold;"><%:Html.TranslateTag("Export/WebhookAttempt|Manually queue this attempt to be retried ?","Manually queue this attempt to be retried ?")%></span>
                                <br />
                                <br />
                                <input style="float: right;" class="btn btn-primary" type="button" id="Retry" name="Retry" value="<%:Html.TranslateTag("Export/WebhookAttempt|Retry","Retry")%>" onclick="Retry();" />
                                <%}
                                    else
                                    {%>
                                <span style="position: relative; font-weight: bold;"><%:Html.TranslateTag("Export/WebhookAttempt|This attempt has reached its maximum retry count.","This attempt has reached its maximum retry count.")%></span>

                                <%}
                                        }
                                    } %>
                            </div>
                            <br />
                        </div>

                        <div class="col-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <h2><%: Html.TranslateTag("Attempts","Attempts")%></h2>
                                    <div class="nav navbar-right panel_toolbox">
                                    </div>

                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">

                                    <div class="col-lg-6">
                                        <div class="row sensorEditForm">
                                            <div class="col-12 col-md-3">
                                                <%: Html.TranslateTag("URL","URL")%>
                                            </div>

                                            <div class="col sensorEditFormInput" style="overflow: auto;">
                                                <%=Model.Url %>
                                            </div>
                                        </div>
                                        <hr />

                                        <div class="row sensorEditForm">
                                            <div class="col-12 col-md-3">
                                                <%: Html.TranslateTag("Attempts","Attempts")%>
                                            </div>

                                            <div class="col sensorEditFormInput">
                                                <%=Model.AttemptCount%>
                                            </div>

                                        </div>
                                        <hr />
                                        <div class="row sensorEditForm">
                                            <div class="col-12 col-md-3">
                                                <%: Html.TranslateTag("Status","Status")%>
                                            </div>

                                            <div class="col sensorEditFormInput">
                                                <%=Model.Status%>
                                            </div>

                                        </div>
                                        <hr />
                                        <div class="row sensorEditForm">
                                            <div class="col-12 col-md-3">
                                                <%: Html.TranslateTag("Date","Date")%>
                                            </div>

                                            <div class="col sensorEditFormInput">
                                                <%:Model.CreateDate.OVToLocalDateTimeShort()%>
                                            </div>

                                        </div>

                                        <%if (Model.Properties.Count > 0)
                                            {
                                        %>
                                        <hr />
                                        <div class="row sensorEditForm">
                                            <div class="col-12 col-md-3">
                                                <%: Html.TranslateTag("Custom Headers","Custom Headers")%>
                                            </div>

                                            <div class="col sensorEditFormInput">

                                                <%List<ExternalDataSubscriptionProperty> headers = Model.Properties.Where(m => m.Name == "header").ToList();%>
                                                <%foreach (ExternalDataSubscriptionProperty prop in headers)
                                                    { %>
                                                <div class="row">
                                                    <div class="col-6"><%=prop.StringValue %></div>
                                                </div>
                                                <%} %>
                                            </div>

                                        </div>
                                        <hr />

                                        <div class="row sensorEditForm">
                                            <div class="col-12 col-md-3">
                                                <%: Html.TranslateTag("Cookies","Cookies")%>
                                            </div>
                                            <div class="col sensorEditFormInput">

                                                <%List<ExternalDataSubscriptionProperty> cookies = Model.Properties.Where(m => m.Name == "cookie").ToList();%>
                                                <%foreach (ExternalDataSubscriptionProperty prop in cookies)
                                                    { %>
                                                <div class="row">
                                                    <div class="col-6"><%=prop.StringValue %></div>
                                                </div>
                                                <%} %>
                                            </div>
                                        </div>

                                        <%} %>
                                        <br />
                                    </div>

                                    <div class="col-lg-6">
                                        <div class="formtitle"><%: Html.TranslateTag("Body","Body")%></div>
                                        <div style="max-height: 500px; overflow-y: auto; padding-right: 20px; padding-left: 20px; font-size: 14px; background-color: #DDEEFF; border: 1px solid black">
                                            <%=Model.body.FormatJson() %>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <h2><%: Html.TranslateTag("Response","Response")%></h2>
                                    <div class="nav navbar-right panel_toolbox">
                                    </div>

                                    <div class="clearfix"></div>
                                </div>

                                <div class="x_content">
                                    <%List<ExternalDataSubscriptionResponse> responseList = ExternalDataSubscriptionResponse.LoadByAttemptID(Model.ExternalDataSubscriptionAttemptID);
                                        foreach (ExternalDataSubscriptionResponse list in responseList)
                                           {
                                    %>
   
                                    <div class="row sensorEditForm">
                                        <div class="col-12 col-md-3">
                                            <%: Html.TranslateTag("Status","Status")%>
                                        </div>

                                        <div class="col sensorEditFormInput">
                                            <%=list.Status  %>
                                        </div>
                                    </div>
                                    <hr />

                                    <div class="row sensorEditForm">
                                        <div class="col-12 col-md-3">
                                            <%: Html.TranslateTag("Code","Code")%>
                                        </div>

                                        <div class="col sensorEditFormInput">
                                            <%=list.StatusCode %> : <%=list.RawCode %>
                                        </div>

                                    </div>
                                    <hr />

                                    <div class="row sensorEditForm">
                                        <div class="col-12 col-md-3">
                                            <%: Html.TranslateTag("Date","Date")%>
                                        </div>

                                        <div class="col sensorEditFormInput">
                                             <%: Html.TranslateTag("Local Time","Local Time")%>: <%: list.ResponseDateLocalTime(MonnitSession.CurrentCustomer.Account.TimeZoneID)%><br />
                                             <%: Html.TranslateTag("UTC","UTC")%>: <%:list.DateTime %>
                                        </div>

                                    </div>
                                    <hr />

                                    <div class="row sensorEditForm">
                                        <div class="col-12 col-md-3">
                                            <%: Html.TranslateTag("Headers","Headers")%>
                                        </div>

                                        <div class="col sensorEditFormInput">
                                            <%=list.Headers %>
                                        </div>

                                    </div>
                                    <hr />

                                    <div class="row sensorEditForm">
                                        <div class="col-12 col-md-3">
                                            <%: Html.TranslateTag("Response","Response")%>
                                        </div>

                                        <div class="col sensorEditFormInput">
                                            <%=list.Response%>
                                        </div>

                                    </div>
                                    <br />
                                    <%} %>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function Retry() {
            var id = '<%=Model.ExternalDataSubscriptionAttemptID%>';
            $.post('/Export/RetryAttempt', { IDStringList: id }, function (data) {
                if (data == 'Success')
                    window.location.href = window.location.href
                else (data.status)
            });
        }

    </script>

<%--    <style>
        .subnav .svg_icon path {
            fill: #333;
        }
    </style>--%>

</asp:Content>
