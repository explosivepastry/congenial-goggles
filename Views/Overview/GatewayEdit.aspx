<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Gateway>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Gateway Settings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>		
        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';

    </script>
    <div class="container-fluid">

        <%Html.RenderPartial("GatewayLink", Model); %>

        <div class="col-12 px-0">
            <div class="rule-card_container w-100" id="hook-five">
                <div class="x_title">
                    <div class="card_container__top">
                        <div class="card_container__top__title d-flex justify-content-between">
                            <div class="col-sm-3 col-6">
                                <%: Html.TranslateTag("Overview/GatewayEdit|Gateway Configuration","Settings")%>
                            </div>
                            <div class="nav navbar-right panel_toolbox">
                                <!-- help button  gatewayedit-->
                                <a class="helpIco" style="cursor: pointer!important;" data-bs-toggle="modal" title="<%: Html.TranslateTag("Overview/SensorEdit|Sensor Help","Sensor Help") %>" data-bs-target=".pageHelp">
                                    <div class="help-hover"><%=Html.GetThemedSVG("circleQuestion") %></div>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content powertour-hook" id="hook-six">

                    <% 
                        //If specific type edit view exists use that page
                        string ViewToFind = string.Format("GatewayEdit\\type_{0}\\Edit", Model.GatewayTypeID.ToString("D3"));
                        if (MonnitViewEngine.CheckPartialViewExists(Request, ViewToFind, "Gateway", MonnitSession.CurrentTheme.Theme))
                        {
                            ViewBag.returnConfirmationURL = ViewToFind;
                            Html.RenderPartial("~\\Views\\Gateway\\" + ViewToFind + ".ascx", Model);
                        }
                        else
                        {
                            Html.RenderPartial("~\\Views\\Gateway\\GatewayEdit\\Default\\Edit.ascx", Model);
                        }
                    %>
                </div>
            </div>
        </div>
    </div>
    <!-- help button modal -->
    <div class="modal fade pageHelp" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">

                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel2"><%: Html.TranslateTag("Overview/GatewayEdit|Gateway Edit","Gateway Edit")%></h4>

                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                    </button>
                </div>
                <div class="modal-body">

                    <% 
                        //If specific type edit view exists use that page

                        string HelpViewToFind = string.Format("GatewayEdit\\type_{0}\\Help", Model.GatewayTypeID.ToString("D3"));
                        if (MonnitViewEngine.CheckPartialViewExists(Request, HelpViewToFind, "Gateway", MonnitSession.CurrentTheme.Theme))
                        {
                            ViewBag.returnConfirmationURL = HelpViewToFind;
                            Html.RenderPartial("~\\Views\\Gateway\\" + ViewToFind + ".ascx", Model);
                        }
                        else
                        {
                            Html.RenderPartial("~\\Views\\Gateway\\GatewayEdit\\Default\\Help.ascx", Model);

                        }

                    %>
                </div>
                <div class="modal-footer">
                </div>

            </div>
        </div>
    </div>
    <!-- End help button modal -->
    <style>
        .help-hover svg {
            fill: var(--help-highlight-color);
            width: 30px;
            height: 30px;
        }
    </style>
</asp:Content>


