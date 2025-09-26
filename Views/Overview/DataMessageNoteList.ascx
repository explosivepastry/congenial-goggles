<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<DataMessageNote>>" %>

<%
    if (Model.Count == 0)
    {%>
<div style="font-size: 1.4em">
    <%: Html.TranslateTag("Overview/DataMessageNoteList|No notes for this data message","No notes for this data message") %>.
</div>
<div class="clearfix"></div>
<%}
    else
    { %>

<table class="table table-hover">
    <tbody>
        <%foreach (DataMessageNote note in Model)

            {%>
        <tr>
            <td>
                <%=note.NoteDate.OVToLocalDateTimeShort() %>
            </td>
            <td>
                <strong><%=Customer.Load(note.CustomerID).FullName %> - </strong><%=note.Note %>
            </td>
            <td>
                <div id="deleteBtn_<%=note.DataMessageNoteID %>" title="<%: Html.TranslateTag("Overview/DataMessageNoteList|Delete","Delete")%>" onclick="removeNote(<%=note.DataMessageNoteID %>, '<%:note.DataMessageGUID %>');" style="vertical-align: top; cursor: pointer;">
                    <%=Html.GetThemedSVG("delete") %>
                </div>
                <div style="display:none;" class="spinner-border spinner-border-sm text-danger" id="removeNote_<%=note.DataMessageNoteID %>" role="status">
                  <span class="sr-only"><%: Html.TranslateTag("Overview/DataMessageNoteList|Loading","Loading")%>...</span>
                </div>
            </td>
        </tr>
<%}
    } %>
    </tbody>
</table>

<script type="text/javascript">
    function removeNote(noteID, readingID) {

        let values = {};
        values.text = "<%: Html.TranslateTag("Are you sure you want to remove this sensor notes","Are you sure you want to remove this sensor notes")%>?";
        values.callback = function () {
            $(`#removeNote_${noteID}`).show();
            $(`#deleteBtn_${noteID}`).hide();
            $.post("/Overview/DeleteMessageNote", { id: noteID }, function (data) {
                if (data != "Success") {
                    console.log(data);
                }
                loadMessageNoteList(readingID);
                $(`#removeNote_${noteID}`).hide();
                $(`#deleteBtn_${noteID}`).show();
            });
        };
        openConfirm(values)

                //$.post("/Overview/DeleteMessageNote", { id: noteID }, function (data) {
                //    if (data != "Success") {
                //        console.log(data);
                //    }
                //    loadMessageNoteList(readingID);
                //    $(`#removeNote_${noteID}`).hide();
                //    $(`#deleteBtn_${noteID}`).show();
                //});
        
    }
</script>

