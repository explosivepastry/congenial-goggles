<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" Inherits="System.Web.Mvc.ViewPage<iMonnit.Models.LogOnModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Login
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%string language = "english";
      if (Request.QueryString["language"] != null)
          language = Request.QueryString["language"].ToString(); 
        string returnUrl = "";
        if (Request.QueryString["ReturnUrl"] != null)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9/]");
            returnUrl = rgx.Replace(Request.QueryString["ReturnUrl"].ToString(), "");
        }
    %>
    <meta name="viewport" content="width=device-width, user-scalable=no" />
    
    <script type="text/javascript">
        if (window.location.href.toLowerCase().indexOf('/account/logonov') == -1)
            window.location.href = '/Account/LogOnOV'; 

        $(document).ready(function () {
            $('#UserName').focus().select();
            if (typeof sessionTimeout != 'undefined' && sessionTimeout != null)
                clearTimeout(sessionTimeout);

            $('.modal').click(function (e) {
                e.preventDefault();
                newModal($(this).attr("title"), $(this).attr("href"), 220, 500);
            });
        });
    </script>

    <% if (Model.RememberMe)
       { //Auto Logged in this will redirect after framebuster script runs %>

    <%--<script type="text/javascript">

        window.location.href = '/Overview';
    </script>--%>

    <% } %>

<div class="login_container">
    <div class="login_form_container">
        <div class="login_logo_container text-center">
            <%if (MonnitSession.CurrentBinaryStyle("Logo") != null && MonnitSession.CurrentBinaryStyle("Logo").Length > 0){%>
                <img class="siteLogo" src="/Overview/Logo" />
                <%}else{%>
                <img class="siteLogo" src="<%= Html.GetThemedContent("/images/Logo_TopBar.png")%>" />
                <%} %>
                <div id="siteLogo2"></div>
        </div>

        <div class="login_form">
            <div id="Form" class="login-form-container">
	        <div class="login_create_tabs">
		        <div class="login_tab current" data-tab="tab-1" >
			        <%: Html.TranslateTag("Account/LogOnOV|LOG IN","LOG IN", language)%>
		        </div>
                <%
                    // if account theme has create account set as true                    
                    bool createAvailable = MonnitSession.CurrentTheme.AllowAccountCreation;
                    if (createAvailable) {
                        %>
		        <div class="login_tab" data-tab="tab-2" >
                    <a href="/Overview/CreateAccountOV" class="login_create_tab" >
			        <%: Html.TranslateTag("Account/LogOnOV|CREATE ACCOUNT","CREATE ACCOUNT", language)%>
                    </a>
		        </div>
                <%} %>
	        </div>

                            <% using (Html.BeginForm())
                               { %>
                            <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                            <div id="loginForm" class="login_inputs col12">
                                <div id="loginFormInside" style="text-align: center;" class="col-12">
                                    <%string error = Html.TranslateTag("Account/LogOnOV|Login was unsuccessful. Try again","Login was unsuccessful. Try again", language);%>
                                    <%if (ViewData.ModelState.ContainsKey("Inactive") && !string.IsNullOrWhiteSpace(ViewBag.CustomerID))
                                      {     // Show Account Recovery button if User is Inactive %>
                                    <%: Html.ValidationMessage("Inactive") %>
                                    <div style="margin: 5px; align-self: flex-start">
                                        <a href="/Account/AccountRecovery/<%: ViewBag.CustomerID %>" type="button" class="btn btn-danger">Account Recovery</a>
                                    </div>
                                    <% }
                                      else
                                      {%>
                                    <%: Html.ValidationSummary(false, error) %>
                                    <% } %>

                                    <div class="editor-field" style="text-align: left;">
                                        <div class="editor-label" style="font-size: 14px; margin-bottom: 0px;">
                                        </div>
                                        <div class="editor-field" style="display:flex;">
                                            <input class="form-control" id="UserName" placeholder="<%: Html.TranslateTag("UserName","User Name", language)%>" name="UserName" type="text" value="<%: Model.UserName %>" autocomplete="off">
                                            <%if (MonnitSession.CurrentTheme.SupportsSaml) 
                                              { %>
                                                <button type="button" id="LogonCheck" class="btn btn-primary" style="margin-left: -8px;">
                                                    <%=Html.GetThemedSVG("arrowRight") %>                                        
                                                </button>
                                            <%} %>
                                        </div>
                 
                                        <div style="clear: both;"></div>
                                        <div>
                                            <%: Html.ValidationMessageFor(m => m.UserName) %>
                                        </div>
                                        <a href="/Account/ForgotUsername" title="Retrieve Username" tabindex="-1" class="modal"><%: Html.TranslateTag("Account/LogOnOV|Forgot UserName","Forgot user name?", language)%></a>
                                    </div>
                                    
                                    <div id="passwordLoginSection" <%: MonnitSession.CurrentTheme.SupportsSaml ? "style=display:none" : ""%>>
                                        <div class="editor-field" style="text-align: left;">
                                            <div class="editor-label" style="font-size: 14px; margin-top: 1em;">
                                            </div>
                                            <div class="editor-field d-flex" style="width:108%;">
                                                <input class="form-control" autocomplete="off" id="Password" placeholder="<%: Html.TranslateTag("Password","Password", language)%>" name="Password" type="password">
                                                   <svg id="pwd_hidden" class="pwd-mask" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 16 16" >
                                                <title> <%: Html.TranslateTag("Show Passwords") %> </title>
                                                <path d="M13.359 11.238C15.06 9.72 16 8 16 8s-3-5.5-8-5.5a7.028 7.028 0 0 0-2.79.588l.77.771A5.944 5.944 0 0 1 8 3.5c2.12 0 3.879 1.168 5.168 2.457A13.134 13.134 0 0 1 14.828 8c-.058.087-.122.183-.195.288-.335.48-.83 1.12-1.465 1.755-.165.165-.337.328-.517.486l.708.709z"/>
                                                <path d="M11.297 9.176a3.5 3.5 0 0 0-4.474-4.474l.823.823a2.5 2.5 0 0 1 2.829 2.829l.822.822zm-2.943 1.299.822.822a3.5 3.5 0 0 1-4.474-4.474l.823.823a2.5 2.5 0 0 0 2.829 2.829z"/>
                                                <path d="M3.35 5.47c-.18.16-.353.322-.518.487A13.134 13.134 0 0 0 1.172 8l.195.288c.335.48.83 1.12 1.465 1.755C4.121 11.332 5.881 12.5 8 12.5c.716 0 1.39-.133 2.02-.36l.77.772A7.029 7.029 0 0 1 8 13.5C3 13.5 0 8 0 8s.939-1.721 2.641-3.238l.708.709zm10.296 8.884-12-12 .708-.708 12 12-.708.708z"/>
                                            </svg>

                                            <svg id="pwd_visible" class="pwd-mask" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 16 16" >
                                                <title> <%: Html.TranslateTag("Hide Passwords") %> </title>
                                                <path d="M16 8s-3-5.5-8-5.5S0 8 0 8s3 5.5 8 5.5S16 8 16 8zM1.173 8a13.133 13.133 0 0 1 1.66-2.043C4.12 4.668 5.88 3.5 8 3.5c2.12 0 3.879 1.168 5.168 2.457A13.133 13.133 0 0 1 14.828 8c-.058.087-.122.183-.195.288-.335.48-.83 1.12-1.465 1.755C11.879 11.332 10.119 12.5 8 12.5c-2.12 0-3.879-1.168-5.168-2.457A13.134 13.134 0 0 1 1.172 8z"/>
                                                <path d="M8 5.5a2.5 2.5 0 1 0 0 5 2.5 2.5 0 0 0 0-5zM4.5 8a3.5 3.5 0 1 1 7 0 3.5 3.5 0 0 1-7 0z"/>
                                            </svg>
                                            </div>
                                         
                                            <div style="clear: both;"></div>
                                            <div>
                                                <%: Html.ValidationMessageFor(m => m.Password) %>
                                            </div>
                                            <a href="/Account/ForgotPassword" title="Retrieve Password" tabindex="-1" class="modal"><%: Html.TranslateTag("Account/LogOnOV|Forgot Password","Forgot password?", language)%></a>
                                        </div>

                                        <div class="col-12 d-flex align-items-center justify-content-between">
                                            <div class="d-flex">
                                                <%: Html.CheckBoxFor(m => m.RememberMe, new {style = "cursor:pointer;"}) %>
                                                <p class="ms-1 mb-0">
                                                    <label for="RememberMe" style="cursor:pointer;">
                                                        <%: Html.TranslateTag("Account/LogOnOV|Remember Me","Remember Me", language)%>
                                                    </label>
                                                </p>
                                            </div>
                                            <p class=" mb-0">
                                                <a href="/Account/LogOnForgot"><%: Html.TranslateTag("Account/LogOnOV|Forgot?","Forgot?", language)%></a>
                                            </p>
                                        </div>
                                        <%if (!ViewData.ModelState.IsValid)
                                          { %>
                                        <div>
                                            <a href="/Account/ForgotPassword" title="Retrieve Password" class="modal"><%: Html.TranslateTag("Account/LogOnOV|Retrieve Password","Retrieve Password", language)%></a>
                                        </div>
                                        <%} %>

                                        <div class="col-12 loginBtn__container" style="clear: both; margin-top: 20px; align-content: center; vertical-align: middle!important;">
                                            <input type="submit" onclick="attemptSubmit(this)" value="<%: Html.TranslateTag("Account/LogOnOV|Login","Login", language)%>" class="btn btn-primary gradButton" style="width:100%" />
                                            <button class="btn btn-primary" id="loadingSubmit" type="submit" disabled style="width: 100%; display: none;">
                                                <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                                <%: Html.TranslateTag("Account/LogOnOV|Logging In...","Logging In...")%>
                                            </button>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-1 col-md-2"></div>
                        <% } %>
                    </div>

	        <div class="login_message">
		        <label id="login_lbl"></label>
	        </div>
            <%if (ViewData.ModelState.IsValid)
                    { %>
                <div class="row">
                    <div class="col-12">
                        
                        <%if (!MonnitSession.IsEnterprise && MonnitSession.CurrentTheme.AllowPWA && MonnitSession.CurrentStyle("MobileAppName").Length > 0 && MonnitSession.CurrentStyles["MobileAppLogo"].BinaryValue.Length > 0)
                        {%>
                            <div id="installAppLink" style="display: none; text-align: center;">
                                
                                <a href="/Setup/InstallApp/" class="btn btn-secondary">
                                    <div class="">
                                        <%=Html.GetThemedSVG("download-file") %>
                                        <span class="">
                                            <%: Html.TranslateTag("Shared/TopBar|Install App", "Install App") %>
                                        </span>
                                    </div>
                                </a>
                            </div>

                            <script>
                                //var platform = {};

                                //function checkPlatform() {
                                //    // browser info and capability
                                //    var _ua = window.navigator.userAgent;

                                //    //platform.isIDevice = (/iphone|ipod|ipad/i).test(_ua);
                                //    platform.isIDevice = /iphone|ipod|ipad/i.test(_ua);
                                //    platform.isSamsung = /Samsung/i.test(_ua);
                                //    platform.isFireFox = /Firefox/i.test(_ua);
                                //    platform.isOpera = /opr/i.test(_ua);
                                //    platform.isEdge = /edg/i.test(_ua);

                                //    // Opera & FireFox only Trigger on Android
                                //    if (platform.isFireFox) {
                                //        platform.isFireFox = /android/i.test(_ua);
                                //    }

                                //    if (platform.isOpera) {
                                //        platform.isOpera = /android/i.test(_ua);
                                //    }

                                //    platform.isChromium = ("onbeforeinstallprompt" in window);
                                //    platform.isInWebAppiOS = (window.navigator.standalone === true);
                                //    platform.isInWebAppChrome = (window.matchMedia('(display-mode: standalone)').matches);
                                //    platform.isMobileSafari = platform.isIDevice && _ua.indexOf('Safari') > -1 && _ua.indexOf('CriOS') < 0;
                                //    platform.isStandalone = platform.isInWebAppiOS || platform.isInWebAppChrome;
                                //    platform.isiPad = (platform.isMobileSafari && _ua.indexOf('iPad') > -1);
                                //    platform.isiPhone = (platform.isMobileSafari && _ua.indexOf('iPad') === -1);
                                //    platform.isCompatible = (platform.isChromium || platform.isMobileSafari ||
                                //        platform.isSamsung || platform.isFireFox || platform.isOpera);
                                //}
                                //checkPlatform();

                                $(function () {

                                    if (platform.isStandalone) {
                                        //User is running the PWA App so don't show them the link to install it (already installed if it is running)
                                        $('#installAppLink').attr('disabled', 'disabled');
                                        $('#installAppLink').hide();
                                        return;

                                    } else if (platform.isCompatible) {
                                        //Device can install PWA's show them the link
                                        $('#installAppLink').show();
                                        return;

                                    } else {
                                        //Check if IOS running in "Desktop" mode  
                                        let isIOSinDesktopMode = (/iPad|iPhone|iPod/.test(navigator.platform) ||
                                            (navigator.platform === 'MacIntel' && navigator.maxTouchPoints > 1)) &&
                                            !window.MSStream

                                        if (isIOSinDesktopMode) {
                                            //They can't install as is, but we hav instructions on the install page that may allow them to
                                            //So allow them to get to the install page so they can see those instructions.
                                            $('#installAppLink').show();
                                            return;
                                        }
                                    }

                                    //If nothing else indicates they can use PWA disable it
                                    $('#installAppLink').attr('disabled', 'disabled');
                                    $('#installAppLink').hide();

                                });
                            </script>
                        <%} %>
                    </div>
                </div>
                <%} %>
            <div style="margin-top: 20px;" >
                <select class="form-select" onchange="switchLanguages(this.value)">
                    <%foreach (Language lang in Language.LoadActive())
                        { %>
                    <option value="<%=lang.Name %>" <%= language.ToLower() == lang.Name.ToLower() ? "selected='selected'" : "" %>><%=lang.DisplayName %></option>
                    <%} %>
                </select>
            </div>
            <div >
                v<%: ViewContext.Controller.GetType().Assembly.GetName().Version.ToString() %>
            </div>
        </div>
    </div>
    <div class="login_image">       
        <img src="<%= Html.GetThemedContent("/images/login-dashPhone.png")%>" style="width:100%;"/>
    </div>
</div>

    <script type="text/javascript">

        $(document).ready(function () {
            $('#pwd_hidden').css('display', 'inline');
            $('#pwd_visible').css('display', 'none');

            var pwString = "<%= Html.TranslateTag("Password","Password", language)%>";
            var unString = "<%= Html.TranslateTag("UserName","User Name", language).Replace("'","&quot;")%>";
            $('#Password').attr("placeholder", pwString);
            $('#UserName').attr("placeholder", unString);
            $('#UserName').focus().select();

            $('#Forgot').click(function (e) {
                e.preventDefault();

                newModal("", $(this).attr("href"), 200, 300);
            });

            $('.login_tab').click(function () {
                var tab_id = $(this).attr('data-tab');

                $('.login_tab').removeClass('current');
                $('.tab-content').removeClass('current');

                $(this).addClass('current');
                $("#" + tab_id).addClass('current');
                $('#login_lbl').html(' ');
                toggleType();
            });


            <%if (MonnitSession.CurrentTheme.SupportsSaml)
              {%>
                $('#UserName').keydown(function (e) {
                    if (e.keyCode === 13) {
                        e.preventDefault();

                        $("#LogonCheck").click();
                    }
                });

                $('#LogonCheck').click(function () {
                    var username = $('#UserName').val();
                    var returnUrl = '<%:returnUrl%>';

                    $.post('/Account/PrelogonOV/', { username: username, returnURL: returnUrl }, function (data) {
                        if (data == "ShowPassword") {
                            $('#LogonCheck').hide();
                            $('#passwordLoginSection').show();
                            $('#Password').select().focus();
                        } else {
                            window.location.href = data;
                        }
                    });
                });
            <%}%>

            $('.pwd-mask').click(() => {
                if ($('#Password').attr('type') === 'password') {
                    $('#Password').get(0).setAttribute('type', 'text');
                    $('#pwd_hidden').css('display', 'none');
                    $('#pwd_visible').css('display', 'inline');
                } else {
                    $('#Password').get(0).setAttribute('type', 'password');
                    $('#pwd_hidden').css('display', 'inline');
                    $('#pwd_visible').css('display', 'none');
                }
            });
        });

        function switchLanguages(languageName) {

            var old_url = window.location.href;
            var new_url = old_url.substring(0, old_url.indexOf('?'));

            window.location.href = new_url + "?language=" + languageName;
        }

        function toggleType() {
            var x = document.getElementById("password");
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
            $('#password').val('');
        }

        function attemptSubmit(submitButton) {
            if ($(submitButton).closest('form')[0].reportValidity()) {
                $(submitButton).hide();
                $('#loadingSubmit').show();
            }
        }

    </script>

</asp:Content>