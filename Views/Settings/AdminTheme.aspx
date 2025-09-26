<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<List<Monnit.StyleGroup>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	AdminTheme
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<div class="formtitle">
		<div id="MainTitle" style="display: none;"></div>
		<div>
			<div class="" style="margin-top: 5px;">
				<%if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.IsCurrentCustomerAccountThemeAdmin)
					{ %>
				<div class="top-add-btn-row media_desktop">
					<a class="add-btn" href="/Settings/AdminThemeCreate">
						<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="16" height="15.999" viewBox="0 0 16 15.999">
							<defs>
								<clipPath id="clip-path">
									<rect width="16" height="15.999" fill="none" />
								</clipPath>
							</defs>
							<g id="Symbol_14_32" data-name="Symbol 14 – 32" clip-path="url(#clip-path)">
								<path id="Union_1" data-name="Union 1" d="M7,16V9H0V7H7V0H9V7h7V9H9v7Z" transform="translate(0)" fill="#fff" />
							</g>
						</svg>
						&nbsp; &nbsp; <%: Html.TranslateTag("Settings/AdminTheme|Add Theme","Add Theme")%>
					</a>
				</div>

				<div class="bottom-add-btn-row media_mobile">
					<a class="add-btn-mobile" href="/Settings/AdminThemeCreate">
						<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="16" height="15.999" viewBox="0 0 16 15.999">
							<defs>
								<clipPath id="clip-path">
									<rect width="16" height="15.999" fill="none" />
								</clipPath>
							</defs>
							<g id="Symbol_14_32" data-name="Symbol 14 – 32" clip-path="url(#clip-path)">
								<path id="Union_1" data-name="Union 1" d="M7,16V9H0V7H7V0H9V7h7V9H9v7Z" transform="translate(0)" fill="#fff" />
							</g>
						</svg>
					</a>
				</div>
				<%}%>
			</div>
		</div>
	</div>

	<div class="clearfix"></div>
	<% if (Model != null)
		{%>
	<div class="container card_container">
		<div class="card_container__top">
			<div class="card_container__top__title">Themes</div>
			<div class="col-xs-12 col-md-6 col-lg-6 dfjcfe dfac" id="newRefresh" style="margin-bottom: 5px;"></div>
		</div>

		<div class="card_container__body">
			<% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
			<% if (Model != null)
				{		
					foreach (var item in Model)
					{%>											
						<div class="gridPanel" style="height: 50px; border-bottom: 1px solid #e6e6e6; border-radius: 0px; padding-top: 6px;">
							<div class="col-lg-2 col-md-2 col-sm-3 col-xs-4" style="font-size: 1.0em;">
								<br />
								<b><%: item.Theme %></b>
							</div>

							<div class="col-lg-3 col-md-4 col-sm-3 col-xs-6 dfac" style="height: 100%;">
								<%: item.AccountThemeID %>
							</div>

							<div class="col-lg-4 col-md-3 col-sm-3 hidden-xs">
								<%: item.IsActive %>
							</div>

							<div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 dfjcfe" style="padding-top: 6px;">
								<%if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.IsCurrentCustomerAccountThemeAdmin)
									{ %>
								<a href="/Settings/AdminThemeEdit/<%:item.AccountThemeID%>">									
									<svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 10.425 10.425" style="font-size: 2em; padding-left: 7px;">
										<path id="Path_110" data-name="Path 110" d="M5.313,2,1.6,5.814,0,10.425l4.611-1.5L8.32,5.213Zm4.711-.3L8.721.4a1.215,1.215,0,0,0-1.8,0l-1.1,1.1L8.821,4.611l1.2-1.2a1.271,1.271,0,0,0,.4-.9A1.237,1.237,0,0,0,10.024,1.7Z" transform="translate(0 0)" class="icon-fill-grey"></path>
									</svg>
								</a>
								<%} %>
							</div>
						</div>
						<div class="clearfix"></div>
						<% }
					}
				}
				else
				{ %>
			<br />
			<%: Html.TranslateTag("Settings/AdminTheme|No Themes","No Themes")%>
			<%} %>

			<div class="clearfix"></div>
		</div>
	</div>

	<div class="clearfix"></div>
</asp:Content>
