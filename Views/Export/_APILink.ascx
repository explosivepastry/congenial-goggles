<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

	<% if (MonnitSession.CurrentCustomer != null)
        { %>

<div class="col-12 view-btns_container shadow-sm rounded mb-5">
	<div class="view-btns deviceView_btn_row rounded">

		<a href="/API/RestAPI/" class="deviceView_btn_row__device">
			<div class="btn-<%:Request.Path.StartsWith("/API/RestAPI/") ? "active-fill shadow mb-2" : "secondaryToggle" %> btn-lg btn-fill ">
				<%=Html.GetThemedSVG("code") %><span class="extra" style="max-width: 80px!important; white-space: normal; overflow-wrap: break-word!important;"><%: Html.TranslateTag("Rest API","Rest API")%></span>
			</div>
		</a>
			
		<a href="/API/ApiKeys/" class="deviceView_btn_row__device">
			<div class="btn-<%:Request.Path.StartsWith("/API/ApiKeys/") ? "active-fill shadow mb-2" : "secondaryToggle" %> btn-lg btn-fill ">
				<%=Html.GetThemedSVG("lock") %><span class="extra" style="max-width: 80px!important; overflow-wrap: break-word!important;"><%: Html.TranslateTag("API Keys","API Keys")%></span>

			</div>
		</a>

		<a href="/Export/DataWebhook/" class="deviceView_btn_row__device">
			<div class=" btn-<%:(Request.Path.Contains("/Export/DataWebhook/") || Regex.IsMatch(Request.Path, @"/Export/Configure[A-za-z]+/Webhook")) ? "active-fill shadow mb-2" : "secondaryToggle" %> btn-lg btn-fill ">
				<%=Html.GetThemedSVG("api") %><span class="extra" style="max-width: 80px!important; white-space: normal; overflow-wrap: break-word!important; margin-bottom:-10px;"><%: Html.TranslateTag("Data Webhook","Data Webhook")%></span>
			</div>
		</a>

		<a href="/Export/NotificationWebhook/" class="deviceView_btn_row__device">
			<div class=" btn-<%:(Request.Path.Contains("/Export/NotificationWebhook/") || Regex.IsMatch(Request.Path, @"/Export/Configure[A-za-z]+/Notification")) ? "active-fill shadow mb-2" : "secondaryToggle" %> btn-lg btn-fill">
				<%=Html.GetThemedSVG("rules") %><span class="extra" style="max-width: 80px!important; white-space: normal; overflow-wrap: break-word!important; margin-bottom:-10px;"><%: Html.TranslateTag("Rule Webhook","Rule Webhook")%></span>
			</div>
		</a>
		<% if (MonnitSession.CurrentCustomer.Account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
            { %>
		<a href="/Export/WebhookHealth/" class="deviceView_btn_row__device">
			<div class="btn-<%:Request.Path.StartsWith("/Export/WebhookHealth/") ? "active-fill shadow mb-2" : "secondaryToggle" %> btn-lg btn-fill btn-secondaryToggle">
				<%=Html.GetThemedSVG("notifications") %><span class="extra" style="max-width: 80px!important; margin-bottom:-10px; white-space: normal; overflow-wrap: break-word!important;"><%: Html.TranslateTag("Webhook Health","Webhook Health")%></span>
			</div>
		</a>
		<% } %>
	
	</div>
</div>

<script type="text/javascript">
    $('.btn-secondaryToggle').hover(
        function () { $(this).addClass('active-hover-fill') },
        function () { $(this).removeClass('active-hover-fill') }
    )
</script>

<style type="text/css">
	.btn-active-fill path {
		fill: #fff !important;
	}

	.btn-active-fill i {
		color: #fff !important;
	}

	.btn-active-fill {
		width: 80px!important;
	}

	.active-hover-fill {
		width: 80px!important;
		height: 90px!important;
	}

	.extra {
/*		margin: 5px 0;*/
	}

</style>
<%} %>
