<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Events/EditInactivityTrigger|Notify when device does not communicate for more than")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" id="CompareType" name="CompareType" type="hidden" value="<%:eCompareType.Greater_Than %>" />
        <input class="form-control" id="CompareValue" name="CompareValue" type="text" value="<%:Model.CompareValue %>" />
        <%: Html.TranslateTag("Minutes","Minutes")%>
    </div>
</div>
    
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <br />
        <%: Html.TranslateTag("Events/EditInactivityTrigger|Ignore condition during active maintenance window")%>
        
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
    <div class="col sensorEditFormInput">
        <input id="IgnoreMaintenanceWindow" name="IgnoreMaintenanceWindow" type="checkbox" <%:Model.IgnoreMaintenanceWindow ? "checked=checked" : ""%>">
        <input name="IgnoreMaintenanceWindow" type="hidden" value="false" />
    </div>
</div>

<script type="text/javascript">
    function triggerSettings() {
        var settings = "compareType=" + $('#CompareType').val();
        settings += "&compareValue=" + $('#CompareValue').val();
        settings += "&IgnoreMaintenanceWindow=" + ($('#IgnoreMaintenanceWindow').attr('checked') == "checked" ? "true" : "false");

        return settings;
    }
    function triggerUrl() {
        return "/Rule/EditInactivityTrigger/<%:Model.NotificationID%>";
    }
</script>
