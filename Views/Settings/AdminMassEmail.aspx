<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<List<iMonnit.Controllers.SettingsController.MassEmailContact>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminMassEmail
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="mt-4">
            <div class="col-md-6 col-12 pe-md-2">
                <div class="x_panel shadow-sm rounded mb-4">
                    <div class="dfac card_container__top__title">
                        <%: Html.TranslateTag("Settings/AdminMassEmail|Mass Email","Mass Email")%>
                    </div>
                    <form id="prefForm" class="form-horizontal form-label-left" method="post">
                        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>

                        <div class="bold col-lg-5 col-sm-7 col-6"><%: Html.TranslateTag("Name","Name")%></div>
                        <div class="bold col-sm-3 col-4" style="text-align: center;"><%: Html.TranslateTag("Settings/AdminMassEmail|Account ID","Acct. ID")%></div>
                        <div class="bold col-lg-2 hidden-md hidden-sm col-1"></div>
                        <div class="col-sm-2 col-1"><a onclick="toggleAll()"><span style="font-weight: bold; cursor: pointer; text-align: center;"><%: Html.TranslateTag("All","All")%></span></a></div>
                        <div class="clearfix"></div>
                        <hr />

                        <div>
                            <%bool alt = true;

                                foreach (iMonnit.Controllers.SettingsController.MassEmailContact contact in Model)
                                {
                                    alt = !alt;
                            %>
                            <div class="<%: alt ? "alt" : "" %> viewContactDetails">
                                <div class="col-lg-5 col-sm-7 col-6">
                                    <%=contact.AccountName %>
                                </div>
                                <div class="col-sm-3 col-4" style="text-align: center;">
                                    <%=contact.AccountID %>
                                </div>
                                <div class="col-lg-2 hidden-md hidden-sm col-1" style="text-align: center;">
                                </div>
                                <div class="col-sm-2 col-1" style="text-align: center;">
                                    <input style="min-height: 12px;" class="checkbox themeboxes" type="checkbox" name="<%=contact.AccountThemeID %>" />
                                </div>
                                <div class="clearfix"></div>
                            </div>

                            <!-- Detail Row Insert -->
                            <div class="holdContactDetails" style="display: none; border-top-width: 0px;" data-themeid="<%=contact.AccountThemeID %>">
                                <div style="padding-left: 4px;">
                                    <div id="loadingGIF" class="text-center" style="display: none;">
                                        <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
                                            <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr style="margin-top: 3px; margin-bottom: 3px;" />
                            <%}%>
                            <!-- Detail Row End -->
                        </div>
                    </form>
                </div>
            </div>
            <div class="col-md-6 col-12 ps-md-2">
                <div class="x_panel shadow-sm rounded">
                    <div class="two-thirdSectionInside">
                        <div id="MassBody">
                            <%Html.RenderPartial("_AdminMassEmailBody", new EmailTemplate());%>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End two-thirdSection -->
        </div>
    </div>

    <link href="/suneditor/suneditor.min.css" rel="stylesheet" />
    <script type="text/javascript" src="/suneditor/suneditor.min.js"></script>


    <script type="text/javascript">
        var pleaseSelect = "<%: Html.TranslateTag("Settings/AdminMassEmail|Please Select a Recipient Account")%>";

        $(document).ready(function () {
            $('#postEmail').click(function (data) {
                var ids = "";
                $('.themeboxes:checked').each(function () {
                    ids += this.name + '|';
                });

                if (ids.length > 0) {
                    var encoded = escape(sunEditorObj.getContents());
                    var postdata = "Subject=" + $('#subject').val();
                    postdata += "&EditorData=" + encoded;
                    postdata += "&IDs=" + ids
                    $.post('/Settings/buildEmail', postdata, function (data) {
                        alert(data);
                    });
                } else {
                    showSimpleMessageModal("<%=Html.TranslateTag("Please Select a Recipient Account")%>");
                }
            });

            $('#rtemplate').click(function (data) {
                gettemplate('ReleaseOneView');
            });

            $('#mtemplate').click(function (data) {
                gettemplate('Maintenance');
            });

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

                    $.get("/Settings/MassEmailContactList?ThemeID=" + ThemeID, function (data) {
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
            $.post('/Settings/MassEmailBody', function (data) {
                $('#MassBody').html(data)
            });
        }

        function gettemplate(type) {
            $.post('/Settings/MassEmailContent?flag=' + type, function (data) {
                $('#mailbody').html(data)
            });
        }

        var sunEditorObj = null;

    </script>

</asp:Content>
