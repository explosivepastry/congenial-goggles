<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Gateway>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit  Server Settings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="col-md-12 col-xs-12">
        <div class="x_panel">
            <div class="x_title">
                <h2><%: Html.TranslateTag("Gateway/EditServerSettings|Edit Gateway Server Settings","Edit Gateway Server Settings")%></h2>
                <div class="nav navbar-right panel_toolbox">
                    <!-- help button -->
                    <%--<a class="helpIco" data-toggle="modal" title="<%: Html.TranslateTag("Overview/SensorEdit|Sensor Help","Sensor Help") %>" data-target=".pageHelp"><img src="../../Content/images/iconmonstr-help-2-240 (1).png" style="height:18px;margin: 0px;margin-top:5px;margin-right: 5px;"></a>--%>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">

                <form action="/Gateway/EditServerSettings" method="post">
                    <%: Html.ValidationSummary(true) %>
                    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>

                    <div class="row sensorEditForm">
                        <div class="bold col-md-6 col-sm-6 col-xs-12">
                            <div class="editor-field" style="margin-top: 44px; font-size: 18px; color: red;">
                                <%: Model.IsDirty ?  Html.TranslateTag("Gateway/ServerSettings|Settings update pending...","Settings update pending..."): "" %>
                            </div>
                        </div>
                        <div class="col-md-6 col-sm-6 col-xs-12 mdBox"></div>
                        <div style="clear: both;"></div>
                    </div>

                    <%if (MonnitSession.CurrentCustomer != null)
                        { %>
                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3">
                            <%: Html.TranslateTag("Gateway/EditServerSettings|Last Check-in Date","Last Check-in Date")%>
                        </div>
                        <div class="col sensorEditFormInput">
                            <% if (Model.LastCommunicationDate != null && Model.LastCommunicationDate < DateTime.UtcNow.AddMinutes(5))
                                { %>
                            <span class="title2"><%: Model.LastCommunicationDate.OVToLocalDateShort() %> -</span>
                            <span class="title2"><%: Model.LastCommunicationDate.OVToLocalTimeShort() %></span>
                            <% } %>
                        </div>
                        <div style="clear: both;"></div>
                    </div>
                    <%} %>

                    <% if (Model.GatewayType.SupportsCustomEncryptionKey)
                        { %>
                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3">
                            <%: Html.TranslateTag("Gateway/EditServerSettings|Custom Encryption Key","Custom Encryption Key")%>
                        </div>
                        <div class="col sensorEditFormInput">

                            <%: Html.TextBoxFor(enc=>enc.CurrentEncryptionKey) %>

                            <%: Html.Label("errorKey") %>
                        </div>
                        <div style="clear: both;"></div>
                    </div>

                    <%} %>

                    <%if (Model.GatewayType.SupportsHostAddress)
                        { %>
                        <%: Html.ValidationSummary(true)%>
                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3">
                                <%: Html.TranslateTag("Gateway/EditServerSettings|Enterprise/Express Host Address","Enterprise/Express Host Address")%>
                            </div>
                            <div class="col sensorEditFormInput">

                                <%: Html.TextBoxFor(SHA => SHA.ServerHostAddress)%>

                                <%: Html.ValidationMessageFor(SHA => SHA.ServerHostAddress)%>
                            </div>
                            <div style="clear: both;"></div>
                        </div>

                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3">
                                <%: Html.TranslateTag("Gateway/EditServerSettings|Enterprise/Express Port Number","Enterprise/Express Port Number")%>
                            </div>
                            <div class="col sensorEditFormInput">

                                <%: Html.TextBoxFor(PN => PN.Port)%>

                                <%: Html.ValidationMessageFor(PN => PN.Port)%>
                            </div>
                            <div style="clear: both;"></div>
                        </div>

                        <%if (Model.GatewayType.SupportsGatewayIP)
                          {%>
                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3">
                                DHCP
                            </div>
                            <div class="col sensorEditFormInput">
                                <div class="form-check form-switch d-flex align-items-center ps-0">
                                    <label class="form-check-label"><%: Html.TranslateTag("Static","Static")%></label>
                                    <input class="form-check-input my-0 y-0 mx-2" type="checkbox" name="dhcp" id="dhcp" <%= Model.GatewayIP == "0.0.0.0" ? "checked" : "" %>>
                                    <label class="form-check-label"><%: Html.TranslateTag("Dynamic","Dynamic")%></label>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                        </div>

                        <div class="row sensorEditForm staticDHCP">
                            <div class="col-12 col-md-3">
                                <%: Html.TranslateTag("Gateway/EditServerSettings|Static IP Address","IP Address")%>
                            </div>
                            <div class="col sensorEditFormInput">

                                <%: Html.TextBox("GatewayIP", Model.GatewayIP)%>

                                <%: Html.ValidationMessageFor(SIP => SIP.GatewayIP)%>
                            </div>
                            <div style="clear: both;"></div>
                        </div>

                        <div class="row sensorEditForm staticDHCP">
                            <div class="col-12 col-md-3">
                                <%: Html.TranslateTag("Gateway/EditServerSettings|Default Gateway","Default Gateway")%>
                            </div>
                            <div class="col sensorEditFormInput">

                                <%: Html.TextBox("DefaultRouterIP", Model.DefaultRouterIP)%>

                                <%: Html.ValidationMessageFor(DG => DG.DefaultRouterIP)%>
                            </div>
                            <div style="clear: both;"></div>
                        </div>

                        <div class="row sensorEditForm staticDHCP">
                            <div class="col-12 col-md-3">
                                <%: Html.TranslateTag("Gateway/EditServerSettings|SubNet Mask","SubNet Mask")%>
                            </div>
                            <div class="col sensorEditFormInput">

                                <%: Html.TextBox("GatewaySubnet", Model.GatewaySubnet)%>

                                <%:  Html.ValidationMessageFor(SM => SM.GatewaySubnet)%>
                            </div>
                            <div style="clear: both;"></div>
                        </div>

                        <div class="row sensorEditForm staticDHCP">
                            <div class="col-12 col-md-3">
                                <%: Html.TranslateTag("Gateway/EditServerSettings|DNS Server IP","DNS Server IP")%>
                            </div>
                            <div class="col sensorEditFormInput">

                                <%: Html.TextBox("GatewayDNS", Model.GatewayDNS)%>

                                <%: Html.ValidationMessageFor(DNS => DNS.GatewayDNS)%>
                            </div>
                            <div style="clear: both;"></div>
                        </div>
                        <%} %>

                    <div class="row sensorEditForm">
                        <div class="bold col-md-5 col-sm-5 col-xs-12" style="color: red;" id="setMessage"></div>
                        <div class="bold col-md-7 col-sm-7 col-xs-12">
                            <%if (Model.GatewayType.SupportsHostAddress)
                                { %>
                            <button type="submit" value="Save" class="btn btn-primary">
                                <%: Html.TranslateTag("Gateway/EditServerSettings|Save")%>
                            </button>
                            <%} %>
                        </div>
                    </div>
                    <div class="row sensorEditForm">
                        <div class="bold col-md-5 col-sm-5 col-xs-12">
                        </div>
                        <div class="bold col-md-7 col-sm-7 col-xs-12">
                            <br />
                            <a href="#" role="button" class="btn btn-dark" id="Reset"><%: Html.TranslateTag("Gateway/EditServerSettings|Default")%></a>
                            <a href="/sethost" class="btn btn-secondary"><%: Html.TranslateTag("Gateway/EditServerSettings|Go Back")%></a>
                            <div style="clear: both;"></div>
                        </div>
                    </div>
                    <% }
                        else
                        {%>
                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3" style="color: red;">
                            <%: Html.TranslateTag("Gateway/EditServerSettings|This gateway is not supported. ")%>
                        </div>
                        <div class="col sensorEditFormInput">
                        </div>
                        <div style="clear: both;"></div>
                    </div>

                    <div class="row sensorEditForm">
                        <div class="bold col-md-5 col-sm-5 col-xs-12">
                        </div>
                        <div class="bold col-md-7 col-sm-7 col-xs-12">
                            <a href="#" role="button" class="btn btn-dark" id="Reset"><%: Html.TranslateTag("Gateway/EditServerSettings|Reset Defaults")%></a>
                            <a href="/overview" class="btn btn-default">Done</a>
                            <div style="clear: both;"></div>
                        </div>
                    </div>

                    <%}%>
                    <!-- Close Form -->
                </form>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {

            $('#setMessage').html('<%=ViewBag.setHostMessage%>');

            if ($('#dhcp').is(':checked'))
                $('.staticDHCP').hide();
            else
                $('.staticDHCP').show();

            $('#dhcp').change(function () {
                if (this.checked) {
                    $('.staticDHCP').hide();
                }
                else {
                    $('.staticDHCP').show();
                }
            });

            $('#Reset').click(function (e) {
                e.preventDefault();
                var GatewayID = <%: Model.GatewayID%>;

                if (confirm("Are you sure you want to reset this gateway to defaults?")) {
                    $.post('/Gateway/ResetDefaults/' + GatewayID, function (data) {
                        if (data == 'Success') {
                            showSimpleMessageModal("<%=Html.TranslateTag("Gateway Reset Pending")%>");
                        }
                        else {
                            showSimpleMessageModal("<%=Html.TranslateTag("Reset gateway command failed")%>");
                        }
                    });
                }
            });

            $('#GatewaySubnet').addClass('form-control');
            $('#GatewayIP').addClass('form-control');
            $('#DefaultRouterIP').addClass('form-control');
            $('#GatewayDNS').addClass('form-control');
        });
    </script>

</asp:Content>