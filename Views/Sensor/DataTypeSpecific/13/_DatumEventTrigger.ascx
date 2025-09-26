<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 13 Geforce-->
<div class="rule-card">
<div class="rule-title">
	Notify when reading is 
</div> 
<div>
    <select class="form-select user-dets grt-less"  id="CompareType" name="CompareType">
        <option value="Greater_Than">Greater Than</option>
        <option value="Less_Than" <%:(Model != null && Model.CompareType == eCompareType.Less_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Less Than","Less Than")%>
    </select>
</div>
<div>
    <input id="CompareValue" class="form-control user-dets grt-less" name="CompareValue" type="text" value="<%:Model.CompareValue %>">
</div>
    </div>
<%--<input type="text" id="range" value="" name="range" />--%>
<%: Html.ValidationMessageFor(model => model.CompareValue)%>
<script type="text/javascript">
    function datumConfigs() {
        var settings = "compareType=" + $('#CompareType').val();
        settings += "&compareValue=" + $('#CompareValue').val();
        return settings;
    }
</script>