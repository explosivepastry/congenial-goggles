<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" Inherits="System.Web.Mvc.ViewPage<iMonnit.Models.DeviceInfoModel>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <%
        var submitAction = "";

        if(MonnitSession.CurrentCustomer != null)
        {
            submitAction = "/Network/ManualSubmit?deviceID=" + Model.DeviceID + "&checkCode=" + Model.Code + "&ID=" + MonnitSession.CurrentCustomer.AccountID;
        }

    %>

    <div class="login_container">
        <form class="login_form_container" action="<%=submitAction %>" method="post">
            <!-- Login link. needs some styling lovin-->
            <div class="login_logo_container">
                <%if (MonnitSession.CurrentBinaryStyle("Logo") != null && MonnitSession.CurrentBinaryStyle("Logo").Length > 0){%>
                <img class="w-75" src="/Overview/Logo" />
                <%}else{%>
                <img class="w-75" src="<%= Html.GetThemedContent("/images/Logo_TopBar.png")%>" />
                <%} %>
                <div id="siteLogo2"></div>
                <div class="col-12 loginBtn__container" style="clear: both; align-content: center; vertical-align: middle!important;">
                </div>
            </div>

            <div class="login_form">

                <div class="login-form-container">
                    <!-- Minimal Device Info -->
                    <div class="login_tab" data-tab="tab-1" style="margin-top: -25px;">
                        <%:Html.TranslateTag ("ID:")%> <strong><%=Model.DeviceID %></strong> &nbsp;&nbsp;&nbsp; <%:Html.TranslateTag ("CODE:")%> <strong><%=Model.Code %></strong>
                    </div>

                    <br />
                    <br />

                    <!-- Device Icon and Status Indicator -->
                    <div class="eventIcon_container">
                        <div class="sensor eventIcon eventIconStatus sensorIcon sensorStatus<%:Model.Sensor.ApplicationID.ToString() %>">
                        </div>
                        <%=Html.GetThemedSVG("app" + Model.Sensor.ApplicationID) %>
                    </div>

                    <br />
                    <br />

                    <!-- Add to Account -->
                    <div style="display: flex; flex-direction: column;">
                        <input name="DeviceID" value="<%=Model.DeviceID %>" hidden />
                        <input name="DeviceCode" value="<%=Model.Code %>" hidden />

                        <%if (!Model.Sensor.Network.HoldingOnlyNetwork) //sensor not available
                            {
                                if (MonnitSession.CurrentCustomer != null)
                                { %>
                                    <div class="clearfix"></div>

                                    <span><strong><%:Html.TranslateTag ("This device belongs to another account")%></strong></span>

                                    <div class="hamburger" style="position: absolute; top: 10px; left: 10px;">
                                        <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="25" height="25" viewBox="0 0 25 25" onclick="$('#device_info_nav').toggleClass('show');$('#arrow').toggleClass('show');">
                                            <path id="Menu" d="M0,23V19.714H15.626V23Zm0-9.857V9.857H25v3.286ZM0,3.286V0H25V3.286Z" transform="translate(0 1)" fill="black"></path>
                                        </svg>
                                    </div>
                                    <div id="arrow" style="position: absolute; top: 10px; left: 10px; z-index: 101;">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="28" height="28" viewBox="0 0 22.541 22.541" class="" onclick="$('#device_info_nav').toggleClass('show');$('#arrow').toggleClass('show');">
                                            <path id="ic_arrow_back_24px" d="M20,11H7.83l5.59-5.59L12,4,4,12l8,8,1.41-1.41L7.83,13H20Z" transform="translate(9.792 -5.635) rotate(40)" fill="#fff"></path>
                                        </svg>
                                    </div>
                                    <div id="device_info_nav" class="device_info_nav " style="background: #0067AB; position: absolute; top: 0; left: 0;">
                                        <% Html.RenderPartial("LeftBar", MonnitSession.SensorListFilters.CSNetID); %>
                                    </div>
                                <%}
                                    
                                else
                                { %>
                                    <span style="text-align: center;"><strong><%:Html.TranslateTag ("This device belongs to another account")%></strong></span>
                                    <a href="/account/LogOnOV?ReturnUrl=/Setup/DeviceInfo?id=<%=Model.DeviceID %>{0}code=<%=Model.Code %>" class="btn btn-primary mt-4 mb-2" style="width: 250px;">Login</a>
                                    <a href="/Setup/CreateAccount" id="create" class="btn btn-primary"><%=Html.TranslateTag("Create Account", "Create Account")%></a>

                                <% } %>
                        
                        <%}
                            else //sensor available
                            {
                                if (MonnitSession.CurrentCustomer != null)
                                { %>
                                    <!-- Side navigation when logged in -->
                                    <div class="hamburger" style="position: absolute; top: 10px; left: 10px;">
                                        <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="25" height="25" viewBox="0 0 25 25" onclick="$('#device_info_nav').toggleClass('show');$('#arrow').toggleClass('show');">
				                            <path id="Menu" d="M0,23V19.714H15.626V23Zm0-9.857V9.857H25v3.286ZM0,3.286V0H25V3.286Z" transform="translate(0 1)" fill="black"></path>
			                            </svg>
                                    </div>
                                    <div id="arrow" style="position: absolute; top: 10px; left: 10px; z-index: 101;">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="28" height="28" viewBox="0 0 22.541 22.541" class="" onclick="$('#device_info_nav').toggleClass('show');$('#arrow').toggleClass('show');">
					                        <path id="ic_arrow_back_24px" d="M20,11H7.83l5.59-5.59L12,4,4,12l8,8,1.41-1.41L7.83,13H20Z" transform="translate(9.792 -5.635) rotate(40)" fill="#fff"></path>
				                        </svg>
                                    </div>
                                    <div id="device_info_nav" class="device_info_nav " style="background: #0067AB;position: absolute; top: 0; left: 0;">
                                        <% Html.RenderPartial("LeftBar", MonnitSession.SensorListFilters.CSNetID); %>
                                    </div>
                                    <input type="submit" value="Add Device" class="btn btn-primary" style="width:250px;">
                                    <!-- Add TopBar Navigation -->
                                <%}
                                else
                                { %>
                                <a href="/account/LogOnOV?ReturnUrl=/Setup/DeviceInfo?id=<%=Model.DeviceID %>{0}code=<%=Model.Code %>" class="btn btn-primary mb-2" style="width: 250px;">Login</a>
                                <a href="/Setup/CreateAccount" id="create" class="btn btn-primary"><%=Html.TranslateTag("Create Account", "Create Account")%></a>
                            <%}
                                }%>
                        </div>

                    <br />
                    <br />
                    <% if (!MonnitSession.IsEnterprise && MonnitSession.CurrentTheme.Theme == "Default")
                        { %>
                    <%Html.RenderPartial("_DeviceInfoSupport", Model.Sensor);%>
                    <div class="clearfix"></div>

                    <br />
                    <br />
                </div>
                	<div class="col-xs-12 col-md-12 col-lg-12" style="display: flex; justify-content: center;">								
					<%Html.RenderPartial("_DeviceInfoApplications", Model.Sensor);%>
                <% } %>
				</div>
            </div>
            <%if (MonnitSession.CurrentCustomer != null)
                {
            %>
            <input type="hidden" id="accID" name="ID" value="<%=MonnitSession.CurrentCustomer.AccountID %>" />
            <input type="hidden" id="netID" name="networkID" value="<%=MonnitSession.SensorListFilters.CSNetID %>" />
            <input type="hidden" id="deviceID" name="deviceID" value="<%=Model.DeviceID %>" />
            <input type="hidden" id="checkCode" name="checkCode" value="<%=MonnitUtil.CheckDigit(Model.DeviceID ?? 0) %>" />
            <% } %>
        </form>

        <div class="login_image">
            <img src="../../Content/images/login-dashPhone.png" style="width: 100%;" />
        </div>

    </div>

    <script>
        $('#device_info_nav').addClass('show');
        $('#arrow').addClass('show');
    </script>


    <style>
        .device_info_nav{
            z-index: 99;
            height: 100vh;
            padding-left:50px;
            padding-top: 50px;
        }

        #arrow {
            cursor: pointer;
        }

        @media screen and (max-width: 765px) {
            .device_info_nav {
                display: flex !important;
                justify-content: center !important;
                width: 100vw !important;
                display: none;
                padding: 0;
            }

            .admin-spacer {
                width: 100% !important;
            }
        }

        @media screen and (max-width: 892px) (min-width: 765px) {
            .device_info_nav {
                padding: 0;
            }
            .device_info_nav .split_mobile {
                margin: auto;
                display: flex!important;
                justify-content: center!important;
            }
        }

        @media screen and (min-width: 893px) {
            .admin-spacer {
                width: 250px!important;
            }
        }


        .show {
            display: none!important;
        }

        #wrapper {
            position: relative;
        }

        .siteLogo {
            height: 38px;
        }

        .goodBox {
            border-color: lightgreen;
        }

        .login_form_container {
            justify-content: start !important;
        }

        .createAccount {
            border: none;
            width: 150px !important;
            margin: 10px auto;
        }

        .btns {
            display: flex;
            background: #0067AB !important;
            color: white !important;
            justify-content: center;
            align-items: center;
            font-weight: 600;
            font-size: 18px;
            height: 40px;
            width: 250px;
            border-radius: 5px;
        }

        .hamburger {
            cursor: pointer;
        }
    </style>

</asp:Content>
