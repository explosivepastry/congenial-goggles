<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Gateway>" %>

	<div>

		<%Html.RenderPartial("_SetupStepper", Model.GatewayID); %>
		
		<%
			Dictionary<string, object> dic = new Dictionary<string, object>();
			//if (!Model.CanUpdate)
			//{
			//	dic.Add("disabled", "disabled");
			//	ViewData["disabled"] = true;
			//}

			ViewData["HtmlAttributes"] = dic;%>


        <div class="setup_device_design">

            <h2><%:Html.TranslateTag ("Gateway Setup") %></h2>

			<h6><%:Html.TranslateTag ("Name Your Gateway and Set Heartbeat") %></h6>
         
					<form class="form-horizontal form-label-left" action="/Setup/GatewayEdit/<%:Model.GatewayID %>" id="simpleEdit_<%:Model.GatewayID %>" method="post">
						<%: Html.ValidationSummary(true)%>
						<input type="hidden" value="/Setup/GatewayEdit/<%:Model.GatewayID %>" name="returns" id="returns" />
                        <input type="hidden" name="SetDefaults" value="<%:ViewBag.SetDefaults %>" />
						<% Html.RenderPartial("~\\Views\\Gateway\\GatewayEdit\\Default\\_SimpleEdit.ascx", Model);%>
					</form>

         
				</div>
		


		
		</div>


