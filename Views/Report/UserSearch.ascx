<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<iMonnit.Models.UserLookUpModel>>" %>


<%if(Model != null){ %>    
<div style="width:610px; overflow:auto;">
    <table width="100%">
        <tr>
            <th>
                Active
            </th>
            <th>
                User Name
            </th>
            <th>
                Email
            </th>
            <th>
                Account
            </th>
           
            <th>
                
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
            <%
                bool active = item.IsActive;
                string FileName = "green.png";
                if (!active) FileName = "yellow.png";
            %>
                <img alt="Status" title='<%: active ? "active" : "inactive"%>' src="/Content/images/<%:FileName %>" />
            </td>
            <td>
                <%if (MonnitSession.CustomerCan("Proxy_Login")){%>
                <a href="/Account/ProxyCustomer/<%:item.CustomerID %>" onclick="proxyCustomer(this); return false;"><img src="../..<%:Html.GetThemedContent("/images/proxy.png")%>" alt="Proxy Login" title="Proxy Login" /></a>
                <%} %>
                <%: item.UserName %><br />
                <%: item.FirstName + " " + item.LastName%>
            </td>
           
            <td>
                <%: item.NotificationEmail %><br />
                <%: item.NotificationPhone %>
            </td>
            
            <td title="<%: item.Retail %>">
                <%= item.AccountNumber %><br />
                  <%: item.Domain %>
            </td>
          
            
        </tr>
    
    <% } %>

    </table>

</div>

    <div class="formtitle" style="margin:0px -10px;">Search</div>
 <% } %>


<% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        
        
            <div class="editor-label">
                Name or Username or Email or Phone
            </div>
            <div class="editor-field">
                <input type="text" id="id" name="id" value="<%: ViewData["query"] %>" /> 
            </div>
    
    <div style="clear:both;"></div> 
    <div class="buttons" style="margin: 10px -10px -10px -10px;">
        <input type="button" onclick="postMain();" value="Search" class="bluebutton" />
        <div style="clear:both;"></div>
    </div>
    <% } %>


<script type="text/javascript">
    $(document).ready(function () {

       

        $('form').submit(function (e) {
            e.preventDefault();
            postMain();
        });

        $("#id").focus().select();
        setTimeout("if(document.activeElement != $('#id')[0]) {$('#id').focus().select();}", 1000);
        setTimeout("if(document.activeElement != $('#id')[0]) {$('#id').focus().select();}", 2000);
        setTimeout("if(document.activeElement != $('#id')[0]) {$('#id').focus().select();}", 3000);
    });
</script>