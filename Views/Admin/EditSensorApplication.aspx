<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage<List<SensorApplication>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit Site Configurations
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="formtitle">Application List</div>
    <%using (Html.BeginForm()) {
          var monnitappNamelist = MonnitApplication.LoadAll().OrderBy(ma=>ma.ApplicationName);%>
    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
    <div class="formBody" style="margin-top:10px;">
        Sensor Type: 
         <%: Html.DropDownList("ApplicationID", new SelectList(monnitappNamelist, "ApplicationID", "ApplicationName"), "Select Application Type" )%>
        <div style="clear:both;"></div>
 
        <div style="float:right; font-size:small;">(Selecting none will default to show all)</div>
        <h3>Select Applications to Display</h3>
        <table style="width: 100%">
             <% 
              var appList = ResellerToSensorApplication.LoadByResellerID(MonnitSession.CurrentCustomer.AccountID);            
                foreach (var item in Model)
               { 
                    bool check = appList.Where(m=>m.SensorApplicationID == item.SensorApplicationID).Count() > 0;
                
                    %>
          
            <tr>
                <td>
                    <%: Html.Label(item.Name) %>
                </td>
                <td>
                    <input type="checkbox" name="SensorAppData" value="<%: item.SensorApplicationID %>"  <%: check ? "checked" : "" %>/>
                </td>
            </tr>
            <%} %>
        
        </table>

    </div>
    
    <div class="buttons">
        <input type="submit" title="Save" class="bluebutton"/>
        <div style="clear: both;"></div>
    </div>
    <%} %>
    <script type="text/javascript" >
        $(document).ready(function () {



            $('#ApplicationID').change(function () {
                var monnitAppID = $('#ApplicationID').val();
                window.location.href = "/Admin/EditSensorApplication?ApplicationID=" + monnitAppID;

            });
        });

    </script>
</asp:Content>
