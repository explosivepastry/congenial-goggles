<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<CustomerContactInfoModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="col-md-12 col-xs-12">
            <%Html.RenderPartial("UserLink", Model.Customer); %>
        </div>
    </div>

    <% using (Html.BeginForm())
       {%>

    <div class="row">
        <div class="col-xs-12">
            <div class="container">
                <div class="x_panel">
                    <div class="x_title">
                        <h2><%:Html.TranslateTag("Settings/UserEdit|Edit User Information","Edit User Information")%></h2>
                        <div class="clearfix"></div>
                    </div>

                    <div class="x_content">

                        <form class="form-horizontal form-label-left">

                            <%: Html.ValidationSummary(true) %>
                            <%: Html.Hidden("AccountID",Model.Customer.AccountID) %>

                            <div class="form-group row">
                                <div class="bold col-md-2 col-sm-2 col-xs-12">
                                    <%:Html.TranslateTag("User Name","User Name")%>:
                                </div>
                                <div class="col-md-6 col-sm-6 col-xs-12 lgBox">
                                    <% if (MonnitSession.CustomerCan("Customer_Change_Username"))
                                       { %>
                                    <%: Html.TextBox("UserName", Model.Customer.UserName)%>
                                    <%}
                                       else
                                       {%>
                                    <%: Model.Customer.UserName %>
                                    <%} %>
                                    <div class="editor-error">
                                        <%: Html.ValidationMessageFor(model => model.Customer.UserName)%>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="bold col-md-2 col-sm-2 col-xs-12">
                                    <%:Html.TranslateTag("Password","Password")%>:
                                </div>
                                <div class="col-md-6 col-sm-6 col-xs-12 lgBox">
                                    <% if (Model.Customer.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
                                       { %>
                                    <a href="/Account/ChangePassword" class="btn btn-grey"><%:Html.TranslateTag("Change password","Change password")%></a>
                                    <%} %>
                                    <% if (MonnitSession.CustomerCan("Customer_Reset_Password_Other"))
                                       { %>
                                    <a href="#" onclick="defaultPassword('<%:Model.Customer.CustomerID  %>'); return false;" class="btn btn-grey"><%:Html.TranslateTag("Settings/UserEdit|Reset password","Reset password")%></a>
                                    <%} %>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="bold col-md-2 col-sm-2 col-xs-12">
                                    <%:Html.TranslateTag("First Name","First Name")%>
                                </div>
                                <div class="col-md-6 col-sm-6 col-xs-12 lgBox">
                                    <%: Html.TextBox("FirstName", Model.Customer.FirstName) %>
                                    <div class="editor-error">
                                        <%: Html.ValidationMessageFor(model => model.Customer.FirstName) %>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="bold col-md-2 col-sm-2 col-xs-12">
                                    <%:Html.TranslateTag("Last Name","Last Name")%>
                                </div>
                                <div class="col-md-6 col-sm-6 col-xs-12 lgBox">
                                    <%: Html.TextBox("LastName", Model.Customer.LastName) %>
                                    <div class="editor-error">
                                        <%: Html.ValidationMessageFor(model => model.Customer.LastName) %>
                                    </div>
                                </div>
                            </div>

                            <%if (MonnitSession.CurrentCustomer.IsAdmin)
                              { %>
                            <div class="form-group row">
                                <div class="bold col-md-2 col-sm-2 col-xs-12">
                                    <%:Html.TranslateTag("Settings/UserEdit|Is Admin","Is Admin")%>
                                </div>
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                    <input type="checkbox" name="IsAdmin" <%:Model.Customer.IsAdmin  ? "checked='checked'" : ""%> />
                                </div>
                            </div>
                            <%} %>

                            <div class="form-group row">
                                <div class="bold col-md-2 col-sm-2 col-xs-12">
                                </div>
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                    <input type="button" onclick="postForm($(this).closest('form'), function (data) { if (data == 'Success') { $('.refreshPic:visible').click(); } });" value="<%:Html.TranslateTag("Save","Save")%>" class="btn btn-primary" />
                                </div>
                            </div>
                        </form>

                        <div style="clear: both;"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <% } %>

    <script type="text/javascript">

        function defaultPassword(custID) {
            $.get('/Customer/DefaultPassword/' + custID, function (data) {
                eval(data);
            });
        }

        function goBack() {

            window.history.back();
        }




    </script>

</asp:Content>
