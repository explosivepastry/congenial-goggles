<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>


<% 
    MaintenanceWindow currentMaintenanceWindow = MaintenanceWindow.LoadActive();
    //If nothing is currently active than there is nothing to show
    //If user not logged in, then don't show yet (so they can acknowledge)
    if (currentMaintenanceWindow != null
        && MonnitSession.CurrentCustomer != null)
    {
        long CustomerMaintenancePopUpLevel = MonnitSession.CurrentCustomer.Preferences["MaintenancePopUpLevel"].ToLong();

        MaintenanceWindowCustomer CurrentCustomerStatus = MonnitSession.CurrentCustomer.MaintWindowAcked(currentMaintenanceWindow.MaintenanceWindowID, eMaintenanceWindowCustomerType.Pop_Up);
        bool CustomerHasAcknowledgedThisWindow = (CurrentCustomerStatus != null && CurrentCustomerStatus.Acknowledged);

        //If Not already acknowledged
        //If Customer Preference indicates they should see this one
        if (!CustomerHasAcknowledgedThisWindow
            && CustomerMaintenancePopUpLevel <= currentMaintenanceWindow.SeverityLevel)
        {
            string RawHTMLToDisplay = currentMaintenanceWindow.Description;
			
			//Don't load any additional popups on this page load
            MonnitSession.CurrentCustomer.ShowPopupNotice = false;

            //Check if the Theme has overwritten the message
            AccountThemeMaintenanceLink atml = AccountThemeMaintenanceLink.LoadByAccountThemeIDAndMaintenanceID(MonnitSession.CurrentTheme.AccountThemeID, currentMaintenanceWindow.MaintenanceWindowID);
			if (atml != null && !string.IsNullOrEmpty(atml.OverriddenNote))
            {
				RawHTMLToDisplay = atml.OverriddenNote;
            }
%>
<style>
	.backdrop-note {
		width: 100%;
		height: 100%;
		background-color: #000000ab;
		position: absolute;
		z-index: 5;
	}
	.maint-notify {
		z-index: 2;
		right: -33vw;
		padding-bottom: 9px;
		width: clamp(305px, 58vw, 471px );
		border-radius: 5px;
		top: 6vh;
		position: relative;
		text-align: center;
		background-color: white ;
	}
	.maintIcon{
		background:#EBEBF0;
		border-radius: 5px 5px 0px 0px;
		margin-bottom: 5px;
	}
	.maintIcon svg {
		fill: #e91820; 
		width:30px;
		height:30px;
		margin:10px;
	}

	.content-text-maintenance{
		padding:1.25rem;
		text-align:justify;
	}

</style>
<div id="maintNotification" class="backdrop-note"> 
	<div class='maint-notify'>
		<div class="maintIcon">
			<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><path d="M78.6 5C69.1-2.4 55.6-1.5 47 7L7 47c-8.5 8.5-9.4 22-2.1 31.6l80 104c4.5 5.9 11.6 9.4 19 9.4h54.1l109 109c-14.7 29-10 65.4 14.3 89.6l112 112c12.5 12.5 32.8 12.5 45.3 0l64-64c12.5-12.5 12.5-32.8 0-45.3l-112-112c-24.2-24.2-60.6-29-89.6-14.3l-109-109V104c0-7.5-3.5-14.5-9.4-19L78.6 5zM19.9 396.1C7.2 408.8 0 426.1 0 444.1C0 481.6 30.4 512 67.9 512c18 0 35.3-7.2 48-19.9L233.7 374.3c-7.8-20.9-9-43.6-3.6-65.1l-61.7-61.7L19.9 396.1zM512 144c0-10.5-1.1-20.7-3.2-30.5c-2.4-11.2-16.1-14.1-24.2-6l-63.9 63.9c-3 3-7.1 4.7-11.3 4.7H352c-8.8 0-16-7.2-16-16V102.6c0-4.2 1.7-8.3 4.7-11.3l63.9-63.9c8.1-8.1 5.2-21.8-6-24.2C388.7 1.1 378.5 0 368 0C288.5 0 224 64.5 224 144l0 .8 85.3 85.3c36-9.1 75.8 .5 104 28.7L429 274.5c49-23 83-72.8 83-130.5zM104 432c0 13.3-10.7 24-24 24s-24-10.7-24-24s10.7-24 24-24s24 10.7 24 24z"/></svg>
		</div>
	
		<div class="content-text-maintenance">
			<%: Html.Raw(RawHTMLToDisplay) %>
		</div>

		<div type="button" class="btn btn-primary" id="HideMaintNotification">Acknowledge</div>
	</div>
</div>

<!-- maintNotification -->

<script>
	$(function () {
		$('#HideMaintNotification').click(function () {
			$.get('/Admin/AcknowledgeMaint/<%:currentMaintenanceWindow.MaintenanceWindowID%>', function (data) {
				$('#maintNotification').hide();
			});
		});
	});
</script>
<%		}
	}%>		