<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string Hyst = "";
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);
        
         
%>
        <input hidden class="aSettings__input_input" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
<%} %>