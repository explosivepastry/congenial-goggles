<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Monnit.InboundPacket>>" %>

<script type="text/javascript">
    $('.messages').click(function() {
        modalDiv = $('<div title="Data">')
        modalDiv.html($(this).attr("title"));
        modalDiv.dialog({
            modal: true
        });

    });
</script>
    <table>
        <tr>
            <th>
                Gateway ID
            </th>
            <th>
                Power
            </th>
            <th>
                Packet Type
            </th>
            <th>
                Message Count
            </th>
            <th>
                Date
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td title="CSNet: <%: item.CSNetID %>">
                <%: item.APNID %>
            </td>
            <td>
                <%: item.Power %>
            </td>
            <td>
                <%: item.Response %>
            </td>
            <td class="messages" title="<%: BitConverter.ToString(item.Payload).Replace("-","") %>">
                <%: item.MessageCount %>
            </td>
            <td>
                <%: String.Format("{0:g}", MonnitSession.MakeLocal(item.ReceivedDate)) %>
            </td>
        </tr>
    
    <% } %>

    </table>


