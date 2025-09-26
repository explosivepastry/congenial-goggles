<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<UITranslateModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AutoTranslation
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">

        <div class="col-12">
            <div class="x_panel shadow-sm rounded my-4">
                <div class="card_container__top__title">
                    <%: Html.TranslateTag("Network/AutoTranslate|Auto Translation","Auto Translation")%>
                    <div class="nav navbar-right panel_toolbox">
                    </div>
                    <div class="clearfix"></div>
                </div>

                <div class="x_content">

                    <div class="col-12">
                        <div class="col-md-9 col-12">
                            <select class="form-select" style="width:250px;" id="languageID">
                                <option value="-1"><%: Html.TranslateTag("Select a Language","Select a Language")%></option>
                                <%List<Language> languages = Language.LoadAll().Where(m => m.LanguageID != Language.EnglishID).ToList();
                                  foreach (Language item in languages)
                                  { %>
                                <option value="<%=item.LanguageID %>"><%=item.Name %></option>
                                <%} %>
                            </select>
                        </div>
                        <div class="clearfix"></div>
                        <br />
                    </div>
                </div>
            </div>
        </div>

        <!-- Results -->
        <div id="results"></div>

        <div id="resultMsg"></div>

    </div>


    <script>
        $(document).ready(function () {

            $('#languageID').change(function (e) {
                e.preventDefault();

                $.post("/Settings/AutoTranslation", { id: this.value }, function (data) {
                    $('#results').html(data);
                });
            });
        });

    </script>

</asp:Content>
