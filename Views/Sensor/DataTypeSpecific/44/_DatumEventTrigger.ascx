<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 44 - Voltage0To1Point2-->
<div class="rule-card">
<div class="rule-title">
	Notify when reading is 
</div> 

    <select class="form-select user-dets grt-less"  id="CompareType" name="CompareType">
        <%--<option value="Equal" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Equal) ? "selected=selected" : "" %>>Equal</option>
        <option value="Not_Equal" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Not_Equal) ? "selected=selected" : "" %>>Not Equal</option>--%>
        <option value="Greater_Than" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Greater_Than) ? "selected=selected" : "" %>>Greater Than</option>
        <option value="Less_Than" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Less_Than) ? "selected=selected" : "" %>>Less Than</option>        
       <%-- <option value="Greater_Than_or_Equal" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Greater_Than_or_Equal) ? "selected=selected" : "" %>>Greater Than or Equal</option>
        <option value="Less_Than_or_Equal" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Less_Than_or_Equal) ? "selected=selected" : "" %>>Less Than or Equal</option>--%>
    </select>    

<input id="CompareValue"  class="form-control user-dets" style="width:60px;" name="CompareValue" type="text" value="<%:Model.CompareValue %>">
<%: Html.ValidationMessageFor(model => model.CompareValue)%>
</div>
<script type="text/javascript">
    function datumConfigs() {
        var settings = "compareType=" + $('#CompareType').val();
        settings += "&compareValue=" + $('#CompareValue').val();
        return settings;
    }
</script>