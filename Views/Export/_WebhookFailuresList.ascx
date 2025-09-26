<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Monnit.WebHookFailures>>" %>

<% string urlsort = HttpUtility.ParseQueryString(Request.Url.Query).Get("sort");%>

<div class="col-12">

    <table class="table table-hover">
        <thead>
            <tr>
                <th scope="col"><%: Html.TranslateTag("Status","Status")%></th>
                <th scope="col"><%: Html.TranslateTag("URL","URL")%></th>
                <th scope="col"><%: Html.TranslateTag("Date","Date")%></th>
                <th scope="col"><%: Html.TranslateTag("Retry","Retry")%></th>
            </tr>
        </thead>
        <tbody>
            <%if (Model.Count < 1)
                {%>

            <tr class="col-12">
                <td style="font-size: 1.1em; align-content: center;"><%: Html.TranslateTag("Export/HistoryList|No attempts found... New webhooks may take up to 15 minutes to take effect.", "No attempts found... New webhooks may take up to 15 minutes to take effect.")%></td>
            </tr>

            <%  }
                else
                {
                    //string filterURL = Request.Url.Host + ":" + Request.Url.Port + "/Overview/SensorIndex?id=" + networkID + "&name=" + urlname + "&profile=" + urlprofile + "&status=" + urlstatus + "&sort=" + urlsort;
            %>

            <%
                foreach (WebHookFailures item in Model)
                {%>
            <tr>
                <td>
                    <a href="/Export/WebhookAttempt/<%=item.AttemptID %>" style="cursor: pointer;">
                        <div class="col-2" style="padding-left: 5px;">
                            <img src="<%=Html.GetThemedContent("/images/Alarm.png") %>" alt="Failed" title="Failed" <%=item.Status == eExternalDataSubscriptionStatus.Failed ? "" : "style='display:none;'"%> />
                            <img src="<%=Html.GetThemedContent("/images/recycle.png") %>" alt="Proccessing" title="Proccessing" <%=item.Status == eExternalDataSubscriptionStatus.Processing ? "" : "style='display:none;'"%> />
                            <img src="<%=Html.GetThemedContent("/images/good.png") %>" alt="Success" title="Success" <%=item.Status == eExternalDataSubscriptionStatus.Success ? "" : "style='display:none;'"%> />
                            <img src="<%=Html.GetThemedContent("/images/alert.png") %>" alt="Retry" title="Retry" <%=item.Status == eExternalDataSubscriptionStatus.Retry ? "" : "style='display:none;'"%> />
                            <img src="<%=Html.GetThemedContent("/images/add.png") %>" alt="New" title="New" <%=item.Status == eExternalDataSubscriptionStatus.New ? "" : "style='display:none;'"%> />
                            <img src="<%=Html.GetThemedContent("/images/inactive.png") %>" alt="Noretry" title="Noretry" <%=item.Status == eExternalDataSubscriptionStatus.NoRetry ? "" : "style='display:none;'"%> />
                        </div>
                    </a>
                </td>
                <td>
                    <a href="/Export/WebhookAttempt/<%=item.AttemptID %>" style="cursor: pointer;">
                        <div class="col-6" style="overflow: auto;">
                            <%: item.Url %>
                        </div>
                    </a>
                </td>
                <td>
                    <a href="/Export/WebhookAttempt/<%=item.AttemptID %>" style="cursor: pointer;">
                        <div class="col-2">
                            <%: item.CreateDate.OVToLocalDateTimeShort() %>
                        </div>
                    </a>
                </td>
                <td>
                    <div class="col-md-2 hidden-xs" id="retry_<%=item.AttemptID %>">

                        <%if (item.Status == eExternalDataSubscriptionStatus.Failed || item.Status == eExternalDataSubscriptionStatus.Processing)
                            { %>
                        <input onclick="retryAttempt(<%=item.AttemptID %>);" class="btn btn-primary" type="button" id="retryButton" value="Retry" />

                        <%}
                            else if (item.Status == eExternalDataSubscriptionStatus.Retry)
                            {%>
                            Queued

                        <%  }
                            else
                            {%>
                        N/A
                        <% } %>
                    </div>
                </td>
            </tr>
            <% }
                } %>
        </tbody>
    </table>
    <div class="text-center w-100" style="display: none" id="webhooksLoading">
        <div style="display: flex; width: 100%; justify-content: center;">
            <div class="spinner-border text-primary d-flex" role="status">
                <span class="visually-hidden"><%= Html.TranslateTag("Loading") %>...</span>
            </div>
        </div>
    </div>
</div>


<script>


    function retryAttempt(attemptID) {

        $.post('/Export/RetryAttempt', { IDStringList: attemptID }, function (data) {
            if (data == "Success") {
                $('#retry_' + attemptID).html("Queued");
            } else {
                console.log(data);
                let values = {};
                    <%--values.redirect = '/Ack/<%:Model.NotificationRecordedID%>/<%:Model.NotificationGUID%>';--%>
                values.text = "<%=Html.TranslateTag("Oops! That did not work, please refresh your page. If this error continues, contact support.")%>";
                openConfirm(values);
                $('#modalCancel').hide();
            }
        });

    }

</script>
