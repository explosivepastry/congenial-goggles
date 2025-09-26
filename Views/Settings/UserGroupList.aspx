<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<CustomerGroup>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    UserGroupList
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <!-- page content -->
        <%Account account = ViewBag.account as Account;
        %>
        <div class="clearfix"></div>
        <div class="rule-card_container w-100" style="padding: 1rem;">
            <div class="card_container__top">
                <div class="card_container__top__title d-flex justify-content-between">
                    <span class="col-6 dfac card-icon-top">
                        <%=Html.GetThemedSVG("user-groups") %>
                        &nbsp;
                         <%: Html.TranslateTag("Network/List|Notification Groups","Notification Groups")%>
                    </span>
                    <div id="newRefresh" style="padding-left: 0px;">
                        <div style="float: right; margin-bottom: 5px;">
                            <div class="btn-group" style="height: 30px; gap: 16px;">
                                <a href="#" onclick="$('#settings').toggle(); return false;">
                                    <%=Html.GetThemedSVG("filter") %>
                                </a>
                                <a href="/" onclick="getMain(); return false;">
                                    <%=Html.GetThemedSVG("refresh") %>
                                </a>
                                <div class="media_desktop" style="padding-bottom: 0px;">
                                    <a href="/Settings/UserGroupEdit/" id="list" class="add-btn adduserList" style="padding: .05rem .75rem">
                                        <%=Html.GetThemedSVG("add") %>
                                        <%:Html.TranslateTag("Network/List|Add Group","Group")%>
                                    </a>
                                </div>
                                <div class="bottom-add-btn-row media_mobile">
                                    <a class="add-btn-mobile" href="/Settings/UserGroupEdit/">
                                        <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="16" height="15.999" viewBox="0 0 16 15.999">
                                            <defs>
                                                <clipPath id="clip-path">
                                                    <rect width="16" height="15.999" fill="none" />
                                                </clipPath>
                                            </defs>
                                            <path id="Union_1" data-name="Union 1" d="M7,16V9H0V7H7V0H9V7h7V9H9v7Z" transform="translate(0)" fill="#fff" />
                                        </svg>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End Form Title -->

            <div class="clearfix"></div>
            <div class="row" id="settings" style="display: none; padding: 5px 30px 15px 30px; align-items: center;">
                <div class="col-12 col-md-2" style="padding-top: 13px">
                    <strong><%: Html.TranslateTag("Filter","Filter")%>:</strong>
<%--                    <span id="filterdGroups"><%=ViewBag.UserGroupCount %></span><span id="totalGroups"><%=Model.Count %></span>--%>
                </div>
                <div class="col-12 col-md-3">
                    <input type="text" id="groupSearch" class="NameFilter form-control user-dets" placeholder="<%: Html.TranslateTag("Network/List|Group Name","Group Name")%>..." style="width: 260px; margin-top: 11px;" />
                </div>
                <div class="col-12 col-md-3">
                </div>
                <div class="col-12 col-md-3">
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="d-flex columnOnMobile" id="GroupList" style="flex-wrap: wrap">
                <%:Html.Partial("UserGroupDetails",Model) %>
            </div>
        </div>

    </div>

    <style>
        @media (max-width:662px) {
            .columnOnMobile {
                flex-direction: column;
            }

                .columnOnMobile .small-list-card {
                    width: 100%;
                    margin: .5rem 0;
                }
        }
    </style>

    <script src="/Scripts/events.js"></script>
    <!-- page content -->

    <script type="text/javascript">
        $(document).ready(function () {
            $('#groupSearch').keyup(function (e) {
                e.preventDefault();
                loadGroups();
            });
        });

        function loadGroups() {
            var query = $('#groupSearch').val();

            $.ajax({
                url: '/Settings/UserGroupFilter',
                type: 'post',
                async: false,
                data: {
                    "q": query
                },
                success: function (returndata) {
                    $("#GroupList").html(returndata);
                }
            });
        }

        function deleteGroup(groupID) {
            if (confirm("<%: Html.TranslateTag("Network/Edit|Are you sure you want to delete this group","Are you sure you want to delete this group")%>")) {
                $.post("/Settings/UserGroupDelete", { id: groupID }, function (data) {
                    if (data == 'Success') {
                        toastBuilder("Group deleted", "Success");
                        setTimeout(() => {
                            window.location.reload(); 
                        },500)
                    }
                    else {
                        console.log(data);
                        showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                    }
                });
            }
        }

    </script>

</asp:Content>
