<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.AccountThemeStyleGroup>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit Theme
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <%AccountTheme theme = ViewBag.Theme;%>
        <%Html.RenderPartial("_WhiteLabelLink", theme);%>
        <div class="x_panel shadow-sm rounded mt-4">
            <div class="x_title">
                <div class="card_container__top__title"><%: Model != null ? Model.Name : "N/A"%></div>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">

                <form id="prefForm" class="form-horizontal form-label-left" method="post">
                    <div>
	                    <input type="hidden" name="AccountThemeStyleGroupID" value="<%: Model.AccountThemeStyleGroupID %>" />
                    </div>
                    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                    <% 
                            Html.RenderPartial("_AdminThemeForm", Model.Styles);
                    %>
                </form>
                <br />
                <div style="color: red; font-weight: bold; font-size: 1.1em;"><%=ViewBag.Result %></div>
            </div>
            <div class="clearfix"></div>
            <div id="UploadDiv" class="modal fade" tabindex="-1">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Upload File</h5>
                        </div>
                        <div class="modal-body">
                            <form method="post" action="" enctype="multipart/form-data" id="UploadForm">
                                <div >
                                    <input type="hidden" id="UploadStyleTypeID" value="" />
                                    <input type="file" id="file" name="file" class="btn" />
                                    <input type="button" class="btn btn-primary" value="Upload" id="uploadFile">
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
            <script type="text/javascript">
                function showUpload(styleTypeID) {
                    $('#UploadStyleTypeID').val(styleTypeID);
                    $('#UploadDiv').modal('show');
                }

                $(document).ready(function() {
                    $("#uploadFile").click(function() {
                        var fd = new FormData();
                        var files = $('#file')[0].files[0];
                        fd.append('file', files);
                        fd.append('styleGroup', <%: Model.AccountThemeStyleGroupID %>);
                        fd.append('styleType', $('#UploadStyleTypeID').val());

                        xhr = new XMLHttpRequest();

                        xhr.open('POST', '/Settings/AdminAccountThemeUpload/<%: Model.AccountThemeID %>', true);
                        xhr.onreadystatechange = function (response) {
                            // In local files, status is 0 upon success in Mozilla Firefox
                            if (xhr.readyState === XMLHttpRequest.DONE) {
                                $('#UploadDiv').modal('hide');
                                var status = xhr.status;
                                if (status === 0 || (status >= 200 && status < 400) && xhr.responseText == "Success") { // The request has been completed successfully
                                    showSimpleMessageModal("<%=Html.TranslateTag("File uploaded")%>");
                                } else {
                                    alert(xhr.responseText);
                                }
                            }
                        };
                        xhr.send(fd);
                    });
                });
            </script>
        </div>
    </div>

</asp:Content>
