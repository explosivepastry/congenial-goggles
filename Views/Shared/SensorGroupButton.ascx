<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>

<%
    List<Sensor> sensors = (List<Sensor>)ViewData["selectedSensors"];
    List<Sensor> peopleCounters = Sensor.LoadByCSNetIDAndApplicationID(Model.Sensors[0].CSNetID, 77);

%>


<div id="sensorGroupWrapper" style="position: relative">
    <button id="sensorGroupButtonAB" class="sensorGroupButton" onclick="openDropdownMenu('sensorGroupDropdownMenu')">
        <%=Html.GetThemedSVG("activeSensors") %>
    </button>
    <p class="sensorGroupText hide">Group Sensors</p>

    <form id="sensorGroupDropdownMenu" class="column hide">
        <h4 style="font-size: 1rem; color: #0469ad; font-weight: 600; text-align: center;">Sensors</h4>
        <%
            foreach (Sensor s in peopleCounters)
            { %>
        <label class="pointer">
            <input class="pointer" type="checkbox" name="sensorGroupCheckboxes" value="<%: s.SensorID %>"
                <%: Model.Sensors.Where(x => x.SensorID == s.SensorID ).Count() > 0 ? "checked" : "" %> />
            <%: s.Name %>
        </label>
        <% } %>
        <button id="submitMe" class="btn btn-primary" style="margin-top: 0.5rem; padding: 0;">Submit</button>
    </form>

</div>
<script>

    //---------Submit the sensor(s) to display the data on the chart---------

    $('#submitMe').click(function () {
        const checkedInputs = $('input[type="checkbox"]:checked');
        if (checkedInputs.length > 0) {
            refreshChartdata();
        }
    });

    //---------Sensor Group Button hide and show text on hover---------

    var sensorButtonAB = document.querySelector(".sensorGroupButton");

    var textToToggleVisibility = document.querySelector(".sensorGroupText");

    function hideText() {
        textToToggleVisibility.classList.add("hide");
    };

    function showText() {
        textToToggleVisibility.classList.remove("hide");
    };

    if (sensorButtonAB) {
        sensorButtonAB.addEventListener("mouseover", showText);
        sensorButtonAB.addEventListener("mouseout", hideText);
    }

    //---------Drop down add (for new: add open button to the openButtons and call openDropdownMenu() on that button---------

    function openDropdownMenu(menuToDisplay) {
        const DropItLikeItsHot = document.querySelector(`#${menuToDisplay}`);

        if (DropItLikeItsHot.classList.contains("scale-in-ver-center")) {
            closeDropdownMenu();
        } else {
            DropItLikeItsHot.classList.remove("scale-out-vertical", "hide");
            DropItLikeItsHot.classList.add("scale-in-ver-center", "flexAB");
        }
    }

    function closeDropdownMenu() {
        const menuToClose = document.querySelector(".scale-in-ver-center");
        if (menuToClose) {
            menuToClose.classList.remove("scale-in-ver-center");
            menuToClose.classList.add("scale-out-vertical");
            setTimeout(function () {
                menuToClose.classList.add("hide");
            }, 350);
        }
    }

    document.addEventListener('click', (event) => {
        const openButton = document.querySelector("#sensorGroupButtonAB");
        const entireDropdownDiv = document.querySelector("#sensorGroupDropdownMenu")
        const innerSvgs = openButton.querySelector('svg')
        if (innerSvgs.contains(event.target) || entireDropdownDiv.contains(event.target)) {
            return;
        }
        if (openButton.contains(event.target)) {
            return;
        } else {
            closeDropdownMenu();
        }
    });

</script>

<style>
    input[type="checkbox"] {
        width: 12px;
        height: 12px;
    }

    .scale-in-ver-center {
        -webkit-animation: scale-in-ver-center 0.2s cubic-bezier(0.250, 0.460, 0.450, 0.940) both;
        animation: scale-in-ver-center 0.2s cubic-bezier(0.250, 0.460, 0.450, 0.940) both;
    }

    @-webkit-keyframes scale-in-ver-center {
        0% {
            -webkit-transform: scaleY(0);
            transform: scaleY(0);
            opacity: 1;
        }

        100% {
            -webkit-transform: scaleY(1);
            transform: scaleY(1);
            opacity: 1;
        }
    }

    @keyframes scale-in-ver-center {
        0% {
            -webkit-transform: scaleY(0);
            transform: scaleY(0);
            opacity: 1;
        }

        100% {
            -webkit-transform: scaleY(1);
            transform: scaleY(1);
            opacity: 1;
        }
    }


    .scale-out-vertical {
        -webkit-animation: scale-out-vertical 0.2s cubic-bezier(0.550, 0.085, 0.680, 0.530) both;
        animation: scale-out-vertical 0.2s cubic-bezier(0.550, 0.085, 0.680, 0.530) both;
    }

    @-webkit-keyframes scale-out-vertical {
        0% {
            -webkit-transform: scaleY(1);
            transform: scaleY(1);
            opacity: 1;
        }

        100% {
            -webkit-transform: scaleY(0);
            transform: scaleY(0);
            opacity: 1;
        }
    }

    @keyframes scale-out-vertical {
        0% {
            -webkit-transform: scaleY(1);
            transform: scaleY(1);
            opacity: 1;
        }

        100% {
            -webkit-transform: scaleY(0);
            transform: scaleY(0);
            opacity: 1;
        }
    }

    .hide {
        display: none;
    }

    .flexAB {
        display: flex
    }

    .sensorGroupText {
        position: absolute;
        right: -6px;
        top: 25px;
        color: #f89725;
    }

    .sensorGroupButton {
        background: none;
        border: none;
        position: absolute;
        right: 44px;
        top: 4px;
        z-index: 2;
    }

        .sensorGroupButton svg {
            fill: #0469ad;
            transition: fill 0.3s ease;
            width: 24px;
            height: 24px !important;
        }

            .sensorGroupButton svg:hover {
                fill: #f89725;
                cursor: pointer;
            }

    #sensorGroupDropdownMenu {
        flex-direction: column;
        background: #F5F5F5;
        border-radius: 1rem;
        padding: 0.5rem;
        max-width: fit-content;
        position: absolute;
        right: 10px;
        top: 40px;
        z-index: 3;
        box-shadow: 0 0.125rem 0.25rem rgb(0 0 0 /18%);
        border: solid 2px #0469ad;
    }

    .pointer:hover {
        cursor: pointer;
        color: #0469ad;
    }

    @media screen and (max-width: 500px) {
        .sensorGroupText {
            display: none;
        }

        .sensorGroupButton {
            top: -58px;
        }
    }
</style>
