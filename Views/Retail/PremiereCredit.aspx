<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Account>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Premiere Credits
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid" style="margin-top: 25px">

        <%
            long accountID = Model != null ? Model.AccountID : long.MinValue;
            Account account = accountID != long.MinValue ? Account.Load(accountID) : null;
            int sensorCount = accountID != long.MinValue ? Sensor.LoadByAccountID(accountID).Count() : 0;
            PurchaseLinkStoreModel purchaseLinkStoreModel = Session["PurchaseLinkStoreModel"] as PurchaseLinkStoreModel;
            AccountIncrement accountIncrement = accountID != long.MinValue ? AccountIncrement.LoadByAccountID(account.AccountID).FirstOrDefault() : null;
        
        %>
        <%:Html.Partial("RetailHeader") %>
        <%:Html.Partial("RedeemCreditHeader") %>

        <%if (MonnitSession.CustomerCan("Account_Edit"))
            {%>
        <div class="rule-card_container" style="margin-bottom: 1rem;">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <%: Html.TranslateTag("Settings/AccountDetail|Active Subscriptions","Active Subscriptions")%>
                    <button type="button" class="btn btn-secondary ms-4" id="whyPremier" onclick="showPremiereFeatures(this);">Why Premiere?</button>

                    <%if (accountIncrement != null && accountIncrement.AccountID == account.AccountID && MonnitSession.CustomerCan("See_Beta_Preview"))
                        {%>
                        <a class="btn btn-secondary ms-4" id="autobill" href="/Settings/AutoBill/<%:account.AccountID %>"><%:Html.TranslateTag("Configure AutoBill Subscriptions") %></a>

                    <%} %>

                </div>
            </div>
            <div class="x_content">
                <div class="form-group sub-list__container hasScroll-sm">
                    <% List<AccountSubscription> subList = accountID != long.MinValue ? AccountSubscription.LoadByAccountID(accountID).Where(m => m.ExpirationDate > DateTime.UtcNow).ToList() : new List<AccountSubscription>();
                        if (subList != null && subList.Count == 0)
                        {%>
                    <p>No Active Subscriptions</p>
                    <%}
                        else
                        {
                            foreach (AccountSubscription subscription in subList)
                            {
                                if (subscription.AccountSubscriptionTypeID == AccountSubscriptionType.BasicID)
                                    continue;%>
                    <div class="col-sm-10 col-12 dfac pd5">
                        <span style="font-size: 14px; display: flex; align-items: center;">
                            <svg xmlns="http://www.w3.org/2000/svg" width="7" height="7" viewBox="0 0 10 10">
                                <path id="ic_lens_24px" d="M7,2a5,5,0,1,0,5,5A5,5,0,0,0,7,2Z" transform="translate(-2 -2)" fill="#21ce99" />
                            </svg>
                            &nbsp;&nbsp;
                                            <%: subscription.AccountSubscriptionType.Name%>
                        </span>
                        &nbsp;&nbsp;&nbsp; 
                                        <span>Expires: <%= subscription.ExpirationDate.OVToLocalDateTimeShort() %>
                                        </span>
                    </div>
                    <br />
                    <%}
                        }
                    %>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>

        <%if (MonnitSession.CurrentTheme.Theme == "Default")
            {%>
        <div class="col-12 device_detailsRow px-0 mb-4">
            <div class="col-md-6 col-12 device_detailsRow__card pe-lg-3">
                <div class="rule-card_container">
                    <div class="card_container__top">
                        <div class="card_container__top__title">
                            <%: Html.TranslateTag("Retail/MessageCredit|Select Product")%>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="col-md-6 col-lg-12" style="margin-bottom: 5px; padding-left: 10px; max-width: 300px;">
                            <%if (purchaseLinkStoreModel.ProductList.Count == 0)
                                {%>
                            <%: Html.TranslateTag("Retail/PaymentOption|No Items Found.","No Items Found.")%>
                            <%}
                                else
                                {%>
                            <p title="Are you planning to expand your sensor network? &#013;If you’re installing additional sensors soon, please select the appropriate subscription to match your total number of sensors.">
                                This account <b><%=purchaseLinkStoreModel.account.CompanyName %></b> has <%=sensorCount %> sensors.  Please choose the iMonnit Premiere yearly subscription below.
                            </p>
                            <%string optionsHtml = "";
                                bool listHasItem = false;
                                double prorateAmount = 0;
                                string currentSubType = MonnitSession.CurrentCustomer.Account.CurrentSubscription.AccountSubscriptionType.KeyType;
                                int currentSubDeviceCount = currentSubType.StartsWith("Premiere_") ? currentSubType.Split('_')[1].ToInt() : 0;
                                DateTime subExpDate = MonnitSession.CurrentCustomer.Account.CurrentSubscription.ExpirationDate;
                                int monthDifference = 0;

                                foreach (ProductInfoModel productInfoModel in purchaseLinkStoreModel.ProductList)
                                {
                                    string[] skuParts = productInfoModel.SKU.Split('-');
                                    int skuCount = skuParts[2].ToInt();

                                    if (skuCount >= sensorCount)
                                    {
                                        listHasItem = true;
                                        optionsHtml += "<option value=\"" + productInfoModel.ProductType + "_" + productInfoModel.SKU + "\">"
                                                    + productInfoModel.Description.Replace("(Selected)", "") + " | " + (productInfoModel.Price - productInfoModel.Discount).ToString("C")
                                                    + "</option>";
                                    }

                                    if (currentSubDeviceCount == skuCount && subExpDate > DateTime.Now)
                                    {
                                        double perMonthPrice = (productInfoModel.Price - productInfoModel.Discount) / 12;
                                        TimeSpan todayToExpiration = MonnitSession.CurrentCustomer.Account.CurrentSubscription.ExpirationDate - DateTime.Now;
                                        monthDifference = ((subExpDate.Year - DateTime.Now.Year) * 12) + subExpDate.Month - DateTime.Now.Month;
                                        monthDifference = monthDifference > 12 ? 12 : monthDifference;
                                        prorateAmount = perMonthPrice * monthDifference;
                                    }
                                }%>
                            <p>
                                One year of Premiere monitoring for:
                            </p>
                            <select class="form-select user-dets" id="premiereDropdown" data-count="<%:currentSubDeviceCount %>" style="max-width: 360px;">
                                <%:Html.Raw(optionsHtml) %>
                            </select>
                            <%if (prorateAmount > 0)
                                {
                            %>
                            <div id="prorateDiv" style="display: none;">
                                <br />
                                <p>
                                    <%:Html.TranslateTag("Retail/PremiereCredit|Premiere Upgrade Prorate", 
                                                                    "If you choose to upgrade your plan, you'll receive a prorated discount for up to one year of the remaining time on your current plan.")%>
                                    <br />
                                    <%:Html.TranslateTag("Retail/PremiereCredit|Premiere Upgrade Prorate Months remaining", "Months remaining: ") + monthDifference%>
                                    <br />
                                    <%:Html.TranslateTag("Retail/PremiereCredit|Premiere Upgrade Prorate Amount", "Discount Available: " ) + prorateAmount.ToString("C")%>
                                </p>
                            </div>
                            <%}
                                if (listHasItem)
                                {%>
                            <button type="button" style="width: 150px;" class="btn btn-primary user-dets" id="checkoutCreditBtn">
                                <%: Html.TranslateTag("Checkout","Checkout")%>
                            </button>

                            <button class="btn btn-primary" id="renewing" style="display: none; width: 150px;" type="button" disabled>
                                <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                            </button>
                            <%} %>
                            <br />
                            <div class="row" id="errorMessage" style="color: red; font-size: 1.2em; font-weight: bold;"></div>
                            <%}%>
                        </div>
                        <div class="form-group row">
                            <div class="form-group row col-12">
                                <div class="bold" style="font-weight: bold;">
                                </div>
                                <div class="bolddfac" style="margin: 10px; padding: 0px; float: left; width: 100%; height: 25px; margin-left: 38px!important;">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%}
            }%>
    </div>

    <script type="text/javascript">
        var tooManyMessage = '<%: Html.TranslateTag("Retail/PaymentOption|Account has too many sensors for this subscription.","Account has too many sensors for this subscription.")%>';
        var sensorCount = <%=sensorCount%>;
        var errDiv = $('#errorMessage');

        $(function () {
            $('#checkoutCreditBtn').click(function (e) {
                e.preventDefault();

                var productVals = $("#premiereDropdown").val();
                var sku = productVals.split("_")[1];

                //var obj = $(this);
                //var oldHtml = $(this).html();
                $('#checkoutCreditBtn').hide();
                $('#renewing').show();
                /*  $(this).html("<img alt='Loading...' src='/content/images/ajax-loader.gif'/>");*/
                /*     $(this).classList.add("spinner-border spinner-border-sm");*/

                $.get('/Retail/CheckoutCheck/<%=accountID%>/?productType=Subscriptions&sku=' + sku, function (data) {
                    var dataSplit = data.split('|');
                    if (data.includes('Success') || data.includes('Redirect')) {
                        var url = dataSplit[1];

                        location.href = url;
                    } else {
                        var msg = dataSplit[1];

                        errDiv.html(msg);
                        $('#checkoutCreditBtn').show();
                        $('#renewing').hide();
                        /* obj.html(oldHtml);*/
                    }
                });
            });

            $('#premiereDropdown').change(function (e) {
                var countValue = $(this).attr('data-count');
                var currentPlanCount = Number(countValue);
                var targetValue = $(this).val().split('-')[2];
                var targetPlanCount = Number(targetValue);

                if (targetPlanCount > currentPlanCount) {
                    $('#prorateDiv').show();
                } else {
                    $('#prorateDiv').hide();
                }
            });
        });

        function showPremiereFeatures(item) {
            $('#confirmLoad').hide();
            $('#modalSubmit').show();

            let values = {};
            values.redirect = "#";
            values.html = `<b style="font-size: 1.0em;"><%: Html.TranslateTag("Retail/MessageCredit|Looking for more features?")%> </b>
                            <br />
                            <%: Html.TranslateTag("Retail/PremiereCredit|12 month subscription to Premiere monitoring service  for your wireless sensor network")%>.

                            <%: Html.TranslateTag("Retail/MessageCredit|Upgrade your account to Premiere and receive the following additional advanced features for a minimal annual cost:")%>
                            <br />
                            <ul style="list-style-type: circle">
                                <li><%: Html.TranslateTag("Retail/MessageCredit|Unlimited user accounts for system configuration, monitoring and rules.")%></li>
                                <li><%: Html.TranslateTag("Retail/MessageCredit|Sensor heartbeats (check-ins) down to 10 minutes.")%></li>
                                <li><%: Html.TranslateTag("Retail/MessageCredit|Timed sensors (i.e. temp) can be user set to check against alert thresholds up to 256 times between heartbeats.")%></li>
                                <li><%: Html.TranslateTag("Retail/MessageCredit|Triggered sensors (i.e. motion, door/window, etc.) record data as it happens, regardless of heartbeat.")%></li>
                                <li><%: Html.TranslateTag("Retail/MessageCredit|Advanced sensor configuration options.")%></li>
                                <li><%: Html.TranslateTag("Retail/MessageCredit|SMS text and email notifications sent from the system when sensor thresholds are exceeded.")%></li>
                                <li><%: Html.TranslateTag("Retail/MessageCredit|Unlimited data storage.")%></li>
                                <li><%: Html.TranslateTag("Retail/MessageCredit|Sensor Mapping Tool.")%></li>
                                <li><%: Html.TranslateTag("Retail/MessageCredit| API commands for integrating into 3rd party applications.")%></li>
                            </ul>`;
            openConfirm(values);
        }

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


    </script>

    <%--<div class="container-fluid " style="margin-top: 15px">
        <button onclick="window.location.href='<%: Url.Action("Credits_NewMenu", "Retail") %> ' " class="btn btn-dark " style="margin-left: 10px">Back to Credits Menu</button>
    </div>--%>
</asp:Content>
