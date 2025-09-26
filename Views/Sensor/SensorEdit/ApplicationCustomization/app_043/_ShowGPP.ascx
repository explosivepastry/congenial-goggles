<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

 <%  bool showGPP = ViewData["ShowGPP"].ToBool();
            if (ViewData["ShowGPP"] == null)
            {
                showGPP = HumiditySHT25.ShowGPP(Model.SensorID);
            }
        %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Show Moisture Weight","Show Moisture Weight")%> 
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Hide","Hide")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" name="showGPP" id="showGPP" <%= showGPP ? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("Show","Show")%></label>
        </div>                                   
    </div>
</div>
