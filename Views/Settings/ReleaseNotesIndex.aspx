<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<Monnit.ChangeLog>>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mt-4">
        <% if (MonnitSession.IsCurrentCustomerMonnitAdmin)
            { %>
            <div class="text-end">
                <a class="btn btn-primary mb-2" href="/Settings/ReleaseNotesEdit"><%: Html.TranslateTag("Settings/ReleaseNotesIndex|Add Release","Add Release")%></a>
            </div>
        <% } %>
        <div class="accordion">
            <% 
                int index = 0;
                foreach (ChangeLog item in Model)
                {
                    index++;
            %>
            <div class="accordion-item rounded shadow-sm">
                <h2 class="accordion-header" id="panelsStayOpen-heading<%:item.ChangeLogID %>">
                    <button
                        class="accordion-button <%: index <= 3 ? "" : "collapsed" %>"
                        type="button"
                        data-bs-toggle="collapse"
                        data-bs-target="#panelsStayOpen-collapse<%:item.ChangeLogID %>"
                        aria-expanded="<%: index <= 3 ? true : false %>"
                        aria-controls="panelsStayOpen-collapse<%:item.ChangeLogID %>">
                        <strong class="me-1"><%: item.ReleaseDate.Date.ToString("d") %></strong> | <span class="ms-1" style="font-weight: 200;">version <%: item.Version %></span>

                        <% if (MonnitSession.IsCurrentCustomerMonnitAdmin) { 
                                if (item.isPublished)
                            { %>
                        <span class="badge ms-1" style="background: #81C784"><%: Html.TranslateTag("Settings/ReleaseNotesIndex|Published","Published")%></span>
                        <% }
                            else
                            { %>
                        <span class="badge ms-1" style="background: #FFB74D"><%: Html.TranslateTag("Settings/ReleaseNotesIndex|Not Published","Not Published")%></span>
                        <% }
 %>
                        <div class="d-flex buttons">
                            <div onclick="event.preventDefault(); openNote(<%:item.ChangeLogID %>)" title="<%: Html.TranslateTag("Settings/ReleaseNotesIndex|Edit Release Notes","Edit Release Notes")%>" style="cursor: pointer;" class="mx-2"><%=Html.GetThemedSVG("edit") %></div>
                            <div onclick="event.preventDefault(); removeChangeLog(<%:item.ChangeLogID %>)" title="<%: Html.TranslateTag("Settings/ReleaseNotesIndex|Delete Release Note","Delete Release Note")%>" style="cursor: pointer;"><%=Html.GetThemedSVG("delete") %></div>
                        </div>
                        <% } %>

                    </button>
                </h2>
                <div
                    id="panelsStayOpen-collapse<%:item.ChangeLogID %>"
                    class="accordion-collapse collapse <%: index <= 3 ? "show" : "" %>"
                    aria-labelledby="panelsStayOpen-heading<%:item.ChangeLogID %>">
                    <div class="accordion-body" style="overflow-y: auto!important; max-height: none!important;">
                        <%Html.RenderPartial("_ChangeLogsNote", item); %>
                    </div>
                </div>
            </div>

            <% } %>
        </div>
    </div>

    <%
        if (MonnitSession.IsCurrentCustomerMonnitAdmin)
        {
    %>
    <script type="text/javascript">
        function removeChangeLog(id) {
            let values = {};
            values.url = `/Settings/RemoveReleaseNote/${id}`;
            values.text = "Are you sure you want to remove this Release Note?";
            openConfirm(values);
        }

        const openNote = (id) => {
            disableUnsavedChangesAlert();
            window.location = `/Settings/ReleaseNotesEdit/${id}`
        }
    </script>
    <% } %>

    <style  type="text/css">
        .accordion-body .svg_icon {
            display: none;
        }

        .buttons {
            position:absolute;
            right: 50px;

        }
    </style>

</asp:Content>
