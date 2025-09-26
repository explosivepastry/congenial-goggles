<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<%----     Tab 1      --%>
<div class="tab tab-antenna tab-first">

    <div class="flex">
        <div class="step-box">
            <span><%: Html.TranslateTag("Step 1.")%></span>
            <span><%: Html.TranslateTag("Attach Antenna")%></span>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("2-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Insert SIM")%></p>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("3-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Power On Gateway")%></p>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("4-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Validate Gateway Status")%></p>
        </div>
    </div>

    <div class="flex" style="justify-content:center;">
        <img src="/Content/dashboard/images/Setup/<%:Model.SKU.Contains("-IN-") ? "AntennaCellInd":"AntennaCell" %>.png" />
    </div>

    <div>
        <p></p>
        <p><%: Html.TranslateTag("Attach both antennas to your gateway.")%></p>
        <p><%: Html.TranslateTag("Then press Next to continue.")%></p>
    </div>
    <div class="row-back-next">
        <div>
        </div>
        <div>
            <button onclick="showTabNext('network'); " class="btn btn-primary"><%: Html.TranslateTag("Next")%></button>
        </div>
    </div>
</div>


<%-----    Tab 2         ------%>
<div class="tab tab-network" style="display: none;">
    
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("1-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Attach Antenna")%></p>
            <div class="step"><%=Html.GetThemedSVG("circle-check") %></div>
        </div>
    </div>
    <div class="flex">
        <div class="step-box">
            <span><%: Html.TranslateTag("Step 2.")%></span>
            <span><%: Html.TranslateTag("Insert SIM Card")%></span>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("3-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Power On Gateway")%></p>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("4-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Validate Gateway Status")%></p>
        </div>
    </div>

    <div>
        <p></p>
        <p><%: Html.TranslateTag("If your gateway did not come with a SIM card pre-installed.")%></p>
        <p><%: Html.TranslateTag("Open the gateway by unscrewing the 4 screws and removing the top.  Insert the SIM card into the SIM holder (gold contacts face down).  Then close the gateway and reinstall the 4 screws.")%></p>
        <%if (Model.SKU.Contains("-IN-")){ %>
        <p><%: Html.TranslateTag("Industrial gateway screws must be torqed to the proper setting.  See user guide for more details.")%></p>
        <%} %>
        <p><%: Html.TranslateTag("Then press Next to continue.")%></p>
    </div>
    
    <div class="row-back-next">
        <div>
            <button class="btn btn-secondary" onclick="showTabNext('antenna')"><%: Html.TranslateTag("Back")%></button>
        </div>

        <div>
            <button onclick="showTabNext('power');" class="btn btn-primary"><%: Html.TranslateTag("Next")%></button>
        </div>
    </div>
</div>

<%-----    Tab 3      ------%>
<div class="tab tab-power tab-last" style="display: none;">
    
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("1-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Attach Antenna")%></p>
            <div class="step"><%=Html.GetThemedSVG("circle-check") %></div>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("2-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Insert SIM Card")%></p>
            <div class="step"><%=Html.GetThemedSVG("circle-check") %></div>
        </div>
    </div>
    <div class="flex">
        <div class="step-box">
            <span><%: Html.TranslateTag("Step 3.")%></span>
            <span><%: Html.TranslateTag("Power On Gateway")%></span>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("4-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Validate Gateway Status")%></p>
        </div>
    </div>


    <div class="flex" style="justify-content:center;">
        <img src="/Content/dashboard/images/Setup/<%:Model.SKU.Contains("-IN-") ? "PowerCellInd":"PowerCell" %>.png" />
    </div>
    
    <div>
        <p></p>
        <p><%: Html.TranslateTag("Power on your gateway.")%></p>
        <p><%: Html.TranslateTag("Then press Next to continue.")%></p>
    </div>
    <div class="row-back-next">
        <div>
            <button onclick="showTabNext('network')" class="btn btn-secondary"><%: Html.TranslateTag("Back")%></button>
        </div>

        <div>
            <button onclick="showTabNext('verify');" class="btn btn-primary"><%: Html.TranslateTag("Next")%></button>
        </div>
    </div>
</div>

<%-----    FINDING GATEWAY    ------%>
<div class="tab tab-verify">

    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("1-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Attach Antenna")%></p>
            <div class="step"><%=Html.GetThemedSVG("circle-check") %></div>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("2-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Insert SIM Card")%></p>
            <div class="step"><%=Html.GetThemedSVG("circle-check") %></div>
        </div>
    </div>
    <div class="flex">
        <div class="circle-fill"><%=Html.GetThemedSVG("3-circle-fill") %></div>
        <div class="step-process">
            <p><%: Html.TranslateTag("Power On Gateway")%></p>
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