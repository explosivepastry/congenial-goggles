<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.VisualMap>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EditMap
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<link href="../../Content/jquery.contextmenu.css" rel="stylesheet" type="text/css" />
	<script src="../../Scripts/OpenSeaDragon/openseadragon.js" type="text/javascript"></script>

	<%--<%VisualMap map = VisualMap.Load(Model.VisualMapID);%>--%>
	<div class="container-fluid">
		<div class="clearfix"></div>
		<div class="my-4">
			<%Html.RenderPartial("_MapLink", Model); %>
		</div>
		<div class="col-12">
			<div class="rule-card_container w-100">
				<div class="card_container__top">
					<div class="card_container__top__title">
						<%=Html.GetThemedSVG("map") %>&nbsp;
						<%: Html.TranslateTag("Map/EditMap|Map Edit","Map Edit")%>
					</div>
				</div>
				<form method="post" enctype="multipart/form-data" action="/Map/EditMap/<%: Model.VisualMapID %>">
					<% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
					<div class="x_content mapEdit">
						<%: Html.ValidationSummary(true) %>
						<div class="form-group">
							<div class="aSettings__title"><%: Html.TranslateTag("Map/EditMap|Map Name","Map Name")%></div>
							<div class="">
								<input type="text" id="mapName" name="Name" value="<%=Model.Name%>" class="form-control user-dets" style="width: 250px;" required >
								<%= Html.ValidationMessageFor(model => model.Name) %>
							</div>
							<div class="clearfix"></div>
						</div>
						<br />
						<% if (Model.MapType == eMapType.StaticMap)
								 { %>
						<div class="form-group" style="padding-bottom: 20px;" id="imageMapDiv">
							<div class="">
								<div class="aSettings__title"><%: Html.TranslateTag("Map/EditMap|Upload New Image","Upload New Image")%></div>
								<input type="file" id="ImageFile" name="ImageFile" class="btn btn-grey" style="display: none;" accept="image/*" />
								<div class=" dfac" id="fileBtn">
									<a class="btn btn-secondary btn-sm user-dets" style="width: fit-content;"><%: Html.TranslateTag("Map/EditMap|Select File","Select File")%></a>
									&nbsp;
									<label id="fileLabel"><%: Html.TranslateTag("Map/EditMap|No file selected","No file selected")%></label>
								</div>
								<%= Html.ValidationMessageFor(model => model.Image) %>
								<div class="clearfix"></div>
								<div>
									* New image upload won't be seen until browser cache has reset
								</div>
							</div>
							<%--<div class="col-lg-2 col-sm-2 col-xs-12"><img style="width:80px;height:80px;" id="mapPreview" src="data:image/png;base64,<%=Convert.ToBase64String(Model.Image) %>" ></div>--%>
						</div>
						<% } %>

						<div class="d-flex justify-content-end">
							<button onclick="event.preventDefault();removeMap('<%=Model.VisualMapID %>')" class="btn btn-light me-2 second-btn">
								<%: Html.TranslateTag("Map/EditMap|Delete Map","Delete Map")%>
								<%=Html.GetThemedSVG("delete") %>
							</button>
							<button class="btn btn-primary user-dets" type="submit" value="<%: Html.TranslateTag("Save","Save")%>">
								<%: Html.TranslateTag("Save","Save")%>
							</button>
						</div>
					</div>
				</form>
			</div>
		</div>
	</div>
	<!-- End Map View -->

<script type="text/javascript">
		$("#fileBtn").click(function () {
			$("#ImageFile").click();
		});

		$('input[type=file]').change(function (e) {
			var removePath = $('#ImageFile').val().replace(/^C:\\fakepath\\/i, '');
			$("#fileLabel").html(removePath);
		});

		$("#loadSnapshot").hide();

		function removeMap(item) {
			let values = {};
			values.url = `/Map/DeleteMap/${item}`;
			values.text = 'Are you sure you want to remove this map?';
			values.redirect = '/Map';
			openConfirm(values);
		}
	</script>

</asp:Content>
