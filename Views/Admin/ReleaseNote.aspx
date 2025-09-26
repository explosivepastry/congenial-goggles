<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage<List<Monnit.ReleaseNote>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Release Note
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- //purgeclassic --%>
    <% 
        bool alt = true;
     %>
    <div>
    <div class="formtitle">Release Note List
        <% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
            { %>
        <a class="bluebutton" href="/Admin/CreateReleaseNote" style="margin-top: -5px;">Create</a>
        <%} %>
    </div>
        <div class="formbody">
    <table class="releaseNote" width="100%">
        <thead>
            <tr>
                <th width="20"></th>

                <th width="150">Description
                </th>

                <th width="75">Version
                </th>

                <th width="75">Date Created
                </th>

                

                <th width="100"></th>
            </tr>
        </thead>
        <tbody>
            <%if (Model != null)
                { %>
                 <%foreach (var info in Model)
                     {
                         alt = !alt;
                       %>
                       <tr class="releaseNoteDetails <%:alt ? "alt" : ""%>"">
                            <td></td>
                            <td style="vertical-align:top;" title="<%: info.ReleaseNoteID%>">
                                <%: info.Description%>
                            </td>
                            <td style="vertical-align:top;">
                                <%: info.Version%>
                                  <%if (info.IsDeleted == true)
                                { %>
                                <span style="color:red;"> Deleted</span>
                                <%} %>
                            </td>
                            <td style="vertical-align:top;">

                                <%: info.ReleaseDate.ToShortDateString() %>
                            </td>
                           
                            <td style="vertical-align:top;">
                                  <a class="stopProp" target="_blank" href="/Admin/ViewReleaseNoteTest?Version=<%:info.Version%>&dateTime=<%:info.ReleaseDate%>"><img style="width:40px;" src="../../Content/images/Notification/view.png" alt="View Note" title="View Note" /></a>
                           
                               <% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
                                   { %>
                                    <a class="stopProp" href="/Admin/EditReleaseNote/<%: info.ReleaseNoteID %>"><img style="width:40px" src="../../Content/images/Notification/pencil.png" title="Edit" alt="Edit"/></a>
                                <a class="stopProp" href="/Admin/DeleteReleaseNote/<%: info.ReleaseNoteID %>"><img style="width:40px; clear:both" src="../../Content/images/Notification/trash.png" title="Delete" alt="Delete"/></a>
                               <%} %>
                             </td>
                        </tr>   

            <!-- Detail Row Insert -->
            <tr class="<%: alt ? "alt" : "" %> holdAccountThemeLinkData" style="display:none; border-top-width: 0px;" data-releasenoteid="<%:info.ReleaseNoteID %>">
             
                <td colspan="9" style="padding:10px 30px 30px 30px;">
<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>                </td>
                
            </tr>
            <!-- Detail Row End -->
                            
                  <% }
                      }%>
        </tbody>
    </table>
            
    <script type="text/javascript">
        $(document).ready(function () {
            $('.stopProp').click(function (e) {
                e.stopPropagation();
            });

            $('.releaseNoteDetails').click(function () {


                var tr = $(this).next();
                var hide = tr.is(":visible");
                var noteID = tr.data('releasenoteid');

                $('.releaseNoteDetails').css('border-bottom-width', '1px');
                $('.holdAccountThemeLinkData').hide().children().empty().html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);

                if (!hide) {
                    $(this).css('border-bottom-width', '0px');
                    tr.show();

                    $.get("/Admin/ReleaseNoteDetails/" + noteID, function (data) {
                        tr.children().html(data);
                    });

                }

            }).css('cursor', 'pointer');


        });
    </script>
            </div>
   </div>
</asp:Content>
