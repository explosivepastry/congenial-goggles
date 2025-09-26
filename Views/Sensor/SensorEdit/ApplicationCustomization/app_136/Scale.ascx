<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />


    <div class="formBody">
        <%  bool isFahrenheit = true;
            if (ViewData["TempScale"] != null)
            {
                isFahrenheit = ViewData["TempScale"].ToStringSafe() == "F";
            }
            else
            {
                isFahrenheit = LCD_Temperature.IsFahrenheit(Model.SensorID);
            }
            if (Model.CanUpdate)
            {
        %>

        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Show Temperature Values in", "Show Temperature Values in")%>
            </div>
            <div class="col sensorEditFormInput">
                <select id="TempScale" name="tempscale" <%=Model.CanUpdate ? "" : "disabled"  %> class="form-select">
                    <option value="<%="on"%>" <%=isFahrenheit ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Fahrenheit", "Fahrenheit")%></option>
                    <option value="<%="off"%>" <%=isFahrenheit ? "" : "selected='selected'" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Celsius", "Celsius")%></option>
                </select>
            </div>
        </div>
        </div>
        <%}
            else
            {%>
        <div class="formBody" style="font-weight: bold">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Scale is not available until transaction is complete.", "Scale is not available until transaction is complete.")%>
        </div>

        <%}%>


    <div class="col-12">
        <span style="color: red;">
            <%: ViewBag.ErrorMessage == null ? "": ViewBag.ErrorMessage %>
        </span>
        <span style="color: black;">
            <%: ViewBag.Message == null ? "":ViewBag.Message %>
        </span>
    </div>

    <div class="clearfix"></div>
    <div class="ln_solid"></div>

    <div class="text-end">
        <%if (!Model.CanUpdate)
            {%>
        <span>
            <%if (MonnitSession.CustomerCan("Support_Advanced"))
                { %>
            <span class="pendingEditIconLeft pendingsvg" style="cursor: pointer; vertical-align: sub;" onclick="clearDirtyFlags(<%: Model.SensorID %>);"><%=Html.GetThemedSVG("Pending_Update") %></span>
            <%}
                else
                { %>
            <span class="pendingEditIconLeft pendingsvg" style="vertical-align: sub;"><%=Html.GetThemedSVG("Pending_Update") %></span>
            <%} %>
            &nbsp;<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fields waiting to be written to sensor are not available for edit until transaction is complete.", "Fields waiting to be written to sensor are not available for edit until transaction is complete.")%>
        </span>
        <%}
            else
            { %>
        <input class="btn btn-primary" type="button" id="save" value="Save" />
        <div style="clear: both;"></div>
        <%} %>
    </div>

    <script>
        $(document).ready(function () {
            $('#save').click(function () {
                postForm($('#SensorScale_<%: Model.SensorID%>'));
            });
        });
    </script>
</form>
