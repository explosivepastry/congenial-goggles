<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 19 Weight-->
<div class="rule-card">
<div class="rule-title">
	Notify when weight value is 
</div> 
<div>
    <select class="form-select user-dets"  id="CompareType" name="CompareType">
        <option value="Greater_Than">Greater Than</option>
        <option value="Less_Than" <%:(Model != null && Model.CompareType == eCompareType.Less_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Less Than","Less Than")%></option>
    </select>
</div>
<input id="CompareValue" class="form-control mt-1 user-dets" name="CompareValue" type="text" value="<%:Model.CompareValue %>">
<%: Html.ValidationMessageFor(model => model.CompareValue)%>
    </div>
<script type="text/javascript">
    function datumConfigs() {
        var settings = "compareType=" + $('#CompareType').val();
        settings += "&compareValue=" + $('#CompareValue').val();
        settings += "&scale=" + $('#scale').val();
        return settings;
    }
</script>