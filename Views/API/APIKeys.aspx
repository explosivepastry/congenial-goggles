<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<Monnit.APIKey>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    APIKeys
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid" style="margin-top: 25px">
        <div class="col-12">

            <%Html.RenderPartial("/Views/Export/_APILink.ascx"); %>
        </div>

        <div class="col-12 px-0">
            <div class="rule-card_container w-100">
                <div class="card_container__top">
                    <div class="card_container__top__title justify-content-between" style="overflow: unset;">
                        <span><%: Html.TranslateTag("API/APIKeys|APIKeys","API Keys")%></span>

                        <%if (MonnitSession.CurrentCustomer.IsAdmin)
                            {%>
                        <a class="btn btn-primary" onclick="$('#overlay').show()" data-bs-toggle="modal" data-bs-target="#apiModal">
                            <%=Html.GetThemedSVG("add") %>
                            &nbsp; <%: Html.TranslateTag("API/APIKeys|Generate Key","Generate Key")%>
                        </a>
                        <%}%>
                    </div>

                    <div class="clearfix"></div>
                </div>

                <div class="x_content">

                    <%if (Model.Count > 0)
                        { %>
                    <table class="table table-hover align-middle" style="margin-bottom: 0px;">
                        <thead>
                            <tr>
                                <th scope="col"><%: Html.TranslateTag("API/APIiKeys|Name","Name")%></th>
                                <th scope="col"><%: Html.TranslateTag("API/APIiKeys|API Key ID","API Keys ID")%></th>
                                <th scope="col" class="hide"><%: Html.TranslateTag("API/APIiKeys|Last Used","Last Used")%></th>
                                <th scope="col"><%: Html.TranslateTag("API/APIKeys|Created By","Created By")%></th>
                                <th scope="col"></th>
                            </tr>
                        </thead>
                        <tbody>
                            <%foreach (var k in Model)
                                {%>
                            <tr scope="row">
                                <td><%=k.Name%></td>
                                <td><%=k.KeyValue%></td>
                                <td class="hide"><%= (k.LastUsedDate != DateTime.MinValue) ? k.LastUsedDate.ToString() : "Has not been used"%></td>
                                <td><%=Customer.Load(k.CustomerID).UserName%></td>
                                <td>
                                    <%if (MonnitSession.CurrentCustomer.IsAdmin)
                                        {%>
                                    <span class="btnDeleteAPIKey" data-keyvalue="<%=k.KeyValue%>" style="cursor: pointer;">
                                        <%=Html.GetThemedSVG("delete") %>
                                    </span>
                                    <%} %>
                                </td>
                            </tr>
                            <%} %>
                        </tbody>
                    </table>
                    <%}
                        else
                        {%>

                    <div class=" aSettings__title">
                        <p style="font-weight: 900; color: #111;">
                            <%: Html.TranslateTag("API/APIKeys|You do not have any API Keys. Please click \"Generate Key\" ","You do not have any API Keys. Please click \"Generate Key\" ")%>
                        </p>
                        <%}%>
                        <div style="clear: both;"></div>
                    </div>
                </div>
            </div>
        </div>

        <%--<div class="container-fluid " style="margin-top: 15px">
<button onclick="window.location.href='/API' " class="btn btn-dark " style="margin-left: 10px">Back to API Menu</button>
</div>--%>

        <div class="modal fade" id="apiModal" tabindex="-1" aria-labelledby="apiModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel"><%: Html.TranslateTag("API/APIKeys|Generate API Key","Generate API Key")%></h5>
                        <button type="button" id="closeCopyWindow" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>

                    <div class="modal-body pt-0">
                        <form class="form-horizontal">
                            <div id="part-1">
                                <div class="row sensorEditForm">
                                    <div class="col-12 col-md-3">
                                        <%: Html.TranslateTag("Name"," Name") %>
                                    </div>

                                    <div class="col">
                                        <input type="text" id="Name" class="form-control 9e-5" name="Name" placeholder="<%: Html.TranslateTag("API/APIKeys|Descriptive, short name for the key","Descriptive, short name for the key")%>" required />
                                        <div class="editor-error"></div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnCreateAPIKey" onclick="$(this).hide();$('#generating').show();" class="btn btn-primary me-0"><%: Html.TranslateTag("Generate","Generate")%></button>
                                    <button class="btn btn-primary" id="generating" style="display: none;" type="button" disabled>
                                        <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                        <%: Html.TranslateTag("API/APIKeys|Generating...","Generating...")%>
                                    </button>
                                </div>
                            </div>

                            <div id="part-2">
                                <div>
                                    <h5><%: Html.TranslateTag("API/APIKeys|API Key ID","API Key ID") %></h5>
                                </div>

                                <div class="col-12 input-group">
                                    <input class="form-control form-control-sm" type="text" id="keyValueCopy" readonly="readonly">
                                    <input type="button" class="btn btn-primary btn-sm" value="<%: Html.TranslateTag("Copy","Copy")%>" onclick="CopyKeyValue()" id="btnCopyKeyValue">
                                </div>

                                <div>
                                    <h5><%: Html.TranslateTag("API/APIKeys|API Secret Key","API Secret Key *") %></h5>
                                    <p style="font-weight: 700; color: #111;">
                                        *<%: Html.TranslateTag("API/APIKeys|This Key will only be displayed this ONE time, please copy it now.","This Key will only be displayed this ONE time, please copy it now.")%>
                                    </p>
                                </div>

                                <div class="col-12 input-group">
                                    <input class="form-control form-control-sm" type="text" id="apiSecretCopy" readonly="readonly">
                                    <input type="button" class="btn btn-primary btn-sm" value="<%: Html.TranslateTag("Copy","Copy")%>" onclick="CopyAPISecret()" id="btnCopyApiSecretValue">
                                </div>
                                <div class="modal-footer px-0">
                                    <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="$('#closeCopyWindow').click();" style="width: 100%;"><%: Html.TranslateTag("API/APIKeys|I have finished copying!","I have finished copying!")%></button>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {

            HidePart2();

            $("#btnCreateAPIKey").click(function () {
                $.post("/API/CreateAPIKey/", { name: $('#Name').val() }, function (data) {
                    if (data != "Success!")
                        HidePart1();

                    var keyvals = data.split("|");

                    $('#keyValueCopy').val(keyvals[0]);
                    $('#apiSecretCopy').val(keyvals[1]);
                });
            });

            $(".btnDeleteAPIKey").click(function () {
                let values = {};
                values.url = `/API/DeleteAPIKey?keyValue=${$(this).data("keyvalue")}`;
                values.text = "<%: Html.TranslateTag("API/APIKeys|Are you sure you want to remove this API key?","Are you sure you want to remove this API key?")%>";
                openConfirm(values);
                $('.overlay').show();
            });

            $('#apiModal').on('hidden.bs.modal', function (e) {
                $('#overlay').hide();
                location.reload();
            })

        });

        function HidePart1() {
            $('#Name').val('');
            $('#part-1').hide();
            $('#part-2').show();
        }

        function HidePart2() {
            $('#codeCopyHere').val('');
            $('#part-2').hide();
            $('#part-1').show();
        }

        function CopyKeyValue() {
            var copyTextarea = $('#keyValueCopy');

            copyTextarea.select();
            document.execCommand("copy");
        }
        function CopyAPISecret() {
            var copyTextarea = $('#apiSecretCopy');

            copyTextarea.select();
            document.execCommand("copy");
        }
    </script>

    <style type="text/css">
        textarea {
            width: 100%;
        }

        .textwrapper {
            border: 1px solid #999999;
            margin: 5px 0;
            padding: 3px;
        }

        @media only screen and (max-width: 900px) {
            .hide {
                display: none;
            }
        }
    </style>

</asp:Content>
