<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Sensor>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Sensor Calibration
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <%Html.RenderPartial("SensorLink", Model); %>
        <div class="col-12">
            <div class="powertour-hook" id="hook-nine">
                <div class="powertour-hook" id="hook-eight">
                    <div class="x_panel rounded shadow-sm powertour-hook" id="hook-seven">
                        <div class="x_title">
                            <div class="card_container__top">
                                <div class="card_container__top__title">
                                    <%: Html.TranslateTag("Overview/SensorCalibrate|Calibrate Sensor","Calibrate Sensor")%>
                                </div>
                            </div>
                        </div>
                        <div class="x_content">
                            <%string ViewToFind = string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Calibrate", Model.ApplicationID.ToString("D3"));
                                if (MonnitViewEngine.CheckPartialViewExists(Request, ViewToFind, "Sensor", MonnitSession.CurrentTheme.Theme))
                                {
                                    ViewBag.returnConfirmationURL = ViewToFind;
                                    Html.RenderPartial("~\\Views\\Sensor\\" + ViewToFind + ".ascx", Model);
                                }
                                else
                                {
                                    Html.RenderPartial("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default\\Calibrate.ascx", Model);

                                }%>

                            <div class="clearfix"></div>
                            <div class="ln_solid"></div>

                            <%CalibrationCertificate cc = CalibrationCertificate.LoadBySensor(Model);
                              if (MonnitSession.CustomerCan("Can_Create_Certificate"))    // If user can upload Certificate permission
                              {
                                  if (cc != null && cc.CertificationValidUntil > DateTime.UtcNow)   // If Calibration Certificate exist, then Admin can edit.
                                  {%>
                                    <div>
                                        <a class="btn btn-secondary" href="/Overview/CalibrationCertificateEdit/<%: cc.CalibrationCertificateID %>"><%: Html.TranslateTag("Edit Calibration Certificate","Edit Calibration Certificate")%></a>
                                    </div>
                                <% }
                                else  // If Calibration Certificate Doesn't exist, then Admin can create.
                                { %>
                                    <div class="text-end">
                                        <a style="width:fit-content; margin-right:10px;" class="btn btn-secondary btn-sm" href="/Overview/CalibrationCertificateCreate/<%: Model.SensorID %>">
                                                <%: Html.TranslateTag("Create Calibration Certificate","Create Calibration Certificate")%>
                                        </a>
                                    </div>
                                <% }
                              }%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


<style>
    .form-control, .form-select {
        width: 250px;
    }
</style>

    <script>
        $('#actual').addClass('form-control');
    </script>

</asp:Content>

