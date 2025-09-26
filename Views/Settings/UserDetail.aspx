<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Customer>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    User Settings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%="" %>

    <%
        bool disableAdminBox = false;
        if (!MonnitSession.CurrentCustomer.IsAdmin || Model.Account.PrimaryContactID == Model.CustomerID)
        {
            disableAdminBox = true;
        }
    %>

    <div class="container-fluid">
        <div class="col-12 px-0">
            <%Html.RenderPartial("UserLink", Model); %>
        </div>

        <!-- profile progress section -->
        <div class="col-12">
            <% foreach (SystemHelp s in SystemHelp.LoadByAccount(Model.AccountID))
                {
                    /*Do we keep this next line?*/
                    if (s.CustomerID == Model.CustomerID)
                    {
                        if (s.Type == "Customer_Setup")
                        {
                            if (!string.IsNullOrEmpty(Model.FirstName)
                                && !string.IsNullOrEmpty(Model.LastName)
                                && !Model.NotificationPhone.Equals(""))
                            {
                                s.Delete(); //Auto Remove if nothing left to do
                                break; //continue to next pottential system help
                            }
            %>

            <div class="alert alert-success shadow-sm rounded px-0 mb-4" role="success">
                <div style="display: flex; justify-content: space-between;">

                    <h2 class="ms-4"><%: Html.TranslateTag("Settings/UserDetail|Profile Information Missing", "Profile Information Missing")%></h2>


                    <div class="clearfix"></div>
                    <svg onclick="removeSystemHelp(<%= s.SystemHelpID %>)" xmlns="http://www.w3.org/2000/svg" class="closeBtn me-3" viewBox="0 0 79.175 79.175">
                        <path id="Union_1" data-name="Union 1" d="M9.9,79.9,4.243,74.246a6,6,0,0,1,0-8.485L27.931,42.073,4.95,19.092a7,7,0,0,1,0-9.9L9.193,4.95a7,7,0,0,1,9.9,0L42.073,27.931,65.761,4.243a6,6,0,0,1,8.485,0L79.9,9.9a6,6,0,0,1,0,8.486L56.215,42.073,79.2,65.054a7,7,0,0,1,0,9.9L74.953,79.2a7,7,0,0,1-9.9,0L42.073,56.215,18.385,79.9a6,6,0,0,1-8.486,0Z" transform="translate(-2.486 -2.486)" />
                    </svg>

                </div>
                <div class="px-2 ms-md-3 px-md-0">
                    <%if (string.IsNullOrEmpty(Model.FirstName))
                        { %>
                    <div onclick="progressNotificationSelect('FirstName')" class="gridPanel eventsList mini-card rounded mx-1 shadow-sm">
                        <div style="width: 100%;">
                            <div class="viewSensorDetails eventsList__tr innerCard-holder" style="height: 60px;">
                                <div class="col-xs-3 px-2" style="display: flex; justify-content: space-around; align-items: center; flex-direction: row;">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" viewBox="0 0 85.294 68.294">
                                        <g transform="translate(-203.095 -441.077)">
                                            <path style="fill: #666;" id="Path_9" d="M261.506,506.57a5.746,5.746,0,0,1-5.01,2.756q-23.471.06-46.94.008a6.294,6.294,0,0,1-6.434-6.566,57.76,57.76,0,0,1,.22-7.924c.952-6.732,4.792-11.368,10.986-14.074a17.469,17.469,0,0,1,8.608-1.257,22.8,22.8,0,0,1,4.018,1.231,21.428,21.428,0,0,0,14.005-.651c2.874-1.123,5.672-.629,8.486.072.239.06.5.623.5.956.039,2.86.024,5.721.022,8.582a12.563,12.563,0,0,0,3.907,9.5C256.423,501.641,258.952,504.1,261.506,506.57Z" />
                                            <path style="fill: #666;" id="Path_10" d="M249.968,458.138a17.044,17.044,0,1,1-17.025-17.061A17.028,17.028,0,0,1,249.968,458.138Z" />
                                            <path style="fill: #666;" id="Path_11" d="M264.007,475.412c2.254,0,4.516-.114,6.761.037a6.772,6.772,0,0,1,4.244,2.2q5.9,5.862,11.772,11.753c2.108,2.115,2.153,4.481.056,6.6q-5.957,6.03-11.987,11.987a4.142,4.142,0,0,1-6.143.035q-6.229-6.131-12.359-12.365a7.648,7.648,0,0,1-2.121-5.4c-.037-3.531-.031-7.063-.006-10.6a4.256,4.256,0,0,1,4.484-4.448c1.766-.02,3.532,0,5.3,0Zm-1.2,11.484a3.173,3.173,0,0,0,3.135-3.164,3.242,3.242,0,0,0-3.233-3.212,3.327,3.327,0,0,0-3.158,3.21A3.2,3.2,0,0,0,262.806,486.9Z" />
                                        </g>
                                    </svg>
                                </div>
                                <div class="col-xs-6" style="display: flex; align-items: center;">
                                    <%: Html.TranslateTag("Settings/UserDetail|First Name","First Name")%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%} %>

                    <%if (string.IsNullOrEmpty(Model.LastName))
                        { %>
                    <div onclick="progressNotificationSelect('LastName')" class="gridPanel eventsList mini-card rounded mx-1 shadow-sm">
                        <div style="width: 100%;">
                            <div class="viewSensorDetails eventsList__tr innerCard-holder" style="height: 60px;">
                                <div class="col-xs-3 px-2" style="display: flex; justify-content: space-around; align-items: center; flex-direction: row;">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" viewBox="0 0 85.294 68.294">
                                        <g transform="translate(-203.095 -441.077)">
                                            <path style="fill: #666;" id="Path_9" d="M261.506,506.57a5.746,5.746,0,0,1-5.01,2.756q-23.471.06-46.94.008a6.294,6.294,0,0,1-6.434-6.566,57.76,57.76,0,0,1,.22-7.924c.952-6.732,4.792-11.368,10.986-14.074a17.469,17.469,0,0,1,8.608-1.257,22.8,22.8,0,0,1,4.018,1.231,21.428,21.428,0,0,0,14.005-.651c2.874-1.123,5.672-.629,8.486.072.239.06.5.623.5.956.039,2.86.024,5.721.022,8.582a12.563,12.563,0,0,0,3.907,9.5C256.423,501.641,258.952,504.1,261.506,506.57Z" />
                                            <path style="fill: #666;" id="Path_10" d="M249.968,458.138a17.044,17.044,0,1,1-17.025-17.061A17.028,17.028,0,0,1,249.968,458.138Z" />
                                            <path style="fill: #666;" id="Path_11" d="M264.007,475.412c2.254,0,4.516-.114,6.761.037a6.772,6.772,0,0,1,4.244,2.2q5.9,5.862,11.772,11.753c2.108,2.115,2.153,4.481.056,6.6q-5.957,6.03-11.987,11.987a4.142,4.142,0,0,1-6.143.035q-6.229-6.131-12.359-12.365a7.648,7.648,0,0,1-2.121-5.4c-.037-3.531-.031-7.063-.006-10.6a4.256,4.256,0,0,1,4.484-4.448c1.766-.02,3.532,0,5.3,0Zm-1.2,11.484a3.173,3.173,0,0,0,3.135-3.164,3.242,3.242,0,0,0-3.233-3.212,3.327,3.327,0,0,0-3.158,3.21A3.2,3.2,0,0,0,262.806,486.9Z" />
                                        </g>
                                    </svg>
                                </div>

                                <div class="col-xs-6" style="display: flex; align-items: center;">
                                    <%: Html.TranslateTag("Settings/UserDetail|Last Name","Last Name")%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%} %>



                    <%if (Model.NotificationPhone.Equals(""))
                        { %>
                    <div class="gridPanel eventsList mini-card rounded mx-1 shadow-sm">
                        <div style="width: 100%;">
                            <a href="/Settings/UserNotification/<%:Model.CustomerID %>/?focus">
                                <div class="viewSensorDetails eventsList__tr innerCard-holder" style="height: 60px;">
                                    <div class="col-xs-3 px-2" style="display: flex; justify-content: space-around; align-items: center; flex-direction: row;">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" viewBox="0 0 85.294 68.294">
                                            <g transform="translate(-203.095 -441.077)">
                                                <path style="fill: #666;" id="Path_9" d="M261.506,506.57a5.746,5.746,0,0,1-5.01,2.756q-23.471.06-46.94.008a6.294,6.294,0,0,1-6.434-6.566,57.76,57.76,0,0,1,.22-7.924c.952-6.732,4.792-11.368,10.986-14.074a17.469,17.469,0,0,1,8.608-1.257,22.8,22.8,0,0,1,4.018,1.231,21.428,21.428,0,0,0,14.005-.651c2.874-1.123,5.672-.629,8.486.072.239.06.5.623.5.956.039,2.86.024,5.721.022,8.582a12.563,12.563,0,0,0,3.907,9.5C256.423,501.641,258.952,504.1,261.506,506.57Z" />
                                                <path style="fill: #666;" id="Path_10" d="M249.968,458.138a17.044,17.044,0,1,1-17.025-17.061A17.028,17.028,0,0,1,249.968,458.138Z" />
                                                <path style="fill: #666;" id="Path_11" d="M264.007,475.412c2.254,0,4.516-.114,6.761.037a6.772,6.772,0,0,1,4.244,2.2q5.9,5.862,11.772,11.753c2.108,2.115,2.153,4.481.056,6.6q-5.957,6.03-11.987,11.987a4.142,4.142,0,0,1-6.143.035q-6.229-6.131-12.359-12.365a7.648,7.648,0,0,1-2.121-5.4c-.037-3.531-.031-7.063-.006-10.6a4.256,4.256,0,0,1,4.484-4.448c1.766-.02,3.532,0,5.3,0Zm-1.2,11.484a3.173,3.173,0,0,0,3.135-3.164,3.242,3.242,0,0,0-3.233-3.212,3.327,3.327,0,0,0-3.158,3.21A3.2,3.2,0,0,0,262.806,486.9Z" />
                                            </g>
                                        </svg>
                                    </div>
                                    <div class="col-xs-6" style="display: flex; align-items: center;">
                                        <%: Html.TranslateTag("Settings/UserDetail|Notification Phone Number","Notification Phone Number")%>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                    <%} %>
                </div>
                <div style="clear: both;"></div>
            </div>

            <%}  
                }   
              }%>  
        </div>

        <div class="col-12">
            <div class="rule-card_container w-100" id="hook-one" style="margin-top:53px">
                <div class="card_container__top d-flex justify-content-between">
                    <div class="card_container__top__title d-flex">
                        <%: Html.TranslateTag("Settings/UserDetail|User Details","User Details")%>
                        <span class="col-6" style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; color: #707070; font-size: small; margin-left: 30px">[<%= Model.FirstName %> <%= Model.LastName%>] - <%=Model.UserName%></span>

                        <div class="nav navbar-right panel_toolbox" style="margin-left:auto">
                            <!-- help button  userdetails-->
                            <%if (MonnitSession.CurrentCustomer.CustomerID == Model.CustomerID || (MonnitSession.CurrentCustomer.CustomerID != Model.CustomerID && MonnitSession.CustomerCan("Customer_Edit_Other")))
                                { %>
                            <a href="/Settings/UserExportData/<%=Model.CustomerID %>" target="_blank" title="<%: Html.TranslateTag("Overview/SensorEdit|In an effort to be transparent with our data gathering practices we have added this button to allow users to see all the data we have about them.","In an effort to be transparent with our data gathering practices we have added this button to allow users to see all the data we have about them.") %>">
                                <i style="font-size: 1.2em;" class="fa fa-cloud-download download-icon"></i>
                            </a>
                            <%} %>
                        </div>
                    </div>
                </div>

                <div class="x_content" style="padding-left: 20px; background-color: white;">
                    <form method="post" action="/Settings/UserDetail/<%=Model.CustomerID %>" class="form-horizontal form-label-left" style="margin-top:20px;">
                        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                        <%
                            bool CanEditUser = false;
                            if (MonnitSession.CurrentCustomer.CustomerID == Model.CustomerID && MonnitSession.CustomerCan("Customer_Edit_Self"))
                                CanEditUser = true;

                            if (MonnitSession.CurrentCustomer.CustomerID != Model.CustomerID && MonnitSession.CustomerCan("Customer_Edit_Other"))
                                CanEditUser = true;

                        %>

                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3 col-lg-2">
                                <%: Html.TranslateTag("Settings/UserDetail|Login Name","Login Name")%>:
                            </div>
                            <div class="col sensorEditFormInput">
                                <% if (MonnitSession.CustomerCan("Customer_Change_Username"))
                                    { %>
                                <input type="text" name="UserName" class="form-control user-dets" value="<%= Model.UserName %>">
                                <%}
                                    else
                                    {%>
                                <input type="text" disabled class="form-control user-dets" placeholder="<%= Model.UserName %>">
                                <%} %>

<%--                                <div class="editor-error" style="color: green;">
                                    <%: Html.ValidationMessageFor(model => model.UserName)%>
                                </div>--%>
                            </div>
                        </div>

                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3 col-lg-2">
                                <%: Html.TranslateTag("Settings/UserDetail|First Name","First Name")%>:
                            </div>
                            <div class="col sensorEditFormInput">
                                <input type="text" <%= CanEditUser ? "" :"disabled" %> name="FirstName" id="FirstName" class="form-control user-dets" style="width: 250px;" value="<%= Model.FirstName %>">
<%--                                <div class="editor-error" style="color: green;">
                                    <%: Html.ValidationMessageFor(model => model.FirstName) %>
                                </div>--%>
                            </div>
                        </div>

                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3 col-lg-2">
                                <%: Html.TranslateTag("Settings/UserDetail|Last Name","Last Name")%>:
                            </div>

                            <div class="col sensorEditFormInput">
                                <input type="text" <%= CanEditUser ? "" :"disabled" %> name="LastName" id="LastName" class="form-control user-dets" style="width: 250px;" value="<%= Model.LastName %>">
<%--                                <div class="editor-error" style="color: green;">
                                    <%: Html.ValidationMessageFor(model => model.LastName) %>
                                </div>--%>
                            </div>
                        </div>

                        <% if (Model != null && Model.Account != null && Model.Account.SamlEndpointID > 0)
                            {
                                SamlEndpoint endpoint = SamlEndpoint.Load(Model.Account.SamlEndpointID);
                                if (endpoint != null)
                                { %>
                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3 col-lg-2">
                                <%: Html.TranslateTag("Custom Saml Name",  endpoint.Name + " Name ID")%>:
                            </div>
                            <div class="col sensorEditFormInput">
                                <input type="text" <%= CanEditUser ? "" :"disabled" %> name="SamlNameID" id="SamlNameID" class="form-control user-dets" style="width: 250px;" value="<%= Model.SamlNameID %>">
<%--                                <div class="editor-error" style="color: green;">
                                    <%: Html.ValidationMessageFor(model => model.SamlNameID) %>
                                </div>--%>
                            </div>
                        </div>
                        <% }
                            }
                            else
                            { %>

                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3 col-lg-2">
                                <%:Html.TranslateTag("Settings/UserDetail|Password","Password")%>:
                            </div>

                            <div class="col sensorEditFormInput">
                                <% if (Model.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
                                    {%>
                                <a href="/Settings/UserChangePassword" class="btn btn-secondary btn-sm me-2"><%:Html.TranslateTag("Change password","Change password")%></a>
                                <%} %>
                                <% if (MonnitSession.CustomerCan("Customer_Reset_Password_Other"))
                                    {%>
                                <a onclick="defaultPassword('<%:Model.CustomerID  %>'); return false;" class="btn btn-secondary btn-sm"><%:Html.TranslateTag("Settings/UserDetail|Reset password", "Reset password")%></a>

                                <%} if (MonnitSession.CurrentCustomer.IsAdmin)
                                             {%>
                                <a href="/Settings/AdminResetPassword?customerID=<%:Model.CustomerID%>" class="btn btn-secondary btn-sm ms-2"><%:Html.TranslateTag("Admin reset password","Admin reset password")%></a>
                                <%} %>
                                <%} %>
                                <%if (MonnitSession.CustomerCan("Unlock_User") && Model.isLocked())
                                    {%>
                                <a title="<%:Html.TranslateTag("Settings/UserDetail|Unlocks a user with too many failed log in attempts","Unlocks a user with too many failed log in attempts")%>" class="btn btn-grey" onclick="unlockUser()"><i class="fa fa-lock">&nbsp;</i><%:Html.TranslateTag("Settings/UserDetail|Unlock Login","Unlock Login")%></a>
                                <%
                                    }%>
                                <div id="passwordmessage" class="editor-error" style="color: green;">
                                </div>
                            </div>
                        </div>

                        <%if (MonnitSession.CurrentCustomer.IsAdmin && MonnitSession.CurrentTheme.IsTFAEnabled)
                            {%>

                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3 col-lg-2">
                                <%: Html.TranslateTag("Settings/UserDetail|Two Factor Authentication (2FA)", "Two Factor Authentication (2FA)") %>:
                            </div>
                            <div class="col sensorEditFormInput">
                                <%
                                    string bypass2FA =
                                        Model.ByPass2FAPermanent ? "always" :
                                        Model.ByPass2FA ? "once" :
                                        "";
                                %>
                                <select id="select2FA" class="form-select">
                                    <option value="enabled" <%: bypass2FA == "" ? "selected" : "" %> >
                                        <%: Html.TranslateTag("Enabled", "Enabled") %>
                                    </option>
                                    <option value="once"    <%: bypass2FA == "once" ? "selected" : "" %> >
                                        <%: Html.TranslateTag("Bypass Next Login", "Bypass Next Login")%>
                                    </option>
                                    <option value="always"  <%: bypass2FA == "always" ? "selected" : "" %> >
                                        <%: Html.TranslateTag("Bypass Indefinitely", "Bypass Indefinitely")%>
                                    </option>
                                </select>
                            </div>
                        </div>

                        <% } if (CanEditUser)
                            {%>
                        <br />
                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3 col-lg-2">
                                <%: Html.TranslateTag("Settings/UserDetail|Is Administrator","Administrator")%>:
                            </div>
                            <div class="col sensorEditFormInput  checkbox-wrapper-65">
                                <label for="ckbAttachment">
                                <input style="cursor: pointer;" <%= disableAdminBox ? "Disabled" : " " %> type="checkbox" name="isAdmin" id="ckbAttachment" <%:Model.IsAdmin ? "checked='checked'" : ""%> />
                                  <span class="cbx">
                                        <svg width="12px" height="11px" viewBox="0 0 12 11">
                                            <polyline points="1 6.29411765 4.5 10 11 1"></polyline>
                                        </svg>                          
                                    </span>
                                    </label>
                                <%--<input style="cursor: pointer;" <%= MonnitSession.CurrentCustomer.IsAdmin ? "" : "Disabled" %> type="checkbox" name="isAdmin" <%:Model.IsAdmin ? "checked='checked'" : ""%> />--%>
                            </div>
                        </div>
                        <script>
                            $("#ckbAttachment").change(function () {
                                if (this.checked) {
                                    $('#lblAttachmentWarning').html('*<%: Html.TranslateTag("Export/_BuildReport|Sending the report as an attachment could cause the email to arrive as spam","Sending the report as an attachment could cause the email to arrive as spam.") %>');
			}
			else {
                                      $('#lblAttachmentWarning').html('*<%: Html.TranslateTag("Export/_BuildReport|The email will contain a report link that will expire after 30 days","The email will contain a report link that will expire after 30 days.") %>');
                                  }
                              });  
                        </script>

                        <div class="row sensorEditForm">
                            <div class="col-12 text-end">
                                <button type="submit" onclick="$(this).hide();$('#saving').show();" value="<%:Html.TranslateTag("Settings/UserDetail|Save","Save")%>" class="btn btn-primary">
                                    <%:Html.TranslateTag("Settings/UserDetail|Save","Save")%>
                                </button>
                                <button class="btn btn-primary" id="saving" style="display: none;" type="button" disabled>
                                    <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                    <%:Html.TranslateTag("Settings/UserDetail|Saving...","Saving...")%>
                                </button>
                            </div>
                        </div>
                        <% } %>
                    </form>
                    <div style="clear: both;"></div>
                </div>
            </div>
        </div>


        <% //Set to always false for now because we have moved to User Preference Maintenance Notifications. BY 10/29
            if (false && AccountTheme.Find(Model.Account).SendMaintenanceNotification == true)
            { %>

        <div class="col-12">
            <div class="x_panel gridPanel powertourhook shadow-sm rounded" id="hook-two">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <%: Html.TranslateTag("Settings/UserDetail|System Maintenance","System Maintenance")%>
                    </div>
                </div>

                <div class="x_content ms-3">

                    <form method="post" action="/Settings/UserMaintenanceDelivery/<%=Model.CustomerID %>" class="form-horizontal form-label-left">
                        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                        <div class="row">
                            <div class="bold col-12">
                                <div class="form-group" style="font-weight: normal;">
                                    <%: Html.TranslateTag("Settings/UserDetail|Choose  desired delivery methods for system maintenance notifications.","Choose  desired delivery methods for system maintenance notifications.")%><br />
                                </div>
                            </div>
                        </div>

<%--                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3 col-lg-2">
                                <%: Html.TranslateTag("Settings/UserDetail|Email","Email")%>:
                            </div>

                            <div class="col sensorEditFormInput" id="hook-two-outline">
                                <div class="form-group d-flex align-items-center" id="toEmailMaintenance">
                                    <%if (CanEditUser)
                                        {%>
                                    <input type="checkbox" class="me-2" id="toEmail" name="SendMaintenanceNotificationToEmail" <%:Model.SendMaintenanceNotificationToEmail ? "checked='checked'" : ""%> />
                                    <% } %>
                                    <span style="font-size: 0.9em;"><%: Model.NotificationEmail %></span>
                                </div>
                            </div>
                        </div>--%>

<%--                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3 col-lg-2">
                                <%: Html.TranslateTag("Settings/UserDetail|Text (SMS)","Text (SMS)")%>:
                            </div>
                            <div class="col sensorEditFormInput form-group d-flex align-items-center" id="hook-two-outline">
                                <%if (CanEditUser)
                                    {%>
                                <input type="checkbox" class="me-2" id="toPhone" name="SendMaintenanceNotificationToPhone" <%= string.IsNullOrEmpty(Model.NotificationPhone) ? "disabled" : "" %> <%:Model.SendMaintenanceNotificationToPhone ? "checked='checked'" : ""%> />
                                <% } %>
                                <span style="font-size: 0.9em; vertical-align: text-bottom;"><%= string.IsNullOrEmpty(Model.NotificationPhone) ? Html.TranslateTag("Settings/UserDetail|No Number Found","No Number Found") : Model.NotificationPhone %></span>
                            </div>
                        </div>--%>

                        <div class="row">
                            <div class="form-group">
                                <%if (CanEditUser)
                                    {%>
                                <div class="col-12 text-end">
                                    <div id="notimessage" class="me-4" style="color: green;">
                                    </div>
                                    <button type="submit" id="mainSave" onclick="$(this).hide();$('#savingSysMain').show();" value="<%:Html.TranslateTag("Settings/UserDetail|Save","Save")%>" class="btn btn-primary me-4">
                                        <%:Html.TranslateTag("Settings/UserDetail|Save","Save")%>
                                    </button>
                                    <button class="btn btn-primary me-4" id="savingSysMain" style="display: none;" type="button" disabled>
                                        <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                        <%:Html.TranslateTag("Settings/UserDetail|Saving...","Saving...")%>
                                    </button>
                                </div>
                                <% } %>
                            </div>
                        </div>
                    </form>

                    <div style="clear: both;"></div>

                </div>
            </div>
        </div>
        <%} %>

    </div>
    
    <script type="text/javascript">
        <%: ExtensionMethods.LabelPartialIfDebug("UserDetail.aspx")  %>

        var customerID = <%: Model.CustomerID  %>;

        $(function () {

            <%if (Model.NotificationOptIn < Model.NotificationOptOut)
        {%>
            document.getElementById("toEmail").disabled = true;
            document.getElementById("toPhone").disabled = true;
            $('#mainSave').hide();
            <%}%>

            $('#notimessage').html('<%=ViewBag.NotiMessage%>');
            $('#passwordmessage').html('<%=ViewBag.PassMessage%>');
        });

        if (`<%=ViewBag.UserMessage%>`.length > 1) {
                    toastBuilder(`<%=ViewBag.UserMessage%>`);
                }

        function defaultPassword(custID) {
            $.get('/Settings/DefaultPassword/' + custID, function (data) {
                alert(data);
            });
        }

        function byPass2FA(custid, setBypass) {
            $.post('/Settings/ByPass2FA/' + custid, { bypass: setBypass }, function (data) {
                if (data.includes('Success')) {
                    window.location.reload();
                }
            });
        }

        function byPass2FAPermanent(custid, setBypass) {
            $.post('/Settings/ByPass2FAPermanent/' + custid, { bypass: setBypass }, function (data) {
                if (data.includes('Success')) {
                    window.location.reload();
                }
            });
        }

        //$("#select2FA").click(
        //    function (e) {
        //        $("#select2FA").data("prev", $(this).val());
        //    }
        //)

        $("#select2FA").change(
            function (e) {
                //var prev = $(this).data("prev"); 
                //console.log(prev);
                var val = $(this).val();
                //console.log(val);
                if (val == "enabled") {
                    select2FA(false, false);
                } else if (val == "once") {
                    select2FA(true, false);
                } else if (val == "always") {
                    select2FA(false, true);
                } else {
                    console.log("select2FA invalid value")
                }
            }
        );

        //let z = 0;

        function select2FA(bypassOnce, bypassAlways) {
            $.post('/Settings/ByPass2FA/' + customerID, { bypass: bypassOnce }, function (result) {
                    //console.log(++z);
                    console.log(result);
                }
            )
            .then(
                function (data) {
                    //console.log(++z);
                    console.log(data);
                    return $.post('/Settings/ByPass2FAPermanent/' + customerID, { bypass: bypassAlways }, function (result) {
                            //console.log(++z);
                            console.log(result);
                        }
                    );
                }
            )
            .then(
                function (data) {
                    //console.log(++z);
                    console.log(data);
                    window.location.reload();
                }
            );
        }

        function unlockUser(custID, custGuid) {
            $.post('/Settings/UnlockCustomer', { customerid: '<%=Model.CustomerID%>', guid: '<%=Model.GUID%>' }, function (data) {
                alert(data);
                if (data.includes('Success')) {
                    window.location.href = window.location.href;
                }
            });
        }

        function progressNotificationSelect(field) {
            let input = document.getElementById(field);
            input.classList.add('selectMaintenance');
            input.scrollIntoView();
            input.focus();
        }

        function goBack() {
            window.history.back();
        }

        function removeSystemHelp(s) {
            $.post("/Overview/ClearSystemHelp", { id: s }, function (data) {
                if (data == "Success") {
                    window.location.href = window.location.href;
                }
                else {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
            });
        }

        //Remove the focus() class for selected inputs
        $('input').on('blur', function () {
            $(this).removeClass('selectMaintenance');
        })

    </script>

</asp:Content>
