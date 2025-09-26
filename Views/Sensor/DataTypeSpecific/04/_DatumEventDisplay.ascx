<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 4 - Voltage-->

<div class="reading-tag1">

    <div class="hidden-xs ruleDevice__icon">
    </div>
    <div class="tag-title"><span><%= Html.TranslateTag("Condition") %></span> </div>

    <div class="triggerDevice__name">
        <strong style="margin-top: 10px;"><%= Html.TranslateTag("Notify when reading is") %>
            <br />
        </strong>
        <span class="reading-tag-condition"><%=Html.TranslateTag(MonnitSession.NotificationInProgress.CompareType.ToString().Replace("_"," ")) %> <%:Model.CompareValue %> <%= Html.TranslateTag("Volts") %>
        </span>
    </div>
  
</div>

