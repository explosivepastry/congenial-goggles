<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    QASteps
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
        svg {
            height: 25px;
        }

/*        .True {
            background-color: #b4b8bf;
            color: white;
        }

            .True svg {
                fill: white !important;
            }*/

            .True ~ .fu {
                content: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 512 512'%3E%3C!--! Font Awesome Pro 6.2.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2022 Fonticons, Inc. --%3E%3Cpath fill='green' d='M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zM369 209L241 337c-9.4 9.4-24.6 9.4-33.9 0l-64-64c-9.4-9.4-9.4-24.6 0-33.9s24.6-9.4 33.9 0l47 47L335 175c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9z'/%3E%3C/svg%3E");
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

        .circleQuestion:hover, .circleQuestion:active {
            transform: scale(1);
        }
    </style>

    <div class="setup_qa_container">
        <div class="setup_container_design">
            <div style="display:flex;">
                <h2 class="heading-color modal-title qa-title"><%:Html.TranslateTag("Setup/QASteps|Set Up Your Devices","Set Up Your Devices")%></h2>
                <div class="circleQuestion " style="transform: translateY(17px) translateX(24px)" data-bs-toggle="modal" data-bs-target="#helpModal">
                    <%=Html.GetThemedSVG("circleQuestion") %>
                </div>
            </div>

            <div class="step-row">
                <h6 class="steps"><%:Html.TranslateTag("Step ","Step ")%>1.</h6>
            </div>

            <div style="display: flex;">
                <%bool GatewayAdded = Gateway.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Count > 0;  %>
                <a href="/Setup/AssignDevice/<%:MonnitSession.CurrentCustomer.AccountID %>" class="qa-item <%: GatewayAdded ? "True" : "" %>">
                    <span class="setup-icon">
                        <%=Html.GetThemedSVG("gateway") %>
                    </span>
                    <%:Html.TranslateTag("Setup/QASteps|Gateway Setup","Gateway Setup")%>
                </a>
                <div class="fu"></div>
            </div>

            <div class="step-row">
                <h6 class="steps"><%:Html.TranslateTag("Setup/QASteps|Step","Step ")%>2.</h6>
            </div>

            <div style="display: flex;">
                <%bool SensorAdded = Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Count > 0;  %>
                <a href="/Setup/AssignDevice/<%:MonnitSession.CurrentCustomer.AccountID %>" class="qa-item <%: SensorAdded ? "True" : "" %>">
                    <span class="setup-icon">
                        <%=Html.GetThemedSVG("sensor") %>
                    </span>
                    <%:Html.TranslateTag("Setup/QASteps|Sensor Setup","Sensor Setup")%>
                </a>
                <div class="fu"></div>
            </div>

            <p style="font-weight:600">
                <%:Html.TranslateTag("Setup/QASteps|Some sensors do not require a gateway.","Some sensors do not require a gateway.")%>
            </p>

            <%if (GatewayAdded && SensorAdded)
                { %>
            <div class="rule-sets btn-next" style="padding-top: 20px;">
                <a href="/Setup/AccountWelcome" class=" btn btn-primary" style="position: relative; box-shadow: rgba(0, 0, 0, 0.16) 0px 1px 3px, rgba(0, 0, 0, 0.23) 0px 1px 3px;">
                    <%: Html.TranslateTag("Setup/QASteps|Device Setup Complete","Device Setup Complete")%>
                </a>
            </div>
            <%} %>
        </div>
    </div>


    <!-- Help Modal -->
    <div class="modal fade" id="helpModal" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title"><%: Html.TranslateTag ("Setup/QASteps|Device Setup","Device Setup") %></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" style="color: blue"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="word-def">
                            <%:Html.TranslateTag("Setup/QASteps|Help Text","Start by setting up your gateway first. Most sensors will need the gateway setup before they will be able to complete their sensor setup.  If you have Wi-Fi, Ethernet, Bluetooth, or LTE based sensors, you can skip the gateway setup and go straigt to Step 2.")%>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
