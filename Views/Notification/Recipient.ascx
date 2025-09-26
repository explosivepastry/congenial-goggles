<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>


<div class="formBody" style="margin-top: 20px;">
    <div style="border: 1px solid #ccc" class="notiTable">
        <div class="blockSectionTitle">
            <div style="float: left; width: 33%;" class="blockTitle">Notification will be sent to</div>
            <div style="clear: both;"></div>

            <div style="float: left; width: 49%;" class="deviceSearch">
                <div class="searchInput">
                    <input id="userFilter" name="userFilter" type="text" />
                </div>
            </div>
            <!-- deviceSearch -->

            <div style="float: left; width: 49%;" class="blockDesc">Click on icon to turn it green to activate</div>
            <div style="clear: both;"></div>
        </div>
        <div id="divUserList">
            <!--devicelist-->
        </div>
    </div>

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
        $.get("/Notification/UserList/<%:Model.NotificationID %>?q=" + $('#userFilter').val(), function (data) {
            $('#divUserList').html(data);
        });
    }

    function toggleRecipient(anchor) {
        anchor = $(anchor);
        var customerID = anchor.data("customerid");
        var notificationType = anchor.data("type");
        var add = anchor.hasClass('inactive');
        var url = "/Notification/ToggleRecipient/<%:Model.NotificationID %>";

        // alert(customerID+" "+notificationType+" "+add+" "+url);
        var params = "customerID=" + customerID;

        params += "&notificationType=" + notificationType;
        params += "&add=" + add;
        $.post(url, params, function (data) {
            if (data == 'Success') {

                loadUsers();
            }
            else {
                console.log(data);
                showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
            }
        });
    }

    function setDelay(Delayinput) {
        Delayinput = $(Delayinput);
        var recipientID = Delayinput.data("recipientid");
        var delayMinutes = Delayinput.val();
        var url = "/Notification/SetDelayMinutes/";

        var params = "notificationRecipientID=" + recipientID;

        params += "&delayMinutes=" + delayMinutes;


        $.post(url, params, function (data) {
            if (data == 'Success') {
                loadUsers();
            }
            else {
                console.log(data);
                showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
            }
        });
    }


</script>
