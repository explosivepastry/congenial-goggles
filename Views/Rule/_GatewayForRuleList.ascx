<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Gateway>>" %>



<div style="margin-top: 0px; ">
	<div style="display:flex; flex-wrap:wrap; padding:5px;">
		    <%
      

            foreach (var item in Model)
            {
                if (item.GatewayTypeID == 10 || item.GatewayTypeID == 11 || item.GatewayTypeID == 35 || item.GatewayTypeID == 36 || item.GatewayTypeID == 38)//don't show wifi gateways here
                    continue;

                GatewayMessage lastGatewayMessage = GatewayMessage.LoadLastByGateway(item.GatewayID);%>


		<a class="card-rule" href="/Rule/ChooseSensorTemplate/<%=item.GatewayID %>?isGateway=true" style="height:auto;">

			<div class="card-rule__container">
				<div class="hidden-xs ruleDevice__icon">
	
					  <div class="icon gwicon "> <%=Html.GetThemedSVGForGateway(item.GatewayTypeID) %></div>
				</div>
				<div class="triggerDevice__name2">
					<strong><%=item.Name %></strong>
				</div>
			</div>
		</a>


		<%} %>
	</div>
</div>

<script type="text/javascript">
	
	$(document).ready(function () {
<%--		$('#filterdGateways').html('<%:gatewaylist.Count%>');
		$('#totalGateways').html('<%:ViewBag.TriggerGateways.Count%>');		--%>
	});		
</script>
