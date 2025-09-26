<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<iMonnit.Models.GatewayDataUsageModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Gateway Data Usage
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<%Gateway gw = ViewBag.Gateway;
	 Html.RenderPartial("GatewayLink", gw); %>

	<div style="width:100%; margin-right:-10px">
		<div class="formtitle">
			<%: Html.TranslateTag("Overview/GatewayDataUsage|Gateway Data Usage", "Gateway Data Usage")%>
		</div>
		 <table width="100%">
			<tr>
				<th style="width:20px;"></th>
				<th style="width:125px;">
					<%: Html.TranslateTag("Overview/GatewayDataUsage|Month", "Month")%>
				</th>
				<th style="text-align:right;">
					<%: Html.TranslateTag("Overview/GatewayDataUsage|Usage", "Usage")%>
				</th>
				<th style="width:20px;"></th>
			</tr>
		</table>
	</div>
	<div style="overflow:auto; width:100%; height:300px; margin-right:-10px">
		<table width="100%">
		<% foreach (var item in Model) { %>
    
			<tr>
				<td style="width:20px;"></td>
				<td style="width:130px;">
					<%: System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.Month) %>
					<%: item.Year %>
				</td>
				<td style="text-align:right;">
					<%: item.MB %> MB
				</td>
				<td style="width:20px;"></td>
			</tr>
    
		<% } %>

		</table>
	</div>
</asp:Content>