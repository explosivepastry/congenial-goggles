<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="tabContainer">
    <ul>
        <%if(Model.GatewayType.SupportsRealTimeInterface){ %><li><a href="/CSNet/GatewayInterfaceRT/<%:Model.GatewayID %>">Real Time TCP Link Configuration</a></li><%}%>
        <%if(Model.GatewayType.SupportsModbusInterface){ %><li><a href="/CSNet/GatewayInterfaceMB/<%:Model.GatewayID %>">Modbus TCP Configuration</a></li><%}%>
        <%if(Model.GatewayType.SupportsSNMPInterface){ %><li><a href="/CSNet/GatewayInterfaceSNMP/<%:Model.GatewayID %>">SNMP Configuration</a></li><%}%>
    </ul>
</div>

<script type="text/javascript">
    $(function () {
        $(".tabContainer").tabs()
    });
</script>
