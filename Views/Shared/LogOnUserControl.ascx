<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    if (Request.IsAuthenticated && MonnitSession.CurrentCustomer != null)
    {


        if (MonnitSession.UserIsCustomerProxied)
        {
            Monnit.Customer cust = Monnit.Customer.Load(MonnitSession.CustomerIDLoggedInAsProxy);
            if (cust == null)
            {
                Response.Redirect("/Account/Logoff");
            }
            else
            {       
%>

<%-- <div style="white-space:nowrap; margin-top: -15px;"><b><%:  (cust.UserName) %></b> [ <a href="/Account/UnProxyCustomer">Admin Mode</a> ]</div> --%>

<%}
        }%>

<div id="accountSection">
    <div id="accountBtn">

        <%--    <% if (MonnitSession.CurrentCustomer.Image.Length > 1)
           { %>
        <span style="float: left">

            <img style="border-radius: 50%; width: 67px" src="<%: string.Format("data:image/{0};base64,{1}",MonnitSession.CurrentCustomer.ImageName.Split('.')[1],Convert.ToBase64String(MonnitSession.CurrentCustomer.Image)) %>" />

        </span>

        <%} %>--%>

        <div class="accountIcon">
            <a style="float: right;" class="accountButton" href="form-url"><span class="icon entypo-cog"></span></a>
        </div>

        <%--<a style="float: right; padding-right: 70px; padding-top: 10px;" href="/Home/Index30">Home 30</a>--%>

        <div style="clear: both"></div>
        <div class="loggedName"><%: System.Web.HttpUtility.HtmlDecode(MonnitSession.CurrentCustomer.Account.CompanyName) %> 
        </div>
        <div style="clear: both"></div>
    </div>
    <!-- End Account Button -->
</div>
<!-- End Account Section -->


<div id="logbox">
    <div id="accountBox">
        <div class="container">
            <div class="menu-box block">
                <!-- MENU BOX (LEFT-CONTAINER) -->
                <h2 class="titular" title="<%=  (MonnitSession.CurrentCustomer.UserName) %>">Welcome <%= System.Web.HttpUtility.HtmlDecode(MonnitSession.CurrentCustomer.FirstName) %>
                    <br />
                    <span title="<%= MonnitSession.CurrentCustomer.Account.AccountNumber %>">Viewing <%= System.Web.HttpUtility.HtmlDecode(MonnitSession.CurrentCustomer.Account.CompanyName) %> </span></h2>
                <ul class="menu-box-menu">
                    <%if (MonnitSession.CustomerCan("Navigation_View_My_Account"))
                      { %>
                    <li>
                        <a class="menu-box-tab" href="/Account/Overview"><span class="icon fontawesome-briefcase scnd-font-color"></span>Account Settings</a>
                    </li>

                    <% }%>


                    <%if (MonnitSession.CustomerCan("Navigation_View_My_Account") && !string.IsNullOrEmpty(MonnitSession.CurrentTheme.FromPhone))
                      { %>
                    <li>
                        <a class="menu-box-tab" href="/Account/NotificationCreditList"><span class="icon scnd-font-color">
                            <img src="/Content/images/credit.png" style="width: 28px; margin: 10px 0px 0px 0px;" />
                        </span>Notification Credits</a>
                    </li>

                    <% }%>

                    <%if (MonnitSession.CustomerCan("Navigation_View_My_Account"))
                      { %>
                    <li>
                        <a class="menu-box-tab" href="/Account/AccountUserList"><span class="icon  scnd-font-color">
                            <img src="/Content/images/pos.png" style="width: 28px; margin: 10px 0px 0px 0px;" />
                        </span>Users</a>
                    </li>

                    <% }%>

                    <%if (MonnitSession.CustomerCan("Navigation_View_API"))
                      { %>
                    <li>
                        <a class="menu-box-tab" href="/Api"><span class="icon  scnd-font-color">
                            <img src="/Content/images/webhook.png" style="width: 28px; margin: 10px 0px 0px 0px;" />
                        </span>API / WebHook</a>
                    </li>
                    <%} %>

                    <%  List<Monnit.CustomerAccountLink> calList = Monnit.CustomerAccountLink.LoadAllByCustomerID(MonnitSession.CurrentCustomer.CustomerID);

                        int deletedCount = 0;
                        foreach (var item in calList)
                        {
                            if (item.CustomerDeleted)
                                deletedCount++;
                        }

                        if (calList.Count > 0 && calList.Count != deletedCount)
                        { 
                    %>

                    <li>
                        <a class="menu-box-tab" href="/Account/AccountLink"><span class="icon fontawesome-eye-open scnd-font-color"></span>Linked Accounts<%--<div class="menu-box-number">1</div>--%>
                        </a>
                    </li>

                    <% }%>
                    <% Html.RenderPartial("CogList"); %>
                    <% if (MonnitSession.UserIsCustomerProxied)
                       {
                           Monnit.Customer cust = Monnit.Customer.Load(MonnitSession.CustomerIDLoggedInAsProxy);
                           if (cust == null)
                           {
                               Response.Redirect("/Account/Logoff");
                           } %>
                    <li>
                        <a class="menu-box-tab" href="/Account/UnProxyCustomer"><span class="icon entypo-home scnd-font-color"></span>Go Home</a>
                    </li>

                    <%}
                       else
                       {%>
                    <li>
                        <a class="menu-box-tab" href="/Account/Logoff"><span class="icon fontawesome-off scnd-font-color"></span>Log Out</a>
                    </li>
                    <%} %>
                    <!--
                        <li>
                            <a class="menu-box-tab" href="/Account/Overview"><span class="icon fontawesome-envelope scnd-font-color"></span>Messages<div class="menu-box-number">24</div></a>                            
                        </li> -->

                    <!-- <li>
                            <a class="menu-box-tab" href="/Account/Overview"><span class="icon entypo-user scnd-font-color"></span>Account Users<div class="menu-box-number">2</div></a>
                        </li> -->
                </ul>
            </div>
        </div>
    </div>
    <!-- End Account Box -->
</div>


<%--<div style="white-space:nowrap;" title="<%= MonnitSession.CurrentCustomer.Account.AccountNumber%>">
                <span>Welcome  <b><%:  (Page.User.Identity.Name) %></b>!</span> [ <a href="/Account/LogOff">Log Off</a> ]
            </div>--%>

<%if (MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Navigation_View_My_Account")))
  { %>
<%--<div><a href="/Account/Overview">My Account</a></div> --%>
<% }

    }
    else
    {
        var rd = ViewContext.RouteData;
        var currentAction = rd.GetRequiredString("action");
        var currentController = rd.GetRequiredString("controller");
        if (currentController.ToLower() != "account" || currentAction.ToLower() != "logon")
        { 
%> 
        [ <a href="/Account/LogOnOV">Login</a> ]
<%
        }
    }
%>
