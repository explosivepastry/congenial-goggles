<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%if (!Model.IsActive && MonnitSession.CustomerCan("Sensor_Active"))
    { %>

<input hidden type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="IsActive" id="IsActive" <%= Model.IsActive? "checked" : "" %> data-toggle="toggle" data-on="<%: Html.TranslateTag("Active","Active")%>" data-off="<%: Html.TranslateTag("Inactive","Inactive")%>" />

<%} %>