<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Sensor>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Sensor Setup Completed
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%Html.RenderPartial("_SetupStepper", Model.SensorID); %>
    <div class="FILE" hidden>SensorComplete</div>

    <div class="container-fluid">
        <div class="rule_container_complete">

            <div class="card_container__top__title">
                <%: Html.TranslateTag("Sensor Setup Complete","Sensor Setup Complete")%>
            </div>

            <h3 class="rule-head"><%= Html.TranslateTag("What do you want to do now ?") %></h3>

            <div class="rule-sets">
                <a href="/Setup/AssignDevice/<%=Model.AccountID%>?networkID=<%=Model.CSNetID %>" class="rule-complete" style="position: relative;">
                    <div class="rule-complete-icon">
                        <span class="rule-svg"><%=Html.GetThemedSVG("add") %></span>
                    </div>
                    <%: Html.TranslateTag("Add Another Sensor", "Add Another Sensor")%>
                </a>
            </div>

            <div class="rule-sets">
                <%if (MonnitSession.CustomerCan("Notification_Edit"))
                    { %>
                <a href="/Rule/ChooseType" class=" rule-complete" style="position: relative;">
                    <div class="rule-complete-icon">
                        <span class="rule-svg"><%=Html.GetThemedSVG("add") %></span>
                    </div>
                    <%: Html.TranslateTag("Create Sensor Rule")%>
                </a>
                <%} %>
            </div>


            <div class="rule-sets">
                <%if (MonnitSession.CustomerCan("Sensor_Edit"))
                    { %>
                <a href="/Overview/SensorEdit/<%=Model.SensorID%>" class=" rule-complete" style="position: relative;">
                    <div class="rule-complete-icon">
                        <span class="rule-svg"><%=Html.GetThemedSVG("edit") %></span>
                    </div>
                    <%: Html.TranslateTag("Edit Sensor Settings")%>
                </a>
                <%} %>
            </div>


            <div class="rule-sets btn-next" style="padding-top: 20px;">
                <a href="/setup/QASteps" class=" btn btn-primary" style="position: relative; box-shadow: rgb(0 0 0 / 16%) 0px 1px 3px, rgb(0 0 0 / 5%) 0px 1px 3px">

                    <%: Html.TranslateTag("I Am Done Adding Sensors", "I Am Done Adding Sensors")%>
                </a>
            </div>

        </div>
    </div>

</asp:Content>
