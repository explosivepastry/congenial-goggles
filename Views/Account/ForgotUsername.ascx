<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.RetrieveUserName>" %>

<!DOCTYPE html>

<html > 
<head> 
<title>Forgot Username</title> 
</head> 
<body> 

<form action="/Account/ForgotUsername" id="forgotUsername" method="post">
     <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>

<div id="fullForm" style="width: 100%;">
    <div class="formtitle">Retrieve User Name</div>
    <div class="formBody">
        
        <div class="editor-label-small">
			<label for="NotificationEmail">Email Address</label>
        </div>
        <div class="editor-field">
            <input id="NotificationEmail" name="NotificationEmail" type="text" value="<%: Model.NotificationEmail %>">
        </div>
        <div class="editor-error-small">
            <%: Html.ValidationMessageFor(model => model.NotificationEmail) %>
            <span><%:Model.Result %></span>
        </div>
        
        <div style="clear:both;"></div>    
    </div>
    <div class="buttons">
        <input type="button" value="Cancel" class="hideModal greybutton"/>
        <input type="button" value="Retrieve" class="postModal bluebutton" data-toggle="modal"/>
        <div style="clear:both;"></div>
    </div>
 </div> 
</form>

<script type="text/javascript">
    $(function () {
        $('.postModal').click(function () { postModal(); });
        $('.hideModal').click(function () { hideModal(); });
    });
</script>

    </body> 
</html> 
