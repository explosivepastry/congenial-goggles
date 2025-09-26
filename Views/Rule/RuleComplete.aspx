<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    RuleCompleted
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%MonnitSession.NotificationInProgress = null; %>
    <%string ruleName = ViewBag.RuleName; %>

      <%=Html.Partial("_CreateNewRuleProgressBar") %>

    <div class="FILE" hidden>RuleComplete</div>

    <div class="container-fluid">
        <div class="rule_container_complete">

            <div class="card_container__top__title">
                <%: Html.TranslateTag("Rule Complete")%>: <%=ruleName %>
            </div>

            <h3 class="rule-title"><%= Html.TranslateTag("What do you want to do now ?") %></h3>

            <div class="rule-sets">
                <a href="/Rule/Triggers/<%=ViewBag.NotiID%>" class="rule-complete" style="position: relative;">
                    <div class="rule-complete-icon">
                        <span class="rule-svg"><%=Html.GetThemedSVG("add") %></span>
                    </div>
                     <%: Html.TranslateTag("Add Additional Devices To")%> "<%=ruleName%>"
                </a>
            </div>

            <div class="rule-sets">
                <a href="/Rule/Calendar/<%=ViewBag.NotiID%>" class=" rule-complete" style="position: relative;">
                    <div class="rule-complete-icon">
                        <span class="rule-svg"><%=Html.GetThemedSVG("schedule") %></span>
                    </div>
                    <%: Html.TranslateTag("Add Schedule To")%> "<%=ruleName%>"
                </a>
            </div>
            <div class="rule-sets">
                <a href="/Rule/RuleEdit/<%=ViewBag.NotiID%>" class=" rule-complete" style="position: relative;">
                    <div class="rule-complete-icon">
                        <span class="rule-svg"><%=Html.GetThemedSVG("edit") %></span>
                    </div>
                    <%: Html.TranslateTag("Edit Advanced Settings")%>
                </a>
            </div>

            <div class="rule-sets">
                <a href="/Rule/ChooseType/" class=" rule-complete" style="position: relative;">
                    <div class="rule-complete-icon">
                        <span class="rule-svg"><%=Html.GetThemedSVG("rules") %></span>
                    </div>
                    <%: Html.TranslateTag("Create New Rule", "Create New Rule")%>
                </a>
            </div>

            <div class="rule-sets btn-next" style="padding-top: 20px;">
                <a href="<%:MonnitSession.IsAccountNew ? "/Setup/AccountWelcome" : "/Rule/Index" %>" class=" btn btn-primary user-dets" style="position: relative;">

                    <%: Html.TranslateTag("Done Adding Rules", "Done Adding Rules")%>
                </a>
            </div>

        </div>
    </div>

</asp:Content>
