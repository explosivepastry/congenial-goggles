<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>


<div class="contain-welcome">
    <div class="welcome-locations">
        <h1><%:Html.TranslateTag("Welcome to Locations") %></h1>
        <h5><%:Html.TranslateTag("This is your central hub to manage sensor data from all locations you supervise or assign.") %> </h5>
        <h5><%:Html.TranslateTag("With this feature, you can:") %> </h5>
    </div>
    <div class="boxes-locate">

        <div class="locations-instruct">
            <div class="Build-locate">

                <p><%:Html.TranslateTag("Build a secure hierarchy from headquarters to sub-locations or multiple branches.") %></p>
            </div>
            <div class="location-icon-instruct people-icon">
    <%=Html.GetThemedSVG("branch") %>
            </div>
        </div>


        <div class="locations-instruct">
            <div class="Build-locate">

                <p><%:Html.TranslateTag("Assign people to data management roles for specific locations and sub-locations.") %></p>
            </div>
            <div class="location-icon-instruct people-icon" style="width: 100px;">

                <%=Html.GetThemedSVG("people-group") %>
            </div>

        </div>
        <div class="locations-instruct">
            <div class="Build-locate">

                <p><%:Html.TranslateTag("Get started by creating a location and linking your gateways and sensors to it.") %></p>
            </div>
            <div class="location-icon-instruct monnitor-icon" style="width: 100px">

               <%=Html.GetThemedSVG("view-data") %>
            </div>

        </div>
    </div>

</div>

