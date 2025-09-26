<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>
<%--APP_097--%>
<%
    bool isF = Thermostat.IsFahrenheit(Model.SensorID);
    Thermostat Data = Monnit.Thermostat.Deserialize(Model.FirmwareVersion, Model.LastDataMessage.Data);

    double minTemp = isF ? 10d.ToFahrenheit() : 10d; // 10 celsius
    double maxTemp = isF ? 40d.ToFahrenheit() : 40d; // 40 celsius
    double inputRange = maxTemp - minTemp;
    double tempValue = Math.Round(isF ? Data.Temperature.ToFahrenheit() : Data.Temperature, 1);
    double dataPercentValue = (1 - ((tempValue - minTemp) / inputRange)) * 100;
    dataPercentValue += 40; // 40deg is top of curve (baseline)
    string triangleRotationValue = dataPercentValue + "deg"; //40deg == maxTemp -- 140deg == minTemp

    string mode = Data.Heater ? "Heat" : Data.Cooler ? "Cool" : "Off";
    string dataPhaseName = mode;// Thermostat.ModeStringValue(mode);
    string dataPhaseSvg;
    switch (mode)
    {
        case "Heat":
            dataPhaseSvg = "fire";
            break;
        case "Cool":
            dataPhaseSvg = "snowflake";
            break;
        case "Off":
        default:
            dataPhaseSvg = "off";
            break;
    }

    double occupiedCoolingThreshold = Math.Round(Thermostat.GetOccupiedCoolingThreshold(Model), 0);
    double occupiedHeatingThreshold = Math.Round(Thermostat.GetOccupiedHeatingThreshold(Model), 0);
    //double occupiedCoolingBuffer = Math.Round(Thermostat.GetOccupiedCoolingBuffer(Model), 0);
    //double occupiedHeatingBuffer = Math.Round(Thermostat.GetOccupiedHeatingBuffer(Model), 0);

    double unoccupiedCoolingThreshold = Math.Round(Thermostat.GetCoolingThreshold(Model), 0);
    double unoccupiedHeatingThreshold = Math.Round(Thermostat.GetHeatingThreshold(Model), 0);

    //int OccupiedTempBuffer = Thermostat.GetOccupiedBuffer(Model.SensorID).ToInt();
    if (isF)
    {
        occupiedCoolingThreshold = Math.Round(occupiedCoolingThreshold.ToFahrenheit(), 0);
        occupiedHeatingThreshold = Math.Round(occupiedHeatingThreshold.ToFahrenheit(), 0);

        unoccupiedCoolingThreshold = Math.Round(unoccupiedCoolingThreshold.ToFahrenheit(), 0);
        unoccupiedHeatingThreshold = Math.Round(unoccupiedHeatingThreshold.ToFahrenheit(), 0);

    }

    double occupiedCoolingThresholdThumbPercentValue = (occupiedCoolingThreshold - minTemp) / inputRange;
    double occupiedHeatingThresholdThumbPercentValue = (occupiedHeatingThreshold - minTemp) / inputRange;

    double unoccupiedCoolingThresholdThumbPercentValue = (unoccupiedCoolingThreshold - minTemp) / inputRange;
    double unoccupiedHeatingThresholdThumbPercentValue = (unoccupiedHeatingThreshold - minTemp) / inputRange;

    bool isFanOn = Data.FanState;
    bool isOccupied = Data.OccupancyState;

%>


<%-- NOTES FOR THERMOSTAT

    🔘  .thermo-circle - The red ring around the circle is when approaching or past the heat setting. This element can be altered by the css property filter: drop-shadow(0px 0px 4px rgba(65, 168, 201, .69)); or I created a sperate property so you can change it in javascrip with .thermo-blu-circle 

    🔺  .red-triangle-pointer - Red triangle that points to the current temp. This alters between .blue-triangle-pointer. This pointer spins based on the css propert transform: rotate(90deg);
--%>

<div class="temp-reading_container" style="gap:1rem;">
    <div class="circle-temp_container">
        <div class="circle-temp-overlay">
        </div>
        <div class="two-columns-temp">
            <div class="circle-align">
                <div class="circle-temp-name"><%=Model.SensorName %></div>
                <div class="circle-temp-date">On <%=Model.LastDataMessage.MessageDate.OVToLocalDateShort() +" " + Model.LastDataMessage.MessageDate.OVToLocalTimeShort()%></div>

                <div class="circle-container">
                    <div class="thermo-circle"></div>
                    <div class='thermo-inner-circle'></div>
                    <div class="thermo-motion" data-title="Motion Detected">
                        <div class="temp-icons blue-icon <%=isOccupied ? "show" : "hide"%>"><%=Html.GetThemedSVG("app23") %></div>
                        <div class="moving-fan <%=isFanOn ? "show" : "hide" %>"><%=Html.GetThemedSVG("fan") %></div>
                    </div>
                    <div class="thermo-stats"><%=tempValue%><span>°<%=isF ? "F" : "C"%></span></div>
                    <div class="thermo-humid"><%=Data.Humidity %>% Humidity</div>

                    <div class="thermo-phase-container">
                        <div class="thermo-phase-btn">
                            <?xml version="1.0" encoding="UTF-8" ?>
                            <%if (dataPhaseSvg == "auto")
                                {%>
                            <div style="display: flex; gap: 5px">
                                <div class="snow-fire-icons">
                                    <%=Html.GetThemedSVG("fire") %>
                                </div>
                                <div style="width: 20px;">
                                    <%=Html.GetThemedSVG("snowflake") %>
                                </div>
                            </div>
                            <%}
                                else if (dataPhaseSvg != "off")
                                {%>
                            <div style="width: 25px"><%=Html.GetThemedSVG(dataPhaseSvg) %></div>
                            <%} %>
                        </div>
                    </div>

                    <div class="red-triangle-pointer"></div>
                </div>
            </div>

            <div class="circle-temp-slider">
                <svg class="temp-bar" xmlns="http://www.w3.org/2000/svg" data-name="Layer 1" viewBox="0 0 100 225.12">
                    <defs>
                        <linearGradient id="a" x1="-659.13" x2="-659.13" y1="8.93" y2="8.34" gradientTransform="matrix(83.85 0 0 -355.93 55318.91 3184.17)" gradientUnits="userSpaceOnUse">
                            <stop offset="0" stop-color="#ef1414" stop-opacity=".87" />
                            <stop offset="1" stop-color="#3caacb" />
                        </linearGradient>
                    </defs><path fill="url(#a)" d="m38.62 215.94-11.51-9.17 1.05-1.33 11.59 9.06-1.14 1.44Zm4.83-6.34-11.89-8.66.98-1.37 11.98 8.54-1.07 1.49Zm4.56-6.56-12.27-8.12.92-1.41 12.36 7.99-1.01 1.54Zm4.25-6.77-12.64-7.53.85-1.45 12.72 7.39-.94 1.59Zm3.93-6.98-12.99-6.9.78-1.49 13.07 6.75-.86 1.64Zm3.58-7.18-13.33-6.23.7-1.52 13.4 6.08-.78 1.68Zm3.21-7.36-13.63-5.53.62-1.55 13.7 5.36-.69 1.72Zm2.82-7.53-13.91-4.8.54-1.58 13.97 4.63-.59 1.76Zm2.42-7.67-14.15-4.04.45-1.61 14.2 3.86-.5 1.79Zm2-7.79-14.34-3.27.36-1.63 14.38 3.09-.4 1.81Zm1.57-7.89-14.5-2.49.27-1.65 14.53 2.31-.3 1.83Zm1.15-7.97-14.61-1.71.18-1.66 14.63 1.52-.2 1.84Zm.72-8-14.68-.93.1-1.67 14.69.75-.11 1.85Zm.3-8.02-14.71-.17v-1.68h14.72v1.85Zm-14.81-7.45-.07-1.68 14.69-.74.08 1.85-14.7.57Zm-.46-7.27-.16-1.68 14.64-1.45.17 1.84-14.66 1.29Zm-.81-7.26-.24-1.67L72.2 94.1l.26 1.82-14.58 1.98Zm-1.15-7.22-.31-1.66 14.45-2.79.34 1.81-14.47 2.64Zm-1.47-7.18-.38-1.65 14.31-3.41.42 1.79-14.34 3.27Zm-1.78-7.12-.45-1.63 14.16-4 .49 1.77-14.2 3.87Zm-2.08-7.05-.52-1.61 13.99-4.56.56 1.74-14.03 4.44Zm-2.36-6.98-.58-1.59 13.8-5.1.63 1.72-13.84 4.98Zm-2.63-6.89-.64-1.57 13.6-5.61.69 1.69-13.65 5.5Zm-2.88-6.79-.7-1.55 13.39-6.11.75 1.66-13.44 6Zm-3.13-6.68-.76-1.53 13.16-6.59.81 1.64-13.21 6.48Zm-3.37-6.57-.81-1.5 12.91-7.06.87 1.61-12.97 6.95Zm-3.6-6.43-.86-1.46 12.63-7.54.93 1.58L33.43 29Zm-3.84-6.28-.92-1.42 12.31-8.06 1 1.55-12.39 7.93Zm-4.06-6.01-.9-1.22L36.28 6.5s.43.55 1.19 1.61L25.53 16.7Z" data-name="Path 73" />
                </svg>
            </div>
            <div class="wrapit">
                <!------------------------ THUMB SLIDER     -->
               <div class="slider-container">
                    <input class="rangeInput" id="occupiedCoolingThresholdRange" type="range" min="<%=minTemp %>" max="<%=maxTemp %>">
                    <input class="rangeInput" id="occupiedHeatingThresholdRange" type="range" min="<%=minTemp %>" max="<%=maxTemp %>">
                    <input class="rangeInput" id="unoccupiedCoolingThresholdRange" type="range" min="<%=minTemp %>" max="<%=maxTemp %>">
                    <input class="rangeInput" id="unoccupiedHeatingThresholdRange" type="range" min="<%=minTemp %>" max="<%=maxTemp %>">

                    <svg id="slider" height="350" width="150" viewBox="-70 -10 200 300">
                        <path id="curve" stroke="gray" stroke-width="4" fill="none" d=""/>
                    
                        <circle id="occupiedHeatingThresholdThumb" class="thumbElement belowThumb inactiveThumb" stroke="#fff" fill="#fff" r="15" />
                        <text id="occupiedHeatingThresholdThumbNum" class="thumbElement belowThumbNum inactiveThumb" x="61.09375" y="148.75" text-anchor="middle" alignment-baseline="central"><%=occupiedHeatingThreshold %></text>
                        <circle id="occupiedCoolingThresholdThumb" class="thumbElement aboveThumb inactiveThumb" stroke="#fff" fill="#fff" r="15" />
                        <text id="occupiedCoolingThresholdThumbNum" class="thumbElement aboveThumbNum inactiveThumb" x="61.09375" y="148.75" text-anchor="middle" alignment-baseline="central"><%=occupiedCoolingThreshold %></text>

                        <circle id="unoccupiedHeatingThresholdThumb" class="thumbElement belowThumb inactiveThumb" stroke="#fff" fill="#fff" r="15" />
                        <text id="unoccupiedHeatingThresholdThumbNum" class="thumbElement belowThumbNum inactiveThumb" x="61.09375" y="148.75" text-anchor="middle" alignment-baseline="central"><%=unoccupiedHeatingThreshold %></text>
                        <circle id="unoccupiedCoolingThresholdThumb" class="thumbElement aboveThumb inactiveThumb" stroke="#fff" fill="#fff" r="15" />
                        <text id="unoccupiedCoolingThresholdThumbNum" class="thumbElement aboveThumbNum inactiveThumb" x="61.09375" y="148.75" text-anchor="middle" alignment-baseline="central"><%=unoccupiedCoolingThreshold %></text>
                    </svg>
                </div>
            </div>
        </div>
    </div>

    <div class="data-temp_container">
        <div class="overlay">
            <div class="alert-icon-oj">
                <%:Html.GetThemedSVG("Pending_Update") %>
            </div>
            <div>
                <div style="max-width: 200px">
                    Fields waiting to be written to sensor are not available for edit until transaction is complete.
                </div>
            </div>
        </div>
        <form action="/Overview/ThermostatUiEdit/<%=Model.SensorID %>" method="post" class="thermostatUiForm">
            <div class="data-temp-top-bar">

                <div class="toggle-bar">
                    <div class="menu-container">
                        <nav>
                            <ul id="fanMenu" class="menu">
                                <li class="dropdown dropdown-1 option-container">
                                    <div id="fanDropdownToggle" class="option-icons2 blue-icon general-btn"><%=Html.GetThemedSVG("fan") %></div>

                                    <ul id="fanDropdownList" class="dropdown_menu dropdown_menu-1 fan">
                                        <li data-value="0" class="dropdown_item-1 dropdown_item-fan"><%:Html.TranslateTag("Off") %></li>
                                        <li data-value="20" class="dropdown_item-fan"><%:Html.TranslateTag("20m") %></li>
                                        <li data-value="60" class="dropdown_item-fan"><%:Html.TranslateTag("1hr") %></li>
                                        <li data-value="240" class="dropdown_item-fan curveit"><%:Html.TranslateTag("4hr") %></li>
                                    </ul>
                                </li>
                            </ul>
                            <div class="output" style="text-align: center" id="fanLabel"></div>
                        </nav>
                        <input type="hidden" id="FanOverride" name="FanOverride" />
                    </div>
                    <div id="occupancyMenu" class="menu-container">
                        <nav>
                            <ul class="menu">
                                <li class="dropdown dropdown-2 option-container">
                                    <div id="occupancyDropdownToggle" class="option-icons2 blue-icon general-btn"><%=Html.GetThemedSVG("app23") %></div>

                                    <ul id="occupancyDropdownList" class="dropdown_menu dropdown_menu-1 occupancy">
                                        <li data-value="20" class="dropdown_item-motion"><%:Html.TranslateTag("Occupied - 20m") %></li>
                                        <li data-value="60" class="dropdown_item-motion"><%:Html.TranslateTag("Occupied - 1hr") %></li>
                                        <li data-value="240" class="dropdown_item-motion curveit"><%:Html.TranslateTag("Occupied - 4hr") %></li>
                                        <li data-value="-20" class="dropdown_item-motion"><%:Html.TranslateTag("Unoccupied - 20m") %></li>
                                        <li data-value="-60" class="dropdown_item-motion"><%:Html.TranslateTag("Unoccupied - 1hr") %></li>
                                        <li data-value="-240" class="dropdown_item-motion curveit"><%:Html.TranslateTag("Unoccupied - 4hr") %></li>
                                    </ul>
                                </li>
                            </ul>
                            <div class="output" style="text-align: center" id="occupancyLabel"></div>
                            <input type="hidden" id="OccupancyOverride" name="OccupancyOverride" />
                        </nav>
                    </div>
                </div>
            </div>

            <div class="occupied-tab">
                <button class="tab-btns link hover-underline-animation" onclick="openSelect('occupied'); return false;" data-selection="occupied"><%:Html.TranslateTag("Occupied") %></button>
                <button class="tab-btns link hover-underline-animation" onclick="openSelect('unoccupied'); return false;" data-selection="unoccupied"><%:Html.TranslateTag("Unoccupied") %></button>
            </div>

            <div id="occupied" class="selection" style="display: none">
                <div class="all-box-container">

                                        <div class="thermo-box-container">
                        <div class="thermo-minus blue-icon general-btn" data-id="occupiedCoolingThreshold">
                            <%=Html.GetThemedSVG("minus-circle") %>
                        </div>

                        <div class="thermo-box-above">
                            <div><%:Html.TranslateTag("Turn Cooling on at:") %></div>
                            <input id="occupiedCoolingThresholdInput" name="OccupiedCoolingThreshold" type="number" value="<%=occupiedCoolingThreshold %>" min="<%=minTemp %>" max="<%=maxTemp %>" class="below-temp-set">
                            <input id="occupiedCoolingBufferInput" name="OccupiedCoolingBuffer" type="hidden" value="<%=occupiedCoolingThreshold - 2 %>" >
                        </div>
                        <div class="thermo-plus blue-icon general-btn" data-id="occupiedCoolingThreshold">
                            <%=Html.GetThemedSVG("circle-add") %>
                        </div>
                    </div>

                    <div class="thermo-box-container">
                        <div class="thermo-minus blue-icon general-btn" data-id="occupiedHeatingThreshold">
                            <%=Html.GetThemedSVG("minus-circle") %>
                        </div>

                        <div class="thermo-box-below">
                            <div><%:Html.TranslateTag("Turn Heating on at:") %></div>
                            <input id="occupiedHeatingThresholdInput" name="OccupiedHeatingThreshold" type="number" value="<%=occupiedHeatingThreshold %>" min="<%=minTemp %>" max="<%=maxTemp %>" class="below-temp-set">
                            <input id="occupiedHeatingBufferInput" name="OccupiedHeatingBuffer" type="hidden" value="<%=occupiedHeatingThreshold + 2 %>" >
                        </div>
                        <div class="thermo-plus blue-icon general-btn" data-id="occupiedHeatingThreshold">
                            <%=Html.GetThemedSVG("circle-add") %>
                        </div>
                    </div>


                </div>

            </div>

            <div id="unoccupied" class="selection" style="display: none">
                <div class="all-box-container">

                                        <div class="thermo-box-container">
                        <div class="thermo-minus blue-icon general-btn" data-id="unoccupiedCoolingThreshold">
                            <%=Html.GetThemedSVG("minus-circle") %>
                        </div>

                        <div class="thermo-box-above">
                            <div><%:Html.TranslateTag("Turn Cooling on at:") %></div>
                            <input id="unoccupiedCoolingThresholdInput" name="CoolingThreshold" type="number" value="<%=unoccupiedCoolingThreshold %>" min="<%=minTemp %>" max="<%=maxTemp %>" class="below-temp-set">
                            <input id="unoccupiedCoolingBufferInput" name="CoolingBuffer" type="hidden" value="<%=unoccupiedCoolingThreshold - 2 %>" >
                        </div>
                        <div class="thermo-plus blue-icon general-btn" data-id="unoccupiedCoolingThreshold">
                            <%=Html.GetThemedSVG("circle-add") %>
                        </div>
                    </div>

                    <div class="thermo-box-container">
                        <div class="thermo-minus blue-icon general-btn" data-id="unoccupiedHeatingThreshold">
                            <%=Html.GetThemedSVG("minus-circle") %>
                        </div>

                        <div class="thermo-box-below">
                            <div><%:Html.TranslateTag("Turn Heating on at:") %></div>
                            <input id="unoccupiedHeatingThresholdInput" name="HeatingThreshold" type="number" value="<%=unoccupiedHeatingThreshold %>" min="<%=minTemp %>" max="<%=maxTemp %>" class="below-temp-set">
                            <input id="unoccupiedHeatingBufferInput" name="HeatingBuffer" type="hidden" value="<%=unoccupiedHeatingThreshold + 2 %>" >
                        </div>
                        <div class="thermo-plus blue-icon general-btn" data-id="unoccupiedHeatingThreshold">
                            <%=Html.GetThemedSVG("circle-add") %>
                        </div>
                    </div>


                </div>

            </div>

            <div class="bottom-container" id="btmContainer">
                <input type="hidden" name="ReportInterval" value="<%=Model.ReportInterval %>" />
                <button type="submit" class="saveBtn btn btn-primary" id="ThermoSaveBtn" onclick="tempSetBuffers()"><%:Html.TranslateTag("SAVE") %></button>
            </div>
        </form>
    </div>

    <div id="successToast">
        <div>
            <%=Html.GetThemedSVG("circle-check-green") %>
        </div>
        <div id="toastMessage">
        </div>
    </div>

    <div id="errorToast">
        <div style="border: 2px solid white; border-radius: 50%; width: 20px;">
            !
        </div>
        <div id="errorToastMessage">
        </div>
    </div>

</div>


<script>
    $(document).ready(function () {
        $('.dropdown-toggle').dropdown();

        $(".default_option").click(function () {
            $(this).parent().toggleClass("active");
        });

        $(".select_ul li").click(function () {
            var currentele = $(this).html();
            $(".default_option li").html(currentele);
            $(this).parents(".select_wrap").removeClass("active");

            var modeNum = this.id.replace("TempMode", "");

            $('#HeatCoolMode').val(modeNum); //populate hidden field
            $('#MultistageSaveBtn').show();

        });
<%--        var currentele = $('#TempMode<%=MultiStageThermostat.GetHeatCoolMode(Model)%>');
        $(".default_option li").html(currentele.html());
        currentele.parents(".select_wrap").removeClass("active");--%>


        $(".fan li").click(function () {
            $(".option-icons").removeClass("active");
            //$(this).parent().closest(".dropdown-1").find(".option-icons").addClass("active");

            var value = $(this).data('value');
            $('#fanLabel').html("Fan <br/> " + value + " min");//populate label
            $('#FanOverride').val(value); //populate hidden field
            $('#ThermoSaveBtn').show();
        });
        $(".occupancy li").click(function () {
            $(".option-icons").removeClass("active");

            var value = $(this).data('value');

            if (value < 0) {
                $('#occupancyLabel').html("Unoccupied " + value * -1 + ' min');//populate label
            }
            else {
                $('#occupancyLabel').html("Occupied " + value + ' min');//populate label
            }
            $('#OccupancyOverride').val(value); //populate hidden field
            $('#ThermoSaveBtn').show();
        });

        function submitMotionAction(value) {
            value = Number(value);
            var name = 'OccupiedTimeout';
            if (value < 0) {
                value = Math.abs(value);
                name = 'UnoccupiedTimeout';
            }

            var formData = new FormData();
            formData.append(name, value);
        }
        $(".dropdown_item-motion").click(function () {
            $(".option-icons2").removeClass("active");
            $(this).closest(".dropdown-2").find(".option-icons2").addClass("active");

            var value = this.getAttribute('data-value');
            submitMotionAction(value);
        });
        $(".dropdown_item-2").click(function () {
            $(".option-icons2").removeClass("active");

            var value = this.getAttribute('data-value');
            submitMotionAction(value);
        });
    });



    // #region Thermo Slider
    const curveEl = document.getElementById('curve');
    const curve = {
        x: 0,
        y: 0,
        cpx: 125,
        cpy: 175,
        endx: 0,
        endy: 350
    };
    // get the XY at the specified percentage along the curve
    const getQuadraticBezierXYatPercent = (curve, percent) => {
        let x = Math.pow(1 - percent, 2) * curve.x + 2 * (1 - percent) * percent *
            curve.cpx + Math.pow(percent, 2) * curve.endx;
        let y = Math.pow(1 - percent, 2) * curve.y + 2 * (1 - percent) * percent *
            curve.cpy + Math.pow(percent, 2) * curve.endy;
        return { x, y };
    };
    const drawCurve = () => {
        curveEl.setAttribute('d', `M${curve.x},${curve.y} Q${curve.cpx},${curve.cpy} ${curve.endx},${curve.endy}`);
    };
    const drawThumb = (percent, thumbElement, numberElement) => {
        let pos = getQuadraticBezierXYatPercent(curve, percent);
        thumbElement.setAttribute('cx', pos.x);
        thumbElement.setAttribute('cy', pos.y);
        //console.log('Thumb Position:', pos.x, pos.y);
        numberElement.setAttribute('x', pos.x);
        numberElement.setAttribute('y', pos.y);
    };

    function updateValue(thumbNumberElement, valueElement, value) {
        thumbNumberElement.textContent = value;
        valueElement.value = value;
        //console.log('update: ' + rangeEl.value);
    }
    const moveThumb = (e, isInput) => {
        let rangeElement = e.target != null ? e.target : document.getElementById(e.id);
        let elementNameStarter = rangeElement.id.replace('Range', '');
        let thumbElement = document.getElementById(elementNameStarter + 'Thumb');
        let numberElement = document.getElementById(elementNameStarter + 'ThumbNum');
        let valueElement = document.getElementById(elementNameStarter + 'Input');
        let saveBtnElement = document.getElementById('ThermoSaveBtn');

        let min = Number(rangeElement.getAttribute('min'));
        let max = Number(rangeElement.getAttribute('max'));
        let inputRange = max - min;
        let value = isInput ? valueElement.value : rangeElement.value;

        if (value < min) {
            value = min;
        }
        if (value > max) {
            value = max;
        }

        if (isInput)
            valueElement.value = value;
        else
            rangeElement.value = value;

        let percent = 1 - ((value - min) / inputRange);
        //console.log('movethumb - ' + rangeElement.id);

        drawThumb(percent, thumbElement, numberElement);
        updateValue(numberElement, valueElement, value);

        if (isInput) {
            thumbElement.classList.remove('inactiveThumb');
            numberElement.classList.remove('inactiveThumb');

            if (saveBtnElement != undefined && saveBtnElement != null && (saveBtnElement.style.display === '' || saveBtnElement.style.display === 'none'))
                saveBtnElement.style.display = 'inline-block';
        }
    };
    const thumbClick = e => {
        if (detectInputType() === 'touch') {
            return
        }
        var thumbID = e.target.id;
        let elementNameStarter = thumbID.replace('ThumbNum', '').replace('Thumb', '');
        var rangeElement = document.getElementById(elementNameStarter + 'Range');
        rangeElement.style.zIndex = 10;
        var thumbElement = document.getElementById(elementNameStarter + 'Thumb');
        var thumbNumberElement = document.getElementById(elementNameStarter + 'ThumbNum');
        thumbElement.classList.remove('inactiveThumb');
        thumbNumberElement.classList.remove('inactiveThumb');
    }
    const thumbMouseout = e => {
        var elementNameStarter = e.target.id.replace('Range', '').replace('Input', '');
        var rangeElement = e.target;
        rangeElement.style.zIndex = 1;
        var thumbElement = document.getElementById(elementNameStarter + 'Thumb');
        var thumbNumberElement = document.getElementById(elementNameStarter + 'ThumbNum');
        thumbElement.classList.add('inactiveThumb');
        thumbNumberElement.classList.remove('inactiveThumb');
    }
    const inputMouseout = e => {
        var elementNameStarter = e.target.id.replace('Input', '');
        var inputElement = e.target;
        inputElement.style.zIndex = 1;
        var thumbElement = document.getElementById(elementNameStarter + 'Thumb');
        var thumbNumberElement = document.getElementById(elementNameStarter + 'ThumbNum');
        thumbElement.classList.add('inactiveThumb');
        thumbNumberElement.classList.remove('inactiveThumb');

        var value = Number(inputElement.value);
        var min = Number(inputElement.getAttribute('min'));
        var max = Number(inputElement.getAttribute('max'));

        if (value < min) {
            value = min;
        }
        if (value > max) {
            value = max;
        }
        inputElement.value = value;
    }

    document.addEventListener("DOMContentLoaded", () => {
        drawCurve();

        // #region Multi Slider usability logic
        const occupiedCoolingThresholdRangeEl = document.getElementById('occupiedCoolingThresholdRange');
        const occupiedCoolingThresholdThumbEl = document.getElementById('occupiedCoolingThresholdThumb');
        const occupiedCoolingThresholdThumbNumEl = document.getElementById('occupiedCoolingThresholdThumbNum');
        const occupiedCoolingThresholdInputEl = document.getElementById('occupiedCoolingThresholdInput');
        const occupiedCoolingThresholdPercent = Number('<%=occupiedCoolingThresholdThumbPercentValue%>');

        const occupiedHeatingThresholdRangeEl = document.getElementById('occupiedHeatingThresholdRange');
        const occupiedHeatingThresholdThumbEl = document.getElementById('occupiedHeatingThresholdThumb');
        const occupiedHeatingThresholdThumbNumEl = document.getElementById('occupiedHeatingThresholdThumbNum');
        const occupiedHeatingThresholdInputEl = document.getElementById('occupiedHeatingThresholdInput');
        const occupiedHeatingThresholdPercent = Number('<%=occupiedHeatingThresholdThumbPercentValue%>');

        const unoccupiedCoolingThresholdRangeEl = document.getElementById('unoccupiedCoolingThresholdRange');
        const unoccupiedCoolingThresholdThumbEl = document.getElementById('unoccupiedCoolingThresholdThumb');
        const unoccupiedCoolingThresholdThumbNumEl = document.getElementById('unoccupiedCoolingThresholdThumbNum');
        const unoccupiedCoolingThresholdInputEl = document.getElementById('unoccupiedCoolingThresholdInput');
        const unoccupiedCoolingThresholdPercent = Number('<%=unoccupiedCoolingThresholdThumbPercentValue%>');

        const unoccupiedHeatingThresholdRangeEl = document.getElementById('unoccupiedHeatingThresholdRange');
        const unoccupiedHeatingThresholdThumbEl = document.getElementById('unoccupiedHeatingThresholdThumb');
        const unoccupiedHeatingThresholdThumbNumEl = document.getElementById('unoccupiedHeatingThresholdThumbNum');
        const unoccupiedHeatingThresholdInputEl = document.getElementById('unoccupiedHeatingThresholdInput');
        const unoccupiedHeatingThresholdPercent = Number('<%=unoccupiedHeatingThresholdThumbPercentValue%>');

        addEventListenerForClass('click', 'thermo-minus', function (e) {
            //console.log("minus");
            var dataID = this.getAttribute('data-id');
            var inputElement = document.getElementById(dataID + "Input");
            var rangeElement = document.getElementById(dataID + "Range");

            var newValue = Number(inputElement.value) - 1;
            var min = Number(inputElement.getAttribute('min'));
            var max = Number(inputElement.getAttribute('max'));

            if (newValue < min) {
                newValue = min;
            }
            if (newValue > max) {
                newValue = max;
            }
            inputElement.value = newValue;
            //console.log(newValue, "inMinus")
            moveThumb(rangeElement, true);
        });
        addEventListenerForClass('click', 'thermo-plus', function (e) {
            //console.log("plus");
            var dataID = this.getAttribute('data-id');
            var inputElement = document.getElementById(dataID + "Input");
            var rangeElement = document.getElementById(dataID + "Range");

            var newValue = Number(inputElement.value) + 1;
            var min = Number(inputElement.getAttribute('min'));
            var max = Number(inputElement.getAttribute('max'));

            if (newValue < min) {
                newValue = min;
            }
            if (newValue > max) {
                newValue = max;
            }
            inputElement.value = newValue;
            //console.log(newValue, "inPlus")
            moveThumb(rangeElement, true);
        });

        occupiedCoolingThresholdRangeEl.addEventListener('input', e => moveThumb(e, false));
        occupiedCoolingThresholdRangeEl.value = Number('<%=occupiedCoolingThreshold%>');
        occupiedCoolingThresholdThumbEl.addEventListener('touchstart', e => thumbClick(e));
        occupiedCoolingThresholdThumbEl.addEventListener('click', e => thumbClick(e));
        occupiedCoolingThresholdThumbNumEl.addEventListener('touchstart', e => thumbClick(e));
        occupiedCoolingThresholdThumbNumEl.addEventListener('click', e => thumbClick(e));
        occupiedCoolingThresholdRangeEl.addEventListener('mouseup', e => thumbMouseout(e));
        occupiedCoolingThresholdRangeEl.addEventListener('touchend', e => thumbMouseout(e));
        occupiedCoolingThresholdInputEl.addEventListener('change', e => moveThumb(occupiedCoolingThresholdRangeEl, true));
        occupiedCoolingThresholdInputEl.addEventListener('blur', e => inputMouseout(e));

        occupiedHeatingThresholdRangeEl.addEventListener('input', e => moveThumb(e, false));
        occupiedHeatingThresholdRangeEl.value = Number('<%=occupiedHeatingThreshold%>');
        occupiedHeatingThresholdThumbEl.addEventListener('touchstart', e => thumbClick(e));
        occupiedHeatingThresholdThumbEl.addEventListener('click', e => thumbClick(e));
        occupiedHeatingThresholdThumbNumEl.addEventListener('touchstart', e => thumbClick(e));
        occupiedHeatingThresholdThumbNumEl.addEventListener('click', e => thumbClick(e));
        occupiedHeatingThresholdRangeEl.addEventListener('mouseup', e => thumbMouseout(e));
        occupiedHeatingThresholdRangeEl.addEventListener('touchend', e => thumbMouseout(e));
        occupiedHeatingThresholdInputEl.addEventListener('change', e => moveThumb(occupiedHeatingThresholdRangeEl, true));
        occupiedHeatingThresholdInputEl.addEventListener('blur', e => inputMouseout(e));

        unoccupiedCoolingThresholdRangeEl.addEventListener('input', e => moveThumb(e, false));
        unoccupiedCoolingThresholdRangeEl.value = Number('<%=unoccupiedCoolingThreshold%>');
        unoccupiedCoolingThresholdThumbEl.addEventListener('touchstart', e => thumbClick(e));
        unoccupiedCoolingThresholdThumbEl.addEventListener('click', e => thumbClick(e));
        unoccupiedCoolingThresholdThumbNumEl.addEventListener('touchstart', e => thumbClick(e));
        unoccupiedCoolingThresholdThumbNumEl.addEventListener('click', e => thumbClick(e));
        unoccupiedCoolingThresholdRangeEl.addEventListener('mouseup', e => thumbMouseout(e));
        unoccupiedCoolingThresholdRangeEl.addEventListener('touchend', e => thumbMouseout(e));
        unoccupiedCoolingThresholdInputEl.addEventListener('change', e => moveThumb(unoccupiedCoolingThresholdRangeEl, true));
        unoccupiedCoolingThresholdInputEl.addEventListener('blur', e => inputMouseout(e));

        unoccupiedHeatingThresholdRangeEl.addEventListener('input', e => moveThumb(e, false));
        unoccupiedHeatingThresholdRangeEl.value = Number('<%=unoccupiedHeatingThreshold%>');
        unoccupiedHeatingThresholdThumbEl.addEventListener('touchstart', e => thumbClick(e));
        unoccupiedHeatingThresholdThumbEl.addEventListener('click', e => thumbClick(e));
        unoccupiedHeatingThresholdThumbNumEl.addEventListener('touchstart', e => thumbClick(e));
        unoccupiedHeatingThresholdThumbNumEl.addEventListener('click', e => thumbClick(e));
        unoccupiedHeatingThresholdRangeEl.addEventListener('mouseup', e => thumbMouseout(e));
        unoccupiedHeatingThresholdRangeEl.addEventListener('touchend', e => thumbMouseout(e));
        unoccupiedHeatingThresholdInputEl.addEventListener('change', e => moveThumb(unoccupiedHeatingThresholdRangeEl, true));
        unoccupiedHeatingThresholdInputEl.addEventListener('blur', e => inputMouseout(e));
        //#endregion

        openSelect('occupied');

        //Show save button when the thumb dial moves. 

        const targetElements = document.querySelectorAll('#occupiedCoolingThresholdThumbNum, #occupiedHeatingThresholdThumbNum, #unoccupiedCoolingThresholdThumbNum, #unoccupiedHeatingThresholdThumbNum');
        const targetArray = Array.from(targetElements);

        const observer = new MutationObserver((mutationsList, observer) => {
            for (const mutation of mutationsList) {
                if (targetArray.includes(mutation.target)) {
                    document.querySelector('.saveBtn').style.display = "block";
                    break;
                }
            }
        });

        const config = { attribute: true, childList: true, subtree: true };
        observer.observe(document.body, config);


        // Show Success Toast on form submit success
        const showSuccessToast = () => {
            const successToast = document.querySelector("#successToast");
            document.getElementById("toastMessage").innerHTML = "Settings updated"
            successToast.classList.add("showToast");

            setTimeout(() => {
                successToast.classList.add("toastExit");
            }, 1500);

            setTimeout(() => {
                successToast.classList.remove("showToast");
                successToast.classList.remove("toastExit");
            }, 2000);
        };


        // Show Error Toast on form error
        const showErrorToast = () => {
            const errorToast = document.querySelector("#errorToast");
            document.getElementById("errorToastMessage").innerHTML = "Error please try again";
            errorToast.classList.add("showToast");

            setTimeout(() => {
                errorToast.classList.add("toastExit");
            }, 1500);

            setTimeout(() => {
                errorToast.classList.remove("showToast");
                errorToast.classList.remove("toastExit");
            }, 2000);
        };

        addEventListenerForClass('submit', 'thermostatUiForm', function (e) {
            e.preventDefault();
            var formData = new FormData(this);

            fetch(this.action, { method: 'post', body: formData }).then(async response => {
                var data = await response.text();

                if (data === "Success") {
                    showSuccessToast();
                } else {
                    showErrorToast();
                }
            });
        });
    });


    function openSelect(selection) {
        var i;
        var x = document.getElementsByClassName("selection");
        for (i = 0; i < x.length; i++) {
            x[i].style.display = "none";
        }
        document.getElementById(selection).style.display = "block";

        var buttons = document.getElementsByClassName("tab-btns");
        for (i = 0; i < buttons.length; i++) {
            buttons[i].classList.remove("activeAnimate");
        }
        document.querySelector("button[data-selection='" + selection + "']").classList.add("activeAnimate");

        var items = document.getElementsByClassName('thumbElement');
        for (i = 0; i < items.length; i++) {
            var item = items[i];
            item.style.display = 'none';
        }

        // Thermo UI
        var elementStarterName = selection + 'CoolingThreshold';
        var rangeElement = document.getElementById(elementStarterName + 'Range');
        var inputElement = document.getElementById(elementStarterName + 'Input');
        var preMoveValue = inputElement.value;
        moveThumb(rangeElement, false);
        var thumbElement = document.getElementById(elementStarterName + 'Thumb');
        thumbElement.style.display = 'inline';
        var thumbNumberElement = document.getElementById(elementStarterName + 'ThumbNum');
        thumbNumberElement.style.display = 'block';
        updateValue(thumbNumberElement, inputElement, preMoveValue);
        thumbElement.classList.remove('inactiveThumb');
        thumbNumberElement.classList.remove('inactiveThumb');

        elementStarterName = selection + 'HeatingThreshold';
        rangeElement = document.getElementById(elementStarterName + 'Range');
        inputElement = document.getElementById(elementStarterName + 'Input');
        preMoveValue = inputElement.value;
        moveThumb(rangeElement, false);
        thumbElement = document.getElementById(elementStarterName + 'Thumb');
        thumbElement.style.display = 'inline';
        thumbNumberElement = document.getElementById(elementStarterName + 'ThumbNum');
        thumbNumberElement.style.display = 'block';
        updateValue(thumbNumberElement, inputElement, preMoveValue);
        thumbElement.classList.remove('inactiveThumb');
        thumbNumberElement.classList.remove('inactiveThumb');
    }
    // #endregion


    //Dropdown logic

    function detectInputType() {
        if ('ontouchstart' in window || navigator.maxTouchPoints) {
            return 'touch';
        } else {
            return 'mouse';
        }
    }

    if (detectInputType() === 'touch') {

        function setupDropdown(dropdownToggleId, dropdownMenuId, itemClassName) {
            const dropdownToggle = document.getElementById(dropdownToggleId);
            const dropdownMenu = document.getElementById(dropdownMenuId);

            dropdownToggle.addEventListener('click', () => {
                if (dropdownMenu.style.display === 'block') {
                    dropdownMenu.style.display = 'none';
                } else {
                    dropdownMenu.style.display = 'block';
                }
            });

            const dropdownItems = dropdownMenu.querySelectorAll(`.${itemClassName}`);
            dropdownItems.forEach((item) => {
                item.addEventListener('click', () => {
                    dropdownMenu.style.display = 'none';
                });
            });
        }

        setupDropdown('fanDropdownToggle', 'fanDropdownList', 'dropdown_item-fan');
        setupDropdown('occupancyDropdownToggle', 'occupancyDropdownList', 'dropdown_item-motion');

    } else {
        const menus = document.querySelectorAll('.dropdown_menu');

        const hideElement = (element) => {
            element.classList.add('hide');

            setTimeout(() => {
                element.classList.remove('hide');
            }, 250);

        };

        menus.forEach(menu => {
            menu.addEventListener('click', () => hideElement(menu));
        });
    }

    /*toggle overlay for thermostat setting card. */

    const saveButton = document.querySelector("#ThermoSaveBtn");
    const overlay = document.querySelector(".overlay");
    const overlayCircleThermostat = document.querySelector(".circle-temp-overlay");

    const showOverlay = (userUpdated) => {
        let sendingResponseToNetwork = <%=Model.IsDirty ? "true" : "false" %>;

        if (sendingResponseToNetwork || userUpdated) {
            overlay.style.display = "flex";
            overlayCircleThermostat.style.display = "block";
        } else {
            overlay.style.display = "none";
            overlayCircleThermostat.style.display = "none";
        }
    };

    showOverlay();
    saveButton.addEventListener('click', () => showOverlay(true));



    // Function to check if the user is using an Android device
    function isAndroidDevice() {
        return /Android/i.test(navigator.userAgent);
    }

    // Function to apply styles for Android users
    function applyAndroidStyles() {
        if (isAndroidDevice()) {
            // Select elements with class "adjustmentOnAndroid" and apply styles
            var androidAdjustments = document.querySelector('#adjustmentOnAndroid');
            androidAdjustments.style.textAlign = 'start';
            androidAdjustments.style.width = '100%';
            androidAdjustments.style.paddingLeft = '0.5rem';

            // Select elements with class "adjustmentOnAndroidDate" and apply styles
            var androidDateAdjustments = document.querySelector('#adjustmentOnAndroidDate');
            androidDateAdjustments.style.textAlign = 'center';
            androidDateAdjustments.style.marginLeft = '-2rem';
            androidDateAdjustments.style.width = '100%';
        }
    }

    // Run the function on page load
    window.addEventListener('load', applyAndroidStyles);

    function tempSetBuffers() {
        //Occupied Cooling Buffer
        $('#occupiedCoolingBufferInput').val($('#occupiedCoolingThresholdInput').val() * 1 - 2)
        //Occupied Heating Buffer
        $('#occupiedHeatingBufferInput').val($('#occupiedHeatingThresholdInput').val() * 1 + 2)

        //Unoccupied Cooling Buffer
        $('#unoccupiedCoolingBufferInput').val($('#unoccupiedCoolingThresholdInput').val() * 1 - 2)
        //Unoccupied Heating Buffer
        $('#unoccupiedHeatingBufferInput').val($('#unoccupiedHeatingThresholdInput').val() * 1 + 2)
    }
</script>


<style>
    #successToast {
        display: none;
        -webkit-animation: scale-in-bottom 0.5s cubic-bezier(0.250, 0.460, 0.450, 0.940) both;
        animation: scale-in-bottom 0.5s cubic-bezier(0.250, 0.460, 0.450, 0.940) both;
        opacity: 0;
        position: fixed;
        bottom: 2%;
        left: 50%;
        width: 250px;
        border-radius: 12px;
        background-color: #43be5f;
        color: white;
        padding: 1rem;
        text-align: center;
        z-index: 4;
        justify-content: center;
        align-items: flex-end;
    }

    #errorToast {
        display: none;
        -webkit-animation: scale-in-bottom 0.5s cubic-bezier(0.250, 0.460, 0.450, 0.940) both;
        animation: scale-in-bottom 0.5s cubic-bezier(0.250, 0.460, 0.450, 0.940) both;
        opacity: 0;
        position: fixed;
        bottom: 2%;
        left: 50%;
        width: 250px;
        border-radius: 12px;
        background-color: red;
        color: white;
        padding: 1rem;
        text-align: center;
        z-index: 4;
        justify-content: center;
        align-items: flex-end;
    }

    @-webkit-keyframes scale-in-bottom {
        0% {
            -webkit-transform: scale(0);
            transform: scale(0);
            -webkit-transform-origin: 50% 100%;
            transform-origin: 50% 100%;
            opacity: 1;
        }

        100% {
            -webkit-transform: scale(1);
            transform: scale(1);
            -webkit-transform-origin: 50% 100%;
            transform-origin: 50% 100%;
            opacity: 1;
        }
    }

    @keyframes scale-in-bottom {
        0% {
            -webkit-transform: scale(0);
            transform: scale(0);
            -webkit-transform-origin: 50% 100%;
            transform-origin: 50% 100%;
            opacity: 1;
        }

        100% {
            -webkit-transform: scale(1);
            transform: scale(1);
            -webkit-transform-origin: 50% 100%;
            transform-origin: 50% 100%;
            opacity: 1;
        }
    }


    .toastExit {
        -webkit-animation: scale-out-bottom 0.5s cubic-bezier(0.550, 0.085, 0.680, 0.530) both !important;
        animation: scale-out-bottom 0.5s cubic-bezier(0.550, 0.085, 0.680, 0.530) both !important;
    }

    @-webkit-keyframes scale-out-bottom {
        0% {
            -webkit-transform: scale(1);
            transform: scale(1);
            -webkit-transform-origin: 50% 100%;
            transform-origin: 50% 100%;
            opacity: 1;
        }

        100% {
            -webkit-transform: scale(0);
            transform: scale(0);
            -webkit-transform-origin: 50% 100%;
            transform-origin: 50% 100%;
            opacity: 1;
        }
    }

    #successToast svg, #errorToast svg {
        width: 1.25rem;
        height: 1.25rem;
    }

    #toastMessage, #errorToastMessage {
        padding-left: 0.5rem;
    }

    #successToast.showToast, #errorToast.showToast {
        display: flex;
    }

    .alert-icon-oj svg {
        fill: #f89725;
        width: 2rem;
        height: 2rem;
        -webkit-animation: heartbeat 1.5s ease-in-out infinite both;
        animation: heartbeat 1.5s ease-in-out infinite both;
    }

    /**
 * ----------------------------------------
 * animation heartbeat
 * ----------------------------------------
 */
    @-webkit-keyframes heartbeat {
        from {
            -webkit-transform: scale(1);
            transform: scale(1);
            -webkit-transform-origin: center center;
            transform-origin: center center;
            -webkit-animation-timing-function: ease-out;
            animation-timing-function: ease-out;
        }

        10% {
            -webkit-transform: scale(0.91);
            transform: scale(0.91);
            -webkit-animation-timing-function: ease-in;
            animation-timing-function: ease-in;
        }

        17% {
            -webkit-transform: scale(0.98);
            transform: scale(0.98);
            -webkit-animation-timing-function: ease-out;
            animation-timing-function: ease-out;
        }

        33% {
            -webkit-transform: scale(0.87);
            transform: scale(0.87);
            -webkit-animation-timing-function: ease-in;
            animation-timing-function: ease-in;
        }

        45% {
            -webkit-transform: scale(1);
            transform: scale(1);
            -webkit-animation-timing-function: ease-out;
            animation-timing-function: ease-out;
        }
    }

    @keyframes heartbeat {
        from {
            -webkit-transform: scale(1);
            transform: scale(1);
            -webkit-transform-origin: center center;
            transform-origin: center center;
            -webkit-animation-timing-function: ease-out;
            animation-timing-function: ease-out;
        }

        10% {
            -webkit-transform: scale(0.91);
            transform: scale(0.91);
            -webkit-animation-timing-function: ease-in;
            animation-timing-function: ease-in;
        }

        17% {
            -webkit-transform: scale(0.98);
            transform: scale(0.98);
            -webkit-animation-timing-function: ease-out;
            animation-timing-function: ease-out;
        }

        33% {
            -webkit-transform: scale(0.87);
            transform: scale(0.87);
            -webkit-animation-timing-function: ease-in;
            animation-timing-function: ease-in;
        }

        45% {
            -webkit-transform: scale(1);
            transform: scale(1);
            -webkit-animation-timing-function: ease-out;
            animation-timing-function: ease-out;
        }
    }

    .hide {
        display: none;
    }

    .show {
        display: block;
    }

    .temp-reading_container {
        display: flex;
    }

    .circle-temp_container {
        width: 100%;
        min-height: 450px;
        background: white;
        border-radius: .5rem;
        box-shadow: 2px 3px 6px #0000003B;
        display: flex;
        flex-direction: column;
        padding: 1rem;
    }

    .circle-align {
        align-items: center;
        display: flex;
        flex-direction: column;
    }

    .two-columns-temp {
        display: flex;
        justify-content: center;
        margin-right: -4rem;
    }

    .data-temp_container {
        width: 100%;
        background: white;
        border-radius: .5rem;
        box-shadow: 2px 3px 6px #0000003B;
        position: relative;
    }

    .overlay {
        display: none;
        width: 97%;
        height: 97%;
        position: absolute;
        z-index: 3;
        background-color: #d3d3d3d6;
        color: #f89725;
        font-weight: 600;
        border-radius: .5rem;
        padding: .75rem;
        flex-direction: column;
        align-items: center;
    }

    .circle-temp-overlay {
        background: #00800000;
        position: absolute;
        width: 500px;
        height: 420px;
        z-index: 100;
    }

    .circle-reading {
        position: relative;
        width: 300px;
        height: 300px;
        border-radius: 150px;
        border: 2px solid #ff000061;
        margin: 1rem;
        align-items: center;
        background: white;
        box-shadow: rgb(204, 219, 232) 3px 3px 6px 0px inset, rgba(255, 255, 255, .01) -3px -3px 6px 1px inset;
        box-shadow: rgba(255, 0, 0, 0.2) 0px 0px 10px 0px, rgba(255, 0, 0, 0.5) 0px 1px 1px 0px, rgb(231 37 37 / 10%) 0px 0px 0px 6px inset;
    }

    .inner-circle-shadow {
        width: 300px;
        padding: 1px;
        position: absolute;
        height: 300px;
        border-radius: 150px;
        box-shadow: rgba(50, 50, 93, 0.25) 0px 30px 60px -1px inset, rgba(0, 0, 0, 0.3) 0px 0px 5px 0px inset;
    }

    .circle-temp-name {
        font-size: 18px;
        font-weight: bold;
        margin-top: 20px;
    }

    .circle-temp-date {
        color: #0067ab;
    }

    .circle-main-temp {
        position: relative;
        display: flex;
        align-items: center;
        justify-content: center;
        width: 100%;
        height: 45%;
        font-size: 4rem;
        font-weight: bold;
        color: #515356;
    }

    .circle-temp-slider {
        position: relative;
    }

    .circle-main-humid {
        position: relative;
        display: flex;
        justify-content: center;
        width: 100%;
        bottom: 28px;
        font-size: 1.1rem;
    }

    .add-sub-circle {
        display: flex;
        justify-content: space-around;
        width: 100%;
        bottom: 6px;
        position: relative;
    }

    .circle-phase {
        width: 60px;
        height: 60px;
        background: #FFFFFF 0% 0% no-repeat padding-box;
        box-shadow: inset 10px 1px 20px #0000002F, 0px 3px 6px #00000029;
        border: 1px solid #7070701F;
        border-radius: 30px;
        align-items: center;
        display: flex;
        justify-content: center;
    }

    .circle-phase-text {
        position: relative;
        display: flex;
        justify-content: center;
        top: 15px;
        color: #515356;
        font-size: 13px;
    }

    .add-sub-btn {
        width: 40px;
        height: 40px;
    }

    .circle-motion-on > svg {
        width: 34px;
    }

    .circle-motion-on {
        width: 100%;
        display: flex;
        justify-content: center;
        top: 30px;
        position: relative;
    }

    .temp-icons {
        width: 33px;
        /*height:40px*/
    }

    .blue-icon, .blue-icon > svg {
        fill: #0067ab !important;
    }

    .temp-bar {
        bottom: -34px;
        left: -79px;
        position: absolute;
        width: 165px;
        height: 100%;
        transition: all 1s ease-in;
    }

    #half-circle {
        position: absolute;
        top: 60px;
        left: 277px;
    }

    .arrow {
        fill: red;
    }

    .data-temp-top-bar {
        display: flex;
        padding:1rem;

    }

    .snow-fire-box {
        display: flex;
        height: 35px;
        background-color: #F6F6F6;
        padding: 5px;
        align-items: center;
        box-shadow: 0px 2px 3px #00000029;
        border-radius: 5px 0px 0px 5px;
    }

    .toggleState {
        width: 60px;
        background: #FFFFFF 0% 0% no-repeat padding-box;
        box-shadow: 0px 3px 6px #00000029;
        border-radius: 0px 5px 5px 0px;
        align-items: center;
        display: flex;
        justify-content: center;
        font-weight: bold;
    }

    .option-icons {
        width: 30px;
        height: 30px;
    }

    .option-icons2 {
        width: 30px;
        height: 30px;
    }

    .option-container {
        background: #F6F6F6 0% 0% no-repeat padding-box;
        box-shadow: 0px 2px 3px #00000029;
        border-radius: 5px;
        padding: 5px;
        height: fit-content;
    }

    .snow-fire-icons {
        width: 13px;
        height: 26px;
    }

    .circle-container {
        align-items: center;
        display: flex;
        justify-content: center;
        position: relative;
        transform: scale(.8);
        transition: 1s all ease-in;
    }

    .thermo-circle {
        width: 350px;
        height: 350px;
        background: #e8e7e7;
        border-radius: 50%;
        outline: 4px solid #ffffff;
        outline-offset: -8px;
        position: relative;
        z-index: 9;
        filter: drop-shadow(0px 0px 4px rgba(247, 15, 15, 0.69));
        box-shadow: rgba(0, 0, 0, 0.4) 0px 3px 8px, inset 0 0 10px #f1f4f9;
    }

    .thermo-blu-circle {
        width: 350px;
        height: 350px;
        background: #e8e7e7;
        border-radius: 50%;
        outline: 4px solid #ffffff;
        outline-offset: -8px;
        position: relative;
        z-index: 9;
        filter: drop-shadow(0px 0px 4px rgba(65, 168, 201, .69));
        box-shadow: rgba(0, 0, 0, 0.4) 0px 3px 8px, inset 0 0 10px #f1f4f9;
    }

    .thermo-inner-circle {
        width: 320px;
        height: 320px;
        position: absolute;
        z-index: 10;
        border-radius: 50%;
        background: white;
        box-shadow: inset 0 0 10px #43444691;
        border: 1px solid #80808042;
    }

    .thermo-motion {
        position: absolute;
        z-index: 11;
        top: 54px;
        display: flex;
        gap: 10px;
    }

    .moving-fan > svg {
        width: 28px;
        fill: #0067ab;
        animation: fanOn 1.5s linear infinite both;
    }

    @keyframes fanOn {
        0% {
            transform: rotate(0deg);
        }

        100% {
            transform: rotate(360deg);
        }
    }

    .thermo-stats {
        position: absolute;
        z-index: 11;
        font-size: 55px;
        font-weight: bold;
        top: 92px;
        font-family: calibri;
        color: #515356;
    }

        .thermo-stats > span {
            font-size: 20px;
        }

    .thermo-humid {
        position: absolute;
        z-index: 11;
        font-size: 20px;
        font-family: Calibri;
    }

    .general-btn {
        cursor: pointer;
    }


        .general-btn:active {
            transform: scale(0.9);
        }

    .therm-on {
        position: relative;
        z-index: 11;
        width: 20px;
        height: 20px;
        fill: blue;
    }

    .thermo-phase-container {
        position: absolute;
        z-index: 11;
        justify-content: center;
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .thermo-phase-btn {
        width: 70px;
        height: 70px;
        background: blue;
        border-radius: 50%;
        background: #FFFFFF 0% 0% no-repeat padding-box;
        box-shadow: inset 10px 1px 20px #0000002F, 0px 3px 6px #00000029;
        border: 1px solid #7070701F;
        top: 83px;
        position: relative;
        justify-content: center;
        display: flex;
        align-items: center;
        margin: 10px;
    }

    .thermo-phase-type {
        top: 82px;
        position: relative;
    }

    .thermo-plus {
        width: 29px;
    }

    .thermo-minus > svg {
        width: 35px;
    }

    .red-triangle-pointer {
        width: 0;
        height: 0;
        border: 118px solid transparent;
        border-top: 0;
        border-bottom: 238px solid #ce6b6b;
        position: absolute;
        top: -63px;
        transform: rotate(<%=triangleRotationValue%>);
        transform-origin: 50% 100%;
        filter: drop-shadow(0 0 0.1rem crimson);
    }

    .blue-triangle-pointer {
        width: 0;
        height: 0;
        border: 118px solid transparent;
        border-top: 0;
        border-bottom: 238px solid #41A8C9;
        position: absolute;
        top: -63px;
        transform: rotate(90deg);
        transform-origin: 50% 100%;
        filter: drop-shadow(0 0 0.1rem #41A8C9);
    }

    .occupied-tab {
        display: flex;
        width: 100%;
        justify-content: center;
        gap: 20px;
        margin-top: 2rem;
    }

    .tab-btns {
        border: none;
        font-size: 20px;
        background: none;
        cursor: pointer;
    }

        .tab-btns:focus {
            outline: none;
        }

    .thermo-box-container {
        display: flex !important;
        align-items: center;
    }

    .thermo-box-above {
        width: 120px;
        height: 120px;
        background: #F6F6F6 0% 0% no-repeat padding-box;
        border: 3px solid #3CAACB26;
        border-radius: 5px;
        display: flex;
        flex-direction: column;
        align-items: center;
        margin: 10px;
    }

    .thermo-box-below {
        width: 120px;
        height: 120px;
        background: #F6F6F6 0% 0% no-repeat padding-box;
        border: 3px solid #C32F2F22;
        border-radius: 5px;
        display: flex;
        flex-direction: column;
        align-items: center;
        margin: 10px;
    }

    .tab-icons {
        width: 30px;
        height: 30px;
    }

    .all-box-container {
        display: flex;
        flex-direction: column;
        align-items: center;
        margin-top: 3rem;
    }

    .thermo-temp-set {
        font-size: 3rem;
        margin-top: 10px;
    }

    .below-temp-set {
        font-size: 3rem;
        margin-top: 10px;
        width: 120px;
        height: 120px;
        border: none;
        background: transparent;
        text-align: center;
        padding-right: 0.5rem;
    }

        .below-temp-set:focus {
            outline: none;
        }

    .link {
        background: 0 0;
        border: none;
        text-decoration: none;
        color: #777;
        cursor: pointer;
        padding: 0
    }

    .hover-underline-animation {
        display: inline-block;
        position: relative;
        cursor: pointer;
    }

        .hover-underline-animation:focus {
            outline: none;
        }

    .activeAnimate {
        border-bottom: 3px solid #0067ab;
    }

    .hover-underline-animation::after {
        content: '';
        position: absolute;
        width: 100%;
        transform: scaleX(0);
        height: 3px;
        bottom: -5px;
        left: 0;
        background-color: #0067ab;
        transform-origin: bottom right;
        transition: transform .25s ease-out;
    }

    .hover-underline-animation:focus::after, .hover-underline-animation:hover::after {
        transform: scaleX(1);
        transform-origin: bottom left;
    }

    .hover-underline-animation:focus:active::after, .hover-underline-animation:hover:active::after {
        transform: scaleX(1);
        transform-origin: bottom left;
    }
    /* ///////// Title Hover /////// */
    [data-title]:hover::after {
        opacity: 1;
        transition: all 0.1s ease 0.5s;
        visibility: visible;
        position: absolute;
        z-index: 999999999;
    }

    [data-title]::after {
        content: attr(data-title);
        position: absolute;
        left: 100%;
        padding: 4px 4px 4px 8px;
        color: #222;
        white-space: nowrap;
        visibility: hidden;
        white-space: nowrap;
        -moz-border-radius: 5px;
        -webkit-border-radius: 5px;
        border-radius: 5px;
        -moz-box-shadow: 0px 0px 4px #222;
        -webkit-box-shadow: 0px 0px 4px #222;
        box-shadow: 0px 0px 4px #222;
        background-image: -moz-linear-gradient(top, #f8f8f8, #cccccc);
        background-image: -webkit-gradient(linear,left top,left bottom,color-stop(0, #f8f8f8),color-stop(1, #cccccc));
        background-image: -webkit-linear-gradient(top, #f8f8f8, #cccccc);
        background-image: -moz-linear-gradient(top, #f8f8f8, #cccccc);
        background-image: -ms-linear-gradient(top, #f8f8f8, #cccccc);
        background-image: -o-linear-gradient(top, #f8f8f8, #cccccc);
        opacity: 0;
        z-index: 999999999999;
    }

    .option-icons {
        fill: #515356;
    }

    .option-icons2 {
        fill: #515356;
    }

    .option-container {
        cursor: pointer;
    }

    .turnOff {
        fill: #515356;
    }

    .turnOn {
        fill: #0067ab;
    }

    .dropdown-menu {
        width: 100px !important;
    }

    .toggle-bar {
        display: flex;
        margin-left: auto;
        gap: 20px;
    }

    .dropdown-toggle::after {
        display: none !important;
    }

    .menu {
        display: flex;
        justify-content: center;
        padding: 0 !important;
        margin: 0 !important;
    }

    .dropdown {
        display: flex;
        justify-content: center;
        align-items: center;
        color: #fff;
        position: relative;
        font-size: 18px;
        perspective: 1000px;
        z-index: 2;
    }

        .dropdown:hover {
            /*background: #0067ab;*/
            cursor: pointer;
        }

            .dropdown:hover > svg path {
                fill: white;
            }

            .dropdown:hover .dropdown_menu li {
                display: block;
            }

    .dropdown_menu {
        position: absolute;
        top: 100%;
        right: 10%;
        perspective: 1000px;
        z-index: -1;
    }

    .curveit {
        border-radius: 0 0 5px 5px;
    }

    .dropdown_item-1 {
        border-radius: 0 5px 0 0;
    }

    .dropdown_menu li {
        display: none;
        color: #515356;
        background-color: #F6F6F6;
        padding: 10px 20px;
        font-size: 16px;
        box-shadow: rgba(0, 0, 0, 0.24) 0px 3px 8px;
        opacity: 0;
    }

        .dropdown_menu li:hover {
            background-color: #e7ebf3;
            color: #515356;
        }

    .dropdown:hover .dropdown_menu--animated {
        display: block;
    }

    .dropdown_menu--animated {
        display: none;
    }

        .dropdown_menu--animated li {
            display: block;
            opacity: 1;
        }

    .dropdown_menu-1 .dropdown_item-1 {
        transform-origin: top center;
        animation: slideDown 300ms 60ms ease-in-out forwards;
    }

    .dropdown_menu-1 .dropdown_item-2 {
        transform-origin: top center;
        animation: slideDown 300ms 60ms ease-in-out forwards;
    }

    .dropdown_menu-1 .dropdown_item-motion {
        transform-origin: top center;
        animation: slideDown 300ms 60ms ease-in-out forwards;
    }

    .dropdown_menu-1 .dropdown_item-fan {
        transform-origin: top center;
        animation: slideDown 300ms 60ms ease-in-out forwards;
    }

    @-webkit-keyframes slideDown {
        0% {
            opacity: 0;
            transform: translateY(-60px);
        }

        100% {
            opacity: 1;
            transform: translateY(0);
        }
    }

    #drag {
        cursor: pointer;
    }

    .oval {
        fill: #0067ab;
        stroke-width: -1px;
        -webkit-text-stroke: thick;
        stroke: gray;
    }

    .arrow {
        fill: #0067ab;
        transform: translate(-2px, 3px);
        transform-origin: center;
    }

    .path {
        fill: none;
        stroke: #979797;
    }

    .option-icons:hover:active > svg path {
        fill: white;
    }

    .heatColdSelect {
        font-weight: bold;
        background: white;
        padding: 3px 6px;
        border-radius: 3px;
        margin-left: 6px;
        width: 100%;
    }

    .saveBtn {
        display: none;
    }

        .saveBtn:focus {
            outline: none;
        }

    .bottom-container {
        width: 100%;
        display: flex;
        justify-content: center;
        margin-top: 1rem;
        margin-bottom: 1rem;
    }
    /* Dropdown fan/ motion */
    .auto-box {
        display: flex;
        width: 100%;
    }

    .autoselect {
        padding: 3px 6px;
    }

    .wrapper .title {
        font-weight: 700;
        font-size: 24px;
        color: #fff;
    }

    .select_wrap {
        width: 141px;
        position: relative;
        user-select: none;
    }

        .select_wrap .default_option {
            position: relative;
            cursor: pointer;
            list-style: none;
            padding: 0;
            display: flex;
            background-color: #F6F6F6;
            padding: 5px;
            align-items: center;
            box-shadow: 0px 2px 3px #00000029;
            width: fit-content;
            font-weight: bold;
            border-radius: 5px;
        }

            .select_wrap .default_option li {
                padding: 5px;
                list-style: none;
            }

    p {
        margin: 0;
        margin-bottom: 0 !important;
    }

    .select_wrap .select_ul {
        position: absolute;
        top: 52px;
        left: 0;
        /*width: 100%;*/
        background: #fff;
        border-radius: 5px;
        display: none;
        box-shadow: rgba(0, 0, 0, 0.24) 0px 3px 8px;
        padding: 0;
    }

    .select_ul {
        background: #F6F6F6;
    }

    .select_wrap .select_ul li {
        padding: 5px;
        cursor: pointer;
    }

        .select_wrap .select_ul li:first-child:hover {
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
        }

        .select_wrap .select_ul li:last-child:hover {
            border-bottom-left-radius: 5px;
            border-bottom-right-radius: 5px;
        }

        .select_wrap .select_ul li:hover {
            background: #e7ebf3;
        }

    .select_wrap .option {
        display: flex;
        align-items: center;
        justify-content: space-evenly;
        gap: 10px;
    }

        .select_wrap .option .icon {
            background: url('https://i.imgur.com/oEZu0sK.png') no-repeat 0 0;
            width: 32px;
            height: 32px;
            margin-right: 15px;
        }

            .select_wrap .option .icon > svg {
                fill: red;
            }

        .select_wrap .option.tog-1 .icon {
            background-position: 0 0;
        }

        .select_wrap .option.tog-2 .icon {
            background-position: 0 -35px;
        }

        .select_wrap .option.ice .icon {
            background-position: 0 -72px;
        }

    .select_wrap.active .select_ul {
        display: block;
        list-style: none;
        background: #F6F6F6;
        z-index: 99;
    }

    .select_wrap.active .default_option:before {
        top: 25px;
        transform: rotate(-225deg);
    }


    .wrapit {
        bottom: -34px;
        left: -79px;
        position: relative;
        /*width: 165px;
        height: 100%;
        transition: all 1s ease-in;*/
    }

    #curve {
        visibility: hidden;
    }

    #slider {
        position: relative;
        transform: scale(1.3);
        bottom: 8px;
        z-index: 5;
        /* bottom: -71px; */
        /* right: 14px; */
    }

    .rangeInput {
        -webkit-appearance: slider-vertical;
        appearance: slider-vertical;
        width: 100%;
        height: 375px;
        cursor: pointer;
        margin: 0;
        margin-top: 20px;
        padding: 0;
        position: absolute;
        z-index: 5;
        opacity: 0;
    }

        .rangeInput::-webkit-slider-thumb {
            -webkit-appearance: none;
            appearance: none;
            width: 100%;
            height: 375px;
            cursor: pointer;
        }

        .rangeInput::-moz-range-thumb {
            width: 100%;
            height: 375px;
            cursor: pointer;
        }

    .thumbElement {
        display: none;
    }

    .aboveThumb {
        fill: #515356;
        stroke: #41A8C9;
        stroke-width: 3px;
    }

        .aboveThumb.inactiveThumb {
            fill: #51535696;
            stroke: #41a8c996;
        }

    .belowThumb {
        fill: #515356;
        stroke: #ce6b6b;
        stroke-width: 3px;
    }
        /*.belowThumb::after {
            content: '3545';
            color: red
        }   */
        .belowThumb.inactiveThumb {
            fill: #51535699;
            stroke: #ce6b6b96;
        }

    .aboveThumbNum, .belowThumbNum {
        fill: white;
        font-size: 12px;
    }

        .aboveThumbNum.inactiveThumb,
        .belowThumbNum.inactiveThumb {
            fill: darkgray;
        }


    @media only screen and (min-width: 1615px) {
        .overlay {
            width: 98%;
        }
    }

    @media only screen and (max-width:1110px) {
        .temp-reading_container {
            flex-wrap: wrap;
        }

        .data-temp_container {
            width: 100%;
        }

        #successToast, #errorToast {
            padding: 0.75rem;
            left: 47%;
        }
    }

    @media only screen and (max-width:1100px) {

        .circle-temp_container {
            width: 100%;
        }
    }

    @media only screen and (max-width:1000px) {


        .temp-reading_container {
            flex-wrap: wrap;
        }
    }

    @media only screen and (max-width:963px) {
        .data-temp_container {
            width: 100%;
        }
    }

    @media only screen and (max-width: 850px) {
        #successToast, #errorToast {
            left: 31%;
        }
    }

    @media only screen and (max-width:475px) {
        .circle-container {
            transform: scale(.5);
        }

        .temp-bar {
            transform: scale(.7);
            left: -207px;
        }

        .wrapit {
            transform: scale(.7);
            left: -200px;
            position: relative;
            top: 40px;
        }

        .circle-container {
            left: -64px;
            position: relative;
        }

        .overlay {
            width: 95%;
        }

        .alert-icon-oj {
            padding-top: 0.5rem;
        }

        #successToast, #errorToast {
            left: 11%;
        }

        .two-columns-temp {
            display: flex;
            justify-content: normal; 
            margin-right: 0;
        }
    }
</style>
