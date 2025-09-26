<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<iMonnit.Models.DatabaseStatistics>>" %>

<table class="table table-hover">
    <thead>
        <tr>
            <th scope="col">
                <%: Html.TranslateTag("Report/DatabaseStatistics|TableName","Name/Status")%>
            </th>
            <th scope="col">
                <%: Html.TranslateTag("Report/DatabaseStatistics|Count","Count")%>
            </th>
            <th scope="col">
                <svg xmlns="http://www.w3.org/2000/svg" width="10" height="5" viewBox="0 0 10 5" class="">
                    <path id="ic_arrow_drop_down_24px" d="M7,10l5,5,5-5Z" transform="translate(-7 -10)" style="fill: #0067ab;" />
                </svg>
                <%: Html.TranslateTag("Report/DatabaseStatistics|Min","Min")%>
            </th>
            <th scope="col">
                <svg xmlns="http://www.w3.org/2000/svg" width="10" height="5" viewBox="0 0 10 5" class="">
                    <path id="ic_arrow_drop_up_24px" d="M7,14l5-5,5,5Z" transform="translate(-7 -9)" style="fill: #ff4d2d;" />
                </svg>
                <%: Html.TranslateTag("Report/DatabaseStatistics|Max","Max")%>
            </th>
        </tr>
    </thead>
    <tbody >
    <%foreach (iMonnit.Models.DatabaseStatistics item in Model)
        {
            String minThresh;
            String maxThresh;
            if (item.TableName == null) { item.TableName = ""; }
            if (item.Status == null) { item.Status = ""; }
            if (item.Counts < 0) { item.Counts = 0; }
            if (item.MinThresh < 0) { minThresh = ""; } else { minThresh = item.MinThresh.ToString(); }
            if (item.MaxThresh < 0) { maxThresh = ""; } else { maxThresh = item.MaxThresh.ToString(); } %>
    <tr style="cursor: pointer; width: 100%;" onclick="window.open('/Report/DatabaseDetails?tableName=<%=item.TableName %>&status=<%=item.Status %>', '_blank');">
        <th class="d-flex flex-column">
            <%= item.TableName %>
            <span class="grey-container" style="width: fit-content; padding: 2px 5px; border-radius: 3px;"><%=item.Status %></span>
        </th>
        <td>
            <%=item.Counts %>
        </td>
        <td>
            <div class="data-statistic-number-fill"><%= minThresh %></div>
        </td>
        <td>
            <div class="data-statistic-number-fill-red"><%= maxThresh %></div>
        </td>
    </tr>
    <%} %>
</tbody>
</table>