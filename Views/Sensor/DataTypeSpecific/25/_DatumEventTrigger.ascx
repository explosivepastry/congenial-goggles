<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 25 - Water Detect-->
<div class="rule-title">
	Notify when the sensor has
    <input class="aSettings__input_input" id="CompareType" name="CompareType" type="hidden" value="<%:eCompareType.Equal %>" />
</div> 

<div>
    <%bool value = Model != null ? (Model.CompareValue == "True") : true; %>
    <select class="form-select" style="width:250px;" id="CompareValue" name="CompareValue">
        <option value='True'>Water Detected</option>
        <option value='False' <%:Model != null && (Model.CompareValue == "False") ? "selected='selected'": "" %>>Water Not Detected</option>
    </select>
</div>
<div class="clearfix"></div>
<script type="text/javascript">
    function datumConfigs() {
        var settings = "compareType=" + $('#CompareType').val();
        settings += "&compareValue=" + $('#CompareValue').val();
        return settings;
    }
</script>