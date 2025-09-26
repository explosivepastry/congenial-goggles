<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>


<div class="divHistory" style="width: 100%;">

    <div class="formtitle">
        Export sensor data
        <%Html.RenderPartial("HistoryDatePicker"); %>
    </div>
    <form id="exportForm" action="/ExportGatewayData" method="post">
        <div class="formBody ">
            <label style="font-size: x-small; float: right">Only the first 2500 records within the given date range will be exported.</label>

            <%: Html.ValidationSummary(false)%>
            <%: Html.Hidden("id", Model.GatewayID)%>
            <table>
                <tr>
                    <td colspan="4">
                        <input type="radio" title="This Gateway" id="uxExportAll_Gateway" name="uxExportAll" value="Sensor" checked="checked" /><label for="uxExportAll_Gateway">Data from this gateway</label></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <input type="radio" title="Include Data from Network Gateways" id="uxExportAll_Network" name="uxExportAll" value="Network" /><label for="uxExportAll_Network">Data from all gateways in network</label></td>
                </tr>
                <%if (MonnitSession.CurrentCustomer.IsAdmin)
                    { %>
                <tr>
                    <td colspan="4">
                        <input type="radio" title="Include Data from Account Gateways" id="uxExportAll_Account" name="uxExportAll" value="Account" /><label for="uxExportAll_Account">Data from all gateways in account</label></td>

                </tr>
                <tr>
                    <td style="width: 102px; padding-left: 2px; padding-bottom: 16px; padding-right: 0px">
                        <h6 style="margin-bottom: 0; margin-top: 0">Fields of data to export:</h6>
                    </td>
                </tr>
                <% } %>
            </table>
            <table style="border: 5px solid #DDDDDD; width: 100%">
                <thead>
                    <tr class="tableRowNoBorder">
                        <th style="color: #666666;">GatewayMessage GUID</th>
                        <th style="color: #666666;">GatewayID</th>
                        <th>Gateway</th>
                        <th>Received</th>
                        <th>Power</th>
                        <th>Battery</th>
                        <th>Message</th>
                        <th>Message</th>
                        <th>Alert</th>
                        <th>Signal</th>


                    </tr>
                    <tr>
                        <th colspan="2" style="font-style: italic; text-align: center; font-weight: normal; color: #666666;">(required)</th>
                        <th>Name</th>
                        <th>Date</th>
                        <th></th>
                        <th></th>
                        <th>Type</th>
                        <th>Count</th>
                        <th>Sent</th>
                        <th>Strength</th>


                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style="padding-left: 29px">
                            <input type="checkbox" disabled checked /><!--GatewayMessageGUID --></td>
                        <td style="padding-left: 30px">
                            <input type="checkbox" disabled checked /><!--GatewayID--></td>
                        <td style="padding-left: 15px">
                            <input type="checkbox" name="Gateway_Name" checked /><!--Gateway Name --></td>
                        <td style="padding-left: 11px">
                            <input type="checkbox" name="ReceivedDate" checked /><!--Received Date--></td>
                        <td style="padding-left: 19px">
                            <input type="checkbox" name="Power" checked /><!--Power--></td>
                        <td style="padding-left: 32px">
                            <input type="checkbox" name="Battery" checked /><!--Battery--></td>
                        <td style="padding-left: 18px">
                            <input type="checkbox" name="MessageType" checked /><!--Message Type--></td>
                        <td style="padding-left: 13px">
                            <input type="checkbox" name="MessageCount" /><!--Message Count--></td>
                        <td style="padding-left: 17px">
                            <input type="checkbox" name="MeetsNotificationRequirement" /><!--Alert--></td>

                        <td style="padding-left: 25px">
                            <input type="checkbox" name="Signal_Strength" /><!--Signal_Strength--></td>


                </tbody>
            </table>
        </div>
        <div class="buttons" style="margin-top: 38px;">
            <input type="button" id="export" value="Export data to CSV file" class="bluebutton" style="float: none;" />
            <a href="/Content/Documentation/Sensor_Export_Definitions.pdf" target="_blank">
                <img alt="help" style="width: 16px;" title="Sensor data export column definitions." src="<%:Html.GetThemedContent("/images/help.png")%>" /></a>
            <div style="clear: both;"></div>
        </div>

    </form>


    <% if (!string.IsNullOrEmpty(Model.ExternalID))
        { %>
    <div class="formtitle">Push incoming data to 3rd party</div>
    <div class="formBody">This external configuration tool allows you to pass data from your wireless sensor network devices to another service in real time.  This is done by coding the data into a url query then sending the data via http get request at the time data is received.  There is an extensive list of parameters that can be passed, as seen below, allowing you to send detailed information about both the gateway and the gateway traffic.</div>
    <div class="buttons">
        <%  if (MonnitSession.AccountCan("can_webhook_push"))
            { %>
        <input type="button" id="exportPush" class="greybutton" value="Configure data push" />
        <%}
            else
            { %>
        <a href="#" onclick="showSimpleMessageModal("<%=Html.TranslateTag("Only available to premiere users")%>"); return false;" class="greybutton">Configure data push</a>
        <%} %>
        <div style="clear: both;"></div>
    </div>
    <%} %>
</div>

<div class="divNote">


    <style>
        h3.ui-helper-reset {
            padding: 5px 0px 5px 25px;
        }
    </style>
    <%
        ExternalDataSubscription exdata = ExternalDataSubscription.LoadByGatewayID(Model.GatewayID).FirstOrDefault();

        if (exdata == null)
        {
            exdata = new ExternalDataSubscription() { SensorID = Model.SensorID, AccountID = MonnitSession.CurrentCustomer.AccountID };
        }

    %>
    <div class="formtitle">Configure data push</div>
    <div style="margin: 25px 25px 25px 25px;">
        <div class="configurations">
            <h3>Details</h3>
            <div>
                This external configuration tool allows you to pass data from your wireless sensor network devices to another service in real time.  This is done by coding the data into a url query then sending the data via http get request at the time data is received.  There is an extensive list of parameters that can be passed, as seen below, allowing you to send detailed information about both the data and the sensor.
        <p>The system will resend your data up to three times if it fails; once after a minute, again after 25 minutes and one more time an hour after the first attempt. If it fails the very first time after being set up, the retries will be disabled.</p>
                <p>If your url fails to send 20 times in a row, your data will no longer be retried after the initial attempt. If it fails to send 100 times in a row, the data push will be disabled altogether, under the assumption that your server is down, or there is an error in the url. The system will email you to notify you that this has been disabled. You can restart attempts with the button at the bottom labeled "Reset Broken Send", which will appear if your sending is disabled.</p>
            </div>
            <h3>Available Parameter Values</h3>
            <div>
                <table>
                    <tr>
                        <td>{0}</td>
                        <td>ExternalID</td>
                        <td>Defined by above</td>
                    </tr>
                    <tr>
                        <td>{1}</td>
                        <td>MessageID</td>
                        <td>Transaction ID for this message from the gateway 
                            <img alt="help" style="width: 16px;" title="GatewayMessageID is being phased out in lieu GatewayMessageGUID by Summer 2016.GatewayMessageGUID will not be sequential but will have a reasonable expectation uniqueness." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
                    </tr>
                    <tr>
                        <td>{2}</td>
                        <td>MessageType</td>
                        <td>Type of message sent from gateway</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>0)	Data<br />
                            1)	Data<br />
                            2)	requesting a new sensor list<br />
                            3)	Gateway received Configuration<br />
                            4)	Gateway received Reform network Command<br />
                            5)	Gateway was updated to factory (long press on reset button)<br />
                            6)	Not Used<br />
                            7)	Wi-Fi Lookup<br />
                            8)	Push Gateway Configuration into database<br />
                            9)	Not Used<br />
                            10)	Wi-Fi Status Info<br />
                            11)	Startup Message<br />
                            250) OTA (Over the Air) Gateway Update information<br />
                            251) OTA (Over the Air) Sensor (Wi-Fi) Update information
                        </td>
                    </tr>
                    <tr>
                        <td>{3}</td>
                        <td>Power</td>
                        <td>Power reading from gateway</td>
                    </tr>
                    <tr>
                        <td>{4}</td>
                        <td>Battery</td>
                        <td>Battery level of gateway</td>
                    </tr>
                    <tr>
                        <td>{5}</td>
                        <td>ReceivedDate</td>
                        <td>Date message sent from gateway</td>
                    </tr>
                    <tr>
                        <td>{6}</td>
                        <td>MessageCount</td>
                        <td>Number of messages sent to the server in the gateway packet</td>
                    </tr>
                    <tr>
                        <td>{7}</td>
                        <td>MeetsNotificationRequirement</td>
                        <td>If a notification was sent from the system when this messages was received</td>
                    </tr>
                    <tr>
                        <td>{8}</td>
                        <td>GatewayID</td>
                        <td>Gateway Identifier</td>
                    </tr>
                    <tr>
                        <td>{9}</td>
                        <td>GatewayName</td>
                        <td>Name set up for gateway</td>
                    </tr>
                    <tr>
                        <td>{10}</td>
                        <td>InternalGatewayIDs</td>
                        <td>Additional Gateway Identifiers</td>
                    </tr>
                    <tr>
                        <td>{11}</td>
                        <td>CSNetID</td>
                        <td>Customer Sensor Network ID</td>
                    </tr>
                    <tr>
                        <td>{12}</td>
                        <td>FirmwareVersion</td>
                        <td>Version of Firmware for Sensor Network</td>
                    </tr>
                    <tr>
                        <td>{13}</td>
                        <td>GatewayFirmware</td>
                        <td>Version of Gateway OS</td>
                    </tr>
                    <tr>
                        <td>{14}</td>
                        <td>IsDirty</td>
                        <td>There are pending configuration changes on the server for this gateway</td>
                    </tr>
                    <tr>
                        <td>{15}</td>
                        <td>GatewayTypeID</td>
                        <td>Coded Gateway type (2 = Temperature, 4=Water, etc)</td>
                    </tr>
                    <tr>
                        <td>{16}</td>
                        <td>CurrentSignalStrength</td>
                        <td>Approximate signal strength of gateway communication</td>
                    </tr>
                    <tr>
                        <td>{17}</td>
                        <td>GatewayMessageGUID</td>
                        <td>Transaction ID for this message from the gateway</td>
                    </tr>
                </table>
            </div>
            <h3>Example</h3>
            <div>
                <table>
                    <tr>
                        <th>Get Example</th>
                    </tr>
                    <tr>
                        <td>External Identifier:</td>
                        <td>3SX7Z</td>
                    </tr>
                    <tr>
                        <td>Connection Information:</td>
                        <td>http://YourDomain/DataFromGateway/?ID={0}&amp;Data={2}&amp;TransactionId={1}</td>
                    </tr>
                    <tr>
                        <td>Http Get Request:</td>
                        <td>http://YourDomain/DataFromGateway/?ID=3SX7Z&amp;Data=26&amp;TransactionId=103254</td>
                    </tr>
                    <tr>
                        <th>Post Example</th>
                        <td></td>
                    </tr>
                    <tr>
                        <td>External Identifier:</td>
                        <td>3SX7Z</td>
                    </tr>
                    <tr>
                        <td>Connection Information:</td>
                        <td>http://YourDomain/DataFromGateway/</td>
                    </tr>
                    <tr>
                        <td>Content Header Type:</td>
                        <td>application/x-www-form-urlencoded</td>
                    </tr>
                    <tr>
                        <td>Connection Body</td>
                        <td>ID={0}&amp;Data={2}&amp;TransactionId={1}</td>
                    </tr>
                    <tr>
                        <td>Http Post Request:</td>
                        <td>http://YourDomain/DataFromGateway/</td>
                        <tr>
                            <td>Uploaded Data: </td>
                            <td>ID=3SX7Z&amp;Data=26&amp;TransactionId=103254</td>
                        </tr>
                    </tr>
                </table>
            </div>
            <%:Html.Partial("../Sensor/WebServerExample") %>
        </div>

        <div style="clear: both;"></div>
        <br />
        <br />

    </div>
    <div class="formtitle" style="height: 32px">
        <span>Parameters</span> <span align="right">
            <input style="margin: 2px 25px -16px -3px;" type="button" id="cancelExportPush" value="Cancel" class="greybutton" /><%--//FIXTHIS--%></span>
    </div>

    <form action="/CSNet/ExternalConfiguration" id="datapush" method="post">



        <%: Html.ValidationSummary(true) %>
        <%: Html.Hidden("ExternalDataSubscriptionID",exdata.ExternalDataSubscriptionID) %>
        <%: Html.Hidden("GatewayID",Model.GatewayID) %>
        <%: Html.Hidden("AccountID",MonnitSession.CurrentCustomer.AccountID) %>

        <input class="aSettings__input_input" type="hidden" name="ExternalDataSubscriptionType" id="ExternalDataSubscriptionType" value="Generic_URL" />
        <%--<div class="editor-label">
        External Data Subscription Type
    </div>
    <div class="editor-field">
        <select id="ExternalDataSubscriptionType" name="ExternalDataSubscriptionType">
            <option value="Generic_URL">Generic URL</option>
            <option value="Sensing_Planet" selected="selected">Sensing Planet</option>
        </select>
    </div>--%>

        <div class="editor-label">
            External Identifier (optional)
        </div>
        <div class="editor-field">
            <%: Html.TextBox("ExternalID",exdata.ExternalID) %>
        </div>
        <div class="editor-error">
            <%: Html.ValidationMessage(exdata.ExternalID) %>
        </div>

        <div class="editor-label">
            Connection Information (required)
        </div>
        <div class="editor-field">
            <%: Html.TextBox("ConnectionInfo1",exdata.ConnectionInfo1) %>
        </div>
        <div class="editor-error">
            <%: Html.ValidationMessage(exdata.ConnectionInfo1) %>
        </div>

        <div class="editor-label">
            <a id="sendTest" href="#">Generate test link</a>
        </div>
        <div class="editor-field" id="testLink">
        </div>
        <div class="editor-error">
        </div>

        <div class="GetOrPostDiv">
            <div class="editor-label">
                Push Type (required):
            </div>
            <select class="GetOrPost tzSelect" id="verb" name="verb" onchange="getOrPost(this.value);">
                <option value="HttpGet" <%:exdata.verb=="HttpGet" || exdata.verb=="get" ? "selected" : ""%>>HttpGet</option>
                <option value="HttpPost" <%:exdata.verb=="HttpPost" || exdata.verb=="get" ? "selected" : ""%>>HttpPost</option>
            </select>
        </div>

        <div class="ContentHeaderDropdown" id="ContentHeaderDiv" style="<%: exdata.verb=="HttpGet" || exdata.verb=="get" ? "display: none;": ""%>">
            <div class="editor-label">
                Content Header Type (required):
            </div>
            <div class="editor-field">
                <select class="ContentHeader tzSelect" name="ContentHeaderType">
                    <option value="raw" <%:exdata.ContentHeaderType=="raw" ? "selected" : ""%>>raw</option>
                    <option value="application/x-www-form-urlencoded" <%:exdata.ContentHeaderType=="application/x-www-form-urlencoded" ? "selected" : ""%>>application/x-www-form-urlencoded</option>
                    <option value="application/json" <%:exdata.ContentHeaderType=="application/json" ? "selected" : ""%>>application/json</option>
                </select>
            </div>
            <br />
            <div class="editor-label">
                Connection Body (recommended)
            </div>
            <div class="editor-field">
                <%: Html.TextBox("ConnectionInfo2",exdata.ConnectionInfo2) %>
            </div>
            <div class="editor-error">
                <%: Html.ValidationMessage(exdata.ConnectionInfo2) %>
            </div>

            <div class="editor-label">
                <a id="sendTestBody" href="#">Generate test body</a>
            </div>
            <div class="editor-field" id="testBody">
            </div>
            <div class="editor-error">
            </div>
        </div>


        <div class="editor-label">
            Last Result
        </div>
        <div class="editor-field">
            <%: exdata.LastResult %>
        </div>
        <div class="editor-error">
        </div>




        <div class="buttons" style="margin: -3px -2px -10px -1px;">
            <%if (exdata != null && exdata.ExternalDataSubscriptionID > 0 && exdata.BrokenCount != 0)
                { %>
            <div class="BrokenCount" style="float: left; margin: 20px 5px 10px 5px; color: #2D4780;">
                Consecutive fails: <%: exdata.BrokenCount%>
            </div>
            <% } %>
            <input type="button" onclick="SaveDataPush();" value="Save" class="bluebutton" />
            <% if (exdata != null && exdata.ExternalDataSubscriptionID > 0 && exdata.ExternalDataSubscriptionID > long.MinValue)
                { %>
            <input type="button" onclick="checkDelete();" value="Delete" class="greybutton" />
            <%
                if (exdata != null && exdata.ExternalDataSubscriptionID > 0 && exdata.BrokenCount > ExternalDataSubscription.killSendLimit)
                {
            %>
            <input type="button" id="resetBrokenSend" onclick="resetBroken();" value="Reset Broken Send" class="bluebutton" />
            <%
                    }
                }  %>
            <div style="clear: both;"></div>
        </div>

    </form>



</div>

<script type="text/javascript">
    $(function () {


        $('.divNote').hide();
        $('#export').click(function (e) {
            e.preventDefault();  //stop the browser from following
            var form = $('#exportForm');

            window.location.href = '/CSNet/ExportGatewayData?' + form.serialize();
        });


        $('#exportPush').click(function () {

            $('.divHistory').hide();
            $('.divNote').show();

        });

        $('#cancelExportPush').click(function () {
            $('.divNote').hide();
            $('.divHistory').show();
        });




        $('#ExternalDataSubscriptionType').change(function () { showHelp(); });
        showHelp();

        $('.configurations').accordion({
            collapsible: true,
            heightStyle: "content"
        });
        //heightStyle: "content" doesn't seem to be working so this removes the height from it
        $('.ui-accordion-content').css('height', '');

        $('#sendTest').click(function (e) {
            e.preventDefault();

            var href = "/CSNet/TestExternalConfiguration/<%: Model.GatewayID %>";
            href += "?formatUrl=" + encodeURIComponent($('#ConnectionInfo1').val());
            href += "&externalID=" + encodeURIComponent($('#ExternalID').val());
            $.post(href, function (data) {
                if (data.result == "Success") {
                    $('#testLink').html("<a href='" + data.data + "' target='_blank'>" + data.data + "</a>");
                } else {
                    alert(data.data);
                }
            });
        });
        $('#sendTestBody').click(function (e) {
            e.preventDefault();

            var href = "/CSNet/TestExternalConfigurationBody/<%: Model.GatewayID %>";
            href += "?formatUrl=" + encodeURIComponent($('#ConnectionInfo2').val());
            href += "&externalID=" + encodeURIComponent($('#ExternalID').val());
            $.post(href, function (data) {
                if (data.result == "Success") {
                    $('#testBody').html("<par href='" + data.data + "' target='_blank'>" + data.data + "</par>");
                } else {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
            });
        });
    });

    function showHelp() {
        $('.help').hide();
        $('#' + $('#ExternalDataSubscriptionType').val()).show();
    }
    function checkDelete() {
        if (confirm('Are you sure you want to delete this?')) {
            $.get('/CSNet/DeleteExternalConfiguration/<%:exdata.ExternalDataSubscriptionID %>', function (data) {

                if (data != 'Success') {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
                //  hideModal();
            });
        }
    }

    function SaveDataPush() {
        var formData = $('#datapush').serialize();

        $.ajax({
            type: 'POST',
            url: '/CSNet/ExternalConfiguration',
            data: formData,
            success: function (data) {



                if (data == "Success") {


                    $('.divHistory').show();
                    $('.divNote').hide();
                    //$("a.exportTab").closest().trigger('click');
                    // window.opener.$('.exportTab')

                }
                else {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
            }
        });
    }

    function resetBroken() {
        if (confirm('Are you sure you want to reset the broken count?')) {
            $.get('/Sensor/ResetBrokenExternalDataSubscription/<%:exdata.ExternalDataSubscriptionID%>', function (data) {
                if (data != 'Success') {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
                else {
                    $('#resetBrokenSend').hide();
                }
            });
        }
    }

    function getOrPost() {
        if ($("#verb").val() == "HttpPost") {
            $('.ContentHeaderDropdown').show();
        }
        else {
            $('.ContentHeaderDropdown').hide();
        }
    }

</script>
