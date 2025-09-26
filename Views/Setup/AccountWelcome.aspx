<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AccountWelcome
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        svg {
            height: 25px;
        }

/*        .True {
            background-color: #b4b8bf;
            color: white;
            fill: white;
        }

        .True svg {
            fill: white !important;
        }*/

        .True ~ .fu {
            content:url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 512 512'%3E%3C!--! Font Awesome Pro 6.2.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2022 Fonticons, Inc. --%3E%3Cpath fill='green' d='M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zM369 209L241 337c-9.4 9.4-24.6 9.4-33.9 0l-64-64c-9.4-9.4-9.4-24.6 0-33.9s24.6-9.4 33.9 0l47 47L335 175c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9z'/%3E%3C/svg%3E");
            position: relative;
            width: 20px;
            height: 20px;
            margin: 0;
            padding: 0;
            right: 14px;
            margin-right: -35px;
            background: white;
            border-radius: 12px;

        }
/*        .setup-icon > svg path {
            fill: var(--primary-color);
        }*/
/*        .setup-icon:hover > svg  path, 
        .setup-icon:hover {
            fill: var(--prime-btn-text-hover);
        }*/
/*        .w-item:hover  .setup-icon > svg path, 
        .w-item:hover {
            fill: var(--prime-btn-text-hover);
        }*/
           
    </style>
    <div class="setup_container">
        <div class="setup_container_design">

            <h2><%:Html.TranslateTag("Welcome to Your Account")%></h2>

            <div class="welcome_items">

                <div style="display:flex;">
                    <%bool CustomerConfigured = !string.IsNullOrEmpty(MonnitSession.CurrentCustomer.FirstName) && !string.IsNullOrEmpty(MonnitSession.CurrentCustomer.LastName);  %>
                    <a href="/Setup/NewCustomer/<%:MonnitSession.CurrentCustomer.CustomerID %>" class="w-item <%: CustomerConfigured ? "True" : "" %>">
                        <span class="setup-icon ">
                              <%=Html.GetThemedSVG("profile") %>
                        </span>
                        <%:Html.TranslateTag ("Profile Setup")%>
                    </a>
                    <div class="fu"></div>
                </div>

                <div style="display:flex;">
                    <%bool SensorAdded = Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Count > 0;  %>
                    <a href="/setup/QASteps" class="w-item <%: SensorAdded ? "True" : "" %>">
                        <div class="setup-icon setup-device-btn">
                             <%=Html.GetThemedSVG("sensor") %>
                        </div>
                        <%:Html.TranslateTag ("Device Setup")%>
                    </a>
                    <div class="fu"></div>
                </div>

                
                <div style="display:flex;">
                    <%bool RuleAdded = Notification.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Count > 0;  %>
                    <a href="/Rule/ChooseType" class="w-item <%: RuleAdded ? "True" : "" %>">
                        <span class="setup-icon">
                            <%=Html.GetThemedSVG("rules") %>
                        </span>
                        <%:Html.TranslateTag ("Rules and Notifications")%>
                    </a>
                    <div class="fu"></div>
                </div>

                
                <%if (CustomerConfigured && SensorAdded && RuleAdded){ %>
                <div class="rule-sets btn-next" style="padding-top: 20px;">
                    <a href="/Overview/" class=" btn btn-primary" style="position: relative; box-shadow: rgba(0, 0, 0, 0.16) 0px 1px 3px, rgba(0, 0, 0, 0.23) 0px 1px 3px;">
                    
                        <%: Html.TranslateTag("Setup Complete", "Setup Complete")%>
                    </a>
                </div>
                <%} %>

            </div>

        </div>
    </div>
</asp:Content>
