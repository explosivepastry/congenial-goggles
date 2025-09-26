<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Account>" %>

<%if (Model != null)
{%>
<div class="col-12">
    <div class="deviceView_btn_row shadow-sm rounded mb-4"style="margin-top:50px; max-width:479px">
        <div class="col-12">
            <div class="input-group my-auto " style="max-width:500px;">
                <input id="KeyActivation" placeholder="<%: Html.TranslateTag("e.g. xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")%>" class="form-control user-dets" type="text" />
                <a href="/Retail/ActivateCredits/<%:Model.AccountID %>?activationCode=" id="activate" class="btn btn-primary rounded-end" onclick="activateKey(this); return false;"><%: Html.TranslateTag("Retail/NotificationCredit|Activate Code")%></a> <span id="codeSubmitError"></span>
                <button class="btn btn-primary" id="saving" style="display: none;" type="button" disabled>
                    <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                    Activating...
                </button>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">

    function activateKey(a) {
        $('#activate').hide();
        $('#saving').show();
        if ($('#KeyActivation').val().length === 0) {
            $('#KeyActivation').attr("placeholder", "Code Required");
            toastBuilder("Code required");
            $('#activate').show();
            $('#saving').hide();
            return;
        }
        var url = $(a).attr("href") + $('#KeyActivation').val() + '&creditClassification=<%:ViewBag.CreditClassification == null ? 1 : ViewBag.CreditClassification%>';
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
                alert(data.split('_')[0]);
                $('#activate').show();
                $('#saving').hide();
            }
        });
    }
</script>
<%} %>