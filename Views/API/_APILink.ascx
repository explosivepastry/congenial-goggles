<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ExternalDataSubscription>" %>

<%
Customer cust = MonnitSession.CurrentCustomer;
%>

<div class="col-12 view-btns_container shadow-sm rounded mb-5">
	<div class="view-btns deviceView_btn_row rounded">
		<a href="/api/RestAPI/" class="deviceView_btn_row__device">
			<div class="btn-<%:Request.Path.StartsWith("/api/RestAPI")?"active-fill shadow mb-2":" " %> btn-lg btn-fill btn-secondaryToggle">
				<%=Html.GetThemedSVG("code") %><span class="extra" style="max-width: 80px!important; white-space: normal; overflow-wrap: break-word!important;"><%: Html.TranslateTag("API/_APILink|Rest API","Rest API")%></span>
			</div>
		</a>
			
		<a href="/api/ApiKeys/" class="deviceView_btn_row__device">
			<div class="btn-<%:Request.Path.StartsWith("/api/ApiKeys") ? "active-fill shadow mb-2" : " " %> btn-lg btn-fill btn-secondaryToggle">
				<%=Html.GetThemedSVG("lock") %><span class="extra" style="max-width: 80px!important;  overflow-wrap: break-word!important;"><%: Html.TranslateTag("API/_APILink|API Keys","API Keys")%></span>
			</div>
		</a>
		
		<a href="/Export/DataPushInfo/" class="deviceView_btn_row__device">
			<div class="btn-<%:Request.Path.StartsWith("/Export/DataPushInfo")?"active-fill shadow mb-2":" " %> btn-lg btn-fill btn-secondaryToggle">
				<%=Html.GetThemedSVG("api") %>
				<span class="extra" style="max-width: 80px!important; white-space: normal; overflow-wrap: break-word!important;"><%: Html.TranslateTag("API/_APILink|Webhook API","Webhook API")%></span>
			</div>
		</a>
		
		<a href="/Export/DataPushHistory/<%:Model.ExternalDataSubscriptionID %>" class="deviceView_btn_row__device">
			<div class=" btn-<%:Request.Path.Contains("History") ? "active-fill shadow mb-2" : "secondaryToggle" %> btn-lg btn-fill btn-secondaryToggle">
				<%=Html.GetThemedSVG("list") %><span class="extra" style="max-width: 80px!important; white-space: normal; overflow-wrap: break-word!important;"><%: Html.TranslateTag("API/_APILink|Webhook History"," Webhook History")%></span>
			</div>
		</a>

		<%if (Model.ExternalDataSubscriptionID > 0)
			{ %>
		<a href="/Export/DataPushEdit/<%:Model.ExternalDataSubscriptionID %>" class="deviceView_btn_row__device">
			<div class="btn-<%:Request.Path.Contains("Configure") ? "active-fill shadshadow mb-2ow" : "secondaryToggle" %> btn-lg btn-fill btn-secondaryToggle">
				<%=Html.GetThemedSVG("settings") %><span class="extra" style="max-width: 80px!important; white-space: normal; overflow-wrap: break-word!important;"><%: Html.TranslateTag("API/_APILink|Configure Webhook","Configure Webhook")%></span>
			</div>
		</a>
		<%}
			else
			{ %>

		<a href="/Export/DataWebhook/" class="deviceView_btn_row__device">
			<div class="btn-<%:(Request.Path.StartsWith("/Export/DataWebhook/") || Request.Path.Contains("Configure")) ? "active-fill shadow mb-2" : "secondaryToggle" %> btn-lg btn-fill btn-secondaryToggle">
				<%=Html.GetThemedSVG("settings") %><span class="extra" style="max-width: 80px!important; white-space: normal; overflow-wrap: break-word!important;"><%: Html.TranslateTag("API/_APILink|Configure Webhook","Configure Webhook")%></span>
			</div>
		</a>

		<%} %>

		<a href="/Export/DataPushNotification/" class="deviceView_btn_row__device">
			<div class="btn-<%:Request.Path.StartsWith("/Export/DataPushNotification") ? "active-fill shadow mb-2" : "secondaryToggle" %> btn-lg btn-fill btn-secondaryToggle">
				<%=Html.GetThemedSVG("rules") %><span class="extra" style="max-width: 80px!important; white-space: normal; overflow-wrap: break-word!important;"><%: Html.TranslateTag("API/_APILink|Rule Webhook","Rule Webhook")%></span>
			</div>
		</a>
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