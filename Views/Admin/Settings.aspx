<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Setup Settings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="fullForm">
    	<div class="formtitle">Administrative Settings</div>
            <div>
               <ul class="listNoBul">    
                   <% Html.RenderPartial("AdminSettingsList"); %>
               </ul>   
            </div>

    	<div class="buttons"></div>
    </div>
</asp:Content>
