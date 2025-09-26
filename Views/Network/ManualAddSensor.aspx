<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Sensor>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ManualSensorAdd
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#SensorTypeID').change(setSensorType);
            setSensorType();
        });
        function setSensorType() {
            if ($('#SensorTypeID').val() == "4") {
                $('#divWiFi').show();
                $('#divStandard').hide();
                $('#RadioBand').val("WIFI");
            }
            else {
                $('#divStandard').show();
                $('#divWiFi').hide();
            }
        }
    </script>
    <% SelectList NetworkList = new SelectList(Monnit.CSNet.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).OrderBy(c => c.Name), "CSNetID", "Name", Model.CSNetID);%>
    <div class="container-fluid">
        <div class="card_container shadow-sm rounded mt-4">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <%: Html.TranslateTag("Network/ManualAddSensor|Add Sensor","Add Sensor")%>
                </div>
            </div>
            <div class="card_container__body">
                <div class="card_container__body__content">
                    <form action="/Network/ManualAddSensor" method="post">
                        <div class="row section">
                            <div class="col-12 col-sm-8 col-md-5 col-lg-4 px-3">
                            <div class="form-group">
                                <label class="aSettings__title" for="networkSelect">
                                    <%: Html.TranslateTag("Network/ManualAddSensor|Network","Network")%>
                                </label>
                                <%: Html.DropDownList("CSNetID", NetworkList, null, new {@class="form-select", @required="required"})%>
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <label class="aSettings__title" for="ApplicationID">
                                    <%: Html.TranslateTag("Network/ManualAddSensor|Sensor Profile","Sensor Profile")%>
                                </label>
                                <%: Html.DropDownList("ApplicationID", (SelectList)ViewData["Applications"], "Select One", new {@class="form-select", @required="required"}) %>
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <label class="aSettings__title" for="SensorID">
                                    <%: Html.TranslateTag("Network/ManualAddSensor|SensorID","SensorID")%>
                                </label>
                                <input type="text" id="sensorID" name="sensorID" class="form-control" required="required" placeholder="<%: Html.TranslateTag("Network/AssignDevice|ID Number","ID Number")%>">
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <label class="aSettings__title" for="SensorID">
                                    <%: Html.TranslateTag("Network/ManualAddSensor|Sensor Name","Sensor Name")%>
                                </label>
                                <input type="text" id="sensorName" name="sensorName" class="form-control" required="required" placeholder="<%: Html.TranslateTag("Network/AssignDevice|Sensor Name","Sensor Name")%>">
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <label class="aSettings__title" for="ApplicationID">
                                    <%: Html.TranslateTag("Network/ManualAddSensor|Sensor Type","Sensor Type")%>
                                </label>
                                <%: Html.DropDownList("SensorTypeID", (SelectList)ViewData["SensorType"], null, new {@class="form-select", @required="required"})%>
                                <div class="clearfix"></div>
                            </div>
                            <%: Html.HiddenFor(model => model.ReportInterval)%>
                            <%: Html.HiddenFor(model => model.ActiveStateInterval)%>
                            <%: Html.HiddenFor(model => model.MinimumCommunicationFrequency)%>
                            <div class="form-group">
                                <label class="aSettings__title" for="GenerationType">
                                    <%: Html.TranslateTag("Network/ManualAddSensor|Generation Type","Generation Type")%>
                                </label>
                                <select class="form-select" id="GenerationType" name="GenerationType">
                                    <option value="Gen1">Generation 1</option>
                                    <option value="Gen2">Alta</option>
                                </select>
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <label class="aSettings__title" for="FirmwareVersion">
                                    <%: Html.TranslateTag("Network/ManualAddSensor|Firmware Version","Firmware Version")%>
                                </label>
                                <input type="text" id="FirmwareVersion" name="FirmwareVersion" class="form-control" required="required" value="<%:Model.FirmwareVersion %>">
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <label class="aSettings__title" for="PowerSourceID">
                                    <%: Html.TranslateTag("Network/ManualAddSensor|Power Source","Power Source")%>
                                </label>
                                <%: Html.DropDownList("PowerSourceID", (SelectList)ViewData["PowerSource"], null, new {@class="form-select", @required="required"})%>
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <label class="aSettings__title" for="RadioBand">
                                    <%: Html.TranslateTag("Network/ManualAddSensor|RadioBand","RadioBand")%>
                                </label>
                                <%: Html.DropDownList("RadioBand", (SelectList)ViewData["RadioBand"], null, new {@class="form-select", @required="required"})%>
                                <div class="clearfix"></div>
                            </div>
                            <div id="divWiFi" style="display: none;">
                                <div class="form-group">
                                    <label class="aSettings__title" for="GatewayID">
                                        <%: Html.TranslateTag("Network/ManualAddSensor|GatewayID","GatewayID")%>
                                    </label>
                                    <input type="text" id="GatewayID" name="GatewayID" class="form-control" placeholder="<%: Html.TranslateTag("Network/ManualAddSensor|GatewayID","GatewayID")%>">
                                    <div class="clearfix"></div>
                                </div>
                                <br />
                                <div class="form-group">
                                    <label class="aSettings__title" for="MacAddress">
                                        <%: Html.TranslateTag("Network/ManualAddSensor|MacAddress","MacAddress")%>
                                    </label>
                                    <input type="text" id="MacAddress" name="MacAddress" class="form-control" placeholder="<%: Html.TranslateTag("Network/ManualAddSensor|MacAddress","MacAddress")%>">
                                    <div class="clearfix"></div>
                                </div>
                                <br />
                                <div class="form-group">
                                    <label class="aSettings__title" for="GatewayFirmwareVersion">
                                        <%: Html.TranslateTag("Network/ManualAddSensor|Gateway Firmware Version","Gateway Firmware Version")%>
                                    </label>
                                    <input type="text" id="GatewayFirmwareVersion" name="GatewayFirmwareVersion" class="form-control" placeholder="<%: Html.TranslateTag("Network/ManualAddSensor|GatewayFirmwareVersion","GatewayFirmwareVersion")%>">
                                    <div class="clearfix"></div>
                                </div>
                                <br />
                            </div>
                            <input class="aSettings__input_input" type="text" name="AccountID" value="<%: Model.AccountID %>" hidden>
                            </div>
                        </div>
                        <div class="form-group text-end">
                            <button type="submit" id="SubmitBtn" class="btn btn-primary">
                                <%: Html.TranslateTag("Network/ManualAddSensor|Add Sensor", "Add Sensor")%>
                            </button>
                        </div>
                    </form>
                </div>
            </div>
            </div>
        </div>
        <div class="row section">
            <% if (ViewData["Result"] != null)
                { %>
            <div>
                <% if (!ViewData["Result"].ToString().Contains("Error:"))
                    {%>
                <font color="green"><%= ViewData["Result"]%> </font>
                <%}
                else
                {%>
                <font color="red"><%= ViewData["Result"]%> </font>
                <%} %>
            </div>
            <%} %>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#networkSelect').change(function (e) {
                e.preventDefault();
                csnetID = $("#networkSelect").val();
            });
        });
    </script>
</asp:Content>
