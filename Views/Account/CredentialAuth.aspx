<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Login
     
</asp:Content>
<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">

    <%string language = "english";
        if (Request.QueryString["language"] != null)
            language = Request.QueryString["language"].ToString();

        string errorString = ViewBag.Error;
    %>


    <div class="container-fluid">
        <div class="row centered-form">
            <div class="col-1 col-md-2"></div>
            <div class="col-10 col-md-8 panel panel-primary panel-body" style="border: 0px; width: 300px; height: auto; margin-top: 0 auto; position: absolute; left: 50%; top: 50%; margin-left: -150px; margin-top: -225px; padding: 0px;">
                <br />
                <%if (MonnitSession.CurrentBinaryStyle("Logo") != null && MonnitSession.CurrentBinaryStyle("Logo").Length > 0){%>
                <img align="middle" style="display: block; margin-left: auto; margin-right: auto;" width="225px" height="auto" src="/Overview/Logo" />
                <%}else{%>
                <img align="middle" style="display: block; margin-left: auto; margin-right: auto;" width="225px" height="auto" src="<%= Html.GetThemedContent("/images/Logo_TopBar.png")%>" />
                <%} %>
                <br />
                <hr style="width: 40%; margin-bottom: 5px; margin-top: 10px;">
                <br />
                <h3 class="panel-title" style="text-align: center; font-weight: bold; font-size: 20px;"><%: Html.TranslateTag("Account/LogOnForgot|Credential Recovery","Credential Recovery", language)%></h3>
                <div class="panel-body" style="margin-bottom: -15px;">
                    <% string authKey = "";
                        if (ViewBag.authKey != null)
                            authKey = ViewBag.authKey;
                    %>
                    <div id="Form" class="container">
                        <div class="row">
                            <div class="col-12">
                                <div class="editor-label-small" style="width: 100%;">
                                    <form action="/Account/CredentialAuthCheck" id="Logon" method="post">
                                        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                                        <div class="col-1"></div>
                                        <div id="recoveryForm" class="col-12">
                                            <div id="loginFormInside">
                                                <div class="editor-field">
                                                    <div class="editor-label" style="font-size: 12px; font-weight: normal; margin-bottom: 0px;">
                                                        <%: Html.TranslateTag("Account/LogOnForgot|Recovery Authorization Key","Recovery Authorization Key", language)%>
                                                    </div>
                                                    <div class="editor-field">
                                                        <input id="RecoveryKey" name="RecoveryKey" type="text" value="<%=authKey %>">
                                                    </div>
                                                    <div style="clear: both;"></div>
                                                    <div id="ErrorDiv" style="color: red;">
                                                        <%=errorString %>
                                                    </div>
                                                </div>
                                                <div class="editor-field">
                                                    <div class="editor-label" style="font-size: 12px; font-weight: normal; margin-top: 1em; max-width: 100%">
                                                        <%: Html.TranslateTag("Account/LogOnForgot|Please enter your recovery authorization key. If you did not receive a recovery email, please click back and try again.", "Please enter your recovery authorization key. If you did not receive a recovery email, please click back and try again.", language) %>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="col-12">
                                                    <div class="form-group" style="text-align: center;">
                                                        <input type="submit" value="<%: Html.TranslateTag("Submit","Submit", language)%>" class="btn btn-danger btn-large" style="margin-left: 40px; margin-right: 40px; width: 60%; height: 40px; border-radius: 3px!important; font-size: 20px; font-weight: ; align-content: center; vertical-align: middle!important; background: linear-gradient(to bottom, #335c80 0%,#207cca 0%,#335c80 100%,#1e5799 100%); /* w3c, ie10+, ff16+, chrome26+, opera12+, safari7+ */ border-color: #335c80;" />
                                                    </div>
                                                </div>
                                                <div class="col-12">
                                                    <div class="form-group" style="text-align: center;">
                                                        <input type="button" onclick="goBack();" value="<%: Html.TranslateTag("Back","Back", language)%>" class="" style="margin-left: 40px; margin-right: 40px; width: 60%; background-color: transparent; border: none;" /><br />
                                                        <br />
                                                    </div>
                                                </div>
                                                <br />
                                                <br />
                                            </div>
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script>

        $(function () {




        });

        function goBack() {

            window.history.back();
        }

    </script>
</asp:Content>
