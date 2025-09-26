<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.SVGIcon>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ImportSVG
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="importSVG" class="container-fluid">
    <h2 class="mt-4">Import SVG</h2>
    <div id="fullForm" style="width: 100%;">
        <div class="col-12">
            <div class="x_panel shadow-sm rounded">
                <form id="prefForm" class="form-horizontal form-label-left" action="/Settings/ImportSVG" method="post">
                    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                    <%if (MonnitSession.IsCurrentCustomerMonnitAdmin)
                      { %>
                    <div class="formBody">
                        <div class="x_content d-flex flex-column">
                            <div class="row sensorEditForm">
                                <div class="col-12 col-sm-3">
                                    <%: Html.LabelFor(m => m.Name, Html.TranslateTag("Name","Name")) %>
                                    <%: Html.ValidationMessageFor(m => m.Name) %>
                                </div>
                                <div class="col-12 col-sm-9 sensorEditFormInput">
                                    <%: Html.TextBoxFor(model => model.Name, new { @class = "form-control" }) %>
                                </div>
                            </div>
                            <div class="row sensorEditForm">
                                <div class="col-12 col-sm-3">
                                    <%: Html.LabelFor(m => m.ImageKey, Html.TranslateTag("Settings/AdminSettings|Image Key","Image Key")) %>
                                    <%: Html.ValidationMessageFor(m => m.ImageKey) %>
                                </div>
                                <div class="col-12 col-sm-9 sensorEditFormInput">
                                    <%: Html.TextBoxFor(model => model.ImageKey, new { @class = "form-control" }) %>
                                </div>
                            </div>
                            <div class="row sensorEditForm">
                                <div class="col-12 col-sm-3">
                                    <%: Html.LabelFor(m => m.Category, Html.TranslateTag("Settings/AdminSettings|Category","Category")) %>
                                    <%: Html.ValidationMessageFor(m => m.Category) %>
                                </div>
                                <div class="col-12 col-sm-9 sensorEditFormInput">
                                    <%: Html.DropDownListFor(model => model.Category, 
                                            new SelectList(iMonnit.Controllers.SettingsController.GetSystemIconEnumDict().Values),
                                            new { @class = "form-select" }) %>
                                </div>
                            </div>
                            <div class="row sensorEditForm">
                                <div class="col-12 col-sm-3">
                                    <%: Html.LabelFor(m => m.HTMLCode, Html.TranslateTag("Settings/AdminSettings|HTML Code","HTML Code")) %>
                                    <%: Html.ValidationMessageFor(m => m.HTMLCode) %>
                                </div>
                                <div class="col-12 col-sm-9 sensorEditFormInput">
                                    <%: Html.TextAreaFor(model => model.HTMLCode, new { @rows="20", style="max-width:100%", @class = "form-control" }) %>
                                </div>
                            </div>

                            <div class="row sensorEditForm">
                                <div class="col-12 col-sm-3">
                                    <%: Html.LabelFor(m => m.IsDefault, Html.TranslateTag("Settings/AdminSettings|Image Default","Image Default")) %>
                                </div>
                                <div class="col-12 col-sm-9 sensorEditFormInput">
                                    <%: Html.CheckBoxFor(m => m.IsDefault, new { @class = "form-check-input" }) %>
                                </div>
                            </div>
                            <div class="row sensorEditForm">
                                <div class="col-12 col-sm-3">
                                    <%: Html.LabelFor(m => m.ApplyTheme, Html.TranslateTag("Settings/AdminSettings|Add To My Theme","Add To My Theme")) %>
                                </div>
                                <div class="col-12 col-sm-9 form-check form-switch">
                                    <%--<% var c = AccountTheme.Find(MonnitSession.CurrentCustomer.AccountID).AccountThemeID == Model.AccountThemeID; %>--%>
                                    <%: Html.CheckBoxFor(m => m.ApplyTheme, new { @class = "form-check-input", @style = "margin-left: -0.3em;" }) %>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <hr />
                    <div class="text-end">
                        <input type="submit" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary" />
                        <span style="color: red; display: inline-block;">
                        </span>
                    </div>
                    <%}%>
                </form>
            </div>

            <% if (ViewData["Result"] != null)
               { %>
            <br />
            <div class="x_panel">
                <% if (ViewData["Result"].ToString().Contains("Success!"))
                   {%>
                <font color="green"><%: ViewData["Result"]  %></font>
                <%}
                   else
                   { %>
                <font color="red"><% Response.Write(ViewData["Result"]);  %></font>
                <%} %>
            </div>
            <%} %>
        </div>
    </div>
    </div>

    <style>
        #importSVG .field-validation-error {
            font-size: 0.8em;
            color: red;
        }

        #importSVG .input-validate-error {
            border-color: red;
        }
    </style>

</asp:Content>
