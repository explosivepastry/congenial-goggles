<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 14 LuxData-->
<div class="rule-card"> 
<div class="rule-title">
	Notify when reading is 
</div> 
<div>
    <select class="form-select user-dets grt-less"  id="CompareType" name="CompareType">
        <option value="Greater_Than">Greater Than</option>
        <option value="Less_Than" <%:(Model != null && Model.CompareType == eCompareType.Less_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Less Than","Less Than")%></option>
    </select>
</div>
<div class="d-flex align-items-center">
    <input id="CompareValue" class="form-control mt-1 me-2 user-dets" style="width:200px;" name="CompareValue" type="text" value="<%:Model.CompareValue %>"> Lux
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