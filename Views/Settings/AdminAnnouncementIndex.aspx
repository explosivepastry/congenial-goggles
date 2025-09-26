<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Monnit.Announcement>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminAnnouncement
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <%if (MonnitSession.IsCurrentCustomerMonnitAdmin)
            { %>
        <div class="top-add-btn-row media_desktop">
            <a class="add-btn shadow-sm rounded" href="/Settings/AdminAnnouncementEdit/">
                <%=Html.GetThemedSVG("add") %>
                <span class="ms-1"><%: Html.TranslateTag("Settings/AdminAnnouncement|Announcement", "Announcement")%></span>
            </a>
        </div>
        <div class="bottom-add-btn-row media_mobile">
            <a class="add-btn-mobile" href="/Settings/AdminAnnouncementEdit/">
                <%=Html.GetThemedSVG("add") %> 
            </a>
        </div>
        <% } %>
        <div class="media-card-margin-top card_container shadow-sm rounded">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <%: Html.TranslateTag("Settings/AdminAnnouncement|Admin Announcement","Admin Announcement")%>
                </div>
            </div>
            <% if (Model != null)
                {%>
            <div class="card_container__body">
                <div class="card_container__body__content">
                    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>

                    <% if (Model != null)
                        {%>
                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th scope="col">
                                    <%: Html.TranslateTag("Subject", "Subject")%>
                                </th>
                                <th scope="col">
                                    <%: Html.TranslateTag("Release Date", "Release Date")%>
                                </th>
                                <th scope="col">
                                    <%: Html.TranslateTag("Account Theme ID", "Account Theme ID")%>
                                </th>
                                <th scope="col"></th>
                            </tr>
                        </thead>
                        <tbody>
                            <% foreach (var item in Model)
                                {%>

                            <%if (!item.IsDeleted)
                                { %>
                            <tr data-noteid="<%:item.AnnouncementID%>">
                                <td>
                                    <% if (item.IsDeleted)
                                        {%>
                                    <span title="Primary account contact" style="position: absolute; left: -20px; top: -16px;" class="badge bg-red"><%: Html.TranslateTag("Settings/AdminAnnouncement|Deleted", "Deleted")%></span>
                                    <%} %>
                                    <%: Html.Raw(item.Subject)%>
                                </td>
                                <td>
                                    <%: item.ReleaseDate.AddDays(1).OVToLocalDateShort()%>
                                </td>
                                <td>
                                    <%: (item.AccountThemeID > 0) ? item.AccountThemeID.ToString() : "NULL" %>
                                </td>
                                <td>
                                    <a class="noteView dfjcac" style="cursor: pointer;" data-announcementid="<%=item.AnnouncementID %>">
                                        <%=Html.GetThemedSVG("view") %> 
                                    </a>
                                </td>
                            </tr>
                            <% }
                            } %>
                        </tbody>
                    </table>
                    <%
                        }
                        else
                        { %>
                    <br />
                    <%: Html.TranslateTag("Settings/AdminAnnouncement|No Upcoming Maintenance Windows", "No Upcoming Maintenance Windows")%>
                    <% }
                    } %>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(function () {

            $('.noteView').click(function (e) {
                e.stopPropagation();
                var id = $(this).data('announcementid');
                DemoModal(id);
            });
            $('tr').click(function () {
                var announcementID = $(this).attr('data-noteid');
                window.location.href = '/Settings/AdminAnnouncementEdit/' + announcementID;
            })
                .hover(function () {
                    $(this).css('cursor', 'pointer');
                });
        });

        function DemoModal(id) {
            $.get("/Settings/AnnouncementPopupDemo/" + id, function (data) {
                $('#modelDemo').html(data);
            });
        }

    </script>
</asp:Content>
