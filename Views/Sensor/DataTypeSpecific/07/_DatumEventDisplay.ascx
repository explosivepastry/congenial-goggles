<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 7 - Angle-->

<%  double CompareValue = ((String.IsNullOrEmpty(Model.CompareValue)) || Model.CompareValue == double.MinValue.ToString()) ? 0.0d : Model.CompareValue.ToDouble();

    if (Model != null && Model.CompareType == eCompareType.Less_Than_or_Equal)
        Model.CompareType = eCompareType.Less_Than;
%>


<div class="reading-tag1">

    <div class="hidden-xs ruleDevice__icon">
    </div>
    <div class="tag-title"><span><%= Html.TranslateTag("Condition") %></span> </div>

    <div class="triggerDevice__name">
        <strong style="margin-top: 10px;"><%: Html.TranslateTag("When reading is")%>:
            <br />
        </strong>
        <span class="reading-tag-condition"><%=MonnitSession.NotificationInProgress.CompareType.ToString().Replace("_"," ") %>  <%=CompareValue %> <%= Html.TranslateTag("Degrees") %>
        </span>
    </div>

</div>

