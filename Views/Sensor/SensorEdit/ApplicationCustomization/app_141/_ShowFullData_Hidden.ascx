<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
        string label = "";
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);
        bool FullNotiString = CurrentZeroToTwentyAmp.GetShowFullDataValue(Model.SensorID);          
%>
    <input hidden type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="fullChk" id="fullChk" <%= CurrentZeroToTwentyAmp.GetShowFullDataValue(Model.SensorID) ? "checked" : "" %> data-toggle="toggle" data-on="<%: Html.TranslateTag("On","On")%>" data-off="<%: Html.TranslateTag("Off","Off")%>" />
        <div style="display: none;"><%: Html.TextBoxFor(model => FullNotiString, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>