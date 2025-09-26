<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<CustomerGroup>>" %>

<%
    if (Model.Count < 1)
    {%>

<div class="row" style="padding-top: 15px; padding-left: 15px;">
    <%: Html.TranslateTag("Network/Details|No User Groups found for this account","No User Groups found for this account")%>.
</div>
<% }
    else
        foreach (CustomerGroup item in Model)
        {%>

<a class="d-flex" style="cursor: pointer;" href="/Settings/UserGroupEdit/<%: item.CustomerGroupID %>" title="<%: Html.TranslateTag("Network/Details|Click to Edit","Click to Edit")%>">
    <div class="small-list-card" style="padding: 0.5rem; align-items: center;">
        <div style="width: 36px;">
            <%=Html.GetThemedSVG("user-groups") %>
        </div>
        <div class="d-flex w-100" style="justify-content: center;">
            <div class="innerCard-holder__data__title">
                <div class="network_small-title" style="font-size: 13px; font-weight: 600;"><%=item.Name %></div>
            </div>
        </div>
        <div style="" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">
            <%=Html.GetThemedSVG("menu") %>
        </div>
        <ul class="dropdown-menu ddm">
            <li class="d-flex dropdown-item menu_dropdown_item" style="justify-content: space-between"
                href="/Settings/UserGroupEdit/<%: item.CustomerGroupID %>">
                <%: Html.TranslateTag("Settings","Settings")%>
                <span>
                    <%=Html.GetThemedSVG("settings") %>
                </span>
            </li>
            <li class="d-flex dropdown-item menu_dropdown_item" onclick="deleteGroup(<%=item.CustomerGroupID %>);" id="list" style="justify-content: space-between">
                <%: Html.TranslateTag("Events/Triggers|Delete Group", "Delete Group")%>
                <span>
                    <%=Html.GetThemedSVG("delete") %>
                </span>
            </li>
        </ul>
    </div>
</a>

<%} %>

<script type="text/javascript">
    $(document).ready(function () {
<%--        $('#filterdGroups').html('<%:ViewBag.UserGroupCount%>');--%>
    });
    var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
</script>
