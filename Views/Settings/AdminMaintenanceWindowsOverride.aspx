<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.MaintenanceWindow>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminCreateMaintenanceWindows
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="x_panel shadow-sm">
        <div class="x_title">
            <h2><%: Html.TranslateTag("Settings/AdminMaintenanceWindowsOverride|Override Maintenance Windows","Override Maintenance Windows")%></h2>
            <div class="clearfix"></div>
        </div>
        <div class="x_content">
            <div class="x_content col-md-12 col-sm-12 col-xs-12">
                <div class="bold col-lg-2 col-md-3 col-sm-4 col-xs-12">
                    <%: Html.TranslateTag("Settings/AdminMaintenanceWindowsOverride|Original Description","Original Description")%>
                </div>
                <div class="col-lg-10 col-md-9 col-sm-8 col-xs-12">
                    <%:Html.Raw(Model.Description) %>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="x_content col-md-12 col-sm-12 col-xs-12">
                <div class="bold col-lg-2 col-md-3 col-sm-4 col-xs-12">
                    <%: Html.TranslateTag("Settings/AdminMaintenanceWindowsOverride|Original Email Body","Original Email Body")%>
                </div>
                <div class="col-lg-10 col-md-9 col-sm-8 col-xs-12">
                    <%:Html.Raw(Model.EmailBody) %>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="clearfix"></div>
            <hr />

            <form id="prefForm" class="form-horizontal form-label-left" method="post">
                <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                <%AccountThemeMaintenanceLink atml = AccountThemeMaintenanceLink.LoadByAccountThemeIDAndMaintenanceID(MonnitSession.CurrentTheme.AccountThemeID, Model.MaintenanceWindowID); %>
                <% 

                    if (atml == null)
                    {
                        atml = new AccountThemeMaintenanceLink();
                        atml.OverriddenNote = Model.Description;
                        atml.OverriddenEmail = Model.EmailBody;
                        atml.MaintenanceWindowID = Model.MaintenanceWindowID;
                        atml.AccountThemeID = MonnitSession.CurrentTheme.AccountThemeID;
                    }
                    //else
                    //{
                    //    mainWin.Description = atml.OverriddenNote;
                    //    mainWin.EmailBody = atml.OverriddenEmail;
                    //    mainWin.MaintenanceWindowID = atml.MaintenanceWindowID;
                    //     mainWin.MaintenanceWindowID = Model.MaintenanceWindowID;
                    //}
                %>

                <input name="AccountThemeMaintenanceLinkID" value="<%:atml.AccountThemeMaintenanceLinkID%>" type="hidden" />

                <input name="AccountThemeID" value="<%:MonnitSession.CurrentTheme.AccountThemeID%>" type="hidden" />

                <input name="MaintenanceWindowID" value="<%:atml.MaintenanceWindowID %>" type="hidden" />


                <br />
                <div class="x_content col-12">
                    <div class="bold col-12 aSettings__title">
                        <%: Html.TranslateTag("Settings/_AdminMaintenanceWindowsForm|Description (Default 8000 Characters)","Description (Default 8000 Characters)")%>:
                    </div>
                    <div class="editor-label col-12"></div>
                    <textarea id="editor"><%:atml.OverriddenNote %></textarea>
                    <%: Html.HiddenFor(model => model.Description)%>
                    <div class="clearfix"></div>
                </div>
                <div class="bold col-12 aSettings__title">
                    <%: Html.TranslateTag("Settings/_AdminMaintenanceWindowsForm|Email Body (Default 8000 Characters)","Email Body (Default 8000 Characters)")%>:
                </div>
                <div class="x_content col-12">
                    <div class="editor-label col-12"></div>
                    <textarea id="emailBody"><%:atml.OverriddenEmail %></textarea>
                    <%: Html.HiddenFor(model => model.EmailBody)%>
                    <div class="clearfix"></div>
                </div>


                <div class="clearfix"></div>
                <div class="text-end mt-2">
                    <button id="prefSave" type="submit" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary me-2">
                        <%: Html.TranslateTag("Save","Save")%>
                    </button>
                    <a href="/Settings/AdminMaintenanceWindows/" class="btn btn-light Cancel"><%: Html.TranslateTag("Cancel","Cancel")%></a>
                    <a id="delBtn" style="display: none" onclick="deleteMainOverride(<%=atml.AccountThemeMaintenanceLinkID %>);" class="btn btn-danger"><%: Html.TranslateTag("Delete","Delete")%></a>
                </div>


                <link href="/suneditor/suneditor.min.css" rel="stylesheet" />
                <script type="text/javascript" src="/suneditor/suneditor.min.js"></script>


                <script type="text/javascript">

                    <% if (atml.AccountThemeMaintenanceLinkID > 0)
                        {%> 

                         $('#delBtn').show();
                      <%}%>

                    var AreYouSure = "<%: Html.TranslateTag("Settings/AdminMaintenanceWindowsOverride|Are you sure you want to delete this Maintenance Override?")%>";

                    function deleteMainOverride(mainLinkID) {

                        if (confirm(AreYouSure)) {
                            $.post("/Settings/MaintenanceOverrideRemove", { id: mainLinkID }, function (data) {
                                if (data != "Success") {
                                    console.log(data);
                                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                                } else {
                                    window.location.href = "/Settings/AdminMaintenanceWindows";
                                }
                            });
                        }
                    }
                    
                    $('form').submit(function (e) {
                        //$('#Description').val(sunObjEditor.getText());
                        //$('#EmailBody').val(sunEmailObjEditor.getText());
                        $('#Description').val(sunObjEditor.getContents());
                        $('#EmailBody').val(sunEmailObjEditor.getContents());
                    });

                    var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
                    $(document).ready(function () {

                        $('#datepicker').mobiscroll().datepicker({
                            theme: 'ios',
                            display: popLocation,
                            controls: ['calendar', 'time'],
                            select: 'range',
                            defaultSelection: [new Date(Number(<%=MonnitSession.HistoryFromDate.Year%>), Number(<%=(MonnitSession.HistoryFromDate.Month - 1)%>), Number(<%=MonnitSession.HistoryFromDate.Day%>)), new Date(Number(<%=MonnitSession.HistoryToDate.Year%>), Number(<%=(MonnitSession.HistoryToDate.Month - 1)%>), Number(<%=MonnitSession.HistoryToDate.Day%>), 23, 59, 59, 0)],
                            onChange: function (event, inst) {
                                $('#startDate').val(inst._tempStartText);
                                $('#endDate').val(inst._tempEndText);
                            }
                        });

                        $('.sf-with-ul').removeClass('currentPage');
                        $('#MenuMaint').addClass('currentPage');

                        //emailObjEditor = null;
                        //objEditor = null;
                        //createDiscriptionEditor();
                        //createEmailEditor();
                        sunObjEditor = createSunEditor('editor', true);
                        sunEmailObjEditor = createSunEditor('emailBody', true);

                    });

                    var sunObjEditor;
                    var sunEmailObjEditor;

                </script>
            </form>
        </div>
    </div>
</asp:Content>
