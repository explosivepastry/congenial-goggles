<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    GatewayUnlock
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <%:Html.Partial("RetailHeader") %>

        <%:Html.Partial("RedeemCreditHeader", Model as Account, new ViewDataDictionary { { "CreditClassification", eCreditClassification.GatewayUnlock.ToInt() } }) %>
        <%--<%:Html.Partial("RedeemCreditHeader", new ViewDataDictionary { { "CreditClassification", eCreditClassification.GatewayUnlock.ToInt() } }) %>--%>

        <%if (MonnitSession.CustomerCan("Account_Edit"))
            {
                CreditSetting cs = Monnit.CreditSetting.LoadByAccountID(Model.AccountID, eCreditClassification.GatewayUnlock);
                if (cs == null)
                {
                    cs = new CreditSetting();
                    cs.AccountID = Model.AccountID;
                    cs.CreditCompareValue = 0;
                    cs.LastEmailDate = DateTime.MinValue;
                    cs.UserId = MonnitSession.CurrentCustomer.CustomerID;
                    cs.CreditClassification = eCreditClassification.GatewayUnlock;
                }%>
        <div class="col-12">
            <div class="x_panel gridPanel shadow-sm rounded">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <span>
                            <%: Html.TranslateTag("Retail/GatewayUnlock|GatewayUnlock Credits", "GatewayUnlock Credits")%>
                        </span>
                    </div>
                </div>
                <div class="x_content">
                    <%:Html.Partial("GatewayUnlockCreditSettings", cs) %>

                    <%=Html.Partial("_GatewayList", cs, new ViewDataDictionary { { "GatewayList", ViewBag.GatewayList } })%>
                    <%--<div id="gatewayUnlockDiv"></div>--%>
                </div>
            </div>
        </div>
        <%--<script type="text/javascript">
                $(function () {
                    $.get('/Retail/GatewayUnlockList/<%:Model.AccountID%>', function (data) {
                        $('#gatewayUnlockDiv').html(data);
                    });
                });
            </script>--%>
        <%} %>
        <div class="col-12 <%=MonnitSession.CurrentTheme.Theme == "Default" ? "col-md-6" : ""%>">
            <div class="rule-card_container w-100" style="margin: 1rem 1rem 1rem 0;">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <span>
                            <%: Html.TranslateTag("GatewayUnlock Store", "GatewayUnlock Store")%>
                        </span>
                    </div>
                </div>
                <div class="x_content col-12">
                    <div class="bold" style="font-weight: bold;">
                        <div style="padding-top: 2px;" class="form-group"><%: Html.TranslateTag("Retail/GatewayUnlock|GatewayUnlock Credits", "GatewayUnlock Credits")%>:</div>
                    </div>
                    <div class="" style="margin-bottom: 5px; padding-left: 10px;">
                        <%string spDesc = "Keep your ALTA Sensor data on private IoT networks and applications. Unlock an ALTA Gateway from Monnit rather than sending sensor data to iMonnit Premiere, the online sensor management portal. Then, you can link a gateway with iMonnit Enterprise—on-premises sensor data hosting and management software—or standalone iMonnit Express or open platforms like Monnit Mine. The tool also allows you to use sensor data in custom applications or third-party software.";%>
                        <%: Html.TranslateTag("Events/TriggersSensors|" + spDesc, spDesc)%>
                    </div>
                    <div class="form-group row">
                        <div class="form-group row col-12">
                            <div class="bold" style="font-weight: bold;">
                                <div style="padding-left: 20px; padding-top: 10px;" class="form-group">
                                    <%: Html.TranslateTag("Retail/GatewayUnlock|Purchase GatewayUnlock Credits","Purchase GatewayUnlock Credits")%>:
                                </div>
                            </div>

                            <br />
                            <select class="form-select" id="creditsDropdown">
                                <%PurchaseLinkStoreModel purchaseLinkStoreModel = Session["PurchaseLinkStoreModel"] as PurchaseLinkStoreModel;
                                    foreach (ProductInfoModel product in purchaseLinkStoreModel.ProductList)
                                    {%>
                                <option value="<%=product.ProductType %>_<%=product.SKU %>">
                                    <%=product.DisplayName %> | <%=(product.Price - product.Discount).ToString("C") %>
                                </option>
                                <%}%>
                            </select>
                            <br />
                            <div style="font-weight: bold; padding-left: 20px; padding-top: 10px;" class="bold form-group"><%: Html.TranslateTag("Retail/GatewayUnlock|Quantity","Quantity")%>:</div>
                            <input class="form-control  user-dets" type="text" id="creditsQty" value="1" style="max-width: 60px" />
                            <br />
                            <div class="d-flex justify-content-end">
                                <button type="button" style="width: 150px;" class="btn btn-primary" id="checkoutCreditBtn"><%: Html.TranslateTag("Checkout","Checkout")%></button>
                            </div>
                            <br />
                            <div class="row" id="errorMessage" style="color: red; font-size: 1.2em; font-weight: bold;"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
            {%>
        <div class="col-12">
            <div class="rule-card_container w-100" style="margin-top: 1rem;">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <span>
                            <%: Html.TranslateTag("Retail/NotificationCredit| Gateway Unlock Administrative Credit","Gateway Unlock Administrative Credit")%>
                        </span>
                    </div>
                </div>
                <div class="ms-2">
                    <div class="bold col-12">
                        <div class="form-group" style="padding-left: 10px;">
                        </div>
                    </div>
                    <div class="bold" style="font-weight: bold;">
                        <div class="form-group"><%: Html.TranslateTag("Retail/NotificationCredit|Assign Credits","Assign Credits")%>:</div>
                    </div>
                    <div class="col-8 form-group">
                        <input type="text" id="MessageCreditsToAssign" class="form-control user-dets" />
                    </div>
                    <%--<div class="bold" style="font-weight: bold;">
                            <div class="form-group"><%: Html.TranslateTag("Retail/NotificationCredit|Expiration Date","Expiration Date")%>:</div>
                        </div>
                        <div class="col-8 form-group dfac" style="height: 25px; cursor: pointer;">
                            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="18" height="18" viewBox="0 0 22 22" style="margin-right: 5px;">
                                <image id="NoPath_-_Copy_47_" data-name="NoPath - Copy (47)" width="22" height="22" xlink:href="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAGAAAABgCAAAAADH8yjkAAACJElEQVRo3mP4T2PAMGoByRYwoAGCBhBQP2oBeRZgY+OzAJ/6UQtGLRgKFoyCwQFoVtmMWjBqwRCyYBSMlkWjFoxaMGrBqAUELPi3M89ThUMtoHLTP0zJO4L4HUeEBYds4YosjqBLftZhoNSCdpRCNwXVE/+CGSi1YBlIkrd85ekNzfIgZieKbAsDpRacYQPKeTwHs7/mAtlMuxCSvwoYKLYgFiil8hHK+esC5DnB5Z5YMVBswWcuoNRuOPcUkMf9C8L+0szLQLkFC4Eywr/h3B+sQP5pMHOaOEiX4jIKLXi2uTOuDsH9CoqRzQiN8R/vU5yKUMB2kMobMI0yK///p64F/2KACjX+QDSqzv35n8oW/CsEKVwG4ayE2ENFC76vAiVShgrUrEwtC5Yaq4MTJV8/WnlHLQuyIIo41qGXp9SywB2mTHUObSxorupb2GcMVpj9mxYWQMBVB5DKUtpZ8P+9NlAl2wPaWfD/IagwyqehBf/NgUodaWkBKDNLUdOCn3f2vEfm9wGVClHPgn9GjAwMM9FrOG8q+iABKOOKLKABFGilogVrgDLM55HKJJDS/VS04CM3UErnB4x7jw/I1f9OzUheBZJzgWStf0vEgBzBu9TNyaUgSZ7sWYdW1YBbKYw7qFzY/U5GaTqC6mFql0WnrOGKeFq/0qI++Le9MUKfS8m7dP4LDDmqFxWjXahRC0YtGLWAphaMguE5bg2P6FELCAEAiX2+a4qCoeAAAAAASUVORK5CYII=" />
                            </svg>
                            <input id="AssignedExpiration" placeholder="<%: Html.TranslateTag("Retail/NotificationCredit|Pick a Date","Pick a Date")%>" class="form-control form-control-sm" style="width: 200px;" />
                        </div>--%>
                    <div class="bold col-4"></div>
                    <div class="col-12 text-end">
                        <a href="/Retail/AssignCredits/<%:Model.AccountID %>?creditsToAssign=" class="btn btn-primary" onclick="assignCredits(this); return false;"><%: Html.TranslateTag("Retail/NotificationCredit|Assign","Assign")%></a>
                    </div>
                    <div style="clear: both;"></div>
                </div>
            </div>
        </div>

        <script type="text/javascript">
                <%--var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
                $(document).ready(function () {
                    $('#AssignedExpiration').mobiscroll().datepicker({
                        theme: 'ios',
                        display: popLocation,
                        onCancel: function (event, inst) {
                            $('#AssignedExpiration').val("");
                        }
                    });
                });--%>
            function assignCredits(a) {
                if ($('#MessageCreditsToAssign').val().length === 0) {
                    $('#MessageCreditsToAssign').attr("placeholder", "Valid Number Required");
                    return;
                }
                var url = $(a).attr("href") + $('#MessageCreditsToAssign').val() + '&creditClassification=<%=eCreditClassification.GatewayUnlock.ToInt()%>';
                    //var expiration = $('#AssignedExpiration').val()
                    //if (expiration.length > 0)
                    //    url += "&expiration=" + expiration;
                    $.post(url, "", function (data) {
                        if (data.includes("Success"))
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

                $.get('/Retail/CheckoutCheck/<%=Model != null ? Model.AccountID.ToString() : ""%>/?productType=GatewayUnlockCredit&sku=' + sku + '&qty=' + qty, function (data) {
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

        //function activateCredits(a) {
        //    if ($('#MessageCreditActivation').val().length === 0) {
        //        $('#MessageCreditActivation').attr("placeholder", "Code Required");
        //        return;
        //    }
        //    var url = $(a).attr("href") + $('#MessageCreditActivation').val() + '&creditClassification=4';
        //    $.post(url, "", function (data) {
        //        if (data == "Success")
        //            window.location.href = window.location.href;
        //        else
        //            alert(data);
        //    });
        //}
        $('.btn-secondaryToggle').hover(
            function () {
                $(this).addClass('active-hover-fill')
            },
            function () {
                $(this).removeClass('active-hover-fill')
            }
        );
    </script>

    <%--<div class="container-fluid " style="margin-top: 15px">
    <button onclick="window.location.href='<%: Url.Action("Credits_NewMenu", "Retail") %> ' " class="btn btn-dark " style="margin-left: 10px">Back to Credits Menu</button>
    </div>--%>
</asp:Content>
