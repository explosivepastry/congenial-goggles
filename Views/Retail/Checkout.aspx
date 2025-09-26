<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<PurchaseLinkStoreModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Checkout
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid d-flex flex-column">
        <div class="col-12">
            <%:Html.Partial("RetailHeader", Model.account) %>
            <%if (MonnitSession.CurrentStoreLinkInfo != null)
    
                { %>
            <%Html.RenderPartial("~/Views/Retail/StoreHeaderLink.ascx", Model.account); %>
            <%} %>
        </div>
        <div class="d-flex flex-wrap">
            <div class="col-md-6 col-12 ps-0 pe-md-2 mb-4">
                <div class="x_panel shadow-sm rounded">

                    <div class="x_title">
                        <h2 style="max-width: 90%; overflow: unset; font-weight: bold"><%: Html.TranslateTag("Retail/PaymentOption|Products","Products")%></h2>
                        <div style="clear: both;"></div>
                    </div>
                    <div class="x_content">
                            <% Html.RenderPartial("ProductList"); %>
                    </div>
                </div>
            </div>

            <div class="col-md-6 col-12 pe-0 ps-md-2">
            <div class="x_panel shadow-sm rounded">
                <div class="x_title dfjcsbac">
                    <span class="dfac">
                        <%=Html.GetThemedSVG("wallet") %>
    
                        <div style="max-width: 90%; font-weight: bold; font-size: 16px;"><%: Html.TranslateTag("Retail/PaymentOption|Wallet","Wallet")%></div>
                    </span>
                    <span class="dfac">
                        <a class="menu-box-tab dfac" href="/Retail/PaymentOption/<%=MonnitSession.CurrentCustomer.AccountID %>">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="13" class="icon-fill" viewBox="0 0 18 13">
                                <path id="credit-card-solid" d="M0,43.607A1.45,1.45,0,0,0,1.5,45h15A1.45,1.45,0,0,0,18,43.607V38.5H0Zm6-1.973a.363.363,0,0,1,.375-.348h4.25a.363.363,0,0,1,.375.348v1.161a.363.363,0,0,1-.375.348H6.375A.363.363,0,0,1,6,42.795Zm-4,0a.363.363,0,0,1,.375-.348h2.25A.363.363,0,0,1,5,41.634v1.161a.363.363,0,0,1-.375.348H2.375A.363.363,0,0,1,2,42.795Zm16-8.241v1.393H0V33.393A1.45,1.45,0,0,1,1.5,32h15A1.45,1.45,0,0,1,18,33.393Z" transform="translate(0 -32)" />
                            </svg>
                            &nbsp;
                            <span style="color: #2699FB; text-decoration: underline; font-weight: bold;">
                                <%: Html.TranslateTag("Payment Profile","Payment Profile") %>
                            </span>
                        </a>
                    </span>
                </div>

                <div class="x_content" id="rightContent">
                    <div class="col-12">
                        <div class="clearfix"></div>
                        <%string returns = "/Retail/Checkout/" + Model.account.AccountID + "?productType=Subscriptions";
                            if (Model.PaymentInfoModelList.Count == 0)
                            {%>
                        <%: Html.TranslateTag("Retail/PaymentOption|No Payment Methods Found","No Payment Methods Found")%>.<br />
                        <br />
                        <button class="btn gen-btn" role="link" onclick="window.location.href='/Retail/PaymentOption/<%=Model.account.AccountID %>?returnURL=<%=returns %>'"><%: Html.TranslateTag("Retail/PaymentOption|Add Payment Method","Add Payment Method")%></button>
                        <%} else {
                            foreach (PaymentInfoModel card in Model.PaymentInfoModelList)
                            {
                                string monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(card.ExpMonth);
                            %>
                                <div class="row dfac walletPurchaseMobile p-3">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="ccard wallet__option__card">
                  			<div class="card__chip">
			<svg class="card__chip-lines" role="img" width="20px" height="20px" viewBox="0 0 100 100" aria-label="Chip">
				<g opacity="0.8">
					<polyline points="0,50 35,50" fill="none" stroke="#000" stroke-width="2" />
					<polyline points="0,20 20,20 35,35" fill="none" stroke="#000" stroke-width="2" />
					<polyline points="50,0 50,35" fill="none" stroke="#000" stroke-width="2" />
					<polyline points="65,35 80,20 100,20" fill="none" stroke="#000" stroke-width="2" />
					<polyline points="100,50 65,50" fill="none" stroke="#000" stroke-width="2" />
					<polyline points="35,35 65,35 65,65 35,65 35,35" fill="none" stroke="#000" stroke-width="2" />
					<polyline points="0,80 20,80 35,65" fill="none" stroke="#000" stroke-width="2" />
					<polyline points="50,100 50,65" fill="none" stroke="#000" stroke-width="2" />
					<polyline points="65,65 80,80 100,80" fill="none" stroke="#000" stroke-width="2" />
				</g>
			</svg>
			<div class="card__chip-texture2"></div>
		</div>
                                                <div class="card__number">
                                                    <span class="card__digit-group">••••</span>
                                                    <span class="card__digit-group">••••</span>
                                                    <span class="card__digit-group">••••</span>
                                                    <span class="card__digit-group wallet__card__top__number"><%=card.CardNumber.Remove(0,(card.CardNumber.Length - 4) )%></span>
                                                </div>                                            

                                                <div class="wallet__card__bottom__exp"><%: Html.TranslateTag("Expires","Expires")%>: <%: Html.TranslateTag(monthName,monthName)%> <%=card.ExpYear %></div>
                                                <div class="wallet__card__bottom">
                                                    <div class="card__name"><%=card.CustomerName %></div>                                    </div>
                                            </div>

                                            <div class="col-3" style="margin-top:.8rem;">
                                                <button onclick="payWithCard(<%=card.ProfileID%>);" type="button" style="width:150px;" class="btn btn-primary checkoutButton"><%: Html.TranslateTag("Checkout","Checkout")%></button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="row pt-2">
                                            <div class="col-3">
                                                <label for="code"><%: Html.TranslateTag("Address","Address")%></label>
                                                <br />
                                                <b><%:Html.Raw(card.Address)%></b>
                                                        
                                                <%if (!string.IsNullOrWhiteSpace(card.Address2))
                                                  {%>
                                                    <br />
                                                    <b><%:Html.Raw(card.Address2)%></b>
                                                <%} %>
                                            </div>
                                            <div class="col-3">
                                                <label for="code"><%: Html.TranslateTag("City","City")%></label>
                                                <br />
                                                <b><%:Html.Raw(card.City)%></b>
                                            </div>
                                            <div class="col-3">
                                                <label for="code"><%: Html.TranslateTag("Country","Country")%></label>
                                                <br />
                                                <b><%:Html.Raw(card.Country)%></b>
                                            </div>
                                            <div class="col-3">
                                                <div class="row">
                                                    <div class="col-12 col-md-6 ps-0"<%-- style="padding-left: 0px"--%>>
                                                        <label for="code"><%: Html.TranslateTag("State","State")%> / <%: Html.TranslateTag("Province","Province")%></label>
                                                    <br />
                                                    <b><%:Html.Raw(card.State)%></b>
                                                    </div>
                                                    <div class="col-12 col-md-6">
                                                        <label for="code"><%: Html.TranslateTag("Postal Code","Postal Code")%></label>
                                                        <br />
                                                        <b><%:Html.Raw(card.PostalCode)%></b>
                                                    </div>
                                                    <%--<div class="clearfix"></div>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                          <%} %>
                            <br />
                            <div class="row" id="errorMessage" style="color: red; font-size: 1.2em; font-weight: bold;">
                            </div>
                        <%} %>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
        </div>
    </div>

    <script type="text/javascript">
        var tooManyMessage = '<%: Html.TranslateTag("Retail/PaymentOption|Account has too many sensors for this subscription.","Account has too many sensors for this subscription.")%>';

        var sensorCount = <%=Sensor.LoadByAccountID(Model.account.AccountID).Count()%>;
        var errDiv = $('#errorMessage');

        $(document).ready(function () {
            $("input[name=productChoice]").on("change", function () {
                clearErrorMessage();
                $('.checkoutButton').show();
            });
        });

        function payWithCard(profileID) {
            var productVals = $("input[name=productChoice]:checked").val();
            var productType = productVals.split("_")[0];
            var sku = productVals.split("_")[1];

            switch (productType) {
                case "Subscriptions":
                    var allowedSensors = Number(sku.split("-")[2]);
                    if (allowedSensors < sensorCount) {
                        setTimeout(clearErrorMessage, 5000);
                        errDiv.html(tooManyMessage);
                        return;
                    }
                    break;
                case "NotificationCredit":
                case "HxCredit":
                case "SensorPrint":
                case "SensorPrintCredit":
                case "GatewayUnlockCredit":
                case "GatewayUnlockGpsCredit":
                    break;
                default:
                    return;
            }

            window.location.href = "/Retail/PurchasePreview/<%=Model.account.AccountID%>?profileID=" + profileID + "&sku=" + sku;
        }

        function clearErrorMessage() {
            errDiv.html("");
        }
    </script>
</asp:Content>