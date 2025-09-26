<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AdminAccountLinkModel>" %>



<%
    foreach (AccountSearchModel item in Model.searchModel)
    {
        bool isLinked = false;

        if (Model.customerAccountLink.Where(m => { return m.AccountID == item.AccountID && !m.AccountDeleted && !m.CustomerDeleted; }).Count() > 0)
            isLinked = true;
		%>
<a class="exsisting-rule" style="cursor: pointer;width:258px;" onclick="toggleAccount(<%:item.AccountID%>);">
	<div class=" triggerDevice__container">
		<div class="hidden-xs triggerDevice__icon">
			 <%=Html.GetThemedSVG("user") %>
		</div>
		<div class="triggerDevice__name" style="width:100%; word-break:break-word;">
			<strong><%:System.Web.HttpUtility.HtmlDecode(item.CompanyName) %></strong>
		        <% if (item.CompanyName != item.AccountNumber) {%>
                <span style="font-size: 0.8em;"><%= " : " + item.AccountNumber%></span>
                <%}else {%>

                <%} %>
		</div>
		<div class="triggerDevice__status ListBorder<%:isLinked ? "Active":"NotActive" %> linkAccount<%:item.AccountID%>">
	 <%=Html.GetThemedSVG("circle-check") %>
		</div>
	</div>
</a>
<%} %>