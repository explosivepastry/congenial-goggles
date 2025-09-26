<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Controllers.AdminController.MassEmailContact>" %>



    

    <%foreach (AccountThemeContact atc in Model.Contacts){ %>
    <p style=" max-height:10px; padding-left:40px; margin-top:0px; cursor:text;"><%= atc.FirstName + " " +  atc.LastName  + " - " + atc.Email %></p>   
    
 <%} %>

