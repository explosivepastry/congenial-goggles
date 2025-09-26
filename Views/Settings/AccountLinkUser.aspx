<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<AddExistingUserAccountModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AccountLinkUser
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div class="container-fluid">
        <div class="col-md-12 col-xs-12">
            <%Html.RenderPartial("UserLink", MonnitSession.CurrentCustomer); %>
        </div>

        <div class="display-flex">
            <div class="col-md-6 col-12">
                <div class="x_panel gridPanel shadow-sm rounded">
                    <div class="card_container__top">
                        <div class="card_container__top__title"><%: Html.TranslateTag("Settings/_AccountLinkUser|Link Existing User","Link Existing User")%></div>
                    </div>

                    <div class="x_content">

                        <form id="linkform" class="form-horizontal form-label-left">

                            <%--                            <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>--%>
                            <input type="hidden" id="AccountID" name="AccountID" value="<%: Model.AccountID %>" />

                            <div class="form-group row">
                                <div class="bold" style="padding-left: 10px;">
                                    <%: Html.TranslateTag("Settings/_AccountLinkUser|Enter the E-mail address of the user to be linked to this account. The user will receive a Link Request via their web portal, The User can accept the Link Request by selecting the User (silhouette icon) > Linked Accounts > Green Check. Once accepted, the User will be linked.")%>
                                </div>
                                <div class="col-xs-12">
                                    <div class="form-group">
                                    </div>
                                </div>
                            </div>

                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3">
                                    <%=Html.TranslateTag("Overview/CreateAccount|Email Address","Email Address")%>:
                                </div>
                                <div class="col sensorEditFormInput">
                                    <input id="email" class="form-control" name="email" required="required" type="text" value="">
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="text-end">
                                    <input type="button" onclick="window.location.href = '/Settings/AccountUserList'" value="<%: Html.TranslateTag("Cancel","Cancel")%>" class="btn btn-secondary" style="width: 80px;" />
                                    <input id="add" type="button" value="<%: Html.TranslateTag("Submit","Submit")%>" class="btn-primary btn" />
                                </div>
                            </div>
                        </form>
                        <div style="clear: both;"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(function () {
            $('#add').click(function (e) {
                e.preventDefault();
                if (('#email').length > 0) {


                    var form = $('#linkform').serialize()

                    $.post('/Settings/AccountLinkUser', form, function (data) {
                        $('.backendMessage').html(data);
                        toastBuilder(data);
                    })
                }
            });
        });
    </script>



</asp:Content>

