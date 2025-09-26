<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.ExternalDataSubscription>" %>

<div class="container-fluid">
    <div class="col-12">
        <div class="rule-card_container w-100">
            <div class="card_container__top accordion-item webhook-card">
                <div class="card_container__top__title d-flex justify-content-between accordion-header d-flex" style="border-bottom:none;">
                 <div style="margin-left:10px;"><%:Html.TranslateTag("Export/NotificationWebhook|Rule Webhook Information","Rule Webhook Information")%></div>
                    <div><button class="accordion-button <%:Model.ExternalDataSubscriptionID > 0 ? "collapsed" : "" %>" type="button" data-bs-toggle="collapse" data-bs-target="#info-toggle" aria-expanded="false" style="background-color:transparent!important; " /></div>
                </div>

                <div class="clearfix"></div>
            </div>

            <div id="info-toggle" class="col-12 accordion-collapse collapse <%:Model.ExternalDataSubscriptionID < 0 ? "show" : "" %>">
                <div class="accordion-body" style="max-height: 55vh;">
                    <div class="x_content">
                        <div class="col-xs-12">
                            <h2><%:Html.TranslateTag("Export/NotificationWebhook|Overview","Overview")%></h2>
                           
                            <span style="font-size: 1.1em; align-content: center;"><%: Html.TranslateTag("Export/NotificationWebhook|A Rule Webhook sends data to your end point when an Rule condition causes the system action to execute. You can configure the destination and parameters used to route the request. Data is compiled as a JSON body and sent via HTTP POST. There is only one (1) data push allowed per account. To switch to a different data push type, first stop your existing data push, only then will the Create button appear.","A Rule Webhook sends data to your end point when an Rule condition causes the system action to execute. You can configure the destination and parameters used to route the request. Data is compiled as a JSON body and sent via HTTP POST. There is only one (1) data push allowed per account. To switch to a different data push type, first stop your existing data push, only then will the Create button appear.")%></span>
                            <hr />
                        </div>

                        <div class="col-xs-12">
                            <h2><%: Html.TranslateTag("Export/NotificationWebhook|Attempts & Retries","Attempts & Retries")%></h2>
                            <span style="font-size: 1.1em; align-content: center;"><%: Html.TranslateTag("Export/NotificationWebhook|Each individual message is attempted up to 4 times unless the total consecutive failure count for all attempts hits 20.  After 20 consecutive failures each message is only attempted 1 time up to 100 total consecutive failures.  After 100 consecutive failures the webhook is suspended until it is manualy reset. First attempt queued immediately (typically delivered within a few seconds) Second attempt queued after 2 minutes Third attempt queued after 15 minutes Fourth attempt queued after 60 minutes Subsequent attempts only sent after manually being manually re-queued. There is a second limit of 10 total attempts that can be sent. After 10 failed attempts of a particular message (4 auto attempts + 6 manual resend commands) you will need to contact support to release that message.","Each individual message is attempted up to 4 times unless the total consecutive failure count for all attempts hits 20.  After 20 consecutive failures each message is only attempted 1 time up to 100 total consecutive failures.  After 100 consecutive failures the webhook is suspended until it is manualy reset. First attempt queued immediately (typically delivered within a few seconds) Second attempt queued after 2 minutes Third attempt queued after 15 minutes Fourth attempt queued after 60 minutes Subsequent attempts only sent after manually being manually re-queued. There is a second limit of 10 total attempts that can be sent. After 10 failed attempts of a particular message (4 auto attempts + 6 manual resend commands) you will need to contact support to release that message.")%></span>
                            <hr />
                        </div>

                        <div style="clear: both;"></div>
                        <div class="col-12 col-md-6">
                            <h2><%: Html.TranslateTag("Export/NotificationWebhook|Rule Webhook Contents","Rule Webhook Contents")%></h2>

                            <div class="form-group row">
                                <div class="bold col-sm-3 col-12">
                                   Subject
                                </div>

                                <div class="col-sm-9 col-12 lgBox">
                                   <%: Html.TranslateTag("Export/NotificationWebhook|Subject or title of the rule.","Subject or title of the rule.")%>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="bold col-sm-3 col-12">
                                    Reading
                                </div>

                                <div class="col-sm-9 col-12 lgBox">
                                    <%: Html.TranslateTag("Export/NotificationWebhook|Reading or event that triggered the rule.","Reading or event that triggered the rule.")%>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="bold col-sm-3 col-12">
                                    Rule
                                </div>

                                <div class="col-sm-9 col-12 lgBox">
                                    <%: Html.TranslateTag("Export/NotificationWebhook|The name of the rule.","The name of the rule.")%>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="bold col-sm-3 col-12">
                                    Date
                                </div>

                                <div class="col-sm-9 col-12 lgBox">
                                    <%: Html.TranslateTag("Export/NotificationWebhook|Date the rule triggered.","Date the rule triggered.")%>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="bold col-sm-3 col-12">
                                    Time
                                </div>

                                <div class="col-sm-9 col-12 lgBox">
                                    <%: Html.TranslateTag("Export/NotificationWebhook|Time the rule triggered.","Time the rule triggered.")%>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="bold col-sm-3 col-12">
                                    ReadingDate
                                </div>
                                <div class="col-sm-9 col-12 lgBox">
                                    <%: Html.TranslateTag("Export/NotificationWebhook|Date of data message (if rule was triggered by sensor reading).","Date of data message (if rule was triggered by sensor reading).")%>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="bold col-sm-3 col-12">
                                   ReadingTime
                                </div>

                                <div class="col-sm-9 col-12 lgBox">
                                    <%: Html.TranslateTag("Export/NotificationWebhook|Time of data message (if rule was triggered by sensor reading).","Time of data message (if rule was triggered by sensor reading).")%>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="bold col-sm-3 col-12">
                                    OriginalReadingDate
                                </div>

                                <div class="col-sm-9 col-12 lgBox">
                                    <%: Html.TranslateTag("Export/NotificationWebhook|Date of the first data message that triggered the rule (if rule was triggered by sensor reading).","Date of the first data message that triggered the rule (if rule was triggered by sensor reading).")%>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="bold col-sm-3 col-12">
                                    OriginalReadingTime
                                </div>

                                <div class="col-sm-9 col-12 lgBox">
                                    <%: Html.TranslateTag("Export/NotificationWebhook|Time of the first data message that triggered the rule (if rule was triggered by sensor reading).","Time of the first data message that triggered the rule (if rule was triggered by sensor reading).")%>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="bold col-sm-3 col-12">
                                    OriginalReading
                                </div>

                                <div class="col-sm-9 col-12 lgBox">
                                    <%: Html.TranslateTag("Export/NotificationWebhook|Reading of the first data message that triggered the rule (if rule was triggered by sensor reading).","Reading of the first data message that triggered the rule (if rule was triggered by sensor reading).")%>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="bold col-sm-3 col-12">
                                    AcknowledgeURL
                                </div>

                                <div class="col-sm-9 col-12 lgBox">
                                    <%: Html.TranslateTag("Export/NotificationWebhook|URL that will acknowledge the rule if called (Log in required).","URL that will acknowledge the rule if called (Log in required).")%>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="bold col-sm-3 col-12">
                                    ParentAccount
                                </div>

                                <div class="col-sm-9 col-12 lgBox">
                                    <%: Html.TranslateTag("Export/NotificationWebhook|Name of the reseller or immediate parent in corporate herirarchy.","Name of the reseller or immediate parent in corporate herirarchy.")%>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="bold col-sm-3 col-12">
                                    DeviceID
                                </div>

                                <div class="col-sm-9 col-12 lgBox">
                                    <%: Html.TranslateTag("Export/NotificationWebhook|The ID of the device that triggerd the rule.","The ID of the device that triggerd the rule.")%>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="bold col-sm-3 col-12">
                                   Name
                                </div>
                                <div class="col-sm-9 col-12 lgBox">
                                    <%: Html.TranslateTag("Export/NotificationWebhook|Name of the device that triggerd the rule.","Name of the device that triggerd the rule.")%>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="bold col-sm-3 col-12">
                                    NetworkID
                                </div>

                                <div class="col-sm-9 col-12 lgBox">
                                    <%: Html.TranslateTag("Export/NotificationWebhook|The ID of the network that the device triggering the rule belongs to.","The ID of the network that the device triggering the rule belongs to.")%>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="bold col-sm-3 col-12">
                                    Network
                                </div>

                                <div class="col-sm-9 col-12 lgBox">
                                    <%: Html.TranslateTag("Export/NotificationWebhook|Network that the device triggering the rule belongs to.","Network that the device triggering the rule belongs to.")%>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="bold col-sm-3 col-12">
                                    AccountID
                                </div>

                                <div class="col-sm-9 col-12 lgBox">
                                    <%: Html.TranslateTag("Export/NotificationWebhook|The ID of the account the rule belongs to.","The ID of the account the rule belongs to.")%>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="bold col-sm-3 col-12">
                                    AccountNumber
                                </div>

                                <div class="col-sm-9 col-12 lgBox">
                                    <%: Html.TranslateTag("Export/NotificationWebhook|Account Number of the account the rule belongs to.","Account Number of the account the rule belongs to.")%>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="bold col-sm-3 col-12">
                                    CompanyName
                                </div>

                                <div class="col-sm-9 col-12 lgBox">
                                    <%: Html.TranslateTag("Export/NotificationWebhook|Company Name of the account the rule belongs to.","Company Name of the account the rule belongs to.")%>
                                </div>
                            </div>
                        </div>

                        <div class="col-12 col-md-6">
                        <h2><%: Html.TranslateTag("Export/NotificationWebhook|Alert Example JSON","Alert Example JSON")%></h2>

  <pre style="border: solid 1px black; background-color: #DDEEFF; padding: 5px; overflow: auto;"> 
      {
    "subject": "Battery below 50%",
    "reading": "Battery: 10%",
    "rule": "Battery below 50%",
    "date": "2022-4-28",
    "time": "14:21",
    "readingDate": "2022-4-28",
    "readingTime": "14:21",
    "originalReadingDate": "2022-4-28",
    "originalReadingTime": "14:21",
    "originalReading": "Battery: 10%",
    "acknowledgeURL": "https://staging.imonnit.com/Ack/1234",
    "parentAccount": "",
    "deviceID": "56789",
    "name": "IOT Gateway - 56789",
    "networkID": "4567",
    "network": "Test Network",
    "accountID": "123456",
    "accountNumber": "Example Company",
    "companyName": "Example Company"
}
 </pre>
</div>
                <div style="clear: both;"></div>
            </div>

            <div style="clear: both;"></div>
        </div>

                <div class="form-group row">
                    <div class="bold col-md-2 col-sm-2 col-12">
                    </div>

                    <div class="col-md-6 col-sm-6 col-12 lgBox">
                        <div class="editor-error">
                        </div>
                    </div>

                    <div style="clear: both;"></div>
                </div>
            </div>
        </div>
    </div>
</div>