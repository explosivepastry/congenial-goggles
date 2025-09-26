<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 56 MoistureTension-->
<div class="rule-card">
    <div class="rule-title">
        <%: Html.TranslateTag("Notify when Moisture Tension reading is","Notify when Moisture Tension reading is")%>
    </div>
    <div>
        <%  double CompareValue = (String.IsNullOrEmpty(Model.CompareValue)) ? 0.0d : Model.CompareValue.ToDouble();

            CompareValue = Math.Round(CompareValue, 2);

            if (Model != null && Model.CompareType == eCompareType.Less_Than_or_Equal)
                Model.CompareType = eCompareType.Less_Than;
        %>
        <select class="form-select user-dets" id="CompareType" name="CompareType">
            <option value="Greater_Than">Greater Than</option>
            <option value="Less_Than" <%:(Model != null && Model.CompareType == eCompareType.Less_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Less Than","Less Than")%></option>
        </select>
        <div class="d-flex align-items-center">
            <input class="form-control mt-1 me-2 user-dets" style="width:150px;" id="CompareValue" name="CompareValue" type="text" value="<%:CompareValue %>">
            <%: Html.TranslateTag("centibars","centibars")%>
        </div>
    </div>
</div>
<%: Html.ValidationMessageFor(model => model.CompareValue)%>
<%--<input type="text" id="range" value="" name="range" />--%>
<script type="text/javascript">
    function datumConfigs() {
        var settings = "compareType=" + $('#CompareType').val();
        settings += "&compareValue=" + $('#CompareValue').val();
        settings += "&scale=" + $('#scale').val();
        return settings;
    }
</script>

<style>
    #actionTriggerConditions_settings {
        display: flex;
        flex-direction: column;
    }

    .short {
        margin: 10px 10px 0 0;
    }
</style>
