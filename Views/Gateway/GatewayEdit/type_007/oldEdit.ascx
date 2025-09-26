<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>


<style>
    * {
        box-sizing: border-box;
    }

    body {
        font-family: Verdana, sans-serif;
        margin: 0;
    }

    .mySlides {
        display: none;
        padding-Left: 3em;
    }


    /* Slideshow container */
    .slideshow-container {
        position: relative;
        margin: auto;
    }

    /* Next & previous buttons */
    .prev, .next {
        cursor: pointer;
        position: absolute;
        top: 50%;
        width: auto;
        padding: 16px;
        margin-top: -22px;
        color: black;
        font-weight: bold;
        font-size: 18px;
        transition: 0.6s ease;
        border-radius: 0 3px 3px 0;
    }

    /* Position the "next button" to the right */
    .next {
        right: 0;
        border-radius: 3px 0 0 3px;
    }

        /* On hover, add a black background color with a little bit see-through */
        .prev:hover, .next:hover {
            background-color: rgba(0,0,0,0.8);
        }



    /* The dots/bullets/indicators */
    .dot {
        cursor: pointer;
        height: 15px;
        width: 15px;
        margin: 0 2px;
        background-color: #bbb;
        border-radius: 50%;
        display: inline-block;
        transition: background-color 0.6s ease;
    }

        .active, .dot:hover {
            background-color: #717171;
        }

    /* Fading animation */
    .fade {
        -webkit-animation-name: fade;
        -webkit-animation-duration: 1.5s;
        animation-name: fade;
        animation-duration: 1.5s;
    }

    @-webkit-keyframes fade {
        from {
            opacity: .4;
        }

        to {
            opacity: 1;
        }
    }

    @keyframes fade {
        from {
            opacity: .4;
        }

        to {
            opacity: 1;
        }
    }

    /* On smaller screens, decrease text size */
    @media only screen and (max-width: 300px) {
        .prev, .next, .text {
            font-size: 11px;
        }
    }
</style>




<%--            <ul id="horizontal-list">

                <li style="float: left; padding-right: 1.6em;"><a id="gGateway" href="#generalGatewayInfo">General</a></li>
                <li style="float: left; padding-right: 1.6em;"><a id="EnterpriseHost" href="#DHCPAndServer">Local Area Network</a></li>
                <li style="float: left; padding-right: 1.6em;"><a id="rRU" href="#gatewayCommands">Commands</a></li>
                <li style="float: left; padding-right: 1.6em;"><a id="activateInterfaces" href="#interface">Interface Activation</a></li>

                <li style="float: left; padding-right: 1.6em;" id="snmpclick" <%if (Model.SNMPInterface1Active != true && Model.SNMPInterface2Active != true && Model.SNMPInterface3Active != true && Model.SNMPInterface4Active != true)
                                                                                {%>style="display:none"
                    <%}%>><a id="sNMPI" href="#snmp">SNMP Interface</a></li>

                <li style="float: left; padding-right: 1.6em;" id="modbusclick" <%if (Model.ModbusInterfaceActive != true)
                                                                                  { %>style="display:none"
                    <%} %>><a id="mBI" href="#modBus">Modbus Interface</a></li>

                <li style="float: left; padding-right: 1.6em;" id="realtimeclick" <%if (Model.RealTimeInterfaceActive != true)
                                                                                    { %>style="display:none"
                    <%} %>><a id="rTI" href="#realtimeTCP">Real Time Interface</a></li>
            </ul>--%>

<div class="formBody">
    <div class="slideshow-container">



        <!-- Full-width images with number and caption text -->
        <div class="mySlides activeSlides">
            <div class="numbertext">General</div>
            <br />

            <div id="generalGatewayInfo" style="width: 80%;">
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_GatewayName.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_HeartBeat.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_PollInterval.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_ObserveAware.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_Servers.ascx", Model); %>
            </div>
        </div>

        <div class="mySlides activeSlides">
            <div class="numbertext">Local Area Network</div>
            <br />
            <div id="DHCPAndServer" style="width: 80%;">
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_MacAddress.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_GatewayIP.ascx", Model); %>
            </div>

        </div>

        <div class="mySlides activeSlides">
            <div class="numbertext">Commands</div>
            <br />
            <div id="gatewayCommands" style="width: 100%;">
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_RemoteNetworkReset.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_UpdateFirmware.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_ResetDefault.ascx", Model); %>
            </div>

        </div>

        <div class="mySlides activeSlides">
            <div class="numbertext">Interface Activation</div>
            <br />
            <div id="interface" style="width: 100%;">
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_Interface.ascx", Model); %>
            </div>

        </div>

       

        <div id="snmpSlide" class="mySlides <%= (Model.SNMPInterface1Active == true || Model.SNMPInterface2Active == true || Model.SNMPInterface3Active == true || Model.SNMPInterface4Active == true) ? "activeSlides" : "" %>">
            <div class="numbertext">SNMP Interface</div>
            <br />
            <div id="snmp" style="display: none">
                <% Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_GatewayInterfaceSNMP.ascx", Model); %>
            </div>


        </div>

        <div id="modBusSlide" class="mySlides <%= Model.ModbusInterfaceActive ? "activeSlides" : "" %>">
            <div class="numbertext">Modbus Interface</div>
            <br />
            <div id="modBus" style="display: none">
                <% Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_GatewayInterfaceMB.ascx", Model); %>
            </div>

        </div>

        <div id="realtimeSlide" class="mySlides <%= Model.RealTimeInterfaceActive ? "activeSlides" : "" %>">
            <div class="numbertext">Real Time Interface</div>
            <br />
            <div id="realtimeTCP" style="display: none">
                <% Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_GatewayInterfaceRT.ascx", Model); %>
            </div>

        </div>

        <!-- Next and previous buttons -->
        <a class="prev" onclick="plusSlides(-1)">&#10094;</a>
        <a class="next" onclick="plusSlides(1)">&#10095;</a>
    </div>
    <br>

    <!-- The dots/circles -->
    <div style="text-align: center">
        <span class="dot" onclick="currentSlide(1)"></span>
        <span class="dot" onclick="currentSlide(2)"></span>
        <span class="dot" onclick="currentSlide(3)"></span>
        <span class="dot" onclick="currentSlide(4)"></span>
        <span class="extraDot " onclick="currentSlide(5)"></span>
        <span class="extraDot " onclick="currentSlide(6)"></span>
        <span class="extraDot " onclick="currentSlide(7)"></span>

    </div>





</div>


<script type="text/javascript">
    $(document).ready(function () {


        setSNMPVisibility();
        $('#SNMPInterface1Active').change(setSNMPVisibility);

        setModbusVisibility();
        $('#ModbusInterfaceActive').change(setModbusVisibility);

        setRealTimeVisibility();
        $('#RealTimeInterfaceActive').change(setRealTimeVisibility);
    });


    var slideIndex = 1;
    showSlides(slideIndex);

    // Next/previous controls
    function plusSlides(n) {
        showSlides(slideIndex += n);
    }

    // Thumbnail image controls
    function currentSlide(n) {
        showSlides(slideIndex = n);
    }

    function showSlides(n) {
        var i;
        var slides = $(".activeSlides");

        var dots = $(".dot");
        if (n > slides.length) { slideIndex = 1 }
        if (n < 1) { slideIndex = slides.length }
        for (i = 0; i < slides.length; i++) {
            slides[i].style.display = "none";
        }

        for (i = 0; i < dots.length; i++) {
            dots[i].className = dots[i].className.replace(" active", "");
        }

        slides[slideIndex - 1].style.display = "block";
        dots[slideIndex - 1].className += " active";
    }




    //function setSNMPVisibility() {
    //    if ($('#SNMPInterface1Active').is(":checked")) {

    //        $('#snmpSlide').className += " activeSlide";
    //    }
    //    else {

    //        $('#snmpSlide').className.replace(" activeSlide", "");
    //    }
    //}

    function setModbusVisibility() {
        if ($('#ModbusInterfaceActive').is(":checked"))
            $('#modbusclick').show();
        else
            $('#modbusclick').hide();
    }

    function setRealTimeVisibility() {
        if ($('#RealTimeInterfaceActive').is(":checked"))
            $('#realtimeclick').show();
        else
            $('#realtimeclick').hide();
    }


</script>
