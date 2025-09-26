<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%----     Tab 1  Download and Install  --%>
<div class="tab tab-app tab-first">
    <div class="flex">
        <div class="step-box">
            <span><%: Html.TranslateTag("Step 1.")%></span>
            <span><%: Html.TranslateTag("Download and Install")%></span>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("2-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Enter Wi-Fi Credentials")%></p>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("3-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Insert Batteries")%></p>
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
        <p><%: Html.TranslateTag("Download and install the Wi-Fi configuration tool on a Windows PC.")%></p>
        <p><%: Html.TranslateTag("Press Next to continue.")%></p>
    </div>

    <div class="row-back-next">

        <div>
        </div>

        <div>
            <button onclick="showTabNext('credentials');" class="btn btn-primary" ><%: Html.TranslateTag("Next")%></button>
        </div>
    </div>
</div>


<%-----    Tab 2   Enter Wi-Fi Credentials  ------%>
<div class="tab tab-credentials" style="display: none;">

    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("1-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Download and Install")%></p>
            <div class="step"><%=Html.GetThemedSVG("circle-check") %></div>
        </div>
    </div>
    <div class="flex">
        <div class="step-box">
            <span><%: Html.TranslateTag("Step 2.")%></span>
            <span><%: Html.TranslateTag("Enter Wi-Fi Credentials")%></span>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("3-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Insert Batteries")%></p>
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
        <p><%: Html.TranslateTag("Configure the WiFi credentials using the WiFi configuration software.")%></p>
        <p class="gwListComplete"><%: Html.TranslateTag("Press Next to continue.")%></p>
    </div>
    <div class="row-back-next">
        <div>
            <button class="btn btn-secondary" onclick="showTabNext('app')"><%: Html.TranslateTag("Back")%></button>
        </div>

        <div>
            <button onclick="showTabNext('power');" class="btn btn-primary"><%: Html.TranslateTag("Next")%></button>
        </div>
    </div>
</div>

<%-----    Tab 3  Insert Batteries ------%>
<div class="tab tab-power" style="display: none;">

    
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("1-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Download and Install")%></p>
            <div class="step"><%=Html.GetThemedSVG("circle-check") %></div>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("2-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Enter Wi-Fi Credentials")%></p>
            <div class="step"><%=Html.GetThemedSVG("circle-check") %></div>
        </div>
    </div>
    <div class="flex">
        <div class="step-box">
            <span><%: Html.TranslateTag("Step 3.")%></span>
            <span><%: Html.TranslateTag("Insert Batteries")%></span>
        </div>
        <div class="battCheckComplete step ms-2 mt-2" style="display: none;"><%:Html.GetThemedSVG("circle-check") %></div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("4-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Validate Sensor Status")%></p>
        </div>
    </div>

    <div class="flex" style="justify-content:center;">
        <img src="/Content/dashboard/images/Setup/BatteryAA.png" />
    </div>

    <div>
        <p></p>
        <p class="battCheckComplete"><%: Html.TranslateTag("Insert batteries into your sensor.")%></p>
        <p><%: Html.TranslateTag("Then press Next to continue.")%></p>
    </div>
    <div class="row-back-next">
        <div>
            <button onclick="showTabNext('credentials')" class="btn btn-secondary"><%: Html.TranslateTag("Back")%></button>
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
            <p><%: Html.TranslateTag("Download and Install")%></p>
            <div class="step"><%=Html.GetThemedSVG("circle-check") %></div>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("2-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Enter Wi-Fi Credentials")%></p>
            <div class="step"><%=Html.GetThemedSVG("circle-check") %></div>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("3-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Insert Batteries")%></p>
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
