<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Announcement>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminAnnouncemenEdit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <div class="card-margin-top col-12 mobile_modalFlip">
            <div class="card_container shadow-sm rounded col-md-6 col-12">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <%: Html.TranslateTag("Settings/AdminAnnouncementEdit|Create Announcement","Create Announcement")%>
                    </div>
                </div>

                <div class="card_container__body">
                    <div class="card_container__body__content">
                        <form id="prefForm" class="form-horizontal form-label-left" method="post">
                            <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                            <% Html.RenderPartial("_AdminAnnouncementForm", Model); %>
                        </form>
                    </div>
                </div>
            </div>

            <div class="col-md-6 col-12" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true" style="display: block;">
                <div class="modal-dialog shadow-sm modal-dialog-centered <%:Request.Path.Contains("AdminAnnouncementEdit") ? "mobile_modalFlip__modalMargin" : " "%>" role="document">
                    <div class="modal-content ANmodal-content">
                        <div class="modal-body ANmodal-top" style="padding: 0px; overflow: hidden;">
                            <img src="http://monnit.blob.core.windows.net/content/images/iMonnit/announcementRocket.png" />
                        </div>

                        <div class="ANmodal-body">
                            <div class="ANmodal-body__modalDescription">
                                <div class="ANmodal-body__modalDescription__text a_modalOutputs" id="outPutSubject"><%: Html.TranslateTag("Settings/AdminAnnouncementEdit|New Product Feature","New Product Feature")%></div>
                            </div>

                            <div class="modalANmodal-body__modalContainer">
                                <div class="ANmodal-body__modalTitle">
                                    <div class="ANmodal-body__modalTitle__text a_modalOutputs" id="outPutTitle">
                                       <%: Html.TranslateTag("Settings/AdminAnnouncementEdit|HX Credits (Sub 10 Minute Heart Beats)","HX Credits (Sub 10 Minute Heart Beats)")%>
                                    </div>
                                </div>

                                <div class="ANmodal-body__modalContent">
                                    <div class="ANmodal-body__modalContent__text a_modalOutputs" id="outPutEditor">
                                       <%: Html.TranslateTag("Settings/AdminAnnouncementEdit|With iMonnit HX feature enabled, users still have the same feature set as the iMonnit Premiere license with the ability to configure heartbeats from 1-9 minutes.","With iMonnit HX feature enabled, users still have the same feature set as the iMonnit Premiere license with the ability to configure heartbeats from 1-9 minutes.")%>
                                    <br />
                                        <%: Html.TranslateTag("Settings/AdminAnnouncementEdit|HX credit is equal to one heartbeat, or one data message.","HX credit is equal to one heartbeat, or one data message.")%> 
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="ANmodal-footer">
                            <button type="button" class="btn btn-secondary btn-sm" data-dismiss="modal"><%: Html.TranslateTag("Settings/AdminAnnouncementEdit|Close","Close")%></button>
                            <button id="modelViewLink" type="button" class="btn btn-primary btn-sm"><%: Html.TranslateTag("Settings/AdminAnnouncementEdit|View","View")%></button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-6 col-12" id="exampleViewLoad" tabindex="-1" role="dialog" style="display: block; max-height: 200px; max-width: 600px; margin-left: 72px;">
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $("iframe > body > p").addClass("editor");
        var subject = document.getElementById("subject");
        var desc = document.getElementById("title");
        var edt = document.getElementById("editor");

        $(this).on('input', function () {
            var x = subject.value;
            var y = desc.value;
            var z = edt.value;
            document.getElementById("outPutSubject").innerHTML = x;
            document.getElementById("outPutTitle").innerHTML = y;
            document.getElementById("outPutEditor").innerHTML = z;
        });

        $('#modelViewLink').click(function () {
            var path = $('#link').val();
            $.get("/Settings/AdminAnnouncementViewLinkTest?path=" + path, function (data) {
                $('#exampleViewLoad').html(data);
            });
        });
    </script>
</asp:Content>
