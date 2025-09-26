<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage<List<SMSCarrier>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit SMS Carriers List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 <div class="formtitle">Application List</div>
    <%using (Html.BeginForm()) {
        %>
    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
    <div class="formBody" style="margin-top:10px;">
     <input type="hidden" name="currentThemeAccountID" id="currentThemeAccountID" value="<%: MonnitSession.CurrentTheme.AccountThemeID %>" />
        <div style="clear:both;"></div>
 
        <div style="float:right; font-size:small;">(Selecting none will default to show all)</div>
        <h3>Select Applications to Display</h3>
        <table style="width: 100%">
             <% 
          var appList = SMSCarrier.LoadAll().Where(accttheme => { return accttheme.AccountThemeID == long.MinValue || accttheme.AccountThemeID == MonnitSession.CurrentTheme.AccountThemeID; }).OrderBy(ma => ma.SMSCarrierName);            
                foreach (var item in Model)
               { 
                    
                    bool check = appList.Where(m=>m.SMSCarrierID == item.SMSCarrierID ).Count() > 0;
                
                    %>
          
            <tr>
                <td>
                    <%: Html.Label(item.SMSCarrierName) %>
                </td>
                <td>                   
                    <input type="checkbox" id="SMSCarrierID_<%: item.SMSCarrierID %>" name="SMSCarrierID_<%: item.SMSCarrierID %>" value="<%:item.SMSCarrierID %>" <%: check ? "checked" : ""  %>/>
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

            
        });

    </script>

</asp:Content>
