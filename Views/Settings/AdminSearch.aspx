<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Account>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminAccountSearch
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <div class="x_panel shadow-sm rounded mb-4">
            <div class="x_title">
                <div class="dfac" style="overflow: visible;">
                    <a id="actSearch_lbl" class="searchLink dfjcac search-tabs__tab" style="cursor: pointer; width: 80px;">
                        <%: Html.TranslateTag("Settings/AdminSearch|Account","Account")%>
                    </a>

                    <a id="usrSearch_lbl" class="searchLink dfjcac search-tabs__tab" style="cursor: pointer; width: 80px;">
                        <%: Html.TranslateTag("Settings/AdminSearch|User","User")%>
                    </a>

                    <a id="devSearch_lbl" class="searchLink dfjcac search-tabs__tab" style="cursor: pointer; width: 80px;">
                        <%: Html.TranslateTag("Settings/AdminSearch|Device","Device")%>
                    </a>
                </div>
                <div class="clearfix"></div>
            </div>
            <div id="actSearch_Div" class="col-12 Search_Div">
                <div class="dfac input-group">
                    <input type="text" id="actSearch_Box" class="form-control" style="max-width: 300px;" placeholder="<%: Html.TranslateTag("Account","Account")%>" onkeypress="enterSubmit(event,'actSearch_Button')" />
                    <button type="button" id="actSearch_Button" class="btn btn-primary" value="<%: Html.TranslateTag("Search","Search")%>">
                        <svg xmlns="http://www.w3.org/2000/svg" width="15" height="15" viewBox="0 0 17.49 17.49">
                            <path id="ic_zoom_in_24px" d="M15.5,14h-.79l-.28-.27a6.51,6.51,0,1,0-.7.7l.27.28v.79l5,4.99L20.49,19Zm-6,0A4.5,4.5,0,1,1,14,9.5,4.494,4.494,0,0,1,9.5,14Z" transform="translate(-3 -3)" style="fill: #fff;" />
                        </svg>
                    </button>
                    <button type="button" id="actSearch_Spinner" class="btn btn-primary" style="display: none;" disabled>
                        <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                        <span class="visually-hidden"><%: Html.TranslateTag("Settings/AdminSearch|Loading...","Loading...")%></span>
                    </button>
                    <button type="button" id="allAccts_Button" class="btn btn-primary" value="<%: Html.TranslateTag("Settings/AdminSearch|Show All","Show All")%>">
                        <span>All</span>
                    </button>
                    <button type="button" id="allAccts_Spinner" class="btn btn-primary" style="display: none;" disabled>
                        <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                        <span class="visually-hidden"><%: Html.TranslateTag("Settings/AdminSearch|Loading...","Loading...")%></span>
                    </button>
                </div>
            </div>

            <div id="usrSearch_Div" class="col-12 Search_Div" style="display: none;">
                <div class="dfac input-group">
                    <input type="text" id="usrSearch_Box" style="max-width: 300px;" value="" class="form-control" placeholder="<%: Html.TranslateTag("Settings/AdminSearch|User","User")%>" onkeypress="enterSubmit(event,'usrSearch_Button')" />
                    <button type="button" id="usrSearch_Button" class="btn btn-primary" value="<%: Html.TranslateTag("Search","Search")%>">
                        <svg xmlns="http://www.w3.org/2000/svg" width="15" height="15" viewBox="0 0 17.49 17.49">
                            <path id="ic_zoom_in_24px" d="M15.5,14h-.79l-.28-.27a6.51,6.51,0,1,0-.7.7l.27.28v.79l5,4.99L20.49,19Zm-6,0A4.5,4.5,0,1,1,14,9.5,4.494,4.494,0,0,1,9.5,14Z" transform="translate(-3 -3)" style="fill: #fff;" />
                        </svg>
                    </button>
                    <button type="button" id="usrSearch_Spinner" class="btn btn-primary" style="display: none;" disabled>
                        <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                        <span class="visually-hidden"><%: Html.TranslateTag("Settings/AdminSearch|Loading...","Loading...")%></span>
                    </button>
                </div>
                <%--<div class="col-md-3 col-12" style="margin-top: 0px; float: right;">
            </div>--%>
            </div>

            <div id="devSearch_Div" class="col-12 Search_Div" style="display: none;">
                <div class="dfac input-group">
                    <input type="text" id="devSearch_Box" class="form-control" style="max-width: 300px;" placeholder="<%: Html.TranslateTag("Settings/AdminSearch|Device","Device")%>" onkeypress="enterSubmit(event,'devSearch_Button')" />
                    <button type="button" id="devSearch_Button" class="btn btn-primary" value="<%: Html.TranslateTag("Settings/AdminSearch|Search","Search")%>">
                        <svg xmlns="http://www.w3.org/2000/svg" width="15" height="15" viewBox="0 0 17.49 17.49">
                            <path id="ic_zoom_in_24px" d="M15.5,14h-.79l-.28-.27a6.51,6.51,0,1,0-.7.7l.27.28v.79l5,4.99L20.49,19Zm-6,0A4.5,4.5,0,1,1,14,9.5,4.494,4.494,0,0,1,9.5,14Z" transform="translate(-3 -3)" style="fill: #fff;" />
                        </svg>
                    </button>
                    <button type="button" id="devSearch_Spinner" class="btn btn-primary" style="display: none;" disabled>
                        <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                        <span class="visually-hidden"><%: Html.TranslateTag("Settings/AdminSearch|Loading...","Loading...")%></span>
                    </button>
                </div>
                <%--<div class="col-md-3 col-12" style="margin-top: 0px; float: right;">
            </div>--%>
            </div>
        </div>

        <div id="actSearch_Result" class="Result_Div" style="display: none;">
            <div id="actSearch_List" class="x_panel shadow-sm rounded scrollParentLarge">
            </div>
        </div>

        <div id="usrSearch_Result" class="Result_Div" style="display: none;">
            <div id="usrSearch_List" class="rule-card_container w-100 scrollParentLarge">
            </div>
        </div>

        <div id="devSearch_Result" class="Result_Div" style="display: none;">
            <div id="devSearch_List" class="rule-card_container w-100">
            </div>
        </div>

    </div>

    <script type="text/javascript">
        function viewCustomer(anchor) {
            newModal("Primary Contact", "", 100, 400);
            modalDiv.html($(anchor).attr("data-details"));
        }
        function enterSubmit(e, button) {
            if (e.keyCode === 13) { // keycode 13 is the "Enter" key
                e.preventDefault(); // Ensure it is only this code that runs
                $('#' + button).click();
            }
        }

        var searchTimeout = null;
        var searchRequest = null;
        var numericOnly = "<%: Html.TranslateTag("Settings/AdminSearch|Only numeric values are permitted","Only numeric values are permitted")%>";
        var emptyBox = "<%: Html.TranslateTag("Settings/AdminSearch|Search field cannot be blank","Search text field cannot be blank")%>";
        var viewFailed = "<%: Html.TranslateTag("Settings/AdminSearch|view account failed.","view account failed.")%>";

        $(document).ready(function () {
            // Tabs to switch between 3 search categories
            $('.searchLink').click(function (e) {
                e.preventDefault();
                $('.searchLink').removeClass("search-tabs__tab__active");
                $('.Search_Div').hide();
                $('.Result_Div').hide();
                $('input[type = "text"]').val("");
                var LinkID = this.id;
                $('#' + LinkID).addClass("search-tabs__tab__active");
                var searchCategory = LinkID.split('_')[0];
                var divToShow = searchCategory + '_Div';
                $('#' + divToShow).show();
                var boxToFocus = searchCategory + '_Box';
                $('#' + boxToFocus).focus();
            });

            // acctSearch
            $('#actSearch_Box').on("input", function () {
                if (searchTimeout != null)
                    clearTimeout(searchTimeout);
                if (searchRequest != null) {
                    searchRequest.abort();
                    searchRequest = null;
                }
                $('.Result_Div').hide();
                $('#actSearch_List').html('');
                searchTimeout = setTimeout("$('#actSearch_Button').click();", 1000);
            });

            $('#actSearch_Button').click(function () {
                if (searchRequest != null) {
                    $('#allAccts_Button').toggle();
                    $('#allAccts_Spinner').toggle();
                    searchRequest.abort();
                    searchRequest = null;
                }
                $('#actSearch_Button').toggle();
                $('#actSearch_Spinner').toggle();
                $('.Result_Div').hide();
                searchRequest = $.get("/Settings/AdminAccountList?searchCriteria=" + encodeURIComponent($('#actSearch_Box').val()), function (data) {
                    $('#actSearch_Button').toggle();
                    $('#actSearch_Spinner').toggle();
                    $('#actSearch_List').html(data);
                    $('#actSearch_Result').show();
                    searchRequest = null;
                });
            });

            $('#allAccts_Button').click(function () {
                if (searchRequest != null) {
                    $('#actSearch_Button').toggle();
                    $('#actSearch_Spinner').toggle();
                    searchRequest.abort();
                    searchRequest = null;
                }
                $('#allAccts_Button').toggle();
                $('#allAccts_Spinner').toggle();
                $('.Result_Div').hide();
                searchRequest = $.get("/Settings/AdminAccountList?searchCriteria=Show All", function (data) {
                    $('#allAccts_Button').toggle();
                    $('#allAccts_Spinner').toggle();
                    $("#actSearch_List").html(data);
                    $('#actSearch_Result').show();
                    searchRequest = null;
                });
            });

            // usrSearch
            $('#usrSearch_Box').on("input", function () {
                if (searchTimeout != null)
                    clearTimeout(searchTimeout);
                if (searchRequest != null) {
                    searchRequest.abort();
                    searchRequest = null;
                }
                $('.Result_Div').hide();
                $('#usrSearch_List').html('');
                searchTimeout = setTimeout("$('#usrSearch_Button').click();", 1000);
            });

            $('#usrSearch_Button').click(function (e) {
                e.preventDefault();
                if ($('#usrSearch_Box').val().length <= 0) {
                    showSimpleMessageModal("<%=Html.TranslateTag("Search field cannot be blank.")%>");
                } else {
                    $('#usrSearch_Button').toggle();
                    $('#usrSearch_Spinner').toggle();
                    $(".Result_Div").hide();
                    var UserQuery = $('#usrSearch_Box').val();
                    $.post("/Settings/UserLookup?id=" + UserQuery, function (data) {
                        if (data == "Failed") {
                            console.log(data);
                            showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");

                            $('#usrSearch_Button').toggle();
                            $('#usrSearch_Spinner').toggle();
                        } else {
                            $('#usrSearch_Button').toggle();
                            $('#usrSearch_Spinner').toggle();
                            $('#usrSearch_List').html(data);
                            $('#usrSearch_Result').show();
                        }
                    });
                }
            });

            // devSearch
            $('#devSearch_Box').on("input", function () {
                if (searchTimeout != null)
                    clearTimeout(searchTimeout);
                if (searchRequest != null) {
                    searchRequest.abort();
                    searchRequest = null;
                }
                $('.Result_Div').hide();
                $('#devSearch_Box').html('');
                searchTimeout = setTimeout("$('#devSearch_Button').click();", 1000);
            });

            $('#devSearch_Button').click(function (e) {
                e.preventDefault();
                if ($('#devSearch_Box').val().length <= 0) {
                    showSimpleMessageModal("<%=Html.TranslateTag("Search field cannot be blank.")%>");
                } else if (isNaN($('#devSearch_Box').val())) {
                    showSimpleMessageModal("<%=Html.TranslateTag("Only numeric values are permitted")%>");
                }
                else {
                    $('#devSearch_Button').toggle();
                    $('#devSearch_Spinner').toggle();
                    $(".Result_Div").hide();
                    var DeviceID = $('#devSearch_Box').val();
                    $.post("/Settings/DeviceLookup/" + DeviceID, function (data) {
                        if (data == "Failed") {
                            console.log(data);
                            showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");

                            $('#modalCancel').hide();
                            $('#devSearch_Button').toggle();
                            $('#devSearch_Spinner').toggle();
                        } else {
                            $('#devSearch_Button').toggle();
                            $('#devSearch_Spinner').toggle();
                            $('#devSearch_List').html(data);
                            $('#devSearch_Result').show();
                        }
                    });
                }
            });

            if (queryString("q").length > 0) {
                tabToShow = queryString("tab");
                $('#acctSearch_Box').val(queryString("q"));
                searchTimeout = setTimeout("$('#acctSearch_Button').click();", 500);
            }

            $('#actSearch_lbl').click();
            $('.sf-with-ul').removeClass('currentPage');
            $('#MenuAccounts').addClass('currentPage');

            $('input:text').keydown(() => {
                if (event.keyCode == 37 || event.key == 'ArrowLeft') {
                    nextTab(-1)
                } else if (event.keyCode == 39 || event.key == 'ArrowRight') {
                    nextTab(1)
                }
            })
        });

        function nextTab(i) {
            let tabs = $('.search-tabs__tab')
            let nTabs = tabs.length
            let idx = $('.search-tabs__tab').filter('.search-tabs__tab__active').index()
            let nxtIdx = (idx + i) % nTabs
            if (nxtIdx < 0) {
                nxtIdx = (nTabs - 1)
            }
            tabs.get(nxtIdx).click()
        }

        var tryCount = 0;
        function viewAccount(lnk) {
            var anchor = $(lnk);
            var acctID = anchor.data('accountid');
            var href = anchor.attr('href');
            $('#proxyMessage_' + acctID).html("");
            var accessToken = $('#accessToken_' + acctID).val().trim();
            if (tryCount < 10) {
                tryCount++;
                if (accessToken.length == 6) {
                    $.post("/Settings/CheckAccessToken", { id: acctID, token: accessToken }, function (data) {
                        if (data == "Success") {
                            $.post(href, { id: acctID }, function (data) {
                                if (data == "Success") {
                                    tryCount = 0;
                                    window.location.href = "/Overview";
                                }
                                else {
                                    $('#proxyMessage_' + acctID).html('Proxy Failed');
                                }
                            });
                        } else {
                            $('#proxyMessage_' + acctID).html('Token Not Accepted');
                        }
                    });
                } else {
                    $('#proxyMessage_' + acctID).html('Invalid Token');
                }
            } else {
                $('#proxyMessage_' + acctID).html('Try Later');
            }
        }
    </script>

</asp:Content>
