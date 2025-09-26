<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Account>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminEmailTemplate
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- page content -->
    <div class="container-fluid">
        <div class="dfac">
            <% Html.RenderPartial("_WhiteLabelLink", Model.getTheme()); %>
        </div>
        <div class="top-add-btn-row media_desktop pt-0 pb-1">
            <a class="btn btn-primary" href="/Settings/TemplateCreate/<%=Model.AccountID %>">
                <%=Html.GetThemedSVG("add") %> 
                <span class="ms-1"><%: Html.TranslateTag("Settings/AdminEmailTemplate|Add Template","Add Template")%></span>
            </a>
        </div>
        <div class="bottom-add-btn-row media_mobile">
            <a class="add-btn-mobile" href="/Settings/TemplateCreate/<%=Model.AccountID %>">
                <%=Html.GetThemedSVG("add") %>
            </a>
        </div>
        <div class="col-12">
            <div class="">
                <div class="x_panel shadow-sm rounded">
                    <div class="x_title">
                        <div class="card_container__top__title">
                            <%: Html.TranslateTag("Settings/AdminEmailTemplate|Templates","Templates")%>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content" id="templateList">

                        <% Html.RenderPartial("TemplateList", ViewData["Templates"]); %>
                    </div>
                </div>
                <div class="col-md-7 col-sm-7 col-xs-12">
                    <div id="templateDetail">

                    </div>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
