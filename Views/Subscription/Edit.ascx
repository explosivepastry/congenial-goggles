<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.AccountSubscriptionChangeLog>" %>
<% 
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();
%>
<script type="text/javascript">
    $(document).ready(function() {
        $(".datepicker").datepicker();
        $('#ui-datepicker-div').css('display', 'none');
    });
</script>
    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        <fieldset>
            <legend>Update Subscription</legend>
            <div class="editor-label">
                Expiration
            </div>
            <div class="editor-field">
                <%: Html.Label(Model.OldExpirationDate.ToShortDateString()) %>
            </div>
            <div class="editor-label">
                Reason for Changing
            </div>
            <div class="editor-field">
                <select id="ChangeType" name="ChangeType">
                    <option value="New Sensor Purchase" <% if(Model.ChangeType == "New Sensor Purchase") Response.Write("selected='selected'"); %>>New Sensor Purchase</option>
                    <option value="Monitoring Fee" <% if(Model.ChangeType == "Monitoring Fee") Response.Write("selected='selected'"); %>>Monitoring Fee</option>
                    <option value="Credit Given" <% if(Model.ChangeType == "Credit Given") Response.Write("selected='selected'"); %>>Credit Given</option>
                    <option value="Other" <% if(Model.ChangeType == "Other") Response.Write("selected='selected'"); %>>Other</option>
                </select>
            </div>
            <div class="editor-label">
                Note
            </div>
            <div class="editor-field">
                <%: Html.TextAreaFor(model => model.ChangeNote)%>
            </div>
            <div class="editor-label">
                New Expiration Date
            </div>
            <div class="editor-field">
                <%: Html.TextBox("NewExpirationDate", Model.NewExpirationDate.ToShortDateString(), new { @class = "datepicker" })%>
            </div>
            
        </fieldset>
        <p>
            <a href="#" onclick="hideModal(); return false;">Back</a>
            <input type="button" onclick="postModal();" value="Save" />
        </p>
        
    <% } %>
