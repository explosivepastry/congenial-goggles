<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Monnit.Sensor>>" %>
<% 
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();
%>
<script type="text/javascript">
    $(document).ready(function() {
        $(".witType").change(function() {
            sensorID = $(this).attr("id").toLowerCase().replace(/wittype/g, "");
            $.post("/Sensor/SetType/" + sensorID, { type: $(this).val() }, function(data) {
                $("#appIcon" + sensorID).attr("src", data);
            });
        });
    });
    
</script>

<table width="100%">
    <tr>
        <th>Serial Identifier (SID)</th>
        <th>Sensor Name</th>
        <th>Sensor Type</th>
        <th>Icon</th>
        <th>Icon Set</th>
    </tr>
    <%foreach (var item in Model)
       {%>
    <tr>
        <td>
            <%:  (item.SensorID) %>
        </td>
        <td>
            <%:  (item.SensorName) %>
        </td>
        <td>
            <%:  (item.MonnitApplication.ApplicationName) %>
        </td>
        <td>
            <img class="applicationIcon" id="appIcon<%: item.SensorID%>" style="height:30px; width:48px;" src="<%: Html.GetThemedContent(string.Format("/images/{1}/app{0}.png",item.ApplicationID, item.WitType.ToString()))%>" alt="Sensor" />
        </td>
        <td>
            <%: Html.DropDownList<eWitType>("WitType" + item.SensorID, "witType", item.WitType) %>
        </td>
    </tr>
    <% } %>
    
</table>
