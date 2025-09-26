<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Customer>" %>

<% using (Html.BeginForm()) {%>
    <div id="fullForm" style="width: 100%;">
        <div class="formtitle">Are you sure you want to delete this User?</div>
        <div class="formBody">
            <input type="hidden" id="AccountID" name="AccountID" value="<%:Convert.ToInt64(ViewBag.AccountID)%>" />
            <%: Html.ValidationSummary(true) %>
        
            <div class="display-label"><%: Html.LabelFor(model => model.UserName)%></div>
            <div class="display-field"><%: Model.UserName %></div>
        
            <div class="display-label"><%: Html.LabelFor(model => model.FirstName)%></div>
            <div class="display-field"><%: Model.FirstName %></div>
        
            <div class="display-label"><%: Html.LabelFor(model => model.LastName)%></div>
            <div class="display-field"><%: Model.LastName %></div>
        
            <div class="display-label"><%: Html.LabelFor(model => model.NotificationEmail)%></div>
            <div class="display-field"><%: Model.NotificationEmail %></div>
        
            <div class="editor-label">
                    User that will receive any notifications currently being sent to this user 
                    <%if (MonnitSession.CustomerCan("Customer_Create") && (Request.UrlReferrer != null && !Request.UrlReferrer.LocalPath.Contains("Account/Overview"))) { %>
                        <a href="/Customer/Create/<%:MonnitSession.CurrentCustomer.AccountID %>" onclick="newCustomer(this); return false;">Add New</a>
                    <%} %>
            </div>
            <div class="editor-field">
                <%: Html.TextBox("CustomerIDLookup", MonnitSession.CurrentCustomer.UniqueName)%>
                <%: Html.Hidden("CustomerToNotifyID", MonnitSession.CurrentCustomer.CustomerID)%>
            </div>
        
    
        </div>
        <div class="buttons">
            <a href="#" onclick="hideModal(); return false;" class="greybutton">Cancel</a>
            <input type="button" onclick="postModal();" value="Save" class="bluebutton"/>
            <div style="clear:both;"></div>
        </div>
    </div>
<% } %>

    <script type="text/javascript">
    $(document).ready(function() {
        createLookup('CustomerIDLookup', '/Lookup/AccountCustomer?accountID=<%: Model.AccountID %>', null, 'CustomerToNotifyID', '/Lookup/AccountCustomerID?accountID=<%: Model.AccountID %>', null);
    });
    </script>