<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 25 - Water Detect-->

<div class="reading-tag1">

    <div class="hidden-xs ruleDevice__icon">
    </div>
    <div class="tag-title"><span><%= Html.TranslateTag("Condition") %></span> </div>

    <div class="triggerDevice__name">
        <strong style="margin-top: 10px;"><%= Html.TranslateTag("Notify when water is") %> 
            <br />
        </strong>
        <span class="reading-tag-condition"><%=(Model != null && Model.CompareValue.ToLower() == "false") ? Html.TranslateTag("Not Detected") : Html.TranslateTag("Detected") %>
        </span>
    </div>

</div>

