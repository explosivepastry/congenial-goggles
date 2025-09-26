<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage" %>



<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Account List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- //purgeclassic --%>
    <div id="fullForm" style="width: 100%;">
        <div class="formtitle">Account Search</div>
        <div class="formTable">

            <div style="margin-left: 30px;">
                <i class="fa fa-search " style="padding-right: 15px; font-size: 1.2em;"></i>
                <input id="Search" name="Search" type="text" value="">&nbsp;
                <input type="button" id="SearchNow" value="Search" class="bluebutton" style="float: none;" />
                <input type="button" id="ShowAll" value="ShowAll" class="greybutton" style="float: none;" />
            </div>
            <div id="AccountList">
                <table width="100%" style="border-top: 1px #e2e2e2 solid;">
                    <tr class="sectionTitle">
                        <th width="20px"></th>
                        <th>Company
                        </th>
                        <%if (MonnitSession.IsCurrentCustomerMonnitAdmin)
                            { %>
                        <th>Parent
                        </th>
                        <%} %>
                        <th>Subscription
                        </th>
                        <th>Expiration
                        </th>
                        <th></th>
                        <th></th>
                        <th colspan="2" style="text-align: right;">Primary Contact
                        </th>
                        <th width="20px"></th>
                    </tr>
                </table>
            </div>

        </div>


        <div class="buttons" style="margin-top: 0px;">
            
            <a href="/Account/CreateAccount" class="bluebutton">Create New Account</a>
            <div style="clear: both;"></div>
        </div>
    </div>

    <script type="text/javascript">
        function viewCustomer(anchor) {
            newModal("Primary Contact", "", 100, 400);
            modalDiv.html($(anchor).attr("data-details"));
        }

        var searchTimeout = null;
        var searchRequest = null;

        $(document).ready(function () {

            $('#Search').keyup(function () {
                if (searchTimeout != null)
                    clearTimeout(searchTimeout);
                if (searchRequest != null) {
                    searchRequest.abort();
                    searchRequest = null;
                }
                $('#AccountList').html('');
                searchTimeout = setTimeout("$('#SearchNow').click();", 1000);
            });

            $('#ShowAll').click(function () {
                if (searchRequest != null) {
                    searchRequest.abort();
                    searchRequest = null;
                }
                searchRequest = $.get("/Account/List?searchCriteria=Show All", function (data) { $("#AccountList").html(data); searchRequest = null; });
            });

            $('#SearchNow').click(function () {
                if (searchRequest != null) {
                    searchRequest.abort();
                    searchRequest = null;
                }

                $('#AccountList').html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);
                searchRequest = $.get("/Account/List?searchCriteria=" + encodeURIComponent($('#Search').val()), function (data) {
                    $('#AccountList').html(data);
                    searchRequest = null;
                });
            });

            $('#Search').focus();

            $('.sf-with-ul').removeClass('currentPage');
            $('#MenuAccounts').addClass('currentPage');
        });

        function changePremiumDate(accountID) {
            $('#premiumUntill_' + accountID).hide();
            $('#workingChangeAccount_' + accountID).show();
            $.post("/Account/ChangePremiumDate/" + accountID, { date: $('#premiumUntill_' + accountID).val() }, function (data) {
                if (data != "Success!")
                    console.log(data);
                let values = {};
            <%--values.redirect = '/Ack/<%:Model.NotificationRecordedID%>/<%:Model.NotificationGUID%>';--%>
            values.text = "<%=Html.TranslateTag("Oops! That did not work, please refresh your page. If this error continues, contact support.")%>";
            openConfirm(values);
            $('#modalCancel').hide();


            var color = '#FFBFBF';
            if (Date.parse($('#premiumUntill_' + accountID).val()) > new Date()) {
                color = '#C5FFBF';
            }

            $('#premiumUntill_' + accountID).css('backgroundColor', color).show();
            $('#workingChangeAccount_' + accountID).hide();
        });
        }

        function viewAccount(lnk) {
            var anchor = $(lnk);
            var acctID = anchor.data('accountid');
            var href = anchor.attr('href');

            $.post(href, { id: acctID }, function (data) {
                if (data == "Success")
                    window.location.href = "/";
                else
                    showSimpleMessageModal("<%=Html.TranslateTag("View Account failed")%>");
            });
        }

    </script>
</asp:Content>
