<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" Inherits="System.Web.Mvc.ViewPage<iMonnit.Models.LogOnForgotModel>" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Login
</asp:Content>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">

    <%string language = "english";
        if (Request.QueryString["language"] != null)
            language = Request.QueryString["language"].ToString();

        string errorString = ViewBag.Error;
    %>

    <div class="login_container">
        <div class="login_form_container">
            <div class="logo_container text-center">
                <%if (MonnitSession.CurrentBinaryStyle("Logo") != null && MonnitSession.CurrentBinaryStyle("Logo").Length > 0)
                    {%>
                <img class="siteLogo" src="/Overview/Logo" />
                <%}
                else
                {%>
                <img class="siteLogo" src="<%= Html.GetThemedContent("/images/Logo_TopBar.png")%>" />
                <%} %>
                <div id="siteLogo2"></div>

            </div>
            <div class="account_title_container">
                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="35" height="35" viewBox="0 0 35 35">
                    <image id="NoPath_-_Copy_9_" data-name="NoPath - Copy (9)" width="35" height="35" xlink:href="data:image/png;base64,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" />
                </svg>
                <div class="login_tab" style="font-size: 20px;"><%: Html.TranslateTag("Account/LogOnForgot|Password Recovery","Password Recovery", language)%></div>
            </div>

            <%if (string.IsNullOrEmpty(MonnitSession.CurrentTheme.SMTP) || MonnitSession.CurrentTheme.SMTP == "smtp.yourhostname.com")
                {
            %>

            <div class="account_title_container">
                <div class="login_tab" style="font-size: 20px;"><%: Html.TranslateTag("Please contact your admin to reset your password","Please contact your admin to reset your password", language)%></div>
            </div>

            <div class="col-12">
                <div class="row login_tab text-center">
                    <a onclick="goBack();" class="btn btn-light mx-auto" style="width: 306px;"><%=Html.TranslateTag("Cancel","Cancel", language)%></a>
                </div>
            </div>
            <%}
                else
                {
            %>

            <div style="min-height: 30px; text-align: center; font-size: 1.5em; white-space: pre" id="infoBox"></div>
            <div class="login_form">
                <%-- #region Code Form --%>
                <div id="codeFormDiv" class="container">
                    <div class="row credential_recovery_row" style="display: flex; flex-direction: column; text-align: center; justify-content: center;">
                        <form id="codeForm" action="/Account/LogOnForgot" method="post" class="recovery_form">
                            <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                            <div class="login_inputs">
                                <div>
                                    <div style="color: red; margin-top: 0; font-size: 1.3em" id="codeError"><%= errorString %></div>
                                    <div style="color: green; margin-top: 0; font-size: 1.3em" id="success"></div>
                                    <div class="editor-field" id="codeField">
                                        <div class="editor-field">
                                            <input class="form-control" id="VerificationCode" name="VerificationCode" autocomplete="off" style="width: 306px;" type="text" placeholder="<%: Html.TranslateTag("Account/LogOnForgot|Verification Code","Verification Code", language)%>">
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </form>
                        <div class="col-12 ">
                            <div class="form-group" style="align-content: center;">
                                <button class="btn btn-primary" style="width: 306px;" id="sendCodeBtn" onclick="sendCode()">
                                    <%: Html.TranslateTag("Set Password","Set Password", language)%>
                                </button>
                                <button class="btn btn-primary" id="sendingCodeBtn" type="submit" disabled style="width: 306px; display: none;">
                                    <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                    <%: Html.TranslateTag("Verifying Code...","Verifying Code...")%>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
                <%-- #endregion Code Form --%>

                <%--#region Email Form--%>
                <div id="emailFormDiv" class="container">
                    <div class="row credential_recovery_row" style="display: flex; flex-direction: column; text-align: center; justify-content: center;">
                        <form id="emailForm" action="/Account/CredentialLookup" method="post" class="recovery_form">
                            <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                            <div class="login_inputs">
                                <div id="loginFormInside">
                                    <div class="editor-field" id="emailField">
                                        <div class="editor-field">
                                            <input style="width: 306px;" class="form-control" id="EmailAddress" name="EmailAddress" type="text" value="" placeholder="<%: Html.TranslateTag("Account/LogOnForgot|Email Address","Email Address", language)%>" autocomplete="off">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </form>

                        <div class="col-12 ">
                            <div class="form-group" style="align-content: center;">
                                <button class="btn btn-primary" style="width: 306px;" onclick="sendEmail()" id="sendEmailBtn">
                                    <%: Html.TranslateTag("Send Email","Send Email", language)%>
                                </button>
                                <button class="btn btn-primary" id="sendingEmailBtn" type="submit" disabled style="width: 306px; display: none;">
                                    <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                   <%: Html.TranslateTag("Sending Email...","Sending Email...")%>
                                </button>
                            </div>
                        </div>

                        <div class="col-12 ">
                            <div class="form-group" style="align-content: center;">
                                <button id="alreadyHaveBtn" onclick="alreadyHave()" class="btn btn-light" style="width: 306px;">
                                    <%: Html.TranslateTag("Already have a recovery code?","Already have a recovery code?", language)%>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
                <%--#endregion Email Form--%>
                <div class="col-12">
                    <div class="row login_tab text-center">
                        <a onclick="cancelClicked();" class="btn btn-light mx-auto" style="width: 306px;"><%=Html.TranslateTag("Cancel","Cancel", language)%></a>
                    </div>
                </div>
            </div>

            <%} %>
            <div class="login_message">
                <label id="login_lbl"></label>
            </div>
            <div>
                <select class="form-select form-select-sm" onchange="switchLanguages(this.value)">
                    <%foreach (Language lang in Language.LoadActive())
                        { %>
                    <option value="<%=lang.Name %>" <%= language.ToLower() == lang.Name.ToLower() ? "selected='selected'" : "" %>><%=lang.DisplayName %></option>
                    <%} %>
                </select>
            </div>
        </div>
        <div class="login_image">
            <img src="<%= Html.GetThemedContent("/images/login-dashPhone.png")%>" style="width: 100%;" />
        </div>
    </div>
    
    <script type="text/javascript">

        $(document).ready(function () {
            $('#EmailAddress').keydown(function (e) {
                if (e.keyCode === 13) {
                    e.preventDefault();
                    sendEmail();
                }
            });

        <% if (Model == null || !Model.SendCodePage)
        {%> 
            // start at "send email"
            $('#emailFormDiv').show();
            $('#codeFormDiv').hide();
        <%}
        else
        { %>
            // start at "submit code"
            alreadyHave();
            if (codeError.length > 0)
                $('#infoBox').html(codeError).css("color", "red");
        <% } %>
        });

        function switchLanguages(languageName) {
            var old_url = window.location.href;
            var new_url = old_url.substring(0, old_url.indexOf('?'));
            window.location.href = new_url + "?language=" + languageName;
        }

        function cancelClicked() {
            window.location.href = "/"
        }

        function goBack() {
            window.history.back();
        }

        let codeError = "<%: String.Join("\n", ViewData.ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage)) %>";

        function alreadyHave() {
            $('#emailFormDiv').hide();
            $('#codeFormDiv').show();
        }

        $('#EmailAddress').focus(() => {
            $('#infoBox').html("");
            $('#codeError').html("");
        });
        $('#VerificationCode').focus(() => {
            $('#infoBox').html("");
            $('#codeError').html("");
        });

        // CredentialLookup
        function sendEmail() {
            $('#sendEmailBtn').hide();
            $('#sendingEmailBtn').show();
            $('#infoBox').html("");

            $.post('/Account/CredentialLookup/', { EmailAddress: $('#EmailAddress').val() }, function (data) {
                $('#sendEmailBtn').show();
                $('#sendingEmailBtn').hide();

                if (data == "Success") {
                    $('#infoBox').html("Email sent").css("color", "green");
                    alreadyHave();
                }
                else {
                    $('#sendEmailBtn').show();
                    $('#sendingEmailBtn').hide();
                    $('#infoBox').html(data).css("color", "red");
                }
            });
        }

        function sendCode() {
            $('#sendCodeBtn').hide();
            $('#sendingCodeBtn').show();
            $('#infoBox').html("");
            $('#codeForm').submit();
        }

    </script>

</asp:Content>
