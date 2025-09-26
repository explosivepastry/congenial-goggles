<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.EmailTemplate>" %>


    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Name) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Name) %>
            </div>
            <div class="editor-error">
                <%: Html.ValidationMessageFor(model => model.Name) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Subject) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Subject)%>
            </div>
            <div class="editor-error">
                <%: Html.ValidationMessageFor(model => model.Subject)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Flags) %>
            </div>
            <div class="editor-field">
                <%: Html.DropDownList("FlagOptions", eEmailTemplateFlag.Generic) %>
                <input id="AddFlag" type="button" value="Add Flag" />
            </div>
            <div class="editor-label">
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Flags)%>
                <input type="button" value="Clear" onclick="$('#Flags').val('');" />
            </div>
            <div class="editor-error">
                <%: Html.ValidationMessageFor(model => model.Flags)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Template)%>
            </div>
            <div class="editor-field">
                <textarea id="editor"></textarea>
                <%: Html.HiddenFor(model => model.Template)%>
            </div>
            <div class="editor-error">
                <%: Html.ValidationMessageFor(model => model.Template)%>
            </div>
            
 <div class="buttons" style="margin: 10px -10px -10px -10px;">
     <a href="#" onclick="getMain(); return false;" class="greybutton">Cancel</a>
     <a href="#" onclick="$('#Template').val(sunObjEditor.getContents()); postMain(); return false;" class="bluebutton">Save</a>
    <div style="clear:both;"></div>
</div>
            

    <% } %>

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

            $('#Template').keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });

            $('#Flags').keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });

            $('#FlagOptions').keydown(function (event) {
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

            //objEditor = null;
            //createEditor();
            sunObjEditor = createSunEditor('editor');

            $('#Flags').attr("readonly", "true");
            $('#Flags').css("background-color", "#eee");

            $('#AddFlag').click(function () {
                if ($('#Flags').val() == "")
                    $('#Flags').val($('#FlagOptions').val());
                else
                    $('#Flags').val($('#Flags').val() + "|" + $('#FlagOptions').val());
            });
        });

        var sunObjEditor;
        
    </script>