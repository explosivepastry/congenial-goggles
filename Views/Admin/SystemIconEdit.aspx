<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.SVGIcon>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SystemIconEdit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div id="fullForm" class="container-fluid mt-4">
        <div class="col-12">

            <div class="x_panel shadow-sm rounded">
                <div class="card_container__top__title">
                    <div class="">
                        <%: Html.TranslateTag("Settings/SystemIconEdit|Edit SVG","Edit SVG")%>
                    </div>
                </div>
                <form id="prefForm" class="form-horizontal form-label-left" action="/Admin/SystemIconEdit" method="post">
                    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                    <%if (MonnitSession.IsCurrentCustomerMonnitAdmin)
                      { %>
                    <div class="formBody">
                        <div class="x_content">
                            <input class="form-control" type="hidden" id="IconID" name="IconID" value="<%: Model.SVGIconID %>" />
                            <input class="form-control" type="hidden" id="ThemeID" name="ThemeID" value="<%: Model.AccountThemeID %>" />

                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3"><%: Html.TranslateTag("Name","Name")%></div>
                                <div class="col sensorEditFormInput">
                                    <input class="form-control" type="text" id="Name" name="Name" value="<%: Model.Name %>" />
                                </div>
                            </div>
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3"><%: Html.TranslateTag("Settings/AdminSettings|Image Key","Image Key")%></div>
                                <div class="col sensorEditFormInput">
                                    <input class="form-control" type="text" id="ImageKey" name="ImageKey" value="<%: Model.ImageKey %>" />
                                </div>
                            </div>
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3"><%: Html.TranslateTag("Settings/AdminSettings|Category","Category")%></div>
                                <div class="col sensorEditFormInput">
                                    <select class="form-select" id="Category" name="Category">
                                        <option><%: Html.TranslateTag("Sensor","Sensor")%></option>
                                        <option><%: Html.TranslateTag("Gateway","Gateway")%></option>
                                    </select>
                                </div>
                            </div>
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3"><%: Html.TranslateTag("Settings/AdminSettings|HTML Code","HTML Code")%></div>
                                <div class="col">
                                    <textarea class="form-control" id="HTMLcode" style="width:100%;" rows="20" name="HTMLcode"><%: Model.HTMLCode %></textarea>
                                </div>
                            </div>
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3"><%: Html.TranslateTag("Settings/AdminSettings|Image Default","Image Default")%></div>
                                <div class="col sensorEditFormInput">
                                    <input type="checkbox" class="form-check-input" value="true" name="IsDefault" id="IsDefault">
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="text-end">
                        <button type="submit" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary">
                            <%: Html.TranslateTag("Save","Save")%>
                        </button>
                        <span style="color: red; padding: 15px; display: inline-block;">

                        </span>
                        <div style="clear: both;"></div>
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
                <font color="green"><%: ViewData["Result"]  %> </font>
                <a href="/Admin/SystemIcons/"><input type="button" class="btn btn-default" value="Back to SystemIcons"></a>
                <%}
                   else
                   { %>
                <font color="red"><%: ViewData["Result"]  %></font>
                <%} %>
            </div>
            <%} %>

        </div>
    </div>



</asp:Content>
