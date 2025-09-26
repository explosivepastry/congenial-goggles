<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" Inherits="System.Web.Mvc.ViewPage<AccountVerification>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%string language = "english";
        if (Request.QueryString["language"] != null)
            language = Request.QueryString["language"].ToString();
        string code = "";
        if (Request.QueryString["code"] != null)
            code = Request.QueryString["code"].ToString();
    %>

    <% using (Html.BeginForm())
        {%>
    <%Response.Write(ViewData["Exception"]);%>
    <%: Html.ValidationSummary(true) %>

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
                        <div class="login_tab current" data-tab="tab-2">
                            <a href="#" class="login_create_tab"><%=Html.TranslateTag("VALIDATE EMAIL","VALIDATE EMAIL",language)%>
                            </a>
                        </div>
                    </div>

                    <div class="col-12 create_input">
                        <div>
                            <label><%=Html.TranslateTag("Please enter the verification code that was sent to your email.","Please enter the verification code that was sent to your email.",language)%></label>
                        </div>
                        <div>
                            <input style="width: 306px;" class="form-control" id="VerificationCode" name="VerificationCode" required="required" type="text" value="<%:code %>" />
                            <label><%=Html.TranslateTag("Not yet received your verification code, please click resend or check your spam.","Not yet received your verification code, please click resend or check your spam.",language)%></label>
                        </div>
                        <div class="form-group has-error">
                            <span id="codeError" class="help-block"><%: Html.ValidationMessageFor(model => model.VerificationCode) %></span>
                        </div>
                    </div>

                    <div class="row mt-2" style="width: 306px;">
                        <button type="submit" class="btn btn-primary w-100" onclick="verifyCode(this)">
                            <%=Html.TranslateTag("Setup/CreateAccount|Verify Code","Verify Code",language)%>
                        </button>
                        <button class="btn btn-primary" id="verify" type="submit" disabled style="width: 306px; display: none;">
                            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                            <%:Html.TranslateTag ("Verifying...")%>
                        </button>

                        <button class="btn btn-secondary mt-2 w-100" data-bs-toggle="modal" data-bs-target="#resendCodeModal">
                            <%=Html.TranslateTag("Setup/CreateAccount|Resend Code","Resend Code",language)%>
                        </button>
                        
                    </div>
                    <div class="login_tab" data-tab="tab-1">
                        <a href="/Account/LogonOV/" class="btn btn-light mt-2"><%=Html.TranslateTag("Back","Back",language)%>
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <div class="login_image">
            <img src="../../Content/images/login-dashPhone.png" style="width: 100%;" />
        </div>
        <div class="modal fade" id="resendCodeModal" tabindex="-1" aria-labelledby="resendCodeModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="resendCodeModalLabel">Resend Code</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <%:Html.TranslateTag ("Please enter your email address below.")%>
                        <input class="form-control" name="emailAddress" id="emailAddress" />
                        <p id="noEmailErr"></p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal"><%:Html.TranslateTag ("Close")%></button>
                        <button type="button" class="btn btn-primary" onclick="resend(this)"><%:Html.TranslateTag ("Request code")%></button>

                        <button class="btn btn-secondary" id="send" disabled>
                            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                            <%:Html.TranslateTag ("Sending...")%>
                        </button>

                        <button class="btn btn-success" disabled="disabled" id="sent">
                            <%:Html.TranslateTag ("Sent")%> 
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <style>
        .goodBox {
            border-color: lightgreen;
        }
    </style>

    <script>

        $('#send').hide();
        $('#sent').hide();

        function switchLanguages(languageName) {

            var old_url = window.location.href;
            var new_url = old_url.substring(0, old_url.indexOf('?'));

            window.location.href = new_url + "?language=" + languageName;
        }

        function verifyCode(btn) {
            if ($('#VerificationCode').val() == "") {
            }
            else {
                $(btn).hide();
                $('#verify').show();
            }
        }

        function resend(btn) {
            let email = $('#emailAddress').val();
            if (email == "") {
                $('#noEmailErr').text('Email required');
            }
            else {
                $(btn).hide();
                $('#send').show();
                $.post('/Setup/ResendCode/', { email: email }, function (data) {
                    if (data = "Sent") {
                        $('#send').hide();
                        $('#sent').show();
                        setTimeout(function () {
                            $('#send').hide();
                            $('#sent').hide();
                            $('#emailAddress').val("");
                            $(btn).show();
                            $('#resendCodeModal').hide();
                        }, 5000);
                    } else {
                        $('#noEmailErr').text(data);
                    }
                });
            }
        }

        function close() {

        }

    </script>


    <% } %>
</asp:Content>
