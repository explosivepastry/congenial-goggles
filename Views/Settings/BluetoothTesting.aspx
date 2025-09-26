<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Sensor>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Bluetooth Testing
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="display: flex; flex-direction: column; align-items: center;">
        <div id="bluetooth_testing" style="min-width: 30%;">
            <div class="container-fluid mt-4">
                <div class="x_panel shadow-sm rounded mb-4">
                    <div style="font-size: 1.5rem; border-bottom: solid 1px lightgrey;">
                        Device
                    </div>
                    <div id="actSearch_Div" class="col-12 Search_Div">
                        <div class="dfac input-group" style="flex-direction: column; gap: 1rem;">
                            <%--         <div class="dffdc">
                        <label for="service_select">Services</label>
                        <select id="service_select">
                            <option value=""></option>
                        </select>
                    </div>
                    <div class="dffdc">
                        <label for="characteristic_select">Characteristics</label>
                        <select id="characteristic_select">
                            <option value=""></option>
                        </select>
                    </div>--%>
                            <%--<input type="text" id="actSearch_Box" class="form-control" style="max-width: 300px;" placeholder="<%: Html.TranslateTag("Account","Account")%>" onkeypress="enterSubmit(event,'linkBtDevicesButton')" />--%>
                            <button type="button" id="linkBtDevicesButton" class="btn btn-primary" style="margin-top: 1rem;" title="<%: Html.TranslateTag("Scan For Bluetooth Devices")%>">
                                Link BT Device
                        <svg xmlns="http://www.w3.org/2000/svg" width="15" height="15" viewBox="0 0 17.49 17.49">
                            <path id="ic_zoom_in_24px" d="M15.5,14h-.79l-.28-.27a6.51,6.51,0,1,0-.7.7l.27.28v.79l5,4.99L20.49,19Zm-6,0A4.5,4.5,0,1,1,14,9.5,4.494,4.494,0,0,1,9.5,14Z" transform="translate(-3 -3)" style="fill: #fff;" />
                        </svg>
                            </button>

                            <button type="button" id="disconnect_Button" class="btn btn-secondary">
                                Disconnect
                            </button>
                            <button type="button" id="reconnect_Button" class="btn btn-secondary">
                                Reconnect
                            </button>
                            <%--			<button type="button" id="readStatusManual_Button" class="btn btn-primary" value="debug">
                    Status
                </button>--%>
                            <%--<input type="number" id="write_val" />--%>
                            <button type="button" id="read_Button" class="btn btn-primary" value="debug">
                                Read
                            </button>

                            <button type="button" id="btConnected" class="" value="debug">
                                Connection
                            </button>


                        </div>
                    </div>
                </div>
            </div>

            <div class="x_panel shadow-sm rounded" style="margin-bottom: 2rem; overflow: auto; max-height: 375px;">

                <div style="font-size: 1.5rem; border-bottom: solid 1px lightgrey;">
                    Developer Tools
                </div>
                <div class="dfac input-group" style="flex-direction: column; gap: 1rem;">
                    <button type="button" id="debug_Button" class="btn btn-primary" value="debug" style="margin-top: 1rem;">
                        <%: Html.GetThemedSVG("bug") %>
                    </button>
                    <button type="button" id="btNull" class="" value="debug">
                        Device Obj Status
                    </button>
                    <button type="button" id="startNtfc_Button" class="" value="debug">
                        Start Notifications
                    </button>
                    <button type="button" id="stopNtfc_Button" class="" value="debug">
                        Stop Notifications
                    </button>
                    <div class="d-flex">
                        <div>
                            <label for="enable_standard_logs">Enable Standard Logs</label>
                            <input type="checkbox" id="enable_standard_logs" />
                        </div>
                        <div>
                            <label for="enable_error_logs">Enable Error Logs</label>
                            <input type="checkbox" id="enable_error_logs" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="" style="display: flex; flex-direction: column; align-items: center; gap: 2rem;">
                <div class="x_panel shadow-sm rounded" style="padding: 1rem !important; display: flex; flex-direction: column; gap: 1rem;">
                    <div id="settings_header" style="font-size: 1.5rem; border-bottom: solid 1px lightgrey;">
                        Settings
                    </div>
                    <div class="dflac">
                        <label for="device_name">Device Name</label>
                        <input type="text" id="device_name" disabled />
                    </div>
                    <div>
                        <div class="dflac">
                            <label for="ssid">SSID</label>
                            <div class="d-flex">
                                <input type="text" id="ssid" style="width: 100%" />
                                <button id="ssid_write" class="btn btn-secondary" style="max-height: 25.5px; display: flex; align-items: center; font-size: .8rem;">
                                    Update</button>
                            </div>
                        </div>
                        <div class="dflac">
                            <label for="password">Password</label>
                            <div class="d-flex">
                                <input type="text" id="password" style="width: 100%; border: 1px solid var(--secondary-color); border-radius: .25em;" />
                                <button id="password_write" class="btn btn-secondary" style="max-height: 25.5px; display: flex; align-items: center; font-size: .8rem;">Update</button>
                            </div>
                        </div>
                        <div class="dflac">
                            <label for="authtype">Auth Type</label>
                            <div class="d-flex">
                                <select id="authtype" style="width: 100%">
                                    <%--<option value="0">Open</option>
            <option value="2">WPA_PSK</option>
            <option value="3">WPA2_PSK</option>
            <option value="4">WPA_WPA2_PSK</option>--%>
                                </select>
                                <button id="authtype_write" class="btn btn-secondary" style="max-height: 25.5px; display: flex; align-items: center; font-size: .8rem;">Update</button>
                            </div>
                        </div>
                        <div class="dflac">
                            <label for="update_wifi"></label>
                            <div class="d-flex py-1" style="justify-content: center;">
                                <button id="update_wifi" class="btn btn-secondary" style="max-height: 25.5px; display: flex; align-items: center; font-size: .8rem; min-width: 65px;">Update All WiFi Settings</button>
                            </div>
                        </div>
                    </div>
                    <div class="dflac">
                        <label for="status">Status</label>
                        <div class="d-flex">
                            <input type="text" id="status" style="width: 100%" disabled />
                            <button id="status_write" class="btn btn-secondary" style="max-height: 25.5px; display: flex; align-items: center; font-size: .8rem;">Read</button>
                        </div>
                    </div>
                    <div class="scannetwork">
                        <label for="scannetwork">Scan Network</label>
                        <div class="d-flex">
                            <input type="text" id="scannetwork" style="width: 100%; visibility: hidden" disabled />
                            <button id="scannetwork_write" class="btn btn-secondary" style="max-height: 25.5px; display: flex; align-items: center; font-size: .8rem;">Scan</button>
                        </div>
                    </div>
                    <div class="servercommstatus">
                        <div class="d-flex" style="justify-content: space-between;">
                            <label for="servercommstatus">Server Comm Status</label>
                            <button id="servercommstatus_write" class="btn btn-secondary" style="max-height: 25.5px; display: flex; align-items: center; font-size: .8rem;">
                                Server Status
                            </button>
                        </div>
                        <div class="d-flex" style="flex-flow: column">
                            <div class="d-flex" style="flex-flow: column">
                                <div class="d-flex">
                                    <input type="text" id="servercommstatus1Hex" title="Primary Server Comm Status Hex" style="width: 25%;" disabled />
                                    <input type="text" id="servercommstatus1Msg" title="Primary Server Comm Status Message" style="width: 75%;" disabled />
                                </div>
                                <input type="text" id="servercommstatus2" title="Secondary Server Comm Status" style="width: 100%;" disabled />
                                <textarea name="serverCommStatus1MsgLong" id="serverCommStatus1MsgLong" style="width: 100%;"></textarea>
                            </div>
                        </div>
                    </div>
                    <div id="routing" style="display: none;">
                        <div style="font-size: 1.15rem; border-bottom: solid 1px lightgrey;">
                            Routing
                        </div>
                        <div class="dflac">
                            <label for="ipaddr0">IP Address</label>
                            <div class="d-flex" style="flex-direction: row;">
                                <input type="number" id="ipaddr0" class="octet 0" tabindex="1" style="width: 100%;" />&#x25CF;
						<input type="number" id="ipaddr1" class="octet 1" tabindex="2" style="width: 100%;" />&#x25CF;
						<input type="number" id="ipaddr2" class="octet 2" tabindex="3" style="width: 100%;" />&#x25CF;
						<input type="number" id="ipaddr3" class="octet 3" tabindex="4" style="width: 100%;" />
                                <button id="ipconfig_write" class="btn btn-secondary" tabindex="-1" style="max-height: 25.5px; display: flex; align-items: center; font-size: .8rem;">Update</button>
                            </div>
                        </div>
                        <div class="dflac">
                            <label for="mask0">Subnet Mask</label>
                            <div class="d-flex" style="flex-direction: row;">
                                <input type="number" id="mask0" class="octet 0" tabindex="5" style="width: 100%;" />&#x25CF;
	            <input type="number" id="mask1" class="octet 1" tabindex="6" style="width: 100%;" />&#x25CF;
	            <input type="number" id="mask2" class="octet 2" tabindex="7" style="width: 100%;" />&#x25CF;
	            <input type="number" id="mask3" class="octet 3" tabindex="8" style="width: 100%;" />
                            </div>
                        </div>
                        <div class="dflac">
                            <label for="gateway0">Gateway</label>
                            <div class="d-flex" style="flex-direction: row;">
                                <input type="number" id="gateway0" class="octet 0" tabindex="9" style="width: 100%;" />&#x25CF;
	            <input type="number" id="gateway1" class="octet 1" tabindex="10" style="width: 100%;" />&#x25CF;
	            <input type="number" id="gateway2" class="octet 2" tabindex="11" style="width: 100%;" />&#x25CF;
	            <input type="number" id="gateway3" class="octet 3" tabindex="12" style="width: 100%;" />
                            </div>
                        </div>
                        <div class="dflac">
                            <label for="dns0">DNS</label>
                            <div class="d-flex" style="flex-direction: row;">
                                <input type="number" id="dns0" class="octet 0" tabindex="13" style="width: 100%;" />&#x25CF;
	            <input type="number" id="dns1" class="octet 1" tabindex="14" style="width: 100%;" />&#x25CF;
	            <input type="number" id="dns2" class="octet 2" tabindex="15" style="width: 100%;" />&#x25CF;
	            <input type="number" id="dns3" class="octet 3" tabindex="16" style="width: 100%;" />
                            </div>
                        </div>
                        <div class="dflac">
                            <div class="d-flex" style="justify-content: space-between;">
                                <label for="serveraddress">Server Address</label>
                                <span id="serveraddress_nbytes"></span>
                            </div>
                            
                            <div class="d-flex">
                                <input type="text" id="serveraddress" style="width: 100%" />
                                <button id="serveraddress_write" class="btn btn-secondary" style="max-height: 25.5px; display: flex; align-items: center; font-size: .8rem;">Update</button>
                            </div>
                        </div>
                        <div class="dflac">
                            <label for="serverport">Server Port</label>
                            <div class="d-flex">
                                <input type="text" id="serverport" style="width: 100%" />
                                <button id="serverport_write" class="btn btn-secondary" style="max-height: 25.5px; display: flex; align-items: center; font-size: .8rem;">Update</button>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="x_panel shadow-sm rounded" style="margin-top: 2rem;">
                    <div style="font-size: 1.5rem; border-bottom: solid 1px lightgrey;">
                        Networks
                    </div>
                    <table id="networks_list" style="width: 100%">
                        <thead>
                            <tr>
                                <th scope="col">SSID</th>
                                <th scope="col">RSSI</th>
                                <th scope="col">Auth</th>
                            </tr>
                        </thead>
                    </table>
                    <%--<div class="dflac" style="gap: 1rem; margin-top: 1rem;">
						<div class="col-2">RSSI</div>
						<div class="col-2">Auth</div>
						<div class="col-8">SSID</div>
					</div>--%>
                    <div class="text-center" id="networks_loading" style="display: none;">
                        <div class="spinner-border text-primary" role="status">
                            <span class="visually-hidden"><%: Html.TranslateTag("Loading","Loading")%>...</span>
                        </div>
                    </div>
                </div>

                <div class="x_panel shadow-sm rounded" style="margin-top: 2rem; overflow: auto; max-height: 375px;">
                    <div style="border-bottom: solid 1px lightgrey; display: flex; justify-content: space-between;">
                        <div style="font-size: 1.5rem;">
                            Logs
                        </div>
                        <button type="button" id="clear_Button" class="btn btn-primary" value="debug">
                            Clear Logs
                    <%: Html.GetThemedSVG("delete") %>
                        </button>
                    </div>
                    <div id="logBox" class="scrollParentSmall"></div>
                </div>
            </div>
        </div>
    </div>
    <%--	<form action="#">
		<div>
			<label>
				Enter an integer between 1 and 10:
      <input min="1" max="10" id="anexample" required />
			</label>
		</div>
		<div>
			<input type="submit" value="submit" /></div>
	</form>
	<hr />
	Invalid values:
	<ul id="log"></ul>
	<div class="grid-container">
		<div class="grid-item">
			1
		</div>
		<div class="grid-item">
			<div class="x_panel shadow-sm rounded" style="margin-top: 5px">
				<div class="card_container__top__title">
					<%: Html.TranslateTag("Remembered Devices")%>
				</div>
				<div id="rememberedDevicesBox" class="scrollParentSmall"></div>
			</div>
		</div>
		<div class="grid-item">
			<div class="x_panel shadow-sm rounded" style="margin-top: 5px">
				<div class="card_container__top__title">
					<%: Html.TranslateTag("Selected Device")%>
				</div>
				<div id="selectedDeviceBox" class="scrollParentSmall"></div>
			</div>
		</div>
		<div class="grid-item">
			<div class="x_panel shadow-sm rounded" style="margin-top: 5px">
				<div class="card_container__top__title">
					<%: Html.TranslateTag("Services")%>
				</div>
				<div id="serviceBox" class="scrollParentSmall"></div>
			</div>
		</div>
		<div class="grid-item">
			<div class="x_panel shadow-sm rounded" style="margin-top: 5px">
				<div class="card_container__top__title">
					<%: Html.TranslateTag("Characteristics")%>
				</div>
				<div id="characteristicBox" class="scrollParentSmall"></div>
			</div>
		</div>
		<div class="grid-item">
			<div class="x_panel shadow-sm rounded" style="margin-top: 5px">
				<div class="card_container__top__title">
					<%: Html.TranslateTag("Descriptors")%>
				</div>
				<div id="descriptorBox" class="scrollParentSmall"></div>
			</div>
		</div>
		<div class="grid-item">
			<div class="x_panel shadow-sm rounded" style="margin-top: 5px">
				<div class="card_container__top__title">
					<%: Html.TranslateTag("Values")%>
				</div>
				<div id="valueBox" class="scrollParentSmall"></div>
			</div>
		</div>
		<div class="grid-item">8</div>
		<div class="grid-item">9</div>
	</div>



	<style>
		input[type="number"]:invalid {
			color:blue;
		}
	</style>

	<script type="text/javascript">



		document.getElementById('phoneNumber').addEventListener("invalid", (event) => {
			console.log('invalid1')
			console.log(event)
		});

		oninvalid = (event) => {
			console.log('invalid2')
			console.log(event)
		};

		const isNumericInput = (event) => {
			const key = event.keyCode;
			return ((key >= 48 && key <= 57) || // Allow number line
				(key >= 96 && key <= 105) // Allow number pad
			);
		};

		const isModifierKey = (event) => {
			const key = event.keyCode;
			return (event.shiftKey === true || key === 35 || key === 36) || // Allow Shift, Home, End
				(key === 8 || key === 9 || key === 13 || key === 46) || // Allow Backspace, Tab, Enter, Delete
				(key > 36 && key < 41) || // Allow left, up, right, down
				(
					// Allow Ctrl/Command + A,C,V,X,Z
					(event.ctrlKey === true || event.metaKey === true) &&
					(key === 65 || key === 67 || key === 86 || key === 88 || key === 90)
				)
		};

		const enforceFormat = (event) => {
			// Input must be of a valid number format or a modifier key, and not longer than ten digits
			if (!isNumericInput(event) && !isModifierKey(event)) {
				event.preventDefault();
			}
		};

		const formatToPhone = (event) => {
			if (isModifierKey(event)) { return; }

			const input = event.target.value.replace(/\D/g, '').substring(0, 12); // First ten digits of input only
			const areaCode = input.substring(0, 3);
			const middle = input.substring(3, 6);
			const last = input.substring(6, 9);
			const last2 = input.substring(9, 12);

			if (input.length >= 9) { event.target.value = `${areaCode}.${middle}.${last}.${last2}`; }
			else if (input.length >= 6) { event.target.value = `${areaCode}.${middle}.${last}`; }
			else if (input.length >= 3) { event.target.value = `${areaCode}.${middle}`; }

		};

		const inputElement = document.getElementById('phoneNumber');
		inputElement.addEventListener('keydown', enforceFormat);
		inputElement.addEventListener('keyup', formatToPhone);

	</script>--%>







    <script src="/Scripts/BluetoothTesting/BluetoothServicesDic.js" type="text/javascript"></script>
    <script src="/Scripts/BluetoothTesting/BluetoothCharacteristicsDic.js" type="text/javascript"></script>
    <script src="/Scripts/BluetoothTesting/BluetoothDescriptorsDic.js" type="text/javascript"></script>
    <script src="/Scripts/BluetoothTesting/MowiGattUuids.js" type="text/javascript"></script>
    <%--<script src="/Scripts/BluetoothTesting/ParseIPAddress.js" type="text/javascript"></script>--%>
    <script src="/Scripts/BluetoothTesting/BluetoothTesting.js" type="text/javascript"></script>




    <script>
		<%: ExtensionMethods.LabelPartialIfDebug("BluetoothTesting.aspx") %>

        $(document).ready(function () {
            $('.octet').attr({ 'min': 0, 'max': 255 })
        })

        //$(document).ready(() => {

        //$('#ipin_write').click(function (x) {

        //	console.log(x)
        //	console.log($('#ipin').val())
        //})

        ////input mask bundle ip address
        //var ipv4_address = $('#ipv4');
        //ipv4_address.inputmask({
        //	alias: "ip",
        //	greedy: false //The initial mask shown will be "" instead of "-____".
        //});

        //$(MowiGattServicesDic).each((i, s) => {
        //	//console.log(s);
        //	let option = `<option value="${s.uuid}"> ${s.name} </option>`
        //	$('#service_select').append(option)
        //})

        //$(MowiGattCharacteristicsDic).each((i, c) => {
        //	//console.log(c);
        //	let option = `<option value="${c.uuid}"> ${c.name} </option>`
        //	$('#characteristic_select').append(option)
        //})

        //})

    </script>
    <style>
        .logLine {
            border-bottom: grey 0.1rem solid;
            margin: 0;
            padding: 0.1rem 0 0 0;
        }

            .logLine.error {
                color: red;
            }

        .octet {
            width: 100%;
        }

        .ntwk_found {
            border: black 1px solid;
            padding: 1px 0px 0px 0px;
            cursor: pointer;
        }

            .ntwk_found.unsupported {
                cursor: default !important;
            }

        .auth_unsupported {
            color: red;
        }

            /*a[title]:hover::after {
	        content: attr(title);
	        position: absolute;
	        top: -100%;
	        left: 0;
	    }*/

            .auth_unsupported:hover::after {
                content: 'Not Supported';
                position: absolute;
                top: 15%;
                color: red;
                right: -24%;
                font-weight: 500;
                background: white;
                border: 1px solid black;
                padding: 3px;
            }

        #networks_list th,
        #networks_list td {
            padding-left: 10px;
            padding-right: 10px;
        }

        .dflac {
            display: flex;
            flex-direction: column;
        }

        input:invalid {
            border: red solid 3px;
        }

        .grid-container {
            display: grid;
            grid-template-columns: auto auto auto;
            /*background-color: #2196F3;*/
            padding: 10px;
        }

        .grid-item {
            /*background-color: rgba(255, 255, 255, 0.8);*/
            border: 1px solid rgba(0, 0, 0, 0.8);
            /*padding: 20px;
			font-size: 30px;
			text-align: center;*/
        }

        #serverCommStatus1MsgLong:hover, #serverCommStatus1MsgLong.hover {
            color: green;
        }
    </style>
    <script>
        function showTooltip() {
            document.getElementById("serverCommStatus1MsgLong").title = "This should only show when input is clicked";
        }

        function removeTooltip() {
            document.getElementById("serverCommStatus1MsgLong").title = "";
        }
    </script>
</asp:Content>
