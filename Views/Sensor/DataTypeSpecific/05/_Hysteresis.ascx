<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string Hyst = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);
         
%>

 <tr>
            <td style="width: 250px;"><h3>Aware State Buffer</h3></td>
     </tr>
<tr>
    <td>Hysteresis</td>
            <td>
                <%: Html.TextBox("Hysteresis_Manual", Hyst, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>%
                            <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
            </td>
            <td>
                <img alt="help" class="helpIcon" title="A buffer to prevent the sensor from bouncing between Standard Operation and Aware State when the assessments are very close to a threshold.  For example, if a Maximum Threshold is set to 90° and a Aware State Buffer of 1°, then once the sensor takes an assessment of 90.0° and enters the Aware State it will remain in the Aware State until the temperature readings drop to 89.0°.  Similarly, at the Minimum Threshold the temperature will have to rise a degree above the threshold to return to Standard Operation." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2">
                <div id="Hysteresis_Slider"></div>
            </td>
        </tr>
        <script type="text/javascript">
            var recovery_array = [0, 1, 2, 5, 10];
            function getRecoveryIndex() {
                var retval = 0;
                $.each(recovery_array, function (index, value) {
                    if (value <= $("#Hysteresis_Manual").val())
                        retval = index;
                });

                return retval;
            }

            $('#Hysteresis_Slider').slider({
                value: <%:Hyst%>,
                min: 0,
                max: 10,
                            <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
                slide: function (event, ui) {
                    //update the amount by fetching the value in the value_array at index ui.value
                    $('#Hysteresis_Manual').val(ui.value);
                }
            });
            $("#Hysteresis_Manual").addClass('editField editFieldSmall');
            $("#Hysteresis_Manual").change(function () {
                if (isANumber($("#Hysteresis_Manual").val())){
                    if ($("#Hysteresis_Manual").val() < 0)
                        $("#Hysteresis_Manual").val(0);
                    if ($("#Hysteresis_Manual").val() > 10)
                        $("#Hysteresis_Manual").val(10)
                    $('#Hysteresis_Slider').slider("value", $("#Hysteresis_Manual").val());
                }
                else
                {
                    $("#Hysteresis_Manual").val(<%: Hyst%>);
                    $('#Hysteresis_Slider').slider("value", $("#Hysteresis_Manual").val());
                }
            });
        </script>
<%} %>

