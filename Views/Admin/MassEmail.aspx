<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage<List<iMonnit.Controllers.AdminController.MassEmailContact>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    MassEmail
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="thirdSection">
        <div class="blockSectionTitle">
            <div class="blockTitle">Recipient Account</div>
            <div style="clear: both;"></div>
        </div>
        <!-- Close Block Section Title -->
        <table class="table table-hover" id="AccountList" style="cursor: pointer; width: 100%;">
            <thead>
                <tr>
                    <th></th>
                    <th><a onclick="toggleAll()"><span style="font-weight: bold; cursor: pointer;">All</span></a></th>
                    <th>Acct. ID</th>
                    <th>Name</th>
                    <th></th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <%bool alt = true;
                    foreach (iMonnit.Controllers.AdminController.MassEmailContact contact in Model)
                    {
                        alt = !alt;
                %>
                <tr class="<%: alt ? "alt" : "" %> viewContactDetails">
                    <td></td>
                    <td>
                        <input class="checkbox themeboxes" type="checkbox" name="<%=contact.AccountThemeID%>" />
                    </td>
                    <td style="text-align: center;"><%=contact.AccountID %>
                    </td>
                    <td><%=contact.AccountName %>
                    </td>
                    <td></td>
                </tr>

                <!-- Detail Row Insert -->
                <tr class="holdContactDetails" style="display: none; border-top-width: 0px;" data-themeid="<%=contact.AccountThemeID %>">
                    <td colspan="4" style="padding-left: 4px;">
                        <div id="loadingGIF" class="text-center" style="display: none;">
                            <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
                                <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
                            </div>
                        </div>
                    </td>
                </tr>
                <%}%>
                <!-- Detail Row End -->
            </tbody>
        </table>
    </div>

    <div class="two-thirdSection">
        <div class="blockSectionTitle">
            <div class="blockTitle" id="MainTitle">Email</div>
            <div style="margin-top: 25px; margin-bottom: 5px;">
                <div id="MainRefresh">
                    <a href="#" onclick="refresh(); return false;">
                        <img src="<%:Html.GetThemedContent("/images/refresh.png")%>" alt="Refresh" title="Refresh" /></a>
                </div>
            </div>
            <div style="clear: both;"></div>
        </div>
        <!-- End blockSectionTitle -->

        <div class="two-thirdSectionInside">
            <div id="MassBody">
                <%Html.RenderPartial("MassEmailBody", new EmailTemplate());%>
            </div>
        </div>
        <!-- End two-thirdSection Inside -->
    </div>
    <!-- End two-thirdSection -->



    <link href="/suneditor/suneditor.min.css" rel="stylesheet" />
    <script type="text/javascript" src="/suneditor/suneditor.min.js"></script>


    <script type="text/javascript">
        $(document).ready(function () {
            $('.viewContactDetails').click(function () {
                var tr = $(this).next();
                var hide = tr.is(":visible");
                var ThemeID = tr.data('themeid');

                $('.viewContactDetails').css('border-bottom-width', '1px');
                $('.holdContactDetails').hide().children().empty().html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);

                if (!hide) {
                    $(this).css('border-bottom-width', '0px');
                    tr.show();

                    $.get("/Admin/MassEmailContactList?ThemeID=" + ThemeID, function (data) {
                        tr.children().html(data);
                    });
                }
            }).css('cursor', 'pointer');
        });

        function toggleAll() {
            $('.checkbox').each(function () {
                if (this.checked == false) {
                    this.checked = true;
                } else {
                    this.checked = false;
                }
            });
        }

        function refresh() {
            $.post('/Admin/MassEmailBody', function (data) {
                $('#MassBody').html(data)
            });
        }
    </script>

</asp:Content>
