<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>
<% 
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();
%>
<div id="sensorTags">
<!--
<script type="text/javascript">
    $(function() {
        $('.removeTag').click(function(e) {
            e.preventDefault();
            ajaxDiv('sensorTags', $(this).attr('href'));
        });

        $('#addTag').click(function() {
            ajaxDiv('sensorTags', '/Sensor/AddTag/<%:Model.SensorID%>?tag=' + $('#newTag').val());
        });
    });
</script>
<div class="editor-label">
    Reporting Tags
</div>
<div class="editor-field">
    <table>
        <%foreach (string tag in Model.Tags)
          { %>
          <tr>
            <td><%:tag %></td>
            <td><a class="removeTag" href="/Sensor/RemoveTag/<%:Model.SensorID%>?tag=<%:tag %>">Remove</a></td>
          </tr>
        <%} %>
        <tr>
            <td><input id="newTag" /></td>
            <td><input type="button" value="Add Tag" id="addTag" /></td>
        </tr>
    </table>
</div>
-->
</div>