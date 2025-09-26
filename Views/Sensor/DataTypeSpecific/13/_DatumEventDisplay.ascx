<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 13 Geforce-->

<div class="reading-tag1">

    <div class="hidden-xs ruleDevice__icon">
    </div>
    <div class="tag-title"><span><%= Html.TranslateTag("Condition") %></span> </div>

    <div class="triggerDevice__name">
        <strong style="margin-top: 10px;"><%: Html.TranslateTag("When the Geforce is")%>:
            <br />
        </strong>
        <span class="reading-tag-condition"><%=Html.TranslateTag(MonnitSession.NotificationInProgress.CompareType.ToString().Replace("_"," ")) %> <%=Model.CompareValue %> G
        </span>
    </div>

</div>

