<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.VisualMap>" %>

<!-- Detail Buttons -->
<div class="mb-4">
    <div class="col-12 view-btns_container top-nav-gap-row shadow-sm rounded">
        <div class="view-btns-half view-btns deviceView_btn_row rounded">
            <a href="/Map" class="btn btn-lg">
                <div class=" btn-secondaryToggle btn-lg btn-fill "><%=Html.GetThemedSVG("map") %><span class="extra"><%: Html.TranslateTag("Maps","Maps") %></span></div>
            </a>

            <a href="/Map/ViewMap/<%:Model.VisualMapID %>" class="deviceView_btn_row__device">
                <div class="<%:Request.Path.StartsWith("/Map/ViewMap")?"btn-active-fill shadow mb-lg-2":"btn-secondaryToggle" %> btn-lg btn-fill">
                    <%=Html.GetThemedSVG("viewMap") %>
                    <span class="extra"><%: Html.TranslateTag("Map/_MapLink|View Map","View Map")%>
                    </span>
                </div>
            </a>

            <a href="/Map/EditMap/<%:Model.VisualMapID %>" class="deviceView_btn_row__device">
                <div class="<%:Request.Path.StartsWith("/Map/EditMap")?"btn-active-fill shadow mb-lg-2":"btn-secondaryToggle" %> btn-lg btn-fill">
                    <%=Html.GetThemedSVG("edit") %>
                    <span class="extra"><%: Html.TranslateTag("Map/_MapLink|Edit Map","Edit Map")%>
                    </span>
                </div>
            </a>

            <a href="/Map/DevicesToShow/<%:Model.VisualMapID %>" class="deviceView_btn_row__device">
                <div style="width:100px;" class="<%:Request.Path.StartsWith("/Map/DevicesToShow")?"btn-active-fill shadow mb-lg-2":"btn-secondaryToggle" %> btn-lg btn-fill">
                    <%=Html.GetThemedSVG("gps-pin") %>
                    <span class="extra" ><%: Html.TranslateTag("Map/_MapLink|Add Devices","Add Devices")%>
                    </span>
                </div>
            </a>
        </div>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
    });

    $('.btn-secondaryToggle').hover(
        function () { $(this).addClass('active-hover-fill') },
        function () { $(this).removeClass('active-hover-fill') }
    )
</script>

<!-- End Detail Buttons -->
