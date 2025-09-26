<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Email Templates
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="thirdSection">
        <div class="blockSectionTitle">
            <div class="blockTitle">Templates</div>
            <div style="clear: both;"></div>
        </div>
        <!-- Close Block Section Title -->
        <div class="thirdSectionInside">
            <div id="divTemplateList">
                <% Html.RenderPartial("../Email/TemplateList", ViewData["Templates"]); %>
            </div>
        </div>
        <%if (MonnitSession.IsCurrentCustomerMonnitAdmin)
            { %>
        <div class="buttons">
            <a href="/Email/TemplateCreate/<%: RouteData.Values["id"] %>" onclick="getMain($(this).attr('href'), 'New Email Template'); return false;" class="greybutton">New Template</a>
            <div style="clear: both;"></div>
        </div>
        <%} %>
    </div>

    <div class="two-thirdSection">
        <div class="blockSectionTitle">
            <div class="blockTitle" id="MainTitle">&nbsp;</div>
            <div style="margin-top: 25px; margin-bottom: 5px;">
                <div id="MainRefresh"><a href="#" onclick="getMain(); return false;">
                    <img src="<%:Html.GetThemedContent("/images/refresh.png")%>" alt="Refresh" title="Refresh" /></a></div>
            </div>
            <div style="clear: both;"></div>
        </div>
        <!-- End blockSectionTitle -->

        <div class="two-thirdSectionInside">
            <div id="Main"></div>
            <div id="MainLoading">
                <div id="loadingGIF" class="text-center" style="display: none;">
                    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
                        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
                    </div>
                </div>
            </div>
        </div>
        <!-- End two-thirdSection Inside -->
    </div>
    <!-- End two-thirdSection -->

    <link href="/suneditor/suneditor.min.css" rel="stylesheet" />
    <script type="text/javascript" src="/suneditor/suneditor.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            getMain('/Overview/Blank', '', true);
        });
    </script>
</asp:Content>
