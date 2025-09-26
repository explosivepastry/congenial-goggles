<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
         <%: Html.TranslateTag("Sensor Mode","Sensor Mode")%>
    </div>

    <div class="col sensorEditFormInput">
        <select class="form-select ms-0" name="Mode" id="Mode" <%:ViewData["disabled"].ToBool() ? "disabled = disabled" : "" %>>
            <option value="0" <%:(((UInt32)Model.Calibration2) & 0x0000FFFF) == 0 ? "selected='selected'" : "" %>> <%: Html.TranslateTag("Volt AC RMS","Volt AC RMS")%></option>
            <option value="1" <%:(((UInt32)Model.Calibration2) & 0x0000FFFF) == 1 ? "selected='selected'" : "" %>> <%: Html.TranslateTag("Volt AC Peak to Peak","Volt AC Peak to Peak")%></option>
            <option value="3" <%:(((UInt32)Model.Calibration2) & 0x0000FFFF) == 3 ? "selected='selected'" : "" %>> <%: Html.TranslateTag("Volt DC US(60 Hz Sample)","Volt DC US(60 Hz Sample)")%></option>
            <option value="4" <%:(((UInt32)Model.Calibration2) & 0x0000FFFF) == 4 ? "selected='selected'" : "" %>> <%: Html.TranslateTag("Volt DC Europe(50 Hz Sample)","Volt DC Europe(50 Hz Sample)")%>/option>
        </select>
    </div>
</div>