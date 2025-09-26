<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

 <%if (!MonnitSession.CustomerCan("Sensor_Advanced_Configuration") && Model.MonnitApplication.HasDefaultRanges) { %>
                <%
                    string Hyst = "";
                    string Min = "";
                    string Max = "";
                    MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst, out Min, out Max);
                      
                    long DefaultMin = 0;
                    long DefaultMax = 0;
                    MonnitApplicationBase.DefaultThresholds(Model, out DefaultMin, out DefaultMax);
                                
                    if (ViewData["BasicThreshold"] == null)
                    {
                        if (DefaultMin == Model.MinimumThreshold && DefaultMax != Model.MaximumThreshold)
                        {
                            ViewData["BasicThresholdDirection"] = 1;//Greater Than
                            ViewData["BasicThreshold"] = Max;
                        }
                        else if (DefaultMin != Model.MinimumThreshold && DefaultMax == Model.MaximumThreshold)
                        {
                            ViewData["BasicThresholdDirection"] = 0;//Less Than
                            ViewData["BasicThreshold"] = Min;
                        }
                        else
                        {
                            ViewData["BasicThresholdDirection"] = -1;
                            ViewData["BasicThreshold"] = "";
                        }
                    }

                    DefaultMin = DefaultMin / 10;
                    DefaultMax = DefaultMax / 10;
                    
                    if (Temperature.IsFahrenheit(Model.SensorID))
                    {
                        DefaultMin = DefaultMin.ToDouble().ToFahrenheit().ToLong();
                        DefaultMax = DefaultMax.ToDouble().ToFahrenheit().ToLong();
                    }
           
                %>
                <tr>
                    <td>Aware State Threshold</td>
                    <td>
                        <select id="BasicThresholdDirection" name="BasicThresholdDirection" <%: Model.CanUpdate ? "" : "disabled='disabled'" %>>
                            <option value="-1" <%: (ViewData["BasicThresholdDirection"] == null || ViewData["BasicThresholdDirection"].ToInt() == -1) ? "selected='selected'" : "" %>><%: Html.TranslateTag("Select One","Select One")%></option>
                            <option value="0" <%: (ViewData["BasicThresholdDirection"] != null && ViewData["BasicThresholdDirection"].ToInt() == 0) ? "selected='selected'" : "" %>><%: Html.TranslateTag("Less Than","Less Than")%></option>
                            <option value="1" <%: (ViewData["BasicThresholdDirection"] != null && ViewData["BasicThresholdDirection"].ToInt() == 1) ? "selected='selected'" : "" %>><%: Html.TranslateTag("Greater Than","Greater Than")%></option>
                        </select>
                        <input id="BasicThreshold" name="BasicThreshold" value="<%: (ViewData["BasicThreshold"]) %>"  <%: (Model.CanUpdate ? "" : "disabled='disabled'") %> />°
                
                        <%: Html.ValidationMessage("BasicThreshold","Invalid Threshold")%>
                
                    </td>
                    <td>
                        <img alt="help" class="helpIcon" title="Aware state is a mode the sensor can be configured to enter at specific threshold.  This allows the sensor to transmit sooner when critical conditions of the application your monitoring are detected.  With basic monitoring the sensor will check against this threshold 6 times during every heartbeat (every 20 minutes if your heartbeat is 2 hours)." src="<%:Html.GetThemedContent("/images/help.png")%>" />
                    </td>
                </tr>
                <tr style="display:none;" id="BasicThresholdMax">
                    <td></td>
                    <td colspan="2"><div id="BasicThresholdMax_Slider"></div></td>
                </tr>
                <tr style="display:none;" id="BasicThresholdMin">
                    <td></td>
                    <td colspan="2"><div id="BasicThresholdMin_Slider"></div></td>
                </tr>
                <script type="text/javascript">
                    function setThresholdSliderVisibility()
                    {
                        switch($('#BasicThresholdDirection').val()){
                            case "-1":
                                $('#BasicThresholdMax').hide();
                                $('#BasicThresholdMin').hide();
                                break;
                            case "0":
                                $('#BasicThresholdMax').hide();
                                $('#BasicThresholdMin').show();
                                break;
                            case "1":
                                $('#BasicThresholdMax').show();
                                $('#BasicThresholdMin').hide();
                                break;
                        }
                    }

                    setThresholdSliderVisibility();
                    
                    $('#BasicThresholdMin_Slider').slider({
                        range: "max",
                        value: <%:Min%>,
                        min: <%:(DefaultMin)%>,
                        max: <%:(DefaultMax)%>,
                        <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
                        slide: function (event, ui) {
                            //update the amount by fetching the value in the value_array at index ui.value
                            $('#BasicThreshold').val(ui.value);
                        }
                    });
                    $('#BasicThresholdMax_Slider').slider({
                        range: "min",
                        value: <%:Max%>,
                        min: <%:(DefaultMin)%>,
                        max: <%:(DefaultMax)%>,
                        <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
                        slide: function (event, ui) {
                            //update the amount by fetching the value in the value_array at index ui.value
                            $('#BasicThreshold').val(ui.value);
                        }
                    });
                    $("#BasicThreshold").addClass('editField editFieldSmall');
                    $("#BasicThreshold").change(function () {
                        //Check if less than min
                        if ($("#MinimumThreshold_Manual").val() < <%:(DefaultMin)%>)
                            $("#MinimumThreshold_Manual").val(<%:(DefaultMin)%>);
                        
                        //Check if greater than max
                        if ($("#MinimumThreshold_Manual").val() > <%:(DefaultMax)%>)
                            $("#MinimumThreshold_Manual").val(<%:(DefaultMax)%>);

                        $('#BasicThresholdMax_Slider').slider("value", $("#BasicThreshold").val());
                        $('#BasicThresholdMin_Slider').slider("value", $("#BasicThreshold").val());
                    });

                    $("#BasicThresholdDirection").change(setThresholdSliderVisibility);
                </script>

                <% } %>