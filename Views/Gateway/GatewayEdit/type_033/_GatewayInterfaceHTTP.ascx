<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Configuration Timeout","Configuration Timeout")%>
    </div>

    <div class="col sensorEditFormInput">
        <select  class="form-select" name="HTTPServiceTimeout" id="HTTPServiceTimeout">
            <option value="0" <%= Model.HTTPServiceTimeout == 0 ? "selected='selected'" : ""%>><%: Html.TranslateTag("Read Only","Read Only")%></option>
            <option value="1" <%= Model.HTTPServiceTimeout > 0 && Model.HTTPServiceTimeout < 5 ? "selected='selected'" : ""%>><%: Html.TranslateTag("1 Minute","1 Minute")%></option>
            <option value="5" <%= Model.HTTPServiceTimeout >= 5 && Model.HTTPServiceTimeout < 30 ? "selected='selected'" : ""%>><%: Html.TranslateTag("5 Minutes","5 Minutes")%></option>
            <option value="30" <%= Model.HTTPServiceTimeout >= 30 && Model.HTTPServiceTimeout < 1092 ? "selected='selected'" : ""%>><%: Html.TranslateTag("30 Minutes","30 Minutes")%></option>
            <option value="1092.25" <%= Model.HTTPServiceTimeout >= 1092 ? "selected='selected'" : ""%>><%: Html.TranslateTag("Always Available","Always Available")%></option>
        </select>
        <%: Html.ValidationMessageFor(model => model.HTTPServiceTimeout)%>
    </div>
</div>
