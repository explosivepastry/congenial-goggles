<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Error.Master" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>

<asp:Content ID="errorTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Error
</asp:Content>

<asp:Content ID="errorContent" ContentPlaceHolderID="MainContent" runat="server">

    <%
        string CacheControl = ConfigData.FindValue("CacheControl");
        if (string.IsNullOrEmpty(CacheControl))
            CacheControl = DateTime.UtcNow.Date.Ticks.ToString();//If nothing else force refresh once a day

        string errorMessage = ViewBag.ErrorMessage; ;
    %>
   
        <!-- Error Page -->
        <div class="error">
                <div class="container-floud">
                    <div class="col-xs-12 ground-color text-center">
                        
                        <div class="container-error-404" style="width:100%;">
                            <br />
                            <br />
                            <a role="button" href="/overview" class="btn btn-danger btn-large" style="margin-left:40%;margin-right:40%; width:20%; height:40px;border-radius:3px!important;font-size: 18px;align-content:center;vertical-align: middle!important;background: linear-gradient(to bottom, #335c80 0%,#207cca 0%,#335c80 100%,#1e5799 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */ border-color:#335c80;">Home</a>
                            <br />
                            <br />
                            <h2 class="h1">Error</h2>
                        <div class="clip"><div class="shadow"><span class="digit thirdDigit"></span></div></div>
                            <div class="clip"><div class="shadow"><span class="digit secondDigit"></span></div></div>
                            <div class="clip"><div class="shadow"><span class="digit firstDigit"></span></div></div>
                        <h2 class="h1">
                            Sorry, something went wrong!
                            <br />
                        
                    <%
                        if (String.IsNullOrEmpty(errorMessage))
                        {
                    %>

                            Page not found

                    <%
                        }
                        else
                        {
                    %>
                            <br />
                            <pre><%: errorMessage %></pre>
                    <%
                        } 
                    %>    
                        </h2>
                    </div>
                </div>
            </div>
        </div>
        <!-- Error Page -->

    <!-- Optional JavaScript -->
    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
    <script src="/Scripts/jqueryPlugins/jquery-3.6.0.min.js?<%:CacheControl %>" type="text/javascript"></script>
    <script src="/content/Overview/js/popper.min.js"></script>
    <script src="/content/Overview/plugins/bootstrap/js/bootstrap.min.js?<%:CacheControl %>"></script>
	  
    <!-- Local JS -->  
	<script src="/Content/Overview/js/404.js" type="text/javascript"></script>

</asp:Content>
