<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 56 MoistureTension-->
<%   double CompareValue = (String.IsNullOrEmpty(Model.CompareValue)) ? 0.0d : Model.CompareValue.ToDouble();
     CompareValue = Math.Round(CompareValue, 2);

    if (Model != null && Model.CompareType == eCompareType.Less_Than_or_Equal)
        Model.CompareType = eCompareType.Less_Than;
%>


<div class="reading-tag1">

    <div class="hidden-xs ruleDevice__icon">
    </div>
    <div class="tag-title"><span><%= Html.TranslateTag("Condition") %></span> </div>

    <div class="triggerDevice__name">
        <strong style="margin-top: 10px;"><%= Html.TranslateTag("When Moisture Tension is") %>
            <br />
        </strong>
        <span class="reading-tag-condition"><%=MonnitSession.NotificationInProgress.CompareType.ToString().Replace("_"," ") %>  <%=CompareValue %> <%: Html.TranslateTag("centibars","centibars")%>
        </span>
    </div>

</div>

