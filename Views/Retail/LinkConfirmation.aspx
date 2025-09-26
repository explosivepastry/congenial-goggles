<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Error.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    LinkConfirmation
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div class="container" style="padding: 30px;">


        <div class="col-md-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2><span class="fa fa-link"></span>&nbsp;<%=Html.TranslateTag("Link Successful","Link Successful") %></h2>

                    <div class="clearfix"></div>
                </div>
                <div class="x_content bold" id="errorMessage">
                    <div style="font-size: 1.4em;color: red;">
                        <%=Html.TranslateTag("Your accounts are now linked, Thank you.","Your accounts are now linked, Thank you.") %>
                        <br />
                    </div>
                    <div style="font-size: 1em">
                        <br />
                        <%=Html.TranslateTag("Please close this page and continue browsing in your original.","Please close this page and continue browsing in your original.") %>
                    </div>
                </div>

            </div>

        </div>
    </div>



</asp:Content>
