<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Account>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminAccountSearch
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% 
        if (Model == null)
        {

    %>
    <div class="col-12">
        <div class="rule-card_container w-100" id="hook-one" style="margin-top: 53px">
            <div class="card_container__top d-flex justify-content-between">
                <div class="card_container__top__title d-flex">
                    <%: Html.TranslateTag("Invalid Account")%>
                </div>
                <button type="button" class="btn btn-primary" onclick="window.history.back();">
                    <%: Html.TranslateTag("Back")%>
                </button>
            </div>
        </div>
    </div>
    <% 
        }
        else
        {
    %>
    <div class="container-fluid mt-4">

        <div class="x_panel shadow-sm rounded mb-4">
            <div class="x_title">
                <div class="dfac" style="overflow: visible;">
                    <%--<a id="actSearch_lbl" class="searchLink dfjcac search-tabs__tab" style="cursor: pointer; width: 80px;">
                    <%: Html.TranslateTag("Settings/AdminSearch|Account","Account")%>
                </a>--%>
                    <h2>Reassigning parent for account:<br />
                        <b><%=Model.CompanyName%></b></h2>
                </div>

                <%--<div class="clearfix"></div>
            <div class="row">
                <h2>Reassigning parent for account:<br />
                    <b><%=ViewBag.AccName %></b></h2>
            </div>--%>
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
                        <span class="visually-hidden">Loading...</span>
                    </button>
                    <button type="button" id="allAccts_Button" class="btn btn-primary" value="<%: Html.TranslateTag("Settings/AdminSearch|Show All","Show All")%>">
                        <span>All</span>
                    </button>
                    <button type="button" id="allAccts_Spinner" class="btn btn-primary" style="display: none;" disabled>
                        <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                        <span class="visually-hidden">Loading...</span>
                    </button>
                </div>
            </div>
        </div>
        <div id="actSearch_Result" class="Result_Div" style="display: none;">
            <div id="actSearch_List" class="x_panel shadow-sm rounded scrollParentLarge">
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function enterSubmit(e, button) {
            if (e.keyCode === 13) { // keycode 13 is the "Enter" key
                e.preventDefault(); // Ensure it is only this code that runs
                $('#' + button).click();
            }
        }

        var searchTimeout = null;
        var searchRequest = null;
        var numericOnly = '<%: Html.TranslateTag("Settings/AdminSearch|Only numeric values are permitted","Only numeric values are permitted")%>';
        var emptyBox = '<%: Html.TranslateTag("Settings/AdminSearch|Search field cannot be blank","Search text field cannot be blank")%>';
        var viewFailed = '<%: Html.TranslateTag("Settings/AdminSearch|view account failed.","view account failed.")%>';
        var targetAccountID = '<%:Model.AccountID%>';
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

            $('#actSearch_Box').focus();

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

            function assignParentSearch(searchCriteria) {
                if (searchTimeout != null)
                    clearTimeout(searchTimeout);
                if (searchRequest != null) {
                    searchRequest.abort();
                    searchRequest = null;
                }
                $('#actSearch_Button').toggle();
                $('#actSearch_Spinner').toggle();
                $('.Result_Div').hide();
                searchRequest = $.get("/Settings/AssignParentList?id=" + targetAccountID + "&searchCriteria=" + encodeURIComponent(searchCriteria), function (data) {
                    searchRequest = null;
                    $('#actSearch_Button').toggle();
                    $('#actSearch_Spinner').toggle();
                    $('#actSearch_List').html(data);
                    $('#actSearch_Result').show();
                    $('#actSearch_Box').focus();
                });
            }

            $('#actSearch_Button').click(function () {
                assignParentSearch($('#actSearch_Box').val());
            });

            $('#allAccts_Button').click(function () {
                assignParentSearch('');
            });

            //if (queryString("q").length > 0) {
            //    tabToShow = queryString("tab");
            //    $('#actSearch_Box').val(queryString("q"));
            //    searchTimeout = setTimeout("$('#acctSearch_Button').click();", 500);
            //}

            //$('#actSearch_lbl').click();
            //$('.sf-with-ul').removeClass('currentPage');
            //$('#MenuAccounts').addClass('currentPage');
        });
    </script>
    <% 
        }
    %>
</asp:Content>
