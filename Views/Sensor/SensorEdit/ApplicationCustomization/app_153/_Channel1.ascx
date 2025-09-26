<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        string OverflowCount_channel1 = "";
        string Accumulate_channel1 = "";
        string TriggerType_channel1 = "";
        string FilterFrequency_channel1 = "";
        string ReArmTime_channel1 = "";
        string Label_Channel1 = "";
        string OverflowControl_channel1 = "";
        string ReportImmediatelyOnOverflow_channel1 = "";
        string GoAwareOnOverflow_channel1 = "";

        OverflowCount_channel1 = TwoInputPulseCounter.GetOverflowCount_channel1(Model);
        Accumulate_channel1 = TwoInputPulseCounter.GetAccumulate_channel1(Model).ToString();
        TriggerType_channel1 = TwoInputPulseCounter.GetTriggerType_channel1(Model).ToString();
        FilterFrequency_channel1 = TwoInputPulseCounter.GetFilterFrequency_channel1(Model).ToString();
        ReArmTime_channel1 = TwoInputPulseCounter.GetReArmTime_channel1(Model).ToString();
        Label_Channel1 = TwoInputPulseCounter.GetLabel_Channel1(Model.SensorID);
        OverflowControl_channel1 = TwoInputPulseCounter.GetOverflowControl_channel1(Model).ToString();
        ReportImmediatelyOnOverflow_channel1 = TwoInputPulseCounter.GetReportImmediatelyOnOverflow_channel1(Model).ToString();
        GoAwareOnOverflow_channel1 = TwoInputPulseCounter.GetGoAwareOnOverflow_channel1(Model).ToString();
%>
<p class="useAwareState"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_153|Channel 1","Channel 1")%></p>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Detection Type","Detection Type")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="TriggerType_channel1" name="TriggerType_channel1" class="form-select ms-0" <%=Model.CanUpdate ? "" : "disabled" %>>
            <option value="0" <%:TriggerType_channel1.ToInt() == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Positive Edge","Positive Edge")%></option>
            <option value="1" <%:TriggerType_channel1.ToInt() == 1 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Negative Edge","Negative Edge")%></option>
            <option value="2" <%:TriggerType_channel1.ToInt() == 2 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Both Edges","Both Edges")%></option>
        </select>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Noise Filter","Noise Filter")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="FilterFrequency_channel1" name="FilterFrequency_channel1" class="form-select ms-0" <%=Model.CanUpdate ? "" : "disabled" %>>
            <option value="1000" <%:FilterFrequency_channel1.ToInt() == 1000 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Strong","Strong")%></option>
            <option value="200" <%:FilterFrequency_channel1.ToInt() == 200 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Medium","Medium")%></option>
            <option value="50" <%:FilterFrequency_channel1.ToInt() == 50 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Weak","Weak")%></option>
            <option value="0" <%:FilterFrequency_channel1.ToInt() == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|None","None")%></option>
        </select>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Accumulate","Accumulate")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label id="accuOff_channel1" class="form-check-label" style="min-width: 20px"><%: Html.TranslateTag("Off","Off")%></label>
            <label id="accuOn_channel1" class="form-check-label" style="min-width: 20px"><%: Html.TranslateTag("On","On")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="Accumulate_channel1Chk" id="Accumulate_channel1Chk" <%= Accumulate_channel1.ToInt() == 1 ? "checked" : "" %>>
        </div>
        <div style="display: none;"><%: Html.TextBox("Accumulate_channel1",Accumulate_channel1, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
        <%
            if (TwoInputPulseCounter.GetAccumulate_channel1(Model) == 1)
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

<div class="row sensorEditForm overflowOptionDivs_channel1">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_153|Overflow Count","Overflow Count")%>:  (<%: Label_Channel1 %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="overflowCount_channel1" id="overflowCount_channel1" value="<%=OverflowCount_channel1 %>" />
        <a id="overflowCount_channel1Num" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<script>

    $(function () {

        if ($('#Accumulate_channel1Chk').prop('checked')) {
            $('#accuOff_channel1').css("display", "none");
            $('#accuOn_channel1').css("display", "block");
        } else {
            $('#accuOn_channel1').css("display", "none");
            $('#accuOff_channel1').css("display", "block");
        }

        var transVal1 = <%: TwoInputPulseCounter.GetTransform_Channel1(Model.SensorID)%>;

        <% if (Model.CanUpdate)
    { %>

        const arrayForSpinner = arrayBuilder((1 * transVal1), (0xFFFFFFFF * transVal1), 10000000);
        createSpinnerModal("overflowCount_channel1Num", '<%: Label_Channel1 %>', "overflowCount_channel1", arrayForSpinner);

        <%}%>

        $('#Accumulate_channel1Chk').change(function () {
            if ($('#Accumulate_channel1Chk').prop('checked')) {
                $('#Accumulate_channel1').val(1);
                $('#accuOff_channel1').css("display", "none");
                $('#accuOn_channel1').css("display", "block");
            } else {
                $('#Accumulate_channel1').val(0);
                $('#accuOn_channel1').css("display", "none");
                $('#accuOff_channel1').css("display", "block");
            }
        });

        $("#overflowCount_channel1").change(function () {
            if (isANumber($("#overflowCount_channel1").val())) {
                if ($('#overflowCount_channel1').val() < 0)
                    $('#overflowCount_channel1').val(0);

                if ($('#overflowCount_channel1').val() > (0xFFFFFFFF * transVal1))
                    $('#overflowCount_channel1').val((0xFFFFFFFF * transVal1));
            }
            else
                $('#overflowCount_channel1').val(<%: OverflowCount_channel1%>);
        });

        $("#ReArmTime_channel1").change(function () {
            if (isANumber($("#ReArmTime_channel1").val())) {
                if (Number($('#ReArmTime_channel1').val()) < 1)
                    $('#ReArmTime_channel1').val(1);

                if (Number($('#ReArmTime_channel1').val()) > parseFloat($('#ActiveStateInterval').val()) * 60)
                    $('#ReArmTime_channel1').val(parseFloat($('#ActiveStateInterval').val()) * 60);
            }
            else
                $('#ReArmTime_channel1').val(<%: ReArmTime_channel1%>);
        });
    });
</script>
<%} %>