<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Account>" %>

<div class="rule-card_container w-100">
    <div class="x_title">

        <div class="card_container__top__title dfjcsb" style="width: 100%;">
            <span style="display: flex; align-items: center; overflow: inherit;">
                <%=Html.GetThemedSVG("subscription") %>
                <span class="ms-1"><%: Html.TranslateTag("Subscription","Subscription")%></span>
            </span>

            <div class="text-end" style="margin-left: auto;">
                <%if (Model.PrimaryContactID == MonnitSession.CurrentCustomer.CustomerID || Model.AccountID == MonnitSession.CurrentCustomer.AccountID || MonnitSession.IsCurrentCustomerMonnitAdmin)
                    {

                        //string removeAllSensors = Html.TranslateTag("You must remove all sensors and gateways from the account you wish to delete!");
                        //bool noDevices = (Gateway.LoadByAccountID(Model.AccountID).Count == 0 && Sensor.LoadByAccountID(Model.AccountID).Count == 0);
                        %>

                <div <%= /*noDevices ? "" : "title='" + removeAllSensors + "'"*/ "" %>>

                    <button id="remove"
                        class="btn btn-danger"
                        <%= /*noDevices ? "" : "disabled"*/ "" %>>
                            <%: Html.TranslateTag("Delete Account","Delete Account")%>
                    </button>
                </div>
                <% } %>
                <div style="clear: both;"></div>
            </div>
        </div>

        <div class="dfjcsb pt-1">
                <%if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Account_Set_Premium"))
                { %>
            <a role="button" class="btn btn-secondary btn-sm" href="/Settings/AdminSubscriptionDetails/<%=Model.AccountID %>"><%: Html.TranslateTag("Edit","Edit")%>
                <%=Html.GetThemedSVG("edit") %>
            </a>
            <%  } %>

            <div class="text-end" style="margin-left: auto;">
            <%if (Model.AccountID != MonnitSession.CurrentCustomer.AccountID && (MonnitSession.IsCurrentCustomerMonnitAdmin))// || MonnitSession.IsCurrentCustomerReseller))
              { %>
                <a style="text-decoration: underline;" class="btn btn-link" href="/Network/List?accountID=<%:Model.AccountID %>">
                    <%: Html.TranslateTag("Settings/_AccountDetails|View Networks","View Networks")%>
                </a>
            <%} %>
        </div>
        </div>

        <div class="clearfix"></div>
    </div>

    <div class="x_content">
        <div class="">
            <br />
            <form class="form-horizontal form-label-left">
                <%if (MonnitSession.CustomerCan("Navigation_View_My_Account"))
                  { %>
                    <div class="innerCard__title-small">
                        <%: Html.TranslateTag("Settings/AccountDetail|Active Subscriptions","Active Subscriptions")%>
                    </div>
                    <div class="form-group sub-list__container ">
                    <% List<AccountSubscription> subList = AccountSubscription.LoadByAccountID(Model.AccountID).Where(m => m.ExpirationDate > DateTime.UtcNow).ToList();
                        foreach (AccountSubscription subscription in subList)
                        {
                            if (subscription.AccountSubscriptionTypeID == AccountSubscriptionType.BasicID)
                                continue;%>
                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12 dfac pd5">
                                <span style="font-size: 14px; display: flex; align-items: center;">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="7" height="7" viewBox="0 0 10 10">
                                        <path id="ic_lens_24px" d="M7,2a5,5,0,1,0,5,5A5,5,0,0,0,7,2Z" transform="translate(-2 -2)" fill="#21ce99" />
                                    </svg>
                                    &nbsp;&nbsp;
                                    <%: subscription.AccountSubscriptionType.Name%>
                                </span>
                                &nbsp;&nbsp;&nbsp; 
                        <span><%: Html.TranslateTag("Expires","Expires")%>: <%= subscription.ExpirationDate.OVToLocalDateTimeShort() %></span>
                            </div>
                            <br />
                      <%} %>

                        <div class="clearfix"></div>
                    </div>

                    <div class="form-group row-d-col">
                        <div class="innerCard__title-small">
                            <%: Html.TranslateTag("Settings/AccountDetail|Account Access Token","Account Access Token")%>
                        </div>

                        <div class="col-10">
                            <%if (Model.AccessTokenExpirationDate == null || Model.AccessTokenExpirationDate == DateTime.MinValue || Model.AccessTokenExpirationDate < DateTime.UtcNow)
                              {
                                if (MonnitSession.CurrentCustomer.AccountID == Model.AccountID)
                                {%>
                                <input class="btn btn-secondary btn-sm" onclick="editToken('create');" type="button" value=" <%: Html.TranslateTag("Settings/AccountDetail|Generate Token","Generate Token")%>" />
                              <%}
                              }
                              else
                              {%>
                                <span style="font-size: 2.2em; color: #666666; font-weight: bold; height: auto; vertical-align: initial;" id="accessToken"><%= Model.AccessToken%></span> <a style="cursor: pointer;" title="<%: Html.TranslateTag("Settings/AccountDetail|Copy to clipboard","Copy to clipboard")%>" onclick="copyToClipboard();"><i style="font-size: 1.0em; vertical-align: central;" class="fa fa-clipboard"></i></a>&nbsp;&nbsp;&nbsp;
                                <%if (MonnitSession.CurrentCustomer.AccountID == Model.AccountID)
                                  {  %>
                                    <input class="btn btn-danger btn-sm" type="button" onclick="editToken('revoke');" value="<%: Html.TranslateTag("Settings/AccountDetail|Revoke Token","Revoke Token")%>" />
                                    <%if (DateTime.UtcNow.AddDays(1) > Model.AccessTokenExpirationDate)
                                      {  %>
                                        <input class="btn btn-primary btn-sm" type="button" onclick="editToken('extend');" value="<%: Html.TranslateTag("Settings/AccountDetail|Extend Token","Extend Token")%>" />
                                    <%}
                                  }%>
                                <br>
                        <span style="font-weight: bold; font-size: 0.8em; font-style: italic;"><%: Html.TranslateTag("Expires","Expires")%>:</span> <%= Model.AccessTokenExpirationDate.OVToLocalDateTimeShort() %>
                            <%} %>
                            <div id="errorDiv" class="bold" style="color: red;"></div>
                            <div class="clearfix"></div>
                        </div>
                        <br />
                    </div>

                    <div class="form-group" style="background-color: white;">
                        <div class="bold col-sm-2 col-12" style="font-weight: bold;">
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-lg-12 input-group" style="padding-top: 10px; display: flex;">
                            <%if (MonnitSession.CustomerCan("Can_Access_Billing"))
                              { %>
                        <input class="form-control user-dets" style="max-width: 300px;" title="<%: Html.TranslateTag("Settings/AccountDetail|Enter Activation Code Here.Please include dashes. Format","Enter Activation Code Here.Please include dashes. Format")%>: XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX" type="text" name="SubscriptionActivationCode" id="SubscriptionActivationCode" placeholder="<%: Html.TranslateTag("Settings/AccountDetail|Activation Code", "Activation Code")%>" />
                        <input type="button" class="btn btn-primary " style="border-radius: 0 5px 5px 0;" value="<%: Html.TranslateTag("Submit", "Submit")%>" id="renewSub" />
                        <button class="btn btn-primary" id="renewing" style="display: none;" type="button" disabled>
                        <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                            <%: Html.TranslateTag("Activating","Activating")%>...
                                </button>
                            <%} %>
                        </div>
                    
                        <div class="bold col-sm-2 col-12" id="redeemMessage" style="color: red;">
                        </div>
                    </div>
                    <% Html.RenderPartial("_PurchaseOptions"); %>
                <%} %>
            </form>
        </div>
    </div>
</div>
<%if (MonnitSession.IsEnterprise)
    { %>
    <div class="x_panel shadow-sm rounded my-4">
    <div class="x_title">
        <div class="card_container__top__title dfjcsb" style="width: 100%;">
            <span style="display: flex; align-items: center; overflow: inherit;">
                <%: Html.TranslateTag("Enterprise Details", "Enterprise Details")%>
            </span>
        </div>
    </div>

    <div class="innerCard__title-small">
        <%: Html.TranslateTag("Settings/AccountDetail|Enterprise Version","Enterprise Version")%>
    </div>

    <div style="color: #B4B4B4; font-size: 11px; margin-top: -7px;">
        <%: ConfigData.AppSettings("EnterpriseType") %>
    </div>
    <br />

    <div class="innerCard__title-small">
        <%: Html.TranslateTag("Settings/AccountDetail|Enterprise Key","Enterprise Key")%>
    </div>
    <div style="color: #B4B4B4; font-size: 11px; margin-top: -7px;">
        <%: ConfigData.AppSettings("EnterpriseKey") %>
    </div>
    <br />

    <div class="innerCard__title-small">
        <%: Html.TranslateTag("Settings/AccountDetail|Expiration Date","Expiration Date")%>
    </div>
    <div style="color: #B4B4B4; font-size: 11px; margin-top: -7px;">
        <%: ConfigData.AppSettings("EnterpriseExpiration") == "" ? "Does not expire" : ConfigData.AppSettings("EnterpriseExpiration") %>
    </div>
    <br />
</div>
<%}%>

<%--<%if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CurrentCustomer.CanAssignParent(Model.AccountID))
    { %>
<div class="rule-card_container w-100">
    <div class="x_title">
        <div class="card_container__top__title"><%: Html.TranslateTag("Settings/AccountDetail|Admin","Admin ") %> <%: Html.TranslateTag("Settings/AccountDetail|Parent Account","Parent Account") %></div>
    </div>
    
    <div class="x_content">
        <div class="form-group">
            <div class="col-12" style="font-weight: bold;">
                <%string parentName = Html.TranslateTag("None", "None");
                    if (Model.RetailAccountID > 0)
                        parentName = Account.Load(Model.RetailAccountID).AccountNumber.ToString(); %>
                <%= parentName %>
            </div>
        </div>
        <br />
    
        <div class="aSettings__title" style="margin-top: 8px; font-weight: bold;">
            <%: Html.TranslateTag("Settings/AccountDetail|Account Look Up","Account Look Up") %>
        </div>

        <div class="input-group">
            <input type="text" id="accountLookUp" class="form-control user-dets" placeholder="<%: Html.TranslateTag("Account ID","Account ID")%>"style="max-width: 250px;" />
            <input type="button" class="btn btn-primary" id="AcctCheck" value="<%: Html.TranslateTag("Search","Search")%>" />
        </div>

        <div class=" dfac">
            <div id="nameBox" class="bold col-sm-2 col-12"></div>
            <div class="col-sm-3 col-12">
                <input type="button" class="btn btn-primary" id="AcctSave" style="display: none;" title="<%: Html.TranslateTag("Settings/AccountDetail|Save as Parent Account","Save as Parent Account")%>" value="<%: Html.TranslateTag("Save","Save")%>" />
            </div>
            <div class="clearfix"></div>
        </div>
    </div>
</div>
<%}
     %>--%>

<div style="clear: both;"></div>

<div class="modal fade premiere" tabindex="-1" role="dialog" aria-hidden="true">
    <div class="modal-dialog modal-sm">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="<%: Html.TranslateTag("Close","Close")%>">
                    <span aria-hidden="true">×</span>
                </button>
                <h4 class="modal-title" id="myModalLabel2"><%: Html.TranslateTag("Settings/AccountDetail|Account Settings Help","Account Settings Help")%></h4>
            </div>

            <div class="modal-body">
                <div class="row">
                    <div class="bold col-md-12 col-sm-12 col-xs-12">
                        <%: Html.TranslateTag("Settings/AccountDetail|Current Subscription","Current Subscription")%>
                    </div>

                    <div class="col-md-12 col-sm-12 col-xs-12" style="font-size: 0.8em;">
                        <%: Html.TranslateTag("Settings/AccountDetail|If you have already purchased or extended your subscription, Just Fill in your Premier Subscription Code and click Redeem! Click Purchase to extend a premiere subscription with a credit card","If you already purchased or extended your subscription, Just Fill in your Premier Subscription Code and click Redeem! Click Purchase to extend a premiere subscription with a credit card")%>
                        <hr />
                    </div>
                </div>

                <div class="row">
                    <div class="innerCard__title-small">
                        <%: Html.TranslateTag("Settings/AccountDetail|Account Access Token","Account Access Token")%>
                    </div>
                    
                    <div class="col-md-12 col-sm-12 col-xs-12" style="font-size: 0.8em;">
                        <%: Html.TranslateTag("Settings/AccountDetail|To grant support access to your account for issue resolution they will request an Account Access Token. This token will allow them access to your account for one (1) day by default, or until you revoke it. To generate a token click the Generate Token Button. The Token will be six (6) characters long numbers and letters all uppercase. It will appear in place of the generate button. After a token is created, you may extend the expiration date to eight (8) days by clicking the Extend Token button. You may revoke the token at any time.","To grant support access to your account for issue resolution they will request an Account Access Token. This token will allow them access to your account for one (1) day by default, or until you revoke it. To generate a token click the Generate Token Button. The Token will be six (6) characters long numbers and letters all uppercase. It will appear in place of the generate button. After a token is created, you may extend the expiration date to eight (8) days by clicking the Extend Token button. You may revoke the token at any time.")%>
                        <hr />
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal"><%: Html.TranslateTag("Close","Close")%></button>
            </div>
        </div>
    </div>
</div>
<!-- End help button -->

<script type="text/javascript">

    var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';

    $(function () {
        var validNum = "<%: Html.TranslateTag("Settings/AccountDetail|Please enter a valid account number","Please enter a valid account number")%>";
        var SureDelete = "<%: Html.TranslateTag("Settings/AccountDetail|Are you sure you want to delete the account","Are you sure you want to delete the account")%>";
        var SureUpdate = "<%: Html.TranslateTag("Settings/AccountDetail|Are you sure you want to update the parent account","Are you sure you want to update the parent account")%>";
        var cantPurchase = "<%: Html.TranslateTag("Settings/AccountDetail|This user does not have permission to purchase premiere. Contact account administrator or support for more help.","This user does not have permission to purchase premiere. Contact account administrator or support for more help.")%>";
        var canSeeBilling ="<%=MonnitSession.CustomerCan("Can_Access_Billing").ToString()%>";

        $('#purchase').removeClass('gen-btn-2');
        $('#purchase').addClass('btn btn-primary btn-sm');

        $('#renewSub').click(function () {
            if ($('#SubscriptionActivationCode').val().length === 0) {
                $('#SubscriptionActivationCode').attr("placeholder", "Code Required");
                return;
            }
            $('#renewSub').hide();
            $('#renewing').show();

            var url = '/Retail/ActivateCredits/<%:Model.AccountID %>?activationCode=' + $('#SubscriptionActivationCode').val() + '&creditClassification=1';
            $.post(url, "", function (data) {
                if (data.match("^Success")) {
                    var SKU = data.split('_')[1];
                    var splitArray = data.split('|');
                    if (data.toLowerCase().includes("_")) {
                        splitArray = data.split('_')[0].split('|');
                    }
                    var purchasedItemID = splitArray != undefined && splitArray.length > 1 ? splitArray[1] : '';
                    window.location.href = "/Retail/PurchaseConfirm/<%=Model.AccountID%>?sku=" + SKU + "&purchasedItemID=" + purchasedItemID;
                }
                else {
                    $('#redeemMessage').html(data.split('_')[0]);

                    $('#renewSub').show();
                    $('#renewing').hide();
                }
            });
        });

        $('#purchase').click(function () {
            if (canSeeBilling == 'True') {
                var purchaseLink = "/Retail/Checkout/<%=Model.AccountID%>?productType=Subscriptions";
                location.href = purchaseLink;
            } else {
                $('#premiereErr').html(cantPurchase);
                setTimeout(clearPremiereErr, 10000);
            }
        });

        $('#remove').click(function (e) {
            e.preventDefault();
            let values = {};
            values.url = '/Settings/AccountRemove/<%:Model.AccountID %>';
            values.text = SureDelete;
            values.callback = function (data) {

                if (data == "Success|Admin") {
                    disableUnsavedChangesAlert();
                    window.location = '/Settings/AdminSearch';
                } else if (data == "Success") {
                    disableUnsavedChangesAlert();
                    window.location = '/Overview/Index';
                } else {
                    showSimpleMessageModal(data);
                }
            };
            openConfirm(values);
        });

            <%--var url = '/Settings/AccountRemove/<%:Model.AccountID %>';
            if (confirm(SureDelete)) { // change to 'openConfirm'
                $.get(url, function (data) {
                    showSimpleMessageModal(data);
                    if (data == "Success|Admin") {
                        disableUnsavedChangesAlert();
                        window.location = '/Settings/AdminSearch';
                    } else if (data == "Success") {
                        disableUnsavedChangesAlert();
                        window.location = '/Overview/Logoff';
                    }
                });
            }--%>

        <%--$('#AcctCheck').click(function () {
            var acctnumber = $('#accountLookUp').val()
            if (acctnumber.length < 1 || !isANumber(acctnumber)) {
                showSimpleMessageModal("<%=Html.TranslateTag("Please enter a valid account number")%>");
            }
            $.get("/Account/accountLookUp?accountID=" + acctnumber, function (data) {
                $('#nameBox').html(data);
                if (data != 'No Account Found') {
                    $('#AcctSave').show();
                }
            });
        });

        $('#AcctSave').click(function () {
            var acctnumber = $('#accountLookUp').val()
            if (acctnumber.length < 1 || !isANumber(acctnumber)) {
                showSimpleMessageModal("<%=Html.TranslateTag("Please enter a valid account number")%>");
            }
            else {
                var url = "/Account/UpdateAccountParent?accountID=" + <%=Model.AccountID%> + "&parentID=" + acctnumber;
                if (confirm(SureUpdate + ' ?')) {
                    $.get(url, function (data) {
                        if (data == "Success") {
                            $('#name').val('Success');
                            window.location.reload();
                        }
                        else {
                            console.log(data);
                            showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                        }
                    });
                }
            }
        });--%>

        // create a datepicker with default settings
        //$("#scroller").mobiscroll().date({
        //	display: popLocation,
        //	theme: "ios"
        //});
    });

    function editToken(tokenAction) {
        $.post("/Settings/EditAccessToken", { id: '<%=Model.AccountID%>', action: tokenAction }, function (data) {
            if (data == "Success") {
                window.location.href = window.location.href;
            }
            else {
                $('#errorDiv').html(data);
            }
        });
    }

    function clearPremiereErr() {
        $('#premiereErr').html('');
    }

    function copyToClipboard() {
        var $temp = $("<input>");
        $("body").append($temp);
        $temp.val($('#accessToken').html()).select();
        document.execCommand("copy");
        $temp.remove();
        $('#errorDiv').html('Token copied to clipboard.');
    }

</script>
<%--</asp:Content>--%>