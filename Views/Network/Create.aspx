<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<iMonnit.Models.CreateNetworkModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">

        <%
            string response = "";
            response = ViewBag.EditResponse == null ? "" : ViewBag.EditResponse;
        %>

        <!-- Event Info Panel -->
        <%--<div class="top-add-btn-row-left">
            <a class="btn btn-primary" id="cancel" href="/Network/List">
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 16 16">
                    <g id="Symbol_83" data-name="Symbol 83" transform="translate(16 16) rotate(180)">
                        <path id="Path_10" data-name="Path 10" d="M8,0,6.545,1.455l5.506,5.506H0V9.039H12.052L6.545,14.545,8,16l8-8Z" fill="#fff" />
                    </g>
                </svg>

                <span class="media_desktop">&nbsp; &nbsp;
                    <%: Html.TranslateTag("Network/Create|Network List","Network List")%>
                </span>
            </a>
        </div>--%>
        <div class="col-12">

            <div class="rule-card_container w-50">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <%: Html.TranslateTag("Network/Create|Create New Network","Create New Network")%>
                    </div>
                    <div class="clearfix"></div>
                </div>

                <div class="card_container__body">
                    <div class="card_container__body__content">
                        <% using (Html.BeginForm())
                            { %>
                        <%=Html.ValidationSummary(true) %>
                        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>

                        <div class="row sensorEditForm">
                            <label class="col-12 col-md-3 col-lg-2" for="networkName">
                                <%: Html.TranslateTag("Network/Create|Network Name","Network Name")%>
                            </label>

                            <div class="col sensorEditFormInput">
                                <%: Html.TextBoxFor(model => model.Name,new { @class = "form-control" })%><br />
                                <%: Html.ValidationMessageFor(model => model.Name)%>
                            </div>
                        </div>
                         <!-- End Half Form -->

                        <div class="clearfix"></div>

                        <div class="col-12 text-end">
                            <div class="col-12 col-md-9" id="saveMessage" style="color: red; font-size: 1.2em;"><%= response %></div>
                            <br />
                            <button id="SaveBtn" onclick="$(this).hide();$('#saving').show();" type="submit" class="btn btn-primary">
                                <%: Html.TranslateTag("Network/Create|Save","Save")%>
                            </button>

                            <button class="btn btn-primary" id="saving" style="display: none;" type="button" disabled>
                                <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                <%: Html.TranslateTag("Network/Create|Saving...","Saving...")%>
                            </button>
                        </div>
                        <%} %>
                        <%--end form --%>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';

        $(document).ready(function () {
            $('#SaveBtn').click(function (e) {
                $('#saveMessage').html("");
                var netName = $('#Name').val().replace(/[<>]/g, '');
                $('#Name').val(netName);
            });
        });
    </script>

</asp:Content>
