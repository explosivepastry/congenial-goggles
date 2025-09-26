<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%
    Account acct = Account.Load(MonnitSession.CurrentCustomer.AccountID);

    List<Sensor> sensList = Sensor.LoadByAccountID(acct.AccountID);
    List<Gateway> gwList = Gateway.LoadByAccountID(acct.AccountID);
    List<CSNet> network = CSNet.LoadByAccountID(acct.AccountID);
    List<Notification> events = Notification.LoadByAccountID(acct.AccountID);
      
%>


<style>
    .CheckList {
        padding: 15px;
        background-color: #fff;
        color: #000;
        font: 14px arial, sans-serif;
    }

    #CheckListForm:hover {
        /*border: 3px solid rgba(179, 0, 0, .3);*/
        box-shadow: inset 0 0 0 3px rgba(179, 0, 0, 0.3);
    }

    .CheckItems {
        text-align: left;
        padding: 5px;
        font-size: 12px;
    }

    .CheckItems h2 {
        font-size: 16px;
        font-weight: 600;
    }

    #skipBtn {
        font-size: 14px;
        margin-top: 15px;
    }

    .addBtn:hover {
        background-color: #256FD5;
        color: white;
    }
</style>

<div id='CheckListForm' class="CheckList">
<div class="container-fluid">
    <div class="row">
        
            <div id="attention">
                <p class="text text-danger"><b><%: Html.TranslateTag("Shared/AccountCompletionList|Attention", "Attention")%>!</b> <%: Html.TranslateTag("Shared/AccountCompletionList|Complete the following tasks to get the most from your account" ,"Complete the following tasks to get the most from your account")%>.</p>
            </div>

            <!-- Set Recovery Email -->
            <% if (acct.RecoveryEmail == string.Empty) 
               {%>
            <div class="CheckItems col-sm-7">
                <h2><%: Html.TranslateTag("Shared/AccountCompletionList|Recovery Email", "Recovery Email")%></h2>
                <p><%: Html.TranslateTag("Shared/AccountCompletionList|Add a Recovery Email to easily regain access if your account is locked. We recommend using a different email than your primary contact", "Add a Recovery Email to easily regain access if your account is locked. We recommend using a different email than your primary contact")%>.</p>
                <a class="btn btn-primary addBtn" href="/Settings/AccountEdit/<%: acct.AccountID %>#RecoveryEmail"><i class="fa fa-plus-square" style="font-size: 16px; color: #F0F0F0!important"></i> <%: Html.TranslateTag("Shared/AccountCompletionList|Add Recovery Email", "Add Recovery Email")%></a>
            </div>
            <% } %>

            <!-- Add Network -->
            <%if (network.Count < 1) {%>
            <div class="CheckItems col-sm-6">
                <h2><%: Html.TranslateTag("Network", "Network")%></h2>
                <p><%: Html.TranslateTag("Shared/AccountCompletionList|Add a Network to link your sensors and gateways", "Add a Network to link your sensors and gateways")%>.</p>
                <a class="btn btn-primary addBtn" href="/Network/Create/<%: acct.AccountID %>"><i class="fa fa-plus-square" style="font-size: 16px; color: #F0F0F0!important"></i> <%: Html.TranslateTag("Shared/AccountCompletionList|Add Network", "Add Network")%></a>
            </div>
            <% } %>

            <!-- Add Gateway -->
            <%if (gwList.Count < 1 ) {%>
            <div class="CheckItems col-sm-6">
                <h2><%: Html.TranslateTag("Gateway", "Gateway")%></h2>
                <p><%: Html.TranslateTag("Shared/AccountCompletionList|Add a Gateway to communicate with sensors in your network", "Add a Gateway to communicate with sensors in your network")%>.</p>
                <a class="btn btn-primary addBtn" href="/Network/NetworkSelect?accountID=<%: acct.AccountID %>"><i class="fa fa-plus-square" style="font-size: 16px; color: #F0F0F0!important"></i> <%: Html.TranslateTag("Add Gateway", "Add Gateway")%></a>
            </div>
            <% } %>

            <!-- Add Sensor -->
            <%if (sensList.Count < 1) {%>
            <div class="CheckItems col-sm-6">
                <h2><%: Html.TranslateTag("Sensor", "Sensor")%></h2>
                <p><%: Html.TranslateTag("Shared/AccountCompletionList|Add Sensors to your network", "Add Sensors to your network")%></p>
                <a class="btn btn-primary addBtn" href="/Network/NetworkSelect?accountID=<%: acct.AccountID %>"><i class="fa fa-plus-square" style="font-size: 16px; color: #F0F0F0!important"></i> <%: Html.TranslateTag("Add Sensor", "Add Sensor")%></a>
            </div>
            <% } %>

            <!-- Add Events -->
            <%if (events.Count < 1) {%>
            <div class="CheckItems col-sm-6">
                <h2><%: Html.TranslateTag("Event", "Event")%></h2>
                <p><%: Html.TranslateTag("Shared/AccountCompletionList|Add Events so sensors can trigger notifications or other actions", "Add Events so sensors can trigger notifications or other actions")%>.</p>
                <a class="btn btn-primary addBtn" href="/Rule/CreateNew"><i class="fa fa-plus-square" style="font-size: 16px; color: #F0F0F0!important"></i> <%: Html.TranslateTag("Add Event", "Add Event")%></a>
            </div>
            <% } %>

            <!-- Close Window -->
            <div class="CheckItems col-sm-12" style="text-align: center">
                <a id="skipBtn" href="#" class="btn btn-default btn-block"><%: Html.TranslateTag("Shared/AccountCompletionList|Skip", "Skip")%></a>
                <div class="form-check">
                    <input type="checkbox" id="disableWindow" class="form-check-input">
                    <%: Html.TranslateTag("Shared/AccountCompletionList|Don't show this window again", "Don't show this window again")%>.
                </div>
            </div>

        </div>
    </div>
</div>

<script>

    $(function () {
        $("#skipBtn").click(function () {
            var disabled = $("#disableWindow").prop('checked');

            if (disabled == true)
                $.post("/Settings/DisableAccountCheckList/", { disabled: disabled }, function (data) {


                });

            $('#CheckListForm').fadeOut('slow');
        });


    });

</script>
