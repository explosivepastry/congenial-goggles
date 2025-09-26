<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">

        <%
            ExternalDataSubscription exdata = null;
            if (MonnitSession.CurrentCustomer != null && MonnitSession.CurrentCustomer.Account != null)
                exdata = MonnitSession.CurrentCustomer.Account.ExternalDataSubscription;

            if (exdata == null)
                exdata = new ExternalDataSubscription();%>

        <%Html.RenderPartial("../Export/_APILink"); %>

        <div class="col-lg-6 col-12 px-0 pe-lg-2 mb-4">
            <div class="rule-card_container w-100" style="min-height: 372px;">
                <div class="card_container__top">
                    <div class="card_container__top__title dfjcsb">
                        <span><%: Html.TranslateTag("API/RestAPI|How to use our APIs","How to use our APIs")%></span>
                    </div>
                </div>

                <div class="x_content">
                    <div class="card__container__body">
                        <div class="col-12 card_container__body__content">
                            <div class="warningBoxAB">
                                <div class="warningIconAB"><%=Html.GetThemedSVG("app13") %></div>
                                <p style="padding: 1rem; margin: 0;">
                                    We have implemented a new policy and all users are now limited to running API calls serially, one at a time. If your system previously made calls to API endpoints in parallel, you will need to modify your processes to wait for the return of each call before making the next one. Otherwise, your parallel calls may encounter an error indicating API call rate exceeded. 
                                </p>
                            </div>
                            <p>
                                <%: Html.TranslateTag("API/RestAPI|Rest API allows users to integrate their sensor data into 3rd party applications.","Rest API allows users to integrate their sensor data into 3rd party applications.")%>
                                <%: Html.TranslateTag("API/RestAPI|API calls can be made from programming language of your choice. Responses are served as either XML(Extensible Markup Language) or JSON (JavaScript Object Notation).","API calls can be made from programming language of your choice. Responses are served as either XML(Extensible Markup Language) or JSON (JavaScript Object Notation).")%>
                                <%: Html.TranslateTag("API/RestAPI|Overuse could result in revocation of API portal.","Overuse could result in revocation of API portal.")%>
                            </p>

                            <%--Rest API allows users to integrate their sensor data into 3rd party applications.
							    API calls can be made from programming language of your choice. Responses are served
								as either<b> XML(Extensible Markup Language) or JSON (JavaScript Object Notation).</b>
                                Overuse could result in revocation of API portal.--%>

                            <p>
                                <%: Html.TranslateTag("API/RestAPI|Overuse Example: making the same call more often than the data is updated (Datamessage Methods: 1 request per 10 minutes).","Overuse Example: making the same call more often than the data is updated (Datamessage Methods: 1 request per 10 minutes)." )%>
                                <%: Html.TranslateTag("API/RestAPI|Please utilize the webhook for real time Datamessages.","Please utilize the webhook for real time Datamessages.")%>
                            </p>

                            <p>
                                *<%: Html.TranslateTag("API/RestAPIAll Dates are in UTC (Universal Coordinated Time) both for the input and for the output.","All Dates are in UTC (Universal Coordinated Time) both for the input and for the output.")%><br />
                                *<%: Html.TranslateTag("API/RestAPI|To request a larger data load, Please contact support","To request a larger data load, Please contact support")%>
                            </p>

                            <h4><%: Html.TranslateTag("API/RestAPI|Authorization","Authorization")%></h4>
                            <p>
                                <%: Html.TranslateTag("API/RestAPI|APIs use authorization to ensure that client requests access data securely.","APIs use authorization to ensure that client requests access data securely.")%>
                                <%: Html.TranslateTag("The","The")%> <b><%: Html.TranslateTag("APIKeyID","APIKeyID")%> </b><%: Html.TranslateTag("and the","and the")%> <b><%: Html.TranslateTag("APISecretKey","APISecretKey")%> </b><%: Html.TranslateTag("both need to be included as headers in the posted request.","both need to be included as headers in the posted request.")%>
                            </p>
                            <p><%: Html.TranslateTag("Click","Click")%> <a href="/api/ApiKeys" style="color: blue; text-decoration: underline;"><%: Html.TranslateTag("Api Keys","Api Keys")%></a> <%: Html.TranslateTag("to get your API keys.","to get your API keys.")%></p>

                            <h4><%: Html.TranslateTag("Parameters","Parameters")%></h4>
                            <p><%: Html.TranslateTag("API/RestAPI|Our parameters will need to be passed in the request as Param entries. The Parameter name will be set as the","Our parameters will need to be passed in the request as Param entries. The Parameter name will be set as the")%> <b><%: Html.TranslateTag("Key","Key")%> </b><%: Html.TranslateTag("and the parameter value will be set as the","and the parameter value will be set as the")%> <b><%: Html.TranslateTag("Value","Value")%></b>.</p>

                            <div class="">
                                <h2 style="background-color: white !important; overflow: unset;"><b><%: Html.TranslateTag("API/RestAPI|Explore our APIs", "Explore our APIs")%></b></h2>
                                <div class="nav navbar-right panel_toolbox">
                                </div>
                                <div class="clearfix"></div>
                            </div>

                            <br />

                            <select id="apiselector" class="networkSelect form-select user-dets" style="width: 250px;">
                                <option selected disabled><%: Html.TranslateTag("API/RestAPI|Select API Type","Select API Type")%></option>
                                <option value="lookup"><%: Html.TranslateTag("Lookup","Lookup")%></option>
                                <option value="account"><%: Html.TranslateTag("Account","Account")%></option>
                                <%if (MonnitSession.CurrentCustomer != null)// && MonnitSession.CurrentCustomer.Account.IsReseller)
                                    { %>
                                <option value="<%: Html.TranslateTag("reseller","reseller")%>"><%: Html.TranslateTag("Reseller Specific","Reseller Specific")%></option>
                                <%}%>
                                <option value="network"><%: Html.TranslateTag("Network","Network")%></option>
                                <option value="gateway"><%: Html.TranslateTag("Gateway","Gateway")%></option>
                                <option value="sensor"><%: Html.TranslateTag("Sensor","Sensor")%></option>
                                <option value="groups"><%: Html.TranslateTag("Sensor Group","Sensor Group")%></option>
                                <option value="datamessage"><%: Html.TranslateTag("Data Message","Data Message")%></option>
                                <option value="notification"><%: Html.TranslateTag("Notification","Notification")%></option>
                                <option value="advanced"><%: Html.TranslateTag("Advanced Notification","Advanced Notification")%></option>
                                <option value="webhook"><%: Html.TranslateTag("Webhook","Webhook")%></option>
                            </select>
                            <div class="clearfix"></div>
                            <br />

                            <!-- Div only here to collapse all API fieldsets-->
                            <div id="collapseAll">

                                <fieldset id="lookup" class="apioption" style="display: none">
                                    <div class="accordion accordion-flush" id="accordionLookup">
                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-TimeZones" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Time Zones","Time Zones")%>
                                                </button>
                                            </h2>
                                            <div id="flush-TimeZones" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Lookup/TimeZones");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-TimeZonesWithRegion" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Time Zones with Region","Time Zones with Region")%>
                                                </button>
                                            </h2>
                                            <div id="flush-TimeZonesWithRegion" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Lookup/TimeZonesWithRegion");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SMSCarriers" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|SMS Carriers","SMS Carriers")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SMSCarriers" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Lookup/SMSCarriers");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-GetApplicationID" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Get Application ID","Get Application ID")%>
                                                </button>
                                            </h2>
                                            <div id="flush-GetApplicationID" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Lookup/GetApplicationID");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-GetDatumByType" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Get Datum By Type","Get Datum By Type")%>
                                                </button>
                                            </h2>
                                            <div id="flush-GetDatumByType" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Lookup/GetDatumByType");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-AccountNumberAvailable" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Account Number Available","Account Number Available")%>
                                                </button>
                                            </h2>
                                            <div id="flush-AccountNumberAvailable" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Lookup/AccountNumberAvailable");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-UserNameAvailable" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|User Name Available","User Name Available")%>
                                                </button>
                                            </h2>
                                            <div id="flush-UserNameAvailable" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Lookup/UserNameAvailable");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationEmailAvailable" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Email Available","Notification Email Available")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationEmailAvailable" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Lookup/NotificationEmailAvailable");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorLookUp" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Lookup","Sensor Lookup")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorLookUp" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Lookup/SensorLookUp");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-GatewayLookUp" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Gateway Lookup","Gateway Lookup")%>
                                                </button>
                                            </h2>
                                            <div id="flush-GatewayLookUp" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Lookup/GatewayLookUp");%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="accordion-item">
                                        <h2 class="accordion-header">
                                            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-CalibrationFacilityList" aria-expanded="false">
                                                <%: Html.TranslateTag("API/RestAPI|Calibration Facility List","Calibration Facility List")%>
                                            </button>
                                        </h2>
                                        <div id="flush-CalibrationFacilityList" class="accordion-collapse collapse">
                                            <div class="accordion-body">
                                                <%Html.RenderPartial("Sensor/CalibrationFacilityList");%>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>

                                <fieldset id="account" class="apioption" style="display: none">
                                    <div class="accordion accordion-flush" id="accordionAccount">
                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-CreateAccount" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Create Account","Create Account")%>
                                                </button>
                                            </h2>
                                            <div id="flush-CreateAccount" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Account/CreateAccount");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-AccountGet" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Account Get","Account Get")%>
                                                </button>
                                            </h2>
                                            <div id="flush-AccountGet" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Account/AccountGet"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-EditAccountInformation" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Edit Account Information","Edit Account Information")%>
                                                </button>
                                            </h2>
                                            <div id="flush-EditAccountInformation" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Account/EditAccountInformation"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-EditCustomerInformation" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Edit Customer Information","Edit Customer Information")%>
                                                </button>
                                            </h2>
                                            <div id="flush-EditCustomerInformation" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Account/EditCustomerInformation"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-GetCustomerPermissions" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Get Customer Permissions","Get Customer Permissions")%>
                                                </button>
                                            </h2>
                                            <div id="flush-GetCustomerPermissions" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Account/GetCustomerPermissions"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-EditCustomerPermissions" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Edit Customer Permissions","Edit Customer Permissions")%>
                                                </button>
                                            </h2>
                                            <div id="flush-EditCustomerPermissions" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Account/EditCustomerPermissions"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-RetrieveUsername" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Retrieve Username","Retrieve Username")%>
                                                </button>
                                            </h2>
                                            <div id="flush-RetrieveUsername" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Account/RetrieveUsername");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-CreateAccountUser" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Create Account User","Create Account User")%>
                                                </button>
                                            </h2>
                                            <div id="flush-CreateAccountUser" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Account/CreateAccountUser");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-AccountUserDelete" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Account User Delete","Account User Delete")%>
                                                </button>
                                            </h2>
                                            <div id="flush-AccountUserDelete" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Account/AccountUserDelete"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-AccountUserList" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Account User List","Account User List")%>
                                                </button>
                                            </h2>
                                            <div id="flush-AccountUserList" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Account/AccountUserList"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-AccountUserGet" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Account User Get","Account User Get")%>
                                                </button>
                                            </h2>
                                            <div id="flush-AccountUserGet" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Account/AccountUserGet"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-AccountUserEdit" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Account User Edit","Account User Edit")%>
                                                </button>
                                            </h2>
                                            <div id="flush-AccountUserEdit" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Account/AccountUserEdit"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-AccountUserScheduleGet" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Account User Schedule Get","Account User Schedule Get")%>
                                                </button>
                                            </h2>
                                            <div id="flush-AccountUserScheduleGet" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Account/AccountUserScheduleGet"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-AccountUserScheduleSet" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Account User Schedule Set","Account User Schedule Set")%>
                                                </button>
                                            </h2>
                                            <div id="flush-AccountUserScheduleSet" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Account/AccountUserScheduleSet"); %>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <%--  <%Html.RenderPartial("Account/AccountUserImageUpload"); %>--%>
                                </fieldset>

                                <fieldset id="reseller" class="apioption" style="display: none">
                                    <div class="accordion accordion-flush" id="accordionReseller">
                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-CreateSubAccount" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Create Sub Account","Create Sub Account")%>
                                                </button>
                                            </h2>
                                            <div id="flush-CreateSubAccount" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Account/CreateSubAccount");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-RemoveSubAccount" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Remove Sub Account","Remove Sub Account")%>
                                                </button>
                                            </h2>
                                            <div id="flush-RemoveSubAccount" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Account/RemoveSubAccount");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SubAccountList" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sub Account List","Sub Account List")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SubAccountList" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Account/SubAccountList");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SubAccountTreeList" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sub Account Tree List","Sub Account Tree List")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SubAccountTreeList" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Account/SubAccountTreeList");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-AccountParentEdit" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Account Parent Edit","Account Parent Edit")%>
                                                </button>
                                            </h2>
                                            <div id="flush-AccountParentEdit" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Account/AccountParentEdit"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-ResellerPasswordReset" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Reseller Password Reset","Reseller Password Reset")%>
                                                </button>
                                            </h2>
                                            <div id="flush-ResellerPasswordReset" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Account/ResellerPasswordReset");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SetExpirationDate" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Set Expiration Date","Set Expiration Date")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SetExpirationDate" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Account/SetExpirationDate");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-RecentResellerNotification" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Recent Reseller Notification","Recent Reseller Notification")%>
                                                </button>
                                            </h2>
                                            <div id="flush-RecentResellerNotification" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/RecentResellerNotification");%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>

                                <fieldset id="network" class="apioption" style="display: none">
                                    <div class="accordion accordion-flush" id="accordionNetwork">
                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-CreateNetwork" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Create Network","Create Network")%>
                                                </button>
                                            </h2>
                                            <div id="flush-CreateNetwork" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Network/CreateNetwork");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-CreateNetwork2" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Create Network 2","Create Network  ")%>2
                                                </button>
                                            </h2>
                                            <div id="flush-CreateNetwork2" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Network/CreateNetwork2");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-RemoveNetwork" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Remove Network","Remove Network")%>
                                                </button>
                                            </h2>
                                            <div id="flush-RemoveNetwork" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Network/RemoveNetwork");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NetworkList" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Network List","Network List")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NetworkList" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Network/NetworkList");%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>

                                <fieldset id="gateway" class="apioption" style="display: none">
                                    <div class="accordion accordion-flush" id="accordionGateway">
                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-GatewayResetDefaults" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Gateway Reset Defaults","Gateway Reset Defaults")%>
                                                </button>
                                            </h2>
                                            <div id="flush-GatewayResetDefaults" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Gateway/GatewayResetDefaults");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-GatewayReform" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Gateway Reform","Gateway Reform")%>
                                                </button>
                                            </h2>
                                            <div id="flush-GatewayReform" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Gateway/GatewayReform"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-RemoveGateway" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Remove Gateway","Remove Gateway")%>
                                                </button>
                                            </h2>
                                            <div id="flush-RemoveGateway" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Gateway/RemoveGateway");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-AssignGateway" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Assign Gateway","Assign Gateway")%>
                                                </button>
                                            </h2>
                                            <div id="flush-AssignGateway" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Gateway/AssignGateway");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-GatewayGet" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Gateway Get","Gateway Get")%>
                                                </button>
                                            </h2>
                                            <div id="flush-GatewayGet" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Gateway/GatewayGet");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-GatewayList" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Gateway List","Gateway List")%>
                                                </button>
                                            </h2>
                                            <div id="flush-GatewayList" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Gateway/GatewayList");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-GatewaySetHeartbeat" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Gateway Set Heartbeat","Gateway Set Heartbeat")%>
                                                </button>
                                            </h2>
                                            <div id="flush-GatewaySetHeartbeat" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Gateway/GatewaySetHeartbeat");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-GatewaySetIP" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Gateway Set IP","Gateway Set IP")%>
                                                </button>
                                            </h2>
                                            <div id="flush-GatewaySetIP" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Gateway/GatewaySetIP"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-GatewaySetName" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Gateway Set Name","Gateway Set Name")%>
                                                </button>
                                            </h2>
                                            <div id="flush-GatewaySetName" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Gateway/GatewaySetName"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-GatewayPoint" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Gateway Point","Gateway Point")%>
                                                </button>
                                            </h2>
                                            <div id="flush-GatewayPoint" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Gateway/GatewayPoint"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-GatewaySetHost" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Gateway Set Host","Gateway Set Host")%>
                                                </button>
                                            </h2>
                                            <div id="flush-GatewaySetHost" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Gateway/GatewaySetHost"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-GatewayClearHost" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Gateway Clear Host","Gateway Clear Host")%>
                                                </button>
                                            </h2>
                                            <div id="flush-GatewayClearHost" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Gateway/GatewayClearHost"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-GatewayCellNetworkConfig" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Gateway Cell Network Config","Gateway Cell Network Config")%>
                                                </button>
                                            </h2>
                                            <div id="flush-GatewayCellNetworkConfig" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Gateway/GatewayCellNetworkConfig"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-GatewayAutoConfig" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Gateway Auto Config","Gateway Auto Config")%>
                                                </button>
                                            </h2>
                                            <div id="flush-GatewayAutoConfig" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Gateway/GatewayAutoConfig"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-GatewayStopAutoConfig" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Gateway Stop Auto Config","Gateway Stop Auto Config")%>
                                                </button>
                                            </h2>
                                            <div id="flush-GatewayStopAutoConfig" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Gateway/GatewayStopAutoConfig"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-GatewayUpdateFirmware" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Gateway Update Firmware","Gateway Update Firmware")%>
                                                </button>
                                            </h2>
                                            <div id="flush-GatewayUpdateFirmware" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Gateway/GatewayUpdateFirmware"); %>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>

                                <fieldset id="sensor" class="apioption" style="display: none">
                                    <div class="accordion accordion-flush" id="accordionSensor">
                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorResetDefaults" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Reset Defaults","Sensor Reset Defaults")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorResetDefaults" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorResetDefaults");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-RemoveSensor" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Remove Sensor","Remove Sensor")%>
                                                </button>
                                            </h2>
                                            <div id="flush-RemoveSensor" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/RemoveSensor");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-AssignSensor" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Assign Sensor","Assign Sensor")%>
                                                </button>
                                            </h2>
                                            <div id="flush-AssignSensor" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/AssignSensor");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorGet" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Get","Sensor Get")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorGet" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorGet");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorGetExtended" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Get Extended","Sensor Get Extended")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorGetExtended" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorGetExtended");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorApplicationIDGet" a-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor ApplicationID Get","Sensor ApplicationID Get")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorApplicationIDGet" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorApplicationIDGet");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorNameGet" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Name Get","Sensor Name Get")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorNameGet" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorNameGet");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorList" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor List","Sensor List")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorList" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorList");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorListFull" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor List Full","Sensor List Full")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorListFull" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorListFull");%>
                                                </div>
                                            </div>
                                        </div>

                                        <%--<div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorListExtended" aria-expanded="false">
                                                    Sensor List Extended
                                                </button>
                                            </h2>
                                            <div id="flush-SensorListExtended" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorListExtended");%>
                                                </div>
                                            </div>
                                        </div>--%>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NetworkIDFromSensorGet" a-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|NetworkID From Sensor Get","NetworkID From Sensor Get")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NetworkIDFromSensorGet" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/NetworkIDFromSensorGet");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorSetHeartbeat" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Set Heartbeat","Sensor Set Heartbeat")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorSetHeartbeat" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorSetHeartbeat");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorSetIP" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Set IP","Sensor Set IP")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorSetIP" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorSetIP");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorSetName" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Set Name","Sensor Set Name")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorSetName" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorSetName");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorSetThreshold" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Set Threshold","Sensor Set Threshold")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorSetThreshold" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorSetThreshold");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorSetCalibration" a-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Set Calibration","Sensor Set Calibration")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorSetCalibration" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorSetCalibration");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorGetCalibration" a-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Get Calibration","Sensor Get Calibration")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorGetCalibration" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorGetCalibration");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-CalibrationCertificateList" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Calibration Certificate List","Calibration Certificate List")%>
                                                </button>
                                            </h2>
                                            <div id="flush-CalibrationCertificateList" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/CalibrationCertificateList");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-CalibrationCertificateCreate" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Calibration Certificate Create","Calibration Certificate Create")%>
                                                </button>
                                            </h2>
                                            <div id="flush-CalibrationCertificateCreate" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/CalibrationCertificateCreate"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-CalibrationCertificateRemove" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Calibration Certificate Remove","Calibration Certificate Remove")%>
                                                </button>
                                            </h2>
                                            <div id="flush-CalibrationCertificateRemove" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/CalibrationCertificateRemove");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorSetAlerts" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Set Alerts","Sensor Set Alerts")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorSetAlerts" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorSetAlerts");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorSetTag" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Set Tag","Sensor Set Tag")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorSetTag" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorSetTag"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorAttributes" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Attributes","Sensor Attributes")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorAttributes" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorAttributes");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorAttributeSet" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Attribute Set","Sensor Attribute Set")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorAttributeSet" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorAttributeSet");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorAttributeRemove" a-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Attribute Remove","Sensor Attribute Remove")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorAttributeRemove" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorAttributeRemove");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorSendControlCommand" a-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Send Control Command","Sensor Send Control Command")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorSendControlCommand" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorSendControlCommand");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-GetDatumNameList" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Get Datum Name List","Get Datum Name List")%>
                                                </button>
                                            </h2>
                                            <div id="flush-GetDatumNameList" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/GetDatumNameList");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SetDatumName" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Set Datum Name","Set Datum Name")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SetDatumName" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SetDatumName");%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>

                                <fieldset id="groups" class="apioption" style="display: none">
                                    <div class="accordion accordion-flush" id="accordionGroups">
                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorGroupSensorList" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Group Sensor List","Sensor Group Sensor List")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorGroupSensorList" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorGroupSensorList");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorGroupList" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Group List","Sensor Group List")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorGroupList" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorGroupList");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorGroupCreate" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Group Create","Sensor Group Create")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorGroupCreate" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorGroupCreate");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorGroupDelete" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Group Delete","Sensor Group Delete")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorGroupDelete" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorGroupDelete");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorGroupAssignSensor" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Group Assign Sensor","Sensor Group Assign Sensor")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorGroupAssignSensor" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorGroupAssignSensor");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorGroupRemoveSensor" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Group Remove Sensor","Sensor Group Remove Sensor")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorGroupRemoveSensor" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Sensor/SensorGroupRemoveSensor");%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>

                                <fieldset id="datamessage" class="apioption" style="display: none">
                                    <div class="accordion accordion-flush" id="accordionDataMessage">
                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorDataMessages" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Data Messages","Sensor Data Messages")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorDataMessages" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("DataMessage/SensorDataMessages");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-GatewayMessages" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Gateway Messages","Gateway Messages")%>
                                                </button>
                                            </h2>
                                            <div id="flush-GatewayMessages" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("DataMessage/GatewayMessages");%>
                                                </div>
                                            </div>
                                        </div>

<%--                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-AccountRecentDataMessages" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Account Recent Data Messages","Account Recent Data Messages")%>
                                                </button>
                                            </h2>
                                            <div id="flush-AccountRecentDataMessages" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("DataMessage/AccountRecentDataMessages"); %>
                                                </div>
                                            </div>
                                        </div>--%>

<%--                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorRecentDataMessages" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Recent Data Messages","Sensor Recent Data Messages")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorRecentDataMessages" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("DataMessage/SensorRecentDataMessages");%>
                                                </div>
                                            </div>
                                        </div>--%>

<%--                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-AccountDataMessages" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Account Data Messages","Account Data Messages")%>
                                                </button>
                                            </h2>
                                            <div id="flush-AccountDataMessages" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("DataMessage/AccountDataMessages");%>
                                                </div>
                                            </div>
                                        </div>--%>

<%--                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorChartMessages" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Chart Messages","Sensor Chart Messages")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorChartMessages" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("DataMessage/SensorChartMessages");%>
                                                </div>
                                            </div>
                                        </div>--%>
                                    </div>
                                </fieldset>

                                <fieldset id="notification" class="apioption" style="display: none">
                                    <div class="accordion accordion-flush" id="accordionNotification">
                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-RecentlySentNotifications" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Recently Sent Notifications","Recently Sent Notifications")%>
                                                </button>
                                            </h2>
                                            <div id="flush-RecentlySentNotifications" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/RecentlySentNotifications"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SentNotifications" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sent Notifications","Sent Notifications")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SentNotifications" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/SentNotifications"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationRecipientList" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Recipient List","Notification Recipient List")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationRecipientList" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationRecipientList"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationSystemActionList" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification System Action List","Notification System Action List")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationSystemActionList" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationSystemActionList"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationControlUnitList" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Control Unit List","Notification Control Unit List")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationControlUnitList" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationControlUnitList"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationLocalNotifierList" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Local Notifier List","Notification Local Notifier List")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationLocalNotifierList" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationLocalNotifierList"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorAssignedToNotification" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Assigned To Notification","Sensor Assigned To Notification")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorAssignedToNotification" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/SensorAssignedToNotification"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-GatewayAssignedToNotification" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Gateway Assigned To Notification","Gateway Assigned To Notification")%>
                                                </button>
                                            </h2>
                                            <div id="flush-GatewayAssignedToNotification" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/GatewayAssignedToNotification"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-ToggleNotification" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Toggle Notification","Toggle Notification")%>
                                                </button>
                                            </h2>
                                            <div id="flush-ToggleNotification" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/ToggleNotification"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-AccountNotificationList" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Account Notification List","Account Notification List")%>
                                                </button>
                                            </h2>
                                            <div id="flush-AccountNotificationList" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/AccountNotificationList"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-SensorNotificationList" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Sensor Notification List","Sensor Notification List")%>
                                                </button>
                                            </h2>
                                            <div id="flush-SensorNotificationList" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/SensorNotificationList"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationScheduleList" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Schedule List","Notification Schedule List")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationScheduleList" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationScheduleList"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationDailyScheduleSet" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Daily Schedule Set","Notification Daily Schedule Set")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationDailyScheduleSet" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationDailyScheduleSet"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationYearlyScheduleList" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Yearly Schedule List","Notification Yearly Schedule List")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationYearlyScheduleList" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationYearlyScheduleList"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationYearlyScheduleSet" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Yearly Schedule Set","Notification Yearly Schedule Set")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationYearlyScheduleSet" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationYearlyScheduleSet"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationAssignSensor" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Assign Sensor","Notification Assign Sensor")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationAssignSensor" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationAssignSensor"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationRemoveSensor" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Remove Sensor","Notification Remove Sensor")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationRemoveSensor" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationRemoveSensor"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationAssignGateway" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Assign Gateway","Notification Assign Gateway")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationAssignGateway" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationAssignGateway"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationRemoveGateway" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Remove Gateway","Notification Remove Gateway")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationRemoveGateway" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationRemoveGateway"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationAssignRecipient" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Assign Recipient","Notification Assign Recipient")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationAssignRecipient" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationAssignRecipient"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationRemoveRecipient" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Remove Recipient","Notification Remove Recipient")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationRemoveRecipient" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationRemoveRecipient"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationAssignSystemAction" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Assign System Action","Notification Assign System Action")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationAssignSystemAction" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationAssignSystemAction"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationRemoveSystemAction" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Remove System Action","Notification Remove System Action")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationRemoveSystemAction" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationRemoveSystemAction"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationAssignControlUnit" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Assign Control Unit","Notification Assign Control Unit")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationAssignControlUnit" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationAssignControlUnit"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationRemoveControlUnit" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Remove Control Unit","Notification Remove Control Unit")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationRemoveControlUnit" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationRemoveControlUnit"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationAssignLocalNotifier" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Assign Local Notifier","Notification Assign Local Notifier")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationAssignLocalNotifier" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationAssignLocalNotifier"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationRemoveLocalNotifier" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Remove Local Notifier","Notification Remove Local Notifier")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationRemoveLocalNotifier" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationRemoveLocalNotifier"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationAcknowledge" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Acknowledge","Notification Acknowledge")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationAcknowledge" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationAcknowledge"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationFullReset" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Full Reset","Notification Full Reset")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationFullReset" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationFullReset"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationPause" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Pause","Notification Pause")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationPause" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationPause"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationUnpause" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Unpause","Notification Unpause")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationUnpause" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationUnpause"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationDelete" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification Delete","Notification Delete")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationDelete" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationDelete"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-ApplicationNotificationCreate" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Application Notification Create","Application Notification Create")%>
                                                </button>
                                            </h2>
                                            <div id="flush-ApplicationNotificationCreate" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/ApplicationNotificationCreate"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-ScheduledNotificationCreate" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Scheduled Notification Create","Scheduled Notification Create")%>
                                                </button>
                                            </h2>
                                            <div id="flush-ScheduledNotificationCreate" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/ScheduledNotificationCreate"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-BatteryNotificationCreate" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Battery Notification Create","Battery Notification Create")%>
                                                </button>
                                            </h2>
                                            <div id="flush-BatteryNotificationCreate" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/BatteryNotificationCreate"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-InactivityNotificationCreate" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Inactivity Notification Create","Inactivity Notification Create")%>
                                                </button>
                                            </h2>
                                            <div id="flush-InactivityNotificationCreate" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/InactivityNotificationCreate"); %>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>

                                <fieldset id="advanced" class="apioption" style="display: none">
                                    <div class="accordion accordion-flush" id="accordionAdvanced">
                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-AdvancedNotificationCreate" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Advanced Notification Create","Advanced Notification Create")%>
                                                </button>
                                            </h2>
                                            <div id="flush-AdvancedNotificationCreate" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/AdvancedNotificationCreate"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationListAdvancedTypes" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification List Advanced Types","Notification List Advanced Types")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationListAdvancedTypes" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationListAdvancedTypes"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationListAdvancedParameters" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification List Advanced Parameters","Notification List Advanced Parameters")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationListAdvancedParameters" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationListAdvancedParameters"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationListAdvancedParameterOptions" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification List Advanced Parameter Options","Notification List Advanced Parameter Options")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationListAdvancedParameterOptions" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationListAdvancedParameterOptions"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-AdvancedNotificationParameterSet" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Advanced Notification Parameter Set","Advanced Notification Parameter Set")%>
                                                </button>
                                            </h2>
                                            <div id="flush-AdvancedNotificationParameterSet" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/AdvancedNotificationParameterSet"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-AdvancedNotificationParameterList" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Advanced Notification Parameter List","Advanced Notification Parameter List")%>
                                                </button>
                                            </h2>
                                            <div id="flush-AdvancedNotificationParameterList" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/AdvancedNotificationParameterList"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-NotificationListAdvancedAutomatedSchedule" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Notification List Advanced Automated Schedule","Notification List Advanced Automated Schedule")%>
                                                </button>
                                            </h2>
                                            <div id="flush-NotificationListAdvancedAutomatedSchedule" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/NotificationListAdvancedAutomatedSchedule"); %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-AdvancedNotificationAutomatedScheduleSet" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Advanced Notification Automated Schedule Set","Advanced Notification Automated Schedule Set")%>
                                                </button>
                                            </h2>
                                            <div id="flush-AdvancedNotificationAutomatedScheduleSet" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("Notification/AdvancedNotificationAutomatedScheduleSet"); %>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>

                                <fieldset id="webhook" class="apioption" style="display: none">
                                    <div class="accordion accordion-flush" id="accordion***">
                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-WebHookGet" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Web Hook Get","Web Hook Get")%>
                                                </button>
                                            </h2>
                                            <div id="flush-WebHookGet" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("WebHook/WebHookGet");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-WebHookCreate" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Web Hook Create","Web Hook Create")%>
                                                </button>
                                            </h2>
                                            <div id="flush-WebHookCreate" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("WebHook/WebHookCreate");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-WebHookRemove" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Web Hook Remove","Web Hook Remove")%>
                                                </button>
                                            </h2>
                                            <div id="flush-WebHookRemove" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("WebHook/WebHookRemove");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-WebHookSetAuthentication" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Web Hook Set Authentication","Web Hook Set Authentication")%>
                                                </button>
                                            </h2>
                                            <div id="flush-WebHookSetAuthentication" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("WebHook/WebHookSetAuthentication");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-WebHookRemoveAuthentication" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Web Hook Remove Authentication","Web Hook Remove Authentication")%>
                                                </button>
                                            </h2>
                                            <div id="flush-WebHookRemoveAuthentication" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("WebHook/WebHookRemoveAuthentication");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-WebHookAddCookie" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Web Hook Add Cookie","Web Hook Add Cookie")%>
                                                </button>
                                            </h2>
                                            <div id="flush-WebHookAddCookie" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("WebHook/WebHookAddCookie");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-WebHookRemoveCookie" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Web Hook Remove Cookie","Web Hook Remove Cookie")%>
                                                </button>
                                            </h2>
                                            <div id="flush-WebHookRemoveCookie" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("WebHook/WebHookRemoveCookie");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-WebHookCookieList" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Web Hook Cookie List","Web Hook Cookie List")%>
                                                </button>
                                            </h2>
                                            <div id="flush-WebHookCookieList" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("WebHook/WebHookCookieList");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-WebHookAddHeader" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Web Hook Add Header","Web Hook Add Header")%>
                                                </button>
                                            </h2>
                                            <div id="flush-WebHookAddHeader" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("WebHook/WebHookAddHeader");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-WebHookRemoveHeader" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Web Hook Remove Header","Web Hook Remove Header")%>
                                                </button>
                                            </h2>
                                            <div id="flush-WebHookRemoveHeader" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("WebHook/WebHookRemoveHeader");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-WebHookHeaderList" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Web Hook Header List","Web Hook Header List")%>
                                                </button>
                                            </h2>
                                            <div id="flush-WebHookHeaderList" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("WebHook/WebHookHeaderList");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-WebHookResetBroken" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Web Hook Reset Broken","Web Hook Reset Broken")%>
                                                </button>
                                            </h2>
                                            <div id="flush-WebHookResetBroken" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("WebHook/WebHookResetBroken");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-WebHookAttempts" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Web Hook Attempts","Web Hook Attempts")%>
                                                </button>
                                            </h2>
                                            <div id="flush-WebHookAttempts" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("WebHook/WebHookAttempts");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-WebHookResend" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Web Hook Resend","Web Hook Resend")%>
                                                </button>
                                            </h2>
                                            <div id="flush-WebHookResend" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("WebHook/WebHookResend");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-WebHookAttemptBody" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Web Hook Attempt Body","Web Hook Attempt Body")%>
                                                </button>
                                            </h2>
                                            <div id="flush-WebHookAttemptBody" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("WebHook/WebHookAttemptBody");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-WebhookListNotificationSettings" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Web Hook List Notification Settings","Web Hook List Notification Settings")%>
                                                </button>
                                            </h2>
                                            <div id="flush-WebhookListNotificationSettings" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("WebHook/WebhookListNotificationSettings");%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-WebhookNotificationSettingsSet" aria-expanded="false">
                                                    <%: Html.TranslateTag("API/RestAPI|Web Hook Notification Settings Set","Web Hook Notification Settings Set")%>
                                                </button>
                                            </h2>
                                            <div id="flush-WebhookNotificationSettingsSet" class="accordion-collapse collapse">
                                                <div class="accordion-body">
                                                    <%Html.RenderPartial("WebHook/WebhookNotificationSettingsSet");%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-6 col-12 ps-lg-2 mb-4">
            <!-- Partial for the Request Explorer Tool-->
            <div id="requestExplorer">
                <%Html.RenderPartial("_Explorer"); %>
            </div>
            <!-- Partial for the Request Explorer Tool Results-->
            <div id="requestResult" class="mt-2">
                <%Html.RenderPartial("_ExplorerResult"); %>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        $('#HeaderSection').hide();
        $('#ParamSection').hide();
        $('#requestResult').hide();

        $(document).ready(function () {

            $('.codeSample').click(function (e) {
                e.preventDefault();
                newModal("Code Sample", $(this).attr("href"), 600, 800);
            });

            $('h3').click(function () {
                $(this).next().toggle();
            }).css("cursor", "pointer");

            //$('.methodDiv').css("display", "none").css('margin-left', '20px');
        });

        $('#apiselector').change(function () {
            $('.apioption').removeClass("selected");
            $('.apioption').hide();

            $('#' + $(this).val()).parent().addClass("selected");
            $('#' + $(this).val()).show();
        });

        $('#requestFormat').change(function () {
            AddRequestURL($('#endpoint').text());
        })

        $('#post_btn').click(function () {

            var url = $('#requestURL').val();
            var headers = {};
            var params = {};
            var type = GetRequestType();

            <% if (System.Diagnostics.Debugger.IsAttached)
        {  %>
            //temporary. localtesting only
            //if (type == "json") {
            //	url = 'http://localhost:64867/json/' + $('#endpoint').text();
            //}
            //else {
            //	url = 'http://localhost:64867/xml/' + $('#endpoint').text();
            //}
            <% } %>

            //Clear Results
            $('#pre_results').text('');
            $('#requestResult').hide();

            if ($('#secretCode_Val').val()) {
                headers["APISecretKey"] = $('#secretCode_Val').val();
                headers["APIKeyID"] = $('#keyValue_Val').val();
            }

            $('.paramval').each(function (index, inp) {
                //check to see if otional is checked				
                var chkbox = $(this).closest("tr").find('input[type="checkbox"]');
                var status = chkbox.prop('checked');

                if (status == true)
                    params[$(this).data("name")] = $(this).val()
            });
            //Set Error Lines on required fields that are empty
			SetRequiredInputs();

			$.ajax({
				url: url,
				headers: headers,
				method: 'POST',
				data: params
			})
				.done(function (data) {
					console.log('success/done');
					$('#requestResult').show();
					(type == "json") ? $('#pre_results').html(JSON.stringify(data, null, 1)) : $('#pre_results').text(xml_to_string(data));
					;
				}).
				fail(function (xhr, textStatus, errorThrown) {
					console.log('fail');
					$('#requestResult').show();
					$('#pre_results').text().length > 0 ? $('#pre_results').append('\n') : '';
					$('#pre_results').append(
						xhr.status + " (" + xhr.statusText + ")\n" +
						xhr.responseText);
				}).
				statusCode({
					429: function (xhr, textStatus, errorThrown) {
						console.log('statusCode: 429');
						$('#requestResult').show();
						$('#pre_results').text().length > 0 ? $('#pre_results').append('\n') : '';
						$('#pre_results').append(
							xhr.status + " (" + xhr.statusText + ")\n" +
							xhr.responseText);
					},
					500: function (xhr, textStatus, errorThrown) {
						console.log('statusCode: 500');
						$('#requestResult').show();

						$('#pre_results').text().length > 0 ? $('#pre_results').append('\n') : '';
						$('#pre_results').append(
							xhr.status + " (" + xhr.statusText + ")\n" +
							xhr.responseText);
					}
				}).always(function () {
					console.log('always');
					//Scroll screen to result
					$([document.documentElement, document.body]).animate({
						scrollTop: $('#ParamSection').offset().top + $('#ParamSection').outerHeight()
					}, 100);
				});
		})

        //$('.requiredStyle').keyup(function () {
        //	alert('up');
        //	if ($(this).val().length > 0) {
        //		$(this).removeClass("requiredStyle");
        //	}
        //});

        function APITest(json) {
            //Scroll screen to explorer
            $([document.documentElement, document.body]).animate({
                scrollTop: $("#explorer_top").offset().top
            }, 100);

            //Hide Header and Paramter tables
            $('#HeaderSection').hide();
            $('#ParamSection').hide();

            //Clear all current rows
            $("#HeaderTable td").remove();
            $("#ParamTable td").remove();
            $('#pre_results').text('');
            $('#requestResult').hide();

            //Set Endpoint and Request URL
            AddAuth(json.auth);
            AddRequestURL(json.api);

            //Create parameter rows
            $.each(json.params, function (idx, obj) {
                AddParam(obj.name, obj.type, obj.description, obj.optional);
            });

            //Enable Post button
            $('#post_btn').prop('disabled', false);
        }

        function AddRequestURL(endpoint) {
            if (endpoint == 'Choose an API from the dropdown')
                endpoint = '';
            else
                $('#endpoint').text(endpoint);

    <% if (Request.Url.Host == "localhost")
        { %>
            var url = 'http://<%:Request.Url.Host %>:<%:Request.Url.Port%>' + '/' + GetRequestType() + '/' + endpoint;
    <%  }
        else
        { %>
            var url = 'https://<%:Request.Url.Host %>' + '/' + GetRequestType() + '/' + endpoint;
    <% } %>
            $("#requestURL").val(url);
        }

        function AddAuth(val) {
            var secretkeyrow = '<tr>' +
                '<td style="max-width:12px;"><input class="form-check-input" type="checkbox" style="margin-top: 8px;" checked disabled></td>' +
                '<td><input class="form-control tableTextBox name" type="text" style="width: 100%; background-color: #FFF;" value="<%: Html.TranslateTag("APISecretKey","APISecretKey")%>" readonly></td>' +
                '<td><input class="form-control tableTextBox"   type="text" style="width: 100%;" id="secretCode_Val" placeholder="<%: Html.TranslateTag("API/RestAPI|This is the secret key you copied","This is the secret key you copied")%>" required></td>';

            var keyvaluerow = '<tr>' +
                '<td style="max-width:12px;"><input class="form-check-input" type="checkbox" style="margin-top: 8px;" checked disabled></td>' +
                '<td><input class="form-control tableTextBox name" type="text" style="width: 100%; background-color: #FFF;" value="<%: Html.TranslateTag("APIKeyID","APIKeyID")%>" readonly></td>' +
                '<td><input class="form-control tableTextBox" onkeyup="checkRequired(this)" type="text" style="width: 100%;" id="keyValue_Val" placeholder="<%: Html.TranslateTag("API/RestAPI|This can be found on your API Key","This can be found on your API Key")%>" required></td>';

            if (val == true) {
                $('#HeaderSection').show();
                $('#HeaderTable > tbody:last-child').append(keyvaluerow);
                $('#HeaderTable > tbody:last-child').append(secretkeyrow);
            }
        }

        function AddParam(name, type, description, optional) {

            $('#ParamSection').show();

            var disabled = optional ? '' : 'disabled';
            var required = optional ? '' : 'required';
            var checked = optional ? '' : 'checked';
            var inputType = type.toLowerCase() == "DateTime".toLowerCase() ? "datetime-local" : "text";

            var row = '<tr>' +
                '<td style="max-width:12px;"><input class="form-check-input" name="paramCheckBox" type="checkbox" style="margin-top: 8px;" ' + checked + ' ' + disabled + '></td>' +
                '<td><input class="form-control tableTextBox name" type="text" style="width: 100%; background-color: #FFF;" value="' + name + '" readonly></td>' +
                '<td><input class="form-control tableTextBox paramval" onkeyup="checkRequired(this)" type="' + inputType + '" style="width: 100%;" data-name="' + name + '" value="" placeholder="' + type + '" ' + required + ' name="' + description + '"' + '></td>';

            $('#ParamTable > tbody:last-child').append(row);
        }

        function GetRequestType() {
            var type = $('#requestFormat').prop('checked');
            type = (type == true) ? "xml" : "json";

            return type;
        }

        function SetRequiredInputs() {
            $('#post_request_parameters').find('input').each(function () {
                if ($(this).prop('required') && $(this).val().length === 0) {
                    $(this).addClass("requiredStyle");
                }
            });
        }

        function checkRequired(input) {
            if ($(input).hasClass('requiredStyle')) {
                $(input).removeClass('requiredStyle')
            }
        }

        function xml_to_string(xml_node) {
            if (xml_node.xml)
                return xml_node.xml;
            else if (XMLSerializer) {
                var xml_serializer = new XMLSerializer();
                return xml_serializer.serializeToString(xml_node);
            }
            else {
                showSimpleMessageModal("<%=Html.TranslateTag("Error: Extremely old browser")%>");
                return "";
            }
        }

	</script>

    <style type="text/css">
        .warningBoxAB {
            border: solid 1px var(--help-highlight-color);
            border-radius: 1rem;
            margin-bottom: 1rem;
            display: flex;
            flex-direction: row;
            align-items: center;
        }

        .warningIconAB {
            padding-left: 1rem;
        }

            .warningIconAB svg {
                width: 30px;
                height: 30px;
                fill: var(--help-highlight-color);
            }

        .accordion-button {
            padding: 10px;
        }

        .requiredStyle {
            border: 1px solid #E53935 !important;
        }

        h3 {
            font-family: 'Open Sans Condensed', sans-serif;
            font-size: 16px;
            font-weight: 700;
            line-height: 5px;
            margin: 0 0 0;
            padding: 12px 0px;
        }

        fieldset {
            margin-left: 10px;
        }

        pre {
            max-height: 400px;
            overflow: auto;
        }

        a:hover {
            cursor: pointer;
        }

        .accordion-collapse:hover ::-webkit-scrollbar-thumb {
            -webkit-border-radius: 10px;
            border-radius: 10px;
            background: #bbb;
        }

        .apioption {
            margin-left: 0;
        }
    </style>

</asp:Content>
