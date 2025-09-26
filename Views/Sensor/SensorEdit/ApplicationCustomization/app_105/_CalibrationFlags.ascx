<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
if (!Monnit.VersionHelper.IsVersion_1_0(Model))
{
        string measurementMode = UltrasonicRangerIndustrialBase.GetMeasurementMode(Model).ToString();
        bool averageData = UltrasonicRangerIndustrialBase.GetAverageData(Model);
%>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|MeasurementMode","Reading Mode")%>
        </div>
        <div class="col sensorEditFormInput">
           <select <%=Model.CanUpdate ? "" : "disabled" %> id="MeasurementMode" name="MeasurementMode" class="form-select">
                <option <%: measurementMode == "0"? "selected":"" %> value="0">Delta Controlled Local Mean</option>
                <option <%: measurementMode == "1"? "selected":"" %> value="1">Min</option>
                <option <%: measurementMode == "2"? "selected":"" %> value="2">Mean</option>
                <option <%: measurementMode == "3"? "selected":"" %> value="3">Max</option>
                <option <%: measurementMode == "4"? "selected":"" %> value="4">Median</option>
                <option <%: measurementMode == "5"? "selected":"" %> value="5">Mode</option>
            </select>
        </div>
    </div>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|AverageData","Average Data")%>
        </div>
        <div class="col sensorEditFormInput">
            <div class="form-check form-switch d-flex align-items-center ps-0">
                <label class="form-check-label">Off</label>
                <input <%=Model.CanUpdate ? "" : "disabled" %> class="form-check-input my-0 y-0 mx-2" type="checkbox" name="AverageData" id="AverageData">
                <label class="form-check-label">On</label>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(function () {
            var isAverageData = '<%:averageData%>';
            if (isAverageData == 'True') {
                $('#AverageData').prop('checked', 'checked');
            } else {
                $('#AverageData').prop('checked', '');
            }
        });
    </script>
<%} %>