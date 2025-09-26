<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<iMonnit.Models.MissedSensorMessages>>" %>
<% 
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();
%>


<div style="width:100%; margin-right:-10px">

    <table class="w-100 ">
        <tr class="d-flex w-100" style="justify-content:space-between;">
            <th style="width:150px;">
                Expected Check-in
            </th>
            <th style="text-align:right;">
                Time Between Messages
            </th>
        </tr>
    </table>
</div>

<div style="overflow:auto; width:100%; max-height:300px; margin-right:-10px">
    <table width="100%">
    <% foreach (var item in Model) { %>
        <tr class="long-card d-flex w-100" style="justify-content:space-between; ">
            <td style="width:150px;">
                <%: item.ExpectedCheckIn.ToShortDateString()%>
                <%: item.ExpectedCheckIn.ToShortTimeString() %>
            </td>
            <td style="width:10%">
                <%if(item.Days > 0)
                  { %>
                    <%: item.Days %> Days    
                <%}
                  if (item.Hours > 0 || item.Days > 0)
                  { %>
                    <%: item.Hours %> Hours    
                <%} %>

                <%: item.Minutes %> Minutes
            </td>
        </tr>    
    <% } %>
    </table>
</div>