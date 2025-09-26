<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<CalibrationCertificate>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CalibrationCertificate
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <%Sensor sensor = ViewBag.sensor;
            Html.RenderPartial("SensorLink", sensor);%>

        <div class="col-md-12 col-xs-12 card_container">
            <form action="/Overview/CalibrationCertificateCreate/<%:sensor.SensorID %>" method="post">
                <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                <div class="x_title card_container__top">
                    <div class="card_container__top__title" style="overflow: unset"><%: Html.TranslateTag("Overview/SensorCalibrate|Calibrate Certification","Calibrate Certification")%></div>
                    </div>
                <div class="x_content">
                    <input type="hidden" id="userID" name="userID" class="form-control" value="<%: MonnitSession.CurrentCustomer.CustomerID %>">
                    <input type="hidden" id="sensorID" name="sensorID" class="form-control" value="<%: sensor.SensorID %>">

                    <% List<CalibrationFacility> cf = CalibrationFacility.LoadAll(); %>
                    <%if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Support_Advanced"))
                        {   // Only MonnitAdmin can see Calibration Facility     %>
                    <!-- Calibration Facility -->
                    <div class="row sensorEditForm" id="calFacility">
                        <div class="col-12 col-md-3">
                            <label class="control-label" for="calFaciltyID">
                                <%: Html.TranslateTag("Overview/SensorCalibrate|Calibration Facility","Calibration Facility")%>
                            </label>
                        </div>
                        <div class="col sensorEditFormInput">
                            <select id="calFaciltyID" name="calFaciltyID" class="form-select">
                                <%foreach (var item in cf)
                                    { %>
                                <option value="<%: item.CalibrationFacilityID %>"><%: item.Name %></option>
                                <%} %>
                            </select>
                        </div>
                    </div>
                    <%} %>

                    <%-- Certificate Create Date --%>
                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3">
                            <label class="control-label" for="DateCertified">
                                <%: Html.TranslateTag("Overview/SensorCalibrate|Date Certified","Date Certified")%>
                            </label>
                        </div>

                        <div class="col sensorEditFormInput">
                            <input class="form-control " id="DateCertified" name="DateCertified" required="required" value="<%: Model.CalibrationCertificateID > 0 ? Model.DateCertified.ToShortDateString() : DateTime.UtcNow.Date.OVToLocalDateShort() %>">
                        </div>
                    </div>

                    <%-- Certificate Expiration Date --%>
                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3">
                            <label class="control-label" for="expirationDate">
                                <%: Html.TranslateTag("Overview/SensorCalibrate|Certificate Valid Until","Certificate Valid Until")%>
                            </label>
                        </div>
                        <div class="col sensorEditFormInput">
                            <input class="form-control " id="expirationDate" name="expirationDate" required="required" value="<%: Model.CalibrationCertificateID > 0 ? Model.CertificationValidUntil.ToShortDateString() : DateTime.UtcNow.Date.OVToLocalDateShort() %>">
                        </div>

                    </div>

                    <%-- Calibration Number --%>
                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3">
                            <label class="control-label" for="calCert">
                                <%: Html.TranslateTag("Overview/SensorCalibrate|Calibration Number","Calibration Number")%>
                            </label>
                        </div>
                        <div class="col sensorEditFormInput">
                            <input type="text" id="calCert" name="calCert" class="form-control " placeholder="<%: Html.TranslateTag("Overview/SensorCalibrate|Calibration Number","Calibration Number")%>">
                        </div>

                    </div>

                    <%-- Certification Type --%>
                     <%if ( MonnitSession.IsCurrentCustomerMonnitSuperAdmin || MonnitSession.CustomerCan("Support_Advanced"))
                        { %>
                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3">
                            <label class="control-label" for="certType">
                                <%: Html.TranslateTag("Overview/SensorCalibrate|Certification Type","Certification Type")%>
                            </label>
                        </div>
                        <div class="col sensorEditFormInput">
                            <input type="text" id="certType" name="certType" class="form-control " placeholder="<%: Html.TranslateTag("Overview/SensorCalibrate|Certification Type","Certification Type")%>">
                        </div>

                    </div>
                    <%} %>
                    <%if (MonnitSession.CustomerCan("AdvancedCalibration") || MonnitSession.IsCurrentCustomerMonnitSuperAdmin || MonnitSession.CustomerCan("Support_Advanced"))
                        { %>
                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3">
                            <label class="control-label" for="certType">
                                <%: Html.TranslateTag("Overview/SensorCalibrate|Heartbeat Interval Reset","Heartbeat Interval Reset")%>
                            </label>
                        </div>
                        <div class="col sensorEditFormInput">
                            <select id="reportInterval" name="reportInterval" class="form-select">
                                <option value="-1"><%: Html.TranslateTag("Overview/SensorCalibrate|No Change","No Change")%></option>
                                <option value="10"><%: Html.TranslateTag("Overview/SensorCalibrate|10 Minutes","10 Minutes")%></option>
                                <option value="60"><%: Html.TranslateTag("Overview/SensorCalibrate|60 Minutes","60 Minutes")%></option>
                                <option value="120"><%: Html.TranslateTag("Overview/SensorCalibrate|120 Minutes","120 Minutes")%></option>
                            </select>
                        </div>

                    </div>
                    <%} %>

                    <div class="ln_solid"></div>

                    <%if (Model.CalibrationCertificateID > 0)
                        {%>

                    <div>
                        <a class="btn btn-secondary" href="/Overview/CalibrationCertificateEdit/<%: Model.CalibrationCertificateID %>"><%: Html.TranslateTag("Overview/SensorCalibrate|Edit Calibration Certificate","Edit Calibration Certificate")%></a>
                    </div>

                    <%}
                    else
                    { %>
                    <%-- Save Button --%>
                    <div class="form-group text-end">
                        <button type="button" id="cancelBtn" onclick="history.go(-1); return false;" class="btn btn-secondary pulse-button"><%: Html.TranslateTag("Cancel","Cancel")%></button>
                        <button type="submit" id="SubmitBtn" class="btn btn-primary" style="margin-right: 5px;"><%: Html.TranslateTag("Save","Save")%></button>
                    </div>
                    <%} %>
                </div>
            </form>
        </div>

        <% if (ViewData["Results"] != null)
            { %>
        <br />
        <div class="x_panel">
            <% if (ViewData["Results"].ToString().Contains("Success"))
                {%>
            <font color="green"><%: ViewData["Results"]  %> </font>
            <%}
                else
                { %>
            <font color="red"><%: ViewData["Results"]  %></font>
            <%} %>
        </div>
        <%} %>
    </div>

    <%
        string[] prefArray = MonnitSession.CurrentCustomer.Preferences.Values.ToArray();
        string prefDate = prefArray[0];
    %>

    <script type="text/javascript">

        var dFormat = '<%= prefDate %>';
        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
        // MobiScroll
        $(function () {

            $('#DateCertified').mobiscroll().datepicker({
                controls: ["calendar"],
                theme: "ios",
                display: popLocation,
                dateFormat: dFormat.toUpperCase(),
                headerText: 'Date Certified',
            });

            $('#expirationDate').mobiscroll().datepicker({
                theme: 'ios',
                display: popLocation,
                dateFormat: dFormat.toUpperCase(),
                headerText: 'Expiration Date',
            });
        });

    </script>

</asp:Content>
