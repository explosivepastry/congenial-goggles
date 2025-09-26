<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>
<div class="rule-card">
    <div class="rule-title">
        <%: Html.TranslateTag("Events/_ApplicationSpecific|Notify when sensor reading is","Notify when sensor reading is")%>
    </div>
    <div class="col-12 ps-0 d-flex flex-column">
        <select class="form-select user-dets grt-less" id="CompareType" name="CompareType" >
            <option value="Greater_Than"><%: Html.TranslateTag("Events/_ApplicationSpecific|Greater Than","Greater Than")%></option>
            <option value="Less_Than" <%:(Model != null && Model.CompareType == eCompareType.Less_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Events/_ApplicationSpecific|Less Than","Less Than")%></option>
            <option value="Equal" <%:(Model != null && Model.CompareType == eCompareType.Equal) ? "selected=selected" : "" %>><%: Html.TranslateTag("Events/_ApplicationSpecific|Equal To","Equal To")%></option>
        </select>
        <input class="form-control mt-1 user-dets grt-less" id="CompareValue" name="CompareValue"  type="text" value="<%:Model.CompareValue %>">
        <%: Html.ValidationMessageFor(model => model.CompareValue)%>
    </div>
</div>

<script type="text/javascript">

    function datumConfigs() {
        var settings = "compareType=" + $('#CompareType').val();
        settings += "&compareValue=" + $('#CompareValue').val();
        return settings;
    }

</script>

