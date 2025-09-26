<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 35 - AccelormeterImpact-->
<div class="rule-card">
<div class="rule-title">
	Notify when sensor is 
    <input class="aSettings__input_input" id="CompareType" name="CompareType" type="hidden" value="<%:eCompareType.Equal %>" />
</div> 

<div>
    <%bool value = Model != null ? (Model.CompareValue == "True") : true; %>
    <select class="form-select user-dets grt-less"  id="CompareValue" name="CompareValue">
        <option value='True'>Impacted</option>
        <option value='False' <%:Model != null && (Model.CompareValue == "False") ? "selected='selected'": "" %>>Unimpacted</option>
    </select>
</div>
<div class="clearfix"></div>
    </div>
<script type="text/javascript">
    function datumConfigs() {
        var settings = "compareType=" + $('#CompareType').val();
        settings += "&compareValue=" + $('#CompareValue').val();
        return settings;
    }
</script>