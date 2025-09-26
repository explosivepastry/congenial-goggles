<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ExternalDataSubscriptionAttempt>" %>

<style type="text/css">
    .refreshPic {display:none;  cursor: pointer;
    }
    .ui-tabs-active .refreshPic {display: inline;
    }  
</style>

<div class="tabContainer">
    <ul>
        <li><a id="tabHistory" href="/API/AttemptInfo/<%:Model.ExternalDataSubscriptionAttemptID %>"><%: Html.TranslateTag("Attempt Details","Attempt Details")%> <img class="refreshPic" src="/Content/images/SmallRefresh.png" alt="refresh" /> </a></li>
        <li><a id="tabEdit" href="/API/AttemptBody/<%:Model.ExternalDataSubscriptionAttemptID %>"><%: Html.TranslateTag("Attempt Body","Attempt Body")%> <img class="refreshPic" src="/Content/images/SmallRefresh.png" alt="refresh" /> </a></li>
        <li><a id="tabFrom" href="/API/RetryRequest/<%:Model.ExternalDataSubscriptionAttemptID %>"><%: Html.TranslateTag("Retry Request","Retry Request")%> <img class="refreshPic" src="/Content/images/SmallRefresh.png" alt="refresh" /> </a></li>
    </ul>
</div>

<script type="text/javascript">
	$(function () {
		var index = $('#tab<%:Request["tab"]%>').parent().index();
    	if (index < 0)
    		index = 0;
    	$(".tabContainer").tabs({ active: index });

    	$('.refreshPic').click(function () {
    		var tabContainer = $('.tabContainer').tabs();
    		var active = tabContainer.tabs('option', 'active');
    		tabContainer.tabs('load', active);
    	});
    });
</script>