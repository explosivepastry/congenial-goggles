<%@  Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="ChangePasswordSuccessTitle" ContentPlaceHolderID="TitleContent" runat="server">
    ChangePasswordSuccess
</asp:Content>

<asp:Content ID="ChangePasswordSuccessContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="login_container">
        <div class="login_form_container">
            <div class="logo_container text-center">
                <%if (MonnitSession.CurrentBinaryStyle("Logo") != null && MonnitSession.CurrentBinaryStyle("Logo").Length > 0){%>
                <img class="siteLogo" src="/Overview/Logo" />
                <%}else{%>
                <img class="siteLogo" src="<%= Html.GetThemedContent("/images/Logo_TopBar.png")%>" />
                <%} %>
                <div id="siteLogo2"></div>
            </div>

            <div class="login_form">
                <div class="container">
                    <div class="row">
                        <h3 class="panel-title" style="text-align: center; font-weight: bold; font-size: 20px;"><%: Html.TranslateTag("Account/ChangePasswordSuccess|Credential Update","Credential Update")%></h3>
                        <h2 style="text-align: center;"><%: Html.TranslateTag("Account/ChangePasswordSuccess|Password Successfully Changed!","Password Successfully Changed!")%></h2>
                        <a role="button" style="width: 306px;" onclick="$(this).hide();$('#redirecting').show();" href="/overview" class="btn btn-primary mx-auto" /><%: Html.TranslateTag("Account/ChangePasswordSuccess|Go To Overview","Go To Overview")%> </a>
                        <button class="btn btn-primary mx-auto" id="redirecting" type="submit" disabled style="width: 306px; display: none;">
                            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                            <%: Html.TranslateTag("Account/ChangePasswordSuccess|Redirecting","Redirecting")%>...
                        </button>
                    </div>
                </div>

                <div class="login_message">
                    <label id="login_lbl"></label>
                </div>
            </div>
        </div>

        <div class="login_image">
            <img src="<%= Html.GetThemedContent("/images/login-dashPhone.png")%>" style="width: 100%;" />
        </div>
    </div>
</asp:Content>
