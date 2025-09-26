<%--<div id="modal_2" class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="modal_1_label" aria-hidden="true">
<div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-fullscreen"></div>--%>


<%--data-bs-backdrop="static"   :   disables close modal upon clicking outside it (on the "background")--%>
<%--data-bs-keyboard="false"    :   disables use of Esc key to close (other buttons still work, e.g. Tab, Space)--%>

<div id="add_modal_div">
    <button id="add_modal_button" type="button" class="btn btn-primary modal-btn" data-bs-toggle="modal" data-bs-target="#add_modal" style="display: none;">
        Add Modal
    </button>

    <!-- Help Button Modal -->
    <div id="add_modal" class="modal fade" role="dialog" aria-labelledby="add_modal_label" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <%--modal-fullscreen modal-dialog-scrollable--%>
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" aria-label="Add Modal" aria-hidden="true" style="display: none;">Add Modal</h5>
                    <button class="btn-close" type="button" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="modal-column">
                        <div class="modal-row">
                            <div class="modal-row-col modal-row-col-lhs">
                                <span>
                                    <svg id="svg_rules_temp" class="svg_icon" viewBox="-16 0 512 512" xmlns="http://www.w3.org/2000/svg">
                                        <path d="M106.75 215.06L1.2 370.95c-3.08 5 .1 11.5 5.93 12.14l208.26 22.07-108.64-190.1zM7.41 315.43L82.7 193.08 6.06 147.1c-2.67-1.6-6.06.32-6.06 3.43v162.81c0 4.03 5.29 5.53 7.41 2.09zM18.25 423.6l194.4 87.66c5.3 2.45 11.35-1.43 11.35-7.26v-65.67l-203.55-22.3c-4.45-.5-6.23 5.59-2.2 7.57zm81.22-257.78L179.4 22.88c4.34-7.06-3.59-15.25-10.78-11.14L17.81 110.35c-2.47 1.62-2.39 5.26.13 6.78l81.53 48.69zM240 176h109.21L253.63 7.62C250.5 2.54 245.25 0 240 0s-10.5 2.54-13.63 7.62L130.79 176H240zm233.94-28.9l-76.64 45.99 75.29 122.35c2.11 3.44 7.41 1.94 7.41-2.1V150.53c0-3.11-3.39-5.03-6.06-3.43zm-93.41 18.72l81.53-48.7c2.53-1.52 2.6-5.16.13-6.78l-150.81-98.6c-7.19-4.11-15.12 4.08-10.78 11.14l79.93 142.94zm79.02 250.21L256 438.32v65.67c0 5.84 6.05 9.71 11.35 7.26l194.4-87.66c4.03-1.97 2.25-8.06-2.2-7.56zm-86.3-200.97l-108.63 190.1 208.26-22.07c5.83-.65 9.01-7.14 5.93-12.14L373.25 215.06zM240 208H139.57L240 383.75 340.43 208H240z"></path></svg>
                                </span>
                                <%: Html.TranslateTag("AddModal|Rules", "Rules")%> 
                            </div>
                            <div class="modal-row-col modal-row-col-rhs">
                                <a title="Add Rule" href="/Rule/CreateNew" class="btn btn-primary btn-sm">
                                    <span>
                                        <%= Html.GetThemedSVG("add") %>
                                    </span>
                                    <%: Html.TranslateTag("AddModal|Add Rule", "Add Rule")%>
                                </a>
                            </div>
                        </div>
                        <div class="modal-row">
                            <div class="modal-row-col modal-row-col-lhs">
                                <span><%= Html.GetThemedSVG("sensor") %></span>

                                <%: Html.TranslateTag("AddModal|Sensors", "Sensors")%>
                            </div>
                            <div class="modal-row-col modal-row-col-rhs">
                                <a title="Add Sensor" href="/Setup/AssignDevice/<%= MonnitSession.CurrentCustomer.AccountID %>" class="btn btn-primary btn-sm">
                                    <span>
                                        <%= Html.GetThemedSVG("add") %>
                                    </span>
                                    <%: Html.TranslateTag("AddModal|Add Sensor", "Add Sensor")%>
                                </a>
                            </div>
                            <hr />
                        </div>
                        <div class="modal-row">
                            <div class="modal-row-col modal-row-col-lhs">
                                <span><%= Html.GetThemedSVG("network") %></span>

                                <%: Html.TranslateTag("AddModal|Networks", "Networks")%>
                            </div>
                            <div class="modal-row-col modal-row-col-rhs">
                                <a title="Add Network" href="/Network/Create/<%= MonnitSession.CurrentCustomer.AccountID %>" class="btn btn-primary btn-sm">
                                    <span>
                                        <%= Html.GetThemedSVG("add") %>
                                    </span>
                                    <%: Html.TranslateTag("AddModal|Add Network", "Add Network")%>
                                </a>
                            </div>
                        </div>
                        <div class="modal-row">
                            <div class="modal-row-col modal-row-col-lhs">
                                <span><%= Html.GetThemedSVG("profile") %></span>

                                <%: Html.TranslateTag("AddModal|Users", "Users")%>
                            </div>
                            <div class="modal-row-col modal-row-col-rhs">
                                <a title="Add User" href="/Settings/UserCreate/<%= MonnitSession.CurrentCustomer.AccountID %>" class="btn btn-primary btn-sm">
                                    <span>
                                        <%= Html.GetThemedSVG("add") %>
                                    </span>
                                    <%: Html.TranslateTag("AddModal|Add User", "Add User")%>
                                </a>
                            </div>
                        </div>
                        <div class="modal-row">
                            <div class="modal-row-col modal-row-col-lhs">
                                <span><%= Html.GetThemedSVG("map") %></span>

                                <%: Html.TranslateTag("AddModal|Maps", "Maps")%>
                            </div>
                            <div class="modal-row-col modal-row-col-rhs">
                                <a title="Add Map" href="/Map/NewMap" class="btn btn-primary btn-sm">
                                    <span>
                                        <%= Html.GetThemedSVG("add") %>
                                    </span>
                                    <%: Html.TranslateTag("AddModal|Add Map", "Add Map")%>
                                </a>
                            </div>
                        </div>
                        <div class="modal-row">
                            <div class="modal-row-col modal-row-col-lhs">
                                <span><%= Html.GetThemedSVG("details") %></span>

                                <%: Html.TranslateTag("AddModal|Charts", "Charts")%>
                            </div>
                            <div class="modal-row-col modal-row-col-rhs">
                                <a title="Add Chart" href="/Chart/MultiChart" class="btn btn-primary btn-sm">
                                    <span>
                                        <%= Html.GetThemedSVG("add") %>
                                    </span>
                                    <%: Html.TranslateTag("AddModal|Add Chart", "Add Chart")%>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal"><%: Html.TranslateTag("Close", "Close")%></button>
                </div>
            </div>
        </div>
    </div>
    <!-- End help button -->
</div>
<style>
    #add_modal .modal-column {
        padding-left: 0px;
        padding-right: 0px;
        margin-left: auto;
        margin-right: auto;
        margin-bottom: 0;
        width: 100%;
        height: 90%;
        display: flex !important;
        flex-direction: column !important;
        flex: 0 0 auto;
        justify-content: space-between;
    }


#add_modal .modal-content {
    width: 30px;
    min-width: 300px;
    max-width: 300px;

    height: 500px;
    min-height: 500px;
    max-height: 500px;
        

}

#add_modal .modal-body {
    padding-top: 0.25rem;
    padding-bottom: 0.25rem;
}

#add_modal .modal-footer {
    height: 10%;
}

#add_modal .modal-header {
    padding-top: 0;
    padding-bottom: 0;
}

/*#add_modal .modal-header .btn-close {
    	background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='16' height='16' fill='currentColor' class='bi bi-x-circle' viewBox='0 0 16 16'%3E%3Cpath d='M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14zm0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16z'/%3E%3Cpath d='M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708z'/%3E%3C/svg%3E");
}*/

#add_modal .modal-row {
    display: flex!important;
    flex-direction: row!important;
    padding: 0.5rem 0;
    border-bottom: 0.2px solid #e6e6e6;
    height:100%;
    align-items: end;
    padding-bottom: 4px;
}

#add_modal .modal-row-col {
    width: 50%;
    display: flex;
    flex-direction: row;
    align-items: center;
}

#add_modal .modal-row-col a {
    display: flex;
    width: 100%;
}

#add_modal .modal-row-col span {
    margin-right: 10px;
}

#add_modal .modal-row-col.modal-row-col-lhs {
    padding-bottom: 8px;
}

#add_modal .modal-header .btn-close {
    width: 32px;
    height: 32px;

    padding-top: 0;

    margin-top: 0.5rem;
    margin-right: -0.5rem;
    margin-bottom: 0;
    margin-left: auto;

    opacity: 1;

    background-repeat: no-repeat;
    background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='16' height='16' fill='%23000' class='bi bi-x-circle' viewBox='0 0 16 16'%3E%3Cpath d='M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14zm0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16z'/%3E%3Cpath d='M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708z'/%3E%3C/svg%3E");
}

</style>
<%--<script>
$(document).ready(() => {
    /*$('#add_modal').modal('toggle');*/
});

// Fog background when modal being displayed
var observer = new MutationObserver(function(mutations) {
    mutations.forEach(function(mutation) {
        //var attributeValue = $(mutation.target).prop(mutation.attributeName);
        //console.log("Class attribute changed to:", attributeValue);
        if ($('body').hasClass('modal-open')) {
            // #windowOV.container.body
            $('#windowOV').css('filter', 'blur(1px)')
        } else {
            $('#windowOV').css('filter', '')
        }
    });
});

observer.observe(document.body, {
    attributes: true,
    attributeFilter: ['class']
});

</script>--%>