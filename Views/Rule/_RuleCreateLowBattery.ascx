<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Notification>" %>


<%---------RuleCreateLowBattery.ascx--%>

<div class="rules_container">
    <div class="rule-card_container " style="max-width:320px; margin-top:0;">


        <div class="card_container__top">
            <div class="card_container__top__title">

                <%: Html.TranslateTag("Pick a Condition","Choose a Condition")%>
            </div>
        </div>

        <hr style="margin: 0 0 10px" />



        <div class="rule-card">
            <div class="rule-title">
                <%: Html.TranslateTag("Events/CreateBatteryTrigger|Notify when battery is below","Notify when battery is below")%>
            </div>

            <div class="battery-low">
                <input class="user-dets" id="CompareType" name="CompareType" type="hidden" value="<%:eCompareType.Less_Than %>" />
                <input class=" user-dets" id="CompareValue" name="CompareValue" type="number" min="0" max="100" value="<%:Model.CompareValue %>">
                <div>%</div>
                                   
                        <%: Html.ValidationMessageFor(model => model.CompareValue)%>
            </div>
        </div>


        <div class="save-me">
            <button type="button" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary user-dets" onclick="$(this).hide();$('#saving').show();createTrigger(this);">
                <%: Html.TranslateTag("Save","Save")%>
            </button>
            <button class="btn btn-primary" id="saving" style="display: none;" type="button" disabled>
                <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                <%= Html.TranslateTag("Saving") %>...
            </button>
        </div>

        <div id="result"></div>
    </div>
</div>



<script type="text/javascript">
    function createTrigger(btn) {
        btn = $(btn);
        btn.hide();


        var settings = "compareType=" + $('#CompareType').val();
        settings += "&compareValue=" + $('#CompareValue').val();

        $.post("/Rule/AddRuleConditions", settings, function (data) {
            if (data == "Success") {
                window.location.href = "/Rule/ChooseTask";
            }
        });
    }
 </script>




<!-- Event List View -->

