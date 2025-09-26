<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Customer>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    UserPermission
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <% List<CustomerPermissionType> PermissionTypeList = CustomerPermissionType.LoadAll().Where(pt =>
            {
                return (
                    pt.RetailAccountID == long.MinValue
                    || pt.RetailAccountID == MonnitSession.CurrentCustomer.AccountID
                    || pt.RetailAccountID == MonnitSession.CurrentCustomer.Account.RetailAccountID
                    ) && pt.CanEdit(MonnitSession.CurrentCustomer.IsAdmin, /*MonnitSession.IsCurrentCustomerReseller,*/ MonnitSession.IsCurrentCustomerMonnitAdmin);
            }).ToList();

            PermissionTypeList = PermissionTypeList.Where(pt =>
            {
                return (pt.Category != "CFR_Compliance" || Model.Account.IsCFRCompliant);
            }).ToList();

            IEnumerable<string> PermissionTypeCategories = PermissionTypeList.OrderBy(pt => pt.Category).Select(cata => cata.Category).Where(type => { return type != "SuperAdmin" || MonnitSession.IsCurrentCustomerMonnitSuperAdmin; }).Distinct();
        %>
        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
        <%Html.RenderPartial("UserLink", Model); %>

        <div class="rule-card_container w-100" style="margin-top:54px;">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <%: Html.TranslateTag("Settings/UserPermission|Permissions","User Permissions") %>
                          <span class="col-6" style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; color: #707070; font-size :small; margin-left: 20px">[<%= Model.FirstName %> <%= Model.LastName%>] - <%=Model.UserName%></span>
               </div>
            </div>

            <form action="/Settings/UserPermission/<%=Model.CustomerID%>" method="post">
                <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                <div class="checkbox-userP">
                    <div class="nav d-flex nav-pills " id="v-pills-tab" role="tablist" aria-orientation="vertical">
                        <%foreach (var Category in PermissionTypeCategories)
                            {
                        %>
                        <button class="nav-link my-2 <%= Category == PermissionTypeCategories.First() ? "active" : "" %>" id="v-pills-<%:Category%>-tab" data-bs-toggle="pill" data-bs-target="#v-pills-<%:Category%>" type="button" role="tab" aria-controls="v-pills-<%:Category%>" aria-selected="false">
                            <%:Html.TranslateTag("Settings/UserPermission|" + Category.Replace("_", " ").ToString(),Category.Replace("_", " ").ToString())%>
                        </button>
                        <% } %>
                    </div>

                    <div class="tab-content my-auto ms-5 my-auto" id="v-pills-tabContent">
                        <%foreach (var Category in PermissionTypeCategories)
                            { %>
                        <div class="tab-pane fade <%= Category == PermissionTypeCategories.First() ? "show active" : "" %>" id="v-pills-<%:Category%>" role="tabpanel" aria-labelledby="v-pills-<%:Category%>-tab">
                            <% List<CustomerPermissionType> TypeList = PermissionTypeList.Where(cpt => cpt.Category == Category && cpt.Name != "Sensor_Heartbeat_Restriction").ToList();
                                List<CustomerPermission> PermissionList = new List<CustomerPermission>();

                                foreach (var PermissionType in TypeList.OrderBy(t => t.Description))
                                {
                                    string Name = string.Format("Permission_{0}", PermissionType.Name.Replace(" ", "_"));
                                    if (PermissionType.NetworkSpecific)
                                    {
                                        foreach (CSNet Network in ((List<CSNet>)ViewData["CSNetList"]).OrderBy(n => n.Name))
                                        {
                                            CustomerPermission Perm = Model.Permissions.Where(p => { return p.CustomerPermissionTypeID == PermissionType.CustomerPermissionTypeID && p.CSNetID == Network.CSNetID; }).FirstOrDefault();
                                            if (Perm == null)
                                                Perm = new CustomerPermission() { CustomerPermissionTypeID = PermissionType.CustomerPermissionTypeID, CSNetID = Network.CSNetID };
                                            PermissionList.Add(Perm);
                                        }
                                    }
                                    else
                                    {
                                        CustomerPermission Perm = Model.Permissions.Where(p => { return p.CustomerPermissionTypeID == PermissionType.CustomerPermissionTypeID && (p.Type.Category != "SuperAdmin" || MonnitSession.IsCurrentCustomerMonnitSuperAdmin); }).FirstOrDefault();
                                        if (Perm == null)
                                            Perm = new CustomerPermission() { CustomerPermissionTypeID = PermissionType.CustomerPermissionTypeID };
                                        if (Name == "Permission_Customer_Create" && !Model.IsAdmin)
                                        {
                                            Perm.Can = false;
                                            Perm.IsOverriden = true;
                                        }
                                        PermissionList.Add(Perm);
                                    }
                                } %>

                            <div>
                                <%bool Split = false;
                                    int Cnt = PermissionList.Count;
                                    int Current = 0;
                                    foreach (var Permission in PermissionList)
                                    {
                                        if (!Split && Cnt > 6 && Current >= Math.Floor(.5 + Cnt / 2.0))
                                        {
                                            Split = true;
                                        }
                                        Current++;
                                        string Name = string.Format("Permission_{0}", Permission.Type.Name.Replace(" ", "_"));
                                        if (Permission.Type.NetworkSpecific)
                                        {
                                            CSNet Network = ((List<CSNet>)ViewData["CSNetList"]).Where(net => { return net.CSNetID == Permission.CSNetID; }).FirstOrDefault();
                                            if (Network == null)
                                                continue;
                                %>

                          
                                  <div class="col-12 col-md-3 checkbox-wrapper-65" style="display: flex; align-items: center; gap: 20px; width: 100%; margin-left:12px;">                                     
                                <label for="<%:Name + "_Net_" + Network.CSNetID%>">
                                    <input type="checkbox" id="<%:Name + "_Net_" + Network.CSNetID%>" class="LevyFee" name="<%:Name + "_Net_" + Network.CSNetID%>" onchange = "AutoCalculateMandateOnChange(this)"  <%:Permission.Can ? "checked='checked'" : ""%> >
                                    <span class="cbx">
                                        <svg width="12px" height="11px" viewBox="0 0 12 11">
                                            <polyline points="1 6.29411765 4.5 10 11 1"></polyline>
                                        </svg>                          
                                    </span>
                                </label>
						 <label style="font-weight: normal!important; text-overflow: ellipsis !important;" for="<%:Name%>">
                                        <%: Html.TranslateTag("Settings/UserPermission|" + Permission.Type.Description,Permission.Type.Description)%>
                                        <%= Network.Name%>
                                    </label>
                            </div>


                                <%}
                                    else
                                    {%>



                                 <div class="col-12 col-md-3 checkbox-wrapper-65" style="display: flex; align-items: center; gap: 20px; width: 100%; margin-left:12px;">                                     
                                <label for="<%:Name%>">
                                    <input type="checkbox" id="<%:Name%>" class="LevyFee"  <%:Permission.IsOverriden ? "disabled=disabled" : ""%> name="<%:Name%>" onchange = "AutoCalculateMandateOnChange(this)"  <%:Permission.Can ? "checked='checked'" : ""%> >

                                    <span class="cbx">
                                        <svg width="12px" height="11px" viewBox="0 0 12 11">
                                            <polyline points="1 6.29411765 4.5 10 11 1"></polyline>
                                        </svg>                          
                                    </span>
                                </label>
						 <label style="font-weight: normal!important; text-overflow: ellipsis !important;" for="<%:Name%>">
                                    <%: Html.TranslateTag("Settings/UserPermission|" + Permission.Type.Description,Permission.Type.Description)%>
                                    </label>
                            </div>
                                

                                <%}
                                    }%>
                            </div>
                        </div>
                        <% } %>
                    </div>
                </div>

                <div class="text-end mt-2">
                    <button value="<%:Html.TranslateTag("Settings/UserPermission|Save","Save")%>" type="submit" onclick="$(this).hide();$('#saving').show();" class="btn btn-primary">
                        <%:Html.TranslateTag("Settings/UserPermission|Save","Save")%>
                    </button>
                    <button class="btn btn-primary" id="saving" style="display: none;" type="button" disabled>
                        <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                        <%: Html.TranslateTag("Settings/UserPermission|Saving","Saving")%>...
                    </button>
                </div>
            </form>
        </div>
    </div>
    
    <script type="text/javascript">

        $(document).ready(function () {
            $('#saveMessage').html('<%=ViewBag.saveMessage%>');
            $("#tabs").tabs().addClass("ui-tabs-vertical ui-helper-clearfix");
            $("#tabs li").removeClass("ui-corner-top").addClass("ui-corner-left");
        });
        function goBack() {
            window.history.back();
        }

        $('.nav-link').click(function () {
            $('#choose').hide();
        })

  

    </script>

    <style>
        @media only screen and (max-width: 700px) {
            .tab-pane {
                column-count:1;
            }
          }

        @media only screen and (min-width: 700px) and (max-width: 1100px) {
            .tab-pane {
                column-count:2;
            }
          }
         
        @media only screen and (min-width: 1100px) {
            .tab-pane {
                column-count:3;
            }
          }
    </style>

   

<script type="text/javascript">

    function AutoCalculateMandateOnChange(element) {
        element.checked ? document.getElementsByClassName("LevyFee").disabled = true : document.getElementsByClassName("LevyFee").disabled = false;
    }

    if (`<%=ViewBag.saveMessage%>`.length > 1) {
                toastBuilder(`<%=ViewBag.saveMessage%>`);
            }

</script>

</asp:Content>
