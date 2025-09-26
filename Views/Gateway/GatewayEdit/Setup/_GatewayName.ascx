<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Gateway>" %>


<div class="gateway-row-1">
	<div class="item-1">
		<%: Html.TranslateTag("Gateway/_GatewayName|Gateway Name","Gateway Name:")%>
	</div>

	<div class="item-2">
	    <input class="form-control" type="text" id="Name" name="Name" value="<%= Model.Name%>" />
      
        <%: Html.ValidationMessageFor(model => model.Name)%> 
	</div>    
      <div class="circleQuestion " data-bs-toggle="modal" data-bs-target="#gwModal">
            <%=Html.GetThemedSVG("circleQuestion") %>
        </div>
</div>

<!-- Modal -->
<div class="modal fade" id="gwModal" tabindex="-1" aria-labelledby="gwModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-sm">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="gwModalLabel"><%:Html.TranslateTag ("Gateway Name") %></h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" style="color: blue"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="word-def">
                  <%:Html.TranslateTag ("The") %> <strong style="color: rgb(17,104,173);"> <%:Html.TranslateTag ("Gateway Name") %> </strong> <%:Html.TranslateTag ("field is where you assign your gateway a unique title. By default, the gateway name will be the type followed by the Device ID.") %> 
                  </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="pwdx btn-secondary " data-bs-dismiss="modal"><%:Html.TranslateTag ("Close") %></button>
            </div>
        </div>
    </div>
</div>

