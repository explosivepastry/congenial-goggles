<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 4 - Voltage-->
<div class="rule-card">
<div class="rule-title">
	<%= Html.TranslateTag("Notify when reading is") %>
</div> 
<div>
    <select class="form-select user-dets grt-less"  id="CompareType" name="CompareType">
        <option value="Greater_Than" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Greater_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Greater Than","Greater Than")%></option>
        <option value="Less_Than" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Less_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Less Than","Less Than")%></option>        
    </select>    
</div>
    

<input id="CompareValue" name="CompareValue" class="form-control user-dets" style="width:60px;" type="text" value="<%:Model.CompareValue %>">
<%: Html.ValidationMessageFor(model => model.CompareValue)%>
</div>
<script type="text/javascript">
    function datumConfigs() {
        var settings = "compareType=" + $('#CompareType').val();
        settings += "&compareValue=" + $('#CompareValue').val();
        return settings;
    }
</script>