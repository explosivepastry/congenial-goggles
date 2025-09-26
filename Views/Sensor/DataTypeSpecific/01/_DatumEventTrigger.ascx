<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 01 Percentage-->
<div class="rule-card">
<div class="rule-title">
	<%: Html.TranslateTag("Notify when reading is","Notify when reading is")%>:
</div> 
<div>
    <select class="form-select user-dets grt-less"   id="CompareType" name="CompareType">
        <option value="Greater_Than">Greater Than</option>
        <option value="Less_Than" <%:(Model != null && Model.CompareType == eCompareType.Less_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Less Than","Less Than")%></option>
    </select>
</div>
<div class="degree-input">
    <input class="form-control user-dets" id="CompareValue" name="CompareValue" type="text" style="width:60px;" value="<%:Model.CompareValue %>"> <div class="percent-choose">%</div>
</div>
    </div>
<%: Html.ValidationMessageFor(model => model.CompareValue)%>
<script type="text/javascript">
    function datumConfigs() {
        var settings = "compareType=" + $('#CompareType').val();
        settings += "&compareValue=" + $('#CompareValue').val();
        return settings;
    }
</script>