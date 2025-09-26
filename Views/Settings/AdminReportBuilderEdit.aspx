<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.ReportQuery>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminReportBuilderEdit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <!-- Edit Form -->
        <div class="x_panel shadow-sm rounded col-md-6 col-12 my-4">
            <div class="card_container__top__title d-flex justify-content-between">
                <%: Html.TranslateTag("Settings/AdminReportBuilderDelete|Delete Report","Delete Report")%>
                <a href="/Settings/AdminReportBuilderDelete/<%:Model.ReportQueryID%>" class="btn btn-light mb-2 d-flex align-items-center" title="<%: Html.TranslateTag("Delete","Delete")%>">
                    <span class="media_desktop me-1"><%: Html.TranslateTag("Settings/AdminReportBuilderDelete|Delete Report","Delete Report")%></span>
                        <%=Html.GetThemedSVG("delete") %>
                </a>
            </div>
            <div class="">
                <form id="prefForm" class="form-horizontal form-label-left" method="post">
                    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                    <% Html.RenderPartial("_AdminReportBuilderForm", Model); %>
                    <div class="clearfix"></div>
                    <div class="text-end">
                        <a href="/Settings/AdminReportBuilder/" class="btn bt-light">
                            <%: Html.TranslateTag("Cancel","Cancel")%>
                        </a>
                        <button id="prefSave" type="submit" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary">
                            <%: Html.TranslateTag("Save","Save")%>
                        </button>
                    </div>
                </form>
            </div>
        </div>
        <!-- Parameters -->
        <div class="x_panel shadow-sm rounded col-md-6 col-12">
            <div class="card_container__top__title d-flex justify-content-between">
                <h2><%: Html.TranslateTag("Settings/AdminReportBuilderEdit|Parameters","Parameters")%></h2>
                <%if (MonnitSession.IsCurrentCustomerMonnitAdmin)
                    { %>
                <a href="/Settings/AdminReportParameterCreate/<%: Model.ReportQueryID%>" class="btn btn-secondary text-nowrap">
                    <svg xmlns="http://www.w3.org/2000/svg" width="14" height="14" viewBox="0 0 16 15.999">
                        <path id="Union_1" data-name="Union 1" d="M7,16V9H0V7H7V0H9V7h7V9H9v7Z" fill="#FFF" />
                    </svg>
                    <%: Html.TranslateTag("Settings/AdminReportBuilderEdit|New Parameter","New Parameter")%>
                </a>
                <%}%>
            </div>

            <table class="table table-hover">
                <thead>
                    <tr>
                        <th scope="col"><%: Html.TranslateTag("Type","Type")%></th>
                        <th scope="col"><%: Html.TranslateTag("Name","Name")%></th>
                        <th scope="col"><%: Html.TranslateTag("Label","Label")%></th>
                        <th scope="col"><%: Html.TranslateTag("Value","Value")%></th>
                        <th scope="col"></th>
                    </tr>
                </thead>
                <tbody id="dragAndDrop">
                    <%foreach (ReportParameter parameter in Model.Parameters)
                        {%>
                    <tr class="sortable">
                        <td><%= ReportParameterType.Load(parameter.ReportParameterTypeID).Name %></td>
                        <td><%= parameter.ParamName %></td>
                        <td><%= parameter.LabelText %></td>
                        <td><%: parameter.DefaultValue %></td>
                        <td class="d-flex align-items-center text-end">
                            <a href="/Settings/AdminReportParameterEdit/<%:parameter.ReportParameterID%>">
                                <i class="fa fa-edit" title="<%: Html.TranslateTag("Edit","Edit")%>" style="font-size: 18px; padding-top: 3px;"></i>
                            </a>
                            <a href="/Settings/AdminReportParameterDelete/<%:parameter.ReportParameterID%>" class="ms-2" title="<%: Html.TranslateTag("Delete","Delete")%>">
                                <%=Html.GetThemedSVG("delete") %>
                            </a>
                        </td>
                    </tr>
                    <% } %>
                </tbody>
            </table>
        </div>
    </div>
    <style>
        .placeholder {
            border: 2px solid #c72129;
            background-color: white;
            -webkit-box-shadow: 0px 0px 10px #888;
            -moz-box-shadow: 0px 0px 10px #888;
            box-shadow: 0px 0px 10px #888;
        }

        .grid {
            margin-top: .5em;
        }
    </style>

    <script>

        SetPosition();

        function removeReport(item) {
            let values = {};
            values.url = `/Settings/AdminReportBuilderDelete/<%:Model.ReportQueryID%>`;
            values.text = "<%: Html.TranslateTag("Settings/AdminReportBuilderEdit|Are you sure you want to delete this report?","Are you sure you want to delete this report?")%>";
            values.redirect = '/Settings/AdminReportBuilder';
            openConfirm(values);
        }

        $(function () {
            $('#dragAndDrop').sortable({
                tolerance: 'pointer',
                revert: 'invalid',
                placeholder: 'span well placeholder tile',
                forceHelperSize: true,
                start: function (event, ui) {
                    ui.item.startPos = ui.item.index();
                },
                stop: function (event, ui) {
                    var start = ui.item.startPos;
                    var end = ui.item.index();
                    $.post('/Settings/AdminReportParameterMove/', { id: <%:Model.ReportQueryID%>, start: start, end: end }, function (data) {
                        //alert("success");
                    });
                }
            });
        })

        function SetPosition() {
            $.post('/Settings/AdminReportParameterSortOrder/', { id: <%:Model.ReportQueryID%> }, function (data) {
                //alert("success");
            });
        }

    </script>

    <style>
        .sortable:hover {
            background: #eee;
        }
    </style>
</asp:Content>
