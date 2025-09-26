<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<style>
    .tableRowNoBorder th {
        border: none;
        padding-bottom: 0px;
    }
</style>

<div class="divHistory">
    <div class="formtitle">
        Export sensor data
        <%Html.RenderPartial("HistoryDatePicker"); %>
    </div>
    <form id="exportForm" action="/Sensor/ExportData" method="post">
        <div class="formBody ">
            <label style="font-size: x-small; float: right">Only the first 5000 records within the given date range will be exported.</label>

            <%: Html.ValidationSummary(false)%>
            <%: Html.Hidden("id", Model.SensorID)%>
            <table>
                <tr>
                    <td colspan="4">
                        <input type="radio" title="This Sensor" id="uxExportAll_Sensor" name="uxExportAll" value="Sensor" checked="checked" /><label for="uxExportAll_Sensor">Data from this sensor</label></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <input type="radio" title="Include Data from Network Sensors" id="uxExportAll_Network" name="uxExportAll" value="Network" /><label for="uxExportAll_Network">Data from all sensors in network</label></td>
                </tr>
                <%if (MonnitSession.CurrentCustomer.IsAdmin)
                    { %>
                <tr>
                    <td colspan="4">
                        <input type="radio" title="Include Data from Account Sensors" id="uxExportAll_Account" name="uxExportAll" value="Account" /><label for="uxExportAll_Account">Data from all sensors in account</label></td>

                </tr>
                <tr>
                    <td style="width: 102px; padding-left: 2px; padding-bottom: 16px; padding-right: 0px">
                        <h6 style="margin-bottom: 0; margin-top: 0">Fields of data to export:</h6>
                    </td>
                </tr>
                <% } %>
            </table>
            <table style="border: 5px solid #DDDDDD; width: 100%;">
                <thead>
                    <tr class="tableRowNoBorder">
                        <th style="color: #666666;">DataMessage GUID</th>
                        <th style="color: #666666;">SensorID</th>
                        <th>Sensor</th>
                        <th>Date</th>
                        <th>Value</th>
                        <th>Formatted</th>
                        <th>Battery</th>
                        <th>Raw</th>
                        <th>Sensor</th>
                        <th>GatewayID</th>
                        <th>Alert</th>
                        <th>Signal</th>
                        <th>Voltage</th>
                        <%if (MonnitApplicationBase.HasSpecialExportValue(Model.ApplicationID))
                            {
                                string spec2 = "none";
                        %>
                        <th>Special</th>
                        <%}%>
                    </tr>
                    <tr>
                        <th colspan="2" style="font-style: italic; text-align: center; font-weight: normal; color: #666666;">(required)</th>
                        <th>Name</th>
                        <th colspan="1"></th>
                        <th colspan="1"></th>
                        <th>Value</th>
                        <th colspan="1"></th>
                        <th>Data</th>
                        <th>State</th>
                        <th colspan="1"></th>
                        <th>Sent</th>
                        <th>Strength</th>
                        <th colspan="1"></th>
                        <%if (MonnitApplicationBase.HasSpecialExportValue(Model.ApplicationID))
                            {
                                string spec1 = "none";
                        %>
                        <th>Value</th>
                        <%}%>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style="padding-left: 29px">
                            <input type="checkbox" disabled checked /><!--DataMessageGUID --></td>
                        <%--<td style="padding-left: 29px">
                            <input type="checkbox" disabled checked /><!--MessageID --></td>--%>
                        <td style="padding-left: 22px">
                            <input type="checkbox" disabled checked /><!--SensorID --></td>
                        <td style="padding-left: 15px">
                            <input type="checkbox" name="Sensor_Name" checked /><!--SensorName --></td>
                        <td style="padding-left: 11px">
                            <input type="checkbox" name="Date" checked /><!--Date--></td>
                        <td style="padding-left: 19px">
                            <input type="checkbox" name="Value" checked /><!--Value--></td>
                        <td style="padding-left: 31px">
                            <input type="checkbox" name="Formatted_Value" checked /><!--Formatted Value--></td>
                        <td style="padding-left: 23px">
                            <input type="checkbox" name="Battery" checked /><!--Battery--></td>
                        <td style="padding-left: 13px">
                            <input type="checkbox" name="Raw_Data" /><!--Raw Data--></td>
                        <td style="padding-left: 17px">
                            <input type="checkbox" name="Sensor_State" /><!--State--></td>
                        <td style="padding-left: 29px">
                            <input type="checkbox" name="GatewayID" /><!--GatewayID--></td>
                        <td style="padding-left: 13px">
                            <input type="checkbox" name="Alert_Sent" /><!--Alert--></td>
                        <td style="padding-left: 25px">
                            <input type="checkbox" name="Signal_Strength" /><!--Signal Strength--></td>
                        <td style="padding-left: 20px">
                            <input type="checkbox" name="Voltage" /><!--Voltage--></td>
                        <%if (MonnitApplicationBase.HasSpecialExportValue(Model.ApplicationID))
                            {
                                string spec = "none";
                        %>
                        <td style="padding-left: 18px">
                            <input type="checkbox" name="Special" /><!--Special--></td>

                        <%} %>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="buttons" style="margin-top: 38px;">
            <input type="button" id="export" value="Export data to CSV file" class="bluebutton" style="float: none;" />
            <!--<a href="/Content/Documentation/Sensor_Export_Definitions.pdf" target="_blank">
                <img alt="help" style="width: 16px;" title="Sensor data export column definitions." src="<%:Html.GetThemedContent("/images/help.png")%>" /></a>-->
            <div style="clear: both;"></div>
        </div>

    </form>


</div>
<% if (!string.IsNullOrEmpty(Model.ExternalID))
    { %>
<div class="divHistory">
    <div class="formBody">This sensor can also be configured to export data in real time to a 3rd party website.  The data is sent via http get request using query string parameters and is sent at the same time the data is received at the server.  There is an extensive list of parameters that can be passed allowing you to send detailed information about both the data and the sensor.</div>
    <div class="buttons">
        <%if (MonnitSession.AccountCan("can_webhook_push"))
            { %>
        <input type="button" id="ExportPush" value="Configure data push" class="greybutton" />
        <%}
            else
            { %>
        <a href="#" onclick="showSimpleMessageModal("<%=Html.TranslateTag("Only available to premiere users")%>"); return false;" class="greybutton">Configure data push</a>
        <%} %>

        <div style="clear: both;"></div>
    </div>
</div>
<%} %>
<div class="divNote">
    <div class="formtitle">Configure data push</div>
    <div style="margin: 25px 25px 25px 25px;">
        <style>
            h3.ui-helper-reset {
                padding: 5px 0px 5px 25px;
            }
        </style>
        <%
            ExternalDataSubscription exdata = ExternalDataSubscription.LoadBySensorID(Model.SensorID).FirstOrDefault();

            if (exdata == null)
            {
                exdata = new ExternalDataSubscription() { AccountID = Model.AccountID };
            }

        %>
        <p style="color: red;">The DataPush service is deprecated and will continue to function through July 2016. To continue to utilize this functionality, transition code to the new <a href="/RestAPI/webhook">Webhook</a> available now.</p>
        <div class="help">
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
                        <td>Text value for use as identifier in external systems</td>
                    </tr>
                    <tr>
                        <td>{1}</td>
                        <td>MessageID</td>
                        <td>Transaction ID for this message from the sensor  
                            <img alt="help" style="width: 16px;" title="MessageID is being phased out in lieu DataMessageGUID by Summer 2016.DataMessageGUID will not be sequential but will have a reasonable expectation uniqueness." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
                    </tr>
                    <tr>
                        <td>{2}</td>
                        <td>Data</td>
                        <td>Data transmitted with the message</td>
                    </tr>
                    <tr>
                        <td>{3}</td>
                        <td>DisplayData</td>
                        <td>Data translated to ui value</td>
                    </tr>
                    <tr>
                        <td>{4}</td>
                        <td>MessageDate</td>
                        <td>Date message was delivered</td>
                    </tr>
                    <tr>
                        <td>{5}</td>
                        <td>Battery</td>
                        <td>Approximate percentage remaining on battery</td>
                    </tr>
                    <tr>
                        <td>{6}</td>
                        <td>SignalStrength</td>
                        <td>Strength of radio signal (0-100)</td>
                    </tr>
                    <tr>
                        <td>{7}</td>
                        <td>State</td>
                        <td>Encoded state data (in general 0 = Normal, 2=Aware State)
                        <a href="/Content/Documentation/Sensor_Export_Definitions.pdf" target="_blank">
                            <img alt="help" style="width: 16px;" title="Sensor data export column definitions." src="<%:Html.GetThemedContent("/images/help.png")%>" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td>{8}</td>
                        <td>SensorID</td>
                        <td>Sensor Identifier</td>
                    </tr>
                    <tr>
                        <td>{9}</td>
                        <td>SensorName</td>
                        <td>Name set up for sensor</td>
                    </tr>
                    <tr>
                        <td>{10}</td>
                        <td>AccountID</td>
                        <td>Account Identifier</td>
                    </tr>
                    <tr>
                        <td>{11}</td>
                        <td>CSNetID</td>
                        <td>Customer Sensor Network ID</td>
                    </tr>
                    <tr>
                        <td>{12}</td>
                        <td>FirmwareVersion</td>
                        <td>Version of Firmware on Sensor</td>
                    </tr>
                    <tr>
                        <td>{13}</td>
                        <td>CanUpdate</td>
                        <td>has pending configuration transaction to take place</td>
                    </tr>
                    <tr>
                        <td>{14}</td>
                        <td>IsActive</td>
                        <td>0 = No notifications will be sent from this sensor, 1 = Notifications will be sent</td>
                    </tr>
                    <tr>
                        <td>{15}</td>
                        <td>Not Used</td>
                        <td></td>
                        <%--IsSleeping – Sensor was in a sleeping state(Off)--%>
                    </tr>
                    <tr>
                        <td>{16}</td>
                        <td>ApplicationID</td>
                        <td>Coded Sensor type (2 = Temperature, 4=Water, etc)</td>
                    </tr>
                    <tr>
                        <td>{17}</td>
                        <td>GatewayID</td>
                        <td>Gateway Identifier that delivered the message from the sensor</td>
                    </tr>
                    <tr>
                        <td>{18}</td>
                        <td>Plot Value</td>
                        <td>Numerical value assigned to message</td>
                    </tr>
                    <tr>
                        <td>{19}</td>
                        <td>DataMessageGUID</td>
                        <td>Transaction ID for this message from the sensor</td>
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
                        <td>http://YourDomain/DataFromSensor/?ID={0}&amp;Data={2}&amp;TransactionId={1}</td>
                    </tr>
                    <tr>
                        <td>Http Get Request:</td>
                        <td>http://YourDomain/DataFromSensor/?ID=3SX7Z&amp;Data=26&amp;TransactionId=103254</td>
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
                        <td>http://YourDomain/DataFromSensor/</td>
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
                        <td>http://YourDomain/DataFromSensor/</td>
                        <tr>
                            <td>Uploaded Data: </td>
                            <td>ID=3SX7Z&amp;Data=26&amp;TransactionId=103254</td>
                        </tr>
                    </tr>
                </table>
            </div>
            <%:Html.Partial("WebServerExample") %>
        </div>

        <div style="clear: both;"></div>
        <br />
    </div>
    <div class="formtitle" style="height: 32px">
        <span>Parameters</span> <span align="right">
            <input style="margin: 2px 25px -16px -3px;" type="button" id="cancelExportPush" value="Cancel" class="greybutton" /><%--//FIXTHIS--%></span>
    </div>
    <form action="/Sensor/ExternalConfiguration" id="datapush" method="post">



        <%: Html.ValidationSummary(true) %>
        <%: Html.Hidden("ExternalDataSubscriptionID",exdata.ExternalDataSubscriptionID) %>
        <%: Html.Hidden("SensorID",Model.SensorID) %>
        <%: Html.Hidden("AccountID",Model.AccountID) %>

        <input type="hidden" name="ExternalDataSubscriptionType" id="ExternalDataSubscriptionType" value="Generic_URL" />


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
            <div class="editor-field">
                <select class="GetOrPost" id="verb" name="verb" onchange="getOrPost(this.value);">
                    <option value="HttpGet" <%:exdata.verb=="HttpGet" || exdata.verb=="get" ? "selected" : ""%>>HttpGet</option>
                    <option value="HttpPost" <%:exdata.verb=="HttpPost" || exdata.verb=="post" ? "selected" : ""%>>HttpPost</option>
                </select>
            </div>
        </div>

        <div class="ContentHeaderDropdown" id="ContentHeaderDiv" style="<%: exdata.verb=="HttpGet" || exdata.verb=="get" ? "display: none;": ""%>">
            <div class="editor-label">
                Content Header Type (required):
            </div>
            <div class="editor-field">
                <select class="ContentHeader" id="ContentHeaderType" name="ContentHeaderType">
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
            <%:  (exdata.LastResult) %>
        </div>
        <div class="editor-error">
        </div>
        <%if (exdata.BrokenCount > 0)
            {%>
        <div class="editor-label">
            Consecutive Fails
        </div>
        <div class="editor-field">
            <%: exdata.BrokenCount%>
        </div>
        <div class="editor-error">
        </div>
        <%}%>

        <div class="buttons" style="margin: 0px -1px 0px -2px;">
            <%if (exdata != null && exdata.ExternalDataSubscriptionID > 0)
                { %>
            <div class="BrokenCount" style="float: left; margin: 10px 5px 10px 5px; color: #2D4780;">
                <%if (exdata.DoSend)
                    {%>Sending: Enabled <%}%><%else
                {%>Sending: Disabled<%}%><%if (exdata.DoRetry)
                                             {%><br />
                Retries: Enabled <%}%><%else
                                          {%><br />
                Retries: Disabled <%}%>
            </div>
            <% } %>
            <%--  <input type="button" onclick="SaveDataPush()" value="Save" class="bluebutton" />//--%>
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

            window.location.href = '/Sensor/ExportData?' + form.serialize();
        });



        $('#ExportPush').click(function () {

            $('.divHistory').hide();


            $('.divNote').show();

        });

        $('#cancelExportPush').click(function () {
            $('.divNote').hide();
            $('.divHistory').show();
        });

        $('#ExternalDataSubscriptionType').change(function () { showHelp(); });
        showHelp();

        $('.help').accordion({
            collapsible: true,
            heightStyle: "content"
        });
        //heightStyle: "content" doesn't seem to be working so this removes the height from it
        $('.ui-accordion-content').css('height', '');

        $('#sendTest').click(function (e) {
            e.preventDefault();

            var href = "/Sensor/TestExternalConfiguration/<%: Model.SensorID %>";
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

            var href = "/Sensor/TestExternalConfigurationBody/<%: Model.SensorID %>";
            href += "?formatUrl=" + encodeURIComponent($('#ConnectionInfo2').val());
            href += "&externalID=" + encodeURIComponent($('#ExternalID').val());
            $.post(href, function (data) {
                if (data.result == "Success") {
                    $('#testBody').html("<par href='" + data.data + "' target='_blank'>" + data.data + "</par>");
                } else {
                    alert(data.data);
                }
            });
        });
    });
    function showHelp() {
        //$('.help').hide();
        $('#' + $('#ExternalDataSubscriptionType').val()).show();
    }
    function checkDelete() {
        if (confirm('Are you sure you want to delete this?')) {
            $.get('/Sensor/DeleteExternalConfiguration/<%:exdata.ExternalDataSubscriptionID %>', function (data) {

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
            url: '/Sensor/ExternalConfiguration',
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
        if (confirm('Reset broken count to 20?')) {
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
