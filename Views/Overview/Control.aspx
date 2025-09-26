<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Sensor>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Control
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <%Html.RenderPartial("SensorLink", Model); %>

        <div class="col-12">
            <div class="x_panel shadow-sm rounded">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <%: Html.TranslateTag("Overview/SensorScale|Control Unit Commands","Control Unit Commands")%>
                    </div>
                </div>
                <div class="x_content">

                    <%string ViewToFind = string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Control", Model.ApplicationID.ToString("D3"));
                        if (MonnitViewEngine.CheckPartialViewExists(Request, ViewToFind, "Sensor", MonnitSession.CurrentTheme.Theme))
                        {
                            ViewBag.returnConfirmationURL = ViewToFind;
                            Html.RenderPartial("~\\Views\\Sensor\\" + ViewToFind + ".ascx", Model);
                        }
                        else
                        {
                            Html.RenderPartial("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default\\Control.ascx", Model);

                        }
                    %>

                    <div class="clearfix"></div>
                    <div class="ln_solid"></div>
                </div>
            </div>
        </div>
    </div>


    <!-- help button modal -->
    <div class="modal fade pageHelp" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">

                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel2"><%: Html.TranslateTag("Overview/SensorScale|Sensor Scale Settings","Overview/SensorScale|Sensor Scale Settings")%></h4>

                    <button type="button" class="btn-close" data-dismiss="modal" aria-label="Close">                  
                    </button>
                </div>
                <div class="modal-body">
                    <p><%: Html.TranslateTag("Overview/Control|Send Commands to the Control Unit.","Overview/Control|Send Commands to the Control Unit.")%></p>
                </div>
                <div class="modal-footer">
          
                </div>

            </div>
        </div>
    </div>
    <!-- End help button modal -->

</asp:Content>
