<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 08 MilliAmps-->

<%
    string currentLow = Monnit.ZeroToTwentyMilliamp.GetLowValue(Model.SensorID).ToString();
	string currentHigh = Monnit.ZeroToTwentyMilliamp.GetHighValue(Model.SensorID).ToString();
	string currentLabel = Monnit.ZeroToTwentyMilliamp.GetLabel(Model.SensorID).ToString();
	string badgeText = Monnit.ZeroToTwentyMilliamp.ScaleBadgeText(Model.SensorID); 
    %>

<div class="reading-tag1">

    <div class="hidden-xs ruleDevice__icon">
    </div>
    <div class="tag-title"><span><%= Html.TranslateTag("Condition") %></span> </div>

    <div class="triggerDevice__name">
        <strong style="margin-top: 10px;"><%: Html.TranslateTag("When reading is")%>:
            <br />
        </strong>
        <span class="reading-tag-condition"><%=Html.TranslateTag(MonnitSession.NotificationInProgress.CompareType.ToString().Replace("_"," ")) %> <%:Model.CompareValue %> <%=currentLabel%>
        </span>
    </div>

</div>

