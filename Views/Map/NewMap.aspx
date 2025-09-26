<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.VisualMap>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    NewMap
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid col-md-12 col-12 mt-4">
        <div class="rule-card_container w-100">
            <form method="post" enctype="multipart/form-data" action="/Map/NewMap">
                <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                <div class="card_container__top d-flex align-items-center">
                    <%=Html.GetThemedSVG("map") %>
                    <div class="card_container__top__title"  style="margin: 8px 0 0 5px;"><%: Html.TranslateTag("Map/NewMap|Add Map","Add Map")%></div>
                    <div class="nav navbar-right panel_toolbox">
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class=" mapEdit" style="padding: 10px;">
                    <div class="row section">
                        <div class="aSettings__title"><%: Html.TranslateTag("Map/NewMap|Map Name","Map Name")%></div>
                        <div class="col-sm-9 col-xs-8">
                            <input type="text" id="mapName" value="<%=Model.Name %>" name="Name" class="form-control user-dets" style="width: 250px;" required >
                            <%= Html.ValidationMessageFor(model => model.Name) %>
                        </div>
                    </div>
                    <%if (!string.IsNullOrEmpty(MonnitSession.CurrentTheme.PropertyValue("Maps_API_Key"))) { %>
                    <div class="row section">
                        <br />
                        <div class="col mapEditFormInput">
                            <div class="form-check form-switch d-flex align-items-end ps-0">
                                <label class="form-check-label aSettings__title"><%: Html.TranslateTag("Map/NewMap|Static Map")%></label>
                                <input class="form-check-input mx-2" type="checkbox" id="mapTypeToggle" >
                                <label class="form-check-label aSettings__title"><%: Html.TranslateTag("Map/NewMap|GPS Map")%></label>
                                <input id="mapType" name="mapType" value="StaticMap" hidden />
                            </div>
                        </div>
                    </div>
                    <%} else {//Force to Static Map %>
                        <input type="hidden" name="mapType" value="1">
                    <%} %>
                    <div class="row section" id="imageMapDiv">
                        <div class="aSettings__title"><%: Html.TranslateTag("Map/NewMap|Map Image","Map Image")%></div>
                        <input type="file" id="ImageFile" name="ImageFile" class="btn btn-grey" style="display: none;" accept="image/*" />
                        <div class="col-sm-9 col-8 dfac" id="fileBtn">
                            <a class="btn btn-secondary btn-sm" style="cursor: pointer; margin-right: 5px;"><%: Html.TranslateTag("Map/NewMap|Select File","Select File")%></a>
                            <label id="fileLabel"><%: Html.TranslateTag("Map/NewMap|No file selected","No file selected")%></label>
                        </div>
                        <%= Html.ValidationMessageFor(model => model.Image) %>
                    </div>
                    <div class="alignright dfac mb-1">
                        <a href="/Map"style="margin-right:15px;" class="btn btn-secondary"><%: Html.TranslateTag("Cancel","Cancel")%></a>
                        <button class="btn btn-primary user-dets" style="max-width: 120px;" type="submit">
                            <%: Html.TranslateTag("Save","Save")%>
                        </button>
                    </div>
                </div>
            </form>
        </div>
    </div>
    <script>
        $(function () {
            $('#mapTypeToggle').change(function () {
                console.log('checked? ' + $(this).is(':checked'));
                if ($(this).is(':checked')) {
                    $('#mapType').val('GpsMap');
                    $('#imageMapDiv').hide();
                } else {
                    $('#mapType').val('StaticMap');
                    $('#imageMapDiv').show();
                }
            });

            $("#fileBtn").click(function () {
                $("#ImageFile").click();
            });

            $('input[type=file]').change(function (e) {
                var removePath = $('#ImageFile').val().replace(/^C:\\fakepath\\/i, '');
                $("#fileLabel").html(removePath);
            });


        });

    </script>

</asp:Content>
