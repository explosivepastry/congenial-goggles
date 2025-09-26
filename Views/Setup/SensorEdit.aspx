<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Sensor>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit Sensor Settings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<script type="text/javascript">
		var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
	</script>
    <div class="container-fluid">
		<%Html.RenderPartial("_SensorSetup", Model); %>
    </div>

</asp:Content>