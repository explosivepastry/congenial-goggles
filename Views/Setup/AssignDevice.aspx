<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<AssignObjectModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AssignDevice
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        List<CSNet> networks = Model.Networks; // iMonnit.Controllers.CSNetController.GetListOfNetworksUserCanSee(MonnitSession.CurrentCustomer.AccountID);

        if (!MonnitSession.CurrentCustomer.CanSeeNetwork(Model.NetworkID))
        {
            Model.NetworkID = long.MinValue;
        }

        long ntwkId = Model.NetworkID > 0 ? Model.NetworkID : MonnitSession.SensorListFilters.CSNetID;

    %>

    <%Html.RenderPartial("_SetupStepper", long.MinValue); %>
    <%
        string CacheControl = ConfigData.FindValue("CacheControl");
        if (string.IsNullOrEmpty(CacheControl))
            CacheControl = DateTime.UtcNow.Date.Ticks.ToString();//If nothing else force refresh once a day
    %>
    <script src="/Scripts/ZXing.js?<%:CacheControl %>" type="text/javascript"></script>

    <div class="container-fluid mt-4">

        <%---------     SETUP DEVICE   ---%>
        <%if (networks.Count() == 0)
            {
        %>
        <div class="setup_device_design shadow rounded">
            <h2><%: Html.TranslateTag("Setup/AssignDevice: No network available to assign a device to.")%></h2>
        </div>
        <% 
            }
            else
            {%>

        <div class="setup_device_design shadow rounded">
            <h2><%: Html.TranslateTag("Device Setup", "Device Setup")%></h2>
            <div class="device-row-1" id="networkDrop" <%= networks.Count() <= 1 ? "hidden" : "" %>>

                <%--<%if (networks.Count() == 1)
                    {
                %>--%>
                <%--<input type="hidden" id="netSelect" name="networkID" value="<%= Model.Networks[0].CSNetID %>" />--%>
                <%--<% }
                    else
                    {%>--%>
                <span style="background-color: white !important; overflow: unset;"><%: Html.TranslateTag("Assigning to", "Assigning to:")%></span>

                <select id="netSelect" name="networkID" class="form-select" style="width: 250px;">
                    <%
                        foreach (CSNet net in networks)
                        {
                    %>
                    <option <%=net.CSNetID == ntwkId ? "selected" : "" %> value="<%=net.CSNetID %>"><%=net.Name %></option>
                    <%   
                        }
                    %>
                </select>

                <%--<%} %>--%>
            </div>

            <!-- QR Code Scanner -->

            <div id="QRPanel" style="display: none;">
                <h5 id="scanDeviceSelect" class="col-md-8 col-12" style="background-color: white !important; overflow: no-display; margin-top: 0px; z-index: 2;"><b><%: Html.TranslateTag("Scan Device with", "Scan Device with")%>&nbsp</b>
                    <select id="sourceSelect" class="form-select" style="max-width: 250px;"></select>
                </h5>
                <div class="clearfix"></div>

                <div class="form-group">
                    <div>
                        <input type="hidden" value="<%= Model.NetworkID%>" />
                        <button class="btn btn-primary" id="startButton" hidden><%:Html.TranslateTag("Start")%></button>
                        <button class="btn btn-primary" id="resetButton" hidden><%:Html.TranslateTag("Reset")%></button>
                    </div>

                    <div class="video-container">
                        <!-- Error Modal -->
                        <div id="errorModal" class="modal fade col-md-6 col-sm-10 col-11" role="dialog" style="height: 75%;">
                            <div class="modal-dialog" style="max-width: 100% !important; height: 100% !important;">

                                <!-- Modal content-->
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h2 class="modal-title"><%:Html.TranslateTag("Scan Error")%></h2>
                                    </div>
                                    <div class="modal-body">
                                        <span id="errorhandler"></span>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" onclick="StartCamera();"><%:Html.TranslateTag("Close")%></button>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <video id="video" style="width: 100%; height: 66%;">
                        </video>
                        <div id="overlayQR" class="overlay-desc">
                        </div>
                    </div>
                    <pre hidden><code id="result"></code></pre>
                </div>
                <button id="btnManualAdd" class="btn btn-secondary"><%: Html.TranslateTag("Enter Manually", "Enter Manually")%></button>
            </div>

            <!-- Manual Device Add -->
            <form id="manualSubmitForm" action="/Network/ManualSubmit" style="width: 100%" onsubmit="$('#manualSubmit').hide(); $('#verify').show(); ">
                <div id="ManualPanel">

                    <div class="scan-QR" style="display: none;">
                        <a id="btnQRAdd" class="btn btn-primary">
                            <svg version="1.1" class="me-2" id="Capa_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px"
                                width="20px" height="20px" viewBox="0 0 401.994 401.994" fill="#fff" style="enable-background: new 0 0 401.994 401.994;"
                                xml:space="preserve">
                                <g>
                                    <path d="M0,401.991h182.724V219.265H0V401.991z M36.542,255.813h109.636v109.352H36.542V255.813z" />
                                    <rect x="73.089" y="292.355" width="36.544" height="36.549" />
                                    <rect x="292.352" y="365.449" width="36.553" height="36.545" />
                                    <rect x="365.442" y="365.449" width="36.552" height="36.545" />
                                    <polygon points="365.446,255.813 328.904,255.813 328.904,219.265 219.265,219.265 219.265,401.991 255.813,401.991 255.813,292.355 292.352,292.355 292.352,328.904 401.991,328.904 401.991,219.265 401.991,219.265 365.446,219.265" />
                                    <path d="M0,182.728h182.724V0H0V182.728z M36.542,36.542h109.636v109.636H36.542V36.542z" />
                                    <rect x="73.089" y="73.089" width="36.544" height="36.547" />
                                    <path d="M219.265,0v182.728h182.729V0H219.265z M365.446,146.178H255.813V36.542h109.633V146.178z" />
                                    <rect x="292.352" y="73.089" width="36.553" height="36.547" />
                                </g>

                            </svg>
                            <%: Html.TranslateTag("Scan QR Code", "Scan QR Code")%>
                        </a>

                        <h5 class="qr-hide">or</h5>
                        <p class="qr-hide"><%:Html.TranslateTag("Enter Device ID and SC code")%></p>
                    </div>

                    <div class="device_num">

                        <div class="device-row-2">
                            <label>ID:</label>
                            <input class="form-control" style="width: 250px" type="text" id="ObjectID" required="required" placeholder="<%: Html.TranslateTag("Device ID", "Device ID")%>" name="deviceID">
                            <%: Html.ValidationMessageFor(model => model.ObjectID)%>
                            <label>SC:</label>
                            <input class="form-control" style="width: 250px" name="checkCode" type="text" id="checkCode" placeholder="<%: Html.TranslateTag("Security Code", "Security Code")%>" required="required">
                            <%: Html.ValidationMessageFor(model => model.Code)%>
                        </div>

                    </div>

                    <input type="hidden" id="accID" name="ID" value="<%=Model.AccountID %>" />
                    <input type="hidden" id="netID" name="networkID" value="<%= Model.NetworkID  %>" />
                    <%--<input type="hidden" id="netID" name="networkID" value="<%=MonnitSession.SensorListFilters.CSNetID == long.MinValue ? Model.Networks[0].CSNetID :MonnitSession.SensorListFilters.CSNetID  %>" />--%>

                    <p style="color: red;"><%=Html.ValidationMessage("LimitReached")%></p>

                    <%--POP UP  MODAL--%>
                    <div class="info-question">
                        <a class="btn idsc-btn" data-bs-toggle="modal" data-bs-target="#idscModal"><%:Html.TranslateTag("Can't find ID / SC?")%>
                            <div class="circleQuestion " data-bs-toggle="modal" data-bs-target="#idscModal"><%=Html.GetThemedSVG("circleQuestion") %></div>
                        </a>
                    </div>

                    <!-- Modal -->
                    <div class="modal fade" id="idscModal" tabindex="-1" aria-labelledby="idscModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-md">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="idscModalLabel"><%:Html.TranslateTag("Cant find ID / SC?") %></h5>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" style="color: blue"></button>
                                </div>
                                <div class="modal-body">
                                    <h6><%:Html.TranslateTag("The ID and SC are located on the device you are setting up. They are near the scannable code.")%></h6>
                                    <img class="device-img" src="/Content/dashboard/images/IDandSC.png" />
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="device-row-3" style="align-items: center; justify-content: space-around;">
                        <a id="advancedOptionsButton" href="#" onclick="toggleAdvancedOptions()"><%:Html.TranslateTag("Advanced Options", "Advanced Options")%></a>
                        <button type="button" id="manualSubmit" class="btn btn-primary "><%:Html.TranslateTag("Next", "Next")%></button>
                        <button class="btn btn-primary" id="verify" type="submit" disabled style="display: none;">
                            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                            Adding...
                        </button>
                    </div>
                    <div id="advancedOptions" style="display: none;">
                        <a id="multiDeviceBtn">
                            <input class="btn btn-secondary btn-sm" type="button" value="<%: Html.TranslateTag("Network/NetworkSelect|Upload CSV","Upload CSV")%>">
                        </a>
                        <% if (MonnitSession.CustomerCan("Gateway_Create") && MonnitSession.CustomerCan("Sensor_Create"))
                            { %>
                        <a id="xmlDeviceBtn">
                            <input class="btn btn-secondary btn-sm" type="button" value="<%: Html.TranslateTag("Network/NetworkSelect|Upload XML","Upload XML")%>">
                        </a>
                        <% }%>
                    </div>
                </div>

            </form>

            <div style="color: red !important; font-weight: 600; text-align: center; padding-top: 20px;" id="messageDiv"></div>
        </div>
        <%} %>
    </div>

    <script type="text/javascript">

       $('#multiDeviceBtn').click(function () {
            window.location.href = "/Network/AssignMultipleDevices/<%= Model.AccountID%>?networkID=" + $("#netSelect").val();
       });

        $('#xmlDeviceBtn').click(function () {
            window.location.href = "/Network/XmlDeviceAdd/<%=Model.AccountID%>?networkID=" + $("#netSelect").val();
           });

        function StartCamera() {
            $('#startButton').click();
            setTimeout(function () { $('#overlay').show(); }, 700);
        }

        function StopCamera() {
            $('#resetButton').click();
            $('#overlay').hide();
        }

        function ShowQRModal() {
            /*$('#errorModal').show();*/
            $('#errorModal').modal('show');
            StopCamera();
        }

        function HideNetworkDrop() {
            if (networks.Count() <= 1)
                $('#networkDrop').hide();
        }


        const toggleAdvancedOptions = () => {
            $('#advancedOptions').slideToggle('slow');
        }


        var deviceCount = 0;

        window.addEventListener('load', function () {
            let selectedDeviceId;
            const codeReader = new ZXing.BrowserMultiFormatReader()
            //console.log('ZXing code reader initialized')
            codeReader.listVideoInputDevices()
                .then((videoInputDevices) => {
                    const sourceSelect = document.getElementById('sourceSelect')
                    selectedDeviceId = videoInputDevices[0].deviceId;

                    if (queryString("DeviceToMove").length > 0) {
                        selectedDeviceId = null;
                        QRHide();
                        ScanDeviceHide();
                        return;
                    }

                    if (queryString("HideCameraPop").length > 0) {
                        selectedDeviceId = null;
                        QRHide();
                        //ScanDeviceHide();
                        return;
                    }

                    if (videoInputDevices.length > 1) {
                        deviceCount = videoInputDevices.length;
                        videoInputDevices.forEach((element) => {
                            const sourceOption = document.createElement('option')
                            sourceOption.text = element.label
                            sourceOption.value = element.deviceId
                            sourceSelect.appendChild(sourceOption)
                        })

                        sourceSelect.onchange = () => {
                            selectedDeviceId = sourceSelect.value;
                            StopCamera();
                            StartCamera();
                        };
                        ScanDeviceShow();
                    }
                    else if (videoInputDevices.length == 1) {
                        deviceCount = videoInputDevices.length;
                        videoInputDevices.forEach((element) => {
                            const sourceOption = document.createElement('option')
                            sourceOption.text = element.label
                            sourceOption.value = element.deviceId
                            sourceSelect.appendChild(sourceOption)
                        })

                        sourceSelect.onchange = () => {
                            selectedDeviceId = sourceSelect.value;
                            StopCamera();
                            StartCamera();
                        };

                        $('#scanDeviceSelect').hide();
                        ScanDeviceShow();
                    }
                    else {
                        selectedDeviceId = null;
                        QRHide();
                        ScanDeviceHide();
                    }

                    document.getElementById('startButton').addEventListener('click', () => {
                        codeReader.decodeFromVideoDevice(selectedDeviceId, 'video', (result, err) => {
                            if (result) {
                                QRCodeResultHandler(result.text);
                                console.log(result)
                                document.getElementById('result').textContent = result.text
                            }
                            if (err && !(err instanceof ZXing.NotFoundException)) {
                                QRHide();
                                ScanDeviceHide();
                                console.error(err);
                                document.getElementById('result').textContent = err;
                                showAlertModal(err.message);
                            }
                        })
                        //console.log(`Started continous decode from camera with id ${selectedDeviceId}`)
                    })

                    document.getElementById('resetButton').addEventListener('click', () => {
                        codeReader.reset()
                    })

                    document.getElementById('btnManualAdd').addEventListener('click', () => {
                        QRHide();
                    })

                    document.getElementById('btnQRAdd').addEventListener('click', () => {
                        ManualHide();
                    })

                    //Show or Hide the Scan button on the Manual Panel
                    if (deviceCount > 0) {
                        ScanDeviceShow();
                    }
                    else {
                        ScanDeviceHide();
                    }

                    //StartCamera();

                })
                .catch((err) => {
                    QRHide();
                    ScanDeviceHide();
                })

        })

        function ManualHide() {
            $('#ManualPanel').hide();
            $('#QRPanel').show();
            StartCamera();
            //$('#ManualPanel').focus();
            //$('#ObjectID').focus();
        }

        function QRHide() {
            $('#QRPanel').hide();
            $('#ManualPanel').show();
            StopCamera();
            $('#QRPanel').focus();
            $('#ObjectID').focus();
        }

        function ScanDeviceHide() {
            $('.scan-QR').hide();
            $('#ObjectID').focus();
        }

        function ScanDeviceShow() {
            $('.scan-QR').show();
            $('#ObjectID').focus();
        }


        function SplitIDCode() {
            var objID = $('#ObjectID').val();

            if (objID.indexOf(":") > -1) {
                //if : then remove : and everything after
                $('#ObjectID').val(objID.substr(0, objID.indexOf(":")));
                if ($('#checkCode').val().length == 0) {
                    //if Codebox is empty put everythign after : in code box (.toUpper()?)
                    $('#checkCode').val(objID.substr(objID.indexOf(":") + 1, objID.length - 1 - objID.indexOf(":")));
                }
            }
        }

        function queryString(name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]").replace(/</g, "&lt;").replace(/>/g, "&gt;");
            var regexS = "[\\?&]" + name + "=([^&#]*)";
            var regex = new RegExp(regexS);
            var results = regex.exec(window.location.href);
            if (results == null)
                return "";
            else
                return decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        function QRCodeResultHandler(result) {
            var id = <%:MonnitSession.CurrentCustomer.CustomerID%>;
            var networkid = $('#netSelect').val();

            $.get('/Setup/QRScanResultHandler', { id: id, networkid: networkid, result: result }, function (data) {

                var values = data.split("|");

                if (values[1] == null) {
                    $('#errorhandler').html(data);
                    ShowQRModal();
                    showAlertModal(data);

                }
                else {
                    $('#ObjectID').val(values[0]);
                    $('#checkCode').val(values[1]);
                    $("#manualSubmit").click();
                }
            });
        }

        $(document).ready(function () {

            $('#netSelect').change(function (e) {
                $('#netID').val($('#netSelect').val());

            });

            $('#manualSubmit').click(function (e) {
                e.preventDefault();

                let values = {};
                var networkid = $('#netSelect').val();
                var deviceID = $('#ObjectID').val();
                var code = $('#checkCode').val();
                values.callback = function () {
                    $('#manualSubmitForm').submit();
                }
                values.text = "<%: Html.TranslateTag("Setup/AssignDevice|No gateways on your network, are you sure you want to continue?")%>"

                $.get('/Gateway/GatewaysOnNetworkCount', { NetworkID: networkid, DeviceID: deviceID, Code: code }, function (data) {

                    if (data == 0) {
                        openConfirm(values);
                    }
                    else {
                        $('#manualSubmitForm').submit();
                    }
                })
            });


            $('#overlay').hide();

            $('#ObjectID').change(function (e) {
                e.preventDefault();

                SplitIDCode();

                if (!isANumber($('#ObjectID').val())) {
                    $('#messageDiv').html("Device ID is a number");
                    $('#ObjectID').val("");
                }

            });



            if (queryString("DeviceToMove").length > 0) {
                var sid = queryString("DeviceToMove");
                $('#ObjectID').val(sid);
                SplitIDCode();
            }

            $('#ObjectID').keyup(function (e) {
                if (e.keyCode == 13) {
                    SplitIDCode();
                }
            });

            var href = "/Network/showDeviceDetails";

            if (queryString("sensorID").length > 0) {
                var sid = queryString("sensorID");
                $.post(href, { ID: sid, type: "sensor" }, function (data) {
                    $('#newDevice').html(data);
                });
            }

            if (queryString("gatewayID").length > 0) {
                var gid = queryString("gatewayID");
                $.post(href, { ID: gid, type: "gateway" }, function (data) {
                    $('#newDevice').html(data);
                });
            }

            if (queryString("failed").length > 0) {
                $('#errorhandler').text(queryString("failed"));
                $('#messageDiv').text(queryString("failed"));
                ShowQRModal();
                showAlertModal(queryString("failed"));
            }
        });


    </script>
    <style>
        #advancedOptions {
            padding-top: 2rem;
            display: flex;
            justify-content: space-evenly;
        }

        #advancedOptionsButton {
            color: var(--primary-color);
        }


            #advancedOptionsButton:hover {
                color: var(--primary-color-hover);
            }

        .video-container {
            width: auto;
            position: relative;
        }

        .overlay-desc {
            background-image: url("/Content/images/qrscan.png");
            width: 100%;
            height: 100%;
            position: absolute;
            left: 0%;
            right: 0%;
            top: 0%;
            bottom: 1%;
            margin: auto;
            background-size: contain;
            background-repeat: no-repeat;
            background-position: center;
            opacity: 0.25;
        }

        .video {
            width: 100%;
        }



        .assigndevice-btn {
            margin: 10px auto !important;
            border-radius: 8px;
            border: none;
            color: white !important;
            font-size: 18px;
            position: relative;
            min-height: 1px;
            padding: 18px;
            width: 100% !important;
            max-width: 300px;
        }

        .primary-btn {
            font-weight: 600;
        }

        .assigndevice_input {
            margin: 20px auto 0px auto !important;
            display: flex;
            justify-content: center;
            align-items: flex-start;
            flex-direction: column;
            max-width: 300px;
            padding: 18px;
            width: 100% !important;
        }

        .modal {
            min-height: 105%;
            position: absolute;
            left: 50%;
            top: 50%;
            transform: translate(-50%, -50%);
        }

        @media (max-width: 991px) {
            /*.video-container {
                    min-height: 30vh;
                    max-height: 30vh;
                }
    
                .overlay-desc {
                    min-height: 30vh;
                    max-height: 30vh;
                }*/
        }

        @media (max-width: 1200px) {
            .video-container {
            }
        }

        @media (max-width: 1270px) and (min-width: 192px) {
            .video-container {
            }
        }
    </style>

</asp:Content>
