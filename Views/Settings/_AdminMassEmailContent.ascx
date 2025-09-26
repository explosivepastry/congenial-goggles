<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.EmailTemplate>" %>


<div class="bold col-lg-2 col-md-2 col-sm-2 col-xs-12 aSettings__title"><%: Html.TranslateTag("Settings/_AdminMassEmailContent|Subject","Subject")%></div>
<div class="col-lg-12 col-md-10 col-sm-10 col-xs-12">
    <input type="text" id="subject" class="form-control aSettings__input_input" name="subject" value="<%=Model.Subject %>" />
</div>
<div class="clearfix"></div>
<br />

<% string temp = System.Net.WebUtility.HtmlDecode(Model.Template);  %>
<%:  Html.Hidden("Template",temp) %>
<textarea id="editor"><%:temp %></textarea>

<script>
    $(document).ready(function () {
        sunEditorObj = createSunEditor('editor', true);
    });
</script>