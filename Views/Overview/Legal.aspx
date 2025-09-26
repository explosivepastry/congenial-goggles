<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Legal
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="display:flex;justify-content:center;width:100%;">
        <div class="panel panel-primary panel-body p-5" style="width:80%; margin-bottom: 35px;">

            <br />
            
            <div class="dfjcac" style="width: 100%;">
                <%if (MonnitSession.CurrentBinaryStyle("Logo") != null && MonnitSession.CurrentBinaryStyle("Logo").Length > 0){%>
                <img class="siteLogo" src="/Overview/Logo" />
                <%}else{%>
                <img class="siteLogo" src="<%= Html.GetThemedContent("/images/Logo_TopBar.png")%>" />
                <%} %>
                <div id="siteLogo2"></div>
            </div>

            <hr style="margin-top: 20px;">
            
            <h3 class="panel-title" style="text-align: center; font-weight: bold; font-size: 20px;">TERMS OF USE</h3>

            <%Html.RenderPartial("_TermsOfUse"); %>

            
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 dfjcac">
                    <a role="button" href="/overview" class="btn gen-btn" style="margin-left: 45px; margin-right: 40px; width: 70%; height: 40px; max-width: 250px; font-size: 18px; align-content: center; vertical-align: middle!important; /* w3c, ie10+, ff16+, chrome26+, opera12+, safari7+ */"><%: Html.TranslateTag("Overview/Legal|Go Home","Go Home")%></a>
                    <br />
                    <br />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
