<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.ExternalDataSubscription>" %>
<%
    DateTime utcFromDate = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryFromDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    DateTime utcToDate = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryToDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    //var historyList = ExternalDataSubscriptionAttempt.LoadBySubscriptionAndDate(Model.ExternalDataSubscriptionID, utcFromDate, utcToDate, 0, 100);
    string urlsort = HttpUtility.ParseQueryString(Request.Url.Query).Get("sort");

%>
<div class="container-fluid">
    <div class="col-12">
        <div class="rule-card_container w-100">
            <div class="card_container__top accordion-item webhook-card ">
                <div class="card_container__top__title d-flex justify-content-between accordion-header" style="border-bottom: none;">
                    <div style="margin-left: 10px;"><%:Html.TranslateTag("Webhook History","Webhook History")%></div>
                    <div>
                        <button class="accordion-button <%:Model.ExternalDataSubscriptionID < 0 ? "collapsed" : "" %>" type="button" data-bs-toggle="collapse" data-bs-target="#history-toggle" aria-expanded="false" style="background-color: transparent!important; border-bottom: none;" />
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
            <div id="history-toggle" class="col-12 accordion-collapse collapse <%:Model.ExternalDataSubscriptionID > 0 ? "show" : "" %>">
                <div class="col-12 d-flex align-items-center my-2">
                    <strong><%: Html.TranslateTag("Export/DataWebhook|Filter","Filter")%>: </strong>
                    <select id="sortFilter" class="form-select ms-4" style="width: 250px;">
                        <option value="Default"><%: Html.TranslateTag("Export/DataWebhook|Default","Default")%></option>
                        <option <%= urlsort == "Webhook Fails" ? "selected" : "" %> value="Webhook Fails" class="sortable"><%: Html.TranslateTag("Export/DataWebhook|Webhook Fails","Webhook Fails")%></option>
                    </select>
                </div>
                <div class="nav navbar-right panel_toolbox align-items-center mt-2">
                    <%Html.RenderPartial("MobiDateRange");%>
                </div>
                <div style="clear: both;"></div>

                <div class="accordion-body" style="max-height: 55vh;">
                    <div id="webhookHistoryList">
                        <%--<%=Html.Partial("_WebhookHistoryList", historyList)%>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">

    let mobiDataDestElem = '#webhookHistoryList';
    let mobiDataPayload = { id: <%=Model.ExternalDataSubscriptionID%> };
    let mobiDataController = '/Export/WebhookHistoryRefresh';

    function loadWebhooks() {
        //$('#webhooksLoading').show();
        //$('#rulesList').html("");
        //var query = $('#nameSearch').val();
        //var type = $('#typeFilter').val();
        //var status = $('#statusFilter').val();
        var sort = $('#sortFilter').val();
        $.post("/Export/WebhookHistoryRefresh", { id: <%=Model.ExternalDataSubscriptionID%>, sort: sort }, function (data) {
            $('#webhookHistoryList').html(data);
            $('#webhooksLoading').hide();
        });
    }


    $('#sortFilter').change(function (e) {
        e.preventDefault();
        loadWebhooks();
    });

</script>
