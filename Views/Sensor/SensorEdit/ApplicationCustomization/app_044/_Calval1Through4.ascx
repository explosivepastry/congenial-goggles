<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
  
    //cal val 1
    int StopBits = SerialDataBridge.GetCalibrationByteValue(Model.Calibration1, SerialDataBridge.SignificanceIndex.Byte_3);
    int Pariity = SerialDataBridge.GetCalibrationByteValue(Model.Calibration1, SerialDataBridge.SignificanceIndex.Byte_2);
    int DataBits = SerialDataBridge.GetCalibrationByteValue(Model.Calibration1, SerialDataBridge.SignificanceIndex.Byte_1);
    int Baudrate = SerialDataBridge.GetCalibrationByteValue(Model.Calibration1, SerialDataBridge.SignificanceIndex.Byte_0);

    //cal val 2
    int ContinuousRadioRX = SerialDataBridge.GetCalibrationByteValue(Model.Calibration2, SerialDataBridge.SignificanceIndex.Byte_1);
    int DeafOnSTX = SerialDataBridge.GetCalibrationByteValue(Model.Calibration2, SerialDataBridge.SignificanceIndex.Byte_0);

    //cal val 3
    int PacketInterval = SerialDataBridge.GetCalibrationByteValue(Model.Calibration3, SerialDataBridge.SignificanceIndex.Byte_3);
    int GroupInterval = SerialDataBridge.GetCalibrationByteValue(Model.Calibration3, SerialDataBridge.SignificanceIndex.Byte_2);
    int Group = SerialDataBridge.GetCalibrationByteValue(Model.Calibration3, SerialDataBridge.SignificanceIndex.Byte_1);
    int PacketSize = SerialDataBridge.GetCalibrationByteValue(Model.Calibration3, SerialDataBridge.SignificanceIndex.Byte_0);
    
%>

<%--Baud Rate--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_044|Baud Rate","Baud Rate")%>
    </div>
    <div class="col sensorEditFormInput">
        <select name="Baudrate" id="Baudrate" class="form-select" <%=Model.CanUpdate ? "" : "disabled" %>>
            <option value="0" <%: Baudrate == 0 ? "selected='selected'" : "" %>>1200</option>
            <option value="1" <%: Baudrate == 1 ? "selected='selected'" : "" %>>2400</option>
            <option value="2" <%: Baudrate == 2 ? "selected='selected'" : "" %>>4800</option>
            <option value="3" <%: Baudrate == 3 ? "selected='selected'" : "" %>>9600</option>
            <option value="4" <%: Baudrate == 4 ? "selected='selected'" : "" %>>14400</option>
            <option value="5" <%: Baudrate == 5 ? "selected='selected'" : "" %>>19200</option>
            <option value="6" <%: Baudrate == 6 ? "selected='selected'" : "" %>>28800</option>
            <option value="7" <%: Baudrate == 7 ? "selected='selected'" : "" %>>38400</option>
            <option value="8" <%: Baudrate == 8 ? "selected='selected'" : "" %>>57600</option>
            <option value="9" <%: Baudrate == 9 ? "selected='selected'" : "" %>>115200</option>
            <option value="10" <%: Baudrate == 10 ? "selected='selected'" : "" %>>230400</option>
        </select>
    </div>
</div>

<%--Data Bits--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_044|Data Bits","Data Bits")%>
    </div>
    <div class="col sensorEditFormInput">
        <select name="DataBits" id="DataBits" class="form-select" <%:Model.CanUpdate ? "" : "disabled" %>>
            <%--<option value="7" <%: DataBits == 7 ? "selected='selected'" : "" %>>7</option>--%>
            <option value="8" <%: DataBits == 8 ? "selected='selected'" : "" %>>8</option>

        </select>
    </div>
</div>


<%-- Parity --%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_044|Parity","Parity")%>
    </div>
    <div class="col sensorEditFormInput">
        <select name="Parity" id="Parity" class="form-select" <%:Model.CanUpdate ? "" : "disabled" %>>
            <option value="0" <%: Pariity == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("None","None")%></option>
            <option value="1" <%: Pariity == 1 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Odd","Odd")%></option>
            <option value="2" <%: Pariity == 2 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Even","Even")%></option>
        </select>
    </div>
</div>

<%--Stop Bits--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_044|Stop Bits","Stop Bits")%>
    </div>
    <div class="col sensorEditFormInput">
        <select name="StopBits" id="StopBits" class="form-select" <%:Model.CanUpdate ? "" : "disabled" %>>
            <option value="1" <%: StopBits == 1 ? "selected='selected'" : "" %>>1</option>
            <option value="2" <%: StopBits == 2 ? "selected='selected'" : "" %>>2</option>
        </select>
    </div>
</div>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Packet Size","Packet Size")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="PacketSize" id="PacketSize" value="<%: PacketSize %>" />
        <a id="packetNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>


<script type="text/javascript">

    //Packet Size
          <% if (Model.CanUpdate)
             { %>

    let arrayForSpinner1 = arrayBuilder(8, 24, 1);
    createSpinnerModal("packetNum", "Packet Size", "PacketSize", arrayForSpinner1);

    <%}%>

        $("#PacketSize").addClass('editField editFieldMedium');

        $("#PacketSize").change(function () {
            if (isANumber($("#PacketSize").val())) {
                if ($("#PacketSize").val() < 8)
                    $("#PacketSize").val(8);
                if ($("#PacketSize").val() > 24)
                    $("#PacketSize").val(24);
            }
            else {
                $("#PacketSize").val(<%: PacketSize%>);
            }
        });
</script>
