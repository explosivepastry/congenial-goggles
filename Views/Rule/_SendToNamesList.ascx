<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<NotificationRecipient>>" %>

<%if (Model.Count > 0)
    {

        foreach (NotificationRecipient recipient in Model)
        {
            Customer cust = recipient.CustomerToNotify;
            string titleDisplay = "";
            string name = "";

            if (cust != null)
                switch (recipient.NotificationType)
                {
                    case eNotificationType.SMS:
                        titleDisplay = cust.NotificationPhone.ToString();
                        name = cust.FullName;
                        break;
                    case eNotificationType.Phone:
                        titleDisplay = cust.NotificationPhone2.ToString();
                        name = cust.FullName;
                        break;
                    case eNotificationType.Group:
                        CustomerGroup group = CustomerGroup.Load(recipient.CustomerGroupID);
                        name = "User Group";
                        if (group != null)
                        {
                            titleDisplay = "";
                            name = group.Name;
                        }

                        break;
                    case eNotificationType.Email:
                    default:
                        titleDisplay = cust.NotificationEmail.ToString();
                        name = cust.FullName;
                        break;

                }

%>
<div class="user-stamp_container">
    <div class="profile-msg-icon"><%=Html.GetThemedSVG("profile") %></div>
    <span class="user-paste" title="<%=titleDisplay %>"><%=name.Replace(",","").Replace(".","") %> &nbsp;</span>
</div>
<%}

    } %>
