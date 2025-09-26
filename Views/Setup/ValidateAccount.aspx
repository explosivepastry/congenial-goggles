<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%string language = "english";
        if (Request.QueryString["language"] != null)
            language = Request.QueryString["language"].ToString(); %>

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
                        <div class="login_tab" data-tab="tab-1">
                            <a href="#" onclick="window.history.back();" class="login_create_tab"><%=Html.TranslateTag("BACK","BACK",language)%>
                        </a>
                        </div>
                        <div class="login_tab current" data-tab="tab-2">
                            <a href="#" class="login_create_tab"><%=Html.TranslateTag("VALIDATE EMAIL","VALIDATE EMAIL",language)%>
                        </a>
                        </div>
                    </div>

                    <div class="col-12 create_input">
                        <div class="editor-label-small">
                            <label><%=Html.TranslateTag("Validation Code","Validation Code",language)%></label>
                            
                        </div>
                        <div>
                            <input class="input_def login_input" id="code" name="Code" required="required" type="text" value="" />
                        </div>
                        <div class="form-group has-error">
                            <span id="codeError" class="help-block"></span>
                        </div>
                    </div>
              
                    

                    <div class="row" style="width: 300px; margin: 10px; align-content: center;">
                        <div class="form-group" style="align-content: center; width: 100%;">
                            <input type="submit" value="<%=Html.TranslateTag("Setup/CreateAccount|Next","Next",language)%>" class="input_def login_button login_tab_content" />
                            
                            <div class="clear"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="login_image">
            <img src="../../Content/images/login-dashPhone.png" style="width: 100%;" />
        </div>
    </div>
    <style>
        .goodBox {
            border-color: lightgreen;
        }
    </style>

    <script>

        function switchLanguages(languageName) {

            var old_url = window.location.href;
            var new_url = old_url.substring(0, old_url.indexOf('?'));

            window.location.href = new_url + "?language=" + languageName;

        }

        $(function () {

            
            var codeError = '<%=Html.TranslateTag("Overview/CreateAccount|Account name taken: Please choose another","Account name taken: Please choose another",language)%>';
            
            $('#code').change(function (e) {
                e.preventDefault();
                $('#codeError').html("");
                $('#code').removeClass("goodBox");
                var code = $('#code').val()

                $.post("/Overview/CheckAccountNumber", { accountnumber: code }, function (data) {
                    if (data != "True") {
                        $('#codeError').html(codeError)
                    } else {
                        $('#code').addClass("goodBox");
                    }

                });

            });

        });
        
    </script>


    <% } %>

</asp:Content>
