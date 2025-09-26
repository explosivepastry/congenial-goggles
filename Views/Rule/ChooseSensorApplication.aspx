<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ChooseSensorApplication
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <div class="x_panel col-12 mt-4 shadow-sm rounded d-flex flex-column" style="max-width: 500px;">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <%: Html.TranslateTag("Choose a Condition","Choose a Condition")%>
                </div>
            </div>
            
        </div>
    </div>
</asp:Content>
