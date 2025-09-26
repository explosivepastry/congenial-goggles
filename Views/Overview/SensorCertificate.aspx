<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Sensor>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Sensor Certificate
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">

        <%
            Html.RenderPartial("SensorLink", Model);

            DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

            CalibrationCertificate cc = CalibrationCertificate.LoadBySensor(Model);
            CalibrationFacility calFacility = CalibrationFacility.Load(Model.CalibrationFacilityID);
        %>

        <div class="col-12">
            <div class="x_panel shadow-sm rounded">
                <div class="card_container__top__title">
                    <%: Html.TranslateTag("Overview/SensorCertificate|Calibration Certificate","Calibration Certificate")%>
                    <div class="nav navbar-right panel_toolbox">
                        
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">

                    <div class="formBody">


                        <% if (calFacility != null)
                           { %>
                        <div>
                            <%: Html.TranslateTag("Overview/SensorCertificate|This sensor has been pre-calibrated and certified by","This sensor has been pre-calibrated and certified by")%>  <%: calFacility.Name %>  <%:  (CalibrationCertificationValidUntil != DateTime.MinValue && CalibrationCertificationValidUntil < new DateTime(2099,1,1)) && !string.IsNullOrWhiteSpace(CalibrationCertificationValidUntil.ToShortDateString()) ? Html.TranslateTag("Overview/SensorCertificate|and is valid until","and is valid until") + " " + CalibrationCertificationValidUntil.ToShortDateString():"" %>.
                       
                        </div>



                        <br />
                        <% if (!string.IsNullOrWhiteSpace(calFacility.CertificationPath))
                           { %>
                        <% if (calFacility.CertificationPath.Contains("74.93.64.170"))//callabco certification numbers are  all numeric digits
                           { %>
                        <div>
                            <a class="btn btn-primary" target="_blank" href="<%: string.Format(calFacility.CertificationPath,  new Regex("[^0-9-]").Replace(Model.CalibrationCertification, "")) %>"><%: Html.TranslateTag("Overview/SensorCertificate|View Calibration Certificate","View Calibration Certificate")%></a>
                        </div>

                        <%}
                           else    // Default Facility
                           {%>
                        <div>
                            <a class="btn btn-primary" target="_blank" href="<%: string.Format(calFacility.CertificationPath, Model.CalibrationCertification) %>"><%: Html.TranslateTag("Overview/SensorCertificate|View Calibration Certificate","View Calibration Certificate")%></a>
                        </div>

                        <%}
                           }
                           }
                           else     // Contains no Certification Facility
                           { %>
                        <div>
                            <%: Html.TranslateTag("Overview/SensorCertificate|This sensor has been pre-calibrated and certified","This sensor has been pre-calibrated and certified")%>.
                       
                        </div>
                        <%} %>




                        <%if (cc != null && cc.DeletedDate != null){    //  if Certification Exist and Not Deleted - then show Certificate Data    %>

                        <%-- Created By --%>
                        <div class="form-group">
                            <label class="control-label col-md-3 col-sm-3 col-xs-12" for="createDate">
                                <%: Html.TranslateTag("Overview/SensorCalibrate|Created By","Created By")%>
                            </label>
                            <div class="col-md-3 col-sm-3 col-xs-12">
                                <input class="form-control" type="text" disabled value="<%: Customer.Load(cc.CreatedByUserID).UserName %>">
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <%--<!-- Create Date -->
                        <div class="form-group">
                            <label class="control-label col-md-3 col-sm-3 col-xs-12" for="createDate">
                                <%: Html.TranslateTag("Overview/SensorCalibrate|Date Created","Date Created")%>
                            </label>
                            <div class="col-md-3 col-sm-3 col-xs-12">
                                <input class="form-control" type="text" disabled value="<%: cc.DateCreate.OVToLocalDateShort() %>">
                            </div>
                            <div class="clearfix"></div>
                        </div>--%>

                        <!-- Certified Date -->
                        <div class="form-group">
                            <label class="control-label col-md-3 col-sm-3 col-xs-12" for="createDate">
                                <%: Html.TranslateTag("Overview/SensorCalibrate|Date Certified","Date Certified")%>
                            </label>
                            <div class="col-md-3 col-sm-3 col-xs-12">
                                <%--<input class="form-control" type="text" disabled value="<%: cc.DateCertified.OVToLocalDateShort() %>">--%>
                                <input class="form-control" type="text" disabled value="<%: cc.DateCertified.ToShortDateString() %>">
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <!-- Certificate Expiration Date -->
                        <div class="form-group">
                            <label class="control-label col-md-3 col-sm-3 col-xs-12" for="createDate">
                                <%: Html.TranslateTag("Overview/SensorCalibrate|Valid Until","Valid Until")%>
                            </label>
                            <div class="col-md-3 col-sm-3 col-xs-12">
                                <%--<input class="form-control" type="text" disabled value="<%: cc.CertificationValidUntil.OVToLocalDateShort() %>">--%>
                                <input class="form-control" type="text" disabled value="<%: cc.CertificationValidUntil.ToShortDateString() %>">
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <!-- Calibration Number -->
                        <div class="form-group">
                            <label class="control-label col-md-3 col-sm-3 col-xs-12" for="createDate">
                                <%: Html.TranslateTag("Overview/SensorCalibrate|Calibration Number","Calibration Number")%>
                            </label>
                            <div class="col-md-3 col-sm-3 col-xs-12">
                                <input class="form-control" type="text" disabled value="<%: cc.CalibrationNumber %>">
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <!-- Calibration Facility -->
                        <div class="form-group">
                            <label class="control-label col-md-3 col-sm-3 col-xs-12" for="createDate">
                                <%: Html.TranslateTag("Overview/SensorCalibrate|Calibration Facility","Calibration Facility")%>
                            </label>
                            <div class="col-md-3 col-sm-3 col-xs-12">
                                <input class="form-control" type="text" disabled value="<%: CalibrationFacility.Load(cc.CalibrationFacilityID).Name %>">
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <!-- Certification Type -->
                        <div class="form-group">
                            <label class="control-label col-md-3 col-sm-3 col-xs-12" for="createDate">
                                <%: Html.TranslateTag("Overview/SensorCalibrate|Certification Type","Certification Type")%>
                            </label>
                            <div class="col-md-3 col-sm-3 col-xs-12">
                                <input class="form-control" type="text" disabled value="<%: cc.CertificationType %>">
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <% } %>
                    </div>
                </div>
                <div class="clearfix"></div>
                <div class="ln_solid"></div>

                <%if (MonnitSession.CustomerCan("Can_Create_Certificate") && cc != null)  //  if Admin permission - show edit and/or remove buttons
                  { %>
                <div class="col-sm-6">
                    <div class="col-sm-4">
                        <a class="btn btn-secondary" href="/Overview/CalibrationCertificateEdit/<%= cc.CalibrationCertificateID %>">Edit Certificate</a>
                    </div>
                    <%if(cc.DeletedDate == DateTime.MinValue) {  //  Show remove button if certificate is not deleted. %>
                    <div class="col-sm-4">
                        <a class="btn btn-danger" href="/Overview/CalibrationCertificateRemove/<%= cc.CalibrationCertificateID %>">Remove Certificate</a>
                    </div>
                    <% } %>
                </div>
                <% } %>
            </div>
        </div>
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



    <!-- help button modal -->
    <div class="modal fade certHelp" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">

                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel2"><%: Html.TranslateTag("Overview/SensorCertificate|Sensor Calibration","Sensor Calibration")%></h4>

                    <button type="button" class="btn-close" data-dismiss="modal" aria-label="Close">
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="word-choice">
                            <%: Html.TranslateTag("Overview/SensorCertificate|Calibration Certificate","Calibration Certificate")%>
                        </div>
                        <div class="word-def" >
                            <%: Html.TranslateTag("Overview/SensorCertificate|Sensors calibrated by an external lab receive a Lab Calibration Certificate.  Manual calibration of Lab Certified sensors is locked. If you wish to calibrate this sensor manually, please contact support.","Sensors calibrated by an external lab receive a Lab Calibration Certificate.  Manual calibration of Lab Certified sensors is locked. If you wish to calibrate this sensor manually, please contact support.")%>
                            <br />
                            <br />
                            <%: Html.TranslateTag("Overview/SensorCertificate|Warning: your calibration certificate will be voided if manually calibrated.","Warning: your calibration certificate will be voided if manually calibrated.")%>
                      
                        </div>
                    </div>

                    <div class="clearfix"></div>
                </div>
                <div class="modal-footer">
             
                </div>

            </div>
        </div>
    </div>
    <!-- End help button modal -->
</asp:Content>