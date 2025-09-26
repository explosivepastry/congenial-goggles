<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>



<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_005|Sensitivity","Sensitivity")%>
    </div>
    <div class="col sensorEditFormInput">
        <select style="margin-left:0px;" class="form-select"  name="Sensitivity" id="Sensitivity" <%=Model.CanUpdate ? "" : "disabled" %>>
            <option value="64" <%=Model.Calibration2 == 64 ? "selected" : "" %>>9 feet</option>
            <option value="40" <%=Model.Calibration2 == 40 ? "selected" : "" %>>12 feet</option>
            <option value="25" <%=Model.Calibration2 == 25 ? "selected" : "" %>>15 feet</option>
        </select>
    </div>
</div>


<script type="text/javascript">

    $('#Sensitivity').addClass("editField editFieldSmall");
<%--
    $(function () {
                <% if (Model.CanUpdate)
                   { %>

        $('#Sensitivity').mobiscroll().select({
            theme: 'ios',
            display: popLocation,
            minWidth: 200
        });



    <%}%>
    });--%>
</script>
