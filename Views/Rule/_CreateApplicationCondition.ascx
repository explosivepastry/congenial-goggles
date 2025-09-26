<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Notification>" %>

<div class="container-fluid">
    <div class="card_container__body rules_container">
        <div class="rule-card_container cst-condition-custom" style="margin-top: 0;">
            <div class="card_container__top">
                <div class="card_container__top__title ">
                    <%: Html.TranslateTag("Choose a Condition","Choose a Condition")%>
                </div>
                <br />
            </div>

            <%
                Html.RenderPartial("~/Views/Sensor/DataTypeSpecific\\Default\\_DatumEventTrigger.ascx", Model);
            %>

            <div class="save-me ">
                <button type="button" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary user-dets" onclick="$(this).hide();$('#saving').show();createTrigger(this);">
                    <%: Html.TranslateTag("Save","Save")%>
                </button>
                <button class="btn btn-primary" id="saving" style="display: none;" type="button" disabled>
                    <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                    <%= Html.TranslateTag("Saving") %>...
                </button>
            </div>
        </div>
        <div id="result"></div>
    </div>
</div>

<script type="text/javascript">
    function createTrigger(btn) {
        btn = $(btn);
        btn.hide();

        const triggerToPost = {
            compareType: $('#CompareType').val(),
            compareValue: $('#previewMessage').data('to-post') ? $('#previewMessage').data('to-post') : $('#CompareValue').val(),
            scale: $('#scale').val()
        }
   
        $.post("/Rule/AddRuleConditions", triggerToPost, function (data) {
            //Show loader for at least 500ms
            if (data == "Success") {
                window.location.href = "/Rule/ChooseTask";
            }
        });
    }
</script>

