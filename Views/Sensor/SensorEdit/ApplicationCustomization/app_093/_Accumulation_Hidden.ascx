<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%if (new Version(Model.FirmwareVersion) >= new Version("2.2.0.0"))
  {
        byte Accumulate = CurrentZeroToTwentyAmp.GetHystFourthByte(Model);
%>

        <div class="col sensorEditFormInput" style="display: none;">
              <%: Html.TextBox("Accum", Accumulate, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        </div>

<%}%> 