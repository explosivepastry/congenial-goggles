<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string>" %>

<!DOCTYPE html>

<html > 
<head> 
<title>Forgot Password</title> 
</head> 
<body> 

    <%--<script>alert(1)</script>--%>
    <%--<script>
        var pos = document.URL.indexOf("message=") + 5;
        document.write(document.URL.substring(pos, document.URL.length));
    </script>--%>

    <form action="/Account/ForgotPassword" id="forgotPassword" method="post">
        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>

    <div id="fullForm" style="width: 100%;">
        <div class="formtitle">Retrieve Password</div>
        <div class="formBody">

            <div class="editor-label">
                <label for="UserName">User Name</label>
            </div>
            <div class="editor-field">
                <input name="UserName" type="text" />
            </div>
            <div class="editor-error-small">
                <span><%:ViewBag.Result%></span>
            </div>

            <div style="clear: both;"></div>
        </div>
        <div class="buttons">
            <input type="button" value="Cancel" class="hideModal greybutton"  />
            <input type="button" value="Retrieve" class="postModal bluebutton" />
            
            <div style="clear: both;"></div>
        </div>
    </div>
</form>
 
<script type="text/javascript">
    $(function () {
        $('.postModal').click(function () { postModal(); });
        $('.hideModal').click(function () { hideModal(); });*
    });
</script>

</body> 
</html> 
