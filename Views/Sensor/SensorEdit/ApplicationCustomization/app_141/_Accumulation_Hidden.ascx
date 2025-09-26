<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%if (new Version(Model.FirmwareVersion) >= new Version("2.2.0.0"))
  {
        string label = "";
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);           
%>

    <input hidden type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="AccumChk" id="AccumChk" <%= CurrentZeroToTwentyAmp.GetHystFourthByte(Model) == 1 ? "checked" : "" %> data-toggle="toggle" data-on="<%: Html.TranslateTag("On","On")%>" data-off="<%: Html.TranslateTag("Off","Off")%>" />
        <div style="display: none;"><%: Html.TextBox("Accum",CurrentZeroToTwentyAmp.GetHystFourthByte(Model), (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
    <%: Html.ValidationMessageFor(model => model.Hysteresis)%>

<%}%> 