<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Account>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    MessageCredit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <%:Html.Partial("RetailHeader") %>
        <%:Html.Partial("RedeemCreditHeader") %>

        <%if (MonnitSession.CustomerCan("Account_Edit"))
            {
                CreditSetting cs = Monnit.CreditSetting.LoadByAccountID(Model.AccountID, eCreditClassification.Message);
                if (cs == null)
                {
                    cs = new CreditSetting();
                    cs.AccountID = Model.AccountID;
                    cs.CreditCompareValue = 0;
                    cs.LastEmailDate = DateTime.MinValue;
                    cs.UserId = MonnitSession.CurrentCustomer.CustomerID;
                    cs.CreditClassification = eCreditClassification.Message;
                }
        %>
        <div class="row">
            <div class="col-12">
                <div class="rule-card_container w-100">
                    <div class="card_container__top">
                        <div class="card_container__top__title" style="margin-top: 5px; overflow: unset;">
                            <span>
                                <%: Html.TranslateTag("Retail/MessageCredit|HX Credits", "HX Credits")%>
                            </span>
                        </div>
                    </div>
                    <div class="x_content">
                        <%:Html.Partial("MessageCreditSettings", cs) %>
                    </div>
                </div>
            </div>
        </div>
        <%} %>

        <%--Carlos Code here--%>

        <div class="col-12">
            <div class="rule-card_container w-100">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <span>
                            <%: Html.TranslateTag("Retail/MessageCredit|HX Exhausted Credits", "HX Exhausted Credits")%>
                        </span>
                    </div>
                </div>

                <div class="row" style="margin: 0px 0px 0px 0px;">
                    <table class="table table-hover">
                        <thead>
                            <tr style="background-color: #e3e3e3;">
                                <th></th>
                                <th style="color: black; padding: 6px;"><%: Html.TranslateTag("Retail/MessageCredit | Activation Date", "Activation Date")%></th>
                                <th style="color: black; padding: 6px;"><%: Html.TranslateTag("Retail/MessageCredit | Credits", "Credits")%></th>
                                <th style="color: black; padding: 6px;"><%: Html.TranslateTag("Retail/MessageCredit | Exhausted Date", "Exhausted Date")%></th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>

                            <%foreach (Credit eachrecord in Credit.LoadExhaustedCreditsByAccountID(Model.AccountID))
                                {  %>
                            <tr>
                                <th style="color: black; padding: 6px;"><%:eachrecord.CreditType.Name %></th>
                                <td style="color: black; padding: 6px;"><%:eachrecord.ActivationDate.ToShortDateString()%></td>
                                <td style="color: black; padding: 6px;"><%:eachrecord.ActivatedCredits%></td>
                                <td style="color: black; padding: 6px;"><%:eachrecord.ExhaustedDate.ToShortDateString()%></td>
                                <td></td>
                                <%} %>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>


        <div class="d-flex" style="flex-wrap: wrap; gap: 1rem">
            <div class="rule-card_container " style="max-width: 600px;">
                <div class="card_container__top__title">
                    <span>
                        <%: Html.TranslateTag("Retail/MessageCredit|HX Store", "HX Store")%>
                    </span>
                </div>

                <div class="x_content col-12">
                    <div class="bold" style="font-weight: bold;">
                        <div style="padding-top: 2px;" class="form-group"><%: Html.TranslateTag("Retail/MessageCredit|HX Credits", "HX Credits")%>:</div>
                    </div>
                    <%if (MonnitSession.CurrentTheme.Theme != "Default")
                        {%>
                    <div class="col-md-6 col-lg-12" style="margin-bottom: 5px; padding-left: 10px;">
                        <%: Html.TranslateTag("Retail/MessageCredit|With HX, users still have the same feature set as the current user license with the ability to configure heartbeats from 1-9 minutes.", "With HX, users still have the same feature set as the current user license with the ability to configure heartbeats from 1-9 minutes.")%>

                        <div style="margin-top: 5px;"></div>
                        <%: Html.TranslateTag("Retail/MessageCredit|An HX credit is equal to one heartbeat, or one data message. Once the credits are live on the user’s accounts, they may select which sensors require faster heartbeats. They may assign all of the sensors to have faster heartbeats, or assign a faster heartbeat to only a few sensors. The more sensors assigned a faster heartbeat, the quicker the credit bundles are consumed. ", "An HX credit is equal to one heartbeat, or one data message. Once the credits are live on the user’s accounts, they may select which sensors require faster heartbeats. They may assign all of the sensors to have faster heartbeats, or assign a faster heartbeat to only a few sensors. The more sensors assigned a faster heartbeat, the quicker the credit bundles are consumed.")%>
                        <%: Html.TranslateTag("Retail/MessageCredit|A credit will", "A credit will")%>
                        <b>
                            <%: Html.TranslateTag("Retail/MessageCredit|ONLY", "ONLY")%>
                        </b>
                        <%: Html.TranslateTag("Retail/MessageCredit|be consumed when a sensor heartbeat (data message) occurs that is set between 1 and 9 minutes.", "be consumed when a sensor heartbeat (data message) occurs that is set between 1 and 9 minutes.")%>
                    </div>
                    <%}
                        if (MonnitSession.CurrentTheme.Theme == "Default")
                        {
                            PurchaseLinkStoreModel purchaseLinkStoreModel = Session["PurchaseLinkStoreModel"] as PurchaseLinkStoreModel;
                    %>
                    <div class="col-md-6 col-lg-12" style="margin-bottom: 5px; padding-left: 10px;">
                        <%: Html.TranslateTag("Retail/MessageCredit|With iMonnit HX feature enabled, users still have the same feature set as the iMonnit Premiere license with the ability to configure heartbeats from 1-9 minutes.", "With iMonnit HX feature enabled, users still have the same feature set as the iMonnit Premiere license with the ability to configure heartbeats from 1-9 minutes.")%>
                        <div style="margin-top: 5px;"></div>
                        <%: Html.TranslateTag("Retail/MessageCredit|An iMonnit HX credit is equal to one heartbeat, or one data message. Once the credits are live on the user’s accounts, they may select which sensors require faster heartbeats. They may assign all of the sensors to have faster heartbeats, or assign a faster heartbeat to only a few sensors. The more sensors assigned a faster heartbeat, the quicker the credit bundles are consumed.", "An iMonnit HX credit is equal to one heartbeat, or one data message. Once the credits are live on the user’s accounts, they may select which sensors require faster heartbeats. They may assign all of the sensors to have faster heartbeats, or assign a faster heartbeat to only a few sensors. The more sensors assigned a faster heartbeat, the quicker the credit bundles are consumed.")%>

                        <%: Html.TranslateTag("Retail/MessageCredit|A credit will", "A credit will")%>
                        <b>
                            <%: Html.TranslateTag("Retail/MessageCredit|ONLY", "ONLY")%>
                        </b>
                        <%: Html.TranslateTag("Retail/MessageCredit|be consumed when a sensor heartbeat (data message) occurs that is set between 1 and 9 minutes. ", "be consumed when a sensor heartbeat (data message) occurs that is set between 1 and 9 minutes. ")%>
                    </div>
                    <div class="col-12 bold extra" style="font-weight: bold; margin-top: 10px; margin-bottom: 10px;">
                        <%--<div  class=" col-lg-12 col-md-12 col-xs-12"><%: Html.TranslateTag("Retail/NotificationCredit|Calculate your credits","Calculate your credits")%>:</div>--%>
                        <a class="extra" style="border-radius: 6px; max-width: 100px; margin-left: 10px; font-size: 15px; font-weight: bold; text-decoration: underline; color: #2699FB;" href="/Retail/RapidCalculator/<%:Model.AccountID%>">HX Calculator</a>
                    </div>
                    <div class="form-group row">
                        <div class="">
                            <div class="bold" style="font-weight: bold;">
                                <div class="form-group" style="margin-top: .3rem;"><%: Html.TranslateTag("Retail/MessageCredit|Purchase HX Credits","Purchase HX Credits")%>:</div>
                            </div>

                            <%--<div class="HXpackages">
                                        <div class="HXpackage">
                                            <div class="HXpackage__icon">
                                                <%=Html.GetThemedSVG("send") %>
                                            </div>
                                            <div class="HXpackage__title"><b>250k</b> HX <%: Html.TranslateTag("Retail/MessageCredit|Messages", "Messages")%></div>
                                            <div class="HXpackage__btn">

                                                <a class="btn btn-sm" style="background: #2585c5; color: white;" href="/Retail/Checkout/<%=Model.AccountID%>?productType=HxCredit&sku=MNW-HX-250K"><%: Html.TranslateTag("Buy Now")%></a>
                                            </div>
                                        </div>
                                        <div class="HXpackage">
                                            <div class="HXpackage__icon">
                                                <%=Html.GetThemedSVG("plane") %>
                                            </div>
                                            <div class="HXpackage__title"><b>1.3M</b> HX <%: Html.TranslateTag("Retail/MessageCredit|Messages", "Messages")%></div>
                                            <div class="HXpackage__btn">
                                                <a class="btn btn-sm" style="background: #0067ab; color: white;" href="/Retail/Checkout/<%=Model.AccountID%>?productType=HxCredit&sku=MNW-HX-1M"><%: Html.TranslateTag("Buy Now")%></a>
                                            </div>
                                        </div>
                                        <div class="HXpackage">
                                            <div class="HXpackage__icon">
                                                <%=Html.GetThemedSVG("rocket") %>
                                            </div>
                                            <div class="HXpackage__title"><b>5.5M</b> HX <%: Html.TranslateTag("Retail/MessageCredit|Messages", "Messages")%></div>
                                            <div class="HXpackage__btn">
                                                <a class="btn btn-sm" style="background: #074d7b; color: white;" href="/Retail/Checkout/<%=Model.AccountID%>?productType=HxCredit&sku=MNW-HX-5M"><%: Html.TranslateTag("Buy Now")%></a>
                                            </div>
                                        </div>
                                    </div>--%>

                            <div class="item-select-input">
                                <select class="form-control user-dets" id="hxCreditsDropdown">
                                    <%foreach (ProductInfoModel product in purchaseLinkStoreModel.ProductList)
                                        {%>
                                    <option value="<%=product.ProductType %>_<%=product.SKU %>">
                                        <%=product.DisplayName %> | <%=(product.Price - product.Discount).ToString("C") %>
                                    </option>
                                    <%}%>
                                </select>
                            </div>

                            <div style="font-weight: bold">
                                <div class="bold form-group"><%: Html.TranslateTag("Retail/MessageCredit|Quantity","Quantity")%>:</div>

                                <div class="quantity-input">
                                    <input class="form-control user-dets" type="text" id="creditsQty" value="1" />
                                    <br />
                                </div>
                            </div>
                            <div>
                                <button type="button" style="width: 100%;" class="btn btn-primary" id="checkoutCreditBtn"><%: Html.TranslateTag("Checkout","Checkout")%></button>
                                <button class="btn btn-primary" id="renewing" style="display: none; width: 100%;" type="button" disabled>
                                    <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                </button>
                            </div>

                            <br />
                            <div class="row" id="errorMessage" style="color: red; font-size: 1.2em; font-weight: bold;"></div>
                        </div>
                    </div>
                    <%} %>
                </div>
            </div>



            <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
                {%>

            <div class="rule-card_container w-100">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <span>
                            <%: Html.TranslateTag("Retail/MessageCredit|HX Administrative Credit","HX Administrative Credit")%>
                        </span>
                    </div>
                </div>
                <div class="ms-2">
                    <div class="bold col-12">
                        <div class="form-group" style="padding-left: 10px;">
                        </div>
                    </div>
                    <div class="bold" style="font-weight: bold;">
                        <div class="form-group"><%: Html.TranslateTag("Retail/MessageCredit|Assign Credits","Assign Credits")%>:</div>
                    </div>
                    <div class="col-8 form-group">
                        <input type="text" id="MessageCreditsToAssign" class="form-control user-dets" style="width: 250px;" />
                    </div>
                    <div class="bold" style="font-weight: bold;">
                        <div class="form-group"><%: Html.TranslateTag("Retail/MessageCredit|Expiration Date","Expiration Date")%>:</div>

                    </div>
                    <div class=" form-group dfac" style="height: 25px;">
                        <%=Html.GetThemedSVG("calendar") %>



                        <input id="AssignedExpiration" class="form-control form-control-sm" style="width: 200px;" placeholder="<%: Html.TranslateTag("Retail/MessageCredit|Pick a Date","Pick a Date")%>" />
                    </div>
                    <div class="bold col-xs-4 col-sm-4 col-md-4"></div>
                    <div class="col-12 text-end">
                        <a href="/Retail/AssignCredits/<%:Model.AccountID %>?creditsToAssign=" class="btn btn-primary" onclick="assignCredits(this); return false;"><%: Html.TranslateTag("Retail/MessageCredit|Assign","Assign")%></a>
                    </div>
                    <div style="clear: both;"></div>
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
                var url = $(a).attr("href") + $('#MessageCreditsToAssign').val() + '&creditClassification=2';
                var expiration = $('#AssignedExpiration').val()
                if (expiration.length > 0)
                    url += "&expiration=" + expiration;
                $.post(url, "", function (data) {
                    if (data.includes("Success")) {
                        window.location.href = window.location.href;
                    }
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

                var productVals = $("#hxCreditsDropdown").val();
                var sku = productVals.split("_")[1];
                var qty = $('#creditsQty').val();

                $('#checkoutCreditBtn').hide();
                $('#renewing').show();

                //var obj = $(this);
                //var oldHtml = $(this).html();
                //$(this).html("<img alt='Loading...' src='/content/images/ajax-loader.gif'/>");

                $.get('/Retail/CheckoutCheck/<%=Model != null ? Model.AccountID.ToString() : ""%>/?productType=HxCredit&sku=' + sku + '&qty=' + qty, function (data) {
                    var dataSplit = data.split('|');
                    if (data.includes('Success') || data.includes('Redirect')) {
                        var url = dataSplit[1];

                        location.href = url;
                    } else {
                        var msg = dataSplit[1];

                        errDiv.html(msg);
                        $('#checkoutCreditBtn').show();
                        $('#renewing').hide();
                        /*  obj.html(oldHtml);*/
                    }
                });
            });
        });

        function activateCredits(a) {
            if ($('#MessageCreditActivation').val().length < 5) {
                showSimpleMessageModal("<%=Html.TranslateTag("Code Required")%>");
                return;
            }

            var url = $(a).attr("href") + $('#MessageCreditActivation').val() + '&creditClassification=2';
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

    <style>
        #svg_plane {
            height: 30px !important;
            width: 30px !important;
            fill: #0067ab;
        }

        #svg_rocket {
            fill: #074d7b;
        }
    </style>
</asp:Content>
