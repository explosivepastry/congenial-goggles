<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>


<%Html.RenderPartial("../Overview/_DisplayMessageBanner", ""); %>
<%DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();%>


<div class="container-fluid px-0 mb-4">
    <div class="col-12 view-btns_container shadow-sm">
        <div class="view-btns deviceView_btn_row rounded">
            <a id="indexLink" href="/Overview/SensorIndex/<%:Model.CSNetID %>#<%:Model.SensorID %>" class="deviceView_btn_row__device">
                <div class="btn-default btn-lg btn-fill">
                    <div class="btn-secondaryToggle btn-lg btn-fill">
                        <%=Html.GetThemedSVG("sensor") %>
                        <span class="extra"><%: Html.TranslateTag("Sensors","Sensors") %></span>
                    </div>
                </div>
            </a>

            <%
                if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Control", Model.ApplicationID.ToString("D3")), "Sensor", MonnitSession.CurrentTheme.Theme))
                {
            %>
            <a id="tabControl" href="/Overview/Control/<%:Model.SensorID %>" class="deviceView_btn_row__device">
                <div class="btn-<%:Request.RawUrl.Contains("Control")? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill">

                    <%=Html.GetThemedSVG("retweet") %>

                    <span class="extra"><%: Html.TranslateTag("Overview/SensorLink|Control","Control") %></span>
                </div>
            </a>
            <%
                }
            %>

            <%
                if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Notify", Model.ApplicationID.ToString("D3")), "Sensor", MonnitSession.CurrentTheme.Theme))
                {
            %>
            <a id="tabData" href="/Overview/DeviceNotify/<%:Model.SensorID %>" class="deviceView_btn_row_device">
                <div class="deviceView_btn_row__device btn-<%:Request.RawUrl.Contains("DeviceNotify")? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill">
                    <%=Html.GetThemedSVG("data") %><span class="extra"><%: Html.TranslateTag("Overview/SensorLink|Data","Data") %></span>
                </div>
            </a>
            <%
                }
            %>

            <%
                if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Terminal", Model.ApplicationID.ToString("D3")), "Sensor", MonnitSession.CurrentTheme.Theme))
                {
            %>
            <a id="tabTerminal" href="/Overview/SensorTerminal/<%:Model.SensorID %>" class=" btn-<%:Request.RawUrl.Contains("SensorTerminal")? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill">
                <div class="deviceView_btn_row__device">
                    <i class="fa fa-terminal"></i><span class="extra"><%: Html.TranslateTag("Overview/SensorLink|Terminal","Terminal") %></span>
                </div>
            </a>
            <%
                }
            %>

            <%
                if (MonnitSession.CustomerCan("Sensor_View_Chart"))
                {
            %>
            <a id="tabChart" href="/Overview/SensorChart/<%:Model.SensorID %>" class="deviceView_btn_row__device">
                <div class=" btn-<%:Request.RawUrl.Contains("SensorChart")? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill ">
                    <%=Html.GetThemedSVG("details") %><span class="extra"><%: Html.TranslateTag("Overview/SensorLink|Details","Details") %></span>
                </div>
            </a>
            <% 
                }
            %>

            <%
                if (MonnitSession.CustomerCan("Sensor_View_History"))
                {
            %>
            <a id="tabHistory" href="/Overview/SensorHome/<%:Model.SensorID %>" class="deviceView_btn_row__device">
                <div class="btn-<%:Request.RawUrl.Contains("SensorHome")? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill">
                    <%=Html.GetThemedSVG("list") %>
                    <span class="extra">
                        <%: Html.TranslateTag("History","History") %>
                    </span>
                </div>
            </a>
            <% 
                }
            %>

            <%
                if (MonnitApplicationBase.HasSensorFile(Model) && MonnitSession.CustomerCan("Sensor_View_History"))
                {
            %>
            <a id="tabFile" href="/Overview/SensorFileList/<%:Model.SensorID %>" class="deviceView_btn_row__device">
                <div class="btn-<%:Request.RawUrl.Contains("SensorFileList")? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill">
                    <i class="fa fa-file-o"></i>
                    <span class="extra">
                        <%: Html.TranslateTag("Overview/SensorLink|File List","File List") %>
                    </span>
                </div>
            </a>
            <%
                }
            %>

            <%
                if (MonnitSession.CustomerCan("Sensor_View_Notifications"))
                {
            %>
            <a id="tabNotification" href="/Overview/SensorNotification/<%:Model.SensorID %>" class="deviceView_btn_row__device">
                <div class="btn-<%:Request.RawUrl.Contains("SensorNotification")? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill">
                    <%=Html.GetThemedSVG("rules") %>
                    <span class="extra"><%: Html.TranslateTag("Rules") %></span>
                </div>
            </a>
            <% 
                }
            %>

            <%
                if (MonnitSession.CustomerCan("Sensor_Edit"))
                {
            %>
            <a id="tabEdit" href="/Overview/SensorEdit/<%:Model.SensorID %>" class="deviceView_btn_row__device">
                <div class=" btn-<%:(Request.RawUrl.Contains("SensorEdit") || Request.RawUrl.Contains("InterfaceEdit"))? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill">
                    <%=Html.GetThemedSVG("settings") %>
                    <span class="extra">
                        <%: Html.TranslateTag("Settings","Settings") %>
                    </span>
                </div>
            </a>
            <% 
                }
            %>

            <%
                if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Calibrate", Model.ApplicationID.ToString("D3")), "Sensor", MonnitSession.CurrentTheme.Theme))
                {
            %>
            <%
                CalibrationCertificate certificate = CalibrationCertificate.LoadBySensor(Model);
                if ((certificate != null && certificate.isInternalCert) || (string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow)))
                {
            %>
            <%
                if (MonnitSession.CustomerCan("Sensor_Calibrate"))
                {
            %>
            <a id="tabCalibrate" href="/Overview/SensorCalibrate/<%:Model.SensorID %>" class="deviceView_btn_row__device">
                <div style="height: 70px;" class=" btn-<%:Request.RawUrl.Contains("SensorCalibrate")? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill">
                    <%=Html.GetThemedSVG("calibrate") %>
                    <span class="extra">
                        <%: Html.TranslateTag("Calibrate","Calibrate") %>
                    </span>
                </div>
            </a>
            <%
                }
            %>
            <%
                }
                else // Sensor Certificate
                {
            %>
            <a id="tabCertificate" href="/Overview/SensorCertificate/<%:Model.SensorID %>" class="deviceView_btn_row__device">
                <div class=" btn-<%:Request.RawUrl.Contains("SensorCertificate")? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill">
                    <%=Html.GetThemedSVG("certificate") %>
                    <span class="extra" style="padding-top: 10px;">
                        <%: Html.TranslateTag("Overview/SensorLink|Certificate","Certificate") %>
                    </span>
                </div>
            </a>
            <%
                }
            %>

            <%
                }
            %>

            <%
                if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Scale", Model.ApplicationID.ToString("D3")), "Sensor", MonnitSession.CurrentTheme.Theme))
                {
            %>
            <%
                if (MonnitSession.CustomerCan("Sensor_Edit"))
                {
            %>
            <a id="tabScale" href="/Overview/SensorScale/<%:Model.SensorID %>" class="deviceView_btn_row__device">
                <div class="btn-<%:Request.RawUrl.Contains("SensorScale")? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill">
                    <%=Html.GetThemedSVG("scale") %>
                    <span class="extra">
                        <%: Html.TranslateTag("Overview/SensorLink|Scale","Scale") %>
                    </span>
                </div>
            </a>
            <%
                }
            %>
            <%
                }
            %>

            <%
                if (MonnitSession.CustomerCan("See_Beta_Preview") && Model.IsCableEnabled == true)
                {
            %>
            <a id="tabLog" href="/Overview/CableLog/<%:Model.SensorID %>" class="deviceView_btn_row__device">
                <div class=" btn-<%:Request.RawUrl.Contains("CableLog")? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill ">
                    <%=Html.GetThemedSVG("details") %>
                    <span class="extra">
                        <%: Html.TranslateTag("Log","Log") %>
                    </span>
                </div>
            </a>
            <% 
                }
            %>

            <%
                if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("EquipmentInfo"), "Sensor", MonnitSession.CurrentTheme.Theme))
                {
            %>
            <a id="tabEquipment" href="/Sensor/EquipmentInfo/<%:Model.SensorID %>" class="deviceView_btn_row__device">
                <div class="btn-<%:Request.RawUrl.Contains("EquipmentInfo")? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill">

                    <%--<div class="equip-ico">   <%=Html.GetThemedSVG("app" + Model.ApplicationID) %></div>--%>
                    <div class="equip-ico">
                        <?xml version="1.0" encoding="UTF-8" ?>
                        <svg id="uuid-d8b3cb27-d186-4803-a36d-49a7b7da9236" data-name="Layer 1" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 460.95 461.66">
                            <g>
                                <path d="m443.99,303.59c-9.36-9.12-18.46-17.86-27.35-26.82-1.03-1.03-1.25-3.27-1.18-4.92.88-19.08-.48-37.88-7.02-55.99-4.96-13.73-12.64-25.52-24.87-34.05-3.81-2.65-7.29-3.06-10.93-.33-5.18,3.87-10.69,7.45-15.25,11.98-38.35,38.09-76.5,76.39-114.76,114.56-2.15,2.15-3.38,4.04-2.56,7.35,2.22,8.91-.06,16.5-6.73,23.14-38.2,38.01-76.37,76.04-114.26,114.36-8.3,8.39-17.61,10.96-28.07,6.91-21.84-8.46-37.14-23.46-42.92-46.8-2.1-8.48.8-15.6,6.91-21.7,25.21-25.12,50.36-50.31,75.53-75.48,13.29-13.29,26.72-26.45,39.81-39.92,6.73-6.93,14.53-8.56,23.68-7.1,2.01.32,4.95-.52,6.37-1.93,34.22-33.99,68.43-67.99,102.31-102.32,9.09-9.22,15.63-20.28,12.82-34.05-2.93-14.38-13.87-21.19-26.61-25.69-3.4-1.2-7.29-1.1-10.62-2.43-5.15-2.07-10.75-4.01-14.86-7.52-8.83-7.54-16.95-15.93-25.13-24.21-4.53-4.59-4.54-8.02-.64-13.26,1.78-2.39,3.82-4.61,5.93-6.72,13.96-14.03,27.97-28.01,41.97-42,1.06-1.06,2.13-2.1,3.24-3.11,8.16-7.45,11.4-7.39,19.27.39,4.85,4.79,9.7,9.58,14.51,14.42,6.97,7.01,12.64,14.61,15.7,24.41,2.13,6.83,6.66,12.91,10.1,19.33,1.85,3.45,4.81,5.16,8.61,4.7,8.42-1,14.27,2.74,19.99,8.65,18.04,18.64,36.21,37.19,55.03,55.03,18.09,17.16,26.2,38.46,28.22,62.49,3.08,36.69-4.24,72.03-15.08,106.81-.14.45-.52.82-1.18,1.83Z" />
                                <path d="m240.47,182.16c-7.84,7.83-15.48,15.45-23.12,23.08-9.41,9.41-18.81,18.82-28.23,28.23-4.91,4.91-11.11,4.92-15.88.13-10.68-10.72-21.43-21.38-32.06-32.15-1.82-1.84-3.29-2.75-5.98-1.39-21.77,10.96-43.8,8.68-64.94-.96C34.45,182.79,11.06,155.52,1.95,116.74c-.61-2.58-.77-5.27-1.42-7.83-2.28-8.91,3.23-13.8,9.64-18.69,5.1,5.13,10.24,10.31,15.38,15.48,3.64,3.66,7.23,7.36,10.92,10.95,17.59,17.12,38.69,19.49,58.78,5.13,9.06-6.47,16.97-14.86,24.34-23.3,10.73-12.28,12.49-27.35,9.32-42.74-1-4.85-4.5-9.63-7.95-13.45-8.02-8.87-16.67-17.18-25.16-25.63-1.99-1.98-4.33-3.62-6.22-5.17C95.04,1.93,102.17-.55,111.83,1.14c33.25,5.8,58.27,23.76,76.99,51.18,10.2,14.95,16.18,31.51,17.73,49.62.94,11.06-1.25,21.61-6.12,31.39-2.39,4.8-1.41,7.36,2.18,10.81,11.75,11.29,23.13,22.96,34.64,34.5,1.03,1.03,1.97,2.16,3.21,3.52Z" />
                            </g>
                            <path d="m316.31,268.16c12.72,12.83,25.1,25.35,37.52,37.83,12.08,12.14,23.8,24.67,36.44,36.21,9.04,8.26,19.29,15.2,29.06,22.64,7.14,5.44,14.54,10.54,21.49,16.2,2.22,1.81,3.66,4.78,4.97,7.46,5.51,11.23,2.88,19.59-6.86,28.12-15.1,13.23-28.67,28.2-42.77,42.56-2.25,2.29-4.17,2.52-7.37,2.08-12.03-1.64-20.24-7.79-26.74-18.02-7.02-11.05-14.86-21.74-23.5-31.56-12.38-14.08-25.71-27.33-38.83-40.75-11.86-12.13-23.97-23.99-35.97-35.98-3.22-3.22-3.77-10.19-.58-13.43,17.51-17.73,35.16-35.33,53.14-53.36Zm47.21,130.83c.05,13.26,10.97,24.1,24.15,23.96,13.05-.14,24.08-11.35,23.97-24.36-.12-13.26-10.95-23.83-24.35-23.74-13.53.09-23.81,10.53-23.76,24.14Z" />
                        </svg>

                    </div>
                    <span class="extra"><%: Html.TranslateTag("Overview/SensorLink|Equipment","Equipment") %></span>
                </div>
            </a>
            <%
                }
            %>
        </div>
    </div>

    <div class="two-card-container" style="gap: 0">
        <%Html.RenderPartial("../Overview/_SensorInfo", Model); %>
        <%
            if (!MonnitSession.CurrentCustomer.Account.HideData && Request.Url.PathAndQuery.Contains("SensorChart"))
            {
        %>
        <div class="d-flex w-100 detailsReadings_card">
            <div class="rule-card_container device_detailsRow__card marginLeftOnLgScreen" style="width: 100%; height: inherit; margin-top: 0;">
                <div class="">
                    <div class="card_container__top__title">
                        <%=Html.GetThemedSVG("list") %>
                            &nbsp;
                        <%: Html.TranslateTag("Readings","Readings")%>
                    </div>
                    <div class="x_body verticalScroll" style="max-height: 160px; overflow-y: scroll;">
                        <%Html.RenderPartial("../Overview/SensorHistoryListSmall", Model); %>
                    </div>
                </div>
            </div>
        </div>
        <%
            }
            else if (Request.Url.PathAndQuery.Contains("SensorNotification"))
            {
        %>
        <%Html.RenderPartial("../Overview/DeviceActionControl", Model); %>
        <%
            }
            else if (Request.Url.PathAndQuery.Contains("SensorEdit"))
            {
        %>
        <%Html.RenderPartial("../Overview/_SensorInfoCard", Model); %>
        <%}%>
    </div>

</div>

<script type="text/javascript">
    $('.btn-secondaryToggle').hover(
        function () { $(this).addClass('btn-active-fill') },
        function () { $(this).removeClass('btn-active-fill') }
    )

    $('.docsBtn').on("click", function () {
        document.getElementById('supportDocs').scrollIntoView();
    })

</script>

<style type="text/css">
    .two-card-container {
        display: flex;
    }

    .docsBtn {
        text-align: center;
        width: 50px;
        height: 24px;
        align-items: center;
        display: flex;
    }

    .docTitle {
        display: flex;
        justify-content: space-between;
        flex-wrap: wrap;
    }

    .first svg {
        fill: #515356;
        height: 15px;
    }

    .second svg {
        fill: #22ae73;
        height: 15px;
    }

    .third svg {
        fill: #ff4d2d;
        height: 15px;
    }

    .equip-ico > svg {
        width: 20px;
    }

/*    @media screen and (max-width: 1075px) {
        .two-card-container {
            flex-direction: column;
            gap: 20px !important;
        }

            .two-card-container > div {
                width:100%
            }

    }*/

</style>
