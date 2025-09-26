<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CustomerContactInfoModel>" %>

    
<% using (Html.BeginForm()) {%>
    
        <div class="formtitle">Edit User Information</div>
        <div class="formBody">
            <%: Html.ValidationSummary(true) %>
        
              <%: Html.Hidden("AccountID",Model.Customer.AccountID) %>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Customer.UserName) %>
            </div>
            <div class="editor-field">
            <% if (MonnitSession.CustomerCan("Customer_Change_Username")) { %>
                <input id="UserName" name="UserName" type="text" value="<%=Model.Customer.UserName %>">
            <%} else {%>
              <%= Model.Customer.UserName %>
            <%} %>
            </div>
            <div class="editor-error">
                <%= Html.ValidationMessageFor(model => model.Customer.UserName)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Customer.Password) %>
            </div>
            <div class="editor-field">
            <% if (Model.Customer.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
               { %>
                <a href="/Account/ChangePassword">Click here to reset password</a>
            <%} %>
            <% if (MonnitSession.CustomerCan("Customer_Reset_Password_Other")) { %>
                <a href="#" onclick="defaultPassword('<%:Model.Customer.CustomerID  %>'); return false;">Reset default password</a>
            <%} %>
            </div>
            
            <div class="editor-label">
               <%: Html.LabelFor(model => model.Customer.FirstName) %>
            </div>
            <div class="editor-field">
               
                <input id="FirstName" name="FirstName" type="text" value="<%=Model.Customer.FirstName%>">
            </div>
            <div class="editor-error">
             <%: Html.ValidationMessageFor(model => model.Customer.FirstName) %>
            </div>
            
            <div class="editor-label">
             <%: Html.LabelFor(model => model.Customer.LastName) %>
            </div>
            <div class="editor-field">
                <input id="LastName" name="LastName" type="text" value="<%=Model.Customer.LastName%>">
            </div>
            <div class="editor-error">
              <%: Html.ValidationMessageFor(model => model.Customer.LastName) %>
            </div>
            
            
       <%if(MonnitSession.CurrentCustomer.IsAdmin)
          { %>
             <div class="editor-label">
                  <%: Html.LabelFor(model => model.Customer.IsAdmin) %>
            </div>
            <div class="editor-field">
                <input type="checkbox" name="IsAdmin" <%:Model.Customer.IsAdmin  ? "checked='checked'" : ""%> />
            </div>
        <%} %>

        </div>
        <div class="buttons">
            <input type="button" onclick="postForm($(this).closest('form'), function (data) { if (data == 'Success') { $('.refreshPic:visible').click(); } });" value="Save" class="bluebutton" />
            <div style="clear:both;"></div>
        </div>
<% } %>

<script type="text/javascript">

    function defaultPassword(custID) {
        $.get('/Customer/DefaultPassword/' + custID, function (data) {
            eval(data);
        });
    }
</script>