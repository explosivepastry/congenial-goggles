<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ChangeLog>" %>

<%
//load all notes by ChangeLogID
%>
<% 
    if (Model != null)
    {
        List<ChangeLogItem> items = ChangeLogItem.LoadByChangeLog(Model.ChangeLogID);

        var list = items.OrderBy(x => x.Heading).ToList();
        int index = 0;

        foreach (ChangeLogItem item in list)
        {
            string color = "";
            string type = item.Type;
            switch (type)
            {
                case "Fix":
                    color = "#FFA667";
                    break;

                case "Updated":
                    color = "#B39DDB";
                    break;

                case "Improved":
                    color = "#50A8FF";
                    break;

                case "New":
                    color = "#13CF93";
                    break;
            }
            int i = index - 1;

%>

<%if (index > 0 && item.Heading.ToString() != list[i].Heading.ToString() || index == 0)
    {%>
<h5 id="heading_<%:item.ChangeLogItemID %>"><%:Html.Raw(item != null ? item.Heading : "") %></h5>
<% } %>
<div class="d-flex align-items-start">
    <div class="col-11 col-md-9 col-lg-8 col-xl-6 pt-1 d-flex">
        <h5 style="max-width: 100px;min-width: 100px;" class="text-end"><span class="badge me-1" style="background: <%:color%>" id="type_<%:item.ChangeLogItemID %>"><%:item != null ? item.Type : "" %></span></h5>
        <!-- DATA -->
        <div id="details_<%:item.ChangeLogItemID %>">
            <%:Html.Raw(item != null ? item.Details : "") %>
        </div>
    </div>
    <% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
        { %>
    <div class="my-auto col-1 d-flex text-end">
        <div onclick="editNote(<%:item.ChangeLogItemID %>, '<%:item.Heading %>')" style="cursor: pointer;"><%=Html.GetThemedSVG("edit") %></div>
        <div onclick="removeNote(<%:item.ChangeLogItemID %>)" style="cursor: pointer;" class="ms-1"><%=Html.GetThemedSVG("delete") %></div>
    </div>
    <% } %>
</div>

<% index++;
    }
        } %>

<style>
    ul {
        list-style-type: disc;
        margin-bottom: 0;
    }

    p {
        margin-bottom: 0;
    }
</style>
