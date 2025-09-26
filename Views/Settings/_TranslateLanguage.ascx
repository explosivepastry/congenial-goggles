<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>


<div class="col-12">
    <div class="x_panel shadow-sm rounded">
        <div class="row px-2">
            <div class="x_title">
                <h2><%: Html.TranslateTag("Network/AutoTranslate|Translate Language","Translate Language")%>: <%: Language.Load(ViewBag.LanguageiD) %></h2>
                <div class="nav navbar-right panel_toolbox"></div>
                <div class="clearfix"></div>
            </div>

            <div class="x_content" style="padding: 10px;">

                <!-- Translated Words -->
                <div class="col-12">
                    <div class="row">
                        <div class="bold col-4">
                            <%: Html.TranslateTag("Network/AutoTranslate|Translated Words","Translated Words")%>:
                        </div>
                        <div class="bold col-4">
                            <b><%: ViewBag.TranslatedNum %></b>
                        </div>
                        <%if (ViewBag.TranslatedNum > 0)
                            {%>
                        <div class="bold col-sm-4 col-12" style="align-content: flex-end">
                            <a class="btn btn-default" href="/Settings/ExportTranslatedText/<%: ViewBag.LanguageiD %>"><b><%: Html.TranslateTag("Network/AutoTranslate|Export Translated word list","Export Translated word list")%></b></a>
                        </div>
                        <% } %>
                    </div>
                    <div class="clearfix"></div>
                    <br />
                </div>

                <!-- Words To Translate -->
                <div class="col-12">
                    <div class="row">
                        <div class="bold col-4">
                            <%: Html.TranslateTag("Network/AutoTranslate|Incomplete Words","Incomplete Words")%>:
                        </div>
                        <div class="bold col-4">
                            <b><%: ViewBag.IncompleteNum %></b>
                        </div>
                        <%if (ViewBag.IncompleteNum > 0)
                            {%>
                        <div class="bold col-sm-4" style="align-content: flex-end">
                            <a class="btn btn-default" href="/Settings/ExportIncompleteText/<%: ViewBag.LanguageiD %>"><b><%: Html.TranslateTag("Network/AutoTranslate|Export Incomplete word list","Export Incomplete word list")%></b></a>
                        </div>
                        <% } %>
                    </div>
                    <br />
                </div>

                <!-- Total Words -->
                <div class="col-12">
                    <div class="row">
                        <div class="bold col-4">
                            <%: Html.TranslateTag("Network/AutoTranslate|Total Translation Tags","Total Translation Tags")%>:
                        </div>
                        <div class="bold col-4">
                            <b><%: ViewBag.TotalNum %></b>
                        </div>
                        <div class="bold col-sm-4 col-12">
                        </div>
                    </div>
                    <br />
                </div>
            </div>
            <%if (ViewBag.IncompleteNum > 0)
                {%>
            <div class="gridPanel">
                <div class="bold text-end col-12">
                    <input type="button" id="TranslateBtn" value="<%: Html.TranslateTag("Network/AutoTranslate|Convert All Incomplete Words","Convert All Incomplete Words")%>" class="btn btn-primary me-2" />
                    <div id="loadingGIF" class="text-center" style="display: none;">
                        <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
                            <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
                <br />
            </div>
            <% } %>
        </div>
    </div>
</div>


<script>
    $(document).ready(function () {

        var languageid = "<%:ViewBag.LanguageiD%>";

        $('#TranslateBtn').click(function (e) {
            e.preventDefault();
            $('#TranslateBtn').hide();
            $('#loading').show();

            $.post("/Settings/TranslateAll/", { id: languageid }, function (data) {
                if (data.includes("Success")) {
                    $('#results').html("<h2 class='text-success'>" + data + "</h2>").fadeOut(2500, function () {
                        $(this).html('').show();
                        $.post("/Settings/AutoTranslation", { id: languageid }, function (data) {
                            $('#results').html(data);
                        });
                    });
                } else {
                    $('#resultMsg').html(data);
                }
                $('#loading').hide();
                $('#TranslateBtn').show();
            });
        });
    });

</script>
