<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<iMonnit.Models.DeviceInfoModel>" %>



<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SensorChart
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% 
        //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        Response.Cache.SetValidUntilExpires(false);
        Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
    %>

    <div class="container-fluid">
        <%Html.RenderPartial("_SensorLink", Model.Sensor); %>

            <div class="col-6 device_detailsRow__card" style="min-height: 10px !important;">
                <div class="x_panel shadow-sm rounded">
                    <div class="card_container__top ">
                        <div class="card_container__top__title">
                            <div class="hidden-xs">
                                <%: Html.TranslateTag("Support","Support")%>
                            &nbsp;
                            </div>
                        </div>
                    </div>
                    <div class="x_content">
                        <div class="card__container__body">
                            <div class="col-12 card_container__body__content">
                                <%Html.RenderPartial("_DeviceInfoSupport", Model.Sensor);%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </div>
</asp:Content>
