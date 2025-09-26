<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<RapidUpdateSensorModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    RapidUpdate
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
    
	.FirmwareBad {
		background-color:red;
		color: white;
    }
	
	.FirmwareGood{
		background-color: lightgreen;
	}

</style>
<h2>Sensor Check</h2>
	
	
    <div class="col-lg-4 col-md-6 col-xs-12">
        <div class="x_panel">
            <div style="font-size: 16px;">
				<%
                    string deviceVal = Request.QueryString["deviceVal"];
                    bool found = false;
                    if (!String.IsNullOrEmpty(deviceVal))
                    {
                        string[] deviceVals = deviceVal.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        if (deviceVal.Length > 1)
                        {
                            long deviceID = Convert.ToInt64(deviceVals[0]);
                            string code = deviceVals[1];

                            if (MonnitUtil.ValidateCheckDigit(deviceID, code))
                            {

                                System.Data.IDbCommand Cmd = Database.GetDBCommand(string.Format("MonnitK_FirmwareCheck @CustomerID = {0}, @DeviceID = {1}", MonnitSession.CurrentCustomer.CustomerID, deviceID));
                                System.Data.DataSet ds = Database.ExecuteQuery(Cmd);

                                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                {
                                    System.Data.DataRow dr = ds.Tables[0].Rows[0];
                                    found = true;
                                    %>
                                    <div class="<%:dr["RV"].ToBool() ? "FirmwareGood" : "FirmwareBad" %>">
                                        Sensor Name: <%:dr["DeviceName"] %>
                                        <br />
                                        Sensor Type: <%:dr["ApplicationName"] %>
                                        <br />
                                        Firmware Version: <%:dr["FirmwareVersion"] %>
                                    </div>
                                    <%
                                }
                            }
                        }

                        if (!found)
                        {
                        %>
                                    <div>
                                       Device Not Found
                                    </div>
                        <%
                        }
                    } %>
				<div>
					<div>Check Device</div>
					<input id="deviceCheck" />
				</div>
			</div>
        </div>
    </div>
    
	<script type="text/javascript">
        $(function () {

            $("#deviceCheck").on("keyup", function (event) {
                var deviceVal = $(this).val();
                if (event.keyCode === 13) {
                	window.location.href = ("/Network/RapidUpdate?deviceVal=" + deviceVal);
                }
			});

			$("#deviceCheck").focus();
        });


    </script>

</asp:Content>
