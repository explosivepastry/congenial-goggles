<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<iMonnit.Models.ChangePasswordModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    User Settings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <div class="col-12">
            <%Html.RenderPartial("UserLink", MonnitSession.CurrentCustomer); %>
        </div>
        <div class="display-flex">
            <div class="col-12">
                <div class="rule-card_container w-100" >
                    <div class="x_title">
                        <h2><%: Html.TranslateTag("Settings/UserChangePassword|Change Password","Change Password")%></h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <form method="post" action="/Settings/UserChangePassword" class="form-horizontal form-label-left">
                            <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                              <div class="ms-2 mb-4">
                                <div class="bold col-12">
                                    <%: Html.TranslateTag("Settings/UserChangePassword|New passwords are required to be a minimum of","New passwords are required to be a minimum of")%> <%: ViewData["PasswordLength"]%> <%: Html.TranslateTag("Settings/UserChangePassword|characters in length.","characters in length.")%>
                                </div>

                                <div class="col-12">
                                    <%: Html.ValidationSummary(true, Html.TranslateTag("Settings/UserChangePassword|Password change was unsuccessful. Please correct the errors and try again.","Password change was unsuccessful. Please correct the errors and try again.")) %>
                                </div>
                            </div>

                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3 col-lg-2">
                                    <%: Html.LabelFor(m => m.OldPassword, Html.TranslateTag("Settings/UserChangePassword|Old Password","Old Password")) %>:
                                </div>

                                <div class="col sensorEditFormInput">
                                    <div class="form-group">
                                        <input autocomplete="off" id="OldPassword" name="OldPassword" type="password" class="form-control" />
                                    </div>

                                    <div class="editor-error" style="color: red;">
                                        <%: Html.ValidationMessageFor(m => m.OldPassword) %>
                                    </div>
                                </div>
                            </div>

                            <div class="row sensorEditForm">
                                <div class=" col-12 col-md-3 col-lg-2">
                                    <%: Html.LabelFor(m => m.NewPassword, Html.TranslateTag("Settings/UserChangePassword|New Password","New Password")) %>:
                                    
                                </div>
                                <div class="col sensorEditFormInput">
                                    <div class="form-group">
                                        <input autocomplete="off" id="NewPassword" name="NewPassword" type="password" class="form-control" />
                                    </div>

                                    <svg id="pwd_hidden" class="pwd-mask2" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 16 16">
                                        <title> <%: Html.TranslateTag("Settings/UserChangePassword|Show Passwords","Show Passwords")%> </title>
                                        <path d="M13.359 11.238C15.06 9.72 16 8 16 8s-3-5.5-8-5.5a7.028 7.028 0 0 0-2.79.588l.77.771A5.944 5.944 0 0 1 8 3.5c2.12 0 3.879 1.168 5.168 2.457A13.134 13.134 0 0 1 14.828 8c-.058.087-.122.183-.195.288-.335.48-.83 1.12-1.465 1.755-.165.165-.337.328-.517.486l.708.709z" />
                                        <path d="M11.297 9.176a3.5 3.5 0 0 0-4.474-4.474l.823.823a2.5 2.5 0 0 1 2.829 2.829l.822.822zm-2.943 1.299.822.822a3.5 3.5 0 0 1-4.474-4.474l.823.823a2.5 2.5 0 0 0 2.829 2.829z" />
                                        <path d="M3.35 5.47c-.18.16-.353.322-.518.487A13.134 13.134 0 0 0 1.172 8l.195.288c.335.48.83 1.12 1.465 1.755C4.121 11.332 5.881 12.5 8 12.5c.716 0 1.39-.133 2.02-.36l.77.772A7.029 7.029 0 0 1 8 13.5C3 13.5 0 8 0 8s.939-1.721 2.641-3.238l.708.709zm10.296 8.884-12-12 .708-.708 12 12-.708.708z" />
                                    </svg>

                                    <svg id="pwd_visible" class="pwd-mask2" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 16 16"
                                        <title> <%: Html.TranslateTag("Settings/UserChangePassword|Hide Passwords","Hide Passwords")%> </title>
                                        <path d="M16 8s-3-5.5-8-5.5S0 8 0 8s3 5.5 8 5.5S16 8 16 8zM1.173 8a13.133 13.133 0 0 1 1.66-2.043C4.12 4.668 5.88 3.5 8 3.5c2.12 0 3.879 1.168 5.168 2.457A13.133 13.133 0 0 1 14.828 8c-.058.087-.122.183-.195.288-.335.48-.83 1.12-1.465 1.755C11.879 11.332 10.119 12.5 8 12.5c-2.12 0-3.879-1.168-5.168-2.457A13.134 13.134 0 0 1 1.172 8z" />
                                        <path d="M8 5.5a2.5 2.5 0 1 0 0 5 2.5 2.5 0 0 0 0-5zM4.5 8a3.5 3.5 0 1 1 7 0 3.5 3.5 0 0 1-7 0z" />
                                    </svg>

                                    <div class="editor-error" style="color: red;">
                                        <%: Html.ValidationMessageFor(m => m.NewPassword) %>
                                    </div>
                                </div>
                            </div>

                            <div class="row sensorEditForm">
                                <div class=" col-12 col-md-3 col-lg-2">
                                    <%: Html.LabelFor(m => m.ConfirmPassword, Html.TranslateTag("Settings/UserChangePassword|Confirm Password","Confirm Password")) %>:
                                </div>
                            
                                <div class="col sensorEditFormInput">
                                    <div class="form-group">
                                        <input autocomplete="off" id="ConfirmPassword" name="ConfirmPassword" type="password"/>
                                    </div>

                                    <div style="position:relative; right: 35px;">
                                        <svg id="pwd_nomatch" xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="red" viewBox="0 0 16 16">
                                        <title> "Confirm Password" does not match "New Password" </title>
                                        <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zM5.354 4.646a.5.5 0 1 0-.708.708L7.293 8l-2.647 2.646a.5.5 0 0 0 .708.708L8 8.707l2.646 2.647a.5.5 0 0 0 .708-.708L8.707 8l2.647-2.646a.5.5 0 0 0-.708-.708L8 7.293 5.354 4.646z" />
                                    </svg>

                                    <%--<%: Html.TranslateTag("Confirm Password matches New Password") %>--%>
                                    <svg id="pwd_match" xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="green" class="bi bi-check-circle-fill" viewBox="0 0 16 16" hidden>
                                        <title>"Confirm Password" matches "New Password"</title>
                                        <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zm-3.97-3.03a.75.75 0 0 0-1.08.022L7.477 9.417 5.384 7.323a.75.75 0 0 0-1.06 1.06L6.97 11.03a.75.75 0 0 0 1.079-.02l3.992-4.99a.75.75 0 0 0-.01-1.05z" />
                                    </svg>
                                        </div>

                                    <div class="editor-error" style="color: red;">
                                       <%: Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                                    </div>
                                </div>
                            </div>

                            <div class="row sensorEditForm">
                                <div class="col-12 col-sm-3">
                                </div>

                                <div class="col-12 d-flex justify-content-end">
                                    <input onclick="goBack();" value="<%: Html.TranslateTag("Settings/UserChangePassword|Cancel","Cancel")%>" class="btn" style="width:90px;" />
                                    <input type="submit" value="<%: Html.TranslateTag("Settings/UserChangePassword|Update","Update")%>" id="updateBtn" onclick="showLoading()" class="btn btn-primary" />
                                    <button class="btn btn-primary" id="update" style="display: none;" type="button" disabled>
                                        <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                       <%: Html.TranslateTag("Settings/UserChangePassword|Update...","Update...")%>
                                    </button>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $('#pwd_hidden').removeAttr('hidden');
        $('#pwd_visible').attr('hidden', '');

        $('#ConfirmPassword').addClass('form-control');
        function goBack() {
            window.history.back();
        }

        function showLoading() {
            $('#update').show();
            $('#updateBtn').hide();
        }

        const oldPassword = $('#OldPassword');
        const newPassword = $("#NewPassword");
        const confirmPassword = $("#ConfirmPassword");
        const passwordNoMatch = $("#pwd_nomatch");
        const passwordMatch = $("#pwd_match");

        confirmPassword.on('keyup', function (e) {
            if (newPassword.val() == confirmPassword.val()) {
                passwordMatch.removeAttr('hidden');
                passwordNoMatch.attr('hidden', '');
            }
            else {
                passwordMatch.attr('hidden', '');
                passwordNoMatch.removeAttr('hidden');
            }
        });

        $('.pwd-mask2').click(() => {
            if (newPassword.attr('type') === 'password') {
                oldPassword.get(0).setAttribute('type', 'text');
                newPassword.get(0).setAttribute('type', 'text');
                confirmPassword.get(0).setAttribute('type', 'text');
                $('#pwd_hidden').attr('hidden', '');
                $('#pwd_visible').removeAttr('hidden');
            } else {
                oldPassword.get(0).setAttribute('type', 'password');
                newPassword.get(0).setAttribute('type', 'password');
                confirmPassword.get(0).setAttribute('type', 'password');
                $('#pwd_hidden').removeAttr('hidden');
                $('#pwd_visible').attr('hidden', '');
            }
        });

    </script>

    <style>
        .form-control {
            width: 300px;
        }
    </style>

</asp:Content>
