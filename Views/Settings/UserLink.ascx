<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Customer>" %>

<div class="col-12 view-btns_container">
    <div class="view-btns deviceView_btn_row shadow-sm rounded">

    <%if (MonnitSession.CustomerCan("Customer_Edit_Other")) 
      {%>
    <a id="indexlink" href="/Settings/AccountUserList/<%=Model.AccountID %>" class="btn btn-lg btn-fill">
        <div class=" btn-secondaryToggle btn-lg btn-fill "><%=Html.GetThemedSVG("recipients") %><span class="extra text-center"><%: Html.TranslateTag("Users","Users") %></span></div>
    </a>
    <% } %>

    <%if (MonnitSession.CustomerCan("Customer_Edit_Other") || (MonnitSession.CustomerCan("Customer_Edit_Self")) && MonnitSession.CurrentCustomer.CustomerID == Model.CustomerID)
      {%>
    <a href="/Settings/UserDetail/<%:Model.CustomerID %>" class="deviceView_btn_row__device">
        <div class="btn-<%:Request.Path.StartsWith("/Settings/UserDetail/")?"active-fill shadow":" " %> btn-lg btn-fill btn-secondaryToggle">
            <%=Html.GetThemedSVG("accountDetails") %>
            <span class="extra text-center"><%: Html.TranslateTag("Settings/UserLink|Details","Details")%></span>
        </div>
    </a>
    <%} %>

    <%if (MonnitSession.CurrentCustomer.IsAdmin && Model.IsActive)
      {%><a href="/Settings/UserPermission/<%:Model.CustomerID %>" class="deviceView_btn_row__device">
          <div class="btn-<%:Request.Path.StartsWith("/Settings/UserPermission/")?"active-fill shadow":" " %> btn-lg btn-fill btn-secondaryToggle">
              <%=Html.GetThemedSVG("lock") %>
              <span class="extra text-center"><%: Html.TranslateTag("Settings/UserLink|Permissions","Permissions")%></span>
          </div>
         </a>
        <% } %>

    <%if (MonnitSession.CustomerCan("Customer_Edit_Other") || (MonnitSession.CustomerCan("Customer_Edit_Self") && MonnitSession.CurrentCustomer.CustomerID == Model.CustomerID))
      {%><a href="/Settings/UserPreference/<%:Model.CustomerID %>" class="deviceView_btn_row__device">
        <div class="btn-<%:Request.Path.StartsWith("/Settings/UserPreference/")?"active-fill shadow":" " %> btn-lg btn-fill btn-secondaryToggle">
            <%=Html.GetThemedSVG("preferences") %>
            <span class="extra text-center"><%: Html.TranslateTag("Settings/UserLink|Preferences","Preferences")%>
            </span>
        </div>
    </a><%} %>

    <%if (MonnitSession.CustomerCan("Customer_Edit_Other") || (MonnitSession.CustomerCan("Customer_Edit_Self") && MonnitSession.CurrentCustomer.CustomerID == Model.CustomerID))
      {%><a href="/Settings/UserNotification/<%:Model.CustomerID %>" class="deviceView_btn_row__device">
          <div class="btn-<%:Request.Path.StartsWith("/Settings/UserNotification/")?"active-fill shadow":" " %> btn-lg btn-fill btn-secondaryToggle">
              <%=Html.GetThemedSVG("notifications") %>
              <span class="extra text-center"><%: Html.TranslateTag("Settings/UserLink|Notifications","Notifications")%>
              </span>
          </div>
         </a><%} %>

      <%if (MonnitSession.CustomerCan("Customer_Admin_AccountLink"))
      {%><a href="/Settings/UserAccountLinking/<%:Model.CustomerID %>" class="deviceView_btn_row__device">
          <div class="btn-<%:Request.Path.StartsWith("/Settings/UserAccountLinking/")?"active-fill shadow":" " %> btn-lg btn-fill btn-secondaryToggle">
              <%=Html.GetThemedSVG("link") %>
              <span class="extra text-center"><%: Html.TranslateTag("Settings/UserLink|Linking","Linking")%>
              </span>
          </div>
         </a><% } %>
    
        <%if (MonnitSession.CurrentTheme.IsTFAEnabled)
            { %>
        <a style="max-width: 80px;" href="/Settings/UserTwoFactorAuth/<%:Model.CustomerID %>" class="deviceView_btn_row__device">
            <div class="btn-<%:Request.Path.StartsWith("/Settings/UserTwoFactorAuth/") ? "active-fill" : " " %> btn-lg btn-fill btn-secondaryToggle">
                <%=Html.GetThemedSVG("tfa") %>
                <span class="extra text-center"><%: Html.TranslateTag("Settings/UserLink|2FA", "2FA")%></span>
            </div>
        </a>
        <% } %>

    </div>
</div>

<script>
    $('.btn-secondaryToggle').hover(
        function () { $(this).addClass('active-hover-fill') },
        function () { $(this).removeClass('active-hover-fill') }
    )
</script>
<style>
    #svg_arrowLeft, #svg_notifications {
        fill: #666 !important;
    }

    .btn-active-fill #svg_notifications {
        fill: #fff !important;
    }

    .extra {
        max-width: 80px;
        overflow-wrap: break-word;
        text-align: center;
    }

    .btn-fill {
        width: 90px!important;
    }

</style>
