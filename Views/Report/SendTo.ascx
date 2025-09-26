<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.ReportSchedule>" %>

<div class="formBody" style="margin-top:20px;">
<table width="100%"> 
    <tr>
        <td style="vertical-align:top;" class="notiLT">
            <div class="tableLeft">
                <div class="blockSectionTitle">
                    <div class="deviceSearch">
						<div class="searchInput"><input id="userFilter" name="userFilter" type="text" /></div>
						<div class="searchButton"><img src="../../Content/images/Notification/device-search.png" /></div>
                    </div> <!-- deviceSearch -->
                    <div style="clear: both;"></div>
                </div>
                <div id="divUserList" style="height:286px; overflow-y:auto; padding:10px;"></div>
            </div>
        </td>
        <td style="vertical-align:top;" align="center" class="notiCT">
            <div id="addRecipients">
                <a href="Add" onclick="addRecipient('Email'); return false;" class="addbutton"><img src="/content/images/notification/add-arrows.png" class="" /></a><br />
            </div>
        </td>
        <td style="vertical-align:top;" class="notiRT">
            <div class="tableRight">
                <div class="blockSectionTitle">
                    <div class="blockTitle">Report notification will be sent to</div>
                    <div style="clear: both;"></div>
                </div>
                <div style="max-height:300px; overflow-y:auto;">
                    <table id="recipientsTable" width="100%">
                        <% Html.RenderPartial("AddRecipient", Model.DistributionList); %>
                    </table>
                </div>
            </div>
        </td>
    </tr>
</table>
</div>    

<script type="text/javascript">
	var userFilterTimeout = null;
	$(document).ready(function () {
		loadUsers();

		$('#userFilter').watermark('User Search', {
			left: 5,
			top: 0
		}).keyup(function () {
			if (userFilterTimeout != null)
				clearTimeout(userFilterTimeout);
			userFilterTimeout = setTimeout("loadUsers();", 1000);
		});
	});

	function loadUsers() {
		$.get("/Report/UserList/<%:Model.ReportScheduleID %>?q=" + $('#userFilter').val(), function (data) {
    		$('#divUserList').html(data);
    	});
    }

    function addRecipient(notificationType) {
    	var url = "/Report/AddRecipient/<%:Model.ReportScheduleID %>";
    	var params = "";
    	var custChecked = false;
    	var checkIndex = 1;
    	$("input:checked").each(function () {
    		var id = $(this).attr("id");
    		if (id && id.indexOf("customerID_") == 0)//make sure it is from the correct list
    		{
    			params += "customerIDs=" + id.replace("customerID_", "");
    			custChecked = true;
    		}
    		if ($("input:checked").length != checkIndex++) {
    			params += "&";
    		}
    	});
    	if (custChecked == false) {
            showSimpleMessageModal("<%=Html.TranslateTag("You must select at least one user to add.")%>");
    		return;
    	}

    	$.post(url, params, function (data) {
    		$('#recipientsTable').html(data);
    		loadUsers();
    	});
    }

    function addRecipientMethod(customerID, notificationType) {
    	var url = "/Report/AddRecipient/<%:Model.ReportScheduleID %>";
    	var params = "customerIDs=" + customerID;
    	
    	$.post(url, params, function (data) {
    		$('#recipientsTable').html(data);
    		loadUsers();
    	});
    }

    function removeRecipient(customerID, notificationType) {
    	var url = "/Report/RemoveRecipient/<%:Model.ReportScheduleID %>";
    	var params = "customerID=" + customerID;
    	$.post(url, params, function (data) {
    		$('#recipientsTable').html(data);
    		loadUsers();
    		//if (data == 'Success')
    		//    $('.recipient' + customerID).hide();
    		//else
    		//    alert(data);
    	});
    }
</script>