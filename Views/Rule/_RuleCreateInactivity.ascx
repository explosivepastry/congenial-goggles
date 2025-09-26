<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Notification>" %>

<div class="rules_container">
    <div class="rule-card_container cst-custom" style="margin-top:0;">

        <div class="card_container__top">
            <div class="card_container__top__title ">
                <%: Html.TranslateTag("Choose a Condition")%>
            </div>
            
            <a class="helpIco" style="cursor: pointer!important;" data-bs-toggle="modal" data-bs-target=".pageHelp">
                <div class="help-hover"><%=Html.GetThemedSVG("circleQuestion") %></div>
            </a>            
            <!-- pageHelp button modal -->
            <div class="modal fade pageHelp" style="z-index: 2000!important;" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="pageHelp"><%: Html.TranslateTag("Rule/ChooseSensorTemplate|Ignore Maintenance Window Details","Ignore Maintenance Window Details")%></h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="word-choice"></div>
                                <div class="word-def">
                                    <div class="lil-m-on-x">
                                        Checking the "Ignore condition during active maintenance window" setting means that if
                                        this inactivity rule were to trigger during a maintenance window, the notification is to be ignored and skipped.
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="modal-footer">
                        </div>
                    </div>
                </div>
            </div>
            <!-- end pageHelp button modal -->
        </div>

        <hr style="margin: 0 0 10px" />

        <div class="rule-card">
            <div class="rule-title">
                <%: Html.TranslateTag("Events/CreateBatteryTrigger|When the device is inactive for more than")%>
            </div>
                
            <div class="battery-low">
                <input class="form-control user-dets" id="CompareType" name="CompareType" type="hidden" value="<%:eCompareType.Greater_Than %>" />
                <input class="form-control user-dets" id="CompareValue" style="width:80px;" name="CompareValue" type="number" min="0" value="<%:Model.CompareValue %>">
                <div>
                    <%: Html.TranslateTag("Minutes","Minutes")%>
                </div>
            </div>
                
            <label>
                <div>
                    <%: Html.TranslateTag("Ignore condition during active maintenance window","Ignore condition during active maintenance window")%>
                </div>
                <input id="IgnoreMaintenanceWindow" name="IgnoreMaintenanceWindow" type="checkbox" <%:Model.IgnoreMaintenanceWindow ? "checked=checked" : ""%>>
                <input name="IgnoreMaintenanceWindow" type="hidden" value="false" />
            </label>
        </div>

        <div class="save-me">
            <button type="button" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary user-dets" onclick="$(this).hide();$('#saving').show();createTrigger(this);">
                <%: Html.TranslateTag("Save","Save")%>
            </button>
            <button class="btn btn-primary user-dets" id="saving" style="display: none;" type="button" disabled>
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
        settings += "&IgnoreMaintenanceWindow=" + ($('#IgnoreMaintenanceWindow').attr('checked') == "checked" ? "true" : "false");

        $.post("/Rule/AddRuleConditions", settings, function (data) {
            //Show loader for at leat 500ms
            if (data == "Success") {
                window.location.href = "/Rule/ChooseTask";
            }
        });
    }
</script>