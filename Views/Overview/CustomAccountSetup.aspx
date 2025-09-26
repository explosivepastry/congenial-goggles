<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<CustomAccountSetupModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Custom Account Setup
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% Account userAccount = MonnitSession.CurrentCustomer.Account; %>

    <style type="text/css">
        .goodBox {
            border-color: lightgreen;
        }
    </style>

    <div class="container-fluid mt-4">
        <div class="dfac" style="margin-left: 10px;">
            <svg xmlns="http://www.w3.org/2000/svg" width="23" height="16" viewBox="0 0 23 16" style="margin-right: 5px; margin-top: 7px;">
                <g id="Group_916" data-name="Group 916" transform="translate(-360 -785)">
                    <path id="ic_person_outline_24px" d="M12,5.9A2.1,2.1,0,1,1,9.9,8,2.1,2.1,0,0,1,12,5.9m0,9c2.97,0,6.1,1.46,6.1,2.1v1.1H5.9V17c0-.64,3.13-2.1,6.1-2.1M12,4a4,4,0,1,0,4,4A4,4,0,0,0,12,4Zm0,9c-2.67,0-8,1.34-8,4v3H20V17C20,14.34,14.67,13,12,13Z" transform="translate(363 781)" style="fill: #0067ab;" />
                    <path id="ic_person_add_24px" d="M6,10V7H4v3H1v2H4v3H6V12H9V10Z" transform="translate(359 782)" style="fill: #0067ab;" />
                </g>
            </svg>
            <h2 style="margin-bottom: 0px;"><%: Html.TranslateTag("Overview/CustomAccountSetup|Custom Account Setup","Custom Account Setup")%></h2>
        </div>

        <div class="col-12">
            <div class="x_panel shadow-sm rounded gridPanel powertourhook" id="hook-one" style="margin-left: 5px;">
                <div class="x_title dfac" style="margin-bottom: 0px;">
                        <h2 style="font-weight: bold; color: #707070;"><%: Html.TranslateTag("Create Account","Create Account")%></h2>
                    <div class="nav navbar-right panel_toolbox">
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content" style="padding-left: 20px; background-color: white;">

                    <form action="/Overview/CustomAccountSetup<%= ViewContext.RouteData.Values["id"] != null ? "?id=" + ViewContext.RouteData.Values["id"] : "" %>" id="accountForm" method="post">
                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3">
                                <strong><%: Html.TranslateTag("Division","Division")%>:</strong>
                            </div>

                            <div class="col sensorEditFormInput">
                                <div>
                                    <input id="accountNumber" value="<%=userAccount.AccountNumber %>" hidden />
                                    <input id="accountID" value="<%=userAccount.AccountID %>" hidden />
                                    <input id="hrefDistrictID" value="<%=ViewContext.RouteData.Values["id"] %>" hidden />

                                    <select id="divisionID" name="DivisionAccountID" class="form-select">
                                        <option><%: Html.TranslateTag("Overview/CustomAccountSetup|Select a Division","Select a Division")%></option>
                                        <%//foreach (Account account in Account.LoadResellers(MonnitSession.CurrentCustomer.CustomerID, 100).Where(s => s.AccountID != MonnitSession.CurrentCustomer.AccountID))
                                            foreach (Account account in Account.LoadByRetailAccountID(MonnitSession.CurrentTheme.AccountID))//canteen corp ID 11069
                                            { %>
                                        <%if (MonnitSession.CurrentCustomer.CanSeeAccount(account) || userAccount.AccountIDTree.Contains('*' + account.AccountID.ToString() + '*')) //Only shows the district user is under
                                            {%>
                                        <option <%=Model.DivisionAccountID == account.AccountID ? "selected='selected'" : "" %> value="<%=account.AccountID %>"><%=account.AccountNumber %></option>
                                        <%}
                                            }%>
                                    </select>
                                    <%: Html.ValidationMessageFor(model => model.DivisionAccountID, null, new { @style = "color:red" })%>
                                </div>
                            </div>
                        </div>

                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3">
                                <strong>
                                    <%: Html.TranslateTag("District","District")%>:
                                </strong>
                            </div>

                            <div class="col sensorEditFormInput">
                                <select required="required" name="DistrictAccountID" id="districtID" class="form-select">
                                    <option><%: Html.TranslateTag("Overview/CustomAccountSetup|Select a District","Select a District")%></option>
                                </select>
                                <%: Html.ValidationMessageFor(model => model.DistrictAccountID, null, new { @style = "color:red" })%>
                            </div>
                        </div>

                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3">
                                <strong><%: Html.TranslateTag("Market Name","Market Name")%>:</strong>
                            </div>

                            <div class="col sensorEditFormInput">
                                <input class="form-control" id="accountName" name="AccountName" required="required" type="text" value="<%=Model.AccountName %>" />
                                <span id="companynameError" class="help-block"><%: Html.ValidationMessageFor(model => model.AccountName, null, new { @style = "color:red" })%></span>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="bold" style="margin-top: 4px; font-weight: bold;">
                                <div class="form-group" style="font-size: 15px;">
                                    <span class="card_container__top__title" style="font-weight: bold;">
                                        <%: Html.TranslateTag("Gateway","Gateway")%>:
                                    </span>
                                </div>
                            </div>
                        </div>

                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3">
                                <strong><%: Html.TranslateTag("Overview/CustomAccountSetup|ID Number","ID Number")%>:</strong>
                            </div>

                            <div class="col sensorEditFormInput">
                                <input value="<%=Model.GatewayID%>" name="gatewayID" type="text" id="gatewayID" required="required" placeholder="<%: Html.TranslateTag("Network/AssignDevice|ID Number","ID Number")%>" class="form-control">
                                <%: Html.ValidationMessageFor(model => model.GatewayID, null, new { @style = "color:red" })%>
                            </div>
                        </div>

                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3">
                                <strong><%: Html.TranslateTag("Overview/CustomAccountSetup|Security Code","Security Code")%>:</strong>
                            </div>

                            <div class="col sensorEditFormInput">
                                <input value="<%=Model.GatewayCode%>" name="gatewayCode" type="text" id="gatewayCode" placeholder="<%: Html.TranslateTag("Network/AssignDevice|Security Code","Security Code")%>" class="form-control">
                                <%: Html.ValidationMessageFor(model => model.GatewayCode, null, new { @style = "color:red" })%>
                            </div>
                        </div>

                        <div class="form-group row" style="margin-bottom: 20px;">
                            <div class="bold" style="margin-top: 4px; font-weight: bold;">
                                <div class="form-group" style="font-size: 15px;">
                                    <span class="card_container__top__title" style="font-weight: bold;">
                                        <%: Html.TranslateTag("Sensors","Sensors")%>: 
                                    </span>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="form-group card_container__mini mx-auto mt-3" style="display: flex; flex-wrap: wrap; justify-content: space-between;">
                                    <div class="card_container__mini__wrapper shadow rounded">
                                        <%=Html.GetThemedSVG("sensor") %>
                                        <div class="dg3 d-flex flex-column" style="width: 100%;">
                                            <div style="font-weight: bold;">
                                                <%: Html.TranslateTag("Overview/CustomAccountSetup|Sensor Type","SensorType")%>:
                                            </div>

                                            <div class="dfjcac input-group mb-0">
                                                <select name="SensorType1" id="sensorType1" class="form-select" style="width: 100px;">
                                                    <option <%=Model.SensorName1 != null && Model.SensorName1.Contains("Cooler") ? "selected='selected'" : "" %> value="Cooler"><%: Html.TranslateTag("Cooler","Cooler")%></option>
                                                    <option <%=Model.SensorName1 != null && Model.SensorName1.Contains("Freezer") ? "selected='selected'" : "" %> value="Freezer"><%: Html.TranslateTag("Freezer","Freezer")%></option>
                                                </select>
                                                <select name="SensorLocation1" id="sensorLocation1" onchange="loadDefaultTypeLocation()" class="form-select" title="<%: Html.TranslateTag("Location","Location")%>">
                                                    <% for (int i = 1; i < 11; i++)
                                                        { %>
                                                    <option <%=Model.SensorName1 != null && Model.SensorName1.Contains("_" + i) ? "selected='selected'" : "" %> value="_<%=i %>"><%=i %></option>
                                                    <% } %>
                                                </select>
                                            </div>
                                        </div>
                                        <br />

                                        <div style="display: flex; justify-content: flex-start; align-items: center; width: 100%; font-weight: bold;">
                                        <%: Html.TranslateTag("Overview/CustomAccountSetup|ID Number","ID Number")%>:
                                        </div>

                                        <input class="form-control" value="<%=Model.SensorID1 == 0 ? "" : Model.SensorID1.ToString()%>" type="text" name="SensorID1" id="sensorID1" placeholder="<%: Html.TranslateTag("Network/AssignDevice|ID Number","ID Number")%>" required="required">
                                        <%: Html.ValidationMessageFor(model => model.SensorID1, null, new { @style = "color:red" })%>
                                        <br />

                                        <div style="display: flex; justify-content: flex-start; align-items: center; width: 100%; font-weight: bold;">
                                        <%: Html.TranslateTag("Overview/CustomAccountSetup|Security Code","Security Code")%>:
                                        </div>

                                        <input class="form-control" value="<%=Model.SensorCode1%>" type="text" name="SensorCode1" id="sensorCode1" placeholder="<%: Html.TranslateTag("Network/AssignDevice|Security Code","Security Code")%>">
                                        <%: Html.ValidationMessageFor(model => model.SensorCode1, null, new { @style = "color:red" })%>
                                    </div>

                                    <div class="card_container__mini__wrapper shadow rounded">
                                        <%--2:--%>
                                        <%=Html.GetThemedSVG("sensor") %>
                                        <div class="dg3 d-flex flex-column" style="width: 100%;">
                                            <div class="text-start" style="font-weight: bold;">
                                                <%:Html.TranslateTag("Overview/CustomAccountSetup|Sensor Type","Sensor Type")%>:
                                            </div>
                                            <div class="dfjcac input-group mb-0">
                                                <select name="SensorType2" id="sensorType2" class="form-select" style="width: 100px;">
                                                    <option <%=Model.SensorName2 != null && Model.SensorName2.Contains("Cooler") ? "selected='selected'" : "" %> value="<%:Html.TranslateTag("Cooler","Cooler")%>"><%:Html.TranslateTag("Cooler","Cooler")%></option>
                                                    <option <%=Model.SensorName2 != null && Model.SensorName2.Contains("Freezer") ? "selected='selected'" : "" %> value="<%:Html.TranslateTag("Freezer","Freezer")%>"><%:Html.TranslateTag("Freezer","Freezer")%></option>
                                                </select>
                                                <select name="SensorLocation2" id="sensorLocation2" onchange="loadDefaultTypeLocation()" class="form-select" title="<%: Html.TranslateTag("Location","Location")%>">
                                                    <% for (int i = 1; i < 11; i++)
                                                        { %>
                                                    <option <%=Model.SensorName2 != null && Model.SensorName2.Contains("_" + i) ? "selected='selected'" : "" %> value="_<%=i %>"><%=i %></option>
                                                    <% } %>
                                                </select>
                                                <%-- <input type="text" value="<%=Model.SensorName2 %>" name="sensorName2" id="sensorName2" class="input_def login_input nameBox sensorTypeInput">--%>
                                            </div>
                                            <div></div>
                                        </div>
                                        <br />

                                        <div style="display: flex; justify-content: flex-start; align-items: center; width: 100%; font-weight: bold;">
                                            <%:Html.TranslateTag("Overview/CustomAccountSetup|ID Number","ID Number")%>:
                                        </div>

                                        <input class="form-control" value="<%=Model.SensorID2 == 0 ? "" : Model.SensorID2.ToString()%>" type="text" name="SensorID2" id="sensorID2" placeholder="<%: Html.TranslateTag("Network/AssignDevice|ID Number","ID Number")%>">
                                        <%: Html.ValidationMessageFor(model => model.SensorID2, null, new { @style = "color:red" })%>
                                        <br />

                                        <div style="display: flex; justify-content: flex-start; align-items: center; width: 100%; font-weight: bold;">
                                            <%:Html.TranslateTag("Overview/CustomAccountSetup|Security Code","Security Code")%>:
                                        </div>

                                        <input class="form-control" value="<%=Model.SensorCode2%>" type="text" name="SensorCode2" id="sensorCode2" placeholder="<%: Html.TranslateTag("Network/AssignDevice|Security Code","Security Code")%>">
                                        <%: Html.ValidationMessageFor(model => model.SensorCode2, null, new { @style = "color:red" })%>
                                    </div>

                                   <div class="card_container__mini__wrapper shadow rounded">
                                        <%=Html.GetThemedSVG("sensor") %>
                                        <div class="dg3 d-flex flex-column" style="width: 100%;">
                                            <div style="font-weight: bold;">
                                                <%:Html.TranslateTag("Overview/CustomAccountSetup|Sensor Type","Sensor Type")%>:
                                            </div>

                                            <div class="dfjcac input-group mb-0">
                                                <select name="SensorType3" id="sensorType3" class="form-select" style="width: 100px;">
                                                    <option <%=Model.SensorName3 != null && Model.SensorName3.Contains("Cooler") ? "selected='selected'" : "" %> value="<%:Html.TranslateTag("Cooler","Cooler")%>"><%:Html.TranslateTag("Cooler","Cooler")%></option>
                                                    <option <%=Model.SensorName3 != null && Model.SensorName3.Contains("Freezer") ? "selected='selected'" : "" %> value="<%:Html.TranslateTag("Freezer","Freezer")%>"><%:Html.TranslateTag("Freezer","Freezer")%></option>
                                                </select>

                                                <select name="SensorLocation3" id="sensorLocation3" onchange="loadDefaultTypeLocation()" class="form-select" title="<%: Html.TranslateTag("Location","Location")%>">
                                                    <% for (int i = 1; i < 11; i++)
                                                        { %>
                                                    <option <%=Model.SensorName3 != null && Model.SensorName3 != null && Model.SensorName3.Contains("_" + i) ? "selected='selected'" : "" %> value="_<%=i %>"><%=i %></option>
                                                    <% } %>
                                                </select>
                                                <%--<input type="text" value="<%=Model.SensorName3 %>" name="SensorName3" id="sensorName3" class="input_def login_input nameBox sensorTypeInput">--%>
                                            </div>
                                            <div></div>
                                        </div>
                                        <br />

                                        <div style="display: flex; justify-content: flex-start; align-items: center; width: 100%; font-weight: bold;">
                                            <%:Html.TranslateTag("Overview/CustomAccountSetup|ID Number","ID Number")%>:
                                        </div>

                                        <input class="form-control" value="<%=Model.SensorID3 == 0 ? "" : Model.SensorID3.ToString()%>" type="text" name="SensorID3" id="sensorID3" placeholder="<%: Html.TranslateTag("Network/AssignDevice|ID Number","ID Number")%>">
                                        <%: Html.ValidationMessageFor(model => model.SensorID3, null, new { @style = "color:red" })%>
                                        <br />

                                        <div style="display: flex; justify-content: flex-start; align-items: center; width: 100%; font-weight: bold;">
                                            <%:Html.TranslateTag("Overview/CustomAccountSetup|Security Code","Security Code")%>:
                                        </div>

                                        <input class="form-control" value="<%=Model.SensorCode3%>" type="text" name="SensorCode3" id="sensorCode3" placeholder="<%: Html.TranslateTag("Network/AssignDevice|Security Code","Security Code")%>">
                                        <%: Html.ValidationMessageFor(model => model.SensorCode3, null, new { @style = "color:red" })%>
                                    </div>

                                    <div class="card_container__mini__wrapper shadow rounded">
                                        <%=Html.GetThemedSVG("sensor") %>
                                        <div class="dg3 d-flex flex-column" style="width: 100%;">
                                            <div style="font-weight: bold;">
                                                <%:Html.TranslateTag("Overview/CustomAccountSetup|Sensor Type","Sensor Type")%>:
                                            </div>

                                            <div class="dgc2 dfjcac input-group mb-0">
                                                <select name="SensorType4" id="sensorType4" class="form-select" style="width: 100px;">
                                                    <option <%=Model.SensorName4 != null && Model.SensorName4.Contains("Cooler") ? "selected='selected'" : "" %> value="<%:Html.TranslateTag("Cooler","Cooler")%>"><%:Html.TranslateTag("Cooler","Cooler")%></option>
                                                    <option <%=Model.SensorName4 != null && Model.SensorName4.Contains("Freezer") ? "selected='selected'" : "" %> value="<%:Html.TranslateTag("Freezer","Freezer")%>"><%:Html.TranslateTag("Freezer","Freezer")%></option>
                                                </select>

                                                <select name="SensorLocation4" id="sensorLocation4" onchange="loadDefaultTypeLocation()" class="form-select" title="<%: Html.TranslateTag("Location","Location")%>">
                                                    <% for (int i = 1; i < 11; i++)
                                                        { %>
                                                    <option <%=Model.SensorName4 != null && Model.SensorName4.Contains("_" + i) ? "selected='selected'" : "" %> value="_<%=i %>"><%=i %></option>
                                                    <% } %>
                                                </select>
                                                <%-- <input type="text" value="<%=Model.SensorName4 %>" name="sensorName4" id="sensorName4" class="input_def login_input nameBox sensorTypeInput">--%>
                                            </div>
                                            <div class="dgc3"></div>
                                        </div>
                                        <br />

                                        <div style="display: flex; justify-content: flex-start; align-items: center; width: 100%; font-weight: bold;">
                                            <%:Html.TranslateTag("Overview/CustomAccountSetup|ID Number","ID Number")%>:
                                        </div>

                                        <input class="form-control" value="<%=Model.SensorID4 == 0 ? "" : Model.SensorID4.ToString()%>" type="text" name="SensorID4" id="sensorID4" placeholder="<%: Html.TranslateTag("Network/AssignDevice|ID Number","ID Number")%>">
                                        <%: Html.ValidationMessageFor(model => model.SensorID4, null, new { @style = "color:red" })%>
                                        <br />
                                        <div style="display: flex; justify-content: flex-start; align-items: center; width: 100%; font-weight: bold;">
                                            <%:Html.TranslateTag("Overview/CustomAccountSetup|Security Code","Security Code")%>:
                                        </div>

                                        <input class="form-control" value="<%=Model.SensorCode4%>" type="text" name="SensorCode4" id="sensorCode4" placeholder="<%: Html.TranslateTag("Network/AssignDevice|Security Code","Security Code")%>">
                                        <%: Html.ValidationMessageFor(model => model.SensorCode4, null, new { @style = "color:red" })%>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="text-end">
                                <button type="submit" value="<%:Html.TranslateTag("Save","Save")%>" class="btn btn-primary">
                                    <%:Html.TranslateTag("Save","Save")%>
                                </button>
                            </div>

                            <div id="usermessage" class="bold col-4" style="color: green;">
                            </div>
                        </div>
                    </form>
                </div>
                <div style="clear: both;"></div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        let districts;

        $(document).ready(function () {
            loadDefaultTypeLocation();
        });

        // this is for adding the sensor type and location to the name, then parsing that to prefill the form for each sensor
        function loadDefaultTypeLocation() {
            for (var i = 1; i < 5; i++) {
                let sensor = $(`#sensorType${i}`).find(":selected");
                let sensorLocation = $(`#sensorLocation${i}`).find(":selected");
                sensor.val(function () {
                    this.value.includes('_') ? this.value = this.value.substring(0, this.value.indexOf('_')) : "";
                    return this.value + sensorLocation.val();
                });
            }
        }

        $(function () {

            var divID = Number('<%=Model.DivisionAccountID%>');

            if (divID > 0) {
                $('#divisionID').val(divID);
                getDistricts(divID);
            }

            var accError = "<%=Html.TranslateTag("Overview/CreateAccount|Account name taken: Please choose another", "Account name taken: Please choose another")%>";

            $('#divisionID').on("change", function () {
                if ($('#divisionID').val() == 'Select a Division') {
                    $('#districtID').empty();
                    $('#districtID').show();
                    var optn = document.createElement('option');
                    optn.text = "Select a Division First";
                    optn.value = "-1";
                    var districtSelector = $('#districtID').get(0);
                    districtSelector.add(optn, null);
                }
                else {
                    var divisionID = $('#divisionID').val();
                    getDistricts(divisionID);
                }
            });

            $('#accountName').change(function (e) {
                $('#companynameError').html("");
                $('#accountName').removeClass("goodBox");
                e.preventDefault();
                var name = $('#accountName').val()

                $.post("/Overview/CheckAccountNumber", { accountnumber: name }, function (data) {
                    if (data != "True") {
                        $('#companynameError').html(accError)
                    } else {
                        $('#accountName').addClass("goodBox");

                    }

                });

            });

        });

        function getDistricts(divisionID) {

            $.get("/Overview/SubAccountsForAccount?accountID=" + divisionID, function (data) {

                if (data.indexOf("Failed") >= 0) {

                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                    return;
                }

                data = JSON.parse(data);
                districts = data;

                $('#districtID').empty();
                $('#districtID').show();
                var optn = document.createElement('option');
                optn.text = "Select a District";
                optn.value = "-1";
                var districtSelector = $('#districtID').get(0);
                districtSelector.add(optn, null);
                let accountTree = '<%=userAccount.AccountIDTree%>';

                // sets the values available in the District select
                $.each(data, function (value) {
                    var text = data[value].AccountName;
                    var selection = data[value].AccountID;
                    var opt = document.createElement('option');
                    opt.text = text;
                    opt.value = selection;
                    data[value].AccountID == '<%= Model.DistrictAccountID%>' ? opt.setAttribute('selected', true) : '';

                        var districtSelector = $('#districtID').get(0);
                        districtSelector.add(opt, null);
                    })

            })
        }

    </script>

</asp:Content>
