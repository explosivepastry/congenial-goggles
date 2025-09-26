<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Gateway>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit Gateway Settings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<script type="text/javascript">
		var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
	</script>

	<%Html.RenderPartial("_GatewaySetup", Model); %>

</asp:Content>
