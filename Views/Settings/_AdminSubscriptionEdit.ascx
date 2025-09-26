<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AccountSubscription>" %>


<%  Account acc = Account.Load(Model.AccountID);
    DateTime newExpirationDate = Model.ExpirationDate; %>

<form method="post" action="/Settings/AdminSubscriptionDetails/<%=Model.AccountID %>" class="form-horizontal form-label-left">
    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>

    <div class="row">
        <div class="form-group row">
            <!-- <div class="hidden col-sm-1 col-md-1"></div>-->
            <div class="col-12 col-sm-4" style="margin-left: 10px;">
                <span style="font-weight: bold"><%: Html.TranslateTag("Settings/_AdminSubscriptionEdit|Current Subscription:","Current Subscription")%></span>
                <%=Model.SubscriptionName %>
            </div>
            <div class="col-12 col-sm-3" style="margin-left: 10px;">
                <span style="font-weight: bold"><%: Html.TranslateTag("Settings/_AdminSubscriptionEdit|Expiration Date:","Expiration Date:")%></span>

                <%=Model.ExpirationDate.ToShortDateString() %>
            </div>
            <div class="col-12 col-sm-3" style="margin-left: 10px;">
                <span style="font-weight: bold"><%: Html.TranslateTag("Settings/_AdminSubscriptionEdit|Sensor Count:","Sensor Count:")%></span>
                <%=Sensor.LoadByAccountID(Model.AccountID).Count %>
            </div>
        </div>
    </div>
    <hr />
    <div class="row">
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Settings/_AdminSubscriptionEdit|Type:","Type:")%>
            </div>
            <div class="col sensorEditFormInput">
                <select class="form-select" style="width: 250px;" name="accountsubscriptionTypeID" id="accountsubscriptionTypeID">
                    <%
                        List<AccountSubscriptionType> list = new List<AccountSubscriptionType>();

                        if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.IsCurrentCustomerMonnitSuperAdmin || MonnitSession.CurrentCustomer.CustomerID == 11850)
                        {
                            list = AccountSubscriptionType.LoadAll();
                        }
                        else if (MonnitSession.CustomerCan("Account_Set_Premium"))// && MonnitSession.IsCurrentCustomerReseller)
                        {
                            list = AccountSubscriptionType.LoadAllowedForReseller();
                        }

                        foreach (AccountSubscriptionType type in list)
                        { %>
                    <option value="<%=type.AccountSubscriptionTypeID %>" <%= type.AccountSubscriptionTypeID == Model.AccountSubscriptionTypeID ? "selected='selected'" : "" %>><%=type.Name %></option>
                    <%} %>
                </select>
            </div>
        </div>
    </div>

    <div class="row sensorEditForm" style="padding-top: 10px;">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Settings/_AdminSubscriptionEdit|Expiration Date:","Expiration Date:")%>
        </div>
        <div class="col sensorEditFormInput">
            <%
                if (newExpirationDate.Date.Subtract(DateTime.UtcNow.Date).Days >= 18250)// 50 years
                    newExpirationDate = DateTime.UtcNow.Date; %>
            <input class="form-control" type="text" id="ExpirationDate" name="ExpirationDate" value="<%=newExpirationDate.Date.ToShortDateString() %>" style="width: auto;"
                <%= acc.AutoBill < 0 || MonnitSession.IsCurrentCustomerMonnitAdmin ? "" : "disabled" %> />

            <a id="ExpirationDatePicker" class="ms-1" style="cursor: pointer;"><span style="font-size: 1.2em;" class="fa fa-list-ul"></span></a>
        </div>
    </div>



    <div class="d-flex justify-content-center">
        <button type="submit" value=" <%:Html.TranslateTag("Save","Save")%>" class="btn btn-primary">
            <%:Html.TranslateTag("Save","Save")%>
        </button>
    </div>
</form>

<style type="text/css">
    .validation-summary-errors ul {
        margin: 0;
        padding: 0;
        list-style: none;
        color: red;
    }

    .validation-summary-errors {
        display: flex;
        align-items: flex-end;
    }
</style>

<script type="text/javascript">

    var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';

    $(document).ready(function () {
        $('#ExpirationDatePicker').mobiscroll().datepicker({
            theme: 'ios',
            display: popLocation,
            defaultselection: new Date(<%=newExpirationDate.Year%>,<%=(newExpirationDate.Month - 1)%>,<%=newExpirationDate.Day%>),
            onChange: function (event, inst) {
                var Date = formatDate(inst.getVal());
                $('#ExpirationDate').val(Date);

            }
        });
    });

    function formatDate(date) {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();

        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;
        return [month, day, year].join('/');
    }

</script>
