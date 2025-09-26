<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 54 MoistureWeight-->
<div class="rule-card">
<div class="rule-title">
	Notify when Moisture Weight reading is 
</div> 
<div>
    <%  double CompareValue = (String.IsNullOrEmpty(Model.CompareValue)) ? 0.0d : Model.CompareValue.ToDouble();
        if (Model.Scale == "F")
        {
            
        }
        else
        {
            CompareValue = Monnit.Application_Classes.DataTypeClasses.MoistureWeight.StandardToMetric(CompareValue);
        }

        CompareValue = Math.Round(CompareValue, 2);

        if (Model != null && Model.CompareType == eCompareType.Less_Than_or_Equal)
            Model.CompareType = eCompareType.Less_Than;
        %>
        <select class="form-select grt-less user-dets"  id="CompareType" name="CompareType">
            <option value="Greater_Than">Greater Than</option>
            <option value="Less_Than" <%:(Model != null && Model.CompareType == eCompareType.Less_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Less Than","Less Than")%></option>
        </select>
        <input class="form-control user-dets" style="width:60px;" id="CompareValue" name="CompareValue" type="text" value="<%:CompareValue %>">
         
        <select class="form-select user-dets grt-less" name="scale" id="scale">
             <option <%:Model.Scale == "F" ? "selected='selected'" : "" %> value="F">Grains Per Pound</option>
             <option <%:Model.Scale == "C" ? "selected='selected'" : "" %> value="C">Grams Per Kilogram</option>
        </select>

</div>
    </div>
<%: Html.ValidationMessageFor(model => model.CompareValue)%>
<script type="text/javascript">
    function datumConfigs() {
        var settings = "compareType=" + $('#CompareType').val();
        settings += "&compareValue=" + $('#CompareValue').val();
        settings += "&scale=" + $('#scale').val();
        return settings;
    }
</script>
