<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%----     Tab 1  Configure WiFi Network/Download the Next App  --%>
<div class="tab tab-app tab-first">
    <div class="flex">
        <div class="step-box">
            <span><%: Html.TranslateTag("Step 1. ")%></span>
            <span><%: Html.TranslateTag("Configure WiFi Network")%></span>
        </div>
    </div>
    <div>
        <p>
            <%: Html.TranslateTag("The Next Sensor app will guide you step-by-step to\r\nconfigure your sensor and connect it to your Wi-Fi network.")%>
        </p>
        <div style="justify-content: space-between" class="d-flex">
            <button id="optionsButton" style="all: unset; color: <%:MonnitSession.CurrentStyle("OptionsIconColor")%>; cursor: pointer">
                <%: Html.TranslateTag("Advanced Settings")%>
            </button>
            <a href="https://app.next-sensors.com/get_ready"
                target="_blank"
                aria-label="Open the Monnit Next App"
                class="btn btn-primary">
                <%: Html.TranslateTag("Next Sensor App")%>
            </a>
        </div>
        <div id="advancedOptions" style="display: none;">
            <i style="margin-top: 16px; display: inline-block">
                <%: Html.TranslateTag("Note: If you choose to skip this step, the sensor will only be registered to your iMonnit account. You’ll still need to complete the Wi-Fi network setup later through the Next Sensor App before the sensor can be used.")%>
            </i>
            <div class="row-back-next">
                <div></div>
                <button onclick="showTabNext('credentials');" class="btn btn-primary"><%: Html.TranslateTag("Skip")%></button>
            </div>
        </div>
    </div>
</div>

<%-----    Tab 2   Validate Sensor   ------%>
<div class="tab tab-credentials" style="display: none;">

    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("1-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Configure WiFi Network")%></p>
            <div class="step"><%=Html.GetThemedSVG("circle-check") %></div>
        </div>
    </div>
    <div class="flex">
        <div class="step-box">
            <span><%: Html.TranslateTag("Step 2.")%></span>
            <span><%: Html.TranslateTag("Validate Sensor Status")%></span>
        </div>
        <span class="senComCheck spinner-border spinner-border-sm ms-2 mt-2"></span>
        <div class="senComComplete step ms-2 mt-2" style="display: none;"><%=Html.GetThemedSVG("circle-check") %></div>
    </div>

    <div>
        <p></p>
        <p><%: Html.TranslateTag("Validate the sensor is able to communicate to the server.")%></p>
        <p class="senComCheck"><%: Html.TranslateTag("This step will complete automatically when sensor communication is verified.  Or press Skip to bypass this check.")%></p>
        <p class="senComComplete" style="display: none;"><%: Html.TranslateTag("Press Complete to continue.")%></p>
    </div>
    <div class="row-back-next">
        <div>
            <button onclick="showTabNext('app')" class="btn btn-secondary"><%: Html.TranslateTag("Back")%></button>
        </div>

        <div>
            <button onclick="confirmModal()" class="btn btn-secondary senComCheck"><%: Html.TranslateTag("Skip")%></button>
            <a href="/Setup/SensorComplete/<%:Model.SensorID%>" class="btn btn-primary senComComplete" style="display: none;"><%: Html.TranslateTag("Complete")%></a>
        </div>
    </div>
</div>

<script>
    const optionsButton = document.querySelector("#optionsButton");
    const advancedOptions = document.querySelector("#advancedOptions");

    optionsButton.addEventListener("click", function () {

        if (advancedOptions.style.display != "block") {
            advancedOptions.style.display = "block";
        } else {
            advancedOptions.style.display = "none";
        }
    });
</script>

