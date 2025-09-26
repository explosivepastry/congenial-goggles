<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Gateway>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Gateway Setup Completed
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%Html.RenderPartial("_SetupStepper", Model.GatewayID); %>
    <div class="FILE" hidden>GatewayComplete</div>

    <div class="container-fluid">
        <div class="rule_container_complete">

            <div class="card_container__top__title">
                <%: Html.TranslateTag("Gateway Setup Complete","Gateway Setup Complete")%>
            </div>

            <h3 class="rule-head"><%= Html.TranslateTag("What do you want to do now ?") %></h3>

            <div class="rule-sets">
                <a href="/Setup/AssignDevice/<%=ViewBag.AccountID%>?networkID=<%=Model.CSNetID %>" class="rule-complete" style="position: relative;">
                    <div class="rule-complete-icon">
                        <span class="rule-svg"><%=Html.GetThemedSVG("add") %></span>
                    </div>
                    <%: Html.TranslateTag("Add Another Gateway", "Add Another Gateway")%>
                </a>
            </div>

            <div class="rule-sets">
                <a href="/Setup/AssignDevice/<%=ViewBag.AccountID%>?networkID=<%=Model.CSNetID %>" class="rule-complete" style="position: relative;">
                    <div class="rule-complete-icon">
                        <span class="rule-svg"><%=Html.GetThemedSVG("add") %></span>
                    </div>
                    <%: Html.TranslateTag("Add a Sensor", "Add a Sensor")%>
                </a>
            </div>


            <div class="rule-sets">
                <%if (MonnitSession.CustomerCan("Notification_Edit"))
                    { %>
                <a href="/Rule/ChooseType" class=" rule-complete" style="position: relative;">
                    <div class="rule-complete-icon">
                        <span class="rule-svg"><%=Html.GetThemedSVG("add") %></span>
                    </div>
                    <%: Html.TranslateTag("Create Gateway Rule")%>
                </a>
                <%} %>
            </div>

            <div class="rule-sets">
                <a href="/Overview/GatewayEdit/<%=Model.GatewayID%>" class=" rule-complete" style="position: relative;">
                    <div class="rule-complete-icon">
                        <span class="rule-svg"><%=Html.GetThemedSVG("edit") %></span>
                    </div>
                    <%: Html.TranslateTag("Edit Gateway Settings")%>
                </a>
            </div>



            <%--<div class="rule-sets">
                <a href="/Rule/ChooseType/" class=" rule-complete" style="position: relative;">
                    <div class="rule-complete-icon">
                        <span class="rule-svg"><%=Html.GetThemedSVG("rules") %></span>
                    </div>
                    <%: Html.TranslateTag("Create a Rule", "Create a Rule")%>
                </a>
            </div>--%>

            <div class="rule-sets btn-next" style="padding-top: 20px;">
                <a href="/setup/QASteps" class=" btn btn-primary" style="position: relative;">

                    <%: Html.TranslateTag("I Am Done Adding Gateways", "I Am Done Adding Gateways")%>
                </a>
            </div>

        </div>
    </div>

</asp:Content>
