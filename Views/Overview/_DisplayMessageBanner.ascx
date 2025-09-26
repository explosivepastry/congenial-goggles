<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<% DisplayMessageModel m = MonnitSession.DisplayMessage;
    MonnitSession.DisplayMessage = null;
    
    if (m != null && !String.IsNullOrEmpty(m.Text))
    {  %>

<div id="banner" class="addsuccess">
    <div class="modal-header">
        <button type="button" id="closeBannerBtn" onclick="closeBanner();" class="btn-close" aria-label="Close"></button>
    </div>
    <%= Html.TranslateTag("Overview/Banner|" + m.Text.Replace("\"", "\\\"")) %>
</div>



<script>

    function closeBanner() {
        $('#banner').hide();

    };

</script>

<%} %>