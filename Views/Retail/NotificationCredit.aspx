<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Account>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Notification Credits
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid" style="display: flex; flex-direction: column; margin-top: 25px">
        <%:Html.Partial("RetailHeader") %>
        <%:Html.Partial("RedeemCreditHeader") %>


        <%if (MonnitSession.CustomerCan("Account_Edit"))
            {
                CreditSetting cs = Monnit.CreditSetting.LoadByAccountID(Model.AccountID, eCreditClassification.Notification);
                if (cs == null)
                {
                    cs = new CreditSetting();
                    cs.AccountID = Model.AccountID;
                    cs.CreditCompareValue = 0;
                    cs.LastEmailDate = DateTime.MinValue;
                    cs.UserId = MonnitSession.CurrentCustomer.CustomerID;
                    cs.CreditClassification = eCreditClassification.Notification;

                }%>


        <div class="rule-card_container" style="width: 100%">
            <div class="card_container__top">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Retail/NotificationCredit|Notification Credits")%>
                    <a class="helpIco" style="cursor: pointer!important;" data-bs-toggle="modal" title="<%: Html.TranslateTag("Retail/Notification Credit|Credits Help","Credits Help") %>" data-bs-target=".notificationCreditHelp">
                        <%=Html.GetThemedSVG("circleQuestion") %>
                    </a>
                </div>
            </div>
            <div class="x_content">
                <%:Html.Partial("NotificationCreditSettings", cs) %>
            </div>
        </div>

        <%}
            PurchaseLinkStoreModel purchaseLinkStoreModel = Session["PurchaseLinkStoreModel"] as PurchaseLinkStoreModel;
        %>

        <div class="d-flex" style="gap: 1rem; flex-wrap: wrap;">

            <div class="rule-card_container" style="max-width: 600px">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <span>
                            <%: Html.TranslateTag("Retail/MessageCredit|Notification Store")%>
                        </span>
                    </div>
                </div>
                <div class="x_content col-12">
                    <div class="bold" style="font-weight: bold;">
                        <div style="padding-top: 2px;" class="form-group"><%: Html.TranslateTag("Retail/MessageCredit|Notification Credits")%>:</div>
                    </div>
                    <%if (MonnitSession.CurrentTheme.Theme != "Default")
                        {%>
                    <div class="col-md-6 col-lg-12" style="margin-bottom: 5px; padding-left: 10px;">
                        <%: Html.TranslateTag("Retail/MessageCredit|These credits are used for sensor alerts/notifications via voice call and direct text (SMS) message. Keep credits stocked for timely sensor alerts.")%>

                        <div style="margin-top: 5px;"></div>

                    </div>
                    <%}
                        if (MonnitSession.CurrentTheme.Theme == "Default")
                        {%>
                    <div class="col-md-6 col-lg-12" style="margin-bottom: 5px; padding-left: 10px;">
                        <%: Html.TranslateTag("Retail/MessageCredit|Purchase notification credits for your Monnit Wireless Sensors. Sold in packs of 100, these credits are used for sensor alerts/notifications via voice call and direct text (SMS) message. Keep credits stocked for timely sensor alerts.")%>

                        <br />
                        <ul style="list-style-type: circle">
                            <li><b><%: Html.TranslateTag("Retail/MessageCredit|Email Notification")%></b> - <%: Html.TranslateTag("Retail/MessageCredit|No credits required")%></li>
                            <li><b><%: Html.TranslateTag("Retail/MessageCredit|External Provider SMS Notification")%></b> - <%: Html.TranslateTag("Retail/MessageCredit|No credits required (Not available in all areas)")%></li>
                            <li><b><%: Html.TranslateTag("Retail/MessageCredit|Direct Delivery SMS Notification")%></b> - <%: Html.TranslateTag("Retail/MessageCredit|Credits based on recipients country")%></li>
                            <li><b><%: Html.TranslateTag("Retail/MessageCredit|Voice Notification")%></b> - <%: Html.TranslateTag("Retail/MessageCredit|Credits based on recipients country")%></li>
                        </ul>

                        <div style="margin-top: 5px;"></div>
                    </div>
                    <div class="col-12 bold extra">
                    </div>
                    <div class="form-group row">
                        <div class="form-group row col-12">
                            <div class="bold" style="font-weight: bold;">
                                <div style="padding-left: 20px; padding-top: 2px;" class="form-group"><%: Html.TranslateTag("Retail/NotificationCredit|Purchase Notification Credits","Purchase Notification Credits")%>:</div>
                            </div>
                            <%--<div class="HXpackages">
                                        <div class="HXpackage">
                                            <div class="HXpackage__icon">
                                                <%=Html.GetThemedSVG("send") %>
                                            </div>
                                            <div class="HXpackage__title"><b>100</b> <%: Html.TranslateTag("Retail/MessageCredit|Credits", "Credits")%></div>
                                            <div class="HXpackage__btn">
                                                <a class="btn btn-primary btn-sm" href="/Retail/Checkout/<%=Model.AccountID%>?productType=NotificationCredit&sku=MNW-NC-100"><%: Html.TranslateTag("Buy Now")%></a>
                                            </div>
                                        </div>
                                    </div>--%>
                            <br />
                            <select class="form-select user-dets" id="creditsDropdown">
                                <%foreach (ProductInfoModel product in purchaseLinkStoreModel.ProductList)
                                    {%>
                                <option value="<%=product.ProductType %>_<%=product.SKU %>">
                                    <%=product.DisplayName %> | <%=(product.Price - product.Discount).ToString("C") %>
                                </option>
                                <%}%>
                            </select>
                            <br />
                            <div style="padding-left: 20px; padding-top: 2px;" class="bold form-group"><%: Html.TranslateTag("Retail/NotificationCredit|Quantity","Quantity")%>:</div>
                            <input class="form-select user-dets" style="flex-basis: 200px;" type="text" id="creditsQty" value="1" />
                            <br />
                            <button type="button" style="max-width: 150px; margin-left: auto;" class="btn btn-primary" id="checkoutCreditBtn"><%: Html.TranslateTag("Checkout","Checkout")%></button>
                            <br />
                            <div class="row" id="errorMessage" style="color: red; font-size: 1.2em; font-weight: bold;"></div>

                        </div>
                    </div>
                    <%} %>
                    <div class="form-group row">
                        <div class="form-group row col-12">
                            <div class="bold" style="font-weight: bold;">
                            </div>
                        </div>
                    </div>
                </div>
            </div>



            <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
                {%>

            <div class="rule-card_container" style="max-width: 600px;">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <span>
                            <%: Html.TranslateTag("Retail/NotificationCredit|Administrative Credit","Administrative Credit")%>
                        </span>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content mx-2">
                    <div class="row">
                        <div class="bold col-12">
                            <div class="form-group" style="padding-left: 10px;">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="bold" style="font-weight: bold;">
                            <div style="padding-top: 2px;" class="form-group"><%: Html.TranslateTag("Retail/NotificationCredit|Assign Credits","Assign Credits")%>:</div>
                        </div>
                        <div class="col-8 form-group">
                            <input type="text" id="NotificationCreditsToAssign" class="form-control user-dets" style="width: 250px;" />
                        </div>
                    </div>
                    <div>
                        <div class="bold" style="font-weight: bold;">
                            <div style="padding-top: 2px;" class="form-group"><%: Html.TranslateTag("Retail/NotificationCredit|Expiration Date","Expiration Date")%>:</div>
                        </div>
                        <div class="col-8 form-group dfac" style="height: 25px; cursor: pointer;">
                            <%=Html.GetThemedSVG("calendar") %>
                            <input id="AssignedExpiration" class="form-control form-control-sm" placeholder="<%: Html.TranslateTag("Retail/NotificationCredit|Pick a Date","Pick a Date")%>" style="width: 200px;" />
                        </div>
                        <div class="bold col-4"></div>
                        <div class="col-12 text-end">
                            <a href="/Retail/AssignCredits/<%:Model.AccountID %>?creditsToAssign=" class="btn btn-primary me-2" onclick="assignCredits(this); return false;"><%: Html.TranslateTag("Retail/NotificationCredit|Assign","Assign")%></a>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                </div>
            </div>
        </div>

        <div class="modal fade notificationCreditHelp" style="z-index: 2000!important;" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="notificationCreditHelp"><%: Html.TranslateTag("Retail/NotificationCredit|Notification Credits","Notification Credits")%></h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="word-choice">
                                <%: Html.TranslateTag("Retail/NotificationCreditSettings/Help|Enable Auto Purchase","Enable Auto Purchase")%>
                            </div>
                            <div class="word-def">
                                <%: Html.TranslateTag("Retail/NotificationCreditSettings/Help|When credits go below set threshold, Notification Credits will be auto purchased. User selected below will receive purchase confirmation email.","When credits go below set threshold, Notification Credits will be auto purchased. User selected below will receive purchase confirmation email.")%>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                    </div>
                </div>
            </div>
        </div>


        <script type="text/javascript">
            var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
            $(document).ready(function () {
                $('#AssignedExpiration').mobiscroll().datepicker({
                    theme: 'ios',
                    display: popLocation,
                    onCancel: function (event, inst) {
                        $('#AssignedExpiration').val("");
                    }
                });
            });
            function assignCredits(a) {
                var url = $(a).attr("href") + $('#NotificationCreditsToAssign').val() + '&creditClassification=1';
                var expiration = $('#AssignedExpiration').val()
                if (expiration.length > 0)
                    url += "&expiration=" + expiration;
                $.post(url, "", function (data) {
                    if (data.includes("Success"))
                        window.location.href = window.location.href;
                    else {
                        console.log(data);
                        showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                    }
                });
            }
            function removeCredits(a) {
                $.post($(a).attr("href"), "", function (data) {
                    if (data == "Success")
                        window.location.href = window.location.href;
                    else {
                        console.log(data);
                        showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                    }
                });
            }
        </script>
        <%} %>
    </div>
            <style>
                .help-hover svg {
                    fill: var(--help-highlight-color);
                    width: 30px;
                    height: 30px;
                }

        </style>

    <script type="text/javascript">

        var errDiv = $('#errorMessage');

        $(function () {
            $('#checkoutCreditBtn').click(function (e) {
                e.preventDefault();

                var productVals = $("#creditsDropdown").val();
                var sku = productVals.split("_")[1];
                var qty = $('#creditsQty').val();

                var obj = $(this);
                var oldHtml = $(this).html();
                $(this).html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);

                $.get('/Retail/CheckoutCheck/<%=Model != null ? Model.AccountID.ToString() : ""%>/?productType=NotificationCredit&sku=' + sku + '&qty=' + qty, function (data) {
                    var dataSplit = data.split('|');
                    if (data.includes('Success') || data.includes('Redirect')) {
                        var url = dataSplit[1];

                        location.href = url;
                    } else {
                        var msg = dataSplit[1];

                        errDiv.html(msg);
                        obj.html(oldHtml);
                    }
                });
            });
        });

        function activateCredits(a) {
            if ($('#NotificationCreditActivation').val().length === 0) {
                $('#NotificationCreditActivation').attr("placeholder", "Code Required");
                return;
            }
            var url = $(a).attr("href") + $('#NotificationCreditActivation').val() + '&creditClassification=1';
            $.post(url, "", function (data) {
                if (data == "Success")
                    window.location.href = window.location.href;
                else {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
            });
        }

        $('.btn-secondaryToggle').hover(
            function () { $(this).addClass('active-hover-fill') },
            function () { $(this).removeClass('active-hover-fill') }

        );
    </script>


</asp:Content>
