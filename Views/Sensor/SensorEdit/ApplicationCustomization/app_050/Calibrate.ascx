<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
   
<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

  TempData["CanCalibrate"] = true;

  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);                                                                                                                       
  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="Form1" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="Hidden1" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
      { %>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

    <%} %>

    <%if(TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
        {
            byte[] CIDbytes = BitConverter.GetBytes(Convert.ToUInt32(Model.Calibration1));

            int HookOnTime = CIDbytes[0].ToInt();
            int HookOffTime = CIDbytes[1].ToInt();
            int RingOnTime = CIDbytes[2].ToInt();
            int RingOffTime = CIDbytes[3].ToInt();

            CIDbytes = BitConverter.GetBytes(Convert.ToUInt32(Model.Calibration2));
            int CIDStopTime = CIDbytes[0].ToInt();
            int CATTime = BitConverter.ToUInt16(CIDbytes, 2);

            CIDbytes = BitConverter.GetBytes(Convert.ToUInt32(Model.Calibration3));
            int OutsideLineSequence1 = CIDbytes[0].ToInt();
            int OutsideLineSequence2 = CIDbytes[1].ToInt();
            int OutsideLineSequence3 = CIDbytes[2].ToInt();
            int OutsideLineSequence4 = CIDbytes[3].ToInt();

    %>
    <div class="form-group">
        <div class="bold col-md-3 col-sm-3 col-xs-12">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_050|Hook On Time","Hook On Time")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <input name="HookOnTime" value="<%: HookOnTime %>" />
        </div>
    </div>
    <div class="clear"></div>
    <br />

    <div class="form-group">
        <div class="bold col-md-3 col-sm-3 col-xs-12">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_050|Hook Off Time","Hook Off Time")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <input name="HookOffTime" value="<%: HookOffTime %>" />
        </div>
    </div>
    <div class="clear"></div>
    <br />

    <div class="form-group">
        <div class="bold col-md-3 col-sm-3 col-xs-12">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_050|Ring On Time","Ring On Time")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <input name="RingOnTime" value="<%: RingOnTime %>" />
        </div>
    </div>
    <div class="clear"></div>
    <br />

    <div class="form-group">
        <div class="bold col-md-3 col-sm-3 col-xs-12">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_050|Ring Off Time","Ring Off Time")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <input name="RingOffTime" value="<%: RingOffTime %>" />
        </div>
    </div>
    <div class="clear"></div>
    <br />
    <div class="form-group">
        <div class="bold col-md-3 col-sm-3 col-xs-12">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_050|CID Stop Time","CID Stop Time")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <input name="CIDStopTime" value="<%: CIDStopTime %>" />
        </div>
    </div>
    <div class="clear"></div>
    <br />

    <div class="form-group">
        <div class="bold col-md-3 col-sm-3 col-xs-12">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_050|Cat Time","Cat Time")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <input name="CATTime" value="<%: CATTime %>" />
        </div>
    </div>
    <div class="clear"></div>
    <br />

    <div class="form-group">
        <div class="bold col-md-3 col-sm-3 col-xs-12">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_050|Outside Line Sequence 1","Outside Line Sequence 1")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <input name="OutsideLineSequence1" value="<%: OutsideLineSequence1 %>" />
        </div>
    </div>
    <div class="clear"></div>
    <br />

    <div class="form-group">
        <div class="bold col-md-3 col-sm-3 col-xs-12">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_050|Outside Line Sequence 2","Outside Line Sequence 2")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <input name="OutsideLineSequence2" value="<%: OutsideLineSequence2 %>" />
        </div>
    </div>
    <div class="clear"></div>
    <br />
    <div class="form-group">
        <div class="bold col-md-3 col-sm-3 col-xs-12">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_050|Outside Line Sequence 3","Outside Line Sequence 3")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <input name="OutsideLineSequence3" value="<%: OutsideLineSequence3 %>" />
        </div>
    </div>
    <div class="clear"></div>
    <br />

    <div class="form-group">
        <div class="bold col-md-3 col-sm-3 col-xs-12">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_050|Outside Line Sequence 4","Outside Line Sequence 4")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <input name="OutsideLineSequence4" value="<%: OutsideLineSequence4 %>" />
        </div>
    </div>
    <div class="clear"></div>

    <br />
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%}%>