<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Account>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Account Settings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid mt-4">
        <%Html.RenderPartial("AccountLink", Model); %>

        <div class="settingsContainer">
            <div class="rule-card_container w-100" style="margin-right: 1rem;">
                <form class="form-horizontal form-label-left" action="/Settings/AccountEdit/<%:Model.AccountID %>" id="accountForm" method="post">
                    <div class="">
                        <div class="x_title">

                            <div class="card_container__top__title dfjcsb" style="overflow: unset;">
                                <span><%: Html.TranslateTag("Account Settings","Account Settings")%></span>
                                <div style="margin-left: auto;">
                                    <%if (MonnitSession.CurrentCustomer.IsAdmin && MonnitSession.CurrentTheme.SupportsSaml)
                                        {%>
                                    <a role="button" class="btn btn-secondary btn-sm pe-1" href="/Settings/SamlSettings/<%=Model.AccountID %>"><%: Html.TranslateTag("SAML Endpoint","SAML Endpoint")%>
                                        <%=Html.GetThemedSVG("edit") %>
                                    </a>
                                    <%} %>
                                    <a role="button" class="btn btn-secondary btn-sm" href="/Network/List">
                                        <%:Html.TranslateTag("Networks","Networks")%>
                                        <%=Html.GetThemedSVG("network") %>
                                    </a>
                                    <a role="button" class="btn btn-secondary btn-sm" href="/Settings/AccountPreference/<%=Model.AccountID %>">
                                        <%: Html.TranslateTag("Preferences","Preferences")%>
                                        <%=Html.GetThemedSVG("preferences") %>
                                    </a>
                                </div>
                            </div>
                        </div>
                        <%: Html.ValidationSummary(true) %>
                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3">
                                <%: Html.TranslateTag("Settings/AccountEdit|Account Number","Account Number") %>
                            </div>
                            <div class="col sensorEditFormInput">
                                <input type="text" id="AccountNumber" class="form-control user-dets" name="AccountNumber" value="<%= Model.AccountNumber %>" />
                                <div class="editor-error">
                                    <%: Html.ValidationMessageFor(model => model.AccountNumber)%>
                                </div>
                            </div>
                        </div>
                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3">
                                <%: Html.TranslateTag("Settings/AccountEdit|Company Name","Company Name") %>
                            </div>
                            <div class="col sensorEditFormInput">
                                <input type="text" id="CompanyName" class="form-control user-dets" name="CompanyName" value="<%= Model.CompanyName %>" />
                                <div class="editor-error">
                                    <%: Html.ValidationMessageFor(model => model.CompanyName)%>
                                </div>
                            </div>
                        </div>
                        <div class="row sensorEditForm" id="primaryContactDropDown">
                            <div class="col-12 col-md-3">
                                <%: Html.TranslateTag("Primary Contact","Primary Contact") %>
                            </div>

                            <div class="col sensorEditFormInput">
                                <!--<input type="text" id="CustomerIDLookup" name="CustomerIDLookup" class="form-control" value="<%//= (Model != null && Model.PrimaryContact != null) ? Model.PrimaryContact.UniqueName : "" %>" required />-->
                                <select name="PrimaryContactID" id="PrimaryContactID" class="form-select user-dets" required>
                                    <%
                                        List<Customer> custList = ViewData["PrimaryContacts"] != null ? ViewData["PrimaryContacts"] as List<Customer> : null;

                                        if (custList != null && custList.Count > 1)
                                        {
                                            Account acc = Account.Load(Model.AccountID);
                                            foreach (Customer cust in custList.OrderBy(c => c.FullName))
                                            {%>
                                    <option value="<%:cust.CustomerID %>" <%: Model.PrimaryContactID == cust.CustomerID ? "selected" : "" %>><%: cust.FirstName%> <%: cust.LastName %> (<%: cust.NotificationEmail %>)</option>
                                    <%}
                                        }
                                        else
                                        {
                                            Customer primary = Model.PrimaryContact;%>
                                    <option value="<%:primary.CustomerID %>" <%Response.Write("selected");%>><%: primary.FirstName%> <%: primary.LastName %> (<%: primary.NotificationEmail %>)</option>
                                    <% } %>
                                </select>
                                <div class="editor-error">
                                    <%: Html.ValidationMessageFor(model => model.PrimaryContactID)%>
                                </div>
                            </div>
                        </div>
                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3">
                            </div>
                            <div class="col sensorEditFormInput">
                                <%=Html.TranslateTag("Only administrator users will appear in this list.") %>
                                <div class="editor-error">
                                </div>
                            </div>
                        </div>
                        <div class="form-group aSettings">
                            <div class="aSettings__title ps-0">
                                <%: Html.TranslateTag("Time Zone","Time Zone") %>
                            </div>

                            <div class="aSettings__input">
                                <%
                                    Monnit.TimeZone tz = Monnit.TimeZone.Load(Model.TimeZoneID);
                                    if (!ViewData.Keys.Contains("Regions"))
                                    {
                                        ViewData["Region"] = tz.Region;
                                        ViewData["Regions"] = new SelectList(Monnit.TimeZone.LoadRegions(), tz.Region);
                                        ViewData["TimeZones"] = new SelectList(Monnit.TimeZone.LoadByRegion(tz.Region), "TimeZoneID", "DisplayName");
                                    }
                                %>
                                <div class="btn-group d-flex flex-wrap justify-content-start" role="group" aria-label="Basic radio toggle button group">
                                    <%
                                        foreach (string region in Monnit.TimeZone.LoadRegions())
                                        {%>

                                    <input type="radio" class="btn-check" onclick="selectRegion('<%:region%>')" name="regions" id="<%:region%>" <%: tz.Region == region ? "checked" : "" %>>
                                    <label class="btn btn-outline-primary" for="<%:region%>"><%:region%></label>
                                    <%}
                                    %>
                                </div>
                                <br />
                                <select id="TimeZoneID" name="TimeZoneID" class="form-select" style="max-width: 100%; width: 350px;" onchange="SetCurrentTime()" required>
                                    <%foreach (var timezone in (Monnit.TimeZone.LoadByRegion(tz.Region)))
                                        {
                                            string currentTime = timezone.CurrentTimeWithName.Split('|')[1];
                                            bool containsGMT = timezone.DisplayName.Contains("GMT");
                                            string displayText = timezone.DisplayName;

                                            if (containsGMT)
                                            {
                                                string gmtOffeset = " " + timezone.DisplayName.Split(')')[0] + ")";
                                                displayText = timezone.TimeZoneIDString + gmtOffeset;

                                                if (timezone.TimeZoneIDString == "GMT Standard Time")
                                                {
                                                    displayText = "GMT Standard Time Observes BST" + gmtOffeset;
                                                }
                                            }
                                    %>

                                    <option data-currenttime="<%:currentTime%>" value="<%:timezone.TimeZoneID%>" <%: timezone.DisplayName == tz.DisplayName ? "selected" : "" %>><%:displayText %></option>
                                    <%}%>
                                </select>
                            </div>
                            <div class="form-group aSettings">
                                <div class="  aSettings__title ps-0">
                                    <b><span id="currentTime"></span></b>
                                </div>
                            </div>
                        </div>

                        <%if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CurrentCustomer.CanAssignParent(Model.AccountID))
                            {%>
                        <div class="row">
                            <a class="btn btn-primary" href="/Settings/AssignParentSearch/<%:Model.AccountID %>" title="<%: Html.TranslateTag("Settings/AccountEdit|Assign Parent", "Assign Parent")%>">
                                <span><%: Html.TranslateTag("Settings/AccountEdit|Assign Parent", "Assign Parent")%></span>
                                <span><%=Html.GetThemedSVG("add") %></span>
                            </a>
                        </div>
                        <%} %>

                        <div class="">
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3">
                                    <%: Html.TranslateTag("Address","Address") %>
                                </div>
                                <div class="col sensorEditFormInput">
                                    <input type="text" id="PrimaryAddress.Address" class="form-control user-dets" name="PrimaryAddress.Address" value="<%= Model.PrimaryAddress.Address %>" />
                                    <div class="editor-error">
                                        <%: Html.ValidationMessageFor(model => model.PrimaryAddress.Address)%>
                                    </div>
                                </div>
                            </div>
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3">
                                    <%: Html.TranslateTag("Address","Address") %> 2:
                                </div>
                                <div class="col sensorEditFormInput">
                                    <input type="text" id="PrimaryAddress.Address2" class="form-control user-dets" name="PrimaryAddress.Address2" value="<%= Model.PrimaryAddress.Address2 %>" />
                                    <div class="editor-error">
                                        <%: Html.ValidationMessageFor(model => model.PrimaryAddress.Address2)%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3">
                                <%: Html.TranslateTag("Settings/AccountEdit|City","City") %>
                            </div>
                            <div class="col sensorEditFormInput">
                                <input type="text" id="PrimaryAddress.City" class="form-control user-dets" name="PrimaryAddress.City" value="<%= Model.PrimaryAddress.City %>" />
                                <div class="editor-error">
                                    <%: Html.ValidationMessageFor(model => model.PrimaryAddress.City)%>
                                </div>
                            </div>
                        </div>
                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3">
                                <%: Html.TranslateTag("Settings/AccountEdit|State","State") %>
                            </div>
                            <div class="col sensorEditFormInput">
                                <input type="text" id="PrimaryAddress.State" class="form-control user-dets" name="PrimaryAddress.State" value="<%= Model.PrimaryAddress.State %>" />
                                <div class="editor-error">
                                    <%: Html.ValidationMessageFor(model => model.PrimaryAddress.State)%>
                                </div>
                            </div>
                        </div>
                        <div class="">
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3">
                                    <%: Html.TranslateTag("Settings/AccountEdit|Postal Code","Postal Code") %>
                                </div>
                                <div class="col sensorEditFormInput">
                                    <input type="text" id="PrimaryAddress.PostalCode" class="form-control user-dets" name="PrimaryAddress.PostalCode" value="<%= Model.PrimaryAddress.PostalCode %>" />
                                    <div class="editor-error">
                                        <%: Html.ValidationMessageFor(model => model.PrimaryAddress.PostalCode)%>
                                    </div>
                                </div>
                            </div>
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3">
                                    <%: Html.TranslateTag("Settings/AccountEdit|Country","Country") %>
                                </div>
                                <div class="col sensorEditFormInput">
                                    <input type="text" id="PrimaryAddress.Country" class="form-control user-dets" name="PrimaryAddress.Country" value="<%= Model.PrimaryAddress.Country %>" />
                                    <div class="editor-error">
                                        <%: Html.ValidationMessageFor(model => model.PrimaryAddress.Country)%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3">
                                <%: Html.TranslateTag("Settings/AccountEdit|Recovery Email","Recovery Email") %>
                            </div>
                            <div class="col sensorEditFormInput">
                                <input type="text" id="RecoveryEmail" class="form-control user-dets" name="RecoveryEmail" value="<%= Model.RecoveryEmail %>" />
                                <div class="editor-error">
                                    <%: Html.ValidationMessageFor(model => model.RecoveryEmail)%>
                                </div>
                            </div>
                        </div>

                        <%if (MonnitSession.CustomerCan("Account_Set_Advertiser") && MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
                            { %>
                        <div class="row sensorEditForm">

                            <div class="col-12 col-md-3 checkbox-wrapper-65" style="display: flex; align-items: center; gap: 20px; width: 100%;">

                                <label for="IsAdvertiser">
                                    <input type="checkbox" id="IsAdvertiser" name="IsAdvertiser" <%=Model.IsAdvertiser ? "checked=checked" : ""%> value="true">
                                    <span class="cbx">
                                        <svg width="12px" height="11px" viewBox="0 0 12 11">
                                            <polyline points="1 6.29411765 4.5 10 11 1"></polyline>
                                        </svg>
                                        <input name="IsAdvertiser" type="hidden" value="false">
                                    </span>
                                </label>
                                <span><%: Html.TranslateTag("Settings/AccountEdit|Is Advertiser","Is Advertiser") %></span>
                            </div>


                        </div>


                        <%}
                            else
                            {%>
                        <%: Html.HiddenFor(model => model.IsAdvertiser) %>
                        <%} %>
                        <%if (MonnitSession.CustomerCan("Support_Advanced"))
                            { %>
                        <div class="row sensorEditForm">

                            <div class="col-12 col-md-3 checkbox-wrapper-65" style="display: flex; align-items: center; gap: 20px; width: 100%;">

                                <label for="IsCFRCompliant">
                                    <input type="checkbox" id="IsCFRCompliant" name="IsCFRCompliant" <%=Model.IsCFRCompliant ? "checked=checked" : ""%> value="true">
                                    <span class="cbx">
                                        <svg width="12px" height="11px" viewBox="0 0 12 11">
                                            <polyline points="1 6.29411765 4.5 10 11 1"></polyline>
                                        </svg>
                                        <input name="IsCFRCompliant" type="hidden" value="false">
                                    </span>
                                </label>
                                <span><%: Html.TranslateTag("Settings/AccountEdit|Is CFR Compliant","Is CFR Compliant") %></span>
                            </div>


                        </div>

                        <%}
                            else
                            {%>
                        <%: Html.HiddenFor(model => model.IsCFRCompliant) %>
                        <%} %>

                        <%if (MonnitSession.CustomerCan("Support_Advanced"))
                            { %>
                        <div class="row sensorEditForm">

                            <div class="col-12 col-md-3 checkbox-wrapper-65" style="display: flex; align-items: center; gap: 20px; width: 100%;">

                                <label for="IsHxEnabled">
                                    <input type="checkbox" id="IsHxEnabled" name="IsHxEnabled" <%=Model.IsHxEnabled ? "checked=checked" : ""%> value="true">
                                    <span class="cbx">
                                        <svg width="12px" height="11px" viewBox="0 0 12 11">
                                            <polyline points="1 6.29411765 4.5 10 11 1"></polyline>
                                        </svg>
                                        <input name="IsHxEnabled" type="hidden" value="false">
                                    </span>
                                </label>
                                <span><%: Html.TranslateTag("Settings/AccountEdit|Is HX Enabled","Is HX Enabled") %></span>
                            </div>

                        </div>

                        <div class="row sensorEditForm">

                            <div class="col-12 col-md-3 checkbox-wrapper-65" style="display: flex; align-items: center; gap: 20px; width: 100%;">

                                <label for="HideData">
                                    <input type="checkbox" id="HideData" name="HideData" <%=Model.HideData ? "checked=checked" : ""%> value="true">
                                    <span class="cbx">
                                        <svg width="12px" height="11px" viewBox="0 0 12 11">
                                            <polyline points="1 6.29411765 4.5 10 11 1"></polyline>
                                        </svg>
                                        <input name="HideData" type="hidden" value="false">
                                    </span>
                                </label>
                                <span><%: Html.TranslateTag("Settings/AccountEdit|Hide Sensor Data","Hide Sensor Data") %></span>
                            </div>


                        </div>
                        <%}
                            else
                            {%>
                        <%: Html.HiddenFor(model => model.IsHxEnabled) %>
                        <%: Html.HiddenFor(model => model.HideData) %>
                        <%} %>

                        <div class="row sensorEditForm" style="margin-bottom: 20px;">

                            <div class="col-12 col-md-3 checkbox-wrapper-65" style="display: flex; align-items: center; gap: 20px; width: 100%;">
                                <label for="AllowCertificateNotifications">
                                    <input type="checkbox" id="AllowCertificateNotifications" name="AllowCertificateNotifications" <%=Model.AllowCertificateNotifications ? "checked=checked" : ""%> value="true">
                                    <span class="cbx">
                                        <svg width="12px" height="11px" viewBox="0 0 12 11">
                                            <polyline points="1 6.29411765 4.5 10 11 1"></polyline>
                                        </svg>
                                        <input name="AllowCertificateNotifications" type="hidden" value="false">
                                    </span>
                                </label>
                                <span><%: Html.TranslateTag("Settings/AccountEdit|Notify on Certificate Expiration") %></span>
                            </div>

                        </div>

                        <%if (MonnitSession.CustomerCan("Unlock_User"))
                            { %>
                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3">
                                <%: Html.TranslateTag("Settings/AccountEdit|Max Failed Logins","Max Failed Logins:") %>
                            </div>
                            <div class="col sensorEditFormInput">
                                <input type="text" class="form-control user-dets" name="MaxFailedLogins" id="MaxFailedLogins" value="<%=Model.MaxFailedLogins %>" />
                            </div>
                        </div>
                        <%}
                            else
                            {%>
                        <%: Html.HiddenFor(model => model.MaxFailedLogins) %>
                        <%} %>



                        <% if (MonnitSession.CustomerCan("Support_Advanced"))
                            { %>
                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3">
                                <%: Html.TranslateTag("Settings/AccountEdit|Tags","Tags:") %>
                            </div>
                            <div class="col sensorEditFormInput">
                                <input type="text" class="form-control user-dets" name="Tags" placeholder="CFR21|MEDIUM|BETA" id="Tags" value="<%=Model.Tags %>" title=" <%: Html.TranslateTag("Settings/AccountEdit|Pipe delimited values to restrict visibility. example: CFR|HSB|NewAccounts","Pipe delimited values to restrict visibility. example: CFR|HSB|NewAccounts") %>" />
                            </div>
                        </div>
                        <%} %>
                    </div>

                    <%: Html.HiddenFor(model => model.CreateDate) %>
                    <%: Html.HiddenFor(model => model.IndustryTypeID) %>
                    <%: Html.HiddenFor(model => model.BusinessTypeID) %>
                    <%: Html.HiddenFor(model => model.PurchaseLocation) %>
                    <%: Html.HiddenFor(model => model.RetailAccountID) %>
                    <%: Html.HiddenFor(model => model.EULADate) %>
                    <%: Html.HiddenFor(model => model.EULAVersion) %>
                    <%: Html.HiddenFor(model => model.CustomField_01) %>
                    <%: Html.HiddenFor(model => model.CustomField_02) %>
                    <%: Html.HiddenFor(model => model.CustomField_03) %>
                    <%: Html.HiddenFor(model => model.CustomField_04) %>
                    <%: Html.HiddenFor(model => model.CustomField_05) %>
                    <div style="clear: both;"></div>
                    <div class="mb-4">
                        <%if (MonnitSession.IsCurrentCustomerMonnitAdmin)
                            { %>

                        <div class="formBody">
                            <%foreach (AccountPermissionType PermissionType in AccountPermissionType.LoadAll().OrderBy(pt => pt.CorporateSees)/*.ThenBy(pt => pt.ResellerSees)*/.ThenBy(pt => pt.Description.Trim()))
                                {
                                    string Name = string.Format("Permission_{0}", PermissionType.Name.Replace(" ", "_"));
                                    if (!PermissionType.CanEdit(MonnitSession.CurrentCustomer.IsAdmin, /*MonnitSession.IsCurrentCustomerReseller,*/ MonnitSession.IsCurrentCustomerMonnitAdmin))
                                        continue;

                                    AccountPermission Permission = new AccountPermission();
                                    foreach (AccountPermission Perm in Model.Permissions)
                                    {
                                        if (Perm.AccountPermissionTypeID == PermissionType.AccountPermissionTypeID)
                                            Permission = Perm;
                                    }%>
                            <div class="row sensorEditForm" style="margin-top: 20px;">

                                <div class="col-12 col-md-3 checkbox-wrapper-65" style="display: flex; align-items: center; gap: 20px; width: 100%;">
                                    <label for="<%:Name %>">
                                        <input type="checkbox" id="<%:Name %>" name="<%:Name %>" <%=Permission.Can ? "checked=checked" : ""%>>
                                        <span class="cbx">
                                            <svg width="12px" height="11px" viewBox="0 0 12 11">
                                                <polyline points="1 6.29411765 4.5 10 11 1"></polyline>
                                            </svg>
                                        </span>
                                    </label>
                                    <span><%: Html.TranslateTag("Settings/AccountEdit|" + PermissionType.Description,PermissionType.Description) %></span>



                                    <div class="d-flex align-items-center">

                                        <%if (PermissionType.RequiresInfo)
                                            {%>

                                        <input class="form-control form-control-sm user-dets" type="text" style="width: 80px;" id='<%:Name + "_Info"%>' name="<%:Name + "_Info"%>" value="<%:Permission.Info %>" />

                                        <%}%>
                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                            </div>



                            <div class="row sensorEditForm">
                                <%if (CustomerPermissionType.Find(PermissionType.Name) != null)
                                    {%>
                                <div class="col-12 col-md-3 checkbox-wrapper-65" style="display: flex; align-items: center; gap: 20px; width: 100%;">

                                    <label for="<%:Name %>_Override">
                                        <input type="checkbox" id="<%:Name %>_Override" name="<%:Name %>_Override" <%:Permission.OverrideCustomerPermission ? "checked='checked'" : ""%>>
                                        <span class="cbx">
                                            <svg width="12px" height="11px" viewBox="0 0 12 11">
                                                <polyline points="1 6.29411765 4.5 10 11 1"></polyline>
                                            </svg>
                                        </span>
                                    </label>
                                    <span><%: Html.TranslateTag("Settings/AccountEdit|Override","Override") %></span>
                                </div>

                                <%}%>
                                <hr class="my-2" />
                            </div>
                            <%  }%>
                        </div>
                        <%} %>
                        <div style="clear: both;"></div>
                        <div class="col-12"></div>
                        <div class="form-group text-end">
                            <button id="acctSave" type="button" onclick="onSaveClick();" value="<%: Html.TranslateTag("Save","Save") %>" class="btn btn-primary">
                                <%:Html.TranslateTag("Settings/AccountEdit|Save","Save")%>
                            </button>
                            <button class="btn btn-primary" id="saving" style="display: none;" type="button" disabled>
                                <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                <%:Html.TranslateTag("Settings/AccountEdit|Saving...","Saving...")%>
                            </button>
                        </div>
                    </div>
                </form>
            </div>

            <div class="settingsContainer__aDetails px-0">
                <%Html.RenderPartial("_AccountDetails", Model); %>
                <!-- Account Parent Edit -->
                <%--<%if (MonnitSession.IsCurrentCustomerMonnitAdmin)// || MonnitSession.CurrentCustomer.Account.IsReseller)
                            { %>
                            <div class="rule-card_container w-100">
                                <div class="x_title">
                                    <div class="card_container__top__title" style="overflow: unset;"><%: Html.TranslateTag("Settings/AccountEdit|Account Parent Edit","Account Parent Edit") %></div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <div class="form-group">
                                        <div class="col-12">
                                            <%string parentName = Html.TranslateTag("None", "None");
                                                Account CurrentAccount = Account.Load(Model.RetailAccountID);
                                                if (CurrentAccount != null)
                                                {
                                                    parentName = CurrentAccount.AccountNumber;
                                                }%>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="form-group">
                                        <div class="aSettings__title ps-0" style="font-weight: bold; margin-top: 5px;">
                                            <%: Html.TranslateTag("Settings/AccountEdit|Account Parent","Parent Account is ...") %>
                                        </div>
                                        <div class="aSettings__input">
                                            <select style="width: 250px;" id="accountDropDown" class="form-select">
                                                <option value="<%=Model.RetailAccountID %>"><%=parentName%></option>
                                                <%foreach (Account account in Account.LoadResellers(MonnitSession.CurrentCustomer.CustomerID, 100).Where(s => s.AccountID != Model.RetailAccountID))
                                                    { %>
                                                <option value="<%=account.AccountID %>"><%=account.AccountNumber %></option>
                                                <%} %>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="btnRow_right">
                                        <button type="button" class="btn btn-primary" id="AcctParentSave" title="<%: Html.TranslateTag("Settings/AccountEdit|Save as Parent Account","Save as Parent Account") %>">
                                            <%: Html.TranslateTag("Save","Save")%>
                                        </button>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                            </div>
                        <%} %>--%>
                <!-- End Account Parent Edit -->
            </div>
        </div>
    </div>


    <script type="text/javascript">


        function onSaveClick() {
            $('#acctSave').hide();
            $('#saving').show();
            /*            console.log($('#accountForm').serialize());*/
            $.post('/Settings/AccountEdit/<%:Model.AccountID %>', $('#accountForm').serialize(), function (data) {
                if (data != "Success") {
                    toastBuilder(data);
                    showSimpleMessageModal(data);
                }
                toastBuilder(data);
                $('#acctSave').show();
                $('#saving').hide();

            });
        }

        function selectRegion(region) {
            $.post('/Account/GetTimeZonesWithCurrentTime/', { Region: region }, function (data) {
                $('#TimeZoneID').empty();
                $('#TimeZoneID').show();

                $.each(data, function (value) {
                    console.log(data[value])
                    var splitvals = data[value].split("|");
                    var text = splitvals[1];
                    var selection = splitvals[0];
                    var currentTime = splitvals[2];
                    var displayText = "";
                    var opt = document.createElement('option');

                    var containsGMT = text.includes("GMT");

                    if (containsGMT) {
                        var textSplit = text.split(")");
                        var gmtOffset = " " + textSplit[0] + ")";
                        var timezoneName = (textSplit.length <= 2) ? textSplit[1] : textSplit[1] + ")";

                        displayText = timezoneName + gmtOffset;
                        opt.text = displayText;
                        if (opt.text === "GMT Standard Time (GMT)") {
                            opt.text = "GMT Standard Time Observes BST (GMT)";
                        }
                    }
                    else {
                        console.log("NOT GMT:",opt)
                        opt.text = text;
                    }
                    opt.value = selection;
                    opt.setAttribute('data-currentTime', currentTime);

                    var tzselector = $('#TimeZoneID').get(0);
                    tzselector.add(opt, null);
                });

                SetCurrentTime();
            });
        }

        $(document).ready(function () {

            SetCurrentTime();

            var alreadyparent = "<%: Html.TranslateTag("Settings/AccountEdit|This account is already set as parent") %>";
            var areyousure = "<%: Html.TranslateTag("Settings/AccountEdit|Are you sure you want to update the parent account","Are you sure you want to update the parent account") %>";



            createLookup('CustomerIDLookup', '/Lookup/AccountCustomer', { extraParams: { accountID:<%:Model.AccountID%>} }, 'PrimaryContactID', '/Lookup/AccountCustomerID', '&accountID=' + <%:Model.AccountID%>);

            //-- Hightlight & Focus on input field if directed -- 
            var url = window.location.href;
            if (url.indexOf("#") > -1)
                var hash = url.substring(url.indexOf("#") + 1);

            //$('#' + hash).focus().effect('highlight', { color: '#FDFF2A' }, 10000);


            $('#AcctParentSave').click(function () {
                var acctnumber = $('#accountDropDown').val();

                if (acctnumber == '<%=Model.RetailAccountID%>') {
                    showSimpleMessageModal("<%=Html.TranslateTag("This account is already set as parent")%>");
                } else {
                    var url = "/Account/UpdateAccountParent?accountID=" + '<%=Model.AccountID%>' + "&parentID=" + acctnumber;
                    $.get(url, function (data) {
                        if (data == "Success") {
                            window.location.reload();
                        }
                        else {
                            showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                        }
                    });
                }
            });
        });

        function changePremiumDate(accountID) {
            $('#premiumUntill_' + accountID).hide();
            $('#workingChangeAccount_' + accountID).show();
            $.post("/Account/ChangePremiumDate/" + accountID, { date: $('#premiumUntill_' + accountID).val() }, function (data) {
                if (data != "Success!") {
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }

                $('#modalCancel').hide();

                var color = '#FFBFBF';
                if (Date.parse($('#premiumUntill_' + accountID).val()) > new Date()) {
                    color = '#C5FFBF';
                }

                $('#premiumUntill_' + accountID).css('backgroundColor', color).show();
                $('#workingChangeAccount_' + accountID).hide();
            });
        }

        function progressNotificationSelect(field) {
            let input = document.getElementById(field);
            input.classList.add('selectMaintenance');
            input.scrollIntoView();
            input.focus();
        }

        //Remove the focus() class for selected inputs
        $('input').on('blur', function () {
            $(this).removeClass('selectMaintenance');
        })

        //This function isnt called anywhere. Commenting for now.

<%--        function removeSystemHelp(s) {
            console.log(s);
            $.post("/Overview/ClearSystemHelp", { id: s }, function (data) {
                if (data == "Success") {
                    toastBuilder("Success")
                } else {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
            });
        }--%>

        function SetCurrentTime() {
            var currentTime = $('#TimeZoneID').find(":selected").attr('data-currentTime');
            $('#currentTime').text(currentTime);
        }

    </script>
    <style>
        .checkbox-wrapper-65 *,
        .checkbox-wrapper-65 ::after,
        .checkbox-wrapper-65 ::before {
            box-sizing: border-box;
        }

        .checkbox-wrapper-65 .cbx {
            position: relative;
            display: block;
            float: left;
            width: 18px;
            height: 18px;
            border-radius: 4px;
            background-color: #606062;
            background-image: linear-gradient(#474749, #606062);
            box-shadow: inset 0 1px 1px rgba(255,255,255,0.15), inset 0 -1px 1px rgba(0,0,0,0.15);
            transition: all 2s ease-in-out;
        }

            .checkbox-wrapper-65 .cbx svg {
                position: absolute;
                top: 3px;
                left: 3px;
                fill: none;
                stroke-linecap: round;
                stroke-linejoin: round;
                stroke: #fff;
                stroke-width: 2.3;
                stroke-dasharray: 17;
                stroke-dashoffset: 17;
                transform: translate3d(0, 0, 0);
            }

            .checkbox-wrapper-65 .cbx + span {
                float: left;
                margin-left: 6px;
            }

        .checkbox-wrapper-65 {
            user-select: none;
        }

            .checkbox-wrapper-65 label {
                display: inline-block;
                cursor: pointer;
            }

            .checkbox-wrapper-65 input[type="checkbox"] {
                display: none;
                visibility: hidden;
            }

                .checkbox-wrapper-65 input[type="checkbox"]:checked + .cbx {
                    background-color: #606062;
                    background-image: linear-gradient(var(--primary-color), var(--primary-color-hover));
                }

                    .checkbox-wrapper-65 input[type="checkbox"]:checked + .cbx svg {
                        stroke-dashoffset: 0;
                        transition: all 0.15s ease;
                    }
    </style>
</asp:Content>
