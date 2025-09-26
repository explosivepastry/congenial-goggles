<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%--Max Threshold--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_081|Apparent Power Usage Threshold","Apparent Power Usage Threshold")%> (kWh)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control user-dets" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=CTPower.MaxThreshForUI(Model)%>" />
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>
<div class="clear"></div>
<br />

<%--Min Threshold--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_081|RMS Voltage Threshold","RMS Voltage Threshold")%> (kWh)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control user-dets" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MintThreshByte1and2" id="MintThreshByte1and2" value="<%=CTPower.GetMinThreshByteOneAndTwo(Model) / 10.0 %>" />
        <a id="MintThreshByte1and2Num" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>
<div class="clear"></div>
<br />

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_081|RMS Current Threshold","RMS Current Threshold")%> (kWh)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control user-dets" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MintThreshByte3and4" id="MintThreshByte3and4" value="<%=CTPower.GetMinThreshByteThreeAndFour(Model) / 100.0 %>" />
        <a id="MintThreshByte3and4Num" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>
<div class="clear"></div>
<br />

<div class="row sensorEditForm">
    <div class="col-12 col-md-3 333">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_081|CT-Size","CT-Size")%> (Amps)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control user-dets" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="CalVal3_Manual" id="CalVal3_Manual" value="<%=CTPower.GetCalVal3Lower(Model)%>" />
        <a id="CalVal3_ManualNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration3)%>
    </div>
</div>
<div class="clear"></div>
<br />

<div class="row sensorEditForm">
    <div class="col-12 col-md-3 333">
        <%: Html.TranslateTag("Accumulate","Accumulate")%>
    </div>

    <%--    Toggle--%>

    <div class="col sensorEditFormInput" style="justify-content:space-between; max-width:273px">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label id="minMaxOff" class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <label id="minMaxOn" class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
            <input value="" class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="Byte1OfHystChkBx" id="Byte1OfHystChkBx" <%=CTPower.GetHystFirstByte(Model) == 1 ? "checked" : "" %> onclick="minMaxToggle()" />

        </div>
        <button class="btn btn-secondary" style="width: 112px;" type="button" id="ResetAccumulator" <%=Model.CanUpdate ? "" : "disabled" %> value="<%: Html.TranslateTag("Reset Accumulator","Reset Accumulator")%>">
            <%: Html.TranslateTag("Reset","Reset")%>
        </button>
        <div style="display: none;"><%: Html.TextBox("Byte1OfHyst",CTPower.GetHystFirstByte(Model), (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>

        <script type="text/javascript">
            $(document).ready(function () {

                $('#Byte1OfHystChkBx').change(function () {

                    if ($('#Byte1OfHystChkBx').prop('checked')) {

                        $('#Byte1OfHyst').val(1);
                    } else {
                        $('#Byte1OfHyst').val(0);

                    }
                    console.log($('#Byte1OfHyst').val());

                });
            });

            var hystChk = document.getElementById("Byte1OfHystChkBx");
            var byteOff = document.getElementById("minMaxOff");
            var byteOn = document.getElementById("minMaxOn");


            function minMaxToggle() {

                if (hystChk.checked) {
                    byteOff.style.display = "none";
                    byteOn.style.display = "block";
                } else {
                    byteOn.style.display = "none";
                    byteOff.style.display = "block";
                }

            };
            minMaxToggle()


        </script>

    </div>


</div>
<div class="clear"></div>
<br />

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_081|Default Frequency","Default Frequency")%> (Hz)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control user-dets" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="DefaultFrequency__Manual" id="DefaultFrequency__Manual" value="<%=CTPower.GetHystSecondByte(Model)%>" />
        <a id="freqNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>
<div class="clear"></div>
<br />

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_081|Active Channel","Active Channel")%>
    </div>
    <div class="col sensorEditFormInput">
        <%
            bool bit1 = (CTPower.GetHystThirdByte(Model) & 0x02) == 0x02;
            bool bit2 = (CTPower.GetHystThirdByte(Model) & 0x04) == 0x04;
        %>
        <div style="float: left">
            <input type="checkbox" checked="checked" disabled="disabled" />&nbsp;<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_081|Channel 0","Channel 0")%><br />
            <input type="checkbox" id="bit1" name="bit1" value="2" <%: bit1 ? "checked " : "" %> <%: Model.CanUpdate ? "": "disabled" %> />&nbsp;<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_081|Channel 1","Channel 1")%><br />
            <input type="checkbox" id="bit2" name="bit2" value="4" <%: bit2 ? "checked " : "" %> <%: Model.CanUpdate ? "": "disabled"%> />&nbsp;<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_081|Channel 2","Channel 2")%><br />
        </div>
    </div>
</div>
<div class="clear"></div>
<br />

<script>

    $(document).ready(function () {

          <% if (Model.CanUpdate)
    { %>

        createSpinnerModal("maxThreshNum", "Power Usage Threshold", "MaximumThreshold_Manual", [1, 100, 1000, 10000, 100000, 1000000, 4294967]);
        createSpinnerModal("MintThreshByte1and2Num", "Voltage Threshold", "MintThreshByte1and2", [1, 10, 100, 1000, 2000, 5000, 6553.5]);
        createSpinnerModal("MintThreshByte3and4Num", "Current Threshold", "MintThreshByte3and4", [1, 10, 100, 200, 500, 655.35]);
        createSpinnerModal("CalVal3_ManualNum", "CT-Size", "CalVal3_Manual", [1, 10, 100, 1000, 2000, 5000, 10000, 60000, 65535]);
        createSpinnerModal("freqNum", "Default Frequency", "DefaultFrequency__Manual", [40, 45, 50, 55, 60, 65, 67]);
        <%}%>

        $('#MaximumThreshold_Manual').change(function () {

            if (isANumber($("#MaximumThreshold_Manual").val())) {
                if ($('#MaximumThreshold_Manual').val() < 1)
                    $('#MaximumThreshold_Manual').val(1);

                if ($('#MaximumThreshold_Manual').val() > 4294967.295)
                    $('#MaximumThreshold_Manual').val(4294967.295);
            }
            else
                $('#MaximumThreshold_Manual').val(<%: CTPower.MaxThreshForUI(Model) %>);
        });
        $('#MintThreshByte1and2').change(function () {

            if (isANumber($("#MintThreshByte1and2").val())) {
                if ($('#MintThreshByte1and2').val() < 1)
                    $('#MintThreshByte1and2').val(1);

                if ($('#MintThreshByte1and2').val() > 6553.5)
                    $('#MintThreshByte1and2').val(6553.5);
            }
            else
                $('#MintThreshByte1and2').val(<%: CTPower.GetMinThreshByteOneAndTwo(Model) / 10.0%>);
        });

        $('#MintThreshByte3and4').change(function () {

            if (isANumber($("#MintThreshByte3and4").val())) {
                if ($('#MintThreshByte3and4').val() < 1)
                    $('#MintThreshByte3and4').val(1);

                if ($('#MintThreshByte3and4').val() > 6553.5)
                    $('#MintThreshByte3and4').val(6553.5);
            }
            else
                $('#MintThreshByte3and4').val(<%: CTPower.GetMinThreshByteThreeAndFour(Model) / 100.0%>);
        });

        $('#CalVal3_Manual').change(function () {

            if (isANumber($("#CalVal3_Manual").val())) {
                if ($('#CalVal3_Manual').val() < 1)
                    $('#CalVal3_Manual').val(1);

                if ($('#CalVal3_Manual').val() > 65535)
                    $('#CalVal3_Manual').val(65535);
            }
            else
                $('#CalVal3_Manual').val(<%: CTPower.GetCalVal3Lower(Model)%>);
        });

        $('#DefaultFrequency__Manual').change(function () {

            if (isANumber($("#DefaultFrequency__Manual").val())) {
                if ($('#DefaultFrequency__Manual').val() < 40)
                    $('#DefaultFrequency__Manual').val(40);

                if ($('#DefaultFrequency__Manual').val() > 67)
                    $('#DefaultFrequency__Manual').val(67);
            }
            else {
                $('#DefaultFrequency__Manual').val(<%: CTPower.GetHystFourthByte(Model)/100d%>);
            }
        });

        $('#Offset_Manual').change(function () {

            if (isANumber($("#Offset_Manual").val())) {
                if ($('#Offset_Manual').val() < -1.28)
                    $('#Offset_Manual').val(-1.28);

                if ($('#Offset_Manual').val() > 1.27)
                    $('#Offset_Manual').val(1.27);
            }
            else
                $('#Offset_Manual').val(<%: CTPower.GetHystFourthByte(Model)/100d%>);
        });


    });

</script>
