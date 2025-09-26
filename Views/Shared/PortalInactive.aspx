<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Portal Inactive
     
</asp:Content>
<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">

    <%string language = "english";
      if (Request.QueryString["language"] != null)
          language = Request.QueryString["language"].ToString(); %>
    <meta name="viewport" content="width=device-width, user-scalable=no" />
    
    <div class="container-fluid">

        <div class="row">
            <div class="col-xs-3 col-sm-3 col-md-3"></div>
            <div class="col-xs-6 col-sm-6 col-md-6 panel panel-primary panel-body" style="border: 0px; width: 300px; height: 425px; margin-top: 0 auto; position: absolute; left: 50%; top: 50%; margin-left: -150px; margin-top: -225px; padding: 0px;">
                <br />

                <div class="col-12" style="border: 0px;">


                    <div id="Form" class="container">
                        <%if (MonnitSession.CurrentBinaryStyle("Logo") != null && MonnitSession.CurrentBinaryStyle("Logo").Length > 0){%>
                        <img align="middle" style="display: block; margin-left: auto; margin-right: auto;" width="225px" height="auto" src="/Overview/Logo" />
                        <%}else{%>
                        <img align="middle" style="display: block; margin-left: auto; margin-right: auto;" width="225px" height="auto" src="<%= Html.GetThemedContent("/images/Logo_TopBar.png")%>" />
                        <%} %>
                        <!--							<span style="text-align: center"><h1 style="color:#c72127;font-size:13px;margin:0px;"><br>MONITORING AND CONNECTING<br>THE THINGS IN YOUR BUSINESS</h1></span>-->
                        <hr style="width: 40%; margin-bottom: 5px;">

                        <div class="row">
                            <div class="col-xs-1 col-sm-1 col-md-2"></div>
                            <div id="loginForm" class="col-xs-10 col-sm-10 col-md-8">
                                <h1 style="font-size: 20px; font-weight: bold; text-align: center;"><%: Html.TranslateTag("Portal Inactive","Portal Inactive", language)%></h1>

                            </div>
                            <div class="col-xs-1 col-sm-1 col-md-2"></div>

                        </div>
                    </div>

                    
                    <div class="row">
                        <div class="col-xs-4 col-sm-4 col-md-4"></div>
                        <div class="col-xs-4 col-sm-4 col-md-4" style="width: 33%; font-size: smaller;">
                            <span>
                                <select style="width: 100%;" onchange="switchLanguages(this.value)">
                                    <%foreach (Language lang in Language.LoadActive())
                                      { %>
                                    <option value="<%=lang.Name %>" <%= language.ToLower() == lang.Name.ToLower() ? "selected='selected'" : "" %>><%=lang.DisplayName %></option>
                                    <%} %>
                                </select></span>
                        </div>
                        <div class="col-xs-4 col-sm-4 col-md-4"></div>
                    </div>
                </div>
            </div>
            <div class="col-xs-3 col-sm-3 col-md-3"></div>
        </div>
    </div>



    <script type="text/javascript">

        
        function switchLanguages(languageName) {

            var old_url = window.location.href;
            var new_url = old_url.substring(0, old_url.indexOf('?'));

            window.location.href = new_url + "?language=" + languageName;

        }


    </script>


</asp:Content>
