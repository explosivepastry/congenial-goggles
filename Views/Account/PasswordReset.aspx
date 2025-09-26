<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" Inherits="System.Web.Mvc.ViewPage<iMonnit.Models.ForgotCredentialModel>" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Login
</asp:Content>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <style type="text/css">
        .validation-summary-errors ul { 
            list-style: disc; 
            text-align: left;
        }
    </style>

    <%
        string language = "english";
        if (Request.QueryString["language"] != null)
            language = Request.QueryString["language"].ToString();

        string Username = ViewBag.Username;
        string Email = ViewBag.Email;
        string VerificationCode = ViewBag.Verification;
    %>

    <div class="login_container">
        <div class="login_form_container">
            <div class="logo_container text-center">
                <%if (MonnitSession.CurrentBinaryStyle("Logo") != null && MonnitSession.CurrentBinaryStyle("Logo").Length > 0) {%>
                <img class="siteLogo" src="/Overview/Logo" />
                <%} else {%>
                <img class="siteLogo" src="<%= Html.GetThemedContent("/images/Logo_TopBar.png")%>" />
                <%} %>
                <div id="siteLogo2"></div>
            </div>

            <div class="account_title_container">
                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="35" height="35" viewBox="0 0 35 35">
                    <image id="NoPath_-_Copy_9_" data-name="NoPath - Copy (9)" width="35" height="35" xlink:href="data:image/png;base64,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" />
                </svg>
                <div class="login_tab" style="font-size: 20px;"><%: Html.TranslateTag("Account/LogOnForgot|Password Reset","Password Reset", language)%></div>
            </div>

             <div class="login_form">
                <div id="Form" class="container">
                    <div class="row credential_recovery_row" style="display: flex; flex-direction: column; text-align: center; justify-content: center;">
                        <form id="lookup" action="/Account/PasswordReset/?VerificationCode=<%= VerificationCode%>" method="post" class="recovery_form">
                            <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                            <div id="recoveryForm" class="login_inputs">
                                <div id="loginFormInside">
                                    <div class="editor-field">
                                        <div class="editor-label" style="font-size: 12px; font-weight: normal; margin-bottom: 0px;">
                                            <%: Html.TranslateTag("User Name","User Name", language)%>: <%= Username%>
                                        </div>
                                        <br />

                                        <div class="editor-field"></div>
                                        <div style="clear: both;"></div>
                                    </div>
                                    <div class="editor-field">
                                        <div class="editor-field">
                                            <%: Html.PasswordFor(m => m.NewPassword, new {@placeholder = "New Password", @class = "form-control", @style = "width: 306px" }) %>
                                        </div>
                                        <div>
                                            <%: Html.ValidationMessageFor(m => m.NewPassword) %>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="editor-field">
                                        <div class="editor-field">
                                            <%: Html.PasswordFor(m => m.ConfirmPassword, new {@placeholder = "Confirm Password", @class = "form-control", @style = "width: 306px" }) %>
                                        </div>
                                        <div>
                                            <%: Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                                        </div>
                                    </div>
                                    <div class="editor-field">
                                        <div class="editor-label" style="font-size: 12px; font-weight: normal;">
                                            <%= Html.ValidationSummary() %>
                                        </div>
                                    </div>

                                        <div class="form-group">
                                            <button class="btn btn-primary" type="submit" onclick="verifyForm(this)" style="width: 306px;">
                                                <%: Html.TranslateTag("Update Password","Update Password", language)%>
                                            </button>
                                            <button class="btn btn-primary" id="updatingPassword" type="submit" disabled style="width: 306px; display: none;">
                                                <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                                <%: Html.TranslateTag("Updating Password...","Updating Password...")%>
                                            </button>
                                        </div>
                                   
                                    <div class="col-12">
                                        <div class="row login_tab" style="align-content: center;">
                                            <a onclick="goBack();" class="btn btn-light" style="width: 306px;"><%=Html.TranslateTag("Cancel","Cancel",language)%></a>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <input type="hidden" name="Username" value="<%= Username%>"  />
                            <input type="hidden" name="EmailAddress" value="<%= Email%>" />
                        </form>
                    </div>
                </div>

                <div class="login_message">
                    <label id="login_lbl"></label>
                </div>
                <div>
                    <select class="form-select form-select-sm" onchange="switchLanguages(this.value)">
                        <%foreach (Language lang in Language.LoadActive())
                            { %>
                        <option value="<%=lang.Name %>" <%= language.ToLower() == lang.Name.ToLower() ? "selected='selected'" : "" %>><%=lang.DisplayName%></option>
                        <%} %>
                    </select>
                </div>
            </div>
        </div>
        <div class="login_image">
            <img src="<%= Html.GetThemedContent("/images/login-dashPhone.png")%>" style="width: 100%;" />
        </div>
    </div>

    <script type="text/javascript">

        function verifyForm(btn) {
            if ($('#NewPassword').val() == "" || $('#ConfirmPassword').val() == "") {
            }
            else {
                $(btn).hide();
                $('#updatingPassword').show();
            }
        }

        function goBack() {
            window.history.back();
        }

        function goBacktoLogin() {
            window.location.href = '/';
        }

</script>

</asp:Content>
