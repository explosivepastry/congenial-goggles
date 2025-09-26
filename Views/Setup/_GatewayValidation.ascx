<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
 

        <div class="flex" style="justify-content:center;">
            <div class="findgw-grid">
                <a class="findgw-grid" onclick="$(this).children().addClass('done'); refreshGatewayBox();">

                    <div class="fgw-icon"><%=Html.GetThemedSVG("wifi-solid") %>    </div>
                    <div class="flashLED gwOnlineCheck color-me1"></div>
                    <div class="solidLED gwOnlineComplete"></div>

                    <div class="fgw-icon"><%=Html.GetThemedSVG("cloud-upload") %></div>
                    <div class="flashLED gwOnlineCheck color-me2"></div>
                    <div class="solidLED gwOnlineComplete"></div>

                    <div class="fgw-icon"><%=Html.GetThemedSVG("connect-net") %></div>
                    <div class="flashLED gwOnlineCheck color-me3"></div>
                    <div class="solidLED gwOnlineComplete"></div>
                </a>
            </div>


        </div>
        <div class="flex center-me">
            <p class="gwOnlineCheck"><%: Html.TranslateTag("Waiting for the gateway to communicate.")%></p>
            <p class="gwOnlineComplete"><%: Html.TranslateTag("Gateway Setup Complete")%></p>
        </div>

        <%---  Spinner---%>

        <div class="gwOnlineCheck">
            <div class="text-center" id="loading">
                <div class="spinner-border text-primary" role="status">
                    <span class="visually-hidden">Loading...</span>
                </div>
            </div>

                    
            <p><%: Html.TranslateTag("This can take a few minutes")%></p>
            <div class="info-question-gw gwOnlineCheck">
                <%: Html.TranslateTag("Still not working?")%>
                <span class="circleQuestion " data-bs-toggle="modal" data-bs-target="#FGWModal">
                    <%=Html.GetThemedSVG("circleQuestion") %>
                </span>
            </div>
                
        </div>
               

        <div class="row-back-next">
            <div>
                <button onclick="showTabNext('last')" class="btn btn-secondary"><%: Html.TranslateTag("Back")%></button>
            </div>
            <div>
                <button id="skipTheseSteps" class="btn btn-secondary gwOnlineCheck" onclick="confirmModal()">
                    <%: Html.TranslateTag("Skip These Steps", "Skip These Steps")%>
                </button>
                <button id="actionSubmit" class="btn btn-primary gwOnlineComplete" onclick="window.location.href = '/Setup/GatewayComplete/<%=Model.GatewayID + (string.IsNullOrWhiteSpace(Request.Params["reset"]) ? "" : "?reset=" + Request.Params["reset"])%>'">
                    <%: Html.TranslateTag("Continue", "Continue")%>
                </button>
            </div>
        </div>



        
    <!-- Modal Info Box -->
    <div class="modal fade" id="FGWModal" tabindex="-1" aria-labelledby="FGWModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="FGWModalLabel"><%: Html.TranslateTag("Gateway Troubleshooting")%></h4>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" style="color: blue"></button>
                </div>
                <div class="modal-body">
                    <div class="gw-help-tab">
                        <h6><%: Html.TranslateTag("Step 1")%></h6>
                        <p><%: Html.TranslateTag("Confirm the device has been added to an Online account.")%></p>
                        <h6><%: Html.TranslateTag("Step 2")%></h6>
                        <p><%: Html.TranslateTag("Confirm the gateway is connected to the network.")%></p>
                        <h6><%: Html.TranslateTag("Step 3")%></h6>
                        <p><%: Html.TranslateTag("Confirm the gateway is connected to power and completes the start up test.")%></p>
                        <h6><%: Html.TranslateTag("Step 4")%></h6>
                        <p><%: Html.TranslateTag("Confirm the network allows for traffic to the Internet over outbound TCP port 3000 (inbound port is not specified), and that the DNS server on the network can resolve sensorsgateway.com.")%></p>
                        <h6><%: Html.TranslateTag("Step 5")%></h6>
                        <p><%: Html.TranslateTag("Update the gateway's firmware if an update is available.")%></p>
                    </div>
                    <hr />
                    <p style="color: red;"><%: Html.TranslateTag("If gateway is still not responding, press (do not hold) button on gateway to reset.")%></p>
                    <br />
                    <%=Html.Partial("_WebsiteLink") %>
                </div>
            </div>
        </div>
    </div>

    <style>
        /*LED Styles*/
        .solidLED {
            background: black; 
            border-radius: 50%;
            height: 29px;
            width: 29px;
            margin: 0;
            background: radial-gradient(circle at 22px 17px, #11AD3D, #000000e6);
        }
        .flashLED{
	        animation-name: circleColorPalette;
	        animation-duration: 1s;
	        animation-iteration-count: infinite;
	        animation-direction: alternate;
	        animation-timing-function: linear; 
	        display: block;
            background: black; 
            border-radius: 50%;
            height: 29px;
            width: 29px;
            margin: 0;
        }
        .color-me1 {
            animation-name: circleColorPalette;
 
        }
        .color-me2 {
            animation-name: circleColorPalette2;
        }
        .color-me3 {
            animation-name: circleColorPalette3;
        }

        @keyframes circleColorPalette {
	        10% {
		        background: radial-gradient(circle at 22px 17px, #ee6055, #000000e6);
	        }
	        50% {
		        background:radial-gradient(circle at 22px 17px, #11AD3D,#000000e6);
	        }
	        100% {
		        background: radial-gradient(circle at 22px 17px, #808080,#000000e6);
	        }
        }
        @keyframes circleColorPalette2 {
	        10% {
			        background: radial-gradient(circle at 22px 17px, #808080,#000000e6);
	        }
	        50% {
		        background:radial-gradient(circle at 22px 17px,#11AD3D,#000000e6);
	        }
	        100% {
			        background: radial-gradient(circle at 22px 17px, #ee6055, #000000e6);
	        }
        }
        @keyframes circleColorPalette3 {
	        10% {
		        background: radial-gradient(circle at 22px 17px, #ee6055, #000000e6);
	        }
	        50% {
	            background: radial-gradient(circle at 22px 17px, #808080,#000000e6);
	        }
	        100% {
				background:radial-gradient(circle at 22px 17px, #11AD3D,#000000e6);
	        }
        }

    </style>