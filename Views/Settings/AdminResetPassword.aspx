<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<iMonnit.Models.AdminResetPasswordModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Admin Settings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <div class="col-12">
            <%Html.RenderPartial("UserLink", MonnitSession.CurrentCustomer); %>
        </div>
        <div class="display-flex">
            <div class="col-12">
                <div class="x_panel gridPanel shadow-sm rounded">
                    <div class="x_title">
                        <h2><%: Html.TranslateTag("Settings/AdminResetPassword|Reset Password","Reset Password")%></h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <form id="passwordForm" class="form-horizontal form-label-left">
                            <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>

                            <input type="hidden" name="CustomerID" value="<%=Model.CustomerID%>" />
                            <div class="ms-2 mb-4">
                                <div class="bold col-12">
                                    <%: Html.TranslateTag("Settings/AdminResetPassword|New passwords are required to be a minimum of","New passwords are required to be a minimum of")%> <%: ViewData["PasswordLength"]%> <%: Html.TranslateTag("Settings/AdminResetPassword|characters in length.","characters in length.")%>
                                </div>
                                <div class="col-12">
                                    <%: Html.ValidationSummary(true, Html.TranslateTag("Settings/AdminResetPassword|Password change was unsuccessful. Please correct the errors and try again.","Password change was unsuccessful. Please correct the errors and try again.")) %>
                                </div>
                            </div>

                            <div class="row sensorEditForm">
                                <div class=" col-12 col-md-3 col-lg-2">
                                    <%: Html.TranslateTag("Settings/AdminResetPassword|New Password","New Password")%>:
                                </div>
                                <div class="col sensorEditFormInput">
                                    <div class="form-group">
                                        <input autocomplete="off" id="NewPassword" name="NewPassword" type="password" class="form-control" autofocus  />
                                    </div>
                                    <div class="editor-error" style="color: red;">
                                        <%: Html.ValidationMessageFor(m => m.NewPassword) %>
                                    </div>
                                </div>
                            </div>
                            <div class="row sensorEditForm">
                                <div class=" col-12 col-md-3 col-lg-2">
                                    <%: Html.TranslateTag("Settings/AdminResetPassword|Confirm Password","Confirm Password")%>:
                                </div>
                                <div class="col sensorEditFormInput">
                                    <div class="form-group">
                                        <input autocomplete="off" id="ConfirmPassword" name="ConfirmPassword" type="password" class="form-control" />
                                    </div>
                                    <div class="editor-error" style="color: red;">
                                        <%: Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                                    </div>
                                </div>
                            </div>
                            <div class="row sensorEditForm">
                                <div class="col-12 col-sm-3">
                                </div>
                                <div class="col-12 d-flex justify-content-end">
                                    <input onclick="goBack();" value="<%: Html.TranslateTag("Cancel","Cancel")%>" class="btn" style="width: 90px;" />
                                    <button
                                        onclick="event.preventDefault(); confirmModal()"
                                        class="btn btn-secondary"
                                        id="adminResetPassword">
                                        <%: Html.TranslateTag("Update", "Update")%>
                                    </button>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        function showLoading() {
            $('#update').show();
            $('#updateBtn').hide();
        }

        function confirmModal() {
            let values = {};
            values.text = "<%: Html.TranslateTag("This will change the users password, are you sure you want to continue?")%>";
            values.redirect = `/Settings/UserDetail/<%=Model.CustomerID%>`;
            values.url = `/Settings/AdminResetPassword/?${$("#passwordForm").serialize()}`
            openConfirm(values);
        }
    </script>

    <style>
        .form-control {
            width: 300px;
        }
    </style>

    <script>
        function goBack() {
            window.history.back();
        }
    </script>

</asp:Content>
