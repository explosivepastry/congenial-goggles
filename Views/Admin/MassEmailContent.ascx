<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.EmailTemplate>" %>

<div class="editor-label" style="padding-top: 5px; padding-left: 35px; width: 76px;">
    Subject
</div>
<div class="editor-field">
    <input type="text" id="subject" name="subject" value="<%=Model.Subject %>" />
</div>

<div style="clear: both;"></div>

<% string temp = System.Net.WebUtility.HtmlDecode(Model.Template);  %>
<%:  Html.Hidden("Template",temp) %>
<textarea id="editor"><%:temp%></textarea>

<script>
    $(document).ready(function () {
        //objEditor = null;
        //createEditor();

        sunEditorObj = createSunEditor('editor');
    });
</script>