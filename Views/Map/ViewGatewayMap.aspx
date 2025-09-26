<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<LocationMessage>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ViewGatewayMap
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
		/* Set the size of the div element that contains the map */
		#map {
		    height: 100%;
		    /* The height is 400 pixels */
		    width: 100%;
		    /* The width is the width of the web page */
		}
	</style>
    <%: Html.Partial("~/Views/Map/_GatewayMap.ascx", Model) %>
</asp:Content>