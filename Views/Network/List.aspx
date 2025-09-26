<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<CSNet>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- page content -->

    <%Account account = ViewBag.account as Account;
    %>

    <div class="container-fluid">

        <!-- Top Filter/Help Buttons -->
        <div class="formtitle">
            <div id="MainTitle" style="display: none;"></div>
            <div class="pt-4 media_desktop d-flex" style="padding-bottom: 0px; justify-content: flex-end;">
            <% if (MonnitSession.CustomerCan("Customer_Can_Update_Firmware"))
               {%>
                <a href="/Network/SensorsToUpdate" class="btn btn-primary me-2">
                    <%=Html.GetThemedSVG("downloadFirmware") %> &nbsp;
                    <%: Html.TranslateTag("Network/List|Check For Updates","Check For Updates")%>
                </a>
            <% }
               if ((MonnitSession.CustomerCan("Network_Create") && ViewBag.AllNetworksForAccount < ConfigData.AppSettings("MaxNetworkCount", "10").ToInt() && (account.CurrentSubscription.Can(Feature.Find("multiple_networks")) || Model.Count < 1)) || MonnitSession.IsCurrentCustomerMonnitAdmin)
               {%>
                <a href="/Network/Create/<%= MonnitSession.CurrentCustomer.AccountID%>" id="list" class="btn btn-primary">
                    <%=Html.GetThemedSVG("add") %> &nbsp;
                    <%: Html.TranslateTag("Network/List|New Network","New Network")%>
                </a>
            <% } %>
            </div>
            
            <% if (MonnitSession.CustomerCan("Customer_Can_Update_Firmware"))
                {%>
            <div class="d-flex">
            <div class="bottom-add-btn-row media_mobile" style="right: 15vw;">
                <a class="add-btn-mobile" href="/Network/SensorsToUpdate">
                    <%=Html.GetThemedSVG("downloadFirmware") %>
                </a>
            </div>
            <% } %>
            <% if ((MonnitSession.CustomerCan("Network_Create") && ViewBag.AllNetworksForAccount < ConfigData.AppSettings("MaxNetworkCount", "10").ToInt() && (account.CurrentSubscription.Can(Feature.Find("multiple_networks")) || Model.Count < 1)) || MonnitSession.IsCurrentCustomerMonnitAdmin)
                {%>
            <div class="bottom-add-btn-row media_mobile">
                <a class="add-btn-mobile" href="/Network/Create/<%= MonnitSession.CurrentCustomer.AccountID%>">
                    <%=Html.GetThemedSVG("add") %>
                </a>
            </div>
                </div>
            <% } %>
        </div>
        <!-- End Form Title -->
        <div class="clearfix"></div>

        <div class="card_container mobile_mgntp10 scrollParentLarge rule-card_container w-100">
            <div class="card_container__top">
                <div class="card_container__top__title d-flex justify-content-between">
                    <span class="col-6 dfac">
                        <%=Html.GetThemedSVG("network") %>
                        &nbsp;  <%: Html.TranslateTag("Network/List|Networks","Networks")%>
                            
                    </span>
                    <div class="col-6 d-flex justify-content-end align-items-center" id="newRefresh">
                        <!--<a href="/Network/SensorsToUpdate" class="btn btn-primary btn-sm me-2">
                            <%//=Html.GetThemedSVG("pending") %>
                            Sensor Updates
                        </a>-->
                        <div class="btn-group mb-1">
                            <a href="#" onclick="$('#settings').toggle(); return false;" class="me-2">
                                <%=Html.GetThemedSVG("filter") %>
                            </a>
                            <a href="/" onclick="getMain(); return false;">
                                <%=Html.GetThemedSVG("refresh") %>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="clearfix"></div>
            <div class="row m-2" id="settings" style="display: none;">
                <div class="col-12 col-md-2 mt-2" style="width: 250px;">
                    <strong><%: Html.TranslateTag("Filter","Filter")%>: &nbsp;</strong>
                    <span id="filterdNetworks"><%= Model.Count %></span><span> of </span><span id="totalNetworks"><%= ViewBag.NetworksUserCanSeeCount %></span>
                </div>
                <div class="col-12 col-md-3">
                    <input type="text" id="networkSearch" class="NameFilter form-control" placeholder="<%: Html.TranslateTag("Network/List|Network Name","Network Name")%>..." style="width: 260px;" />
                </div>
                <div class="col-12 col-md-3">
                </div>
                <div class="col-12 col-md-3">
                    <select id="netFilter" class="form-select" style="width: 250px;">
                        <option value=""><%: Html.TranslateTag("All Networks","All Networks")%></option>
                        <option value="true"><%: Html.TranslateTag("Holding","Holding")%></option>
                        <option value="false"><%: Html.TranslateTag("Active","Active")%></option>
                    </select>
                </div>
            </div>
            <div class="clearfix"></div>
            <div id="NetworkList" class="card_container__body NetworkList hasScroll d-flex flex-wrap bsInset" >
                <%:Html.Partial("Details",Model) %>
            </div>
        </div>

    </div>


    <script src="/Scripts/events.js"></script>
    <!-- page content -->

    <script type="text/javascript">
        
        $(document).ready(function () {


            $('#networkSearch').keyup(function (e) {
                e.preventDefault();
                loadNetworks()

            });

            $('#netFilter').change(function (e) {
                e.preventDefault();
                loadNetworks()
            });
        });

        function loadNetworks() {
            var query = $('#networkSearch').val();
            var Holding = $('#netFilter').val();

            $.ajax({
                url: '/Network/NetworkFilter',
                type: 'post',
                async: false,
                data: {
                    "holdOnly": Holding,
                    "q": query
                },
                success: function (returndata) {
                    $(".NetworkList").html(returndata);
                }
            });
        }

    </script>

</asp:Content>
