<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%--Communication Mode--%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_011|Communication Mode","Communication Mode")%>
    </div>
    <div class="col sensorEditFormInput">
       <select style="margin:0;"  <%=Model.CanUpdate ? "" : "disabled" %> id="CommunicationMode" name="CommunicationMode" class="form-select" <%:ViewData["disabled"].ToBool() ? "disabled" : ""%>>
            <option <%: Model.RearmTime == 3 ? "selected" : "" %> value="3">Site Survey</option>
            <option <%: Model.RearmTime == 20 ? "selected" : "" %> value="20">Service Button</option>
        </select>
    </div>
</div>