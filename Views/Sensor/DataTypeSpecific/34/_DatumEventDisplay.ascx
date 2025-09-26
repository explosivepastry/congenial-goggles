<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 34 - SeatOccupied-->

<div class="reading-tag1">

    <div class="hidden-xs ruleDevice__icon">
    </div>
    <div class="tag-title"><span><%= Html.TranslateTag("Condition") %></span> </div>

    <div class="triggerDevice__name">
        <strong style="margin-top: 10px;"><%= Html.TranslateTag("Notify when seat is") %> 
            <br />
        </strong>
        <span class="reading-tag-condition"><%=(Model != null && Model.CompareValue.ToLower() == "false") ? Html.TranslateTag("Unoccupied") : Html.TranslateTag("Occupied") %>
        </span>
    </div>

</div>

