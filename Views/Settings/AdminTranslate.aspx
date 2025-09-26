<%@ Page Title=string.Empty Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<List<UITranslateModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminTranslate
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .filters label {
            margin-right:5px;
            font-size: 1rem;
            /*clear: both;
            float: left;
            display: block;*/
            white-space: nowrap;
        }

        .filters input, .filters select {
            padding: 0.0rem 0.75rem;
        }
    </style>

    <%Language lang = Language.Load((long)ViewBag.LanguageID);
        if (lang == null)
            lang = Language.Load(Language.EnglishID);%>

    <div class="container-fluid">
        <div class="col-12">
            <div class="x_panel shadow-sm rounded mt-4">
                <div class="x_title">
                    <div style="float:left;">
                        <%=Html.GetThemedSVG("list") %>
                    </div>
                    <div class="dfjcac filters">
                        <div class="dfjcac mx-3">
                            <label for="languageSelect">&nbsp;<%: Html.TranslateTag("Translate","Translate")%>&nbsp;</label>
                            <select id="languageSelect" class="form-select" style="width: 200px;">
<%
    List<Language> languages = Language.LoadAll().Where(m => m.LanguageID != Language.EnglishID).ToList();
    foreach (Language item in languages)
    { 
%>
                                <option <%= item.LanguageID == lang.LanguageID ? "selected='selected'" : string.Empty %> 
                                    value="<%=item.LanguageID %>"><%=item.Name %></option>
<%
    } 
%>
                            </select>
                        </div>
                        <div class="dfjcac mx-3">
                            <label for="resultsPerPage""><%: Html.TranslateTag("Results per Page") %></label>
                            <select id="resultsPerPage" name="resultsPerPage" class="form-select" style="width: 70px;">
                                <option value="10">10</option>
                                <option value="15">15</option>
                                <option value="20">20</option>
                                <option value="25">25</option>
                                <option value="30">30</option>
                            </select>
                        </div>
                        
                        <div class="dfjcac mx-3">
                            <label for="page"><%: Html.TranslateTag("Page") %></label>
                            <input id="page" name="page" type="number" value="<%= ViewBag.Page %>" 
                                class="form-control" 
                                style="width:70px;" />
                            <button id="prevBtn">&larr;</button>
                            <button id="nextBtn">&rarr;</button>
                        </div>
                        

                        <div class="dfjcac mx-3">
                            <label for="tagFilter" class="">Tag Filter</label>
                            <input id="tagFilter" class="form-control"
                                name="tagFilter" type="text" 
                                value="<%= ViewBag.TagFilter %>" placeholder="Tag Filter" />
                        </div>
                        
                        <div class="dfjcac mx-3">
                            <label for="search">Load</label>
                            <button id="search">&#8635;</button>
                        </div>
                        
                        
                    </div>
                    <div class="nav navbar-right panel_toolbox">
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content ps-2" id="translate">
                    <div>
                        <div class="x_content">
                            <div class="gridPanel col-12">
                                <div class="row">
                                    <div class="bold col-sm-2 col-12">
                                    </div>
                                    <div class="bold col-sm-10 col-12">
                                        Tanslate each word from the left column using  the text box opposite it. After all words are translated click "Done". A new group of words will appear.
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                            </div>
                            <div>
                                <div class="bold col-sm-4 col-12">
                                    <span style="font-weight: bold; font-size: 1.2em;"><%: Html.TranslateTag("Settings/AdminTranslate|To Be Translated","To Be Translated")%></span>
                                </div>
                                <div class="bold col-sm-8 col-12 lgbox">
                                    <span style="font-weight: bold; font-size: 1.2em;"><%: Html.TranslateTag("Settings/AdminTranslate|Translation Input","Translation Input")%></span>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                            </div>
                            <%if (Model.Count < 1)
                                {
                            %>

                            <div>
                                <div class="row">
                                    <div class="bold col-sm-4 col-12">
                                    </div>
                                    <div class="bold col-sm-8 col-12">
                                        <%: Html.TranslateTag("Settings/AdminTranslate|No translations found","No translations found")%>.
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                            </div>
                            <%}
                                else
                                { %>

                            <form class="col-12" action="/Settings/CreateTTLEntry" method="post">
                                <input type="hidden" name="LanguageID" value="<%= lang.LanguageID%>" />
                                <%
                                    string location = string.Empty;
                                    string toBeTranslated = string.Empty; 
                                %>
                                <%foreach (UITranslateModel link in Model)
                                    {
                                        if (link.TagIDName.Split('|').Length < 2)
                                        {
                                            toBeTranslated = link.TagIDName;
                                            location = "Single Word";

                                        }
                                        else
                                        {

                                            location = link.TagIDName.Split('|')[0];
                                            toBeTranslated = link.TagIDName.Split('|')[1];
                                        }
                                %>
                                <div class="d-sm-flex align-items-baseline">
                                    <div class="col-12 col-sm-4">
                                        <span title="<%=location %>"><%=toBeTranslated %></span>
                                    </div>
                                    <div class="col-12 col-sm-8">
                                        <input class="form-control mb-3" placeholder="<%= "&quot;" + toBeTranslated + "&quot;" + " in " + lang.Name + "..." %>" type="text" name="textValue_<%=link.TranslationTagID %>" id="textValue_<%=link.TranslationTagID %>" value="<%=link.Text %>" style="width: 100%;" />
                                    </div>
                                </div>
                                <hr class="col-12 mt-2" />
                                <%
                                    } %>

                                <input type="submit" value="Next" class="btn btn-primary" />
                            </form>

                            <%} %>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        <%= ExtensionMethods.LabelPartialIfDebug("AdminTranslate.js") %>

        $(document).ready(function () {

            $('#resultsPerPage').val(<%= ViewBag.ResultsPerPage %>);

            $('#languageSelect').change(function (e) {
                e.preventDefault();

                window.location.href = "/Settings/AdminTranslate/" + this.value;
            });
        });

        function submitText(tagID) {
            $.post("/Settings/CreateTTLEntry", { name: "value" }, function (data) {
            });
        }

        const tagFilterInit = $('#tagFilter').val();
        
        function getPrams() {
            return {
                resultsPerPage: $('#resultsPerPage').val(),
                page: tagFilterInit != $('#tagFilter').val() ? 0 : $('#page').val(),
                tagFilter: $('#tagFilter').val(),
            };
        }
        $('#page, #tagFilter').keypress(function (e) {
            if (e.key === 'Enter') {
                $('#search').click();
            }
        });
        
        $('#search').click(function (e) {
            var prams = getPrams();
            location = location.origin + location.pathname + "?" + $.param(prams);
        });


        $('#prevBtn').click(function (e) {
            var prams = getPrams();
            prams.page <= 0 ? prams.page = 0 : prams.page -= 1;
            var newUrl = location.origin + location.pathname + "?" + $.param(prams);
            location = newUrl;
            
        });

        $('#nextBtn').click(function (e) {
            var prams = getPrams();
            prams.page += 1;
            location = location.origin + location.pathname + "?" + $.param(prams);
        });
    </script>


</asp:Content>
