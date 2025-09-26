<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 7 - Angle-->
<div class="rule-card">
<div class="rule-title">
	Notify when reading is 
</div> 
<div>
    <select class="form-select user-dets grt-less"  id="CompareType" name="CompareType">
        <option value="Greater_Than" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Greater_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Greater Than","Greater Than")%></option>
        <option value="Less_Than" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Less_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Less Than","Less Than")%></option>        
    </select>    
</div>
<div class="d-flex align-items-center">
    <input id="CompareValue" name="CompareValue" type="text" style="width:60px;" class="form-control user-dets " value="<%:Model.CompareValue %>">
    <div class="degree-text">°</div>
    <%: Html.ValidationMessageFor(model => model.CompareValue)%>
</div>
    </div>
<script type="text/javascript">
    function datumConfigs() {
        var settings = "compareType=" + $('#CompareType').val();
        settings += "&compareValue=" + $('#CompareValue').val();
        return settings;
    }
    $("#CompareValue").change(function () {
        if ($("#CompareValue").val() < -180)
            $("#CompareValue").val(-180);
        if ($("#CompareValue").val() > 180)
            $("#CompareValue").val(180)
    })
</script>