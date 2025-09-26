<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<LocationMessage>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Gateway History
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%
        Gateway gw = ViewData["Gateway"] as Gateway;
    %>

    <style type="text/css">
		/* Set the size of the div element that contains the map */
		#map {
		    height: 500px;/*100%;*/
		    /* The height is 400 pixels */
		    width: 100%;
		    /* The width is the width of the web page */
		}
	</style>

    <div class="container-fluid">
        <% if (Request.Url.PathAndQuery.Contains("newDevice=true"))
            { %>
        <div class="col-12 alert alert-success sensor_chart_add_new" role="alert">
            <p style="font-size: 1rem;"><%: Html.TranslateTag("Overview/GatewayGPS|Congrats on adding your new gateway","Congrats on adding your new gateway")%>.</p>
            <button class="btn btn-primary btn-sm" id="add_new_btn" style="background-color: #0067ab;">
                <%=Html.GetThemedSVG("add") %>
            <%: Html.TranslateTag("Overview/GatewayGPS|Add Another Device","Add Another Device")%>
            </button>
        </div>
        <% } %>
        <%Html.RenderPartial("GatewayLink", gw); %>
        
    <%: Html.Partial("~/Views/Map/_GatewayMap.ascx", Model) %>
    </div>

</asp:Content>
