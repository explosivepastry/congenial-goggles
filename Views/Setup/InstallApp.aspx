<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.AccountTheme>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <%if (MonnitSession.CurrentStyle("MobileAppName").Length > 0)
        { %>
    <link rel="manifest" href="/pwamanifest">
    <meta name="theme-color" content="#000000" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <link rel="apple-touch-icon" sizes="180x180" href="/PWAIcon/180x180">
    <link rel="apple-touch-startup-image" sizes="2048x2732" href="/PWAIcon/2048x2732" />
    <link rel="apple-touch-startup-image" sizes="1668x2224" href="/PWAIcon/1668x2224" />
    <link rel="apple-touch-startup-image" sizes="1536x2048" href="/PWAIcon/1536x2048" />
    <link rel="apple-touch-startup-image" sizes="1125x2436" href="/PWAIcon/1125x2436" />
    <link rel="apple-touch-startup-image" sizes="1242x2208" href="/PWAIcon/1242x2208" />
    <link rel="apple-touch-startup-image" sizes="750x1334" href="/PWAIcon/750x1334" />
    <link rel="apple-touch-startup-image" sizes="640x1136" href="/PWAIcon/640x1136" />
    <%} %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%="" %>
    <%string language = "english";
        if (Request.QueryString["language"] != null)
            language = Request.QueryString["language"].ToString();

        string appName = MonnitSession.CurrentStyle("MobileAppName");
        string url = MonnitSession.CurrentTheme != null ? MonnitSession.CurrentTheme.Domain : "";
        string[] urlSplit = url.Split('.');
        string themeUrl = "";
        for (int i = urlSplit.Length - 1; i >= 0; i--)
        {
            if (i == 0)
                themeUrl += urlSplit[i];
            else
                themeUrl += urlSplit[i] + ".";
        }

        if (MonnitSession.CurrentStyle("MobileAppName").Length > 0)
        {%>
    <div class="login_container">
        <div class="login_form_container">
            <div class="text-center">
                <div id="testMsgs" class="col-6"></div>

                <div class="col-12" style="text-align: center; margin-top: 50px; font-size: 28px;">
                    <img src="/PWAIcon/50x50" alt="PWA">
                    <%:appName%>
                </div>
            </div>

            <div class="login_form" id="installDiv">
                <div class="login_create_tabs">
                    <div class="login_tab current" data-tab="tab-1">
                        <%:Html.TranslateTag("Setup/InstallApp|Install Instructions","Install Instructions")%>
                    </div>

                   
                    <div class="login_tab" data-tab="tab-2">
                        <a href="/Overview/" class="login_create_tab btn-cancel"><%:Html.TranslateTag("Done","Done")%>
                        </a>
                    </div>
                </div>

                <div id="loginForm" class="login_inputs  col-12">

                    <div class="col-12 promptDiv" style="margin-top: 5px;" id="defaultPrompt">
                        <%: Html.TranslateTag("Setup/InstallApp|To install the app to your device click the \"Install\" button below. Your browser will then prompt you for permissions to install","To install the app to your device click the \"Install\" button below. Your browser will then prompt you for permissions to install")%> <%:appName%>.
                    </div>

                    <div class="d-flex w-100" style="justify-content: center;">
                        <button class="learnBtn" data-bs-target=".pwaHelp" data-bs-toggle="modal">
                            <svg class="learnMore" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
                                <path d="M256 512A256 256 0 1 0 256 0a256 256 0 1 0 0 512zM216 336h24V272H216c-13.3 0-24-10.7-24-24s10.7-24 24-24h48c13.3 0 24 10.7 24 24v88h8c13.3 0 24 10.7 24 24s-10.7 24-24 24H216c-13.3 0-24-10.7-24-24s10.7-24 24-24zm40-208a32 32 0 1 1 0 64 32 32 0 1 1 0-64z" />
                            </svg>Learn More 
                        </button>
                    </div>




                    <%--Samsung--%>
                    <div class="col-12 promptDiv" style="margin-top: 5px; display: none;" id="samsungPrompt">
                        <b class="d-flex pwa-txt"><%: Html.TranslateTag("Setup/InstallApp|To install the app to your device tap the \"Menu\" icon","To install the app to your device tap the \"Menu\" icon")%></b>
                        <div style="display: flex; flex-wrap: wrap; justify-content: center;">
                            <img class="pwa-img" src="../../PWA/A2HS-Images/Samsung-Android-1.png" />
                            <b class="d-flex pwa-txt"><%: Html.TranslateTag("Setup/InstallApp|Scroll down list and tap \"Add to Home screen\" ","Scroll down list and tap \"Add to Home screen\" ")%></b>
                            <img class="pwa-img" src="../../PWA/A2HS-Images/Samsung-Android-2.png" />
                            <b class="d-flex pwa-txt"><%: Html.TranslateTag("Setup/InstallApp|Tap \"Add\" button","Tap \"Add\" button")%> </b>
                            <img class="pwa-img" src="../../PWA/A2HS-Images/Samsung-Android-3.png" />
                        </div>
                    </div>

                    <%--Firefox--%>
                    <div class="col-12 promptDiv" style="margin-top: 5px; display: none;" id="firefoxPrompt">
                        <%: Html.TranslateTag("Setup/InstallApp|To install the app to your device tap the \"Add to Homescreen\" icon","To install the app to your device tap the \"Add to Homescreen\" icon")%>
                        <img src="../../PWA/A2HS-Images/firefox-a2hs-icon.jpg" style="width: 230px;" />
                    </div>

                    <%--Opera--%>
                    <div class="col-12 promptDiv" style="margin-top: 5px; display: none;" id="operaPrompt">
                        <div style="display: flex; flex-wrap: wrap; justify-content: center;">
                            <b class="d-flex pwa-txt"><%: Html.TranslateTag("Setup/InstallApp|To install the app to your device tap the plus icon from your browser","To install the app to your device tap the plus icon from your browser")%></b>
                            <img class="pwa-img-opera" src="../../PWA/A2HS-Images/Opera-pwa-1.png" />
                            <b class="d-flex pwa-txt"><%: Html.TranslateTag("Setup/InstallApp|Tap \"Add to Home Screen\" ","Tap \"Add to Home Screen\" ")%> </b>
                            <img class="pwa-img-opera" src="../../PWA/A2HS-Images/Opera-pwa-2.png" />
                            <b class="d-flex pwa-txt"><%: Html.TranslateTag("Setup/InstallApp|Tap \"Add\" to complete","Tap \"Add\" to complete")%></b>
                            <img class="pwa-img-opera" src="../../PWA/A2HS-Images/Opera-pwa-3.png" />
                        </div>
                    </div>

                    <%--Edge--%>
                    <div class="col-12 promptDiv" style="margin-top: 5px; display: none;" id="edgePrompt">
                        <div class="edge-pwa-step">
                            <b class="d-flex pwa-txt"><%: Html.TranslateTag("Setup/InstallApp|To install the app to your device tap the \"Add to Homescreen\" icon","To install the app to your device tap the \"Add to Homescreen\" icon")%></b>
                            <div class="pwa-svg">
                                <svg id="uuid-c3733b9d-7ae8-4c3f-a186-cbaa8dd539ec" data-name="Layer 2" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 68.26 68.78">
                                    <g id="uuid-761612f6-2d18-4c62-b7b1-4ed506269fb0" data-name="edge">
                                        <rect x="1" y="1" width="32.35" height="32.35" rx="2.35" ry="2.35" fill="none" stroke="#666" stroke-miterlimit="10" stroke-width="2" />
                                        <rect x="34.91" y="35.43" width="32.35" height="32.35" rx="2.35" ry="2.35" fill="none" stroke="#666" stroke-miterlimit="10" stroke-width="2" />
                                        <rect x="1" y="35.43" width="32.35" height="32.35" rx="1.83" ry="1.83" fill="none" stroke="#666" stroke-miterlimit="10" stroke-width="2" />
                                        <line x1="52.65" y1="1" x2="52.65" y2="31.26" fill="none" stroke="#666" stroke-miterlimit="10" stroke-width="2" />
                                        <line x1="37" y1="16.13" x2="66.22" y2="16.13" fill="none" stroke="#666" stroke-miterlimit="10" stroke-width="2" />
                                    </g>
                                </svg>
                            </div>
                        </div>

                        <img class="pwa-img-chrome" src="../../PWA/A2HS-Images/Edge-Pwa-01.png" />
                    </div>

                    <%--Chrome--%>
                    <div class="col-12 promptDiv" style="margin-top: 5px; display: none;" id="chromiumPrompt">
                        <b class="d-flex pwa-txt"><%: Html.TranslateTag("Setup/InstallApp|To install the app to your device tap the \"Add to Homescreen\" from your browser","To install the app to your device tap the \"Add to Homescreen\" from your browser")%></b>
                        <div class="pwa-img-contain">
                            <img class="pwa-img-chrome" src="../../PWA/A2HS-Images/Chrome-01.png" />
                        </div>
                    </div>

                    <%--Safari ipad--%>
                    <div class="col-12 promptDiv" style="margin-top: 5px; display: none;" id="safariIpadPrompt">
                        <b class="d-flex pwa-txt"><%: Html.TranslateTag("Setup/InstallApp|To install the app to your device tap the \"Add to Homescreen\" icon","To install the app to your device tap the \"Add to Homescreen\" icon")%></b>
                        <img class="pwa-img-safari" src="../../PWA/A2HS-Images/safari-ipad-share-a2hs-right.jpg" />
                    </div>

                    <%--IOS--%>
                    <div class=" promptDiv" style="display: flex; margin-top: 5px; flex-wrap: wrap; display: none; width: 100%; justify-content: center;" id="iosPrompt">
                        <div class="d-flex" style="flex-wrap: wrap; justify-content: start; margin-bottom: 15px; font-weight: bold;">
                            <b class="d-flex pwa-txt"><%: Html.TranslateTag("Setup/InstallApp|To install the app to your iOS device:","To install the app to your iOS device:")%></b>
                            <ol class="direct-instructions">
                                <li class=" d-flex pwa-txt"><%: Html.TranslateTag("Setup/InstallApp|iOS TapShare","Tap the share icon on the bottom bar")%></li>
                                <li class=" d-flex pwa-txt"><%: Html.TranslateTag("Setup/InstallApp|iOS Add to Home","Drag the share menu up and scroll down to find \"Add to Home Screen\" ")%></li>
                                <li class=" d-flex pwa-txt"><%: Html.TranslateTag("Setup/InstallApp|iOS Add Button","Tap the \"Add\" button on the top right corner")%></li>
                            </ol>
                        </div>

                        <div class="d-flex" style="flex-wrap: wrap; justify-content: center;">
                            <img class="iosGif" src="../../PWA/A2HS-Images/IosPwaSafari.gif" />
                        </div>
                    </div>

                    <%--IPhone Safari using Request Desktop --%>
                    <div class="col-12 promptDiv" style="margin-top: 5px; display: none;" id="requestDesktopPrompt">
                        <%: Html.TranslateTag("Setup/InstallApp|It looks like you are requesting the Desktop version of the website. Please update this setting and refresh the page to install.","It looks like you are requesting the Desktop version of the website. Please update this setting and refresh the page to install.")%><br />
                    </div>

                    <%--IPhone Chrome --%>
                    <div class="col-12 promptDiv" style="margin-top: 5px; display: none;" id="switchToSafariPrompt">
                        <%: Html.TranslateTag("Setup/InstallApp|This browser is not supported on iOS.  Please use the mobile Safari browser to install the App.")%><br />
                    </div>

                    <%--Install Button is Disabled Temporarly--%>
                    <div id="installBtnDiv" class="col-12 loginBtn__container" style="clear: both; margin-top: 20px; align-content: center; vertical-align: middle!important;">
                        <button id="installAppBtn" class="btn btn-install btn-primary" style="width: 100%;"><%:Html.TranslateTag("Setup/InstallApp|Install","Install")%></button>
                    </div>
                    <div id="AndroidInstructionsDiv" class="col-12 loginBtn__container" style="clear: both; margin-top: 20px; align-content: center; vertical-align: middle!important; display: none;">
                    <%: Html.TranslateTag("Setup/InstallApp|Thank you for installing this.  You can find the app on your Home Screen.")%>
                    </div>
                     <div id="WindowsInstructionsDiv" class="col-12 loginBtn__container" style="clear: both; margin-top: 20px; align-content: center; vertical-align: middle!important; display: none;">
                     <%: Html.TranslateTag("Setup/InstallApp|Thank you for installing this.  You can find the app in your Start Menu.")%>
                     </div>
                     <div id="InstalledAndroidInstructionsDiv" class="col-12 loginBtn__container" style="clear: both; margin-top: 20px; align-content: center; vertical-align: middle!important; display: none;">
                     <%: Html.TranslateTag("Setup/InstallApp|Looks like the app is already installed. You can find the app on your Home Screen.")%>
                     </div>
                     <div id="InstalledWindowsInstructionsDiv" class="col-12 loginBtn__container" style="clear: both; margin-top: 20px; align-content: center; vertical-align: middle!important; display: none;">
                     <%: Html.TranslateTag("Setup/InstallApp|Thank you for installing this.  You can find the app in your Start Menu.")%>
                     </div>
                </div>
            </div>

            <div id="notCompatibleDiv" style="display: none;" class="notCompatible">
                <div class="d-flex mbsc-justify-content-center">
                    <h6><%: Html.TranslateTag("Setup/InstallApp|Sorry looks like the app can'tbe installed on this device.","Sorry looks like the app can't be installed on this device.")%><br />
                        <%: Html.TranslateTag("Setup/InstallApp|Look below for compatible OS/Browser combinations","Look below for compatible OS/Browser combinations")%>
                    </h6>
                </div>

                <div style="color: #0067ab">
                    <ul class="noncomp-box">
                        <li><%: Html.TranslateTag("Setup/InstallApp|IOS/Safari (Desktop Safari not compatible)","IOS/Safari (Desktop Safari not compatible)")%></li>
                        <li><%: Html.TranslateTag("Setup/InstallApp|Android/Chrome","Android/Chrome")%></li>
                        <li><%: Html.TranslateTag("Setup/InstallApp|Android/Samsung Internet","Android/Samsung Internet")%></li>
                        <li><%: Html.TranslateTag("Setup/InstallApp|Windows/Edge","Windows/Edge")%></li>
                        <li><%: Html.TranslateTag("Setup/InstallApp|Windows/Chrome","Windows/Chrome")%></li>
                        <li><%: Html.TranslateTag("Setup/InstallApp|MacOS/Chrome","MacOS/Chrome")%></li>
                        <li><%: Html.TranslateTag("Setup/InstallApp|MacOS/Edge","MacOS/Edge")%></li>
                    </ul>
                    <div class="col-12 loginBtn__container" style="clear: both; margin-top: 20px; align-content: center; vertical-align: middle!important;">
                        <button class="btn btn-install btn-primary" style="width: 100%;" onclick="location.href='https://www.imonnit.com/account/LogOnOV';"><%:Html.TranslateTag("Back to Login","Back to Login")%></button>
                    </div>
                </div>
            </div>
        </div>

        <div class="login_image">
            <img src="<%= Html.GetThemedContent("/images/login-dashPhone.png")%>" style="width: 100%;" />
        </div>
    </div>



    <div class="modal fade pwaHelp" style="z-index: 2000!important; background: rgba(0,0,0,0.5);" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="pageHelp"><%= Html.TranslateTag("Progressive Web App") %> (PWA)</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">

                    <div class="container">


                        <div class="row">
                            <div class="word-choice" style="text-align: center;">
                                <%= Html.TranslateTag("Benefits of Progressive Web Apps ") %>(PWA)
                                <hr />
                            </div>
                            <div class=" word-def">

                                <div class="d-flex" style="gap: 10px;">
                                    <div class="circle">
                                        <svg class="fast-access" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 384 512">
                                            <path d="M317.4 44.6c5.9-13.7 1.5-29.7-10.6-38.5s-28.6-8-39.9 1.8l-256 224C.9 240.7-2.6 254.8 2 267.3S18.7 288 32 288H143.5L66.6 467.4c-5.9 13.7-1.5 29.7 10.6 38.5s28.6 8 39.9-1.8l256-224c10-8.8 13.6-22.9 8.9-35.3s-16.6-20.7-30-20.7H240.5L317.4 44.6z" />
                                        </svg>
                                    </div>

                                    <div class=" pwaWords">
                                        <b style="font-size: 16px; color: #0067ab;"><%= Html.TranslateTag("Fast Access") %></b>
                                        <p style="margin: 0"><%= Html.TranslateTag("Download, add to the home screen, open, and use.") %></p>
                                    </div>
                                </div>

                                <%--  SECURE  -------------%>

                                <div class="d-flex" style="gap: 10px;">
                                    <div class="circle">
                                        <svg class="fast-access" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
                                            <path d="M240 0c4.6 0 9.2 1 13.4 2.9L441.7 82.8c22 9.3 38.4 31 38.3 57.2c-.5 99.2-41.3 280.7-213.7 363.2c-16.7 8-36.1 8-52.8 0C41.3 420.7 .5 239.2 0 140c-.1-26.2 16.3-47.9 38.3-57.2L226.7 2.9C230.8 1 235.4 0 240 0zm0 66.8V444.8C378 378 415.1 230.1 416 141.4L240 66.8l0 0z" />
                                        </svg>
                                    </div>

                                    <div class=" pwaWords">
                                        <b style="font-size: 16px; color: #0067ab;"><%= Html.TranslateTag("Secure") %></b>
                                        <p style="margin: 0">
                                            <%= Html.TranslateTag("Works only through HTTPs, so your data is encrypted during transmission.") %>
                                        </p>
                                    </div>
                                </div>
                                <%--   COMPACT-------------%>
                                <div class="d-flex" style="gap: 10px;">
                                    <div class="circle">
                                        <svg class="fast-access" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 640 512">
                                            <path d="M45.9 42.1c3-6.1 9.6-9.6 16.3-8.7L307 64 551.8 33.4c6.7-.8 13.3 2.7 16.3 8.7l41.7 83.4c9 17.9-.6 39.6-19.8 45.1L426.6 217.3c-13.9 4-28.8-1.9-36.2-14.3L307 64 223.6 203c-7.4 12.4-22.3 18.3-36.2 14.3L24.1 170.6C4.8 165.1-4.7 143.4 4.2 125.5L45.9 42.1zM308.1 128l54.9 91.4c14.9 24.8 44.6 36.6 72.5 28.6L563 211.6v167c0 22-15 41.2-36.4 46.6l-204.1 51c-10.2 2.6-20.9 2.6-31 0l-204.1-51C66 419.7 51 400.5 51 378.5v-167L178.6 248c27.8 8 57.6-3.8 72.5-28.6L305.9 128h2.2z" />
                                        </svg>
                                    </div>

                                    <div class=" pwaWords">
                                        <b style="font-size: 16px; color: #0067ab;"><%= Html.TranslateTag("Compact") %></b>
                                        <p style="margin: 0">
                                            <%= Html.TranslateTag("Takes less than 1 percent of the space required for a native app.") %>
                                        </p>
                                    </div>
                                </div>
                                <%--  AUTO -------------%>
                                <div class="d-flex" style="gap: 10px;">
                                    <div class="circle">
                                        <svg class="fast-access" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
                                            <path d="M89.1 202.6c7.7-21.8 20.2-42.3 37.8-59.8c62.5-62.5 163.8-62.5 226.3 0L370.3 160H320c-17.7 0-32 14.3-32 32s14.3 32 32 32H447.5c0 0 0 0 0 0h.4c17.7 0 32-14.3 32-32V64c0-17.7-14.3-32-32-32s-32 14.3-32 32v51.2L398.4 97.6c-87.5-87.5-229.3-87.5-316.8 0C57.2 122 39.6 150.7 28.8 181.4c-5.9 16.7 2.9 34.9 19.5 40.8s34.9-2.9 40.8-19.5zM23 289.3c-5 1.5-9.8 4.2-13.7 8.2c-4 4-6.7 8.8-8.1 14c-.3 1.2-.6 2.5-.8 3.8c-.3 1.7-.4 3.4-.4 5.1V448c0 17.7 14.3 32 32 32s32-14.3 32-32V396.9l17.6 17.5 0 0c87.5 87.4 229.3 87.4 316.7 0c24.4-24.4 42.1-53.1 52.9-83.7c5.9-16.7-2.9-34.9-19.5-40.8s-34.9 2.9-40.8 19.5c-7.7 21.8-20.2 42.3-37.8 59.8c-62.5 62.5-163.8 62.5-226.3 0l-.1-.1L109.6 352H160c17.7 0 32-14.3 32-32s-14.3-32-32-32H32.4c-1.6 0-3.2 .1-4.8 .3s-3.1 .5-4.6 1z" />
                                        </svg>
                                    </div>

                                    <div class=" pwaWords">
                                        <b style="font-size: 16px; color: #0067ab;"><%= Html.TranslateTag("Auto Sync") %></b>
                                        <p style="margin: 0">
                                            <%= Html.TranslateTag("Updates are automatic and immediately appear.") %>
                                        </p>
                                    </div>
                                </div>
                                <%--   DOWNLOAD-------------%>
                                <div class="d-flex" style="gap: 10px;">
                                    <div class="circle">
                                        <svg class="fast-access" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
                                            <path d="M288 32c0-17.7-14.3-32-32-32s-32 14.3-32 32V274.7l-73.4-73.4c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3l128 128c12.5 12.5 32.8 12.5 45.3 0l128-128c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0L288 274.7V32zM64 352c-35.3 0-64 28.7-64 64v32c0 35.3 28.7 64 64 64H448c35.3 0 64-28.7 64-64V416c0-35.3-28.7-64-64-64H346.5l-45.3 45.3c-25 25-65.5 25-90.5 0L165.5 352H64zm368 56a24 24 0 1 1 0 48 24 24 0 1 1 0-48z" />
                                        </svg>
                                    </div>

                                    <div class=" pwaWords">
                                        <b style="font-size: 16px; color: #0067ab;"><%= Html.TranslateTag("Direct Download") %></b>
                                        <p style="margin: 0">
                                            <%= Html.TranslateTag("Meets high-quality, direct accessibility standards that do not require App Store or Google Play Store access.") %> 

                                        </p>
                                    </div>
                                </div>
                                <%--  MULTI -------------%>
                                <div class="d-flex" style="gap: 10px;">
                                    <div class="circle">
                                        <svg class="fast-access" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512">
                                            <path d="M64 0C28.7 0 0 28.7 0 64V352c0 35.3 28.7 64 64 64H240l-10.7 32H160c-17.7 0-32 14.3-32 32s14.3 32 32 32H416c17.7 0 32-14.3 32-32s-14.3-32-32-32H346.7L336 416H512c35.3 0 64-28.7 64-64V64c0-35.3-28.7-64-64-64H64zM512 64V352H64V64H512z" />
                                        </svg>
                                    </div>

                                    <div class=" pwaWords">
                                        <b style="font-size: 16px; color: #0067ab;"><%= Html.TranslateTag("Multi-Platform") %></b>
                                        <p style="margin: 0">
                                            <%= Html.TranslateTag("Delivers the same experience across any mobile device.") %>
                                        </p>
                                    </div>
                                </div>
                                <%--   SUPPORT -------------%>
                                <div class="d-flex" style="gap: 10px;">
                                    <div class="circle">
                                        <svg class="fast-access" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 384 512">
                                            <path d="M318.7 268.7c-.2-36.7 16.4-64.4 50-84.8-18.8-26.9-47.2-41.7-84.7-44.6-35.5-2.8-74.3 20.7-88.5 20.7-15 0-49.4-19.7-76.4-19.7C63.3 141.2 4 184.8 4 273.5q0 39.3 14.4 81.2c12.8 36.7 59 126.7 107.2 125.2 25.2-.6 43-17.9 75.8-17.9 31.8 0 48.3 17.9 76.4 17.9 48.6-.7 90.4-82.5 102.6-119.3-65.2-30.7-61.7-90-61.7-91.9zm-56.6-164.2c27.3-32.4 24.8-61.9 24-72.5-24.1 1.4-52 16.4-67.9 34.9-17.5 19.8-27.8 44.3-25.6 71.9 26.1 2 49.9-11.4 69.5-34.3z" />
                                        </svg>
                                    </div>

                                    <div class=" pwaWords">
                                        <b style="font-size: 16px; color: #0067ab;"><%= Html.TranslateTag("Supports iOS") %></b>
                                        <p style="margin: 0">
                                            <%= Html.TranslateTag("Runs on iOS version 11.3 to present.") %>
                                        </p>
                                    </div>
                                </div>



                            </div>
                        </div>



                    </div>

                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>

    <style type="text/css">
        .pwaWords {
            display: flex;
            flex-direction: column;
            margin: 6px 0 0 55px;
        }

        .learnMore {
            fill: #0067ab;
            width: 20px;
            height: 20px;
            margin-right: 5px;
        }

        .learnBtn:hover svg, .learnBtn:hover {
            fill: white;
        }

        .learnBtn {
            border: 1px solid #00000014;
            border-radius: 5px;
            padding: 5px 7px;
            font-weight: bold;
            color: var(--options-text-color);
            display: flex;
            margin: 10px 0 10px;
            justify-content: center;
            background: var(--option-menu-color);
            box-shadow: rgb(0 0 0 / 16%) 0px 3px 6px, rgb(0 0 0 / 5%) 0px 3px 6px;
            width: 127px;
            align-items: center;
        }

            .learnBtn:hover {
                transform: scale(1.02);
                background-color: var(--options-hover-color);
                color: var(--options-text-hover);
                box-shadow: rgb(9 30 66 / 40%) 0px 2px 10px, rgb(9 30 66 / 30%) 0px 7px 13px -3px, rgb(9 30 66 / 20%) 0px -3px 0px inset;
            }

        .circle {
            background: #e1e1e1;
            border-radius: 36px;
            width: 37px;
            height: 36px;
            display: flex;
            position: absolute;
            align-items: center;
            justify-content: center;
            filter: drop-shadow(1px 1px 1px #14161759);
        }

        .fast-access {
            width: 20px;
            height: 20px;
            fill: #0076ab;
            filter: drop-shadow(1px 1px 1px #14161759);
        }

        .direct-instructions > li {
            display: list-item !important;
            margin-bottom: 10px;
        }

        .iosGif {
            border-radius: 38px;
        }

        .notCompatible {
            display: flex;
            flex-direction: column;
            padding: 1rem;
            text-align: center;
            flex-wrap: wrap;
            font-weight: bold;
        }

        .login_logo_container {
            display: flex;
            flex-direction: column;
            align-items: center;
        }

        h3 {
            margin: 20px 0;
        }

        .pwa-img-edge {
            width: 100%;
        }

        .pwa-img-chrome {
            width: 100%;
            box-shadow: rgba(0, 0, 0, 0.44) 0px 3px 8px;
        }

        .pwa-img-opera {
            width: 100%;
            box-shadow: rgba(0, 0, 0, 0.44) 0px 3px 8px;
            border-radius: 5px;
            margin: 10px 0;
        }

        .pwa-img-safari {
            margin: 10px 0;
            width: 100%;
            box-shadow: rgb(0 0 0 / 54%) 0px 3px 8px;
            border-radius: 10px;
        }

        .pwa-img-contain {
            display: flex;
            justify-content: center;
        }

        .pwa-img {
            width: 80%;
        }

        .pwa-txt {
            color: #0067ab;
        }

        .pwa-svg svg {
            width: 30px;
            height: 30px;
        }

        .edge-pwa-step {
            display: flex;
            display: flex !important;
            margin: 10px;
            align-items: center;
        }

        .noncomp-box {
            box-shadow: rgb(0 0 0 / 24%) 0px 3px 8px;
            padding: 21px;
            border-radius: 5px;
            margin: 16px;
        }

        @media (max-width: 930px) {
            .login_image {
                display: none;
            }

            .login_form_container {
                width: 100vw;
            }
        }
    </style>

    <script src="/PWA/addtohomescreen.js"></script>

    <script type="text/javascript">

        var platform;
        $(function () {
            if ("serviceWorker" in navigator) {
                navigator.serviceWorker.register("/pwa.js")
                    .then(function (registration) {
                        callATH();
                        // Registration was successful
                        //console.log("ServiceWorker registration successful with scope: " + registration.scope);
                        //printTestMsg("ServiceWorker registration successful with scope: " +  registration.scope);

                    }).catch(function (err) { // registration failed :(
                        //console.log("ServiceWorker registration failed: ", err);
                        //printTestMsg("ServiceWorker registration failed: " + err);
                    });

                $('#installAppBtn').click(function () {
                    if (platform != null && platform != undefined && platform.beforeInstallPrompt) {
                        $('#installBtnDiv').hide();
                        triggerNativePrompt();

                        if (platform.isAndroid) {
                            $('#AndroidInstructionsDiv').show();
                        } else {
                            $('#WindowsInstructionsDiv').show();
                        }
                    } else if (platform != null && platform.beforeInstallPrompt == undefined && !platform.isIOS) {
                        $('#installBtnDiv').hide();
                        //Show text related to app already being installed
                        if (platform.isAndroid) {
                            $('#InstalledAndroidInstructionsDiv').show();
                        } else {
                            $('#InstalledWindowsInstructionsDiv').show();
                        }
                    }
                });
            }

            /* https://caniuse.com/mdn-api_navigator_getinstalledrelatedapps -- Last checked 3/28/24 - Chrome Android "supports" detecting PWA.
            *  But with code below navigator.getInstalledRelatedApps() still returns no results in Chrome Android
            */
            //printTestMsg('getInstalledRelatedApps exists: ' + ('getInstalledRelatedApps' in window.navigator));
            //if ('getInstalledRelatedApps' in window.navigator) {
            //    navigator.getInstalledRelatedApps().then(relatedApps => {
            //        printTestMsg('relatedapps -> ' + JSON.stringify(relatedApps));
            //        console.log('======================================================');
            //        console.table(relatedApps);
            //        console.log('======================================================');

            //        relatedApps.forEach(app => {
            //            //if your PWA exists in the array it is installed
            //            console.log(app.id, app.platform, app.url);
            //            printTestMsg(app.id, app.platform, app.url);
            //        })
            //    });
            //}
        });

        function printTestMsg(msg) {
            var newMsg = $('#testMsgs').html() + "<br/>";
            var commaCount = 0;
            for (var i = 0; i < msg.length; i++) {
                var currentChar = msg[i];
                if (currentChar == ',') {
                    commaCount++;
                }

                if (commaCount == 3) {
                    newMsg += "<br/>";
                    commaCount = 0;
                }

                newMsg += msg[i];
            }
            $('#testMsgs').html(newMsg);
        }

        function callATH() {
            var ath = addToHomescreen({
                appID: "<%:themeUrl%>",
                appName: "<%:appName%>",

                onCanInstall: function (_platform, _instance) {
                    platform = _platform;
                    console.log(`---- onCanInstall - platform ----\r\n ${JSON.stringify(_platform)} \r\n----------------`);
                    //printTestMsg(`---- onCanInstall - platform ----\r\n ${JSON.stringify(_platform)} \r\n----------------`);
                    platformPrompt(platform);
                },

                onInstall: function (_platform) {
                    console.log(`---- onAppInstalled - platform ----\r\n ${JSON.stringify(_platform)} \r\n----------------`);
                    //printTestMsg(`---- onAppInstalled - platform ----\r\n ${JSON.stringify(_platform)} \r\n----------------`);
                    setTimeout(function () {
                        checkPlatform();

                        if (platform.isStandalone) {
                            console.log('overview redirect');
                            window.location.href = "/Overview";
                        }
                    }, 1000) // 1 second
                },

                onBeforeInstallPrompt: function (_platform) {
                    platform = _platform;
                    console.log(`---- onBeforeInstallPrompt - platform ----\r\n ${JSON.stringify(_platform)} \r\n----------------`);
                    //printTestMsg(`---- onBeforeInstallPrompt - platform ----\r\n ${JSON.stringify(_platform)} \r\n----------------`);

                    platformPrompt(platform);
                }
                
            });

            //if (platform.NotSet == true) {
                let isIOS = (/iPad|iPhone|iPod/.test(navigator.platform) ||
                    (navigator.platform === 'MacIntel' && navigator.maxTouchPoints > 1)) &&
                    !window.MSStream;

                if (isIOS) {
                    $('#installDiv').show();
                    $('#notCompatibleDiv').hide();
                    $('#installBtnDiv').hide();
                    $('.promptDiv').hide();
                    $('#requestDesktopPrompt').show();
                    return;
                }
                //$('#installDiv').hide();
                //$('#notCompatibleDiv').show();
            //}
        }

        function platformPrompt(_platform) {
            var forcePlatform = queryString("forcePlatform");
            console.log(forcePlatform);
            if (forcePlatform.length == 0) {
                $('#installDiv').show();
                $('#notCompatibleDiv').hide();
                $('.promptDiv').hide();

                if (_platform.nativePrompt) {
                    $('#defaultPrompt').show();
                }
                else if (_platform.isSamsung) {
                    $('#samsungPrompt').show();
                }
                else if (_platform.isFirefox) {
                    $('#firefoxPrompt').show();
                }
                else if (_platform.isOpera) {
                    $('#operaPrompt').show();
                }
                else if (_platform.isEdge) {
                    $('#edgePrompt').show();
                }
                else if (_platform.isChromium) {
                    $('#chromiumPrompt').show();
                }
                else if (_platform.isiPad) {
                    $('#safariIpadPrompt').show();
                }
                else if (_platform.isMobileSafari) {
                    $('#iosPrompt').show();
                    $('#installBtnDiv').hide();
                }
                else if (_platform.isIDevice) {
                    $('#switchToSafariPrompt').show();
                    $('#installBtnDiv').hide();
                }
            } else {
                $('#installDiv').show();
                $('#notCompatibleDiv').hide();
                $('.promptDiv').hide();

                if (forcePlatform == "nativePrompt") {
                    $('#defaultPrompt').show();
                    return;
                }
                else if (forcePlatform == "isSamsung") {
                    $('#samsungPrompt').show();
                    return;
                }
                else if (forcePlatform == "isFirefox") {
                    $('#firefoxPrompt').show();
                    return;
                }
                else if (forcePlatform == "isOpera") {
                    $('#operaPrompt').show();
                    return;
                }
                else if (forcePlatform == "isEdge") {
                    $('#edgePrompt').show();
                    return;
                }
                else if (forcePlatform == "isChromium") {
                    $('#chromiumPrompt').show();
                    return;
                }
                else if (forcePlatform == "isiPad") {
                    $('#safariIpadPrompt').show();
                    return;
                }
                else if (forcePlatform == "isIDevice") {
                    $('#iosPrompt').show();
                    $('#installBtnDiv').hide();
                    return;
                } else {
                    $('#installDiv').hide();
                    $('#notCompatibleDiv').show();
                    $('.promptDiv').show();
                    return;
                }
            }
        }

        function queryString(name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regexS = "[\\?&]" + name + "=([^&#]*)";
            var regex = new RegExp(regexS);
            var results = regex.exec(window.location.href);
            if (results == null)
                return "";
            else
                return decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        function triggerNativePrompt() {
            platform.beforeInstallPrompt.prompt();
            //.then(function (e) {
            //    return platform.beforeInstallPrompt.userChoice;
            //})
            //.then(function (choiceResult) {
            //    console.log(`---choiceResult---\r\n ${JSON.stringify(choiceResult)} \r\n ------------`);
            //    if (choiceResult.outcome === "accepted") {
            //    }
            //})
            //.catch(function (err) {
            //    //console.error(err.message);
            //});
        }

    </script>

    <script type="text/javascript">

        function switchLanguages(languageName) {
            var old_url = window.location.href;
            var new_url = old_url.substring(0, old_url.indexOf('?'));
            window.location.href = new_url + "?language=" + languageName;
        }

    </script>

    <%}
        else
        {%>
    <h1><%: Html.TranslateTag("App not available","App not available")%></h1>
    <%} %>
</asp:Content>
