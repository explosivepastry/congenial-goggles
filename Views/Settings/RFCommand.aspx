<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.RFCommand>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    RF Command
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% Gateway gateway = Gateway.Load(Model.GatewayID); %>

    <div class="x_panel">
        <div class="x_title">
            <h2>Select Gateway</h2>
            <div class="clearfix"></div>
        </div>

        <div class="x_content col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="col-lg-2 col-md-2 col-sm-4 col-xs-11 input-group">
                <input id="ID" type="text" class="form-control" name="LoadGateway" placeholder="<%= Model.GatewayID > 0 ? "" : "Gateway ID" %>" value="<%= Model.GatewayID > 0 ? Model.GatewayID.ToString() : "" %>" />
                <span class="input-group-addon btn btn-primary" onclick="loadGateway();"><i title="Search"></i><%:Html.GetThemedSVG("search") %></span>
            </div>
            <div class="clearfix"></div>
        </div>
    </div>

    <%if (Model.GatewayID > 0)
        { %>
    <div class="x_panel">
        <form method="post">
            <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
            <div class="row" style="margin-left: 5px;">
                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                    <div class="x_content col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="bold col-lg-2 col-md-2 col-sm-4 col-xs-12">
                            Gateway ID
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-4 col-xs-12">
                            <input name="GatewayIDDisplay" type="text" disabled value="<%= Model.GatewayID > 0 ? Model.GatewayID.ToString() : "Gateway ID" %>" />
                            <input type="hidden" name="GatewayID" value="<%:Model.GatewayID %>" />
                        </div>
                    </div>

                    <div class="x_content col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="bold col-lg-2 col-md-2 col-sm-4 col-xs-12">
                            Device ID
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-4 col-xs-12">
                            <input name="DeviceID" type="text" value="<%= Model.GatewayID > 0 ? Model.GatewayID.ToString() : "Device ID" %>" />
                        </div>
                    </div>
                </div>
            </div>

            <br>

            <div class="row" style="margin-left: 5px;">
                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">

                    <div class="x_title">
                        <h2>RF Command Generator (<1 GHz)</h2>
                        <div class="clearfix"></div>
                    </div>

                    <div class="x_content col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="bold col-lg-2 col-md-2 col-sm-4 col-xs-12">
                            Test Mode
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-4 col-xs-12" style="width:200px;">
                            <select class="form-select" name="TestMode" id="TestMode">
                                <option value="0" <%:Model.TestMode == 0 ? "selected" : "" %>>Sleep</option>
                                <option value="1" <%:Model.TestMode == 1 ? "selected" : "" %>>Receive single channel</option>
                                <option value="2" <%:Model.TestMode == 2 ? "selected" : "" %>>Transmit single channel</option>
                                <option value="10" <%:Model.TestMode == 10 ? "selected" : "" %>>Transmit linear sweep</option>
                                <option value="11" <%:Model.TestMode == 11 ? "selected" : "" %>>Transmit random sweep</option>
                            </select>
                        </div>
                    </div>

                    <div class="x_content col-lg-12 col-md-12 col-sm-12 col-xs-12 HideMode HideMode0">
                        <div class="bold col-lg-2 col-md-2 col-sm-4 col-xs-12">
                            Modulation Format
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-4 col-xs-12" style="width:200px;">
                            <select class="form-select" name="Modulation" id="Modulation">
                                <option value="0" <%:Model.Modulation == 0 ? "selected" : "" %> class="HideMode HideMode1 HideMode10 HideMode11">CW continuous</option>
                                <option value="1" <%:Model.Modulation == 1 ? "selected" : "" %> class="HideMode HideMode1 HideMode10 HideMode11">ALTA-LSM continuous</option>
                                <option value="2" <%:Model.Modulation == 2 ? "selected" : "" %>>ALTA-LSM packet</option>
                                <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin || MonnitSession.CustomerCan("RFCommandHSM"))
                                    { %>
                                <option value="3" <%:Model.Modulation == 3 ? "selected" : "" %> class="HideMode HideMode1 HideMode10 HideMode11">ALTA-HSM continuous</option>
                                <option value="4" <%:Model.Modulation == 4 ? "selected" : "" %>>ALTA-HSM packet</option>
                                <%} %>
                            </select>
                        </div>
                    </div>

                    <div id="divFrequency" class="x_content col-lg-12 col-md-12 col-sm-12 col-xs-12 HideMode HideMode0" style="<%: Model.TestMode == 0 ? "display:none;" : "" %>">
                        <div class="bold col-lg-2 col-md-2 col-sm-4 col-xs-12">
                            Frequency (MHz)
   
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-4 col-xs-12" style="width:200px;">
                            <input name="Frequency" type="text" value="<%:Model.Frequency.ToString("###.000") %>" />
                        </div>
                    </div>

                    <div class="x_content col-lg-12 col-md-12 col-sm-12 col-xs-12 HideMode HideMode0 HideMode1">
                        <div class="bold col-lg-2 col-md-2 col-sm-4 col-xs-12">
                            Power Level
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-4 col-xs-12" style="width:200px;">
                            <select class="form-select" name="PowerSelector" id="PowerSelector">
                                <option value="0000" <%:Model.Power == "0000" ? "selected" : "" %>>Max</option>
                                <option value="0001" <%:Model.Power == "0001" ? "selected" : "" %>>High</option>
                                <option value="0002" <%:Model.Power == "0002" ? "selected" : "" %>>Medium</option>
                                <option value="0003" <%:Model.Power == "0003" ? "selected" : "" %>>Low</option>
                                <option value="-1" <%:(Model.Power != "0000" && Model.Power != "0001" && Model.Power != "0002" && Model.Power != "0003") ? "selected" : "" %>>Custom</option>
                            </select>

                            <div class="HidePower0000 HidePower0001 HidePower0002 HidePower0003 ShowPower-1" style="padding-top: 10px; width:200px;">
                                0x
                    <input id="Power" name="Power" type="text" value="<%:Model.Power %>" style="width: 200px; display: inline;" />
                            </div>
                        </div>
                    </div>
                </div>

                <%--    Gen 4 (2.4 GHz)--%>
                <%if (gateway.GenerationType == "Gen4")
                    { %>

                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">

                    <div class="x_title">
                        <h2>RF Command Generator (2.4 GHz)</h2>
                        <div class="clearfix"></div>
                    </div>


                    <div class="x_content col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="bold col-lg-2 col-md-2 col-sm-4 col-xs-12">
                            Test Mode
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-4 col-xs-12" style="width:200px;">
                            <select class="form-select" name="TestMode24" id="TestMode24">
                                <option value="0" <%:Model.TestMode24 == 0 ? "selected" : "" %>>Sleep</option>
                                <option value="1" <%:Model.TestMode24 == 1 ? "selected" : "" %>>Receive single channel</option>
                                <option value="2" <%:Model.TestMode24 == 2 ? "selected" : "" %>>Transmit single channel</option>
                                <option value="10" <%:Model.TestMode24 == 10 ? "selected" : "" %>>Transmit linear sweep</option>
                                <option value="11" <%:Model.TestMode24 == 11 ? "selected" : "" %>>Transmit random sweep</option>
                            </select>
                        </div>
                    </div>

                    <div class="x_content col-lg-12 col-md-12 col-sm-12 col-xs-12 HideMode24 HideMode24-0">
                        <div class="bold col-lg-2 col-md-2 col-sm-4 col-xs-12">
                            Modulation Format
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-4 col-xs-12" style="width:200px;">
                            <select class="form-select" name="Modulation24" id="Modulation24">
                                <option value="0" <%:Model.Modulation24 == 0 ? "selected" : "" %> class="HideMode24 HideMode24-1 HideMode24-10 HideMode24-11">BLE CW continuous</option>
                                <option value="1" <%:Model.Modulation24 == 1 ? "selected" : "" %> class="HideMode24">BLE 1M Packet</option>
                                <option value="5" <%:Model.Modulation24 == 5 ? "selected" : "" %> class="HideMode24 HideMode24-1 HideMode24-10 HideMode24-11">BLE 1M Continuous</option>
                                <option value="2" <%:Model.Modulation24 == 2 ? "selected" : "" %> class="HideMode24">BLE 2M Packet</option>
                                <option value="6" <%:Model.Modulation24 == 6 ? "selected" : "" %> class="HideMode24 HideMode24-1 HideMode24-10 HideMode24-11">BLE 2M Continuous</option>
                                <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin || MonnitSession.CustomerCan("RFCommandHSM"))
                                    { %>
                                <option value="3" <%:Model.Modulation24 == 3 ? "selected" : "" %> class="HideMode24">BLE 500k Packet</option>
                                <option value="7" <%:Model.Modulation24 == 7 ? "selected" : "" %> class="HideMode24 HideMode24-1 HideMode24-10 HideMode24-11">BLE 500k Continuous</option>
                                <option value="4" <%:Model.Modulation24 == 4 ? "selected" : "" %> class="HideMode24">BLE 125k Packet</option>
                                <option value="8" <%:Model.Modulation24 == 8 ? "selected" : "" %> class="HideMode24 HideMode24-1 HideMode24-10 HideMode24-11">BLE 125k Continuous</option>
                                <%} %>
                            </select>
                        </div>
                    </div>

                    <div id="divFrequency24" class="x_content col-lg-12 col-md-12 col-sm-12 col-xs-12 HideMode24 HideMode24-0" style="<%: Model.TestMode == 0 ? "display:none;" : "" %>">
                        <div class="bold col-lg-2 col-md-2 col-sm-4 col-xs-12">
                            Frequency (MHz)
   
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-4 col-xs-12" style="width:200px;">
                            <input name="Frequency24" type="text" value="<%:Model.Frequency24.ToString("####.000") %>" />
                        </div>
                    </div>

                    <div class="x_content col-lg-12 col-md-12 col-sm-12 col-xs-12 HideMode24 HideMode24-0 HideMode24-1">
                        <div class="bold col-lg-2 col-md-2 col-sm-4 col-xs-12">
                            Power Level
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-4 col-xs-12">
                            <select class="form-select" name="PowerSelector24" id="PowerSelector24" style="width:200px;">
                                <option value="0000" <%:Model.Power24 == "0000" ? "selected" : "" %>>Max</option>
                                <option value="0001" <%:Model.Power24 == "0001" ? "selected" : "" %>>High</option>
                                <option value="0002" <%:Model.Power24 == "0002" ? "selected" : "" %>>Medium</option>
                                <option value="0003" <%:Model.Power24 == "0003" ? "selected" : "" %>>Low</option>
                                <option value="-1" <%:(Model.Power24 != "0000" && Model.Power24 != "0001" && Model.Power24 != "0002" && Model.Power24 != "0003") ? "selected" : "" %>>Custom</option>
                            </select>

                            <div class="HidePower0000 HidePower0001 HidePower0002 HidePower0003 ShowPower24-1" style="padding-top: 10px;">
                                0x
                    <input id="Power24" name="Power24" type="text" value="<%:Model.Power24 %>" style="width: 200px; display: inline;" />
                            </div>
                        </div>
                    </div>
                </div>
                <%}%>
            </div>
            <div class="clearfix"></div>
            <hr />
            <div class="formtitle col-lg-2 col-md-2 col-sm-4 col-xs-11 input-group">
                <input type="submit" value="Save" class="btn btn-primary" style="width: 100px;" />
                <div style="clear: both;"></div>
            </div>
            <div class="col-lg-2 col-md-2 col-sm-4 col-xs-11 input-group">
                <%:Model.ErrorMsg %>
            </div>

        </form>
    </div>
    <%} %>

    
    <%
        //IEnumerable<GatewayAttribute> list = GatewayAttribute.LoadRFCommands().Where(ta => ta.GatewayID == Model.GatewayID);
        //if (list.Count() > 0)
        //{%>
    <%--<div class="x_panel">
        <div class="x_title">
            <h2>Pending commands to send through this gateway<a href="javascript:window.location.href = window.location.href;">
                <img src="../../Content/images/iconmonstr-refresh-3-240 (1).png" style="height: 20px; margin: 0px; margin-left: 13px;"></a>
            </h2>
            <div class="clearfix"></div>
        </div>--%>

        <%
            /**
            foreach (GatewayAttribute att in GatewayAttribute.LoadByGatewayID(Model.GatewayID).Where(g => g.Name == "RFCommand"))
            {
                var Obj = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<RFCommand>(att.Value);

                string Mode = "";
                switch (Obj.TestMode)
                {
                    case 0:
                        Mode = "Sleep";
                        break;
                    case 1:
                        Mode = "Receive single channel";
                        break;
                    case 2:
                        Mode = "Transmit single channel";
                        break;
                    case 10:
                        Mode = "Transmit continuous linear sweep of all channels";
                        break;
                    case 11:
                        Mode = "Transmit continuous random sweep of all channels";
                        break;
                }

                string Modulation = "";
                switch (Obj.Modulation)
                {
                    case 0:
                        Modulation = "CW continuous";
                        break;
                    case 1:
                        Modulation = "ALTA continuous";
                        break;
                    case 2:
                        Modulation = "ALTA packet";
                        break;
                    case 3:
                        Modulation = "HSM continuous";
                        break;
                    case 4:
                        Modulation = "HSM packet";
                        break;
                }

                string Power = Obj.Power;
                switch (Obj.Power)
                {
                    case "0000":
                        Power = "Max";
                        break;
                    case "0001":
                        Power = "High";
                        break;
                    case "0002":
                        Power = "Meduim";
                        break;
                    case "0003":
                        Power = "Low";
                        break;
                    default:
                        Power = "0x" + Power;
                        break;
                }

                //GHz 2.4
                string Mode24 = "";
                switch (Obj.TestMode24)
                {
                    case 0:
                        Mode24 = "Sleep";
                        break;
                    case 1:
                        Mode24 = "Receive single channel";
                        break;
                    case 2:
                        Mode24 = "Transmit single channel";
                        break;
                    case 10:
                        Mode24 = "Transmit continuous linear sweep of all channels";
                        break;
                    case 11:
                        Mode24 = "Transmit continuous random sweep of all channels";
                        break;
                }

                string Modulation24 = "";
                switch (Obj.Modulation24)
                {
                    case 0:
                        Modulation24 = "CW continuous";
                        break;
                    case 1:
                        Modulation24 = "ALTA continuous";
                        break;
                    case 2:
                        Modulation24 = "ALTA packet";
                        break;
                    case 3:
                        Modulation24 = "HSM continuous";
                        break;
                    case 4:
                        Modulation24 = "HSM packet";
                        break;
                }

                string Power24 = Obj.Power24;
                switch (Obj.Power24)
                {
                    case "0000":
                        Power24 = "Max";
                        break;
                    case "0001":
                        Power24 = "High";
                        break;
                    case "0002":
                        Power24 = "Meduim";
                        break;
                    case "0003":
                        Power24 = "Low";
                        break;
                    default:
                        Power24 = "0x" + Power24;
                        break;
                }
            
}
            **/
            %>
    <%--</div>--%>
    <%/**}%**/%>

    <%--<div style="clear: both;"></div>--%>

    <script type="text/javascript">


        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';

        $('select').addClass("form-control");
        $("input").addClass("form-control");
        $(":checkbox").removeClass("form-control");



        $(function () {
            $('#ID').bind('keypress', function (e) {
                var code = e.keyCode || e.which;
                if (code == 13) { //Enter keycode
                    loadGateway();
                }
            }).focus();

            $('#TestMode').change(function () {

                $('.HideMode').show();
                $('.HideMode' + $('#TestMode').val()).hide();
                $('#Modulation option').each(function () {
                    if ($(this).css('display') != 'none') {
                        $(this).prop("selected", true);
                        return false;
                    }
                });

                if ($('#TestMode').val() == '0' || $('#TestMode').val() == '10' || $('#TestMode').val() == '11') {
                    $('#divFrequency').hide();
                } else {
                    $('#divFrequency').show();
                }
            });



            $('.HideMode' + $('#TestMode').val()).hide();
            $('#PowerSelector').change(function () {
                $('.ShowPower' + $('#PowerSelector').val()).show();//
                $('.HidePower' + $('#PowerSelector').val()).hide();

                //AssignValue to textbox
                var powerString = $('#PowerSelector').val();
                if (powerString == "-1") powerString = "0000"
                $('#Power').val(powerString);
            });
            $('.HidePower' + $('#PowerSelector').val()).hide();
        });


        //Gen 4 javascript
        $(function () {

            $('#TestMode24').change(function () {
                $('.HideMode24').show();
                $('.HideMode24-' + $('#TestMode24').val()).hide();

                $('#Modulation24 option').each(function () {
                    if ($(this).css('display') != 'none') {
                        $(this).prop("selected", true);
                        return false;
                    }
                });

                if ($('#TestMode24').val() == '0' || $('#TestMode24').val() == '10' || $('#TestMode24').val() == '11') {
                    $('#divFrequency24').hide();
                } else {
                    $('#divFrequency24').show();
                }
            });

            $('.HideMode24-' + $('#TestMode24').val()).hide();

            $('#PowerSelector24').change(function () {
                $('.ShowPower24' + $('#PowerSelector24').val()).show();
                $('.HidePower24' + $('#PowerSelector24').val()).hide();

                var powerString = $('#PowerSelector24').val();
                if (powerString == "-1") powerString = "0000";
                $('#Power24').val(powerString);
            });

            $('.HidePower24' + $('#PowerSelector24').val()).hide();
        });

        function loadGateway() {
            window.location.href = '/Settings/RFCommand/' + $('#ID').val(); // + '?gatewayId=' + $('#ID').val();
        }

    </script>

</asp:Content>
