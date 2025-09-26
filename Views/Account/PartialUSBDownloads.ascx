<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Gateway>>" %>


    <%
        List<Gateway> USBPre21 = new List<Gateway>();
        List<Gateway> USBPost21 = new List<Gateway>();
        foreach (Gateway g in Model)
        {
            if (g.GatewayTypeID == 1 || g.GatewayTypeID == 2)
            {
                if (new Version(g.APNFirmwareVersion) >= new Version("2.0.1.0"))
                    USBPost21.Add(g);
                else
                    USBPre21.Add(g);
            }
        }
    if(USBPre21.Count > 0 || USBPost21.Count > 0){ %>
<div class="formtitle">Downloads</div>
    <div class="formBody">
	    <table width="100%">
            
            <tr>
                <td width="49%"><p><strong>USB Gateway Application</strong></p></td>
                <td width="2%">&nbsp;</td>
                <td width="49%"><p><strong>USB Gateway Driver</strong></p></td>
            </tr>
            
            <tr>
                <td width="49%" valign="top"><a href="<%: Html.GetThemedContent("USBGateway/USB_Gateway_Utility_Setup_v3.3.2.1.exe")%>" title="USB Gateway Installer">USB Gateway Installer</a></td>
                <td width="2%">&nbsp;</td>
                <td width="49%" valign="top">
                    <%if(USBPre21.Count > 0) { %>
                    <div>
                        <a href="<%: Html.GetThemedContent("Drivers/WirelessSensorDriver-3.3.0-X64.exe")%>" title="64 bit driver 3.3">Version 3.3 Driver for 64 bit Windows</a>
                    </div>
                    <div>
                        <a href="/Content/Drivers/WirelessSensorDriver-3.3.0-X86.exe" title="32 bit driver 3.3">Version 3.3 Driver for 32 bit Windows</a>
                    </div>
                    <div>
                        <br />
                        For use with the following gateways:
                    </div>
                    <ul>
                        <%foreach(Gateway g in USBPre21) { %>
                        <li><%:g.Name %></li>
                        <%}%>
                    </ul>
                    <%}%>
                                        
                    <%if(USBPost21.Count > 0) { %>
                    
                    <div>
                        &nbsp;&nbsp;&nbsp;&nbsp;<a href="/Content/Drivers/USB_Gateway_Driver_2.3.3.msi" title="32 and 64 bit driver">USB Driver for 32 and 64 bit Windows</a>
                    </div>
                    <div>
                        <br />
                        For use with the following gateways:
                    </div>
                    <ul>
                    <%foreach (Gateway g in USBPost21) { %>
                    <li><%:g.Name %></li>
                    <%}%>
                    </ul>
                    <%}%>
                </td>
            </tr>
        </table>
	    
	</div>
	<%}%>

                                  