<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<List<Monnit.AccountTheme>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminAccountTheme
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
    <div class="formtitle">
    <div id="MainTitle" style="display: none;"></div>

        <div>
            <div class="" style="margin-top: 5px;">
                <%if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.IsCurrentCustomerAccountThemeAdmin)
                    { %>
                <div class="top-add-btn-row media_desktop">
                    <a class="btn btn-primary" href="/Settings/AdminAccountThemeCreate">
                        <%=Html.GetThemedSVG("add") %> 
                        <span class="ms-1"><%: Html.TranslateTag("Settings/AdminAccountTheme|Add Portal","Add Portal")%></span>
                    </a>
                </div>

                <div class="bottom-add-btn-row media_mobile">
                    <a class="add-btn-mobile" href="/Settings/AdminAccountThemeCreate">
                        <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="16" height="15.999" viewBox="0 0 16 15.999">
                            <defs>
                                <clipPath id="clip-path">
                                    <rect width="16" height="15.999" fill="none" />
                                </clipPath>
                            </defs>
                            <g id="Symbol_14_32" data-name="Symbol 14 – 32" clip-path="url(#clip-path)">
                                <path id="Union_1" data-name="Union 1" d="M7,16V9H0V7H7V0H9V7h7V9H9v7Z" transform="translate(0)" fill="#fff" />
                            </g>
                        </svg>
                    </a>
                </div>
                <%}%>
            </div>
        </div>
    </div>

    <div class="clearfix"></div>
    <% if (Model != null)
        {%>
    <div class="card_container shadow-sm rounded">
        <div class="card_container__top">
            <div class="card_container__top__title"><%: Html.TranslateTag("Settings/AdminAccountTheme|Portals","Portals")%></div>
            <div class="col-10 col-md-6 dfjcfe dfac" id="newRefresh" style="margin-bottom: 5px;">
                <span class="media_desktop" style="vertical-align: sub;"><%: Html.TranslateTag("Settings/AdminAccountTheme|Display:","Display:")%></span>
                <a href="/Settings/AdminAccountTheme?" class="btn btn-secondary btn-sm ms-1"><span><%: Html.TranslateTag("Settings/AdminAccountTheme|All","All")%></span></a>
                <a href="/Settings/AdminAccountTheme?DNS=true" class="btn btn-secondary btn-sm mx-2"><span><%: Html.TranslateTag("Settings/AdminAccountTheme|DNS","DNS")%></span></a>
                <a href="/Settings/AdminAccountTheme?Inactive=true" class="btn btn-secondary btn-sm"><span><%: Html.TranslateTag("Settings/AdminAccountTheme|Inactive","Inactive")%></span></a>
            </div>
        </div>

        <div class="card_container__body">
            <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
            <% if (Model != null)
                {
                    bool ShowDNS = Request.QueryString["DNS"].ToBool();
                    bool ShowInactive = Request.QueryString["Inactive"].ToBool();

                    foreach (var item in Model)
                    {
                        if (item.IsActive == ShowInactive) continue;
            %>
            <%--Carson CSS move this inline styling to css and make it cooooooler. mmmm k --%>
            <div class="gridPanel col-12 px-2" style="height: 50px;border-bottom: 1px solid #e6e6e6;border-radius:0px;padding-top: 6px;">
                <div class="col-md-2 col-sm-3 col-4" style=" font-size: 1.0em;">
                    <br />
                    <b><%: item.Theme %></b>
                </div>

                <div class="col-lg-3 col-md-4 col-sm-3 col-6 dfac" style="height: 100%;">
                    <a href="https://<%: item.Domain %>" target="_blank"><%: item.Domain %></a>
                    <%if (ShowDNS)
                        {
                            string ip = "Unknown";
                            string color = "#FF0000";
                            try
                            {
                                ip = System.Net.Dns.GetHostAddresses(item.Domain)[0].ToString();
                                if (ip.Contains("64.58.225"))
                                    color = "#555555";
                            }
                            catch { }
                    %>
                    <br />
                    (<span style="color: <%:color%>"><%:ip %></span>) 
                    <%} %>
                </div>

                <div class="col-lg-4 col-md-3 d-none d-sm-block" >
                    <b><%= Html.TranslateTag("Settings/AdminAccountTheme|Account:","Account:")%></b> <%= Account.Load(item.AccountID).AccountNumber %>
                    <br />
                    <b><%: Html.TranslateTag("Settings/AdminAccountTheme|Account ID:","Account:ID")%></b> <%: item.AccountID %>
                </div>

                <div class="col-2 dfjcfe" style="padding-top: 6px;">
                    <%if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.IsCurrentCustomerAccountThemeAdmin)
                        { %>
                    <a href="/Settings/AdminAccountThemeEdit/<%:item.AccountThemeID%>">
                       <%=Html.GetThemedSVG("edit") %>
                    </a>
                    <%} %>
                </div>
            </div>

            <div class="clearfix"></div>
            <% }
                    }
                }
                else
                { %>
            <br />
            <%: Html.TranslateTag("Settings/AdminAccountTheme|No Account Themes","No Account Themes")%>
            <%} %>
            <div class="clearfix"></div>
        </div>
    </div>
 </div>

</asp:Content>
