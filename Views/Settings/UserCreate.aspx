<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Customer>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    UserCreate
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <div class="col-md-6 col-12">
            <div class="rule-card_container w-100">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <%: Html.TranslateTag("Settings/UserCreate|New User","New User")%>
                    </div>
                </div>

                <div class="x_content">

                    <form id="userCreateForm" method="post" action="/Settings/UserCreate" class="form-horizontal form-label-left">
                        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>

                        <%: Html.ValidationSummary(true) %>

                        <%: Html.Hidden("AccountID", ViewData["AccountID"])%>

                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3"><%=Html.TranslateTag("First Name","First Name")%>:</div>
                            <div class="col sensorEditFormInput">
                                <input id="FirstName" name="FirstName" class="form-control" required="required" type="text" />
                            </div>
                        </div>
                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3"><%=Html.TranslateTag("Last Name","Last Name")%>:</div>
                            <div class="col sensorEditFormInput">
                                <%: Html.TextBoxFor(model => model.LastName,new { required="required", @class="form-control"})%>
                                <div class="editor-error">
                                    <%: Html.ValidationMessageFor(model => model.LastName) %>
                                </div>
                            </div>
                        </div>

                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3"><%=Html.TranslateTag("Email","Email")%>:</div>
                            <div class="col sensorEditFormInput">
                                <%: Html.TextBoxFor(model => model.NotificationEmail,new {id="emailbox", required="required", @class="form-control"}) %>
                                <div class="editor-error">
                                    <%: Html.ValidationMessageFor(model => model.NotificationEmail) %>
                                </div>
                            </div>
                        </div>

                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3"><%=Html.TranslateTag("Login Name","Login Name")%>:</div>
                            <div class="col sensorEditFormInput">
                                <%: Html.TextBoxFor(model => model.UserName,new {id="username", required="required", @class="form-control", @autocomplete="new-password"}) %>
                                <div id="usernameError" class="editor-error">
                                    <%: Html.ValidationMessageFor(model => model.UserName) %>
                                </div>
                            </div>
                        </div>

                       <% if (ViewData["SamlEndpointID"] != null && ViewData["SamlEndpointID"].ToLong() > 0)
                          { %>
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3"><%=Html.TranslateTag("SamlNameID", ViewData["SamlEndpointName"] + " Name ID")%>: </div>
                                <div class="col sensorEditFormInput">
                                    <input name="SamlNameID" class="form-control" type="text">
                                </div>
                                <div class="editor-error has-error">
                                    <span class="help-block"><%: Html.ValidationMessageFor(model => model.SamlNameID) %></span>
                                </div>
                            </div>
                        <%}
                          else 
                          {%>
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3"><%=Html.TranslateTag("Password","Password")%>: </div>
                                <div class="col sensorEditFormInput">
                                    <input autocomplete="new-password" id="passwordbox" name="Password" required="required" class="form-control" type="password">
                                </div>
                                <div class="editor-error has-error">
                                    <span id="passwordError" class="help-block"><%: Html.ValidationMessageFor(model => model.Password) %></span>
                                </div>
                            </div>
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3"><%=Html.TranslateTag("Confirm Password","Confirm Password")%>: </div>
                                <div class="col sensorEditFormInput">
                                    <input autocomplete="new-password" id="ConfirmPassword" name="ConfirmPassword" class="form-control" required="required" type="password">
                                </div>
                                <div class="editor-error has-error">
                                    <span class="help-block"><%: Html.ValidationMessageFor(model => model.ConfirmPassword) %></span>
                                </div>
                            </div>
                        <%}%>

                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3"><%: Html.TranslateTag("Settings/UserCreate|Is Administrator","Is Administrator")%>: </div>
                            <div class="checkbox-wrapper-65 col sensorEditFormInput">

                                   <label for="IsAdmin">                                    
                                    <input type="checkbox" id="IsAdmin" name="IsAdmin"  <%=Model.IsAdmin ? "checked=checked" : ""%> value="true">                            
                                    <span class="cbx">
                                        <svg width="12px" height="11px" viewBox="0 0 12 11">
                                            <polyline points="1 6.29411765 4.5 10 11 1"></polyline>
                                        </svg>
                                                <input name="IsAdmin" type="hidden" value="false">
                                    </span>                             
                                </label>

       <%--                         <%: Html.CheckBoxFor(model => model.IsAdmin)%>--%>
                            </div>
                            <div class="editor-error">
                                <%: Html.ValidationMessageFor(model => model.IsAdmin)%>
                            </div>
                        </div>
                        <div class="text-end col-12">
                            <button onclick="goBack();" value="<%: Html.TranslateTag("Cancel","Cancel")%>" class="btn-secondary btn">
                                <%: Html.TranslateTag("Cancel","Cancel")%>
                            </button>
                            <button type="submit" value="<%: Html.TranslateTag("Submit","Submit")%>" class="btn btn-primary">
                                <%: Html.TranslateTag("Submit","Submit")%>
                            </button>
                            <button class="btn btn-primary" id="saving" style="display: none;" type="button" disabled>
                                <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                Adding...
                            </button>
                        </div>
                    </form>
                    <div style="clear: both;"></div>
                </div>
            </div>
        </div>
    </div>
    <script>
        <%= ExtensionMethods.LabelPartialIfDebug("Settings_UserCreate.aspx")  %>

        $(function () {

            var passwordString = "<%=Html.TranslateTag("Overview/CreateAccount|Password must contain at least:","Password must contain at least:")%>";
            var CaseString = "\n " + "<%=Html.TranslateTag("Overview/CreateAccount|1 uppercase and 1 lowercase letter","1 uppercase and 1 lowercase letter,")%>";
            var SpecialString = "\n " + "<%=Html.TranslateTag("Overview/CreateAccount|1 special character,","1 special character,")%>";
            var NumberString = "\n " + "<%=Html.TranslateTag("Overview/CreateAccount|1 number,","1 number,")%>";
            var LengthString = "<%=Html.TranslateTag("Overview/CreateAccount|characters.","characters.")%>";

            var accError = "<%=Html.TranslateTag("Overview/CreateAccount|Account name taken: Please choose another","Account name taken: Please choose another")%>";
            var unError = "<%=Html.TranslateTag("Overview/CreateAccount|Username taken: Please choose another","Username taken: Please choose another")%>";

            var pwRequiredStrArray = "<%= MonnitUtil.PasswordRequirementsString(MonnitSession.CurrentTheme)%>";

            if (pwRequiredStrArray.split("|")[0] == 'False') {
                CaseString = "";
            }
            if (pwRequiredStrArray.split("|")[1] == 'False') {
                SpecialString = "";
            }
            if (pwRequiredStrArray.split("|")[2] == 'False') {
                NumberString = "";
            }

            var fullString = passwordString + ": " + CaseString + SpecialString + NumberString + pwRequiredStrArray.split("|")[3] + " " + LengthString;


            $('#emailbox').change(function (e) {
                e.preventDefault();
                var email = $('#emailbox').val();

                $.post("/Overview/CheckUserName", { username: email }, function (data) {
                    if (data == "True") {
                        $('#usernameError').html("");
                        $('#username').val(email);
                    }
                });
            });


            $('#passwordbox').change(function (e) {
                e.preventDefault();
                var pw = $('#passwordbox').val();


                $.post("/Overview/CheckPassword/", { password: pw }, function (data) {
                    if (data != "True") {
                        $('#passwordError').html(fullString);
                    } else {
                        $('#passwordError').html("");
                    }
                });
            });

            let usernameTimeout;
            $('#username').change(
                function (e) {
                    clearTimeout(usernameTimeout);
                    usernameTimeout = setTimeout(usernameChange, 2000, e);
                }
            );
            $('#username').on('keyup',
                function (e) {
                    clearTimeout(usernameTimeout);
                    usernameTimeout = setTimeout(usernameChange, 2000, e);
                }
            );

            function usernameChange (e) {

                e.preventDefault();
                var name = $('#username').val();
                $('#usernameError').html("");

                if (!name) {
                    return;
                }

                $.post("/Overview/CheckUserName", { username: name }, function (data) {
                    if (data != "True") {
                        $('#usernameError').html(unError)
                    } else {
                        $('#username').addClass("goodBox");
                    }
                });
            }
            });

        $("#userCreateForm button[type='submit']").click(
            function () {
                if ($("#userCreateForm input:invalid").length == 0) {
                    $(this).hide();
                    $("#saving").show();
                }
            }
        );

        function goBack() {
            window.history.back();
        }

    </script>

</asp:Content>
