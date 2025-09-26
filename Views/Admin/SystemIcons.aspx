<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Monnit.SVGIcon>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Icons
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <!-- page content -->
        <!-- Top Filter/Help Buttons -->
        <div class="top-add-btn-row ">
            <div class="timezoneDisplay" style="flex: auto; margin-top: 10px;">
                <p style="font-size: 15px;"><b>Local Time: </b><%: DateTime.UtcNow.OVToLocalTimeShort(MonnitSession.CurrentCustomer.Account.TimeZoneID)%></p>
            </div>
            <a id="list" href="/Settings/ImportSVG" class="btn btn-primary">
                <%=Html.GetThemedSVG("add") %>
                <span class="ms-1"><%: Html.TranslateTag("Admin/SystemIcons|Add Icon","Add Icon")%></span>
            </a>
        </div>
        <%--<div class="formtitle mt-4">
            <div id="MainTitle" style="display: none;"></div>
            <div style="height: 20px">
                <div class="col-6">
                    <a href="/Settings/ImportSVG" id="list" class="btn btn-primary"><%=Html.GetThemedSVG("add") %><span class="ms-1"><%: Html.TranslateTag("Admin/SystemIcons|Add Icon","Add Icon")%></span></a>
                </div>
            </div>
            <div class="col-2 mx-auto" id="newRefresh">
                <div class="btn-group d-flex align-items-center justify-content-center" style="height: 40px;">
                    <!-- help button -->
                    <a class="btn btn-default ico-btn" onclick="$('#overlay').show()" data-bs-toggle="modal" data-bs-target="#helpModal">
                        <%=Html.GetThemedSVG("help") %></a>
                    <a href="#" onclick="$('#settings').toggle(); return false;" class="btn btn-default ico-btn">
                        <span class="svgIcon"><%=Html.GetThemedSVG("filter") %></span>
                    </a>
                    <a href="/" onclick="loadIconList(); return false;" class="btn btn-default ico-btn">
                        <span class="svgIcon"><%=Html.GetThemedSVG("refresh") %></span>
                    </a>
                </div>
            </div>
            <!-- End Main Refresh -->
        </div>--%>

        <div class="bottom-add-btn-row media_mobile">
            <a class="add-btn-mobile" style="box-shadow: 0px 3px 6px 1px rgba(0, 0, 0, 0.5);" href="/Settings/ImportSVG">
                <%=Html.GetThemedSVG("add") %>
            </a>
        </div>
        <div class="container-fluid card_container shadow-sm rounded mobile_mgntp20">
            <div class="card_container__top">
                <div class="card_container__top__title d-flex justify-content-between">
                    <div class="col-xs-6 dfac">
                        <svg version="1.1" viewBox="0 0 300 300" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink">
                            <defs>
                                <g id="svg_logo" transform="scale(2) translate(20,79)" fill="#fff">
                                    <path id="S" d="m5.482 31.319c-3.319-3.318-5.373-7.9-5.373-12.961 0-10.126 8.213-18.334 18.334-18.334 10.126 0 18.339 8.208 18.339 18.334h-10.74c0-4.194-3.404-7.593-7.599-7.593-4.194 0-7.593 3.399-7.593 7.593 0 2.095 0.851 3.993 2.22 5.363h5e-3c1.375 1.38 2.52 1.779 5.368 2.231 5.066 0.527 9.648 2.054 12.966 5.372 3.319 3.319 5.373 7.901 5.373 12.962 0 10.126-8.213 18.339-18.339 18.339-10.121 0-18.334-8.213-18.334-18.339h10.741c0 4.194 3.399 7.598 7.593 7.598 4.195 0 7.599-3.404 7.599-7.598 0-2.095-0.851-3.988-2.221-5.363h-5e-3c-1.375-1.375-3.348-1.849-5.373-2.226v-5e-3c-4.91-0.753-9.643-2.054-12.961-5.373z" />
                                    <path id="V" d="m73.452 0.024-12.97 62.601h-10.74l-12.96-62.601h10.74l7.6 36.663 7.59-36.663h10.74z" />
                                    <path id="G" d="m91.792 25.952h18.334v18.334h5e-3c0 10.127-8.213 18.34-18.339 18.34-10.127 0-18.334-8.213-18.334-18.34v-25.927h-5e-3c0-10.126 8.212-18.334 18.339-18.334 10.121 0 18.334 8.208 18.334 18.334h-10.741c0-4.19-3.404-7.594-7.593-7.594-4.195 0-7.594 3.404-7.594 7.594v25.927c0 4.195 3.399 7.594 7.594 7.594 4.189 0 7.588-3.399 7.593-7.589v-5e-3 -7.588h-7.593v-10.746z" />
                                </g>
                                <filter id="filter2686" x="0" y="0" width="1" height="1" color-interpolation-filters="sRGB">
                                    <feColorMatrix values="0.21 0.72 0.072 0 0 0.21 0.72 0.072 0 0 0.21 0.72 0.072 0 0 0 0 0 1 0 " />
                                </filter>
                                <filter id="filter2690" x="0" y="0" width="1" height="1" color-interpolation-filters="sRGB">
                                    <feColorMatrix values="0.21 0.72 0.072 0 0 0.21 0.72 0.072 0 0 0.21 0.72 0.072 0 0 0 0 0 1 0 " />
                                </filter>
                                <filter id="filter2698" x="-.077616" y="-.077616" width="1.1552" height="1.1552" color-interpolation-filters="sRGB">
                                    <feColorMatrix values="0.21 0.72 0.072 0 0 0.21 0.72 0.072 0 0 0.21 0.72 0.072 0 0 0 0 0 1 0 " />
                                </filter>
                                <filter id="filter2702" x="0" y="0" width="1" height="1" color-interpolation-filters="sRGB">
                                    <feColorMatrix values="0.21 0.72 0.072 0 0 0.21 0.72 0.072 0 0 0.21 0.72 0.072 0 0 0 0 0 1 0 " />
                                </filter>
                            </defs>
                            <path id="base" d="m8.5 150h283v100c0 23.5-18 41.5-41.5 41.5h-200c-23.5 0-41.5-18-41.5-41.5z" filter="url(#filter2702)" />
                            <g filter="url(#filter2698)" stroke="#000" stroke-width="38.009">
                                <g id="svgstar" transform="translate(150,150)">
                                    <path id="svgbar" d="m-84.149-15.851a22.417 22.417 0 1 0 0 31.703h168.3a22.417 22.417 0 1 0 0-31.703z" fill="#ffb13b" />
                                    <use transform="rotate(45)" xlink:href="#svgbar" />
                                    <use transform="rotate(90)" xlink:href="#svgbar" />
                                    <use transform="rotate(135)" xlink:href="#svgbar" />
                                </g>
                            </g>
                            <use filter="url(#filter2690)" opacity=".85" xlink:href="#base" />
                            <g>
                                <use filter="url(#filter2686)" xlink:href="#SVG" />
                                <path d="m27.855 147.92c1.3951-11.084 9.9977-19.32 20.941-20.05 5.2933-0.35273 11.415 1.6349 15.144 4.917l1.6718 1.4716h46.571l-32.621-32.621-2.6878-0.33709c-5.4746-0.6866-9.8897-2.8805-13.629-6.7726-8.9981-9.3648-7.9678-24.22 2.2564-32.535 10.769-8.758 27.236-5.1952 33.537 7.256 1.2085 2.3881 2.4707 6.9282 2.4707 8.887 0 1.2327 0.86045 2.161 16.466 17.763l16.466 16.463v-46.569l-1.4027-1.5936c-1.8161-2.0632-3.8124-5.8919-4.5375-8.7024-0.72567-2.8128-0.73788-8.0157-0.0254-10.819 2.0116-7.9137 8.3393-14.208 16.331-16.246 3.1494-0.803 7.7003-0.72845 10.992 0.18006 7.6622 2.1145 13.768 8.3512 15.729 16.066 0.71249 2.8029 0.70028 8.0059-0.0254 10.819-0.72508 2.8105-2.7214 6.6392-4.5375 8.7024l-1.4028 1.5936v46.569l16.466-16.463c15.605-15.602 16.466-16.531 16.466-17.763 0-1.9587 1.2622-6.4989 2.4707-8.887 3.7512-7.4128 11.355-12.08 19.656-12.066 3.6984 0.0065 6.8311 0.71079 9.8128 2.2059 6.3138 3.166 10.645 8.8639 12.049 15.851 2.4911 12.396-6.4011 24.453-19.207 26.043l-2.8328 0.35169-32.624 32.624h46.571l1.6718-1.4716c3.7287-3.2821 9.8503-5.2698 15.144-4.917 10.943 0.72923 19.546 8.9657 20.941 20.05l0.23912 1.8999h-244.77z" fill="#fff" stroke="#000" stroke-linejoin="round" stroke-width="1.3752" />
                            </g>
                        </svg>

                        <span class="card_container__top__title__text ms-2">Import System Icon SVG
                        </span>
                    </div>
                    <div class="col-xs-6" id="newRefresh" style="padding-left: 0px;">
                        <div style="float: right; margin-bottom: 5px;">
                            <div class="btn-group" style="height: 30px;">
                                <a class="btn btn-default ico-btn" onclick="$('#overlay').show()" data-bs-toggle="modal" data-bs-target="#helpModal">
                                   <div class="help-hover"><%=Html.GetThemedSVG("circleQuestion") %></div> </a>
                                <a href="#" class="me-2" onclick="$('#settings').toggle(); return false;">
                                    <%=Html.GetThemedSVG("filter") %>
                                </a>
                                <a id="refreshBtn" href="/" onclick="$(this).hide();$('#refreshSpinner').show(); loadIconList(); return false;">
                                    <%=Html.GetThemedSVG("refresh") %>
                                </a>
                                <div style="display: none;" class="spinner-border spinner-border-sm text-primary" id="refreshSpinner" role="status">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card_container__body">
                <div class="row" id="settings" style="display: none; border: 1px solid #dbdbdb;">
                    <div class="col-12 mb-2 mt-2 d-md-flex align-items-center">
                        <div class="d-flex align-items-center my-2 flex-wrap">
                            <div class="col-12 d-flex">
                                <strong><%: Html.TranslateTag("Filter","Filter")%>: &nbsp;</strong>
                                <span id="filteredIcons"></span>/<span id="totalIcons"></span>
                            </div>
                        </div>
                        <div class="col-12 col-md-10 d-flex flex-wrap">
                            <input type="text" id="nameSearch" class="form-control ms-lg-2" placeholder="Icon Name..." style="width: 250px" />
                            <%: Html.DropDownList("categoryFilter", 
                                    iMonnit.Controllers.AdminController.GetSystemIconsDropdownFilter(), 
                                    new { @class = "form-select mx-lg-1", @style = "width: 250px;" }) 
                            %>
                        </div>
                    </div>
                </div>

                <%--            <div class="clearfix"></div>--%>

                <!-- Report List View -->
                <div class="glanceView">
                    <!-- Report List View -->
                    <div class="col-12 scrollParentLarge">
                        <div class="hasScroll" id="iconsList">
                            <%--<%:Html.Partial("SystemIconFilter",Model) %>--%>
                        </div>
                    </div>
                    <div id="iconsLoading" class="text-center" style="display: none;">
                        <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
                            <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End help button -->
    <script src="/Scripts/events.js"></script>
    <!-- page content -->
    <div class="modal fade" id="helpModal" tabindex="-1" aria-labelledby="helpModal" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Help</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Ok"></button>
                </div>
                <div class="modal-body">
                    <p id="message">Content here to help with configuring icons.</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Ok</button>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">


        $(document).ready(function () {
            loadIconList();

            let searchTimeoutID;
            let searchRequest;

            $('#nameSearch').keyup(function (e) {
                e.preventDefault();

                clearTimeout(searchTimeoutID);
                if (searchRequest)
                    searchRequest.abort();

                if (e.keyCode === 13) {
                    loadIconList();
                } else {
                    searchTimeoutID = setTimeout('loadIconList()', 1000);
                }
            });

            $('#categoryFilter').change(function (e) {
                e.preventDefault();
                loadIconList();
            });
        });

        function loadIconList() {
            $('#iconsList').hide();
            $('#iconsLoading').show();
            var name = $('#nameSearch').val();
            var category = $('#categoryFilter').val();
            searchRequest = $.post("/Admin/SystemIconFilter", { id: <%= ViewBag.accountThemeID %>, categoryFilter: category, nameFilter: name }, function (data) {
                if (data == "Failed") {
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                } else {
                    $('#iconsList').html(data);
                    $('#iconsLoading').hide();
                    $('#refreshSpinner').hide();
                    $('#refreshBtn').show();
                    $('#iconsList').show();
                }
            });
        }

        $('#helpModal').on('hidden.bs.modal', function (e) {
            $('#overlay').hide();
        })

    </script>

    <style>
        .modal-backdrop {
            display: none;
        }

        .btn-active-fill .svg_icon, .btn .svg_icon {
            fill: #666 !important;
        }

        .btn-primary .svg_icon {
            fill: white !important;
        }
    </style>

</asp:Content>
