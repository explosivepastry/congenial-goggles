<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 34 - SeatOccupied-->
<div class="aSettings__title">
	Notify when sensor is 
    <input class="aSettings__input_input" id="CompareType" name="CompareType" type="hidden" value="<%:eCompareType.Equal %>" />
</div> 

<div>
    <%bool value = Model != null ? (Model.CompareValue == "True") : true; %>
    <select class="form-select"  id="CompareValue" name="CompareValue">
        <option value='True'>Occupied</option>
        <option value='False' <%:Model != null && (Model.CompareValue == "False") ? "selected='selected'": "" %>>Unoccupied</option>
    </select>
</div>
<div class="clearfix"></div>
<script type="text/javascript">
    function datumConfigs() {
        var settings = "compareType=" + $('#CompareType').val();
        settings += "&compareValue=" + $('#CompareValue').val();
        return settings;
    }
</script>