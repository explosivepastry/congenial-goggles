<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.EmailTemplate>" %>
<% using (Html.BeginForm()) 
   {%>
    <%: Html.ValidationSummary(true) %>

    <div class="editor-label">
        <%: Html.LabelFor(model => model.Name) %>
    </div>
    <div class="editor-field">
            <input type="text" id="Name" name="Name" value="<%= Model.Name %>" />
    </div>
    <div class="editor-error">
        <%: Html.ValidationMessageFor(model => model.Name)%>
    </div>
            
    <div class="editor-label">
        <%: Html.LabelFor(model => model.Subject) %>
    </div>
    <div class="editor-field">
            <input type="text" id="Subject" name="Subject" value="<%= Model.Subject %>" />
    </div>
    <div class="editor-error">
        <%: Html.ValidationMessageFor(model => model.Subject)%>
    </div>
            
    <div class="editor-label">
        <%: Html.LabelFor(model => model.Template) %>
    </div>
    <div class="editor-field">
        the variable "{Content}" will be replaced with the<br /> body of the email that uses this template.
    </div>

    <div class="editor-label">
        <% string temp = System.Net.WebUtility.HtmlDecode(Model.Template);  %>
        <%:  Html.Hidden("Template",temp) %>

        <textarea id="editor"><%:Model.Template%></textarea>
    </div>
    <div class="editor-error">
        <%: Html.ValidationMessageFor(model => model.Template) %>
    </div>
            
    <div class="buttons" style="margin: 10px -10px -10px -10px;">
        <a href="#" onclick="getMain(); return false;" class="greybutton">Cancel</a>
        <%--<a href="#" onclick="$('#Template').val(objEditor.getData()); postMain(); return false;" class="bluebutton">Save</a>--%>
        <a href="#" onclick="$('#Template').val(sunObjEditor.getContents()); postMain(); return false;" class="bluebutton">Save</a>
        <div style="clear:both;"></div>
    </div>

<% } %>

<script type="text/javascript">
    $(document).ready(function () {
        $('#Template').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        $('#Subject').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        $('#Name').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        //$(window).keydown(function (event) {
        //    if ($("*:focus").attr("id") != "savebtn") {
        //        if (event.keyCode == 13) {
        //            event.preventDefault();
        //            return false;
        //        }
        //    }
        //});

        //objEditor = null;
        //createEditor();

        sunObjEditor = createSunEditor('editor');
    });

    var sunObjEditor;
    
</script>