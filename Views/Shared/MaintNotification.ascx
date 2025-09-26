<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<% MaintenanceWindow current = MaintenanceWindow.LoadActive();
    bool customerHasMainPrefPopUpLevel = MonnitSession.CurrentCustomer == null ? false : MonnitSession.CurrentCustomer.Preferences.ContainsKey("MaintenancePopUpLevel");
    int customerPreferredSeverityLevel = customerHasMainPrefPopUpLevel 
                                                                    ? MonnitSession.CurrentCustomer.Preferences["MaintenancePopUpLevel"].ToInt()
																	: 3;

if (current != null && ((MonnitSession.CurrentCustomer == null)
                        || (MonnitSession.CurrentCustomer.MaintWindowAcked(current.MaintenanceWindowID, eMaintenanceWindowCustomerType.Pop_Up) == null)
                        || ((MonnitSession.CurrentCustomer.MaintWindowAcked(current.MaintenanceWindowID, eMaintenanceWindowCustomerType.Pop_Up) != null)
                            && MonnitSession.CurrentCustomer.MaintWindowAcked(current.MaintenanceWindowID, eMaintenanceWindowCustomerType.Pop_Up).Acknowledged == false))
            && (customerHasMainPrefPopUpLevel && customerPreferredSeverityLevel >= current.SeverityLevel)
     )
{%>
    <div id="maintNotification">
        <% if (Request.IsAuthenticated && MonnitSession.CurrentCustomer != null)
           { %>
            <div id="HideMaintNotification" style="float: right; cursor: pointer; border: 1px solid; padding: 3px;">Close X</div>
        <% }
           if (current.AcctThemeLink.Count > 0)
           {
               bool isFound = false;%>
               <% foreach (var link in current.AcctThemeLink)
                  {
                    if (link.MaintenanceWindowID == current.MaintenanceWindowID && link.AccountThemeID == MonnitSession.CurrentTheme.AccountThemeID && !string.IsNullOrWhiteSpace(link.OverriddenNote))
                    {
                        isFound = true;%>
                        <%: Html.Raw(link.OverriddenNote)%>
                  <%}
                  }
                  if (isFound == false)
                  { %>
                    <%: Html.Raw(current.Description) %>
                <%}
           }
           else
           { %>
             <%: Html.Raw(current.Description) %>
         <%} %>
    </div>
<!-- maintNotification -->

<script>
    $(function () {
        $('#HideMaintNotification').click(function () {
            $.get('/Admin/AcknowledgeMaint/<%:current.MaintenanceWindowID%>', function (data) {
                $('#maintNotification').hide();
            });
        });
    });
</script>
<%} %>