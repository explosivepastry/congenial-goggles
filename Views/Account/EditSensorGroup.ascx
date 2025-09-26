<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.SensorGroup>" %>

<form action="/Account/EditSensorGroup" method="post">
    
    <div class="formtitle" style="margin: -10px -10px 10px -10px;">Sensor Group</div>

    <%: Html.ValidationSummary(true) %>
    <%: Html.HiddenFor(model => model.SensorGroupID) %>
    <%: Html.HiddenFor(model => model.AccountID) %>

        
    <div class="editor-label">
        <%: Html.LabelFor(model => model.Name) %>
    </div>
    <div class="editor-field">
        <%: Html.EditorFor(model => model.Name) %>
        <%: Html.ValidationMessageFor(model => model.Name) %>
    </div>

    <div class="editor-label">
        <%: Html.LabelFor(model => model.Location) %>
    </div>
    <div class="editor-field">
        <%: Html.EditorFor(model => model.Location) %>
        <%: Html.ValidationMessageFor(model => model.Location) %>
    </div>

    <div class="editor-label">
        <%: Html.LabelFor(model => model.Size) %>
    </div>
    <div class="editor-field">
        <%: Html.EditorFor(model => model.Size) %>
        <%: Html.ValidationMessageFor(model => model.Size) %>
    </div>

    <div class="editor-label">
        <%: Html.LabelFor(model => model.TagString) %>
    </div>
    <div class="editor-field">
        <%: Html.EditorFor(model => model.TagString) %>
        <%: Html.ValidationMessageFor(model => model.TagString) %>
    </div>


    <div class="buttons" style="margin: 10px -10px -10px -10px;">
        <input type="button" value="Save" class="saveSensorGroup bluebutton"/>
        <input type="button" value="Cancel" class="cancelSensorGroup greybutton" />
        <div style="clear:both;"></div>
    </div>
</form>
<script>
    $(document).ready(function () {
        $('.saveSensorGroup').click(function () {
            var form = modalDiv.children('form');
            $.post(form.attr("action"), form.serialize(), function (data) {
                modalDiv.html(data);
                if (data == "Success")
                    window.location.href = window.location.href;
            }, "text");

            modalDiv.html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);
        });

        $('.cancelSensorGroup').click(function () {
            hideModal();
        });
    });
</script>