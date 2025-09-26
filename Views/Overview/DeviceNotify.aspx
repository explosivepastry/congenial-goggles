<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Sensor>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DeviceNotify
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <%Html.RenderPartial("SensorLink", Model); %>

        <div class="col-12">
            <div class="rule-card_container" style="width:100%;">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <%: Html.TranslateTag("Overview/SensorScale|Pending Message History","Pending Message History")%>
                    </div>
                </div>
                <div class="x_content">

                    <%string ViewToFind = string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Notify", Model.ApplicationID.ToString("D3"));
                        if (MonnitViewEngine.CheckPartialViewExists(Request, ViewToFind, "Sensor", MonnitSession.CurrentTheme.Theme))
                        {
                            ViewBag.returnConfirmationURL = ViewToFind;
                            Html.RenderPartial("~\\Views\\Sensor\\" + ViewToFind + ".ascx", Model);
                        }
                        else
                        {
                            Html.RenderPartial("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default\\Notify.ascx", Model);

                        }
                    %>

                    <div class="clearfix"></div>
                    <div class="ln_solid"></div>
                </div>
            </div>
        </div>
        <br />
        <div class="col-12">
            <div class="rule-card_container">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <%: Html.TranslateTag("Overview/SensorScale|Attach Sensor Messages","Attach Sensor Messages")%>
                    </div>
                    <div class="col-sm-4 col-12">
                        <div class="deviceSearch">
                            <div class="searchInput">
                                <input class="form-control user-dets" id="userFilter" name="userFilter" type="text" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="x_content" style="height:300px;">

                    <%string AttachMessageViewToFind = string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\AttachMessage", Model.ApplicationID.ToString("D3"));
                        if (MonnitViewEngine.CheckPartialViewExists(Request, ViewToFind, "Sensor", MonnitSession.CurrentTheme.Theme))
                        {
                            ViewBag.returnConfirmationURL = AttachMessageViewToFind;
                            Html.RenderPartial("~\\Views\\Sensor\\" + AttachMessageViewToFind + ".ascx", Model);
                        }
                        else
                        {
                            Html.RenderPartial("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default\\AttachMessage.ascx", Model);

                        }
                    %>

                    <div class="clearfix"></div>
                    <div class="ln_solid"></div>
                </div>
            </div>
        </div>
    </div>


    <!-- help button modal -->
    <%--    <div class="modal fade pageHelp" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title" id="myModalLabel2"><%: Html.TranslateTag("Overview/SensorScale|Sensor Scale Settings","Overview/SensorScale|Sensor Scale Settings")%></h4>
                </div>
                <div class="modal-body">
                    <p><%: Html.TranslateTag("Overview/Control|Send Commands to the Control Unit.","Overview/Control|Send Commands to the Control Unit.")%></p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal"><%: Html.TranslateTag("Close","Close")%></button>
                </div>

            </div>
        </div>
    </div>--%>
    <!-- End help button modal -->

</asp:Content>
