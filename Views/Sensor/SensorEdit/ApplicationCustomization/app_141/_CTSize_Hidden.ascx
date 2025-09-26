<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string CT = "";
        string label = "";
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out CT);
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);
         
%>

        <input hidden class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Calibration2_Manual" id="Calibration2_Manual" value="<%=CT%>" />

        <%: Html.ValidationMessageFor(model => model.Calibration2)%>
<%} %>