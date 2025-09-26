<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PurchaseLinkStoreModel>" %> <%="" %>

<%  string errorMsg = ViewBag.ErrorMsg;
	bool currentCustomerHasDefaultPayment = MonnitSession.CurrentCustomer.Account.DefaultPaymentID > 0 ? true : false;


	if (Model == null || (Model != null && Model.PaymentInfoModelList.Count == 0))
	{%>
    <div class="col-12">
        <% if (!string.IsNullOrWhiteSpace(errorMsg))
           {%>
            <h2 style="color: red;"><%:errorMsg%></h2>
         <%}
           else
           {%>
                <%: Html.TranslateTag("Retail/PaymentOption|No Payment Methods Found","No Payment Methods Found")%>
                <br />
         <%} %>
    </div>
<%}
else 
{
    if (!string.IsNullOrWhiteSpace(errorMsg))
    {%>
        <h5 id="cardErrorMsg" style="color: red;"><%=errorMsg %></h5>
        <script>
            $(function () {
                var cemObj = $('#cardErrorMsg');
                window.scroll(0, cemObj.offset().top - 75);
            });
        </script>
    <%}
    foreach (PaymentInfoModel card in Model.PaymentInfoModelList)
    {
        if(string.IsNullOrEmpty(card.CardNumber))
            continue;
        string monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(card.ExpMonth);
    %>
        <div class="row dfjcc">
            <div class="col-12">
                <div class="row" style="justify-content:center; ">
                    <div class="d-xs-none d-sm-block "></div>
                    <div class="ccard wallet__card">
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
			<div class="card__chip-texture"></div>
		</div>
                        <div class="card__number  ">
                            <span class="card__digit-group" >••••</span>
                            <span class="card__digit-group" >••••</span>
                            <span class="card__digit-group" >••••</span>
                            <span class=" card__digit-group wallet__card__top__number"><%=card.CardNumber.Remove(0,(card.CardNumber.Length - 4) )%></span>
                        </div>

						 <div class="wallet__card__bottom__exp"><%: Html.TranslateTag("Expires","Expires")%>: <%: Html.TranslateTag(monthName,monthName)%> <%=card.ExpYear %></div>
                        <div class="wallet__card__bottom">
                            <div class="card__name"><%=card.CustomerName %></div>
                           
                        </div>
                    </div>

					        <%if (MonnitSession.CurrentTheme.Theme == "Default" && Model.account.AutoPurchase == true || !currentCustomerHasDefaultPayment && MonnitSession.CurrentTheme.Theme == "Default")
								{%>
        <div class="col sensorEditFormInput d-flex flex-column">
            <label class="col-12">
                <input type="checkbox" class="defaultPaymentChxbox" id="defaultPaymentChxbox_<%:card.ProfileID %>" data-profileid="<%=card.ProfileID%>" <%=Model.account.DefaultPaymentID == card.ProfileID ? "checked='checked'" : "" %> />

                <%: Html.TranslateTag("Retail/NotificationCredit|Set Default Payment", "Set Default Payment")%>&nbsp;
            </label>
        </div>
							<%}%>

                    <div style="width:10%; padding: 5px 0 0 5px;" class="removeCardDiv">
                        <div class="text-end">
                            <div onclick="removeCard(<%=card.ProfileID%>);">
                                <span title="<%: Html.TranslateTag("Remove Card","Remove Card")%>" style="cursor:pointer;"><%=Html.GetThemedSVG("delete") %></span>
                            </div>
                         
                        </div>
                    </div>
                    <div class="d-flex " style="justify-content:center;">
					<button type="button" class="btn-lg  newActionBtn  locationCheckoutCardBtn" style="display: none; border: none; padding-left: 0; justify-content:center; width:100%;" data-id="<%=card.ProfileID %>" data-ccnum="<%=card.CardNumber.Remove(0, (card.CardNumber.Length - 4)) %>" 
							data-expdate="<%= Html.TranslateTag("Expires","Expires")%>: <%=Html.TranslateTag(monthName,monthName)%> <%=card.ExpYear %>" data-name="<%=card.CustomerName %>">
						Use this Card
					</button>
						</div>

					   <%if (!string.IsNullOrWhiteSpace(ViewBag.SKU))
                              {%>
                                <div>
                                    <br /><br />
                                    <button type="button" class="btn-lg  newActionBtn checkoutCardBtn"  style=" border: none; padding-left: 0; justify-content:center;" data-id="<%=card.ProfileID %>">Use this Card</button>
                                </div>
                            <%} %>
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
        <hr />
    <%}
} %>

<style>
    

.ccard,
.card__chip {
	overflow: hidden;
	position: relative;
}
.ccard,
.card__chip-texture,
.card__texture:hover {
	animation-duration: 3s;
	animation-timing-function: ease-in;
	animation-iteration-count: infinite;
}

.ccard {
	    font-size: calc(20px + (25 - 15) * (25vw - 720px) / (1280 - 220));
	background-color: var(--primary-color);
	background-image:
		radial-gradient(circle at 100% 0%,hsla(0,0%,100%,0.08) 29.5%,hsla(0,0%,100%,0) 30%),
		radial-gradient(circle at 100% 0%,hsla(0,0%,100%,0.08) 39.5%,hsla(0,0%,100%,0) 40%),
		radial-gradient(circle at 100% 0%,hsla(0,0%,100%,0.08) 49.5%,hsla(0,0%,100%,0) 50%);
	border-radius: 0.5em;
	box-shadow:
		0 0 0 hsl(0,0%,80%),
		0 0 0 hsl(0,0%,100%),
		-0.2rem 0 0.75rem 0 hsla(0,0%,0%,0.3);
	color: hsl(0,0%,100%);
	width: 14.3em;
	height: 8.8em;
	/*transform: translate3d(0,0,0);*/
}
.card__info,
.card__chip-texture,
.card__texture {
	position: absolute;
}
.card__chip-texture,
.card__texture {
	animation-name: texture;
	top: 0;
	left: 0;
	width: 200%;
	height: 100%;
}
.card__info {
	font: 0.75em/1 "DM Sans", sans-serif;
	display: flex;
	align-items: center;
	flex-wrap: wrap;
	padding: 0.75rem;
	inset: 0;
}
.card__logo,
.card__number {
	width: 100%;
}
.card__logo {
	font-weight: bold;
	font-style: italic;
}
.card__chip {
	background-image: linear-gradient(hsl(0,0%,70%),hsl(0,0%,80%));
	border-radius: 0.2rem;
	box-shadow: 0 0 0 0.05rem hsla(0,0%,0%,0.5) inset;
	width: 1.25rem;
	height: 1.25rem;
	transform: translate3d(0,0,0);
}
.card__chip-lines {
	width: 100%;
	height: auto;
}
.card__chip-texture {
	background-image: linear-gradient(-80deg,hsla(0,0%,100%,0),hsla(0,0%,100%,0.6) 48% 52%,hsla(0,0%,100%,0));
}
.card__type {
	align-self: flex-end;
	margin-left: auto;
}
.card__digit-group,
.card__exp-date,
.card__name {
	background: linear-gradient(hsl(0,0%,100%),hsl(0,0%,85%) 15% 55%,hsl(0,0%,70%) 70%);
	-webkit-background-clip: text;
	-webkit-text-fill-color: transparent;
	font-family: "Courier Prime", monospace;
	filter: drop-shadow(0 0.05rem hsla(0,0%,0%,0.3));
}
.card__number {
	font-size: 0.8rem;
	display: flex;
	justify-content: space-between;
}
.card__valid-thru,
.card__name {
	text-transform: uppercase;
}
.card__valid-thru,
.card__exp-date {
	margin-bottom: 0.25rem;
	width: 50%;
}
.card__valid-thru {
	font-size: 0.3rem;
	padding-right: 0.25rem;
	text-align: right;
}
.card__exp-date,
.card__name {
	font-size:1rem;
}
.card__exp-date {
	padding-left: 0.25rem;
}
.card__name {
	overflow: hidden;
	white-space: nowrap;
	text-overflow: ellipsis;
/*	width: 6.25rem;*/
}
.card__vendor,
.card__vendor:before,
.card__vendor:after {
	position: absolute;
}
.card__vendor {
	right: 0.375rem;
	bottom: 0.375rem;
	width: 2.55rem;
	height: 1.5rem;
}
.card__vendor:before,
.card__vendor:after {
	border-radius: 50%;
	content: "";
	display: block;
	top: 0;
	width: 1.5rem;
	height: 1.5rem;
}
.card__vendor:before {
	background-color: #e71d1a;
	left: 0;
}
.card__vendor:after {
	background-color: #fa5e03;
	box-shadow: -1.05rem 0 0 #f59d1a inset;
	right: 0;
}
.card__vendor-sr {
	clip: rect(1px,1px,1px,1px);
	overflow: hidden;
	position: absolute;
	width: 1px;
	height: 1px;
}
.card__texture {
	animation-name: texture;
	background-image: linear-gradient(-80deg,hsla(0,0%,100%,0.3) 25%,hsla(0,0%,100%,0) 45%);
}

/* Dark theme */
@media (prefers-color-scheme: dark) {
	:root {
		--bg: hsl(var(--hue),10%,30%);
		--fg: hsl(var(--hue),10%,90%);
	}
}



</style>

<script>
    var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
</script>