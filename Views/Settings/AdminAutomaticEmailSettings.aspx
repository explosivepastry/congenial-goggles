<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminAutomaticEmailSettings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <%if (MonnitSession.IsCurrentCustomerMonnitAdmin)
            { %>
        <div class="card_container shadow-sm rounded mt-4">
            <div class="row">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <%: Html.TranslateTag("Settings/AdminAutomaticEmailSettings|Automatic Email Settings","Automatic Email Settings")%>
                    </div>
                </div>
                <div class="card_container__body">
                        <form action="/Settings/EditSiteConfigs" method="post">
                            <%: Html.Hidden("formName", "AutomatedEmails")%>
                            <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3">
                                    <%: Html.TranslateTag("Settings/AdminAutomaticEmailSettings|Send Inactivity Notifications","Send Inactivity Notifications")%>
                                </div>
                                <div class="col sensorEditFormInput">
                                <select name="SendInactivityNotifications" class="form-select">
                                    <option value="False"><%: Html.TranslateTag("False","False")%></option>
                                    <option value="True" <%: ConfigData.AppSettings("SendInactivityNotifications").ToBool() ? "selected=selected" : ""%>><%: Html.TranslateTag("True","True")%></option>
                                </select>
                                </div>
                            </div>
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3">
                                    <%: Html.TranslateTag("Settings/AdminAutomaticEmailSettings|Send Subscription Notifications","Send Subscription Notifications")%>
                                </div>
                                <div class="col sensorEditFormInput">
                                <select name="SendSubscriptionNotifications" class="form-select">
                                    <option value="False"><%: Html.TranslateTag("False","False")%></option>
                                    <option value="True" <%: ConfigData.AppSettings("SendSubscriptionNotifications").ToBool() ? "selected=selected" : ""%>><%: Html.TranslateTag("True","True")%></option>
                                </select>
                                </div>
                            </div>
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3">
                                    <%: Html.TranslateTag("Settings/AdminAutomaticEmailSettings|Send Maintenance Notifications","Send Maintenance Notifications")%>
                                </div>
                                <div class="col sensorEditFormInput">
                                <select name="SendMaintenanceNotifications" class="form-select">
                                    <option value="False"><%: Html.TranslateTag("False","False")%></option>
                                    <option value="True" <%: ConfigData.AppSettings("SendMaintenanceNotifications").ToBool() ? "selected=selected" : ""%>><%: Html.TranslateTag("True","True")%></option>
                                </select>
                                </div>
                            </div>
                            <% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
                                { %>
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3">
                                    <%: Html.TranslateTag("Settings/AdminAutomaticEmailSettings|Send Advertisements","Send Advertisements")%>
                                </div>
                                <div class="col sensorEditFormInput">
                                <select name="SendAds" class="form-select">
                                    <option value="False"><%: Html.TranslateTag("False","False")%></option>
                                    <option value="True" <%: ConfigData.AppSettings("SendAds").ToBool() ? "selected=selected" : ""%>><%: Html.TranslateTag("True","True")%></option>
                                </select>
                                </div>
                            </div>
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3">
                                    <%: Html.TranslateTag("Settings/AdminAutomaticEmailSettings|Send Scheduled Reports","Send Scheduled Reports")%>
                                </div>
                                <div class="col sensorEditFormInput">
                                <select name="RunScheduledReports" class="form-select">
                                    <option value="False"><%: Html.TranslateTag("False","False")%></option>
                                    <option value="True" <%: ConfigData.AppSettings("RunScheduledReports").ToBool() ? "selected=selected" : ""%>><%: Html.TranslateTag("True","True")%></option>
                                </select>
                                </div>
                            </div>
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3">
                                    <%: Html.TranslateTag("Settings/AdminAutomaticEmailSettings|Report Processing Plug-in Folder","Report Processing Plug-in Folder")%>
                                </div>
                                <div class="col sensorEditFormInput">
                                <input type="text" class="form-control" name="ReportProcessingPluginPath" value="<%: ConfigData.AppSettings("ReportProcessingPluginPath")%>" />
                                </div>
                            </div>
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3">
                                    <%: Html.TranslateTag("Settings/AdminAutomaticEmailSettings|Aws Ses Config Set","Aws Ses Config Set")%>
                                </div>
                                <div class="col sensorEditFormInput">
                                <input type="text" class="form-control" name="AwsSesConfigSet" value="<%: ConfigData.AppSettings("AwsSesConfigSet")%>" />
                                </div>
                            </div>
                            <%} %>
                            <div class="clearfix"></div>
                        </form>
                </div>
                <div class="text-end">
                    <button id="prefSave" type="submit" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary mt-2">
                        <%: Html.TranslateTag("Save","Save")%>
                    </button>
                </div>
            </div>
        </div>
        <%} %>
    </div>
</asp:Content>
