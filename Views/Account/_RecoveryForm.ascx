<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Customer>" %>

<% 
    Account account = Account.Load(Model.AccountID);    
%>


<div class="col-xs-1 col-sm-1 col-md-2"></div>
<div id="loginForm" class="col-xs-10 col-sm-10 col-md-8">

    <h1 style="font-size: 20px; font-weight: bold; text-align: center;"><%: Html.TranslateTag("Account/LogOnOV|Account Recovery Form","Account Recovery Form")%></h1>
    
    <form action="/Account/RecoveryForm/<%: Model.CustomerID %>" method="post">

    <div style="text-align: left;">
        <p>
            Switch Account Primary Contact to the following user:
        </P>
    </div>
    <div class="form-group" style="text-align: center;">
        <input type="hidden" value="<%: Model.CustomerID %>">
        <input class="form-control" name="Customer" type="text" disabled value="<%: Model.FullName %>">
        <input type="submit" id="switchBtn" value="<%: Html.TranslateTag("Account/AccountRecovery|Switch Primary Contact","Switch Primary Contact")%>" class="btn btn-primary btn-block" style="margin-top: 10px" />
        <a id="cancelBtn" href="/Account/LogOnOV/" class="btn btn-secondary btn-block" style="margin-top: 20px">Cancel<a/>
    </div>

    </form>
    <%--<input type="button" id="OtherOption" value="<%: Html.TranslateTag("Account/AccountRecovery|Choose another Primary Contact","Choose a different Primary Contact")%>" class="btn btn-secondary btn-block" style="margin-top: 10px" />--%>

    <% if (ViewData["Results"] != null)
           { %>
        <br />
        <div class="x_panel">
            <font color="red"><%: ViewData["Results"]  %></font>
        </div>

        <%} %>


</div>




