<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Gateway>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ManualAddGateway
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <% 
        
        //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        Response.Cache.SetValidUntilExpires(false);
        Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
    %>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#GatewayID').focus().select();
            setTimeout("if(document.activeElement != $('#GatewayID')[0]) {$('#GatewayID').focus().select();}", 1000);
            setTimeout("if(document.activeElement != $('#GatewayID')[0]) {$('#GatewayID').focus().select();}", 2000);
        });
    </script>

    <% SelectList NetworkList = new SelectList(Monnit.CSNet.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).OrderBy(c => c.Name), "CSNetID", "Name", Model.CSNetID);%>
    <div class="container-fluid">
        <div class="card_container shadow-sm rounded mt-4">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <%: Html.TranslateTag("Network/ManualAddGateway|Add Gateway","Add Gateway")%>
                </div>
            </div>
            <div class="card_container__body">
                <div class="card_container__body__content">
                    <form action="/Network/ManualAddGateway" class="d-flex flex-column" method="post">
                        <div class="col-12 col-sm-8 col-md-5 col-lg-4">
                            <div class="row section">
                                <%-- NetWork Select --%>
                                <div class="form-group">
                                    <label class="aSettings__title" for="CSNetID">
                                        <%: Html.TranslateTag("Network/ManualAddGateway|Network","Network")%>
                                    </label>
                                    <%: Html.DropDownList("CSNetID", NetworkList, null, new {@class="form-select" })%>
                                    <div class="clearfix"></div>
                                </div>
                                <br />

                                <%-- GatewayID --%>
                                <div class="form-group">
                                    <label class="aSettings__title" for="SensorID">
                                        <%: Html.TranslateTag("Network/ManualAddGateway|GatewayID","GatewayID")%>
                                    </label>
                                    <input type="text" id="GatewayID" name="GatewayID" class="form-control" required="required" placeholder="<%: Html.TranslateTag("Network/AssignDevice|ID Number","ID Number")%>">
                                    <div class="clearfix"></div>
                                </div>
                                <br />

                                <%-- Gateway Name --%>
                                <div class="form-group">
                                    <label class="aSettings__title" for="SensorID">
                                        <%: Html.TranslateTag("Network/ManualAddGateway|Gateway Name","Gateway Name")%>
                                    </label>
                                    <input type="text" id="Name" name="Name" class="form-control" required="required" placeholder="<%: Html.TranslateTag("Network/AssignDevice|Gateway Name","Gateway Name")%>">
                                    <div class="clearfix"></div>
                                </div>
                                <br />

                                <%-- Gateway Type --%>
                                <% SelectList GatewayTypes = new SelectList(Monnit.GatewayType.LoadAll(), "GatewayTypeID", "Name", Model.GatewayTypeID);%>

                                <div class="form-group">
                                    <label class="aSettings__title" for="GatewayTypeID">
                                        <%: Html.TranslateTag("Network/ManualAddGateway|Gateway Type","Gateway Type")%>
                                    </label>
                                    <%: Html.DropDownList("GatewayTypeID", GatewayTypes, "Select One", new {@class="form-select", @required="required"})%>
                                    <div class="clearfix"></div>
                                </div>
                                <br />

                                <%-- Generation Type --%>
                                <div class="form-group">
                                    <label class="aSettings__title" for="GenerationType">
                                        <%: Html.TranslateTag("Network/ManualAddGateway|Generation Type","Generation Type")%>
                                    </label>
                                    <select class="form-select" id="GenerationType" required="required" name="GenerationType">
                                        <option value="Gen1">Generation 1</option>
                                        <option value="Gen2">Alta</option>
                                    </select>
                                    <div class="clearfix"></div>
                                </div>
                                <br />

                                <%-- RadioBand --%>
                                <div class="form-group">
                                    <label class="aSettings__title" for="RadioBand">
                                        <%: Html.TranslateTag("Network/ManualAddGateway|RadioBand","RadioBand")%>
                                    </label>
                                    <%: Html.DropDownList("RadioBand", "form-select", string.IsNullOrEmpty(Model.RadioBand) ? eRadioBand._900_MHz : (eRadioBand)Enum.Parse(typeof(eRadioBand), "_" + Model.RadioBand.Replace(" ", "_")))%>
                                    <div class="clearfix"></div>
                                </div>
                                <br />

                                <%-- APN Firmware Version --%>
                                <div class="form-group">
                                    <label class="aSettings__title" for="APNFirmwareVersion">
                                        <%: Html.TranslateTag("Network/ManualAddGateway|APN Firmware Version","APN Firmware Version")%>
                                    </label>
                                    <input type="text" id="APNFirmwareVersion" name="APNFirmwareVersion" class="form-control" value="<%: Model.APNFirmwareVersion %>">
                                    <div class="clearfix"></div>
                                </div>
                                <br />

                                <%-- Gateway Firmware Version --%>
                                <div class="form-group">
                                    <label class="aSettings__title" for="GatewayFirmwareVersion">
                                        <%: Html.TranslateTag("Network/ManualAddGateway|Gateway Firmware Version","Gateway Firmware Version")%>
                                    </label>
                                    <input type="text" id="GatewayFirmwareVersion" name="GatewayFirmwareVersion" class="form-control" value="<%: Model.GatewayFirmwareVersion %>" title="<%: Html.TranslateTag("Network/ManualAddGateway|If the firmware version is unknown, leave the field blank to use the latest Firmware as default.","If the firmware version is unknown, leave the field blank to use the latest Firmware as default.")%>">
                                    <div class="clearfix"></div>
                                </div>
                                <br />

                                <%-- Power Source --%>
                                <div class="form-group">
                                    <label class="aSettings__title" for="PowerSourceID">
                                        <%: Html.TranslateTag("Network/ManualAddGateway|Power Source","Power Source")%>
                                    </label>
                                    <%: Html.DropDownList("PowerSourceID", new SelectList(PowerSource.LoadAll(), "PowerSourceID", "Name"), null, new {@class="form-select", @required="required"})%>
                                    <div class="clearfix"></div>
                                </div>


                                <%-- MacAddress --%>
                                <div class="form-group">
                                    <label class="aSettings__title" for="MacAddress">
                                        <%: Html.TranslateTag("Network/ManualAddGateway|MacAddress","MacAddress")%>
                                    </label>
                                    <input type="text" id="MacAddress" name="MacAddress" class="form-control" placeholder="<%: Html.TranslateTag("Network/ManualAddGateway|MacAddress","MacAddress")%>">
                                    <div class="clearfix"></div>
                                </div>
                                <br />

                                <%-- Save Button --%>
                            </div>
                            <!-- End of Row -->
                        </div>
                        <div class="form-group text-end col-12">
                            <button type="submit" id="SubmitBtn" class="btn btn-primary">
                                <%: Html.TranslateTag("Network/ManualAddGateway|Add Gateway","Add Gateway")%>
                            </button>
                            <div class="clearfix"></div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <!-- End of Row -->


    <%-- Results --%>
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


    <script type="text/javascript">

        $(document).ready(function () {

            $('#CSNetID').change(function (e) {
                e.preventDefault();
                csnetID = $("#CSNetID").val();
            });


        });

    </script>


</asp:Content>
