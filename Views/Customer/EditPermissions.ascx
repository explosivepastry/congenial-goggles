<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Customer>" %>

<style>
  /*.ui-tabs-vertical { width: 55em; }*/
  .ui-tabs-vertical .ui-tabs-nav { padding: .2em .1em .2em .2em; float: left; width: 12em; }
  .ui-tabs-vertical .ui-tabs-nav li { clear: left; width: 100%; border-bottom: 1px solid #cccccc !important; border-right-width: 0 !important; margin: 0 -1px .2em 0; }
  .ui-tabs-vertical .ui-tabs-nav li a { display:block; padding: 12px; width:135px; }
  .ui-tabs-vertical .ui-tabs-nav li.ui-tabs-active { padding-bottom: 0; padding-right: .1em; border-right-width: 1px; border-bottom-width: 2px; }
  .ui-tabs-vertical .ui-tabs-panel { padding: 1em; float: left; width: 690px; min-height: 272px;}
</style>

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

    using (Html.BeginForm())
    {%>

    <div class="formtitle">Permissions</div>
    <div class="formBody">


        <%: Html.ValidationSummary(true) %>
        <div id="tabs">
            <ul>
            <%foreach (var Category in PermissionTypeCategories) {
                
                %><li><a href="#tabs-<%: Category%>"><%: Category.Replace("_", " ")%></a></li>
            <%}%>
            </ul>

<%foreach (var Category in PermissionTypeCategories) {
    List<CustomerPermissionType> TypeList = PermissionTypeList.Where(cpt => cpt.Category == Category && cpt.Name != "Sensor_Heartbeat_Restriction").ToList();
    List<CustomerPermission> PermissionList = new List<CustomerPermission>();
                  
                  
    foreach (var PermissionType in TypeList.OrderBy(t=>t.Description)) {
        string Name = string.Format("Permission_{0}", PermissionType.Name.Replace(" ", "_"));
        
                          
        if (PermissionType.NetworkSpecific)
        {
            foreach (CSNet Network in ((List<CSNet>)ViewData["CSNetList"]).OrderBy(n=>n.Name))
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
                  
                  
            <div id="tabs-<%: Category%>">
                <div style="float: left;  width:300px;">
                <%bool Split = false;
                  int Cnt = PermissionList.Count;
                  int Current = 0;
                foreach (var Permission in PermissionList) {
                    if (!Split && Cnt > 6 && Current >= Math.Floor(.5 + Cnt / 2.0))
                    {
                        Split = true;
                    %>
                    
                    <div style="clear: both;"></div>
                </div>
                <div style="float: left;  width:300px;">
                    <%}

                    Current++;
                    
                    string Name = string.Format("Permission_{0}", Permission.Type.Name.Replace(" ", "_"));
                          
                    if (Permission.Type.NetworkSpecific)
                    {
                        CSNet Network = ((List<CSNet>)ViewData["CSNetList"]).Where(net => { return net.CSNetID == Permission.CSNetID; }).FirstOrDefault();
                        if (Network == null)
                            continue;
                        %>
                    
                    <div style="margin:10px 20px;">
                        <input type="checkbox" <%:Permission.IsOverriden ? "disabled=disabled" : ""%> style="margin: 0px 15px 0px 0px;" id="<%:Name + "_Net_" + Network.CSNetID%>" name="<%:Name + "_Net_" + Network.CSNetID%>" <%:Permission.Can ? "checked='checked'" : ""%> /><label for="<%:Name%>"><%: Permission.Type.Description%> <%: Network.Name%></label>
                    </div>
                        
                    <%} else {%>
                    
                    <div style="margin:10px 20px;">
                        <input type="checkbox" <%:Permission.IsOverriden ? "disabled=disabled" : ""%> style="margin: 0px 15px 0px 0px;" id="<%:Name%>" name="<%:Name%>" <%:Permission.Can ? "checked='checked'" : ""%> /><label for="<%:Name%>"><%: Permission.Type.Description%></label>
                    </div>

                    <%-- <%if (Permission.Type.RequiresInfo){%>
                    <div class="editor-field ">
                        <input type="text" id="<%:Name + "_Info"%>" name="<%:Name + "_Info"%>" value="<%:Permission.Info%>" />
                    </div>
                    <%}--%>
                    <%}
                }%>
                    <div style="clear: both;"></div>
                </div>
            </div>
<%}%>

        </div>
    </div>

    <div class="buttons">
        <input type="button" onclick="postForm($(this).closest('form'), function (data) { if (data == 'Success') { $('.refreshPic:visible').click(); } });" value="Save Permissions" class="bluebutton" />
        <div style="clear: both;"></div>
    </div>

<script type="text/javascript">
    $(document).ready(function () {
        //$("#tabs").tabs();
        $("#tabs").tabs().addClass("ui-tabs-vertical ui-helper-clearfix");
        $("#tabs li").removeClass("ui-corner-top").addClass("ui-corner-left");

    });
</script>
<% } %>
