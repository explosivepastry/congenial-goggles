<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 57 - ProbeStatus-->
<div class="rule-card">
<div class="rule-title">
	<%=Html.TranslateTag("Notify when Observation Mode","Notify when Observation Mode")%>
</div> 
<div>
    <input type="hidden" id="CompareType" name="CompareType" value="1" />
    <select id="CompareValue" class="form-select user-dets" name="CompareValue">
        <option value="start" <%=Model.CompareValue.ToString() == "start" ? "selected" : "" %>><%=Html.TranslateTag("Begins","Begins")%></option>
        <option value="end" <%=Model.CompareValue.ToString() == "end" ? "selected" : "" %>><%=Html.TranslateTag("Ends","Ends")%></option>
    </select> 
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