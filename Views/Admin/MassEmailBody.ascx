<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.EmailTemplate>" %>

<form id="emailform" method="post" action="/Admin/buildEmail">
    <div id="mailbody">
        <%Html.RenderPartial("MassEmailContent", new EmailTemplate());%>
    </div>
</form>

<div class="buttons" style="margin: 10px -10px -10px -10px;">
    <div style="float: left;">
        <h1 style="float: left;">Template:</h1>
        <a style="float: left; padding-left: 20px; cursor: pointer;" id="rtemplate">
            <h3>Release</h3>
        </a>
        <a style="float: left; padding-left: 20px; cursor: pointer;" id="mtemplate">
            <h3>Maintenance</h3>
        </a>
    </div>
    <a href="#" onclick="getMain(); return false;" class="greybutton">Cancel</a>
    <input id="postEmail" type="button" value="Send" class="bluebutton" />
    <div style="clear: both;"></div>
</div>


<script type="text/javascript">
    var pleaseSelect = "<%: Html.TranslateTag("Settings/AdminMassEmail|Please Select a Recipient Account")%>";
    $(document).ready(function () {

        $('#postEmail').click(function (data) {

            var ids = "";
            $('.themeboxes:checked').each(function () {
                ids += this.name + '|';
            });            

            if (ids.length > 0) {
                //var encoded = escape(objEditor.getData());
                var encoded = escape(sunEditorObj.getContents());
                var postdata = "Subject=" + $('#subject').val();
                postdata += "&EditorData=" + encoded;
                postdata += "&IDs=" + ids
                $.post('/Admin/buildEmail', postdata, function (data) {
                    alert(data);
                });
            } else {
                showSimpleMessageModal("<%=Html.TranslateTag("Please Select a Recipient Account")%>");
            }
        });

        $('#rtemplate').click(function (data) {
            gettemplate('Release');
        });

        $('#mtemplate').click(function (data) {
            gettemplate('Maintenance');
        });
    });

    function gettemplate(type) {
        $.post('/Admin/MassEmailContent?flag=' + type, function (data) {
            $('#mailbody').html(data)
        });
    }

    var sunEditorObj = null;

    
</script>