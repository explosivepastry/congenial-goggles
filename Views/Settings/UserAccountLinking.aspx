<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Customer>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    UserAccountLinking
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="right_col" style="display: flex; flex-direction: column;">

        <%Html.RenderPartial("UserLink", Model); %>
        <div class="rule-card_container w-100">
            <div class="trigger-device__top">
                <div class="card_container__top" style="border-bottom: none; margin-bottom: -8px;">
                    <div class="card_container__top__title">
                        <div style="margin-right: 5px;"><%=Html.GetThemedSVG("link") %></div>
                        <%: Html.TranslateTag("Linked Accounts","Linked Accounts")%>
                        <div style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; color: #707070; font-size: small; position: sticky; margin-left: 30px;">
                            [<%= Model.FirstName%> <%= Model.LastName%>] - <%=Model.UserName%>
                        </div>

                    </div>
                </div>

                <div class="clearfix"></div>
            </div>

            <div id="accSearch_Div" class="Search_Div" style="width: 100%; margin-top: 10px;">
                <div class="d-flex">

                    <input type="text" id="Search" value="" class="form-control aSettings__input_input user-dets" style="float: none; border-radius: 6px 0px 0px 6px; border-right: none;" placeholder="Search">
                    <button type="button" id="SearchNow" value="<%: Html.TranslateTag("Search","Search")%>" class="gen-btn" style="border-radius: 0px 6px 6px 0px; float: none; width: 50px; margin-right: 10px;">
                        <div class="searchIcon">
                            <%=Html.GetThemedSVG("search") %>
                        </div>

                    </button>
                    <button type="button" id="ShowAll" value="<%: Html.TranslateTag("Settings/AdminSearch|Show All","Show All")%>" class="gen-btn dfjcc" style="">
                        <span>Top</span> <span class="hidden-xs">&nbsp;100</span>
                    </button>
                </div>
            </div>

            <div
                style="margin: 10px 0; background: #eee; display: flex; justify-content: space-between; align-items: center; padding: 10px;">
                <font color="gray">
                    <%: Html.TranslateTag("Click Account to Enable/Disable User Link","Click Account to Enable/Disable User Link")%>
                </font>
            </div>
            <div class="col-12 verticalScroll bsInset" style="min-height: 100px;">
                <div class=" ov-scroll250 " style="max-height: 300px; margin-top: 0px;">

                    <div class="row" id="settings" style="display: none; padding: 5px 30px 15px 30px; border: 1px solid #dbdbdb;">
                        <div class="col-xs-12 col-md-2" style="padding-top: 13px">
                            <strong><%: Html.TranslateTag("Filter","Filter")%>: &nbsp;</strong>
                            <span id="filterdSensors"></span>/<span id="totalSensors"></span>
                        </div>
                        <div class="col-xs-12 col-md-3">
                            <input type="text" id="NameFilter" placeholder="Account Name..." style="width: 150px; height: 25px; margin-top: 11px;" />
                        </div>
                        <div class="col-xs-12 col-md-3">
                            <%--        <select id="applicationFilter" style="margin-top: 11px; width: 150px; height: 25px;">
                                    <option value="-1"><%: Html.TranslateTag("Overview/Index|All Sensor Profiles","All Sensor Profiles")%></option>
                                    <%foreach (ApplicationTypeShort App in ApplicationTypeShort.LoadAllByAccountID(MonnitSession.CurrentCustomer.AccountID))
                                        {%>
                                    <option value='<%: App.ApplicationID%>'><%:App.ApplicationName %></option>
                                    <%}%>
                                </select>--%>
                        </div>

                    </div>

                    <div id="AccountList" class="sensorList_main verticalScroll mh300 ">
                    </div>
                </div>
            </div>


        </div>


    </div>



    <script type="text/javascript">

        function enterSubmit(e, button) {
            if (e.keyCode === 13) {
                e.preventDefault(); // Ensure it is only this code that rusn
                $('#' + button).click();
            }
        }
        var searchTimeout = null;
        var searchRequest = null;

        $(document).ready(function () {

            $('#Search').on("input", function () {
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
                searchRequest = $.get("/Settings/AdminLinkAccountList/<%=Model.CustomerID%>?searchCriteria=Show All", function (data) { $("#AccountList").html(data); searchRequest = null; });
            });
            $('#SearchNow').click(function () {
                if (searchRequest != null) {
                    searchRequest.abort();
                    searchRequest = null;
                }
                $('#AccountList').html(`<div style="text-align:center;"> <div id="loadingGIF" class="text-center" style="display: none;">
                    < div class= "spinner-border text-primary" style = "margin-top: 50px;" role = "status" >
                    <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div >
</div> </div>`);
                searchRequest = $.get("/Settings/AdminLinkAccountList/<%=Model.CustomerID%>?searchCriteria=" + encodeURIComponent($('#Search').val()), function (data) {
                    $('#AccountList').html(data);
                    searchRequest = null;
                });
            });

            $('#Search').focus();



        });


        function toggleAccount(accountID) {
            var add = $('.linkAccount' + accountID).hasClass('ListBorderNotActive');
            var url = "/Settings/ToggleAccountLink/<%:Model.CustomerID %>";
            var params = "accountID=" + accountID;
            params += "&add=" + add;


            $.post(url, params, function (data) {
                if (data == "Success") {
                    if (add) {
                        $('.linkAccount' + accountID).removeClass('ListBorderNotActive').addClass('ListBorderActive');
                    } else {
                        $('.linkAccount' + accountID).removeClass('ListBorderActive').addClass('ListBorderNotActive');
                    }
                }
                else {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
            });
        }




        function goBack() {
            window.history.back();
        }
    </script>
</asp:Content>
