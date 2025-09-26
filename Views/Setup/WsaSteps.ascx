<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>


<%----     Tab 1 Install Application   --%>
<div class="tab tab-first tab-app">
    
    <div class="flex">
        <div class="step-box">
            <span><%: Html.TranslateTag("Step 1.")%></span>
            <span><%: Html.TranslateTag("Install Application")%></span>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("2-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Attach Antenna")%></p>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("3-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Plug in Cable")%></p>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("4-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Validate Gateway Status")%></p>
        </div>
    </div>

    <div class="flex" style="justify-content:center;">
        <div>
            <svg class="svg-inline--fa fa-cloud-download-alt fa-w-20" aria-hidden="true" focusable="false" data-prefix="fas" data-icon="cloud-download-alt" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 640 512" data-fa-i2svg="">
                <path fill="currentColor" d="M537.6 226.6c4.1-10.7 6.4-22.4 6.4-34.6 0-53-43-96-96-96-19.7 0-38.1 6-53.3 16.2C367 64.2 315.3 32 256 32c-88.4 0-160 71.6-160 160 0 2.7.1 5.4.2 8.1C40.2 219.8 0 273.2 0 336c0 79.5 64.5 144 144 144h368c70.7 0 128-57.3 128-128 0-61.9-44-113.6-102.4-125.4zm-132.9 88.7L299.3 420.7c-6.2 6.2-16.4 6.2-22.6 0L171.3 315.3c-10.1-10.1-2.9-27.3 11.3-27.3H248V176c0-8.8 7.2-16 16-16h48c8.8 0 16 7.2 16 16v112h65.4c14.2 0 21.4 17.2 11.3 27.3z"></path>
            </svg>
            <a class="btn btn-primary" href="https://www.monnit.com/support/downloads/" target="_blank">Download</a>
        </div>
    </div>

    <div>
        <p></p>
        <p><%: Html.TranslateTag("Download the WSA application and install on the host computer.")%> </p>
        <p><%: Html.TranslateTag("Then press Next to continue.")%></p>
    </div>

    <div class="row-back-next">
        <div>
        </div>
        <div>
            <button onclick="showTabNext('antenna'); " class="btn btn-primary"><%: Html.TranslateTag("Next")%></button>
        </div>
    </div>
</div>

<%----     Tab 2 Attach Antenna   --%>
<div class="tab tab-antenna">

    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("1-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Install Application")%></p>
            <div class="step"><%=Html.GetThemedSVG("circle-check") %></div>
        </div>
    </div>
    <div class="flex">
        <div class="step-box">
            <span><%: Html.TranslateTag("Step 2.")%></span>
            <span><%: Html.TranslateTag("Attach Antenna")%></span>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("3-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Plug in Cable")%></p>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("4-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Validate Gateway Status")%></p>
        </div>
    </div>

    <div class="flex" style="justify-content:center;">
        <img src="/Content/dashboard/images/Setup/AntennaWSA.png" />
    </div>

    <div>
        <p></p>
        <p><%: Html.TranslateTag("Attach the antenna to your gateway.")%></p>
        <p><%: Html.TranslateTag("Then press Next to continue.")%></p>
    </div>
    <div class="row-back-next">
        <div>
            <button class="btn btn-secondary" onclick="showTabNext('app')"><%: Html.TranslateTag("Back")%></button>
        </div>
        <div>
            <button onclick="showTabNext('network'); " class="btn btn-primary"><%: Html.TranslateTag("Next")%></button>
        </div>
    </div>
</div>

<%-----    Tab 3  Plug in Cable      ------%>
<div class="tab tab-network tab-last" style="display: none;">
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("1-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Install Application")%></p>
            <div class="step"><%=Html.GetThemedSVG("circle-check") %></div>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("1-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Attach Antenna")%></p>
            <div class="step"><%=Html.GetThemedSVG("circle-check") %></div>
        </div>
    </div>
    <div class="flex">
        <div class="step-box">
            <span><%: Html.TranslateTag("Step 3.")%></span>
            <span><%: Html.TranslateTag("Plug in Cable")%></span>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("4-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Validate Gateway Status")%></p>
        </div>
    </div>
    
    <div class="flex" style="justify-content:center;">
        <img src="/Content/dashboard/images/Setup/PowerWSA.png" />
    </div>
    
    <div>
        <p></p>
        <p><%: Html.TranslateTag("Attach the USB cable to your gateway and the host computer.")%></p>
        <p><%: Html.TranslateTag("Then press Next to continue.")%></p>
    </div>
    <div class="row-back-next">
        <div>
            <button class="btn btn-secondary" onclick="showTabNext('antenna')"><%: Html.TranslateTag("Back")%></button>
        </div>
        <div>
            <button onclick="showTabNext('verify'); " class="btn btn-primary"><%: Html.TranslateTag("Next")%></button>
        </div>
    </div>
</div>

<%-----    Tab 4 Validate Gateway Status   ------%>
<div class="tab tab-verify">

    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("1-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Install Application")%></p>
            <div class="step"><%=Html.GetThemedSVG("circle-check") %></div>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("2-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Attach Antenna")%></p>
            <div class="step"><%=Html.GetThemedSVG("circle-check") %></div>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("3-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Plug in Cable")%></p>
            <div class="step"><%=Html.GetThemedSVG("circle-check") %></div>
        </div>
    </div>
    <div class="flex">
        <div class="step-box">
            <span><%: Html.TranslateTag("Step 4.")%></span>
            <span><%: Html.TranslateTag("Validate Gateway Status")%></span>
        </div>
    </div>

    <%:Html.Partial("_GatewayValidation") %>
    

</div>