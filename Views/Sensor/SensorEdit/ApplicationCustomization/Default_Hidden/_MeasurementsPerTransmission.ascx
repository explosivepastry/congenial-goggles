<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<input type="hidden" name="MeasurementsPerTransmission" id="MeasurementsPerTransmission" value="<%=Model.MeasurementsPerTransmission %>" />
<%: Html.ValidationMessageFor(model => model.MeasurementsPerTransmission)%>
   