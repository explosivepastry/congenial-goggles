<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div class="modal-content">
    <div class="modal-header">
        <h5 class="modal-title" id="pageHelp"><%: Html.TranslateTag("Rule/ChooseType|Rule Options")%></h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
    </div>
    <div class="modal-body">

        <div class="row">
            <div class="col- word-choice">
                <%: Html.TranslateTag("Rule/ChooseType|What is a System Action?")%>
            </div>
            <div class="word-def" >
                <%: Html.TranslateTag("Rule/ChooseType|A command or \"action\" for the system to process when this rule triggers")%><br />
                <hr />
            </div>
        </div>
        <div class="row">
            <div class="col- word-choice"">
                <%: Html.TranslateTag("Rule/ChooseType|Acknowledge")%>
            </div>
            <div class="word-def">
                <%: Html.TranslateTag("Rule/ChooseType|The Acknowledge System Action simply Disarms (Acknowledges) a Rule. A common use case would be to execute a System Action to Disarm (Acknowledge) the rule its assigned to after it is triggered. The purpose behind this might be to make a Sensor Reading trigger and send an E-Mail on that triggering condition, but not continue to send repeat E-Mails on the Rule's configured Snooze time (which by default is 60 minutes).")%>
                <hr />
            </div>
        </div>
        <div class="row">
            <div class="col- word-choice"">
                <%: Html.TranslateTag("Rule/ChooseType|Full Reset")%>
            </div>
            <div class="word-def" >
                <%: Html.TranslateTag("Rule/ChooseType|The Full Reset would Rearm (Reset) a Rule when it is executed. A common scenario for this might be a user that wants to be immediately notified via SMS on a triggering condition every time the sensor reports a reading with that triggering condition (as opposed to utilizing the Snooze feature)")%>
                <hr />
            </div>
        </div>
        <div class="row">
            <div class="col- word-choice"">
                <%: Html.TranslateTag("Rule/ChooseType|Activate and Deactivate")%>
            </div>
            <div class="word-def">
                <%: Html.TranslateTag("Rule/ChooseType|The Activate and Deactivate System Actions turn on or off the target rule. This completely enables/disables the rule from triggering. You might use this any time you wish a triggering condition to turn on or off a rule.")%>
                <hr />
            </div>
        </div>

        <div class="row">
            <div class="col- word-choice"">
                <%: Html.TranslateTag("Rule/ChooseType|Rule Webhook")%>
            </div>
            <div class="word-def" >
                <%: Html.TranslateTag("Rule/ChooseType|A Rule Webhook sends data to your end point when an Rule condition causes the system action to execute. You can configure the destination and parameters used to route the request in the API section.")%>
                <hr />
            </div>
        </div>
    

        <div class="row">
            <div class="col- word-choice"">
                <%: Html.TranslateTag("Rule/ChooseType|Time Delay")%>
            </div>
            <div class="word-def" >
                <%: Html.TranslateTag("Rule/ChooseType|The amount of time after the rule has been triggered before the system action runs.Each System Action has the ability to set up a Delay. This is useful for when you need to set up logic that happens in a planned sequence. If no delay is enabled, the System Action is executed immediately.")%>
                <hr />
            </div>
        </div>
        <div class="row">
            <div class="col- word-choice"">
                <%: Html.TranslateTag("Rule/ChooseType|Target Rule")%>
            </div>
            <div class="word-def" >
                <%: Html.TranslateTag("Rule/ChooseType|The rule this system action will affect.")%>
   
            </div>
        </div>


    </div>
    <div class="modal-footer">
    
    </div>
</div>
