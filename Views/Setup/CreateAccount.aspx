<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.AccountVerification>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <%string language = "auto:en-US";
        if (Request.QueryString["language"] != null)
            language = Request.QueryString["language"].ToString();
            Model.IanaTimeZone = "America/New_York";
    %>

   <%-- <%Language lang = Language.Load((long)ViewBag.LanguageID);
                if (lang == null)
                lang = Language.Load(Language.EnglishID);%>--%>

    <% using (Html.BeginForm())
        {%>
    <%Response.Write(ViewData["Exception"]);%>
    <%: Html.ValidationSummary(true) %>
    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>

    <div class="login_container" style="padding: 0; position: relative;">
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
                <div id="Form" class="login-form-container" style="display: flex; flex-direction: column; align-content: center!important;">
                    <div class="login_create_tabs">
                        <div class="login_tab current" data-tab="tab-2">
                            <a href="/Setup/CreateAccount" class="login_create_tab"><%=Html.TranslateTag("CREATE ACCOUNT","CREATE ACCOUNT",language)%>
                            </a>
                        </div>
                    </div>

                    <div class="col-12">
                        <div class="form-group" style="text-align: center;">
                            <p id="enterCode" style="margin: 0; font-size: 18px; cursor: pointer;">
                                <%=Html.TranslateTag("Setup/CreateAccount|Skip to account verification?","Skip to account verification",language)%>
                            </p>
                        </div>
                    </div>
                    <div class="col-12 create_input" style="text-align: center;">
                        <%: Html.ValidationMessageFor(model => model.Name) %>
                    </div>
                    <div class="col-12 create_input">
                        <div class="editor-label-small">
                            <label><%=Html.TranslateTag("Email (Username)","Email (Username)",language)%></label>
                        </div>

                        <div>
                            <input class="form-control" placeholder="example@email.com" name="EmailAddress" id="EmailAddress" required="required" type="text" value="<%=Model.EmailAddress %>" />
                            
                        </div>

                        <div class="form-group has-error">
                            <span id="emailboxError" class="help-block"><%: Html.ValidationMessageFor(model => model.EmailAddress) %></span>
                        </div>
                    </div>

                    <div class="col-12 create_input">
                        <div class="editor-label-small">
                            <label><%=Html.TranslateTag("Password","Password",language)%></label>
                        </div>

                        <div>
                            <input class="form-control" id="Password" name="Password" placeholder="password" required="required" type="password" autocomplete="new-password" />
                        </div>
                        <div class="form-group has-error">
                            <span id="passwordError" class="help-block"><%: Html.ValidationMessageFor(model => model.Password) %></span>
                        </div>
                    </div>

                    <div class="col-6" style="width: 100%; display: flex; justify-content: center; align-items: center;">

                    <div class="" style="font-size: smaller; max-width: 300px;">
                            <%: Html.CheckBoxFor(model => model.EULA,new { required="required"})%>
                            <%=Html.TranslateTag("Overview/CreateAccount|I acknowledge that I have read and agree to the","&nbsp;I acknowledge I have read and agree to the ",language)%><a href="javascript:void(0);" data-bs-toggle="modal" data-bs-target=".pageHelp" class="text-info"> <%=Html.TranslateTag("Overview/CreateAccount|Terms and Conditions","Terms and Conditions",language)%></a>
                       </div>
                    </div>


        <div class="col-6">
            <div class="form-group has-error">
                <span class="help-block"><%: Html.ValidationMessageFor(model => model.EULA) %></span>
            </div>
        </div>

        <div class="col-xs-8">
            <div class="editor-label" style="font-size: 12px; font-weight: normal; width: 100%; padding: 0px 10px; margin-bottom: 0;">
                <p style="margin-bottom: 0; max-width: 300px;"><%: Html.TranslateTag("Account/LogOnForgot|After clicking Create Account, we will send a verification code to the email you provided.", "After clicking Create Account, we will send a verification code to the email you provided.", language) %></p>
            </div>
        </div>

<div class="row" style=" margin: 10px; text-align: center;">
<div class="form-group" style="text-align: center; width: 100%;">
<input type="hidden" id="IANA_timezone" name="IanaTimeZone" value="<%= Model.IanaTimeZone %>" />

<input type="submit" data-loading-text="creating" id="btnSubmit" value="<%=Html.TranslateTag("Setup/CreateAccount|Create Account","Create Account",language)%>" onclick="verifyForm()"; class="btn btn-primary" style="width:306px;" />
<button class="btn btn-primary" id="createSubmit" type="submit" disabled style="width: 306px; display: none;">
<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
<%:Html.TranslateTag ("Creating Account...")%>
</button>

<script type="text/javascript">

    function verifyForm(e) {
        e.preventDefault();
        if ($('#EmailAddress').val() == "" || $('#Password').val() == "") {
        }
        else {
            $(function () {
                $('[id*=btnSubmit]').on("click", function () {
                    $('[id*=createSubmit]').hide();
                    setTimeout(function () {
                        $('[id*=btnSubmit]').show();
                    }, 8000);
                });
            });
        }
    }
</script>

        <div class="col-12">
            <div class="row login_tab" style="align-content: center;">
                <a style="padding: 10px;" href="/" class="btn btn-light "><%=Html.TranslateTag("Cancel","Cancel",language)%></a>
            </div>
        </div>

        <div class="clear"></div>
        <br />

        <div class="col-12">
            <select onchange="switchLanguages(this.value)" class="form-select">
                <%foreach (Language lang in Language.LoadActive())
                    { %>
                <option value="<%=lang.Name %>" <%= language.ToLower() == lang.Name.ToLower() ? "selected='selected'" : "" %>><%=lang.DisplayName %></option>
                <%} %>
            </select>
       </div>
     </div>  
   </div>  
 </div>     

        </div>  
        </div>  

        <div class="login_image" style="z-index: 1;">
        <img src="<%= Html.GetThemedContent("/images/login-dashPhone.png")%>" style="width: 100%;" />
        </div>

        </div>   <%--login container--%>

    
<!-- Terms & Conditions modal -->
<div class="modal fade pageHelp" tabindex="-1" role="dialog" aria-hidden="true">
    <div class="modal-dialog modal-dialog-scrollable">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" title="Close window"></button>
            </div>    
            
            <h3 class="panel-title" style="text-align: center; font-weight: bold; font-size: 20px;"><%:Html.TranslateTag ("TERMS OF USE")%></h3>

            <div class="modal-body">
                <div class="panel-body">
                    <%Html.RenderPartial("../Overview/_TermsOfUse"); %>
                </div>
            </div>   

            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" title="Close window" data-bs-dismiss="modal"><%: Html.TranslateTag("Close","Close")%></button>
            </div>   
        </div> 
    </div>  
</div>  
<!-- End Terms & Conditions modal -->

    <style>
        .goodBox {
            border-color: lightgreen;
        }

        .dialog {
            z-index: 2000;
            position: fixed;
            width: 300px;
            background: white;
            box-shadow: 0 5px 15px rgb(0 0 0 / 50%);
            top: 50%;
            border: 1px solid rgba(0,0,0,.2);
            border-radius: 6px;
            outline: 0;
            left: 50%;
            transform: translate(-50%, -50%);
        }

        .dialog-title {
            font-size: 18px;
            line-height: 1.42857143;
            padding: 15px;
            display: flex;
            justify-content: space-between;
        }

            .dialog-title h4 {
                margin-bottom: 0;
            }

        .dialog-body {
            padding: 15px;
        }

        .show {
            display: block;
        }

/* add animation effects */
@-webkit-keyframes animatetop {
    from {top:-300px; opacity:0}
    to {top:0; opacity:1}
}

@keyframes animatetop {
    from {top:-300px; opacity:0}
    to {top:0; opacity:1}
}

</style>

<script type="text/javascript">

        function switchLanguages(languageName) {
            var old_url = window.location.href;
            var new_url = old_url.substring(0, old_url.indexOf('?'));
            window.location.href = new_url + "?language=" + languageName;
        }

        $('.dialog').hide();
        function openDialog() {
            $('.dialog').show();
        }

        function closeDialog() {
            $('.dialog').hide();
        }

        $(function () {
            var passwordString = '<%=Html.TranslateTag("Overview/CreateAccount|Password must contain at least:","Password must contain at least:",language)%>';
            var CaseString = "\n " + '<%=Html.TranslateTag("Overview/CreateAccount|1 uppercase and 1 lowercase letter","1 uppercase and 1 lowercase letter,",language)%>';
            var SpecialString = "\n " + '<%=Html.TranslateTag("Overview/CreateAccount|1 special character,","1 special character,",language)%>';
            var NumberString = "\n " + '<%=Html.TranslateTag("Overview/CreateAccount|1 number,","1 number,",language)%>';
            var LengthString = '<%=Html.TranslateTag("Overview/CreateAccount|characters.","characters.",language)%>';

            var accError = '<%=Html.TranslateTag("Overview/CreateAccount|Account name taken: Please choose another","Account name taken: Please choose another",language)%>';
            var unError = '<%=Html.TranslateTag("Overview/CreateAccount|Username taken: Please choose another","Username taken: Please choose another",language)%>';
            var emailError = '<%=Html.TranslateTag("Overview/CreateAccount|Invalid Email","Invalid Email: Please enter a valid email address",language)%>';

            var pwRequiredStrArray = '<%= MonnitUtil.PasswordRequirementsString(MonnitSession.CurrentTheme)%>';

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
                $('#emailboxError').html("");
                $('#emailbox').removeClass("goodBox");
                var email = $('#emailbox').val();

                $.post("/Overview/CheckEmailAddress", { emailAddress: email }, function (data) {
                    if (data != "True") {
                        $('#emailboxError').html(emailError)
                    } else {
                        $.post("/Overview/CheckUserName", { username: email }, function (data) {
                            if (data != "True") {
                                $('#emailboxError').html(unError)
                            } else {
                                $('#emailbox').addClass("goodBox");
                            }
                        });
                    }
                });
            });

            $('#passwordbox').change(function (e) {
                e.preventDefault();
                $('#passwordError').html("");
                $('#passwordbox').removeClass("goodBox");
                var pw = $('#passwordbox').val();

                $.post('/Overview/CheckPassword/', { password: pw }, function (data) {
                    if (data != "True") {
                        $('#passwordError').html(fullString);
                    } else {
                        $('#passwordbox').addClass("goodBox");
                    }
                });
            });

            (Intl.DateTimeFormat().resolvedOptions().timeZone);
            $('#IANA_timezone').val(Intl.DateTimeFormat().resolvedOptions().timeZone);
        });

        $("#enterCode").click(function () {
            window.location.href = "/Setup/ValidateEmail";
        });
            
</script>

    <% } %>
</asp:Content>






