<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Monnit.SensorFile>>" %>

<div class="title2">File List</div>
<div>
    <ul>
    <% foreach (var item in Model) { %>
        <li>
            <a href="/Sensor/DownloadFile/<%: item.SensorFileID %>" target="_blank"><%: MonnitSession.MakeLocal(item.Date).ToString(); %></a>
        </li>
    <% } %>
    </ul>
</div>