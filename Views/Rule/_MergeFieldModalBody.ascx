<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="modal-content">
    <div class="modal-header">
        <h5 class="modal-title" id="modal"><%: Html.TranslateTag("Rule/SendNotification|Merge Fields")%></h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
    </div>
    <div class="modal-body">

        <div class="row">
            <div class="col- word-choice">
                {ID} 
            </div>
            <div class="col word-def">
                <%: Html.TranslateTag("Rule/SendNotification|Unique ID of the sensor or gateway that triggered the notification", "Unique ID of the sensor or gateway that triggered the notification")%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col- word-choice">
                {Name} 
            </div>
            <div class="col word-def">
                <%: Html.TranslateTag("Rule/SendNotification|Name of the sensor or gateway that triggered the notification", "Name of the sensor or gateway that triggered the notification")%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col- word-choice">
                {Reading} 
            </div>
            <div class="col word-def">
                <%: Html.TranslateTag("Rule/SendNotification|Current Reading of the notification", "Current Reading of the notification")%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col- word-choice">
                {Network} 
            </div>
            <div class="col word-def">
                <%: Html.TranslateTag("Rule/SendNotification|Name of the network the sensor or gateway belongs to", "Name of the network the sensor or gateway belongs to")%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col- word-choice">
                {Notification} 
            </div>
            <div class="col word-def">
                <%: Html.TranslateTag("Rule/SendNotification|Name of the Notification", "Name of the Notification")%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col- word-choice">
                {Subject} 
            </div>
            <div class="col word-def">
                <%: Html.TranslateTag("Rule/SendNotification|Subject of the Notification", "Subject of the Notification")%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col- word-choice">
                {AccountNumber} 
            </div>
            <div class="col word-def">
                <%: Html.TranslateTag("Rule/SendNotification|Account number of the account the notification belongs to", "Account number of the account the notification belongs to")%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col- word-choice">
                {CompanyName} 
            </div>
            <div class="col word-def">
                <%: Html.TranslateTag("Rule/SendNotification|Company name of the account the notification belongs to", "Company name of the account the notification belongs to")%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col- word-choice">
                {Address} 
            </div>
            <div class="col word-def">
                <%: Html.TranslateTag("Rule/SendNotification|Address of the account where the sensor should", "Address of the account where the sensor should")%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col- word-choice">
                {Address2} 
            </div>
            <div class="col word-def">
                <%: Html.TranslateTag("Rule/SendNotification|Suite, apartment, ect number of the accounts address", "Suite, apartment, ect number of the accounts address")%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col- word-choice">
                {City} 
            </div>
            <div class="col word-def">
                <%: Html.TranslateTag("Rule/SendNotification|City of where the account is located", "City of where the account is located")%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col- word-choice">
                {State} 
            </div>
            <div class="col word-def">
                <%: Html.TranslateTag("Rule/SendNotification|State of where the account is located", "State of where the account is located")%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col- word-choice">
                {PostalCode} 
            </div>
            <div class="col word-def">
                <%: Html.TranslateTag("Rule/SendNotification|PostalCode of where the account is located", "PostalCode of where the account is located")%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col- word-choice">
                {Country} 
            </div>
            <div class="col word-def">
                <%: Html.TranslateTag("Rule/SendNotification|Country of where the account is located", "Country of where the account is located")%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col- word-choice">
                {Date} 
            </div>
            <div class="col word-def">
                <%: Html.TranslateTag("Rule/SendNotification|Date the Notification was sent", "Date the Notification was sent")%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col- word-choice">
                {Time} 
            </div>
            <div class="col word-def">
                <%: Html.TranslateTag("Rule/SendNotification|Time the Notification was sent", "Time the Notification was sent")%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col- word-choice">
                {ReadingDate} 
            </div>
            <div class="col word-def">
                <%: Html.TranslateTag("Rule/SendNotification|Current Notification trigger Date", "Current Notification trigger Date")%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col- word-choice">
                {ReadingTime} 
            </div>
            <div class="col word-def">
                <%: Html.TranslateTag("Rule/SendNotification|Current Notification trigger Time", "Current Notification trigger Time")%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col- word-choice">
                {OriginalReadingDate} 
            </div>
            <div class="col word-def">
                <%: Html.TranslateTag("Rule/SendNotification|Original Notification triggering Date", "Original Notification triggering Date")%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col- word-choice">
                {OriginalReadingTime} 
            </div>
            <div class="col word-def">
                <%: Html.TranslateTag("Rule/SendNotification|Original Notification triggering Time", "Original Notification triggering Time")%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col- word-choice">
                {OriginalReading} 
            </div>
            <div class="col word-def">
                <%: Html.TranslateTag("Rule/SendNotification|Original Reading that triggered the notification", "Original Reading that triggered the notification")%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col- word-choice">
                {Acknowledge} 
            </div>
            <div class="col word-def">
                <%: Html.TranslateTag("Rule/SendNotification|Link to Acknowledge a Notification (Email Only)", "Link to Acknowledge a Notification (Email Only)")%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col- word-choice">
                {AcknowledgeURL}
            </div>
            <div class="col word-def">
                <%: Html.TranslateTag("Rule/SendNotification|Link to Acknowledge a Notification (SMS or Email)", "Link to Acknowledge a Notification (SMS or Email)")%>
            </div>
        </div>

    </div>
    <div class="modal-footer">
        <button type="button" class="btn btn-default" data-bs-dismiss="modal"><%: Html.TranslateTag("Close", "Close")%></button>
    </div>

</div>
