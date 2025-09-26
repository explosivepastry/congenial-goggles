<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.AccountTheme>" %>
<div class="col-12 view-btns_container">
    <div class="view-btns deviceView_btn_row shadow-sm rounded">
        <a href="/Settings/AdminAccountTheme" class="btn btn-lg btn-fill">
            <div class="btn-secondaryToggle btn-lg btn-fill"><%=Html.GetThemedSVG("cardList") %><span class="extra"><%: Html.TranslateTag("Themes","Themes") %></span></div>
        </a>
        <a href="/Settings/AdminAccountThemeEdit/<%:Model.AccountThemeID %>">
            <div class="btn-<%:Request.Path.StartsWith("/Settings/AdminAccountThemeEdit/")?"active-fill shadow mb-lg-2":" " %> btn-lg btn-fill btn-secondaryToggle">
                <%=Html.GetThemedSVG("edit") %>
                <span class="extra">
                    <%: Html.TranslateTag("Settings/_WhiteLabelLink|Portal Edit","Edit")%>
                </span>
            </div>
        </a>
        <a href="/Settings/AdminPreferences/<%:Model.AccountThemeID %>">
            <div class="btn-<%:Request.Path.StartsWith("/Settings/AdminPreferences/")?"active-fill shadow mb-lg-2":" " %> btn-lg btn-fill btn-secondaryToggle">
                <%=Html.GetThemedSVG("desktop") %>
                <span class="extra">
                    <%: Html.TranslateTag("Settings/_WhiteLabelLink|Portal Preferences","Preferences")%>
                </span>
            </div>
        </a>
        <a href="/Settings/AdminEmailTemplate/<%:Model.AccountThemeID %>">
            <div class="btn-<%:Request.Path.StartsWith("/Settings/AdminEmailTemplate/")?"active-fill shadow mb-lg-2":" " %> btn-lg btn-fill btn-secondaryToggle">
                <%=Html.GetThemedSVG("envelope") %>
                <span class="extra">
                    <%: Html.TranslateTag("Settings/_WhiteLabelLink|Email Template","Template")%>
                </span>
            </div>
        </a>
        <%if (!MonnitSession.IsEnterprise)
            { %>
        <a href="/Settings/AdminThemeEdit/<%:Model.AccountThemeID %>">
            <div class="btn-<%:Request.Path.StartsWith("/Settings/AdminThemeEdit/") ? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill btn-secondaryToggle">
                <%=Html.GetThemedSVG("theme") %>
                <span class="extra">
                    <%: Html.TranslateTag("Settings/_WhiteLabelLink|Style", "Style")%>
                </span>
            </div>
        </a>
        <%}%>

        <%--		<a href="/Settings/AdminSMSCarriersList/<%:Model.AccountThemeID %>" class="btn <%:Request.Path.StartsWith("/Settings/AdminSMSCarriersList/")?"btn-primary":"btn-grey" %> btn-lg btn-fill"><i class="fa fa-phone"></i><span class="extra">&nbsp;SMS Carriers</span></a>--%>

        <a href="/Settings/AdminSMSCarriersList/<%:Model.AccountThemeID %>">
            <div class="btn-<%:Request.Path.StartsWith("/Settings/AdminSMSCarriersList/")?"active-fill shadow mb-lg-2":" " %> btn-lg btn-fill btn-secondaryToggle">
                <%=Html.GetThemedSVG("list") %>
<%--                <%=Html.GetThemedSVG("CallerID") %>--%>
                <span class="extra">
                    <%: Html.TranslateTag("Settings/_WhiteLabelLink|SMS List","SMS List")%>
                </span>
            </div>
        </a>


        <a href="/Settings/AdminContacts/<%:Model.AccountThemeID %>">
            <div class="btn-<%:Request.Path.StartsWith("/Settings/AdminContacts/")?"active-fill shadow mb-lg-2":" " %> btn-lg btn-fill btn-secondaryToggle">
                <%=Html.GetThemedSVG("accountDetails") %>
                <span class="extra">
                    <%: Html.TranslateTag("Settings/_WhiteLabelLink|Contacts","Contacts")%>
                </span>
            </div>
        </a>
    </div>
</div>

<script>
    $('.btn-secondaryToggle').hover(
        function () { $(this).addClass('active-hover-fill') },
        function () { $(this).removeClass('active-hover-fill') }
    )
</script>

<style>
    #svg_arrowLeft {
        fill: #666 !important;
    }
</style>
