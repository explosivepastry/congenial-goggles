<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%----     Tab 1  Gateway Online  --%>
<div class="tab tab-gateway tab-first">
    <div class="flex">
        <div class="step-box">
            <span><%: Html.TranslateTag("Step 1.")%></span>
            <span><%: Html.TranslateTag("Gateway Online")%></span>
        </div>
        <span class="gwOnlineCheck spinner-border spinner-border-sm ms-2 mt-2"></span> 
        <div class="gwOnlineComplete step ms-2 mt-2" style="display:none;" ><%=Html.GetThemedSVG("circle-check") %></div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("2-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Sensor in Gateway List")%></p>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("3-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Power On Sensor")%></p>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("4-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Validate Sensor Status")%></p>
        </div>
    </div>

    <div>
        <p></p>
        <p><%: Html.TranslateTag("Checking that there is one or more gateways online that the sensor can communicate with.")%></p>
        <p><%: Html.TranslateTag("Verify that your gateway is powered on.")%></p>
        <p class="gwOnlineCheck"><%: Html.TranslateTag("This check will complete automatically when gateway communication is verified.  Or press Skip to bypass this check.")%></p>
        <p class="gwOnlineComplete" style="display:none;"><%: Html.TranslateTag("Press Next to continue.")%></p>
    </div>

    <div class="row-back-next">

        <div>
        </div>

        <div>
            <button onclick="showTabNext('list');" class="btn btn-secondary gwOnlineCheck"><%: Html.TranslateTag("Skip")%></button>
            <button onclick="showTabNext('list');" class="btn btn-primary gwOnlineComplete" style="display:none;"><%: Html.TranslateTag("Next")%></button>
        </div>
    </div>
</div>


<%-----    Tab 2   Sensor in Gateway List  ------%>
<div class="tab tab-list" style="display: none;">

    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("1-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Gateway Online")%></p>
            <div class="step"><%=Html.GetThemedSVG("circle-check") %></div>
        </div>
    </div>
    <div class="flex">
        <div class="step-box">
            <span><%: Html.TranslateTag("Step 2.")%></span>
            <span><%: Html.TranslateTag("Sensor in Gateway List")%></span>
        </div>
        <span class="gwListCheck spinner-border spinner-border-sm ms-2 mt-2"></span> 
        <div class="gwListComplete step ms-2 mt-2" style="display:none;" ><%=Html.GetThemedSVG("circle-check") %></div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("3-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Power On Sensor")%></p>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("4-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Validate Sensor Status")%></p>
        </div>
    </div>


    <div>
        <p></p>
        <p><%: Html.TranslateTag("Checking that the gateway has communicated with the server to download this sensor into the gateway sensor list.")%></p>
        <p><%: Html.TranslateTag("To expedite this process, press (do not hold) the utility button one time.")%></p>
        <p class="gwListCheck"><%: Html.TranslateTag("This check will complete when gateway has received the sensor id.  Or press Skip to bypass this check.")%></p>
        <p class="gwListComplete" style="display:none;"><%: Html.TranslateTag("Press Next to continue.")%></p>
    </div>
    <div class="row-back-next">
        <div>
            <button class="btn btn-secondary" onclick="showTabNext('gateway')"><%: Html.TranslateTag("Back")%></button>
        </div>

        <div>
            <button onclick="showTabNext('power');" class="btn btn-secondary gwListCheck"><%: Html.TranslateTag("Skip")%></button>
            <button onclick="showTabNext('power');" class="btn btn-primary gwListComplete" style="display:none;"><%: Html.TranslateTag("Next")%></button>
        </div>
    </div>
</div>

<%-----    Tab 3  Power On Sensor ------%>
<div class="tab tab-power" style="display: none;">

    
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("1-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Gateway Online")%></p>
            <div class="step"><%=Html.GetThemedSVG("circle-check") %></div>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("2-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Sensor in Gateway List")%></p>
            <div class="step"><%=Html.GetThemedSVG("circle-check") %></div>
        </div>
    </div>
    <div class="flex">
        <div class="step-box">
            <span><%: Html.TranslateTag("Step 3.")%></span>
            <span><%: Html.TranslateTag("Power On Sensor")%></span>
        </div>
        <div class="battCheckComplete step ms-2 mt-2" style="display: none;"><%:Html.GetThemedSVG("circle-check") %></div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("4-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Validate Sensor Status")%></p>
        </div>
    </div>

    <%--<div class="flex" style="justify-content:center;">
        <img src="/Content/dashboard/images/Setup/BatteryAA.png" />
    </div>--%>

    <div>
        <p></p>
        <p class="battCheckComplete"><%: Html.TranslateTag("Power on your sensor.")%></p>
        <p><%: Html.TranslateTag("Then press Next to continue.")%></p>
    </div>
    <div class="row-back-next">
        <div>
            <button onclick="showTabNext('list')" class="btn btn-secondary"><%: Html.TranslateTag("Back")%></button>
        </div>

        <div>
            <button onclick="showTabNext('validate');" class="btn btn-primary battCheckComplete"><%: Html.TranslateTag("Next")%></button>
        </div>
    </div>
</div>

<%-----    Tab 4  Validate Sensor Status  ------%>
<div class="tab tab-validate" style="display: none;">

    
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("1-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Gateway Online")%></p>
            <div class="step"><%=Html.GetThemedSVG("circle-check") %></div>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("2-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Sensor in Gateway List")%></p>
            <div class="step"><%=Html.GetThemedSVG("circle-check") %></div>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("3-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Power On Sensor")%></p>
            <div class="step"><%=Html.GetThemedSVG("circle-check") %></div>
        </div>
    </div>
    <div class="flex">
        <div class="step-box">
            <span><%: Html.TranslateTag("Step 4.")%></span>
            <span><%: Html.TranslateTag("Validate Sensor Status")%></span>
        </div>
        <span class="senComCheck spinner-border spinner-border-sm ms-2 mt-2"></span> 
        <div class="senComComplete step ms-2 mt-2" style="display:none;" ><%=Html.GetThemedSVG("circle-check") %></div>
    </div>

    <div>
        <p></p>
        <p><%: Html.TranslateTag("Validate the sensor is able to communicate to the server.")%></p>
        <p class="senComCheck"><%: Html.TranslateTag("This step will complete automatically when sensor communication is verified.  Or press Skip to bypass this check.")%></p>
        <p class="senComComplete" style="display:none;"><%: Html.TranslateTag("Press Complete to continue.")%></p>
    </div>
    <div class="row-back-next">
        <div>
            <button onclick="showTabNext('power')" class="btn btn-secondary"><%: Html.TranslateTag("Back")%></button>
        </div>

        <div>
            <button onclick="confirmModal()" class="btn btn-secondary senComCheck"><%: Html.TranslateTag("Skip")%></button>
            <a href="/Setup/SensorComplete/<%:Model.SensorID%>" class="btn btn-primary senComComplete" style="display:none;"><%: Html.TranslateTag("Complete")%></a>
        </div>
    </div>
</div>
