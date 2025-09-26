<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminExceptionLogging
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <div class="card_container shadow-sm rounded mt-4">
            <div class="row">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <%: Html.TranslateTag("Settings/AdminExceptionLogging|Exception Logging","Exception Logging")%>
                    </div>
                </div>
            </div>
            <div class="card_container__body">
                <div class="card_container__body__content">
                    <form action="/Settings/EditSiteConfigs" method="post">
                        <%: Html.Hidden("formName", "ExceptionLogging")%>
                        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>

                        <%if (MonnitSession.IsCurrentCustomerMonnitAdmin)
                            { %>
                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3">
                                <%: Html.TranslateTag("Settings/AdminExceptionLogging|Log Unknown Gateways","Log Unknown Gateways")%>
                            </div>
                            <div class="col sensorEditFormInput">
                                <select name="LogUnknownGatewaysAsException" class="form-select">
                                    <option value="False"><%: Html.TranslateTag("False","False")%></option>
                                    <option value="True" <%: ConfigData.AppSettings("LogUnknownGatewaysAsException").ToBool() ? "selected=selected" : ""%>><%: Html.TranslateTag("True","True")%></option>
                                </select>
                            </div>
                        </div>
                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3">
                                <%: Html.TranslateTag("Settings/AdminExceptionLogging|Log External Subscription","Log External Subscription")%>
                            </div>
                            <div class="col sensorEditFormInput">
                                <select name="LogExternalSubscriptionAsException" class="form-select">
                                    <option value="False"><%: Html.TranslateTag("False","False")%></option>
                                    <option value="True" <%: ConfigData.AppSettings("LogExternalSubscriptionAsException").ToBool() ? "selected=selected" : ""%>><%: Html.TranslateTag("True","True")%></option>
                                </select>
                            </div>
                        </div>
                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3">
                                <%: Html.TranslateTag("Settings/AdminExceptionLogging|Log Bad Web Path","Log Bad Web Path")%>
                            </div>
                            <div class="col sensorEditFormInput">
                                <select name="LogBadPathAsException" class="form-select">
                                    <option value="False"><%: Html.TranslateTag("False","False")%></option>
                                    <option value="True" <%: ConfigData.AppSettings("LogBadPathAsException").ToBool() ? "selected=selected" : ""%>><%: Html.TranslateTag("True","True")%></option>
                                </select>
                            </div>
                        </div>
                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3">
                                <%: Html.TranslateTag("Settings/AdminExceptionLogging|Inbound Log 0 or Max","Inbound Log 0 or Max")%>
                            </div>
                            <div class="col sensorEditFormInput">
                                <select name="InboundLog0orMax" class="form-select">
                                    <option value="False"><%: Html.TranslateTag("False","False")%></option>
                                    <option value="True" <%: ConfigData.AppSettings("InboundLog0orMax").ToBool() ? "selected=selected" : ""%>><%: Html.TranslateTag("True","True")%></option>
                                </select>
                            </div>
                        </div>
                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3">
                                <%: Html.TranslateTag("Settings/AdminExceptionLogging|API Usage","API Usage")%>
                            </div>
                            <div class="col sensorEditFormInput">
                                <select name="APIUsage" class="form-select">
                                    <option value="False"><%: Html.TranslateTag("False","False")%></option>
                                    <option value="True" <%: ConfigData.AppSettings("APIUsage").ToBool() ? "selected=selected" : ""%>><%: Html.TranslateTag("True","True")%></option>
                                </select>
                            </div>
                        </div>
                        <div class="text-end">
                            <button type="submit" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary mt-2">
                                <%: Html.TranslateTag("Save","Save")%>
                            </button>
                            <span style="color: red; display: inline-block;"><%:ViewBag.Result ?? ""%></span>
                        </div>
                        <%}%>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
