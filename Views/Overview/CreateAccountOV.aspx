<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" Inherits="System.Web.Mvc.ViewPage<iMonnit.Models.CreateAccountOVModel>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <%string language = "english";
        if (Request.QueryString["language"] != null)
            language = Request.QueryString["language"].ToString(); %>

    <% using (Html.BeginForm())
        {%>
    <%Response.Write(ViewData["Exception"]);%>
    <%: Html.ValidationSummary(true) %>
    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>

    <%: Html.Hidden("MaxFailedLogins", MonnitSession.CurrentTheme.MaxFailedLogins)%>


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
                        <div class="login_tab" data-tab="tab-1">
                            <a href="/Account/LogonOV?ReturnUrl=/Overview" class="login_create_tab"><%: Html.TranslateTag("Overview/CreateAccountOV|LOG IN","LOG IN",language)%>
                            </a>
                        </div>
                        <div class="login_tab current" data-tab="tab-2">
                            <a href="/Overview/CreateAccountOV" class="login_create_tab"><%: Html.TranslateTag("Overview/CreateAccountOV|CREATE ACCOUNT","CREATE ACCOUNT",language)%>
                            </a>
                        </div>
                    </div>
                    <div class="col-12 create_input">
                        <div class="editor-label-small">
                            <label><%=Html.TranslateTag("Overview/CreateAccountOV|First Name","First Name")%></label>
                            <span class="required">*</span>
                        </div>
                        <div>
                            <input class="form-control w-100" id="FirstName" name="FirstName" required="required" type="text" value="<%=Model.FirstName %>" />
                        </div>
                        <div class="form-group has-error">
                            <span class="help-block"><%: Html.ValidationMessageFor(model => model.FirstName) %></span>
                        </div>
                    </div>
                    <div class="col-12 create_input">
                        <div class="editor-label-small">
                            <label><%=Html.TranslateTag("CreateAccountOV|Last Name","Last Name")%></label><span class="required">*</span>
                        </div>
                        <div>
                            <input class="form-control w-100" id="LastName" name="LastName" required="required" type="text" value="<%=Model.LastName %>" />
                        </div>
                        <div class="form-group has-error">
                            <span class="help-block"><%: Html.ValidationMessageFor(model => model.LastName) %></span>
                        </div>
                    </div>
                    <div class="col-12 create_input">
                        <div class="editor-label-small">
                            <label><%: Html.TranslateTag("Overview/CreateAccountOV|Email","Email",language)%></label><span class="required">*</span>
                        </div>
                        <div>
                            <input class="form-control w-100" id="emailbox" name="NotificationEmail" required="required" type="text" value="<%=Model.NotificationEmail %>" />
                        </div>
                        <div class="form-group has-error">
                            <span class="help-block"><%: Html.ValidationMessageFor(model => model.NotificationEmail) %></span>
                        </div>
                    </div>

                    <div class="col-12 create_input">
                        <div class="editor-label-small">
                            <label><%=Html.TranslateTag("Overview/CreateAccountOV|User Name","User Name",language)%></label>
                            <span class="required">*</span>
                        </div>

                        <div>
                            <input class="form-control w-100" id="username" name="userName" required="required" type="text" <%=Model.UserName %> autocomplete="new-password" />
                        </div>
                        <div class="form-group has-error">
                            <span id="usernameError" class="help-block"><%: Html.ValidationMessageFor(model => model.UserName) %></span>
                        </div>
                    </div>
                    <div class="col-12 create_input">
                        <div class="editor-label-small">
                            <label><%: Html.TranslateTag("Overview/CreateAccountOV|Password","Password",language)%></label><span class="required">*</span>
                        </div>
                        <div>
                           
                            <input class="form-control w-100" id="passwordbox" name="Password" required="required" type="password" autocomplete="new-password" />
                        </div>
                        <div class="form-group has-error">
                            <span id="passwordError" class="help-block"><%: Html.ValidationMessageFor(model => model.Password) %></span>
                        </div>
                    </div>
                    <div class="col-12 create_input">
                        <div class="editor-label-small">
                            <label><%: Html.TranslateTag("Overview/CreateAccountOV|Confirm Password","Confirm Password",language)%></label><span class="required">*</span>
                        </div>
                        <div>

                            <input class="form-control w-100" id="ConfirmPassword" name="ConfirmPassword" required="required" type="password" autocomplete="off" />
                        </div>
                        <div class="form-group has-error">
                            <span class="help-block"><%: Html.ValidationMessageFor(model => model.ConfirmPassword) %></span>
                        </div>
                    </div>

                    <div class="col-12 create_input">
                        <div class="editor-label-small">
                            <label><%=Html.TranslateTag("Overview/CreateAccountOV|Company Name","Company Name",language)%></label><span class="required">*</span>
                            <br />
                            <span style="font-size: smaller;">(<%: Html.TranslateTag("Overview/CreateAccountOV|Must be unique","Must be unique",language)%>)</span>
                        </div>
                        <div>

                            <input class="form-control w-100" id="companyName" name="CompanyName" required="required" type="text" value="<%=Model.CompanyName %>" />
                        </div>
                        <div class="form-group has-error">
                            <span id="companynameError" class="help-block"><%: Html.ValidationMessageFor(model => model.CompanyName) %></span>
                        </div>
                    </div>

                    <div class="col-12 create_input">
                        <%if (!ConfigData.AppSettings("IsEnterprise").ToBool() && !this.Request.IsSensorCertMobile())
                            {%>
                        <div class="editor-label-small">
                            <label><%=Html.TranslateTag("Overview/CreateAccountOV|Subscription Code","Subscription Code",language)%></label>
                            <br />
                            <span style="font-size: smaller;">(<%: Html.TranslateTag("Overview/CreateAccountOV|Free trial if left blank","Free Trial if left blank",language)%>)</span>
                        </div>
                        <div>
                            <%--<input type="hidden" name="SubscriptionCode" value="None" />--%>
                            <input class="form-control w-100" id="subscriptionCode" name="SubscriptionCode" type="text" />
                        </div>
                        <div class="form-group has-error">
                            <span id="subscriptionCodeError" class="help-block"><%: Html.ValidationMessageFor(model => model.SubscriptionCode) %></span>
                        </div>
                        <%}
                            else
                            {%>
                        <input type="hidden" name="SubscriptionCode" value="0" />
                        <%} %>
                    </div>
                    
                    <div class="col-12 create_input">
                        <div class="editor-label-small">
                            <label><%=Html.TranslateTag("Overview/CreateAccountOVTime Zone","Time Zone",language)%></label><span class="required">*</span>
                        </div>

                        <div style="width: 100%;">
                            <% string savedRegion = string.Empty;
                                if (Model.TimeZoneID > 0)
                                    savedRegion = Monnit.TimeZone.Load(Model.TimeZoneID).Region.ToString();
                            %>
                            <select required id="Regions" class="form-select w-100">
                                <option value="<%=Html.TranslateTag("Overview/CreateAccountOV|Choose a region","Choose a region",language)%>"><%=Html.TranslateTag("Overview/CreateAccountOV|Choose a region","Choose a region",language)%></option>
                                <%foreach (string region in Monnit.TimeZone.LoadRegions())
                                    {
                                %>
                                <option <%: savedRegion == region  ? "selected='selected'" : "" %> value="<%:region%>"><%=Html.TranslateTag(region,region,language)%></option>
                                <%}%>
                            </select>
                            <select class="form-select form-select-sm w-100" id="TimeZoneID" name="TimeZoneID">
                            </select>
                        </div>

                        <div class="form-group has-error">
                            <span class="help-block"><%: Html.ValidationMessageFor(model => model.TimeZoneID)%></span>
                        </div>
                        <% Html.RenderPartial("RegistrationVariablesOV", Model); %>
                        <div class="form-group has-error"></div>
                    </div>

                    <div class="col-6 col-sm-6 col-md-6 mt-2" style="width: 100%; display: flex; justify-content: center; align-items: center;">
                        <%if (MonnitSession.CurrentTheme.CurrentEULAVersion != Version.Parse("0.0.0.0"))
                            { %>
                        <div class="" style="font-size: smaller; max-width: 190px;">
                            <%: Html.CheckBoxFor(model => model.EULA,new { required="required"})%>
                            <%=Html.TranslateTag("Overview/CreateAccountOV|I acknowledge that I have read and agree to the","&nbsp;I acknowledge I have read and agree to the",language)%><a href="/Overview/Legal" class="text-info" target="_blank"> <%=Html.TranslateTag("Overview/CreateAccountOV|Terms and Conditions","Terms and Conditions",language)%></a>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group has-error">
                            <span class="help-block"><%: Html.ValidationMessageFor(model => model.EULA) %></span>
                        </div>
                        <%}
                            else
                            {%>
                        <div class="" style="font-size: smaller; max-width: 190px;">
                            <%: Html.CheckBoxFor(model => model.EULA,new { required="required"})%>
                            <%=Html.TranslateTag("Overview/CreateAccountOV|I acknowledge that I have read and agree to the","I acknowledge I have read and agree to the",language)%><a href="/Overview/Legal" class="text-info" target="_blank"> <%=Html.TranslateTag("Overview/CreateAccountOV|Terms and Conditions"," Terms and Conditions",language)%></a>
                        </div>
                        <input type="hidden" name="EULA" value="<%=Html.TranslateTag("Overview/CreateAccountOV|true","true")%>" />
                        <%} %>

                        <div style="font-size: smaller;">
                            <span class="required">*</span> <%=Html.TranslateTag("Overview/CreateAccountOV|Required information","Required information",language)%>
                        </div>
                    </div>
                    <div class="form">
                        <input type="submit" onclick="$(this).hide();$('#saving').show();" value="<%=Html.TranslateTag("Overview/CreateAccountOV|Next","Next",language)%>" class="btn btn-primary w-100 my-2" />
                        <button class="btn btn-primary w-100 my-2" id="saving" style="display: none;" type="button" disabled>
                            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                            <%=Html.TranslateTag("Overview/CreateAccountOV|Creating","Creating")%>...
                        </button>
                        <a href="/account/LogOnOV" class="btn btn-light mx-auto w-100"><%=Html.TranslateTag("Overview/CreateAccountOV|Cancel","Cancel",language)%></a>
                        <div class="clear"></div>
                        <div class="col-12">
                            <select onchange="switchLanguages(this.value)" class="form-select w-100 mt-2">
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
   
    <%if (!Request.Browser.IsMobileDevice)
		{%>
        <div class="login_image">
            <img src="../../Content/images/login-dashPhone.png" style="width: 100%;" />
        </div>
    <%} %>
         </div>
    <style type="text/css">
        .goodBox {
            border-color: lightgreen;
        }
    </style>

    <script type="text/javascript" >
        function switchLanguages(languageName) {
            var old_url = window.location.href;
            var new_url = old_url.substring(0, old_url.indexOf('?'));
            window.location.href = new_url + "?language=" + languageName;
        }

        $(function () {
            var passwordString = "<%=Html.TranslateTag("Overview/CreateAccountOV|Password must contain at least","Password must contain at least",language)%>:";
            var CaseString = "\n " + "<%=Html.TranslateTag("Overview/CreateAccountOV|1 uppercase and 1 lowercase letter","1 uppercase and 1 lowercase letter,",language)%>";
            var SpecialString = "\n " + "<%=Html.TranslateTag("Overview/CreateAccountOV|1 special character,","1 special character,",language)%>";
            var NumberString = "\n " + "<%=Html.TranslateTag("Overview/CreateAccountOV|1 number,","1 number,",language)%>";
            var LengthString = "<%=Html.TranslateTag("Overview/CreateAccountOV|characters.","characters.",language)%>";

            var accError = "<%=Html.TranslateTag("Overview/CreateAccountOV|Account name taken: Please choose another","Account name taken: Please choose another",language)%>";
            var unError = "<%=Html.TranslateTag("Overview/CreateAccountOV|Username taken: Please choose another","Username taken: Please choose another",language)%>";

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

                $.post('/Overview/CheckPassword/', { password: pw }, function (data) {
                    if (data != "True") {
                        $('#passwordError').html(fullString);
                    } else {
                        $('#passwordError').html("");
                    }
                });
            });

            $('#companyName').change(function (e) {
                $('#companynameError').html("");
                $('#companyName').removeClass("goodBox");
                e.preventDefault();
                var name = $('#companyName').val()

                $.post("/Overview/CheckAccountNumber", { accountnumber: name }, function (data) {
                    if (data != "True") {
                        $('#companynameError').html(accError)
                    } else {
                        $('#companyName').addClass("goodBox");
                    }
                });
            });

            $('#username').change(function (e) {
                $('#usernameError').html("");
                $('#username').removeClass("goodBox");
                e.preventDefault();
                var name = $('#username').val()
                $.post("/Overview/CheckUserName", { username: name }, function (data) {
                    if (data != "True") {
                        $('#usernameError').html(unError)
                    } else {
                        $('#username').addClass("goodBox");
                    }
                });
            });
        });

        $('#TimeZoneID').hide();
        var timeID = '<%=Model.TimeZoneID%>';
        if (timeID != '0') {
            getTimeZone($('#Regions').val(), timeID)
        }
        $('#Regions').change(function () {
            var region = $('#Regions').val();
            getTimeZone(region, 0)
        });

        function getTimeZone(region, timeID) {
            $.post('/Account/GetTimeZones/', { Region: region }, function (data) {

                $('#TimeZoneID').empty();
                $('#TimeZoneID').show();
                $.each(data, function (value) {
                    var splitvals = data[value].split("|");
                    var text = splitvals[1];
                    var selection = splitvals[0];
                    var opt = document.createElement('option');
                    opt.text = text;
                    opt.value = selection;

                    var tzselector = $('#TimeZoneID').get(0);
                    tzselector.add(opt, null);
                });
                if (timeID != 0) {
                    $('#TimeZoneID').val(timeID).change();
                }
            });
        }

    </script>
    <% } %>

</asp:Content>
