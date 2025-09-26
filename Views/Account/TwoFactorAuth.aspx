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

            <!-- 2FA Method -->
            <div class="methodCards login_form">

                <h4>Two-Factor Authentication</h4>

                <p style="max-width: 380px; margin-top: 0; line-height: 25px; font-weight: 600;">To keep your account secure, please choose a security method to receive your verification code.</p>
                <%if (!Model.NotificationPhone.Equals("") && !Model.DirectSMS && ViewBag.smtp)
                    { %>
                <div class="card stay shadow-sm rounded d-flex flex-row align-items-center" id="SMS" onclick="SMS = true">
                    <div class="cardIcon">
                        <svg xmlns="http://www.w3.org/2000/svg" width="47.995" height="47.995" viewBox="0 0 47.995 47.995">
                            <path fill="none" stroke="#666" d="M47.5,33.163a4.777,4.777,0,0,1-4.777,4.777H14.054L4.5,47.5V9.277A4.777,4.777,0,0,1,9.277,4.5H42.718A4.777,4.777,0,0,1,47.5,9.277Z" transform="translate(-2 -2)" stroke-linecap="round" stroke-linejoin="round" stroke-width="5" />
                        </svg>

                    </div>
                    <div class="cardBody">
                        <p class="my-auto">SMS Text Message</p>
                    </div>
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" width="15" class="rightArrow">
                        <path fill="#666" d="M190.5 66.9l22.2-22.2c9.4-9.4 24.6-9.4 33.9 0L441 239c9.4 9.4 9.4 24.6 0 33.9L246.6 467.3c-9.4 9.4-24.6 9.4-33.9 0l-22.2-22.2c-9.5-9.5-9.3-25 .4-34.3L311.4 296H24c-13.3 0-24-10.7-24-24v-32c0-13.3 10.7-24 24-24h287.4L190.9 101.2c-9.8-9.3-10-24.8-.4-34.3z" />
                    </svg>
                </div>
                <% } %>

                <%if (ViewBag.smtp)
                    { %>
                <div class="card stay shadow-sm rounded d-flex flex-row align-items-center" id="email">
                    <div class="cardIcon">
                        <svg xmlns="http://www.w3.org/2000/svg" width="52.577" height="35.491" viewBox="0 0 52.577 35.491">
                            <path fill="#666" d="M.072,36.415V7.211q0-.051.152-.963l17.188,14.7L.275,37.429a4.3,4.3,0,0,1-.2-1.014ZM2.354,4.22a2.185,2.185,0,0,1,.862-.152h46.29a2.871,2.871,0,0,1,.913.152L33.18,18.974,30.9,20.8l-4.512,3.7-4.512-3.7-2.282-1.825ZM2.4,39.407,19.693,22.827l6.693,5.425,6.693-5.425L50.367,39.407a2.434,2.434,0,0,1-.862.152H3.215a2.3,2.3,0,0,1-.811-.152ZM35.36,20.951,52.5,6.248a3.026,3.026,0,0,1,.152.963v29.2a3.886,3.886,0,0,1-.152,1.014Z" transform="translate(-0.072 -4.068)" />
                        </svg>
                    </div>
                    <div class="cardBody">
                        <p class="my-auto">Email Message</p>
                            <%if(UnsubscribedNotificationEmail.EmailIsUnsubscribed(Model.NotificationEmail)) {%>
                            <span style="color:red;">Clicking will opt you into notifications</span>
                            <%}%>
                    </div>
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" width="15" class="rightArrow">
                        <path fill="#666" d="M190.5 66.9l22.2-22.2c9.4-9.4 24.6-9.4 33.9 0L441 239c9.4 9.4 9.4 24.6 0 33.9L246.6 467.3c-9.4 9.4-24.6 9.4-33.9 0l-22.2-22.2c-9.5-9.5-9.3-25 .4-34.3L311.4 296H24c-13.3 0-24-10.7-24-24v-32c0-13.3 10.7-24 24-24h287.4L190.9 101.2c-9.8-9.3-10-24.8-.4-34.3z" />
                    </svg>
                </div>
                <% } %>
                <% if (Model.TOTPSecret != "")
                    { %>
                <div class="card next shadow-sm rounded d-flex flex-row align-items-center" id="TOTP">
                    <div class="cardIcon">
                        <svg xmlns="http://www.w3.org/2000/svg" width="39.242" height="50.784" viewBox="0 0 39.242 50.784">
                            <path fill="#666" d="M40.625,1.5H17.542a4.63,4.63,0,0,0-4.617,4.617v6.925h4.617V8.425H40.625V45.359H17.542V40.742H12.925v6.925a4.63,4.63,0,0,0,4.617,4.617H40.625a4.63,4.63,0,0,0,4.617-4.617V6.117A4.63,4.63,0,0,0,40.625,1.5ZM21.7,24.584V21.121c0-3.232-3.232-5.771-6.463-5.771S8.77,17.889,8.77,21.121v3.463A2.979,2.979,0,0,0,6,27.354v8.079a3.022,3.022,0,0,0,2.77,3h12.7a3.022,3.022,0,0,0,3-2.77V27.584A3.022,3.022,0,0,0,21.7,24.584Zm-3,0H11.771V21.121c0-1.847,1.616-3,3.463-3s3.463,1.154,3.463,3Z" transform="translate(-6 -1.5)" />
                        </svg>

                    </div>
                    <div class="cardBody">
                        <p class="my-auto">Use Authenticator App</p>
                    </div>
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" width="15" class="rightArrow">
                        <path fill="#666" d="M190.5 66.9l22.2-22.2c9.4-9.4 24.6-9.4 33.9 0L441 239c9.4 9.4 9.4 24.6 0 33.9L246.6 467.3c-9.4 9.4-24.6 9.4-33.9 0l-22.2-22.2c-9.5-9.5-9.3-25 .4-34.3L311.4 296H24c-13.3 0-24-10.7-24-24v-32c0-13.3 10.7-24 24-24h287.4L190.9 101.2c-9.8-9.3-10-24.8-.4-34.3z" />
                    </svg>
                </div>
                <% } %>
            </div>

            <!-- 2FA Email and SMS Form -->
            <div id="Form" class="nonTOTPForm login-form-container login_form" style="display: flex; flex-direction: column; align-content: center!important;">
                <form action="/Account/VerifyTwoFactorAuthCode" method="post"  id="tfaModel">
                    <h4>Two-Factor Authentication</h4>
                        <% if (ViewBag.SMSError != null)
                            { %>
                        <div class="alert alert-danger" role="alert">
                          <%: ViewBag.SMSError %>
                        </div>
                        <% } %>
                    <p class="email">An email with your confirmation code has been sent to <%=emailResult %>. Please provide the code below.</p>
                    <p class="SMS">An SMS message with your confirmation code has been sent to <%=phoneResult %>. Please provide the code below.</p>
                    <div class="center">
                        <input name="code" class="form-control mx-auto" style="width: 306px;" placeholder="verification code" id="code" required="required" type="number" />
                        <input name="returnUrl" hidden value="<%=ViewBag.returnUrl %>" />
                        <input name="sms" class="sms" hidden />
                    </div>

                    <div class="form-group has-error">
                        <span id="codeError" class="help-block"><%=ViewBag.error != null ? ViewBag.error : "" %></span>
                    </div>
                    <p class="email">If an email message does not arrive within 5 minutes, check your spam folder.</p>

                    <div class="form-group center">
                        <button id="validateCode" onclick="verifyCode(this)" class="btn btn-primary" style="width: 306px;">
                            Verify Code
                        </button>
                        <button class="btn btn-primary" id="verify" type="submit" disabled style="width: 306px; display: none;">
                            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                            Verifying Code...
                        </button>
                        <div class="login_tab" data-tab="tab-1" style="margin-top: 10px;">
                            <a href="/Account/ChangeTwoFactorMethod?returnUrl=<%=ViewBag.returnUrl %>" class="btn btn-light" style="width: 306px;">Choose Another Method</a>
                        </div>
                        <p style="margin-top: 10px;"><strong>This device will be remembered for 90 days.</strong></p>
                    </div>

                </form>
            </div>
            
            <!-- 2FA TOTP Form -->
            <div class="TOTPForm login-form-container login_form" style="display: flex; flex-direction: column;">
                <form action="/Account/VerifyTwoFactorAuthCode" method="post"  id="totpForm">
                    <h4>Two-Factor Authentication</h4>

                    <div class="step">
                        <h3>Enter the verification code</h3>
                    </div>

                    <p>Enter the 6-digit verification code generated by your authenticator application.</p>

                    <div class="center">
                        <input id="totpCode" class="form-control mx-auto" name="Code" placeholder="verification code" style="width: 306px;" required="required" type="number" />
                        <input hidden name="returnUrl" value="<%=ViewBag.returnUrl%>" />
                        <input hidden name="totp" value="true" />
                    </div>

                    <div class="form-group has-error" style="margin-left: 20px;">
                        <span id="totpCodeError" class="help-block"><%=ViewBag.error != null ? ViewBag.error : "" %></span>
                    </div>

                    <div class="form-group center">
                        <button id="totpSubmit" onclick="verifyTotpBtn(this)" class="btn btn-primary" style="width: 306px;">
                            Verify Code
                        </button>
                        <button class="btn btn-primary" id="totpVerify" type="submit" disabled style="width: 306px; display: none;">
                            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                            Verifying Code...
                        </button>
                            <div style="margin-top: 10px;">
                                <a href="/Account/ChangeTwoFactorMethod?returnUrl=<%=ViewBag.returnUrl %>" class="btn btn-light mx-0" style="width: 306px;">Choose Another Method</a>
                            </div>
                    </div>

                    <p style="text-align: center;"><strong>This device will be remembered for 90 days.</strong></p>
                </form>
            </div>

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
        let error = "<%= ViewBag.type != null ? ViewBag.type : "" %>";

        $('.detailsMobile').hide();

        function openDetails() {
            let screenWidth = $(window).width();
            $('.detailsMobile').animate({ height: `${screenWidth > 680 ? '200px' : '40vh'}`, bottom: '0', left: '0' }, 500);
            $('.detailsMobile').show();
        }

        function closeDetails() {
            $('.detailsMobile').animate({ height: '0', bottom: '0', left: '0', display: 'none' }, 500);
        }

        //for sms
        if (error === "sms") {
            $('.methodCards').hide();
            $('.tfaDetails').hide();
            $('.SMS').show();
            $('.email').hide();
            $('.TOTPForm').hide();
            $('.sms').val(true);
            $('#codeError').html('Code Invalid');
        }
        //for email
        else if (error === "email") {
            $('.methodCards').hide();
            $('.tfaDetails').hide();
            $('.SMS').hide();
            $('.email').show();
            $('.TOTPForm').hide();
            $('.sms').val(false);
            $('#codeError').html('Code Invalid');
        }
        else {
            $('.notRemembered').hide();
            $('.nonTOTPForm').hide();
            $('.SMS').hide();
            $('.email').hide();
            $('.TOTPForm').hide();
        }

        //for selecting sms or email
        $(".stay").click(function (event) {
            var id = $(this).attr('id');
            $.ajax({
                url: '/Account/TwoFactorAuthResponse',
                data: { type: id, returnUrl: "<%=ViewBag.returnUrl%>", sms: SMS },
            }).then(e => {
                if (e == 'Success') {
                    $('.methodCards').hide();
                    $('.tfaDetails').hide();
                    $('.nonTOTPForm').show();
                    console.log('sms', SMS);
                    SMS ? $('.SMS').show() : $('.email').show();
                    SMS ? $('.sms').val(true) : $('.sms').val(false);
                    $("#code").focus();
                }
                else {
                    $('.methodCards').hide();
                }
            })
        })

        //for selecting totp as method
        $(".next").click(function (event) {
            var id = $(this).attr('id');
            $.ajax({
                url: '/Account/TwoFactorAuthResponse',
                data: { type: id, returnUrl: "<%=ViewBag.returnUrl%>", sms: SMS },
            }).then(e => {
                console.log('response',e);
                if (e) {
                    $('.methodCards').hide();
                    $('.tfaDetails').hide();
                    $('.TOTPForm').show();
                    $('.totpForm').show();
                    $("#totpCode").focus();
                }
                //else if (e == 'False') {
                //    $('.methodCards').hide();
                //    $('.notRemembered').show();
                //}
                else if (e == 'TOTPSetup') {
                    new QRCode(document.getElementById('qrCode'),
                        {
                            text: value.qr,
                            width: 120,
                            height: 120
                        });
                    $('.totpCode').html(value.qr);
                    $('.methodCards').hide();
                    $('.TOTPSetup').hide();
                }
                else {
                    $('.methodCards').hide();
                }
            })
        })

        //for validating SMS/Email
        $("#validateCode").click(function (e) {
            e.preventDefault();
            $.ajax({
                url: '/Account/VerifyTwoFactorAuthCode',
                type: 'post',
                data: $('#tfaModel').serialize(),
                success: function (e) {
                    if (e == "Success") {
                        window.location.href = 'FinalizeLogin?returnUrl=<%=ViewBag.returnUrl%>';
                    }
                    else if (e.includes('DOCTYPE html')) {
                        window.location.href = '/Overview'
                    }
                    else {
                        $('#codeError').html(e);
                        $('#validateCode').show();
                        $('#verify').hide();
                    }
                },
            })
        })

        function verifyCode(btn) {
            if ($('#code').val() == "") {
            }
            else {
                $(btn).hide();
                $('#verify').show();
            }
        }

        function verifyTotpBtn(btn) {
            if ($('#totpCode').val() == "") {
            }
            else {
                $(btn).hide();
                $('#totpVerify').show();
            }
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

        .card {
            height: 80px;
            width: 380px;
            display: flex;
            margin: 10px auto;
            cursor: pointer;
            border: .5px solid #eee;
        }

            .card:hover {
                box-shadow: 0 .5rem 1rem rgba(0,0,0,.11) !important;
            }

        .cardIcon {
            width: 100px;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .cardBody {
            padding-left: 20px;
            text-align: left;
            width: 280px;
        }

        .cardBody {
            margin-bottom: 0 !important;
        }

        .rightArrow {
            margin-right: 15px;
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

        #totpForm {
            width: 306px;
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

            form {
                margin-bottom: 120px;
            }

        }
    </style>

</asp:Content>
