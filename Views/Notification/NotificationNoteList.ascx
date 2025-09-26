<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<NotificationNote>>" %>

<%
    if (Model.Count == 0)
    {%>
<div style="font-size: 1.4em">
    <%: Html.TranslateTag("Events/NotificationNoteList|No notes for this notification","No notes for this notification") %>.
</div>
<div class="clearfix"></div>
<%}
    else
    { %>
<table class="table table-hover">
    <thead>
        <tr>
            <th scope="col">Date Added</th>
            <th scope="col">User & Message</th>
            <th scope="col"></th>
        </tr>
    </thead>
    <tbody>

        <%foreach (NotificationNote notes in Model)
            {%>
        <tr>
            <td><%=notes.CreateDate.OVToLocalDateTimeShort() %></td>
            <td><strong><%=Customer.Load(notes.CustomerID).FullName %> - </strong><%=notes.Note %></td>
            <td class="text-end" id="add_<%:notes.NotificationNoteID %>" title="<%: Html.TranslateTag("Delete", "Delete")%>" >
                <div style="cursor:pointer;" onclick="removeNote(<%=notes.NotificationNoteID %>,<%:notes.NotificationTriggeredID %>);$('#adding_<%:notes.NotificationNoteID %>').show();$(this).hide();"><%=Html.GetThemedSVG("delete") %></div>
                <div style="display:none;" class="spinner-border text-danger spinner-border-sm" role="status" id="adding_<%:notes.NotificationNoteID %>">
                  <span class="visually-hidden">Loading...</span>
                </div>
            </td>
        </tr>
        <%}
            } %>
    </tbody>
</table>

<script>
    <%= ExtensionMethods.LabelPartialIfDebug("NotificationNoteList.ascx") %>
    //function removeNote(noteID) {
    //    let values = {};
    //    values.url = `/Notification/DeleteNotificationNote?id=${noteID}`;
    //    values.text = 'Are you sure you want to remove this note?';
    //    openConfirm(values);
    //} 

    function removeNote(noteID, id) {
        $.post("/Notification/DeleteNotificationNote", { id: noteID }, function (data) {
            if (data == "Success") {
                loadMessageNoteList(id);
            } else {
                console.log(data);
                let values = {};
                    <%--values.redirect = '/Ack/<%:Model.NotificationRecordedID%>/<%:Model.NotificationGUID%>';--%>
                values.text = "<%=Html.TranslateTag("Oops! That did not work, please refresh your page. If this error continues, contact support.")%>";
                openConfirm(values);
                $('#modalCancel').hide();
            }
        });
    }
</script>
