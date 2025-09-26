<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Account>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    UserList
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%string language = "en-us";
        if (Request.QueryString["language"] != null)
            language = Request.QueryString["language"].ToString(); %>

    <%List<Customer> custList = Customer.LoadAllByAccount(Model.AccountID); %>

    <div class="d-flex media_desktop">
        <%if ((Model.CurrentSubscription.Can(Feature.Find("muliple_users")) && MonnitSession.CustomerCan("Customer_Create") && MonnitSession.CurrentCustomer.IsAdmin) || MonnitSession.CustomerCan("Support_Advanced") || custList.Count < 1)
            {  %>
        <a href="/Settings/UserGroupList/<%:Model.AccountID %>" id="userGroup" class="add-btn adduserList" style="background: #90979e; margin-left: auto;">
<%--            <%=Html.GetThemedSVG("add") %> &nbsp; --%>
            <%: Html.TranslateTag("Notification Groups","Notification Groups",language)%>
        </a>
        <a href="/Settings/UserCreate/<%:Model.AccountID %>" id="newUser" class="btn btn-primary user-dets adduserList" style="margin-left: 10px;">
            <%=Html.GetThemedSVG("add") %> &nbsp; 
                <%: Html.TranslateTag("Add User","Add User",language)%>
        </a>
        <%} %>
    </div>

    <div class="container-fluid">
        <div class="bottom-add-btn-row media_mobile" style="box-shadow: none;">
            <%
                if (MonnitSession.CurrentTheme.Theme.ToLower() == "canteen")
                {
            %>
            <a href="/Settings/UserGroupList/<%:Model.AccountID %>" id="userGroup" class="add-btn-mobile" style="background: #90979e; margin-bottom: 5px; box-shadow: 0px 6px 8px 0px rgba(0,0,0,0.5);">
                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="15" viewBox="0 0 26 18.06">
                    <g id="Group_918" data-name="Group 918" transform="translate(-524 -669)">
                        <path id="ic_person_24px" d="M10,10A3,3,0,1,0,7,7,3,3,0,0,0,10,10Zm0,1.5c-2,0-6,1.005-6,3V16H16V14.5C16,12.505,12,11.5,10,11.5Z" transform="translate(520 671.06)" style="fill: #fff;" />
                        <path id="ic_person_24px-2" data-name="ic_person_24px" d="M10,10A3,3,0,1,0,7,7,3,3,0,0,0,10,10Zm0,1.5c-2,0-6,1.005-6,3V16H16V14.5C16,12.505,12,11.5,10,11.5Z" transform="translate(527 671.06)" style="fill: #fff;" />
                        <path id="ic_person_24px-3" data-name="ic_person_24px" d="M10,10A3,3,0,1,0,7,7,3,3,0,0,0,10,10Zm0,1.5c-2,0-6,1.005-6,3V16H16V14.5C16,12.505,12,11.5,10,11.5Z" transform="translate(534 671.06)" style="fill: #fff;" />
                        <path id="ic_check_box_outline_blank_24px" d="M23.059,5.227V8.313L5.507,8.451V5.227H23.059m0-2.227H5.507A2.532,2.532,0,0,0,3,5.227v4.4A4,4,0,0,1,5.507,8.451l17.552-.138a3.881,3.881,0,0,1,2.507,1.316v-4.4A2.532,2.532,0,0,0,23.059,3Z" transform="translate(522.717 666)" style="fill: #fff;" />
                    </g>
                </svg>
            </a>
            <%} %>

            <a class="add-btn-mobile" href="/Settings/UserCreate/<%:Model.AccountID %>" id="newUser" style="box-shadow: 0px 6px 8px 0px rgba(0,0,0,0.5);">
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

        <div class="clearfix"></div>
        <!-- User List View -->
        <div class="rule-card_container w-100">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <span class="col-6">
                        <%=Html.GetThemedSVG("recipients") %>
                        <%: Html.TranslateTag("Users","Users",language)%>
                        <span style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; color: #707070; font-size: small; position: sticky; margin-left: 45px;">[<%= MonnitSession.CurrentCustomer.FirstName%> <%= MonnitSession.CurrentCustomer.LastName%>] - <%=MonnitSession .CurrentCustomer.UserName%></span>
                    </span>

                    <div class="col-6 d-flex justify-content-end" id="newRefresh" style="padding-left: 0px;">
                        <div style="float: right; margin-bottom: 5px;">
                            <div class="btn-group" style="height: 30px;">
                                <%if (Model.CurrentSubscription.Can(Feature.Find("link_users")) || MonnitSession.CustomerCan("Support_Advanced"))
                                    { %>
                                <a href="/Settings/AccountLinkUser/<%:Model.AccountID %>" id="AddExisting" title="Link User">
                                    <%=Html.GetThemedSVG("link") %>
                                </a>
                                <%} %>
                                <a href="#" class="mx-2" onclick="$('#settings').toggle(); return false;" title="Filter">
                                    <%=Html.GetThemedSVG("filter") %>
                                </a>
                                <a href="/" onclick="getMain(); return false;" title="Refresh">
                                    <%=Html.GetThemedSVG("refresh") %>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row" id="settings" style="display: none; padding: 5px 30px 15px 30px;">
                <div class="col-12 col-md-6 col-lg-2" style="padding-top: 13px">
                    <strong><%: Html.TranslateTag("Filter","Filter",language)%>: &nbsp;</strong>
                    <span id="filterdUsers"></span>/<span id="totalUsers"><%=custList.Count %></span>
                </div>

                <div class="col-12 col-md-6 col-lg-3" style="padding: 2px">
                    <input type="text" id="userSearch" class="form-control" placeholder="<%: Html.TranslateTag("Settings/AccountUserList|User Name","User Name")%>..." style="width: 250px;" />
                </div>

                <div class="col-12 col-md-6 col-lg-3">
                </div>

                <div class="col-12 col-md-6 col-lg-3">
                    <select id="userFilter" class="form-select" style="width: 250px;">
                        <option value=""><%: Html.TranslateTag("Settings/AccountUserList|All Users","All Users",language)%></option>
                        <option value="1"><%: Html.TranslateTag("Settings/AccountUserList|Admin","Admin",language)%></option>
                        <option value="2"><%: Html.TranslateTag("Settings/AccountUserList|Basic","Basic",language)%></option>
                        <option value="3"><%: Html.TranslateTag("Settings/AccountUserList|Linked","Linked",language)%></option>
                    </select>
                </div>
            </div>

            <div id="toggleScroll" class="card_container__body bsInset">
                <div class="card_container__body__content">
                    <div id="UserList" style="min-height: 85px;">
                        <%:Html.Partial("UserListDetails",custList) %>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--container_fluid--%>

    <script src="/Scripts/events.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#userSearch').keyup(function (e) {
                e.preventDefault();
                loadUsers();
            });
            $('#userFilter').change(function (e) {
                e.preventDefault();
                loadUsers();
            });
        });

        function loadUsers() {
            var query = $('#userSearch').val();
            var type = $('#userFilter').val();
            var accID = <%=Model.AccountID%>;
            $.post("/Settings/UserFilter", { id: accID, userType: type, q: query }, function (data) {
                if (data == "Failed") {
                    showSimpleMessageModal("<%=Html.TranslateTag("Failed")%>");
                } else {
                    $('#UserList').html(data);
                }
            });
        }

        $('.btn-secondaryToggle').hover(
            function () { $(this).addClass('active-hover-fill') },
            function () { $(this).removeClass('active-hover-fill') }
        )

    </script>

</asp:Content>

