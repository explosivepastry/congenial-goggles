<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<ReportSchedule>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Reports
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <div class="top-add-btn-row media_desktop d-flex" style="justify-content:flex-end;">
            <a href="/Export/ReportCategory" class="btn btn-primary">
                <%=Html.GetThemedSVG("add") %>
                &nbsp;
        <%: Html.TranslateTag("Export/ReportIndex|Add Report","Add Report")%>
            </a>
        </div>

        <div class="bottom-add-btn-row media_mobile  ">
            <a href="/Export/ReportTemplates" class="add-btn-mobile">
                <%=Html.GetThemedSVG("add") %>
        
            </a>
        </div>

        <div class="rule-card_container w-100">
            <div class="row">
                <div class="card_container__top">
                    <div class="card_container__top__title dfac">
                        <%=Html.GetThemedSVG("book") %>
                        &nbsp;
                        <%: Html.TranslateTag("Reports","Reports")%>
                    </div>

                    <div id="MainTitle" style="display: none;"></div>
                    <div class="col-xs-6" id="newRefresh">
                        <div style="float: right; margin-bottom: 5px;">
                            <div class="dfac">
                                <a href="#" class="me-2" onclick="$('#settings').toggle(); return false;">
                                    <%=Html.GetThemedSVG("filter") %>
                                </a>
                                <a href="/" onclick="getMain(); return false;">
                                    <%=Html.GetThemedSVG("refresh") %>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="card_container__body">
                    <div class="card_container__body__content">
                        <div class="clearfix"></div>
                        <div style="margin: 5px">
                            <font color="gray">
<%--                    <%: Html.TranslateTag("Export/ReportIndex|Click on icon to enable/disable", "Click on icon to enable/disable")%>--%>
                </font>
                        </div>

                        <div class="filter-report-container" id="settings" style="display: none;  ">
                            <div class="" style="padding-top: 13px">
                                <strong><%: Html.TranslateTag("Filter:","Filter")%>: &nbsp;</strong>
                                <span id="filterdReports"><%=ViewBag.ReportCount %></span>/<span id="totalReports"><%=Model.Count %></span>
                            </div>

                            <div class="">
                                <input type="text" id="reportSearch" class="NameFilter form-control user-dets" placeholder="<%: Html.TranslateTag("Export/ReportIndex|Report Name...","Report Name...")%>" style="width: 250px;" />
                            </div>
                      

                            <div class="">
                                <select id="repFilter" class="form-select" style="width: 250px;">
                                    <option value=""><%: Html.TranslateTag("Export/ReportIndex|All Reports","All Reports")%></option>
                                    <option value="true"><%: Html.TranslateTag("Active","Active")%></option>
                                    <option value="false"><%: Html.TranslateTag("Inactive","Inactive")%></option>
                                </select>
                            </div>
                        </div>

                        <div class="clearfix"></div>
                        <div class="glanceView">
                            <div class="col-12 ReportList d-flex flex-wrap" id="hook-three">
                                <%:Html.Partial("ReportDetails",Model) %>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="/Scripts/events.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#reportSearch').keyup(function (e) {
                e.preventDefault();
                loadReports();
            });
            $('#repFilter').change(function (e) {
                e.preventDefault();
                loadReports();
            });
        });

        function loadReports() {
            var query = $('#reportSearch').val();
            var active = $('#repFilter').val();
            event.stopPropagation()

            $.ajax({
                url: '/Export/ReportFilter',
                type: 'post',
                async: false,
                data: {
                    "isActive": active,
                    "q": query
                },
                success: function (returndata) {
                    $(".ReportList").html(returndata);
                }
            });
        }
    </script>

</asp:Content>
