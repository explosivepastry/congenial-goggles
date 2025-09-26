<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Monnit.MaintenanceWindow>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Maintenance Windows
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- //purgeclassic --%>
    <% 
        bool alt = true;
    %>
    <div id="fullForm" style="width: 100%;">
        <div class="formtitle">
            Upcoming Maintenance Windows
         <%if (MonnitSession.IsCurrentCustomerMonnitAdmin)
             { %>
            <a href="/Admin/CreateMaintenanceWindow" style="margin-top: -7px;" class="bluebutton">Create New Window</a>
            <%} %>
        </div>
        <div class="formbody">
            <table width="100%" style="border-top: 1px #e2e2e2 solid;">
                <tr>
                    <th width="20px"></th>
                    <th>Display Date
                    </th>
                    <th>Display Until
                    </th>
                    <th>Description
                    </th>
                    <th>Email Body</th>
                    <th></th>
                    <th width="20px"></th>
                </tr>

                <% if (Model != null)
                    {
                        foreach (var item in Model)
                        {
                            alt = !alt;
                %>
                <tr class="MaintenanceNoteDetails <%:alt ? "alt" : ""%>">
                    <td></td>
                    <td>
                        <%: item.DisplayDate.ToShortDateString()%>
                    </td>
                    <td>
                        <%: item.HideDate.ToShortDateString()%>
                    </td>
                    <td>
                        <%: Html.Raw(item.Description)%>
                    </td>
                    <td>
                        <%: Html.Raw(item.EmailBody)%>
                    </td>
                    <td>
                        <%if (MonnitSession.IsCurrentCustomerMonnitAdmin)
                            { %>
                        <a href="/Admin/EditMaintenanceWindow/<%:item.MaintenanceWindowID%>">
                            <img style="width: 40px" src="../../Content/images/Notification/pencil.png" title="Edit" alt="Edit" /></a>
                        <%} %>
                    </td>
                    <td></td>
                </tr>
                <!-- Detail Row Insert -->
                <tr class="<%: alt ? "alt" : "" %> holdAccountThemeLinkData" style="display: none; border-top-width: 0px;" data-maintancenoteid="<%:item.MaintenanceWindowID %>">

                    <td colspan="9" style="padding: 10px 30px 30px 30px;">
                        <div id="loadingGIF" class="text-center" style="display: none;">
                            <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
                                <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
                            </div>
                        </div>
                    </td>

                </tr>
                <!-- Detail Row End -->
                <% }
                    } %>
            </table>
        </div>

        <div class="buttons" style="height: 45px; margin-top: 0px;">
            <%--<%if (MonnitSession.IsCurrentCustomerMonnitAdmin) { %>
        <input type="button" onclick="$.get('/Generic/ClearTimedCache', function (data) { alert(data); });" class="greybutton" value="Clear Server Cache" />
        <%}%>--%>
        </div>
    </div>

    <link href="/suneditor/suneditor.min.css" rel="stylesheet" />
    <script type="text/javascript" src="/suneditor/suneditor.min.js"></script>

    <%--<link href="https://cdn.jsdelivr.net/npm/suneditor@latest/dist/css/suneditor.min.css" rel="stylesheet">
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/suneditor@latest/dist/suneditor.min.js"></script>
    <!-- languages (Basic Language: English/en) -->
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/suneditor@latest/src/lang/ko.js"></script>

    <!-- KaTeX (^0.11.1) https://github.com/KaTeX/KaTeX -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/katex@0.11.1/dist/katex.min.css">
    <script src="https://cdn.jsdelivr.net/npm/katex@0.11.1/dist/katex.min.js"></script>--%>

    <script type="text/javascript">
        $(document).ready(function () {

            $('.sf-with-ul').removeClass('currentPage');
            $('#MenuMaint').addClass('currentPage');


            $('.stopProp').click(function (e) {
                e.stopPropagation();
            });

            $('.MaintenanceNoteDetails').click(function () {
                var tr = $(this).next();
                var hide = tr.is(":visible");
                var noteID = tr.data('maintancenoteid');

                $('.MaintenanceNoteDetails').css('border-bottom-width', '1px');
                $('.holdAccountThemeLinkData').hide().children().empty().html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);

                if (!hide) {
                    $(this).css('border-bottom-width', '0px');
                    tr.show();

                    $.get("/Admin/MaintanceNoteDetails/" + noteID, function (data) {
                        tr.children().html(data);
                    });
                }

            }).css('cursor', 'pointer');
        });
    </script>
</asp:Content>
