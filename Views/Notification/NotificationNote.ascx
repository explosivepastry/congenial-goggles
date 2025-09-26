<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NotificationTriggered>" %>


    <%Notification noti = Notification.Load(Model.NotificationID);%>

    <% 
        string sensorName = "";

        SensorNotification sensNoti = SensorNotification.Load(Model.SensorNotificationID);
        if (sensNoti != null)
        {
            Sensor s = Sensor.Load(sensNoti.SensorID);
            if (s != null)
                sensorName = s.SensorName;
        }
    %>

    <div class="col-sm-6 col-12 pe-sm-2">
        <div class="" style="margin-top: 15px;">
            <div class="x_title">
                <h2><%: Html.TranslateTag("Events/NotificationNote|Create Note","Create Note") %></h2>
            </div>
            <div class="x_content">
                <form id="noteForm_<%=Model.NotificationTriggeredID%>">
                    <% //Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                    <div class="col-12">
                        <div class="form-group">
                            <div class="col-12">
                                <textarea id="note_<%=Model.NotificationTriggeredID%>" class="form-control" name="note" rows="5" placeholder="<%: Html.TranslateTag("Events/NotificationNote|Write a short note, then click add note","Write a short note, then click add note") %>..."></textarea>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 text-end">
                        <input id="AddButton" onclick="postNote(<%=Model.NotificationTriggeredID%>)" type="button" class="btn btn-primary btn-sm" value="<%: Html.TranslateTag("Add Note","Add Note") %>" />
                    </div>
                </form>
            </div>
        </div>
    </div>
    <div class="col-sm-6 col-12 ps-sm-2">
        <div class="" style="margin-top: 15px;">
            <div class="x_title">
                <h2><%: Html.TranslateTag("Events/NotificationNote|Notes","Notes") %></h2>
            </div>
            <div class="x_content mt-0" id="noteList_<%=Model.NotificationTriggeredID%>">
            </div>
        </div>
    </div>
    <script type="text/javascript">
        <%= ExtensionMethods.LabelPartialIfDebug("NotificationNote.ascx") %>
        //$(document).ready(function () {

            //$('#AddButton').click(function (e) {
            //    var body = $('#noteForm').serialize();
          //      var href = "/Notification/AddMessageNote/<%//=Model.NotificationTriggeredID%>";
        //            $.post(href, body, function (data) {
        //                if (data != "Success") {
        //                    alert(data);
        //                }
        //                else {
        //                    loadMessageNoteList(id);
        //                    $('#note').val('');
        //                }
        //            });
        //        });
        //});

        function postNote(id) {
            var body = $(`#noteForm_${id}`).serialize();
            var href = `/Notification/AddMessageNote/${id}`;
            $.post(href, body)
                .done(function (data) {
                    data != "Success" ? showAlertModal(data) : loadMessageNoteList(id);
                })
                .fail(function (data) {
                    //let code = data.status;
                    //let txt = data.statusText;
                    //let msg = $($.trim(data.responseText)).filter((x, y) => y.nodeName === 'TITLE').text();

                    //showAlertModal(code + '\r\n' + txt + '\r\n' + msg);
                    showAlertModal(data.statusText);
                })
            ;
        }

        function loadMessageNoteList(id) {
            // NotificationNote.ascx
            
            var notiID = '<%:Model.NotificationTriggeredID%>';
            $.post("/Notification/NotificationNoteList", { id: notiID }, function (data) {
                $(`#noteList_${id}`).html(data);
                $(`#note_${id}`).val('');
                if (!data.includes('svg_delete')) {
                    $(`#hasNotes_${id}`).hide();
                    $(`#noNotes_${id}`).show();
                }
                else {
                    $(`#hasNotes_${id}`).show();
                    $(`#noNotes_${id}`).hide();
                }
                $(`#adding_${id}`).hide();
                $(`#add_${id}`).show();
            });
        }
    </script>
