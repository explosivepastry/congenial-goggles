<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<PurchaseConfirmationModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    PurchaseConfirm
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <%MvcHtmlString creditPreviewHtml = null;
        switch (Model.PurchaseType)
        {
            case "nc":
                Model.ConfirmationMessage = "Your notification credits have been added to your account!";
                Model.PurchaseType = Html.TranslateTag("Purchase","Purchase");
                Model.PurchaseProduct = Html.TranslateTag("Notification Credits","Notification Credits");
                creditPreviewHtml = Html.Partial("_ThankYouNotificationCredits", Model);
                break;
            case "hx":
                Model.ConfirmationMessage = "Your HX message credits have been added to your account!";
                Model.PurchaseType = Html.TranslateTag("Purchase","Purchase");
                Model.PurchaseProduct = Html.TranslateTag("Retail/MessageCredit|HX Credits", "HX Credits");
                creditPreviewHtml = Html.Partial("_ThankYouMessageCredits", Model);
                break;
            case "sp":
                Model.ConfirmationMessage = "Your SensorPrint credits have been added to your account!";
                Model.PurchaseType = Html.TranslateTag("Purchase","Purchase");
                Model.PurchaseProduct = Html.TranslateTag("Retail/NotificationCredit|SensorPrint Credits", "SensorPrint Credits");
                creditPreviewHtml = Html.Partial("_ThankYouSensorPrints", Model);
                break;
            case "gw":
                Model.ConfirmationMessage = "Your GatewayUnlock credits have been added to your account!";
                Model.PurchaseType = Html.TranslateTag("Purchase","Purchase");
                Model.PurchaseProduct = Html.TranslateTag("Retail/NotificationCredit|GatewayUnlock Credits", "GatewayUnlock Credits");
                creditPreviewHtml = Html.Partial("_ThankYouGatewayUnlock", Model);
                break;
            case "gps":
                Model.ConfirmationMessage = "Your GatewayUnlockGps credits have been added to your account!";
                Model.PurchaseType = Html.TranslateTag("Purchase","Purchase");
                Model.PurchaseProduct = Html.TranslateTag("Retail/NotificationCredit|GatewayUnlockGps Credits", "GatewayUnlockGps Credits");
                creditPreviewHtml = Html.Partial("_ThankYouGatewayUnlockGps", Model);
                break;
            case "ip":
                Model.ConfirmationMessage = "Your Premiere subscription is updated!";
                Model.PurchaseType = Html.TranslateTag("Subscription","Subscription");
                if (string.IsNullOrEmpty(Model.PurchaseProduct)) { Model.PurchaseProduct = "Premiere"; }
                creditPreviewHtml = Html.Partial("_ThankYouPremiereList", Model);

                long itemID = long.Parse(Model.PurchasedItemIDs);
                AccountSubscription accountSubscription = AccountSubscription.Load(itemID);
                if (accountSubscription != null) { Model.PurchaseExpiration = accountSubscription.ExpirationDate; }
                break;
            default:
                Model.ConfirmationMessage = "Your order is confirmed!";
                Model.PurchaseType = "";
                creditPreviewHtml = null;
                break;
        }
    %>
    
    <div>
        <button type="button" class="btn btn-primary" id="purchaseConfirmPrintBtn">Print this form</button>
    </div>
    <div id="purchaseConfirmPrintDiv" class="container-fluid mt-4" style="display: flex; flex-direction: column;">
        <div class="col-12">
            <div class="x_panel gridPanel">
                <div class="x_title">
                    <div class="jumbotron text-center" style="background-color: #ffffff;">
                        <h1 class="display-3"><%: Html.TranslateTag("Retail/PurchaseConfirm|Thank You!")%></h1>
                        <p class="lead"><strong><%: Model.ConfirmationMessage%></strong></p>
                        <%if (!string.IsNullOrEmpty(Model.CardMask)){ %>
                        <p class="lead"><%: Html.TranslateTag("Retail/PurchaseConfirm|An email with your transaction's receipt will be sent within 1 business day.")%></p>
                        <%} %>
                        <hr>
                        <div class="row">
                            <div class="row h4">
                                <div class="col-5" style="text-align:right; font-weight:bold;"><%: Html.TranslateTag("Account","Account")%>:</div>
                                <div class="col-7" style="text-align:left;"><%=Model.Account.CompanyName%></div>
                            </div>
                            <div class="row h4">
                                <div class="col-5" style="text-align:right; font-weight:bold;"><%: Html.TranslateTag("User","User")%>:</div>
                                <div class="col-7" style="text-align:left;"> <%=MonnitSession.CurrentCustomer.FirstName%> <%=MonnitSession.CurrentCustomer.LastName%></div>
                            </div>
                            <div class="row h4">
                                <div class="col-5" style="text-align:right; font-weight:bold;"><%: Model.PurchaseType%>:</div>
                                <div class="col-7" style="text-align:left;"> <%: Model.PurchaseProduct %></div>
                            </div>
                            <%if (Model.PurchaseExpiration > DateTime.UtcNow){ %>
                            <div class="row h4">
                                <div class="col-5" style="text-align:right; font-weight:bold;"><%: Html.TranslateTag("Expiration", "Expiration") %>:</div>
                                <div class="col-7" style="text-align:left;"> <%: Monnit.TimeZone.GetLocalTimeById(Model.PurchaseExpiration, Model.Account.TimeZoneID).ToShortDateString() %></div>
                            </div>
                            <%} %>
                            <%if (!string.IsNullOrEmpty(Model.CardMask)){ %>
                            <div class="row h4">
                                <div class="col-5" style="text-align:right; font-weight:bold;"><%: Html.TranslateTag("Card Number", "Card Number")%>:</div>
                                <div class="col-7" style="text-align:left;"> <%:Model.CardMask%></div>
                            </div>
                            <%} %>
                            <%if (Model.PurchasePrice > 0){ %>
                            <div class="row h4">
                                <div class="col-5" style="text-align:right; font-weight:bold;"><%: Html.TranslateTag("Amount", "Amount")%>:</div>
                                <div class="col-7" style="text-align:left;"> <%:Model.PurchasePrice.ToString("C")%></div>
                            </div>
                            <%} %>
                            <%if (!string.IsNullOrEmpty(Model.ActivationKey)){ %>
                            <div class="row h4">
                                <div class="col-5" style="text-align:right; font-weight:bold;"><%: Html.TranslateTag("Activation Key", "Activation Key")%>:</div>
                                <div class="col-7" style="text-align:left;"> <%=Html.Raw(Model.ActivationKey.Replace("~", "<br/>"))%></div>
                            </div>
                            <%} %>
                        </div>
                        <hr>
                        <%if (!string.IsNullOrWhiteSpace(Model.PurchaseType)) {%>
                            <div class="row">
                                <div class="col-12">
                                    <div class="x_panel gridPanel shadow-sm rounded">
                                        <div class="card_container__top">
                                            <div class="card_container__top__title" style="margin-top: 5px; overflow: unset;">
                                                <span>
                                                    <%:Model.PurchaseType%>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="x_content">
                                            <%if (creditPreviewHtml != null) {%>
                                                <%:creditPreviewHtml%>
                                            <%}%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        <%}%>
                        <div class="row">
                            <p class="lead" style="text-align: -webkit-center;">
                                <a style="width: fit-content;" class="btn btn-primary" href="/Overview" role="button"><%: Html.TranslateTag("Retail/PurchaseConfirm|Home Page")%></a>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        $(function () {
            $('#purchaseConfirmPrintBtn').click(function () {
                var htmlToPrint = $('#purchaseConfirmPrintDiv').html();
                var printWindow = window.open('', 'PRINT', 'height=600,width=900');
                printWindow.document.write('<html><head><title>Confirmation</title><style>body {font-family: Arial;}</style>');
                printWindow.document.write('<style> div.col-6 {display:inline;}</style>');
                printWindow.document.write('</head><body>' + htmlToPrint + '</body></html>');
                printWindow.document.close();
                printWindow.focus();
                printWindow.print();
                printWindow.close();
            });
        });
    </script>
</asp:Content>