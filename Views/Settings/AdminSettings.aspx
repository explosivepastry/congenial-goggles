<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Account>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminSettings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%Account account = Account.Load(MonnitSession.CurrentCustomer.AccountID); %>
    <%--<%Html.RenderPartial("AccountLink", account); %>--%>
    <div class="container-fluid">
        <div class="card_container shadow-sm rounded mt-4" id="fullForm">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <%: Html.TranslateTag("Settings/AdminSettings|Admin Settings","Admin Settings")%>
                </div>
            </div>
            <div class="card_container__body">
                <div class="card_container__body__content">
                    <form id="prefForm" class="form-horizontal form-label-left" method="post">
                        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                        <%if (MonnitSession.IsCurrentCustomerMonnitAdmin)
                            { %>
                        <div class="formBody">
                            <div class="col-12">
                                <div class="row sensorEditForm">
                                    <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Admin AccountID","Admin AccountID")%></div>
                                    <div class="col sensorEditFormInput">
                                        <input class="form-control" type="text" name="AdminAccountID" value="<%: ConfigData.AppSettings("AdminAccountID")%>" />
                                    </div>
                                </div>
                                <div class="row sensorEditForm">
                                    <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Default NetworkID","Default NetworkID")%></div>
                                    <div class="col sensorEditFormInput">
                                        <input class="form-control" type="text" name="DefaultCSNetID" value="<%: ConfigData.AppSettings("DefaultCSNetID")%>" />
                                    </div>
                                </div>
                                <!-- force these to to one and take them out if it is enterprise -->
                                <% if (!ConfigData.AppSettings("IsEnterprise").ToBool())
                                    { %>
                                <div class="row sensorEditForm">
                                    <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Min Password Length", "Min Password Length")%></div>
                                    <div class="col sensorEditFormInput">
                                        <input class="form-control" type="text" name="MinPasswordLength" value="<%: ConfigData.AppSettings("MinPasswordLength")%>" />
                                    </div>
                                </div>
                                <%} %>
                                <div class="row sensorEditForm">
                                    <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|New Account Notification Email","New Account Notification Email")%></div>
                                    <div class="col sensorEditFormInput">
                                        <input class="form-control" type="text" name="NewAccountNotificationEmail" value="<%: ConfigData.AppSettings("NewAccountNotificationEmail")%>" />
                                    </div>
                                </div>
                                <div class="row sensorEditForm">
                                    <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Look Up Host","Look Up Host")%></div>
                                    <div class="col sensorEditFormInput">
                                        <input class="form-control" type="text" name="LookUpHost" value="<%: ConfigData.AppSettings("LookUpHost")%>" />
                                    </div>
                                </div>
                                <% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin && !ConfigData.AppSettings("IsEnterprise").ToBool())
                                    { %>

                                <div class="row sensorEditForm">
                                    <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Current EULA Version","Current EULA Version")%></div>
                                    <div class="col sensorEditFormInput">
                                        <input class="form-control" type="text" name="EULA" value="<%: ConfigData.AppSettings("EULA")%>" />
                                    </div>
                                </div>
                                <div class="row sensorEditForm">
                                    <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|MEA API Auth Guid","MEA API Auth Guid")%></div>
                                    <div class="col sensorEditFormInput">
                                        <input class="form-control" type="text" name="MEA_API_Auth_Guid" value="<%: ConfigData.AppSettings("MEA_API_Auth_Guid")%>" />
                                    </div>
                                </div>

                                <div class="row sensorEditForm">
                                    <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|MEA API Location","MEA API Location")%></div>
                                    <div class="col sensorEditFormInput">
                                        <input class="form-control" type="text" name="MEA_API_Location" value="<%: ConfigData.AppSettings("MEA_API_Location")%>" />
                                    </div>
                                </div>

                                <div class="row sensorEditForm">
                                    <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|External Message Table","External Message Table")%></div>
                                    <div class="col sensorEditFormInput">
                                        <select name="ExternalMessageTable" class="form-select">
                                            <option value="False"><%: Html.TranslateTag("False","False")%></option>
                                            <option value="True" <%: ConfigData.AppSettings("ExternalMessageTable").ToBool() ? "selected=selected" : ""%>><%: Html.TranslateTag("True","True")%></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="row sensorEditForm">
                                    <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|SensorMessageAudit","SensorMessageAudit")%></div>
                                    <div class="col sensorEditFormInput">
                                        <select name="SensorMessageAudit" class="form-select">
                                            <option value="Off"><%: Html.TranslateTag("Off","Off")%></option>
                                            <option value="On" <%: ConfigData.AppSettings("SensorMessageAudit").ToBool() ? "selected=selected" : ""%>><%: Html.TranslateTag("On","On")%></option>
                                        </select>
                                    </div>
                                </div>

                                <div class="row sensorEditForm">
                                    <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Cred Session Length","Cred Session Length")%></div>
                                    <div class="col sensorEditFormInput">
                                        <input class="form-control" type="text" name="CredSessionLength" value="<%: ConfigData.AppSettings("CredSessionLength")%>" />
                                    </div>
                                </div>

                                <div class="row sensorEditForm">
                                    <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Translate Mode Active","Translate Mode Active")%></div>
                                    <div class="col sensorEditFormInput">
                                        <select name="TranslateModeActive" class="form-select">
                                            <option value="False"><%: Html.TranslateTag("False","False")%></option>
                                            <option value="True" <%: ConfigData.AppSettings("TranslateModeActive").ToBool() ? "selected=selected" : ""%>><%: Html.TranslateTag("True","True")%></option>
                                        </select>
                                    </div>
                                </div>

                                <%--Twilio Section--%>
                                <div id="twilioAdd" class="useAwareState" href="#" onclick="toggleTwilioSection()"><%: Html.TranslateTag("Twilio","Twilio")%> <%=Html.GetThemedSVG("add") %></div>
                                <div id="twilioMinus" class="useAwareState" style="display: none" href="#" onclick="toggleTwilioSection()"><%: Html.TranslateTag("Twilio","Twilio")%> <%=Html.GetThemedSVG("minus") %></div>

                                <div id="twilioSection">


                                    <div class="row sensorEditForm">
                                        <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Twilio Account ID","Twilio Account ID")%></div>
                                        <div class="col sensorEditFormInput">
                                            <input class="form-control" type="text" name="TwilioAccountSid" value="<%: ConfigData.AppSettings("TwilioAccountSid")%>" />
                                        </div>
                                    </div>
                                    <div class="row sensorEditForm">
                                        <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Twilio Account Auth Token","Twilio Account Auth SID")%></div>
                                        <div class="col sensorEditFormInput">
                                            <input class="form-control" type="text" name="TwilioAuthSid" value="<%: ConfigData.AppSettings("TwilioAuthSid")%>" />
                                        </div>
                                    </div>
                                    <div class="row sensorEditForm">
                                        <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Twilio Account Auth Token","Twilio Account Auth Secret")%></div>
                                        <div class="col sensorEditFormInput">
                                            <input class="form-control" type="text" name="TwilioAuthSecret" value="<%: ConfigData.AppSettings("TwilioAuthSecret")%>" />
                                        </div>
                                    </div>
                                    <div class="row sensorEditForm">
                                        <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Twilio Base API","Twilio Base API")%></div>
                                        <div class="col sensorEditFormInput">
                                            <input class="form-control" type="text" name="TwilioBaseAPI" value="<%: ConfigData.AppSettings("TwilioBaseAPI")%>" />
                                        </div>
                                    </div>
                                    <div class="row sensorEditForm">
                                        <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Twilio Call CallBack","Twilio Call CallBack")%></div>
                                        <div class="col sensorEditFormInput">
                                            <input class="form-control" type="text" name="TwilioCallCallback" value="<%: ConfigData.AppSettings("TwilioCallCallback")%>" />
                                        </div>
                                    </div>
                                    <div class="row sensorEditForm">
                                        <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Twilio Call Resource","Twilio Call Resource")%></div>
                                        <div class="col sensorEditFormInput">
                                            <input class="form-control" type="text" name="TwilioCallResource" value="<%: ConfigData.AppSettings("TwilioCallResource")%>" />
                                        </div>
                                    </div>
                                    <div class="row sensorEditForm">
                                        <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Twilio Call Url","Twilio Call Url")%></div>
                                        <div class="col sensorEditFormInput">
                                            <input class="form-control" type="text" name="TwilioCallUrl" value="<%: ConfigData.AppSettings("TwilioCallUrl")%>" />
                                        </div>
                                    </div>
                                    <div class="row sensorEditForm">
                                        <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Twilio Message Callback","Twilio Message Callback")%></div>
                                        <div class="col sensorEditFormInput">
                                            <input class="form-control" type="text" name="TwilioMessageCallback" value="<%: ConfigData.AppSettings("TwilioMessageCallback")%>" />
                                        </div>
                                    </div>
                                    <div class="row sensorEditForm">
                                        <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Twilio Message Resource","Twilio Message Resource")%></div>
                                        <div class="col sensorEditFormInput">
                                            <input class="form-control" type="text" name="TwilioMessageResource" value="<%: ConfigData.AppSettings("TwilioMessageResource")%>" />
                                        </div>
                                    </div>
                                    <%} %>
                                    <!-- take out current firmware/gateway versions -->
                                </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="text-end">
                            <button type="submit" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary mt-2">
                                <%: Html.TranslateTag("Save","Save")%>
                            </button>
                            <div style="clear: both;"></div>
                        </div>
                </div>


                <%--Cassandra Section--%>
                <div id="cassandraAdd" class="useAwareState" href="#" onclick="toggleCassandraSection()"><%: Html.TranslateTag("Cassandra","Cassandra")%> <%=Html.GetThemedSVG("add") %></div>
                <div id="cassandraMinus" class="useAwareState" style="display: none" href="#" onclick="toggleCassandraSection()"><%: Html.TranslateTag("Cassandra","Cassandra")%> <%=Html.GetThemedSVG("minus") %></div>

                <div id="cassandraSection">

                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Cassandra File Path","Cassandra File Path")%></div>
                        <div class="col sensorEditFormInput">
                            <input class="form-control" type="text" name="CassandraFilePath" value="<%: ConfigData.AppSettings("CassandraFilePath")%>" />
                        </div>
                    </div>

                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Cassandra User","Cassandra User")%></div>
                        <div class="col sensorEditFormInput">
                            <input class="form-control" type="text" name="CassandraUser" value="<%: ConfigData.AppSettings("CassandraUser")%>" />
                        </div>
                    </div>

                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Cassandra Password","Cassandra Password")%></div>
                        <div class="col sensorEditFormInput">
                            <input class="form-control" type="text" name="CassandraPass" value="<%: ConfigData.AppSettings("CassandraPass")%>" />
                        </div>
                    </div>

                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Cassandra Reader","Cassandra Reader")%></div>
                        <div class="col sensorEditFormInput">
                            <input class="form-control" type="text" name="CassandraReader" value="<%: ConfigData.AppSettings("CassandraReader")%>" />
                        </div>
                    </div>

                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Cassandra Writer","Cassandra Writer")%></div>
                        <div class="col sensorEditFormInput">
                            <input class="form-control" type="text" name="CassandraWriter" value="<%: ConfigData.AppSettings("CassandraWriter")%>" />
                        </div>
                    </div>

                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Cassandra Key Space","Cassandra Key Space")%></div>
                        <div class="col sensorEditFormInput">
                            <input class="form-control" type="text" name="CassandraKeySpace" value="<%: ConfigData.AppSettings("CassandraKeySpace")%>" />
                        </div>
                    </div>

                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Cassandra Enable Gateway Messages","Cassandra Enable Gateway Messages")%></div>
                        <div class="col sensorEditFormInput">
                            <input class="form-control" type="text" name="CassandraEnableGatewayMessages" value="<%: ConfigData.AppSettings("CassandraEnableGatewayMessages")%>" />
                        </div>
                    </div>

                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Cassandra Enable Data Messages","Cassandra Enable Data Messages")%></div>
                        <div class="col sensorEditFormInput">
                            <input class="form-control" type="text" name="CassandraEnableDataMessages" value="<%: ConfigData.AppSettings("CassandraEnableDataMessages")%>" />
                        </div>
                    </div>


                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Cassandra Enable Inbound Packets","Cassandra Enable Inbound Packets")%></div>
                        <div class="col sensorEditFormInput">
                            <input class="form-control" type="text" name="CassandraEnableInboundPackets" value="<%: ConfigData.AppSettings("CassandraEnableInboundPackets")%>" />
                        </div>
                    </div>

                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Cassandra Enable Outbound Packets","Cassandra Enable Outbound Packets")%></div>
                        <div class="col sensorEditFormInput">
                            <input class="form-control" type="text" name="CassandraEnableOutboundPackets" value="<%: ConfigData.AppSettings("CassandraEnableOutboundPackets")%>" />
                        </div>
                    </div>

                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Cassandra Data Message Cutoff Date","Cassandra Data Message Cutoff Date")%></div>
                        <div class="col sensorEditFormInput">
                            <input class="form-control" type="text" name="CassandraDataMessageCutoffDate" value="<%: ConfigData.AppSettings("CassandraDataMessageCutoffDate")%>" />
                        </div>
                    </div>

                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Cassandra Gateway Message Cutoff Date","Cassandra Gateway Message Cutoff Date")%></div>
                        <div class="col sensorEditFormInput">
                            <input class="form-control" type="text" name="CassandraGatewayMessageCutoffDate" value="<%: ConfigData.AppSettings("CassandraGatewayMessageCutoffDate")%>" />
                        </div>
                    </div>

                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3 col-lg-2"><%: Html.TranslateTag("Settings/AdminSettings|Cassandra Secure Connect Bundle Name","Cassandra Secure Connect Bundle Name")%></div>
                        <div class="col sensorEditFormInput">
                            <input class="form-control" type="text" name="CassandraSecureConnectBundleName" value="<%: ConfigData.AppSettings("CassandraSecureConnectBundleName")%>" />
                        </div>
                    </div>

                </div>


                <%}%>
                    </form>
            </div>
        </div>
    </div>
    </div>

        <script>

            function toggleTwilioSection() {
                $('#twilioSection').slideToggle('slow');

                if ($('#twilioAdd').is(':visible')) {
                    $('#twilioAdd').hide();
                    $('#twilioMinus').show();
                } else {
                    $('#twilioAdd').show();
                    $('#twilioMinus').hide();
                }
            }

            function toggleCassandraSection() {
                $('#cassandraSection').slideToggle('slow');

                if ($('#cassandraAdd').is(':visible')) {
                    $('#cassandraAdd').hide();
                    $('#cassandraMinus').show();
                } else {
                    $('#cassandraAdd').show();
                    $('#cassandraMinus').hide();
                }
            }
        </script>

    <style>
        #twilioSection {
            display: none;
        }

        #cassandraSection {
            display: none;
        }

        .sensorEditFormInput input, .sensorEditFormInput select {
            max-width: 400px !important;
        }
    </style>


</asp:Content>


