<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 40 - UnscaledVoltage-->
<div class="aSettings__title">
	Notify when reading is 
</div> 
<div>
    <select class="form-control"  id="CompareType" name="CompareType">
        <option value="Greater_Than" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Greater_Than) ? "selected=selected" : "" %>>Greater Than</option>
        <option value="Less_Than" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Less_Than) ? "selected=selected" : "" %>>Less Than</option>
    </select>    
</div>
<div class="d-flex align-items-center">
    <input id="CompareValue" class="form-control mt-1 me-1" name="CompareValue" type="text" value="<%:Model.CompareValue %>">
    <%: Html.ValidationMessageFor(model => model.CompareValue)%>
</div>
<script type="text/javascript">
    function datumConfigs() {
        var settings = "compareType=" + $('#CompareType').val();
        settings += "&compareValue=" + $('#CompareValue').val();
        return settings;
    }
</script>