<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.AnnouncementPopup>" %>

<%	
    if (Model != null && Model.AnnouncementObj != null)
    {
        var announcement = Model.AnnouncementObj;
        var isPreview = ((Model.Prev == long.MinValue && Model.Next == long.MinValue) && Model.CustomerViewed == false) ? true : false; // If both Prev or Next values are null it is a preview.
        var isPastAnnouncement = (Model.Prev != long.MinValue && Model.Next != long.MinValue) ? true : false;

        var showPopup = (
                        Model.CustomerViewed == false && announcement.ReleaseDate <= DateTime.UtcNow || //It hasn't been viewed and the release date is today or earlier
                        isPreview ||                                                                    //Its a preview from AdminAnnouncementIndex
                        isPastAnnouncement                                                              //Is being viewed from "View All Announcements"
                        ) ? true : false;
        var path = announcement.Link;
        var image = announcement.Image;

        if (showPopup)
        { 

            MonnitSession.CurrentCustomer.ShowPopupNotice = false;
            
%>
<div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered " role="document">
        <div class="modal-content ANmodal-content shadow-lg">
            <div class="modal-body ANmodal-top" style="padding: 0px; overflow: hidden;">
                <img src="<%=image%>" />
            </div>
            <div class="ANmodal-body">
                <div class="ANmodal-body__modalDescription">
                    <div class="ANmodal-body__modalDescription__text a_modalOutputs" id="outPutSubject"><%=announcement.Subject %></div>
                </div>
                <div class="modalANmodal-body__modalContainer">
                    <div class="ANmodal-body__modalTitle">
                        <div class="ANmodal-body__modalTitle__text a_modalOutputs" id="outPutTitle">
                            <%=announcement.Title %>
                        </div>
                    </div>
                    <div class="ANmodal-body__modalContent">
                        <div class="ANmodal-body__modalContent__text a_modalOutputs" id="outPutEditor">
                            <%=announcement.Content %>
                        </div>
                    </div>
                </div>
            </div>

            <div style="padding: 0 20px;" class="d-flex justify-space-between">
                <%if (Model.Prev > 0)
                    { %>
                <button class="prevbtn btn btn-primary btn-sm" data-dismiss="modal">Prev</button>
                <% }
                    if (Model.Next > 0)
                    { %>
                <button class="nextbtn btn btn-primary btn-sm ms-auto" data-announcementid="<%=Model.Next%>" data-dismiss="modal">Next</button>
                <%} %>
            </div>

            <div class="ANmodal-footer">
                <button id="modalClose" type="button" class="btn btn-secondary btn-sm" data-dismiss="modal">Close</button>

                <%if (announcement.Link != String.Empty)
                    {%>
                <%--<button id="modalViewLink" type="button" class="btn btn-primary btn-sm">View</button>--%>
                <a id="modalViewLink" class="btn btn-primary btn-sm" href="<%:announcement.Link %>" <%: announcement.Link.ToLower().StartsWith("http") ? "target=_blank" : "" %>>View</a>
                <%} %>

                <%--<div style="position: absolute; color: white; margin-top: 55px; margin-left: -20px;">
                    <%:Model.AnnouncementObj.ReleaseDate.ToString("MMMM.dd.yyyy") %>
                </div>--%>
            </div>
        </div>
    </div>
</div>
<%} %>

<script>

    $(document).ready(function () {

        ShowModal();

        $('#modalClose').click(function () {
            MarkAsViewed();
            HideModal();
        });

        $('#modalViewLink').click(function () {
            MarkAsViewed();
            HideModal();
        });

        $('.prevbtn').click(function () {
            MarkAsViewed();
            HideModal();
            PrevNextAnnouncement('prev');
        });

        $('.nextbtn').click(function () {
            MarkAsViewed();
            HideModal();
            PrevNextAnnouncement('next');
        });
    });

    function ShowModal() {
        $("#exampleModalCenter").modal("show");
    }

    function HideModal() {
        $("#exampleModalCenter").modal("hide");
    }

    function MarkAsViewed() {
        var id = <%=MonnitSession.CurrentCustomer.CustomerID%>;
        var accountthemeid = <%=MonnitSession.CurrentTheme.AccountThemeID%>;
        $.get(`/Settings/MarkReadByCustomerID?id=${id}&accountthemeid=${accountthemeid}`, function () {

        });
    }

    function PrevNextAnnouncement(val) {
        var customerid = <%=MonnitSession.CurrentCustomer.CustomerID%>;
        var accountthemeid = <%=MonnitSession.CurrentTheme.AccountThemeID%>;
        var announcementid = val == 'prev' ? <%:Model.Prev%> : <%:Model.Next%>;
        $.get(`/Settings/LoadAnnouncementForPopup?customerid=${customerid}&accountthemeid=${accountthemeid}&announcementid=${announcementid}`, function (data) {
            $('#AnnouncementModel').html(data);
            ShowModal();
        });
    }

</script>
<%} %>