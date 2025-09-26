<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<AssignObjectModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	AssignMultipleDevices
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<div class="container-fluid">
	<h2><%: Html.TranslateTag("Network/AssignDevice|Assign Multiple Devices","Assign Multiple Devices")%></h2>
		<div class="col-12">
			<div class="x_panel shadow-sm rounded">
				<div class="x_title">
					<h2 style="background-color: white !important; overflow: unset;">
						<img src="../../Content/images/iconmonstr-plus-1-240.png" width="20px" height="auto" style="padding-bottom: 7px;">&nbsp;<b><%: Html.TranslateTag("Network/AssignDevice|Add Devices","Adding Devices to:")%></b>
						<br />
						<img src="../../Content/images/iconmonstr-sitemap-21-240-darkgrey.png" width="20px" height="auto" style="padding-bottom: 7px;">&nbsp;<b><%: Html.TranslateTag("Network/AssignDevice|Network","Network")%>: </b><span style="font-size: smaller;"><%=CSNet.Load(Model.NetworkID).Name %></span>
						<%if (Model.Networks.Count > 1)
							{ %>
						<a style="cursor: pointer;" href="/network/NetworkSelect?accountID=<%=Model.AccountID %>">
							<br />
							<img src="../../Content/images/iconmonstr-connection-8-240.png" width="20px" height="auto">&nbsp;&nbsp;<b><span style="font-size: 12px; margin-left: -5px;"><%: Html.TranslateTag("Network/AssignDevice|Switch Network","Switch Network")%></span></b></a>
						<%} %>
					</h2>

					<div class="nav navbar-right panel_toolbox">
					</div>
					<div class="clearfix"></div>
				</div>

				<div class="aSettings__title"><%: Html.TranslateTag("Network/AssignMultipleDevices|Download CSV Template","Download CSV Template")%></div>
				<div class="form-group">
					<div style="display: flex;">
						<a href="/Network/ExportMultiDeviceTemplate/">
							<input  class="btn btn-secondary btn-sm" type="button" value="<%: Html.TranslateTag("Network/AssignDevice|Download Template","Download Template")%>">
						</a>
					</div>
				</div>

				<form action="/Network/UploadDeviceCSV" method="post" enctype="multipart/form-data" id="fileUpload">
					<div class="aSettings__title"><%: Html.TranslateTag("Network/AssignMultipleDevices|Upload CSV File","Upload CSV File")%></div>
						<input required class="form-control form-control-sm" id="Upload" type="file" name="Upload" style="width: 300px;">
						<br />
						<div class="text-end">
							<input id="submitBtn" class="btn btn-primary" type="submit" value="<%: Html.TranslateTag("Network/AssignMultipleDevices|Upload","Upload")%>" />						
						</div>									
						
						<%: Html.ValidationMessageFor(model => model.ObjectID)%>
						<input type="text" name="acctID" value="<%: Model.AccountID %>" hidden>
						<input type="text" name="networkID" value="<%: Model.NetworkID %>" hidden>

						<div class="clearfix"></div>					
						<div class="bold" style="padding-left: 10px; font-weight: bold; color: red; font-size: 1.2em;" id="messageDiv">
						</div>
	                    </form>
					</div>

			</div>
		</div>

	<div id="results">
	</div>

	<script type="text/javascript">

		$(document).ready(function () {
			$('#submitBtn').click(function (e) {
				if ($('#Upload').get(0).files.length === 0) {
				}
				else {
					$('#submitBtn').hide();
                    $('#messageDiv').html(`Adding Devices <div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div> `)
				}
			});
		});
    </script>

</asp:Content>
