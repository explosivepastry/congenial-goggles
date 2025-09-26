<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();%>

<div class="col-12">
    <div class="powertour-hook" id="hook-nine">
        <div class="powertour-hook" id="hook-eight">
            <div class="x_panel rounded shadow-sm powertour-hook" id="hook-seven">
                <div class="x_title">
                    <div class="card_container__top">
                        <div class="card_container__top__title">
                             <%=Html.GetThemedSVG("calibrate") %> &nbsp;
                            <%: Html.TranslateTag("Testing/SensorCalibrate|Calibrate Sensor","Calibrate Sensor")%> &nbsp; -
                                  <span style="color: #0094ff">&nbsp;  <%:Model.SensorName %></span>
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
                        if (cc != null && CalibrationCertificationValidUntil > DateTime.MinValue)   // If Calibration Certificate exist, then Admin can edit.
                        {%>
                            <div>
                                <a class="btn btn-secondary" target="_blank" href="/Overview/CalibrationCertificateEdit/<%: cc.CalibrationCertificateID %>"><%: Html.TranslateTag("Testing/LoadSensorCalibrate|Edit Calibration Certificate","Edit Calibration Certificate")%></a>
                            </div>
                        <%}
                        else  // If Calibration Certificate Doesn't exist, then Admin can create.
                        {%>
                            <div class="text-end">
                                <a target="_blank" style="width: fit-content;" class="btn btn-secondary btn-sm" href="/Overview/CalibrationCertificateCreate/<%: Model.SensorID %>">
                                    <%: Html.TranslateTag("Testing/LoadSensorCalibrate|Create Calibration Certificate","Create Calibration Certificate")%>
                                </a>
                            </div>
                        <%}
                    }%>
                </div>
            </div>
        </div>
    </div>
</div>