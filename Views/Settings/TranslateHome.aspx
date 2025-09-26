<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    TranslateHome
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">

        <div class="col-12 mt-4">
            <div class="x_panel shadow-sm rounded">
                <div class="card_container__top__title">
                    <h2><%=Html.GetThemedSVG("list") %>&nbsp;<%: Html.TranslateTag("Translation Search","Translation Search")%></h2>
                    <div class="nav navbar-right panel_toolbox">
                    </div>
                    <div class="clearfix"></div>
                </div>

                <div class="x_content">
                    <div class="col-12">
                        <div class="col-md-9 col-12 d-flex">
                            <span class="input-group">
                                <input class="form-control" id="searchText" type="text" placeholder="Search Translations..." />
                                <select class="form-select" id="languageID">
                                    <option value="-1"><%: Html.TranslateTag("Select a Language","Select a Language")%></option>
                                    <%List<Language> languages = Language.LoadAll().Where(m => m.LanguageID != Language.EnglishID).ToList();
                                        foreach (Language item in languages)
                                        { %>
                                    <option value="<%=item.LanguageID %>"><%=item.Name %></option>
                                    <%} %>
                                </select>
                                <input class="btn btn-primary" type="button" id="searchBtn" value="Search" />
                            </span>
                        </div>

                        <div class="clearfix"></div>
                        <br />
                    </div>
                    <div class="col-12 p-2" id="translationResult">
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {

            $('#searchBtn').click(function (e) {
                e.preventDefault();
                var searchText = $('#searchText').val();
                var langID = $('#languageID').val();

                if (langID == -1) {
                    showSimpleMessageModal("<%=Html.TranslateTag("Language Required")%>");
                    return;
                }

                if (searchText.length > 1) {

                    $.post("/Settings/TranslateSearch", { id: langID, textToFind: searchText }, function (data) {
                        $('#translationResult').html(data);
                    });

                } else {
                    showSimpleMessageModal("<%=Html.TranslateTag("Search field required")%>");
                }
            });
        });
    </script>

</asp:Content>
