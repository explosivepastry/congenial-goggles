<%@  Page Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" Inherits="System.Web.Mvc.ViewPage<Customer>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Account Unlock
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <meta name="viewport" content="width=device-width, user-scalable=no" />
    <div class="login_container">
        <div class="login_form_container">
            <div class="login_logo_container text-center">
                <%if (MonnitSession.CurrentBinaryStyle("Logo") != null && MonnitSession.CurrentBinaryStyle("Logo").Length > 0){%>
                <img class="siteLogo" src="/Overview/Logo" />
                <%}else{%>
                <img class="siteLogo" src="<%= Html.GetThemedContent("/images/Logo_TopBar.png")%>" />
                <%} %>
                <div id="siteLogo2"></div>
                <%if(Model != null && !Model.isLocked()) { %>
                <h3>Account Unlocked!</h3>
                <button class="col-xs-12 col-md-12 col-lg-12 input_def loginBtn__container__btn col-xs-12 col-md-12 col-lg-12" onclick="loginPage()">
                    <%: Html.TranslateTag("Account/LogOnOV|Login","Login")%>
                </button>
                <%} else { %>
                <h3>Failed to unlock account. Check the link and try again.</h3>
                <%} %>
            </div>
        </div>
        <div class="login_image">
            <img src="<%= Html.GetThemedContent("/images/login-dashPhone.png")%>" style="width: 100%;" />
        </div>

    </div>

    <script>
        function loginPage() {
            window.location.href = '/';
        }
    </script>

    <style>
        .login_logo_container {
            display: flex;
            flex-direction: column;
            align-items: center;
        }

        h3 {
            margin: 20px 0;
        }

        @media (max-width: 930px) {
            .login_image {
                display: none;
            }

            .login_form_container {
                width: 100vw;
            }
        }
    </style>
</asp:Content>
