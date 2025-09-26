<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string PowerMode = "";
    string DataMode = "";
    string BitRate = "";
    string FIFOSize = "";
    string ReadSize = "";
    string Compensator = "";

    PowerMode = AdvancedVibration2.GetPowerMode(Model).ToString();
    DataMode = AdvancedVibration2.GetDataMode(Model).ToString();
    //BitRate = AdvancedVibration2.GetBitRate(Model).ToString();
    //FIFOSize = AdvancedVibration2.GetFIFOSize(Model).ToString();
    //ReadSize = AdvancedVibration2.GetReadSize(Model).ToString();
    //Compensator = AdvancedVibration2.GetCompensator(Model).ToString();

%>


        <select hidden <%=Model.CanUpdate ? "" : "disabled" %> id="PowerMode_Manual" name="PowerMode_Manual" class="form-select" <%:ViewData["disabled"].ToBool() ? "disabled" : ""%>>
            <option <%: PowerMode == "0"? "selected":"" %> value="0"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_142|Low Power")%></option>
            <option <%: PowerMode == "1"? "selected":"" %> value="1"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_142|Medium Power")%></option>
            <option <%: PowerMode == "2"? "selected":"" %> value="2"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_142|High Performance")%></option>
        </select>


        <select hidden <%=Model.CanUpdate ? "" : "disabled" %> id="DataMode_Manual" name="DataMode_Manual" class="form-select" <%:ViewData["disabled"].ToBool() ? "disabled" : ""%>>
            <option <%: PowerMode == "0"? "selected":"" %> value="0"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_142|Most Recent")%></option>
            <option <%: PowerMode == "1"? "selected":"" %> value="1"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_142|Average")%></option>
            <option <%: PowerMode == "2"? "selected":"" %> value="2"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_142|Maximum")%></option>
            <option <%: PowerMode == "3"? "selected":"" %> value="3"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_142|Minimum")%></option>
        </select>
