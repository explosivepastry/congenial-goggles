<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<iMonnit.Models.CreateLocationAccountModel>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <%Account parentAccount = Account.Load(Model.ParentAccountID);
        string language = "english";
        if (Request.QueryString["language"] != null)
            language = Request.QueryString["language"].ToString();

        PurchaseLinkStoreModel storeModel = Session["PurchaseLinkStoreModel"] as PurchaseLinkStoreModel;
        Account userAccount = storeModel.account;
        ProductInfoModel selectedItem = storeModel.ProductList.Where(x => x.SKU == Model.SubscriptionSKU).FirstOrDefault();
        string productName = selectedItem != null ? selectedItem.DisplayName : "";
        string productPrice = selectedItem != null ? (selectedItem.Price - selectedItem.Discount).ToString("C") : "";
        string productTax = Model != null ? Model.TaxAmount.ToString("C") : "";
        string productTotal = selectedItem != null && Model != null ? ((selectedItem.Price - selectedItem.Discount) + Model.TaxAmount.ToDouble()).ToString("C") : "";
    %>

    <form method="post" action="/Settings/CreateLocationAccount/">
        <%Response.Write(ViewData["Exception"]);%>
        <%:Html.ValidationSummary(true) %>
        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>

        <input type="hidden" id="ParentAccountID" name="ParentAccountID" value="<%:Model.ParentAccountID %>" />
        <input type="hidden" id="PaymentID" name="PaymentID" value="<%=Model.PaymentID %>" />
        <input type="hidden" id="TaxAmount" name="TaxAmount" value="<%=Model.TaxAmount %>" />
        <input type="hidden" id="SalesOrderID" name="SalesOrderID" value="<%=Model.SalesOrderID %>" />
        <input type="hidden" id="SalesOrderItemID" name="SalesOrderItemID" value="<%=Model.SalesOrderItemID %>" />
        <input type="hidden" id="Total" name="Total" value="<%=Model.Total %>" />
        <input type="hidden" id="MaskedCardNumber" name="MaskedCardNumber" value="<%=Model.MaskedCardNumber %>" />
        <input type="hidden" id="CardExpDate" name="CardExpDate" value="<%=Model.CardExpDate %>" />
        <input type="hidden" id="CardOwnerName" name="CardOwnerName" value="<%=Model.CardOwnerName %>" />

        <div class="login_container">
            <div class="login_form_container">

                <div class="rule-card_container">
                    <div id="Form" class="login-form-container">

                        <div class="card_container__top">
                            <div class="card_container__top__title " style="justify-content: space-between;">
                                <div class="dfac newlocation-top d-flex">
                                    <%=Html.GetThemedSVG("location") %>
                                    <div class="card_container__top__title__text ms-2">
                                        <span>Add New Location</span>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="locate-box-control">
                            <div class=" create_input">
                                <div class="locate-p1">
                                    <div class="d-flex" style="flex-direction: column; width: 130px;">
                                        <label class="tag-name-locate"><%=Html.TranslateTag("Settings/CreateLocationAccount|Parent Location","Parent Location",language)%></label><span class="required"></span>
                                    </div>
                                    <br />
                                    <div class="w-75">
                                        <div id="parentLocationDiv"  class="d-flex form-control user-dets" <%=MonnitSession.CurrentCustomer.IsAdmin ? "data-bs-toggle=\"modal\" data-bs-target=\"#parentSearchResultsModal\"" : "" %>>
                                            <span id="parentLocationName" style="width:100%;"><%=parentAccount.CompanyName %></span>
                                            <span style=" margin-right: auto"><%=Html.GetThemedSVG("search") %></span>
                                        </div>
                                    </div>
                                </div>

                                <div class="locate-p1 " >
                                    <div class="d-flex" style="flex-direction: column; width: 130px;">
                                        <label class="tag-name-locate"><%=Html.TranslateTag("Settings/CreateLocationAccount|Company Name","Company Name",language)%></label>
                                        <div>
                                            <span class="required">*</span>
                                            <span style="font-size: smaller;">(<%=Html.TranslateTag("Settings/CreateLocationAccount|Must be unique","Must be unique",language)%>)</span>
                                        </div>
                                    </div>
                                    <div>
                                        <input class="form-control user-dets" id="companyName" name="CompanyName" required="required" type="text" value="<%=Model.CompanyName %>" />
                                    </div>
                                
                                </div>
                            </div>
                            <div class="form-group has-error">
                                <span id="companynameError" class="help-block" style="color:red;"><%: Html.ValidationMessageFor(model => model.CompanyName) %></span>
                            </div>

                            <div class="form-group has-error">
                                <span class="help-block" style="color:red;"><%: Html.ValidationMessageFor(model => model.SubscriptionSKU) %></span>
                            </div>

                            <%bool showBasicOptions = true;
                                if (MonnitSession.CurrentTheme.Theme == "Default")
                                {
                                    if (userAccount.StoreLinkGuid == Guid.Empty)
                                    {%>
                                        <a class="btn btn-primary" href="/Retail/LoginToStore/<%=userAccount.AccountID %>">Add Store Credentials</a>
                                    <%}
                                    else 
                                    {
                                        showBasicOptions = false;
                                        List<ProductInfoModel> premiereProducts = storeModel.ProductList;%>
                                        
                                    <div class="create_input">
                                        <div class="locate-p1">
                                            <div class="d-flex" style="flex-direction: column; width: 130px;">
                                                <label class="tag-name-locate"><%=Html.TranslateTag("Settings/CreateLocationAccount|Subscription","Subscription",language)%></label>
                                            </div>
                                            <div class="w-75">
                                                <select class="form-select user-dets" id="SubscriptionSKU" name="SubscriptionSKU" style="max-width: 360px;">
                                                    <option value="Basic" <%=Model.SubscriptionSKU == "Basic" ? "selected=selected" : "" %>>Basic</option>
                                                    <%if (MonnitSession.CurrentTheme.DefaultPremiumDays > 0)
                                                    {%>
                                                        <option value="Trial" <%=Model.SubscriptionSKU == "Trial" ? "selected=selected" : "" %>>Trial</option>
                                                    <%}
                                                    if (!ConfigData.AppSettings("IsEnterprise").ToBool() && !this.Request.IsSensorCertMobile())
                                                    {%>
                                                        <option value="Code" <%=Model.SubscriptionSKU == "Code" ? "selected=selected" : "" %>>Subscription Code</option>
                                                    <%} %>

                                                    <%foreach (ProductInfoModel product in premiereProducts)
                                                        {
                                                            if (product.SKU == "MNW-IP-500" || product.SKU == "MNW-IP-999")
                                                                continue;
                                                            double itemPrice = (product.Price - product.Discount);
                                                            string[] skuParts = product.SKU.Split('-');
                                                            int skuCount = skuParts[2].ToInt();%>

                                                        <option value="<%=product.SKU %>" data-price="<%=itemPrice%>" data-name="<%=product.DisplayName %>" <%=Model.SubscriptionSKU == product.SKU ? "selected=selected" : "" %>>
                                                            <%=product.Description.Replace("(Selected)", "") %> | <%=itemPrice.ToString("C") %>
                                                        </option>
                                                    <%} %>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <%}
                                }

                                if (showBasicOptions) 
                                {%>
                                <div class="create_input">
                                    <div class="locate-p1">
                                        <div class="d-flex" style="flex-direction: column; width: 130px;">
                                            <label class="tag-name-locate"><%=Html.TranslateTag("Settings/CreateLocationAccount|Subscription","Subscription",language)%></label>
                                        </div>
                                        <div class="w-75">
                                            <select class="form-select user-dets" id="SubscriptionSKU" name="SubscriptionSKU" style="max-width: 360px;">
                                                <option value="Basic" <%=Model.SubscriptionSKU == "Basic" ? "selected=selected" : "" %>>Basic</option>
                                                <%if (MonnitSession.CurrentTheme.DefaultPremiumDays > 0)
                                                {%>
                                                    <option value="Trial" <%=Model.SubscriptionSKU == "Trial" ? "selected=selected" : "" %>>Trial</option>
                                                <%}
                                                if (!ConfigData.AppSettings("IsEnterprise").ToBool() && !this.Request.IsSensorCertMobile())
                                                {%>
                                                    <option value="Code" <%=Model.SubscriptionSKU == "Code" ? "selected=selected" : "" %>>Subscription Code</option>
                                                <%} %>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            <%} %>
                            
                            <div class="create_input" id="walletOption" style="<%=(Model.PaymentID > 0 && !string.IsNullOrWhiteSpace(Model.SubscriptionSKU) && Model.SubscriptionSKU.Contains("MNW")) ? "" : "display: none;"%>">
                                <div class="locate-p1">
                                    <a class="btn btn-primary" href="#" data-bs-toggle="modal" data-bs-target="#walletModal">Set Payment</a>
                                </div>
                            </div>

                            <div class="create_input" id="ccPreview" style="<%=(Model.PaymentID > 0 && !string.IsNullOrWhiteSpace(Model.SubscriptionSKU) && Model.SubscriptionSKU.Contains("MNW")) ? "" : "display: none;"%>">
                                <div class="locate-p1 d-flex" style="justify-content:center;">
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
                                        <div class="card__number">
                                            <span class="card__digit-group" >••••</span>
                                            <span class="card__digit-group" >••••</span>
                                            <span class="card__digit-group" >••••</span>
                                            <span class="card__digit-group wallet__card__top__number" id="ccNum"><%=Model.MaskedCardNumber %></span>
                                        </div>

						                <div class="wallet__card__bottom__exp" id="expDate"><%=Model.CardExpDate %></div>
                                        <div class="wallet__card__bottom">
                                            <div class="card__name" id="ccName"><%=Model.CardOwnerName %></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="locate-p1">
                                    <div class="x_panel gridPanel shadow-sm rounded">
                                        <div class="x_content">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="col-12" style="padding-top: 5px; text-align: left; display:flex;justify-content:space-between; padding:0 10px;">
                                                        <span style="font-size: 1.2em; font-weight: 600; color: grey;">
                                                            <%: Html.TranslateTag("Product","Product")%>:
                                                        </span>
                                                        <span style="font-size: 1.2em; font-weight: 600;" id="purchaseProduct"><%=productName %></span>
                                                    </div>
                                                    <div class="col-12" style="padding-top: 5px; text-align: left; display:flex;justify-content:space-between; padding:0 10px;" id="itemPrice" >
                                                        <span style="font-size: 1.2em; font-weight: 600; color: grey;">
                                                            <%: Html.TranslateTag("Price","Price")%>:
                                                        </span>
                                                        <span style="font-size: 1.2em; font-weight: 600;" id="purchasePrice"><%=productPrice %></span>
                                                    </div>
                                                    <div class="col-12" style="padding-top: 5px; text-align: left; display:flex;justify-content:space-between; padding:0 10px;">
                                                        <span style="font-size: 1.2em; font-weight: 600; color: grey;">
                                                            <%: Html.TranslateTag("Tax","Tax")%>:
                                                        </span>
                                                        <span style="font-size: 1.2em; font-weight: 600;" id="purchaseTax"><%=productTax %></span>
                                                    </div>
                                                    <div class="col-12" style="padding-top: 5px; text-align: left;background:#eee; display:flex;justify-content:space-between; padding:5px 10px;" id="price">
                                                        <span style="font-size: 1.5em; font-weight: bold; color: #444;">
                                                            <%: Html.TranslateTag("Total","Total")%>:
                                                        </span>&nbsp;
                                                        <span style="font-size: 1.5em; font-weight: bold; color: #444;" id="purchaseTotal">
                                                            <%=productTotal %>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <h3 class="row" style="color: red; float: right; font-size:12px; max-width:500px; " id="purchaseMessage">
                                                <%: Html.ValidationMessageFor(model => model.PaymentID) %>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="create_input" id="subCodeInput" style="<%=Model.SubscriptionSKU == "Code" ? "" : "display: none;"%>>
                                <%if (!ConfigData.AppSettings("IsEnterprise").ToBool() && !this.Request.IsSensorCertMobile())
                                  {%>
                                <div class="locate-p1" style="margin: 20px 0">
                                    <div class="d-flex" style="flex-direction: column; width: 130px;">
                                        <label class="tag-name-locate"><%=Html.TranslateTag("Settings/CreateLocationAccount|Subscription Code","Subscription Code",language)%></label>

                                        <span style="font-size: smaller;">(<%=Html.TranslateTag("Settings/CreateLocationAccount|Free trial if left blank","Free Trial if left blank",language)%>)</span>
                                    </div>
                                    <div>
                                        <%--<input type="hidden" name="SubscriptionCode" value="None" />--%>
                                        <input class="form-control user-dets " id="subscriptionCode" name="SubscriptionCode" type="text" />
                                    </div>
                                </div>

                                <div class="form-group has-error">
                                    <span id="subscriptionCodeError" class="help-block"><%: Html.ValidationMessageFor(model => model.SubscriptionCode) %></span>
                                </div>
                                <%}
                                  else
                                  {%>
                                <input type="hidden" name="SubscriptionCode" value="0" />
                                <%} %>
                            </div>

                            <div class="create_input" style="display: flex; flex-direction: column;">
                                <div class="editor-label-small">
                                    <label class="tag-name-locate"><%=Html.TranslateTag("Time Zone","Time Zone",language)%></label><span class="required">*</span>
                                </div>
                                <div style="width: 100%;">
                                    <% string savedRegion = string.Empty;
                                        if (Model.TimeZoneID > 0)
                                            savedRegion = Monnit.TimeZone.Load(Model.TimeZoneID).Region.ToString();
                                    %>
                                    <select required id="Regions" class="form-select select-location-time">
                                        <option value="<%=Html.TranslateTag("Settings/CreateLocationAccount|Choose a region","Choose a region",language)%>"><%=Html.TranslateTag("Settings/CreateLocationAccount|Choose a region","Choose a region",language)%></option>
                                        <%foreach (string region in Monnit.TimeZone.LoadRegions())
                                          {%>
                                        <option <%: savedRegion == region  ? "selected='selected'" : "" %> value="<%:region%>"><%=Html.TranslateTag(region,region,language)%></option>
                                        <%}%>
                                    </select>
                                    <select class="form-select select-location-time" id="TimeZoneID" name="TimeZoneID">
                                    </select>
                                </div>
                                <div class="form-group has-error">
                                    <span class="help-block"><%: Html.ValidationMessageFor(model => model.TimeZoneID)%></span>
                                </div>
                                <div class="form-group has-error"></div>
                            </div>

                            <div class="create_input">
                                <div style="font-size: smaller;">
                                    <span class="required">*</span> <%=Html.TranslateTag("Settings/CreateLocationAccount|Required information","Required information",language)%>
                                </div>
                                <%if (MonnitSession.CurrentCustomer == null)
                                    {%>
                                <select onchange="switchLanguages(this.value)" class="form-select select-location-time">
                                    <%foreach (Language lang in Language.LoadActive())
                                      { %>
                                    <option value="<%=lang.Name %>" <%= language.ToLower() == lang.Name.ToLower() ? "selected='selected'" : "" %>><%=lang.DisplayName %></option>
                                    <%} %>
                                </select>
                                <%}%>
                            </div>
                        </div>

                        <div class="form nextCancel ">
                            <div>
                                <a href="/Settings/LocationOverview/<%:Model.ParentAccountID %>" class="btn btn-secondary my-2"><%=Html.TranslateTag("Cancel","Cancel",language)%></a>
                            </div>
                            <div>
                                <input id="nextSubmitBtn" type="submit" onclick="$(this).hide();$('#saving').show();" value="<%=Html.TranslateTag("Settings/CreateLocationAccount|Next","Next",language)%>" class="btn btn-primary w-100 my-2" />
                                <button class="btn btn-primary w-100 my-2" id="saving" style="display: none;" type="button" disabled>
                                    <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                    Creating...
                                </button>
                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </div>

    </form>
    
    <!-- parentSearchResults button modal -->
    <div class="modal fade" id="parentSearchResultsModal" style="z-index: 2000!important;" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" style="max-width:700px;">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title" id="pageHelp"><%: Html.TranslateTag("Settings/CreateLocationAccount|Select a Parent","Select a Parent")%> &nbsp;</h5>

                    <div class="search-account">
                        <input type="text" id="parentSearchInput" class="form-control search-input" style="max-width: 200px;" placeholder="<%: Html.TranslateTag("Settings/CreateLocationAccount|Search Locations")%>">
                        <button type="button" id="parentSearchBtn" class="btn btn-primary search-location-icon" value="Search">
                            <%=Html.GetThemedSVG("search") %>
                        </button>
                        <button type="button" id="parentSearch_Spinner" class="btn btn-primary" style="display: none;" disabled="">
                            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                            <span class="visually-hidden">Loading...</span>
                        </button>
                    </div>

                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body" id="parentSearchResults"></div>
                <div class="modal-footer"></div>
            </div>
        </div>
    </div>
    
    <!-- wallet button modal -->
    <div class="modal fade" id="walletModal" style="z-index: 2000!important;" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" style="max-width:700px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title"><%: Html.TranslateTag("Settings/CreateLocationAccount|Set Payment","Set Payment")%> &nbsp;</h5>

                    <button type="button" id="paymentModalCloseBtn" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="col-12 d-flex flex-wrap">
                        <div class="col-md-6 col-12 px-0 mb-4 <%=MonnitSession.CurrentStoreLinkInfo != null ? "pe-md-2" : "" %>">
                            <div class="rule-card_container w-100" style="min-height: 112px;">
                                <div class="card_container__top dfac">
                                    <div class="card_container__top__title">
                                      <div class="walleticon">  <%=Html.GetThemedSVG("wallet") %></div>
                                        <div style="max-width: 90%;" class="ms-2"><%: Html.TranslateTag("Settings/CreateLocationAccount|Wallet","Wallet")%></div>
                                    </div>
                                </div>
                                <div class="x_content" id="leftContent">
                                    <% Html.RenderPartial("~/Views/Retail/PaymentInfoList.ascx", storeModel); %>
                                    <div class="clearfix"></div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6 col-12 mb-4 ps-0 <%=MonnitSession.CurrentStoreLinkInfo != null ? "ps-md-2" : "" %>">
                            <div class="rule-card_container w-100">
                                <div class="card_container__top dfac">
                                    <div class="card_container__top__title">
                                        <%=Html.GetThemedSVG("card") %>
                                        <div style="max-width: 90%;" class="ms-2"><%: Html.TranslateTag("Settings/CreateLocationAccount|Add Card to Wallet","Add Card to Wallet")%></div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content" id="rightContent">
                                    <% Html.RenderPartial("~/Views/Retail/NewPaymentMethod.ascx", new PaymentInfoModel()); %>
                                    <div class="clearfix"></div>
                                    <div id="spinner"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer"></div>
            </div>
        </div>
    </div>


    <style>
        .goodBox {
            border-color: lightgreen;
        }
    </style>

    <script>

        $(function () {
            var customerIsAdmin = '<%=MonnitSession.CurrentCustomer.IsAdmin.ToString().ToLower()%>';
            var accError = '<%=Html.TranslateTag("Settings/CreateLocationAccount|Account name taken: Please choose another","Account name taken: Please choose another",language)%>';

            $('#companyName').focus();

            $('#companyName').change(function (e) {
                $('#companynameError').html("");
                $('#companyName').removeClass("goodBox");
                e.preventDefault();
                var name = $('#companyName').val()

                $.post("/Overview/CheckAccountNumber", { accountnumber: name }, function (data) {
                    if (data != "True") {
                        $('#companynameError').html(accError)
                    } else {
                        $('#companyName').addClass("goodBox");
                    }
                });
            });

            $('#TimeZoneID').hide();
            var timeID = '<%=Model.TimeZoneID%>';
            if (timeID != '0') {
                getTimeZone($('#Regions').val(), timeID)
            }

            $('#Regions').change(function () {
                var region = $('#Regions').val();
                getTimeZone(region, 0)
            });

            $('#parentLocationDiv').click(function () {
                if (customerIsAdmin == 'true') {
                    setTimeout(function () {
                        $('#parentSearchInput').focus().select();
                    }, 500);

                }
            });

            $('#parentSearchInput').keyup(function (e) {
                if (e.keyCode == 13 || this.value.length >= 3 ) {
                    $('#parentSearchBtn').click();
                }
            });

            $('#parentSearchBtn').click(function () {

                if (customerIsAdmin == 'true') {
                    var searchText = encodeURIComponent($('#parentSearchInput').val());

                    $('#parentSearchBtn').hide();
                    $('#parentSearch_Spinner').show();

                    $.get("/Settings/CreateLocationAccountParentList?searchCriteria=" + searchText, function (data) {
                        $('#parentSearchResults').html(data);
                        $('#parentSearch_Spinner').hide();
                        $('#parentSearchBtn').show();
                    });
                }
            });

            preparePaymentInfoList();

            $('#SubscriptionSKU').change(function () {
                var value = this.value;

                if (value == "Code") {
                    $('#subCodeInput').show();
                } else {
                    $('#subCodeInput').hide();
                }

                if (value != "Code" && value != "Basic" && value != "Trial") {
                    $('#walletOption').show();

                    var paymentID = Number($("#PaymentID").val());
                    if (paymentID <= 0) {
                        $('#ccPreview').hide();
                    } else {
                        $('#ccPreview').show();
                        updatePriceInfo();

                        var taxAttr = $('#SubscriptionSKU :selected').attr('data-tax');
                        if (taxAttr != undefined && taxAttr.length == 0) {
                            $('#SubscriptionSKU').attr('disabled', 'disabled');
                        }
                    }
                } else {
                    $('#walletOption').hide();
                    $('#ccPreview').hide();
                }
            });

            $('body').on('click', '.locationCheckoutCardBtn', function () {
                var ccID = $(this).attr('data-id');
                var ccNumber = $(this).attr('data-ccnum');
                var expDate = $(this).attr('data-expdate');
                var name = $(this).attr('data-name');

                $('#PaymentID').val(ccID);
                $('#ccNum').html(ccNumber);
                $('#expDate').html(expDate);
                $('#ccName').html(name);

                $('#MaskedCardNumber').val(ccNumber);
                $('#CardExpDate').val(expDate);
                $('#CardOwnerName').val(name);

                $('#ccPreview').show();
                $('#paymentModalCloseBtn').click();

                clearSkuOptionTaxes();
                updatePriceInfo();
            });

            $('body').on('click', '#saveCC', function (e) {
                e.preventDefault();

                if ($(this).hasClass("disabled"))
                    return false;
                $this = $(this);

                $this.addClass("disabled");
                $('#cardMessage').html("");

                var isCardValid = $.payform.validateCardNumber($('#cardNumber').val());
                var isExpiryValid = $.payform.validateCardExpiry(Number($('#expirationMonth').val()), Number($('#expirationYear').val()));

                setTimeout(allowClick, 4 * 1000);

                var allValid = true;
                if ($('#cardHolder').val().length < 5) {
                    $('#cardMessage').append(holdernameString);
                    allValid = false;
                }
                if (!isExpiryValid) {
                    $('#cardMessage').append(expiryValidString);
                    allValid = false;
                }

                if (!isCardValid) {
                    $('#cardMessage').append(cardnumberString);
                    allValid = false;
                }

                if ($('#zipcode').val() == "") {
                    $('#cardMessage').append(billingreqString);
                    allValid = false;
                }

                if ($('#city').val() == "") {
                    $('#cardMessage').append(cityreqString);
                    allValid = false;
                }

                if ($('#address1').val() == "") {
                    $('#cardMessage').append(addressreqString);
                    allValid = false;
                }

                if ($('#country').val() == "") {
                    $('#cardMessage').append(countryreqString);
                    allValid = false;
                }

                if (!allValid) {
                    $('#cardMessage').show();
                    setTimeout(clearErrorMessage, 4 * 1000);
                    $('#CCsaving').hide();
                    $('#saveCC').show();
                }
                else {
                    $.post('/Retail/SubmitNewPaymentLocations/<%=userAccount.AccountID%>', $("#creditCardForm").serialize(), function (data) {
                        $('#CCsaving').hide();
                        $('#saveCC').show();

                        if (data.startsWith("Failed")) {
                            $('#newCardMsg').html('<h3 style=color: red;>' + data + '</h3>');
                            $('#ccPreview').hide();

                        } else {
                            $('#newCardMsg').html('<h3 style=color: green;>Success</h3>');
                            $('#ccPreview').show();
                            $('#paymentModalCloseBtn').click();

                            data = data.replace('Success:', '');
                            var dataArray = data.split('|');
                            var ccID = dataArray[0];
                            var ccNumber = dataArray[1];
                            var expDate = dataArray[2];
                            var name = dataArray[3];

                            $('#PaymentID').val(ccID);
                            $('#ccNum').html(ccNumber);
                            $('#expDate').html('<%=Html.TranslateTag("Expires","Expires")%>: ' + expDate);
                            $('#ccName').html(name);

                            $('#MaskedCardNumber').val(ccNumber);
                            $('#CardExpDate').val(expDate);
                            $('#CardOwnerName').val(name);

                            clearSkuOptionTaxes();
                            updatePriceInfo();

                            $('#leftContent').html('Loading...');
                            $.get('/Retail/PaymentInfoList/', function (data) {
                                $('#leftContent').html(data);

                                preparePaymentInfoList();
                            });
                        }
                    });
                }
            });
        });

        function switchLanguages(languageName) {
            var old_url = window.location.href;
            var new_url = old_url.substring(0, old_url.indexOf('?'));
            window.location.href = new_url + "?language=" + languageName;
        }

        function getTimeZone(region, timeID) {
            $.post('/Account/GetTimeZones/', { Region: region }, function (data) {

                $('#TimeZoneID').empty();
                $('#TimeZoneID').show();
                $.each(data, function (value) {
                    var splitvals = data[value].split("|");
                    var text = splitvals[1];
                    var selection = splitvals[0];
                    var opt = document.createElement('option');
                    opt.text = text;
                    opt.value = selection;

                    var tzselector = $('#TimeZoneID').get(0);
                    tzselector.add(opt, null);
                });
                if (timeID != 0) {
                    $('#TimeZoneID').val(timeID).change();
                }
            });
        }

        function clearErrorMessage() {
            $('#cardMessage').fadeOut('slowest');
            setTimeout(function () { $('#cardMessage').html("") }, 2 * 1000);
        }
        function allowClick() {
            $this.removeClass("disabled");
        }

        function preparePaymentInfoList() {
            $('.removeCardDiv').hide();
            $('.locationCheckoutCardBtn').show();
        }

        function updatePriceInfo() {
            var profileID = Number($('#PaymentID').val());

            if (profileID > 0) {
                $('#nextSubmitBtn').attr('disabled', 'disabled');

                var sku = $('#SubscriptionSKU').val();
                var productName = $('#SubscriptionSKU :selected').attr('data-name');
                var price = Number($('#SubscriptionSKU :selected').attr('data-price'));
                var taxString = $('#SubscriptionSKU :selected').attr('data-tax');
                var tax = Number(taxString);
                var total = price + tax;
                $('#purchaseProduct').html(productName);
                $('#purchasePrice').html(price.toLocaleString("en-US", { style: "currency", currency: "USD" }));
                $('#purchaseTax').html('');
                $('#purchaseTotal').html('');

                if (tax >= 0 && taxString.length > 0) {
                    $('#purchaseTax').html(tax.toLocaleString("en-US", { style: "currency", currency: "USD" }));
                    $('#purchaseTotal').html(total.toLocaleString("en-US", { style: "currency", currency: "USD" }));
                    $('#TaxAmount').val(tax);
                    $('#nextSubmitBtn').removeAttr('disabled');
                } else {
                    $.get('/Settings/RetrieveItemTaxValue/', { sku: sku, paymentProfileID: profileID }, function (data) {
                        tax = Number(data);
                        total = price + tax;

                        // See if any other option already exists with this value
                        if (tax > 0 && $('#SubscriptionSKU option[data-tax="' + tax + '"]').length > 0) {
                            // Because it does, that means something loaded incorrectly so clear all values
                            clearSkuOptionTaxes();
                        }

                        $('#purchaseTax').html(tax.toLocaleString("en-US", { style: "currency", currency: "USD" }));
                        $('#purchaseTotal').html(total.toLocaleString("en-US", { style: "currency", currency: "USD" }));
                        $('#SubscriptionSKU :selected').attr('data-tax', tax);
                        $('#TaxAmount').val(tax);

                        $('#SubscriptionSKU').removeAttr('disabled');
                        $('#nextSubmitBtn').removeAttr('disabled');
                    });
                }

            }
        }

        function clearSkuOptionTaxes() {
            $('#SubscriptionSKU option').each(function () {
                var obj = $(this);
                obj.attr('data-tax', '');
            });
        }
    </script>
</asp:Content>
