<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<long?>" %>

<%if (Model != null)
  { %>
<div class="editor-label">
    Name (SensorID)
</div>
<div class="editor-field">
    Not Found (<%:Model %>)
</div>

<div style="clear: both;"></div>
<%} %>

<form action="/Report/DeviceLookup" method="post">
    <%: Html.ValidationSummary(true) %>

    <div class="editor-label">
        Device ID
    </div>
    <div class="editor-field">
        <input type="text" id="id" name="id" value="0" />
    </div>

    <div style="clear: both;"></div>
    <div class="buttons" style="margin: 10px -10px -10px -10px;">

        <input type="button" onclick="postMain();" value="Submit" class="bluebutton" />
        <div style="clear: both;"></div>
    </div>
</form>



<script type="text/javascript">
    $(document).ready(function () {

        //$(window).keydown(function (event) {
        //    if ($("*:focus").attr("id") != "savebtn") {
        //        if (event.keyCode == 13) {
        //            event.preventDefault();
        //            return false;
        //        }
        //    }
        //});

        $('form').submit(function (e) {
            e.preventDefault();
            postMain();
        });
        $("#id").keypress(function (e) {
            if (e.which == 13) {
                $('.bluebutton').focus().click();
            }
        }).focus().select();
        setTimeout("if(document.activeElement != $('#id')[0]) {$('#id').focus().select();}", 1000);
        setTimeout("if(document.activeElement != $('#id')[0]) {$('#id').focus().select();}", 2000);
        setTimeout("if(document.activeElement != $('#id')[0]) {$('#id').focus().select();}", 3000);
    });

</script>
