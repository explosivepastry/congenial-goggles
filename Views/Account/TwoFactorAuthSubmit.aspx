<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Customer>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Login
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%
        string s = Model.NotificationEmail;
        string pattern = @"(?:(?:^|(?<=@))([^.@])|\G(?!\A))[^.@](?:([^.@])(?=[.@]))?";
        string substitution = @"$1_$2";
        RegexOptions options = RegexOptions.Multiline;

        Regex regex = new Regex(pattern, options);
        string emailResult = regex.Replace(s, substitution);


        string phoneResult = "";

        if (!Model.NotificationPhone.Equals(""))
        {
            string phone = Model.NotificationPhone;

            for (var i = 0; i < phone.Length - 2; i++)
            {
                phoneResult += "•";
            }
            if (phone.Length > 0)
                phoneResult += phone.Substring(phone.Length - 2);
        }

    %>

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

            <% if (Model.TFAMethod > 0 && Model.TFAMethod < 3 || ViewBag.SMSError != null)
                { %>
            <!-- 2FA Email and SMS Form -->
            <div id="Form" class="nonTOTPForm login-form-container login_form" style="display: flex; flex-direction: column; align-content: center!important;">
                <form action="/Account/VerifyTwoFactorAuthCode" method="post" id="tfaModel">
                    <h2>Two-Factor Authentication</h2>
                    <% if (ViewBag.SMSError != null)
                            { %>
                        <div class="alert alert-danger" role="alert">
                          <%: ViewBag.SMSError %>
                        </div>
                        <% } %>
                    <% if (Model.TFAMethod == 2 || ViewBag.SMSError != null)
                        { %>
                    <p class="email">An email with your confirmation code has been sent to <%=emailResult %>. Please provide the code below.</p>
                    <% }
                        else if (Model.TFAMethod == 1)
                        {%>
                    <p class="SMS">An SMS message with your confirmation code has been sent to <%=phoneResult %>. Please provide the code below.</p>
                    <% } %>
                    <div class="center">
                        <input name="code" class="form-control mx-auto" style="width:306px;" placeholder="verification code" id="code" required="required" type="number" />
                        <input name="returnUrl" hidden value="<%=ViewBag.returnUrl %>" />
                    </div>

                    <div class="form-group has-error">
                        <span id="codeError" class="help-block"><%=ViewBag.error != null ? ViewBag.error : "" %></span>
                    </div>

                    <%if (Model.TFAMethod == 2)
                        {%>
                    <p class="email">If an email message does not arrive within 5 minutes, check your spam folder.</p>
                    <% } %>

                    <div class="form-group center">
                        <button id="validateCode" onclick="$(this).hide();$('#verify').show();" class="btn btn-primary" style="width: 306px;">
                            Verify Code
                        </button>
                        <button class="btn btn-primary" id="verify" type="submit" disabled style="width: 306px; display: none;">
                            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                            Verifying Code...
                        </button>
                        <div class="login_tab" data-tab="tab-1" style="margin-top: 10px;">
                            <a href="/Account/ChangeTwoFactorMethod?returnUrl=<%=ViewBag.returnUrl %>" class="login_create_tab">Choose Another Method</a>
                        </div>
                        <p style="margin-top: 10px;"><strong>This device will be remembered for 90 days.</strong></p>
                    </div>

                </form>
            </div>
            <% } %>
            <% else if (Model.TFAMethod == 3 || !ViewBag.SMSError)
                { %>
            <!-- 2FA TOTP Form -->
            <div class="TOTPForm login-form-container login_form" style="display: flex; flex-direction: column;">
                <form action="/Account/VerifyTwoFactorAuthCode" method="post" id="totpForm">
                    <h2>Two-Factor Authentication</h2>

                    <div class="step">
                        <h3>Enter the verification code</h3>
                    </div>

                    <p class="">Enter the 6-digit verification code generated by your authenticator application.</p>

                    <div class="center">
                        <input id="totpCode" class="form-control mx-auto" style="width:306px;" name="Code" placeholder="verification code" required="required" type="number" />
                        <input hidden name="returnUrl" value="<%=ViewBag.returnUrl%>" />
                        <input hidden name="totp" value="true" />
                    </div>

                    <div class="form-group has-error" style="margin-left: 20px;">
                        <span id="totpCodeError" class="help-block"><%=ViewBag.error != null ? ViewBag.error : "" %></span>
                    </div>

                    <div class="form-group center">
                        <button id="totpSubmit" onclick="$(this).hide();$('#verifyTOTP').show();" class="btn btn-primary" style="width: 306px;">
                            Verify Code
                        </button>
                        <button class="btn btn-primary" id="verifyTOTP" type="submit" disabled style="width: 306px; display: none;">
                            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                            Verifying Code...
                        </button>
                        <div class="login_tab" data-tab="tab-1" style="margin-top: 10px;">
                            <a href="/Account/ChangeTwoFactorMethod?returnUrl=<%=ViewBag.returnUrl %>" class="login_create_tab">Choose Another Method</a>
                        </div>
                    </div>

                    <p style="text-align: center;"><strong>This device will be remembered for 90 days.</strong></p>
                </form>
            </div>
            <% } %>

            <div class="tfaDetailsMobile">
                <button class="btn btn-primary why2fa" onclick="openDetails()"><%= Html.TranslateTag("Why Two-Factor Auth?","Why Two-Factor Auth?")%></button>
            </div>

            <div class="detailsMobile">
                <button type="button" class="btn-close text-end" onclick="closeDetails()"></button>
                <br />
                <div>
                    <h3><%= Html.TranslateTag("Why is two-factor authentication (2FA) required?","Why is two-factor authentication (2FA) required?")%></h3>
                    <p><%= Html.TranslateTag("We recognize that stolen, reused and weak passwords remain a leading cause of security breaches. 2FA adds an extra layer of security to your account by requiring you to enter a verification code at login. This additional step along with your password reduces the risk of your account being taken over.","We recognize that stolen, reused and weak passwords remain a leading cause of security breaches. 2FA adds an extra layer of security to your account by requiring you to enter a verification code at login. This additional step along with your password reduces the risk of your account being taken over.")%></p>

                </div>
            </div>

        </div>

        <div class="login_image">
            <img src="<%= Html.GetThemedContent("/images/login-dashPhone.png")%>" style="width: 100%;" />
        </div>

    </div>

    <script type="text/javascript">

        let SMS = false;

        $('.detailsMobile').hide();

        function openDetails() {
            let screenWidth = $(window).width();
            $('.detailsMobile').animate({ height: `${screenWidth > 680 ? '200px' : '40vh'}`, bottom: '0', left: '0' }, 500);
            $('.detailsMobile').show();
        }

        function closeDetails() {
            $('.detailsMobile').animate({ height: '0', bottom: '0', left: '0', display: 'none' }, 500);
        }

    </script>

    <style>
        .login_container {
            position: relative;
        }

        .tfaDetails {
            position: absolute;
            bottom: 0;
            padding: 20px;
            background: #eee;
        }

        h2 {
            font-size: 30px;
        }

        .login_logo_container {
            padding-bottom: 20px;
        }

        input::-webkit-outer-spin-button,
        input::-webkit-inner-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        #codeError {
            margin-left: 22px;
        }

        input[type=number] {
            -moz-appearance: textfield;
        }

        form {
            width: 350px;
        }

            nonTOTPForm p, form h2 {
                text-align: center;
                width: 100%;
            }

        .step div {
            background: #0067ab;
            border-radius: 25px;
            color: white;
            width: 25px;
            height: 25px;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .step {
            margin-top: 20px;
        }

        .TOTPForm p {
            margin: 10px 0;
        }

        .totpCode {
            padding: 5px;
            background: #ddd;
            text-align: center;
            margin: 10px 0;
            width: 100%;
        }

        #TFA_QR {
            width: 140px;
            margin: 5px 0;
        }

        .center {
            text-align: center;
        }

        .tfaDetails {
            display: none;
        }

        .detailsMobile {
            width: 100%;
            position: fixed;
            height: 0px;
            background: white;
            z-index: 99;
            background: #eee;
            display: flex;
            flex-direction: column;
        }

        .detailsMobile div {
            display: flex;
            justify-content: center;
            align-items: center;
            flex-direction: column;
            padding: 20px;
        }

        .close {
            font-size: 45px;
            padding-right: 10px;
            float: none;
            position: absolute;
            top: 10px;
            right: 10px;
        }

        @media only screen and (min-width: 1050px) {
            .tfaDetails {
                padding: 10px;
            }

                .tfaDetails h3 {
                    font-size: 17px;
                }
        }
    </style>

</asp:Content>
