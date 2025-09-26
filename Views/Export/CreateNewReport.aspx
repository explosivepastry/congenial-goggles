<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<ReportSchedule>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create New Report
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div class="container-fluid">
        <div class="formtitle" style="ps-4">
            <h2 style="padding-top: 20px;"><%: Html.TranslateTag("Export/CreateNewReport|Create New Report","Create New Report") %></h2>
			<div><%: Html.TranslateTag("Category") %>: <%:ViewBag.ReportTypeName%></div>
			<div><%: Html.TranslateTag("Template") %>: <%:ViewBag.ReportQueryName%></div>
			<div><%: Html.TranslateTag("Description") %>: <%:ViewBag.ReportQueryDescription %></div>
        </div>
        <%:Html.Partial("_BuildReport") %>
    </div>
    
	<script type="text/javascript">
		$(function() {
			$('#mainBuildReport').css('overflow-y', 'hidden')
								.css('max-height', '');
			$('.formtitle').css('margin-top', '-6px');
		});
	</script>
</asp:Content>