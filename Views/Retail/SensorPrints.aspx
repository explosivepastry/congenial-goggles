<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SensorPrint
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <%:Html.Partial("RetailHeader") %>

        <%:Html.Partial("RedeemCreditHeader") %>

        <%if (MonnitSession.CustomerCan("Account_Edit"))
            {
                CreditSetting cs = Monnit.CreditSetting.LoadByAccountID(Model.AccountID, eCreditClassification.SensorPrint);
                if (cs == null)
                {
                    cs = new CreditSetting();
                    cs.AccountID = Model.AccountID;
                    cs.CreditCompareValue = 0;
                    cs.LastEmailDate = DateTime.MinValue;
                    cs.UserId = MonnitSession.CurrentCustomer.CustomerID;
                    cs.CreditClassification = eCreditClassification.SensorPrint;
                }%>
        <div class="col-12">
            <div class="rule-card_container" style="width: 100%">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <span>
                            <%: Html.TranslateTag("Retail/NotificationCredit|SensorPrint Credits", "SensorPrint Credits")%>
                        </span>
                    </div>
                </div>
                <div class="x_content">
                    <%:Html.Partial("SensorPrintCreditSettings", cs) %>

                    <%=Html.Partial("_SensorList",cs) %>
                </div>
            </div>
        </div>
        <%} %>
        <div class=" <%=MonnitSession.CurrentTheme.Theme == "Default" ? "col-md-6" : ""%>">
            <div class="rule-card_container" style="margin-bottom: 1rem; margin-right: 1rem;">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <span>
                            <%: Html.TranslateTag("SensorPrints Store", "SensorPrints Store")%>
                        </span>
                    </div>
                </div>
                <div class="x_content ">
                    <div class="bold" style="font-weight: bold;">
                        <div style="padding-top: 2px;" class="form-group"><%: Html.TranslateTag("Retail/NotificationCredit|SensorPrint Credits", "SensorPrint Credits")%>:</div>
                    </div>
                    <div class="" style="margin-bottom: 5px; padding-left: 10px;">
                        <%string spDesc = "SensorPrints brings end-to-end, authentication security to Monnit ALTA Sensor data. With SensorPrints, data from each sensor is encapsulated with best-of-breed encryption that ensures nothing malicious happens to that data. SensorPrints is the answer to enterprise-class security for the Internet of Things.";
                            string spRedDesc = "SensorPrints is only available for ALTA Sensors version 16.34.x.x. or newer. You can find all your compatible devices above in your SensorPrints sensor list.";
                        %>
                        <%: Html.TranslateTag("Events/TriggersSensors|" + spDesc, spDesc)%>
                        <div class="callAttention__description">
                            <%: Html.TranslateTag("Events/TriggersSensors|" + spRedDesc, spRedDesc)%>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="form-group row ">
                            <div class="bold" style="font-weight: bold;">
                                <div style="padding-left: 20px; padding-top: 2px;" class="form-group"><%: Html.TranslateTag("Retail/NotificationCredit|Purchase SensorPrint Credits","Purchase SensorPrint Credits")%>:</div>
                            </div>

                            <br />
                            <select class="form-select user-dets" id="creditsDropdown">
                                <%PurchaseLinkStoreModel purchaseLinkStoreModel = Session["PurchaseLinkStoreModel"] as PurchaseLinkStoreModel;
                                    foreach (ProductInfoModel product in purchaseLinkStoreModel.ProductList)
                                    {%>
                                <option value="<%=product.ProductType %>_<%=product.SKU %>">
                                    <%=product.DisplayName %> | <%=(product.Price - product.Discount).ToString("C") %>
                                </option>
                                <%}%>
                            </select>
                            <br />
                            <div style="padding-left: 20px; padding-top: 2px;" class="bold form-group"><%: Html.TranslateTag("Retail/SensorPrints|Quantity","Quantity")%>:</div>

                            <input class="form-select user-dets" style="max-width: 115px;" type="text" id="creditsQty" value="1" />
                            <br />
                            <button type="button" style="margin: 15px 0" class="btn btn-primary" id="checkoutCreditBtn"><%: Html.TranslateTag("Checkout","Checkout")%></button>
                            <br />
                            <div class="row" id="errorMessage" style="color: red; font-size: 1.2em; font-weight: bold;"></div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
            {%>

        <div class="rule-card_container">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <span>
                        <%: Html.TranslateTag("Retail/NotificationCredit| SensorPrint Administrative Credit","SensorPrint Administrative Credit")%>
                    </span>
                </div>
            </div>
            <div class="ms-2">
                <div class="bold ">
                    <div class="form-group" style="padding-left: 10px;">
                    </div>
                </div>
                <div class="bold" style="font-weight: bold;">
                    <div class="form-group"><%: Html.TranslateTag("Retail/NotificationCredit|Assign Credits","Assign Credits")%>:</div>
                </div>
                <div class="col-8 form-group">
                    <input type="text" id="MessageCreditsToAssign" class="form-control user-dets" style="width: 250px;" />
                </div>
                <div class="bold" style="font-weight: bold;">
                    <div class="form-group"><%: Html.TranslateTag("Retail/NotificationCredit|Expiration Date","Expiration Date")%>:</div>
                </div>
                <div class="col-8 form-group dfac" style="height: 25px; cursor: pointer;">
                    <%=Html.GetThemedSVG("calendar") %>

                    <input id="AssignedExpiration" placeholder="<%: Html.TranslateTag("Retail/NotificationCredit|Pick a Date","Pick a Date")%>" class="form-control form-control-sm" style="width: 200px;" />
                </div>
                <div class="bold col-4"></div>
                <div class="col-12 text-end">
                    <a href="/Retail/AssignCredits/<%:Model.AccountID %>?creditsToAssign=" class="btn btn-primary" onclick="assignCredits(this); return false;"><%: Html.TranslateTag("Retail/NotificationCredit|Assign","Assign")%></a>
                </div>
                <div style="clear: both;"></div>
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
                if ($('#MessageCreditsToAssign').val().length === 0) {
                    $('#MessageCreditsToAssign').attr("placeholder", "Valid Number Required");
                    return;
                }
                var url = $(a).attr("href") + $('#MessageCreditsToAssign').val() + '&creditClassification=3';
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
    <script type="text/javascript">
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

                $.get('/Retail/CheckoutCheck/<%=Model != null ? Model.AccountID.ToString() : ""%>/?productType=SensorPrintCredit&sku=' + sku + '&qty=' + qty, function (data) {
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
            if ($('#MessageCreditActivation').val().length === 0) {
                $('#MessageCreditActivation').attr("placeholder", "Code Required");
                return;
            }
            var url = $(a).attr("href") + $('#MessageCreditActivation').val() + '&creditClassification=3';
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

    <%--<div class="container-fluid " style="margin-top: 15px">
    <button onclick="window.location.href='<%: Url.Action("Credits_NewMenu", "Retail") %> ' " class="btn btn-dark " style="margin-left: 10px">Back to Credits Menu</button>
    </div>--%>
</asp:Content>
