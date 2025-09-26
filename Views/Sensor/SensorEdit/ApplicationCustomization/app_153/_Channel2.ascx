<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        string OverflowCount_channel2 = "";
        string Accumulate_channel2 = "";
        string TriggerType_channel2 = "";
        string FilterFrequency_channel2 = "";
        string ReArmTime_channel2 = "";
        string Label_Channel2 = "";
        string OverflowControl_channel2 = "";
        string ReportImmediatelyOnOverflow_channel2 = "";
        string GoAwareOnOverflow_channel2 = "";

        OverflowCount_channel2 = TwoInputPulseCounter.GetOverflowCount_channel2(Model);
        Accumulate_channel2 = TwoInputPulseCounter.GetAccumulate_channel2(Model).ToString();
        TriggerType_channel2 = TwoInputPulseCounter.GetTriggerType_channel2(Model).ToString();
        FilterFrequency_channel2 = TwoInputPulseCounter.GetFilterFrequency_channel2(Model).ToString();
        ReArmTime_channel2 = TwoInputPulseCounter.GetReArmTime_channel2(Model).ToString();
        Label_Channel2 = TwoInputPulseCounter.GetLabel_Channel2(Model.SensorID);
        OverflowControl_channel2 = TwoInputPulseCounter.GetOverflowControl_channel2(Model).ToString();
        ReportImmediatelyOnOverflow_channel2 = TwoInputPulseCounter.GetReportImmediatelyOnOverflow_channel2(Model).ToString();
        GoAwareOnOverflow_channel2 = TwoInputPulseCounter.GetGoAwareOnOverflow_channel2(Model).ToString();

%>
<p class="useAwareState"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_153|Channel 2","Channel 2")%></p>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Detection Type","Detection Type")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="TriggerType_channel2" name="TriggerType_channel2" class="form-select ms-0" <%=Model.CanUpdate ? "" : "disabled" %>>
            <option value="0" <%:TriggerType_channel2.ToInt() == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Positive Edge","Positive Edge")%></option>
            <option value="1" <%:TriggerType_channel2.ToInt() == 1 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Negative Edge","Negative Edge")%></option>
            <option value="2" <%:TriggerType_channel2.ToInt() == 2 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Both Edges","Both Edges")%></option>
        </select>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Noise Filter","Noise Filter")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="FilterFrequency_channel2" name="FilterFrequency_channel2" class="form-select ms-0" <%=Model.CanUpdate ? "" : "disabled" %>>
            <option value="1000" <%:FilterFrequency_channel2.ToInt() == 1000 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Strong","Strong")%></option>
            <option value="200" <%:FilterFrequency_channel2.ToInt() == 200 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Medium","Medium")%></option>
            <option value="50" <%:FilterFrequency_channel2.ToInt() == 50 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Weak","Weak")%></option>
            <option value="0" <%:FilterFrequency_channel2.ToInt() == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|None","None")%></option>
        </select>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Accumulate","Accumulate")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label id="accuOff_channel2" class="form-check-label" style="min-width: 20px"><%: Html.TranslateTag("Off","Off")%></label>
            <label id="accuOn_channel2" class="form-check-label" style="min-width: 20px"><%: Html.TranslateTag("On","On")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="Accumulate_channel2Chk" id="Accumulate_channel2Chk" <%= Accumulate_channel2.ToInt() == 1 ? "checked" : "" %>>
        </div>
        <div style="display: none;"><%: Html.TextBox("Accumulate_channel2",Accumulate_channel2, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
        <%: Html.ValidationMessageFor(model => model.Calibration3)%>
        <%
            if (TwoInputPulseCounter.GetAccumulate_channel2(Model) == 1)
            {
        %>
        <button class="btn btn-secondary " style="width: 112px; margin-left: 3.8rem;" title="<%: Html.TranslateTag("Reset Accumulator","Reset Accumulator")%>" data-bs-toggle="modal" data-bs-target=".accumulatorModal" type="button" <%=Model.CanUpdate ? "" : "disabled" %> value="<%: Html.TranslateTag("Reset Accumulator", "Reset Accumulator")%>">
            <%: Html.TranslateTag("Reset","Reset")%>
        </button>
        <% 
            }
        %>
    </div>
</div>

<div class="modal fade accumulatorModal" id="accumulatorModal" tabindex="-1" aria-labelledby="accumulatorModal" aria-hidden="true">
    <div class="modal-dialog modal-sm">
        <div class="modal-content">
            <div class="modal-header" style="background: white;">
                <h5 class="modal-title" id="accumulatorModalLabel"><%:Html.TranslateTag ("Reset Accumulate") %></h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" style="color: blue"></button>
            </div>
            <div class="modal-body" style="border-top: solid 1px lightgrey; border-bottom: solid 1px lightgrey;">
                <div class="row">
                    <div class="word-def">
                        <%:Html.TranslateTag ("Please select the channel(s) you would like to reset.") %>
                    </div>
                </div>
            </div>
            <div style="display: flex; flex-direction: column; align-items: center; justify-content: space-between; height: 180px; padding: 1rem; border-radius: .25rem; background: white;">
                <button <%:TwoInputPulseCounter.GetAccumulate_channel1(Model) == 1 ? "" : "disabled" %> style="width: 200px;" type="button" class="btn btn-primary resetAccumulator" data-acc="3" data-bs-dismiss="modal"><%:Html.TranslateTag("Channel 1") %></button>
                <button <%:TwoInputPulseCounter.GetAccumulate_channel2(Model) == 1 ? "" : "disabled" %> style="width: 200px;" type="button" class="btn btn-primary resetAccumulator" data-acc="4" data-bs-dismiss="modal"><%:Html.TranslateTag("Channel 2") %></button>
                <button <%:TwoInputPulseCounter.GetAccumulate_channel1(Model) == 1 && TwoInputPulseCounter.GetAccumulate_channel2(Model) == 1 ? "" : "disabled" %> style="width: 200px;" type="button" class="btn btn-primary resetAccumulator" data-acc="5" data-bs-dismiss="modal"><%:Html.TranslateTag("Both") %></button>
            </div>
        </div>
    </div>
</div>

<div class="row sensorEditForm overflowOptionDivs_channel2">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Overflow Count","Overflow Count")%>:  (<%: Label_Channel2 %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="overflowCount_channel2" id="overflowCount_channel2" value="<%=OverflowCount_channel2 %>" />
        <a id="overflowCount_channel2Num" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script>

    $(function () {

        if ($('#Accumulate_channel2Chk').prop('checked')) {
            $('#accuOff_channel2').css("display", "none");
            $('#accuOn_channel2').css("display", "block");
        } else {
            $('#accuOn_channel2').css("display", "none");
            $('#accuOff_channel2').css("display", "block");
        }
        var transVal2 = <%: TwoInputPulseCounter.GetTransform_Channel2(Model.SensorID)%>;

        <% if (Model.CanUpdate)
    { %>

        const arrayForSpinner = arrayBuilder((1 * transVal2), (0xFFFFFFFF * transVal2), 10000000);
        createSpinnerModal("overflowCount_channel2Num", '<%: Label_Channel2 %>', "overflowCount_channel2", arrayForSpinner);

        <%}%>

        $('#Accumulate_channel2Chk').change(function () {
            if ($('#Accumulate_channel2Chk').prop('checked')) {
                $('#Accumulate_channel2').val(1);
                $('#accuOff_channel2').css("display", "none");
                $('#accuOn_channel2').css("display", "block");
            } else {
                $('#Accumulate_channel2').val(0);
                $('#accuOn_channel2').css("display", "none");
                $('#accuOff_channel2').css("display", "block");
            }
        });

        $("#overflowCount_channel2").change(function () {
            if (isANumber($("#overflowCount_channel2").val())) {
                if ($('#overflowCount_channel2').val() < 0)
                    $('#overflowCount_channel2').val(0);

                if ($('#overflowCount_channel2').val() > (0xFFFFFFFF * transVal2))
                    $('#overflowCount_channel2').val((0xFFFFFFFF * transVal2));
            }
            else
                $('#overflowCount_channel2').val(<%: OverflowCount_channel2%>);
        });

        $("#ReArmTime_channel2").change(function () {
            if (isANumber($("#ReArmTime_channel2").val())) {
                if (Number($('#ReArmTime_channel2').val()) < 1)
                    $('#ReArmTime_channel2').val(1);

                if (Number($('#ReArmTime_channel2').val()) > parseFloat($('#ActiveStateInterval').val()) * 60)
                    $('#ReArmTime_channel2').val(parseFloat($('#ActiveStateInterval').val()) * 60);
            }
            else
                $('#ReArmTime_channel2').val(<%: ReArmTime_channel2%>);
        });
    });
</script>
<%} %>