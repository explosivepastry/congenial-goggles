<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Monnit.ReportQuery>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminReportBuilder
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <%if (MonnitSession.IsCurrentCustomerMonnitAdmin)
            { %>
        <div class="top-add-btn-row media_desktop">            
            <a class="btn btn-primary" href="/Report/CleanUpOldReportsFromAzure/" id="cleanUpOldReportsFromAzureBtn" style="margin-right: 150px;">
                <%=Html.GetThemedSVG("delete") %> &nbsp; 
                <%: Html.TranslateTag("Settings/AdminReportBuilder|Delete Old Reports","Delete Old Reports")%>
            </a>

            <a class="btn btn-primary" href="/Settings/AdminReportBuilderCreate/">
                <%=Html.GetThemedSVG("add") %> &nbsp; 
                <%: Html.TranslateTag("Settings/AdminReportBuilder|Create Report","Create Report")%>
            </a>
        </div>
        <div class="bottom-add-btn-row media_mobile">
            <a class="add-btn-mobile" href="/Settings/AdminReportBuilderCreate/">
                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="16" height="15.999" viewBox="0 0 16 15.999">
                    <defs>
                        <clipPath id="clip-path">
                            <rect width="16" height="15.999" fill="none" />
                        </clipPath>
                    </defs>
                    <g id="Symbol_14_32" data-name="Symbol 14 – 32" clip-path="url(#clip-path)">
                        <path id="Union_1" data-name="Union 1" d="M7,16V9H0V7H7V0H9V7h7V9H9v7Z" transform="translate(0)" fill="#fff" />
                    </g>
                </svg>
            </a>
        </div>
        <%}%>


        <div class="card_container shadow-sm rounded">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <%: Html.TranslateTag("Settings/AdminReportBuilder|Report Management","Report Management")%>
                </div>
            </div>
            <div class="card_content__body">
                <div class="card_content__body__container" style="padding: 15px;">
                    <div class="clearfix"></div>
                </div>
            </div>

            <% bool alt = true;
                foreach (var item in Model)
                {
                    alt = !alt; %>
            <a href="/Settings/AdminReportBuilderEdit/<%:item.ReportQueryID%>" class="reportRow">
                <div class='<%: alt ? "alt" : "" %> gridPanel p-2 col-12'>
                    <div class="col-sm-2 col-12">
                        <%=  item.Name %>
                    </div>
                    <div class="col-sm-4 col-12 extra">
                        <%= item.Description %>
                    </div>
                    <div class="col-sm-1 col-12 extra">
                         <%if (item.AccountID > 0)
                            { try{ %>
                        <span title="<%: item.AccountID %>"><%= Account.Load(item.AccountID).CompanyName %></span>
                        <%} catch{}}%>
                    </div>
                    <div class="col-sm-1 col-12 extra">
                        <%if (item.AccountThemeID > 0)
                            { %>
                        <span title="<%: item.AccountThemeID %>"><%= AccountTheme.Load(item.AccountThemeID).Theme %></span>
                        <%} %>
                    </div>

                    <div class="col-md-3 col-sm-2 col-12 extra text-break">
                        <%: item.ReportBuilder %>
                    </div>

                    <div class="col-md-1 col-sm-2 col-12 dfjcfe">
                        <a href="/Settings/AdminReportBuilderEdit/<%:item.ReportQueryID%>">
                            <%=Html.GetThemedSVG("edit") %>
                        </a>
                    </div>
                </div>
            </a>
            <% } %>
        </div>
    </div>

    <script type="text/javascript">

        var AreYouSure = "<%: Html.TranslateTag("Settings/AdminReportBuilder|Are you sure you want to delete this report query?","Are you sure you want to delete this report query?")%>";

        $(function () {
            $('.delete').click(function (e) {
                e.preventDefault();

                if (confirm(AreYouSure)) {
                    $.get($(this).attr("href"), function (data) {
                        if (data != "Success") {
                            console.log(data);
                            showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                        }

                        window.location.href = window.location.href;
                    });
                }
            });

            $('#cleanUpOldReportsFromAzureBtn').click(function (e) {
                e.preventDefault();

                var obj = $(this);
                var oldHtml = $(this).html();
                $(this).html("<img alt='Loading...' src='/content/images/ajax-loader.gif'/>");

                $.post(this.href, function (data) {
                    if (data.startsWith('Success')) {
                        var msg = data.split('|')[1];
                        showSimpleMessageModal('Success\n' + msg);
                    } else {
                        showSimpleMessageModal(data);
                    }
                    obj.html(oldHtml);
                });
            });
        });
    </script>

    <style>
        .gridPanel:hover {
            background: #eee !important;
        }
    </style>

</asp:Content>
