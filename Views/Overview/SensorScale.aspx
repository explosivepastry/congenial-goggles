<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Sensor>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Sensor Scale
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%
        bool sensorHasScaleHelpModal = false;
        string HelpViewToFind = string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\ScaleSettingsHelp", Model.ApplicationID.ToString("D3"));

        if (MonnitViewEngine.CheckPartialViewExists(Request, HelpViewToFind, "Sensor", MonnitSession.CurrentTheme.Theme))
        {
            sensorHasScaleHelpModal = true;
            ViewBag.returnConfirmationURL = HelpViewToFind;
        }
    %>

    <div class="container-fluid">
        <%Html.RenderPartial("SensorLink", Model); %>
        <div class="col-12">
            <div class="rule-card_container w-100" id="hook-seven">
                <div class="card_container__top__title" style="justify-content: space-between;">
                    <div><%: Html.TranslateTag("Overview/SensorScale|Sensor Scale Settings","Sensor Scale Settings")%></div>

                    <%if (sensorHasScaleHelpModal)
                        {%>

                    <a class="helpIco" style="cursor: pointer!important;" data-bs-toggle="modal" title="<%: Html.TranslateTag("Overview/SensorEdit|Sensor Help","Sensor Help") %>" data-bs-target=".pageHelp">
                        <div class="help-hover" style="padding: 0.5rem;">
                            <%=Html.GetThemedSVG("circleQuestion") %>
                        </div>
                    </a>
                    <%}%>
                </div>
                <div class="x_content">

                    <%string ViewToFind = string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Scale", Model.ApplicationID.ToString("D3"));
                        if (MonnitViewEngine.CheckPartialViewExists(Request, ViewToFind, "Sensor", MonnitSession.CurrentTheme.Theme))
                        {
                            ViewBag.returnConfirmationURL = ViewToFind;
                            Html.RenderPartial("~\\Views\\Sensor\\" + ViewToFind + ".ascx", Model);
                        }
                        else
                        {
                            Html.RenderPartial("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default\\Scale.ascx", Model);

                        }
                        %>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>

    <!-- help button modal -->
    <div class="modal fade pageHelp" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel2"><%: Html.TranslateTag("Overview/SensorScale|Sensor Scale Settings","Sensor Scale Settings")%></h4>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">

                    <% if (sensorHasScaleHelpModal)
                        { %>
                    <% Html.RenderPartial("~\\Views\\Sensor\\" + HelpViewToFind + ".ascx", Model); %>
                    <% } %>
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


