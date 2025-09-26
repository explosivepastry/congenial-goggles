<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<CalibrationCertificate>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EditCalibrationCertificate
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%   
        DateTime DateCertified = Model.DateCertified;
        DateTime expDate = Model.CertificationValidUntil;

        // Date and Time Format for OV Preference
        string prefDate = MonnitSession.CurrentCustomer.Preferences["Date Format"].ToLower();
        string prefTime = MonnitSession.CurrentCustomer.Preferences["Time Format"];

        if (prefTime.Contains("tt"))
            prefTime = prefTime.Replace("tt", "A");
        if (prefTime.Contains("mm"))
            prefTime = prefTime.Replace("mm", "ii");

    %>


    <%      Sensor sensor = null;

            if (Model.SensorID > 0)
            {
                sensor = Sensor.Load(Model.SensorID);
            }
            else if (Model.CableID > 0)
            {
                sensor = Sensor.LoadByCableID(Model.CableID);
            } %>

    <%--    <h2>Edit Calibration Certificate</h2>--%>

    <div class="container-fluid">
        <%Html.RenderPartial("SensorLink", sensor); %>

        <div class="col-12 card_container">
            <form action="/Overview/CalibrationCertificateEdit/<%:Model.CalibrationCertificateID %>" method="post">
                <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>

                <div class="x_title card_container__top">
                    <div class="card_container__top__title" style="overflow: unset"><%: Html.TranslateTag("Overview/SensorCalibrate|Calibration Certificate","Calibration Certificate")%></div>

                    <div class="clearfix"></div>
                </div>

                <div class="x_content">

                    <input type="hidden" id="userID" name="userID" class="form-control" value="<%: MonnitSession.CurrentCustomer.CustomerID %>">
                    <input type="hidden" id="sensorID" name="sensorID" class="form-control" value="<%: Model.SensorID %>">

                    <% List<CalibrationFacility> cf = CalibrationFacility.LoadAll(); %>

                    <%if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Support_Advanced"))
                        {   // Only MonnitAdmin can see Calibration Facility     %>
                    <%-- Calibration Facility --%>
                    <div class="row sensorEditForm">
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
                        <div class="clearfix"></div>
                    </div>
                    <%}
                        else if (MonnitSession.CurrentCustomer.Account.AccountID == 2301) // cal lab co 
                        { %>
                    <div class="row sensorEditForm" id="calFacility">
                        <div class="col-12 col-md-3">
                            <label class="control-label " for="calFaciltyID">
                                <%: Html.TranslateTag("Overview/SensorCalibrate|Calibration Facility","Calibration Facility")%>
                            </label>
                        </div>
                        <div class="col sensorEditFormInput">
                            <select id="calFaciltyID" name="calFaciltyID" class="form-control">
                                <option <%=Model.CalibrationFacilityID == 1 ? "selected": "" %> value="1"><%: Html.TranslateTag("CalLabCo","CalLabCo")%></option>
                                <option <%=Model.CalibrationFacilityID == 4 ? "selected": "" %> value="4"><%: Html.TranslateTag("Custom","Custom")%></option>
                            </select>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <%} %>

                    <%-- Date Certified --%>
                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3">
                        <label class="control-label" for="DateCertified">
                            <%: Html.TranslateTag("Overview/SensorCalibrate|Date Certified","Date Certified")%>
                        </label>
                        </div>
                        <div class="col sensorEditFormInput">
                           <input class="form-control" id="DateCertified" name="DateCertified" required="required" value="<%=Model.DateCertified.OVToLocalDateShort() %>"/>

                        </div>
                        <div class="clearfix"></div>
                    </div>

                    <%-- Certificate Expiration Date --%>
                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3">
                        <label class="control-label" for="expirationDate">
                            <%: Html.TranslateTag("Overview/SensorCalibrate|Certificate Valid Until","Certificate Valid Until")%>
                        </label>
                        </div>
                        <div class="col sensorEditFormInput">
                           <input class="form-control" id="expirationDate" name="expirationDate" required="required" value="<%: Model.CertificationValidUntil.OVToLocalDateShort()%>">

                        </div>
                        <div class="clearfix"></div>
                    </div>

                    <%-- Calibration Number --%>
                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3">
                        <label class="control-label" for="calCert">
                            <%: Html.TranslateTag("Overview/SensorCalibrate|Calibration Number","Calibration Number")%>
                        </label>
                        </div>
                        <div class="col sensorEditFormInput">
                            <input type="text" id="calCert" name="calCert" class="form-control aSettings__input_input" value="<%: Model.CalibrationNumber %>">
                        </div>
                        <div class="clearfix"></div>
                    </div>

                    <%-- Certification Type --%>
                    <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin || MonnitSession.CustomerCan("Support_Advanced"))
                              { %>
                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3">
                        <label class="control-label" for="certType">
                            <%: Html.TranslateTag("Overview/SensorCalibrate|Certification Type","Certification Type")%>
                        </label>
                        </div>
                        <div class="col sensorEditFormInput">
                            <input type="text" id="certType" name="certType" class="form-control aSettings__input_input" value="<%: Model.CertificationType %>">
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <%} %>
                    <%if (MonnitSession.CustomerCan("Support_Advanced") || MonnitSession.IsCurrentCustomerMonnitSuperAdmin || MonnitSession.CustomerCan("Support_Advanced"))
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
                                <option value="10">10 Minutes</option>
                                <option value="60">60 Minutes</option>
                                <option value="120">120 Minutes</option>
                            </select>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <%} %>

                    <div class="clearfix"></div>
                    <div class="ln_solid"></div>

                    <%-- Save Button --%>
                    <div class="text-end">
                        <button type="button" id="cancelBtn" onclick="history.go(-1); return false;" class="btn btn-secondary"><%: Html.TranslateTag("Cancel","Cancel")%></button>
                        <button type="submit" id="SubmitBtn" class="btn btn-primary" style="margin-right: 5px;"><%: Html.TranslateTag("Save","Save")%></button>
                        <div class="clearfix"></div>
                    </div>
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

<script type="text/javascript">

    var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
    var dFormat = '<%=MonnitSession.CurrentCustomer.Preferences["Date Format"]%>'.toUpperCase();
    //var dFormat = prefDate.replace("yyyy", "yyyy");

  
    var dateCreated = new Date("<%= Model.DateCertified %>");
    var dateCertified = new Date("<%= Model.CertificationValidUntil %>");

    $(document).ready(function () {
        var certFacilityID = '<%= Model.CalibrationFacilityID %>';
        $('#calFaciltyID').val(certFacilityID);

        $('#calFaciltyID').change(function (e) {
            e.preventDefault();
            certFacilityID = $("#calFaciltyID").val();
        });

        $('#DateCertified').mobiscroll().datepicker({
            controls: ["calendar"],
            theme: "ios",
            select: 'date',
            display: popLocation,
            dateFormat: dFormat.toUpperCase(),
            headerText: 'Date Certified',
            onInit: function (event, inst) {
                inst.setVal(dateCreated);
            }
        });

        $('#expirationDate').mobiscroll().datepicker({
            controls: ["calendar"],
            theme: 'ios',
            display: popLocation,
            select: 'date',
            dateFormat: dFormat.toUpperCase(),
            headerText: 'Expiration Date',
            onInit: function (event, inst) {
                inst.setVal(dateCertified);
            }
        });
    });

    </script>

</asp:Content>
