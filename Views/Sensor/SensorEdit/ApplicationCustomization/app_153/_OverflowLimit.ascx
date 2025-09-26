<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%  
    string ReArmTime_channel1 = "";
    ReArmTime_channel1 = TwoInputPulseCounter.GetReArmTime_channel1(Model).ToString();

    string ReArmTime_channel2 = "";
    ReArmTime_channel2 = TwoInputPulseCounter.GetReArmTime_channel2(Model).ToString();
%>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Overflow Limit","Overflow Limit")%> (<%: Html.Label(Html.TranslateTag("Seconds")) %>)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="ReArmTime_channel1" id="ReArmTime_channel1" value="<%: ReArmTime_channel1 %>" />
        <a id="ReArmTime_channel1Num" style="cursor: pointer;"></a>
    </div>
</div>
<input class="form-control user-dets" type="hidden" <%=Model.CanUpdate ? "" : "disabled"  %> name="ActiveStateInterval" id="ActiveStateInterval" value="<%=Model.ActiveStateInterval %>" />
<input type="hidden" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="ReArmTime_channel2" id="ReArmTime_channel2" value="<%: ReArmTime_channel2 %>" />


<script>
    const reArmTimeInputShown = document.querySelector("#ReArmTime_channel1");
    const reArmTimeInputHidden = document.querySelector("#ReArmTime_channel2");
    const activeStateInterval = document.getElementById('ActiveStateInterval');

    reArmTimeInputShown.addEventListener("change", () => {
        if (reArmTimeInputShown.value < 1) {
            reArmTimeInputShown.value = 1;
            return reArmTimeInputHidden.value = 1;
        }
        if (Number(reArmTimeInputShown.value) > parseFloat(activeStateInterval.value) * 60) {
            return reArmTimeInputHidden.value = parseFloat(activeStateInterval.value) * 60;
        }
        reArmTimeInputHidden.value = reArmTimeInputShown.value;
    });

</script>






