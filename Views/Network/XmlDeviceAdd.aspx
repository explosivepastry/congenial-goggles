<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<xmlDeviceAddModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    XmlDeviceAdd
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <h2><%: Html.TranslateTag("Network/AssignDevice|Assign Devices","Assign Devices")%></h2>
        <div class="col-12">

            <div class="x_panel shadow-sm rounded">
                <div class="x_title">
                    <h2 style="background-color: white !important; overflow: unset;">
                        <img src="../../Content/images/iconmonstr-plus-1-240.png" width="20px" height="auto" style="padding-bottom: 7px;">&nbsp;<b><%: Html.TranslateTag("Network/AssignDevice|Add Devices","Adding Devices to:")%></b>
                        <br />
                        <img src="../../Content/images/iconmonstr-sitemap-21-240-darkgrey.png" width="20px" height="auto" style="padding-bottom: 7px;">&nbsp;<b><%: Html.TranslateTag("Network/AssignDevice|Network","Network")%>: </b><span style="font-size: smaller;" for="networkSelect"><%=CSNet.Load(Model.NetworkID).Name %> </span>
                        <%if (Model.Networks.Count > 1)
                            { %>
                        <a style="cursor: pointer;" href="/network/NetworkSelect?accountID=<%=Model.AccountID %>">
                            <br />
                            <img src="../../Content/images/iconmonstr-connection-8-240.png" width="20px" height="auto">&nbsp;&nbsp;<b><span style="font-size: 12px; margin-left: -5px;"><%: Html.TranslateTag("Network/AssignDevice|Switch Network","Switch Network")%></span></b></a>
                        <%} %>
                        <%: Html.ValidationMessageFor(model => model.NetworkID)%>
                    </h2>

                    <div class="nav navbar-right panel_toolbox">
                    </div>
                    <div class="clearfix"></div>
                </div>

                <div class="aSettings__title"><%: Html.TranslateTag("Network/XmlDeviceAdd|Download Xml Template","Download Xml Template")%></div>
                <div class="form-group">
                    <div style="display: flex;">
                        <a href="/Network/ExportXMLDeviceTemplate/">
                            <input class="btn btn-secondary btn-sm" type="button" value="<%: Html.TranslateTag("Network/AssignDevice|Download Template","Download Template")%>">
                        </a>
                    </div>
                </div>

                <form action="/Network/XmlDeviceAdd" method="post" enctype="multipart/form-data" id="fileUpload">
                    <div class="aSettings__title"><%: Html.TranslateTag("Network/XmlDeviceAdd|Upload Xml File","Upload Xml File")%></div>
                    <input required class="form-control form-control-sm" name="xmlMetaData" id="Upload" type="file" style="width: 300px;">
                    <br />
                    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                    <input type="text" name="AccountID" value="<%: Model.AccountID %>" hidden>
                    <input type="hidden" name="NetworkID" value="<%: Model.NetworkID %>">

                    <div class="text-end">
                        <input id="submitBtn" class="btn btn-primary" type="submit" value="<%: Html.TranslateTag("Network/XmlDeviceAdd|Upload","Upload")%>" />
                    </div>


                    <div class="clearfix"></div>
                    <div class="bold" style="padding-left: 10px; font-weight: bold; color: red; font-size: 1.2em;" id="messageDiv">
                    </div>
                </form>
            </div>

        </div>
    </div>

    <div id="results">
    </div>

    <script type="text/javascript">

        $(document).ready(function () {
            $('#networkSelect').change(function (e) {
                e.preventDefault();
                csnetID = $("#networkSelect").val();
            });

            $('#submitBtn').click(function (e) {
                if ($('#Upload').get(0).files.length === 0) {
                }
                else {
                    $('#submitBtn').hide();
                    $('#messageDiv').html(`Adding Devices <div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div> `)
                }
            });
        });
    </script>

</asp:Content>
