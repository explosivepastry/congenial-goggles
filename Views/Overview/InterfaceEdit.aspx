<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Sensor>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SensorEdit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% 
        Dictionary<string, object> dic = new Dictionary<string, object>();
        if (!Model.CanUpdate)
        {
            dic.Add("disabled", "disabled");
            ViewData["disabled"] = true;
        }
        ViewData["HtmlAttributes"] = dic;

        Monnit.Gateway gateway = Monnit.Gateway.LoadBySensorID(Model.SensorID);
    %>

    <%Html.RenderPartial("SensorLink", Model); %>

    <div class="col-md-12 col-12">
        <div class="x_panel powertour-hook shadow-sm rounded" id="hook-seven">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <div class="col-6">
                        <h2 style="overflow: unset; font-weight: bold;"><%: Model.MonnitApplication.ApplicationName%> <%: Html.TranslateTag("Overview/SensorEdit|Interface Settings"," Interface Settings")%></h2>
                    </div>
                    <div class="col-6 text-end media_desktop">
                        <a href="/Overview/SensorEdit/<%:Model.SensorID%>" class="btn btn-secondary btn-sm">
                            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Sensor Settings","Sensor Settings")%>
                            <svg xmlns="http://www.w3.org/2000/svg" width="10.425" height="10.425" viewBox="0 0 10.425 10.425" style="margin-left: 10px;">
                                <path id="Path_725" fill="#fff" data-name="Path 725" d="M5.313,2,1.6,5.814,0,10.425l4.611-1.5L8.32,5.213Zm4.711-.3L8.721.4a1.215,1.215,0,0,0-1.8,0l-1.1,1.1L8.821,4.611l1.2-1.2a1.271,1.271,0,0,0,.4-.9A1.237,1.237,0,0,0,10.024,1.7Z"></path>
                            </svg>
                        </a>
                    </div>
                </div>
            </div>
            <br />
            <%if (Model.IsPoESensor)
                {%>
            <%Html.RenderPartial("_InterfaceEditForm", Model); %>
            <%}
                else if (Model.IsLTESensor)
                {%>
            <%Html.RenderPartial("_InterfaceEditFormLTE", Model); %>
            <%}%>
        </div>
    </div>


</asp:Content>
