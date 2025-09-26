<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ReleaseNote>" %>

<div class="x_panel col-md-12 shadow-sm rounded">
    <div class="card_container__top">
        <div class="card_container__top__title">
            <%: Html.TranslateTag("Release Note Preview","Release Note Preview")%>
        </div>
    </div>
    <div class="accordion">
        <div class="accordion-item">
            <h2 class="accordion-header" id="panelsStayOpen-heading">
                <button
                    class="accordion-button"
                    type="button"
                    data-bs-toggle="collapse"
                    data-bs-target="#panelsStayOpen-collapse"
                    aria-expanded="false"
                    aria-controls="panelsStayOpen-collapse">
                    <strong id="date" class="me-1"><%:Model != null ? Model.ReleaseDate.Date.ToString() : "Date" %></strong> 
                    | version <span id="version" class="ms-1" style="font-weight: 200;"><%:Model != null ? Model.Version : "--" %> </span>
                </button>
            </h2>
            <div
                id="panelsStayOpen-collapse"
                class="accordion-collapse collapse show"
                aria-labelledby="panelsStayOpen-heading">
                <div class="accordion-body">
                    <div class="mt-2">
                        <%Html.RenderPartial("_ReleaseNotesNote",Model); %>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<script type="text/javascript">
</script>

<style>
        
    </style>
