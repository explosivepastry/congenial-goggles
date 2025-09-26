<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<Monnit.Notification>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Rule
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



    <%=""%> <%-- Fix for Intellisense Error CS0103 --%>
    <div class="container-fluid">
        <!-- page content -->
        <!-- Top Filter/Help Buttons -->
        <div class="formtitle">
            <div id="MainTitle" style="display: none;"></div>
            <div class="top-add-btn-row media_desktop d-flex">
                <div class="timezoneDisplay" style="flex: auto;">
                    <p style="font-size: 15px; margin-bottom: 0;"><b><%: Html.TranslateTag("Overview/SensorIndex|Local Time","Local Time")%>:</b><%: DateTime.UtcNow.OVToLocalTimeShort(MonnitSession.CurrentCustomer.Account.TimeZoneID)%></p>
                </div>
                <a href="/Rule/ChooseType" id="list" class="btn btn-primary user-dets">
                    <%=Html.GetThemedSVG("add") %>
                    &nbsp;
                    <%: Html.TranslateTag("Rule/Index|Create New Rule","Create New Rule")%>
                </a>
            </div>
            <div class="bottom-add-btn-row media_mobile">
                <a class="add-btn-mobile shadow-sm" href="/Rule/ChooseType">
                    <%=Html.GetThemedSVG("add") %>
            
                </a>
            </div>
        </div>
        <!-- End Form Title -->


        <div class="clearfix"></div>

        <div class="rule-card_container w-100" style="margin-top: 1rem;">

            <%--   
                CG dont have time to do this to all places so setting back to old way
                <div class="card_container__top">

                <div class="title-pg">
                    <span class="col-6 dfac">
                        <%=Html.GetThemedSVG("rules") %>&nbsp;<%: Html.TranslateTag("Rules","Rules")%>
                    </span>
                    <div class="col-xs-6" id="newRefresh" style="padding-left: 0px;">
                        <div style="float: right; margin-bottom: 5px;">
                            <div class="btn-group" style="height: 30px;">
                                <a href="#" class="filter-icon" onclick="$('#settings').toggle(); return false;">
                                    <%=Html.GetThemedSVG("filter") %>
                                </a>
                                <a href="/" onclick="getMain(); return false;">
                                    <%=Html.GetThemedSVG("refresh") %>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>--%>
            <div class="card_container__top">
                <div class="card_container__top__title d-flex justify-content-between">
                    <div class="col-6 dfac">
                        <span class="card_container__top__title__text ms-2">
                            <%=Html.GetThemedSVG("rules") %>&nbsp;<%: Html.TranslateTag("Rules","Rules")%>
                        </span>
                    </div>
                    <div class="col-xs-6" id="newRefresh" style="padding-left: 0px;">
                        <div style="float: right; margin-bottom: 5px;">
                            <div class="btn-group" style="height: 30px;">
                                <a href="#" class="me-2" onclick="$('#settings').toggle(); return false;">
                                    <%=Html.GetThemedSVG("filter") %>
                                </a>
                                <div style="display: none;" class="spinner-border spinner-border-sm text-primary" id="refreshSpinner" role="status">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row my-2" id="settings" style="display: none;">
                <div class="col-12">
                    <strong><%: Html.TranslateTag("Filter","Filter")%>: &nbsp;</strong>
                    <span id="filterdEvents"></span>/<span id="totalEvents"></span>
                </div>
                <div class="col-12 d-flex flex-wrap">
                    <input type="text" id="nameSearch" class="NameFilter form-control user-dets" placeholder="<%: Html.TranslateTag("Rules/Index|Rule Name","Rule Name")%>..." style="width: 250px;" />
                    <select id="typeFilter" class="form-select mx-md-2 user-dets" style="width: 250px;">
                        <option value=""><%: Html.TranslateTag("Rules/Index|All Rule Types","All Rule Types")%></option>
                        <option value="1"><%: Html.TranslateTag("Rules/Index|Sensor Reading","Sensor Reading")%></option>
                        <option value="3"><%: Html.TranslateTag("Rules/Index|Battery Level","Battery Level")%></option>
                        <option value="2"><%: Html.TranslateTag("Rules/Index|Device Inactivity","Device Inactivity")%></option>
                        <option value="5"><%: Html.TranslateTag("Rules/Index|Advanced","Advanced")%></option>
                        <option value="7"><%: Html.TranslateTag("Rules/Index|Scheduled","Scheduled")%></option>
                    </select>
                    <select id="statusFilter" class="form-select user-dets" style="width: 250px;">
                        <option value=""><%: Html.TranslateTag("Rules/Index|All Rule Statuses","All Rule Statuses")%></option>
                        <option value="true"><%: Html.TranslateTag("Active","Active")%></option>
                        <option value="false"><%: Html.TranslateTag("Inactive","Inactive")%></option>
                    </select>


                    <select id="sortFilter" class="form-select mx-md-2  user-dets" style="width: 250px;">
                        <option value="Rule Name - Asc" class="sortable"><%: Html.TranslateTag("Rules/Index|Rule Name A-Z", "Rule Name A-Z")%></option>
                        <option value="Rule Name - Desc" class="sortable"><%: Html.TranslateTag("Rules/Index|Rule Name Z-A", "Rule Name Z-A")%></option>
                        <option value="Last Sent Date - Desc" class="sortable"><%: Html.TranslateTag("Rules/Index|Last Sent Date Old-Old", "Last Sent Date Old-New")%></option>
                        <option value="Last Sent Date - Asc" class="sortable"><%: Html.TranslateTag("Rules/Index|Last Sent Date New-Old", "Last Sent Date New-Old")%></option>
                    </select>
                </div>
            </div>


            <div id="rulesList" class="small-card_container">
            </div>
            <div class="text-center w-100" style="display: none" id="rulesLoading">
                <div style="display: flex; width: 100%; justify-content: center;">
                    <div class="spinner-border text-primary d-flex" role="status">
                        <span class="visually-hidden"><%= Html.TranslateTag("Loading") %>...</span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- page content -->

    <script type="text/javascript">
        loadRules();
        $(document).ready(function () {


            let searchTimeoutID;
            let searchRequest;

            $('#nameSearch').keyup(function (e) {
                e.preventDefault();

                clearTimeout(searchTimeoutID);
                if (searchRequest)
                    searchRequest.abort();

                if (e.keyCode === 13) {
                    loadRules();
                } else {
                    searchTimeoutID = setTimeout('loadRules()', 1000);
                }
            });

            $('#typeFilter, #statusFilter, #sortFilter').change(function (e) {
                e.preventDefault();
                loadRules();
            });

            //$('#statusFilter').change(function (e) {
            //    e.preventDefault();
            //    loadRules();
            //});

        });

        function loadRules() {
            $('#rulesLoading').show();
            $('#rulesList').html("");
            var query = $('#nameSearch').val();
            var type = $('#typeFilter').val();
            var status = $('#statusFilter').val();
            var sort = $('#sortFilter').val();
            $.post("/Rule/RuleFilter", { isActive: status, eventType: type, name: query, sort: sort }, function (data) {
                $('#rulesList').html(data);
                $('#rulesLoading').hide();
            });
        }
    </script>


</asp:Content>
