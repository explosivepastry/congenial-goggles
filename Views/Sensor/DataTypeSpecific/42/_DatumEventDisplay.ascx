<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 42 - 0-10 Volt-->

<%  
    string label = string.Empty;
    double lowVal = double.MinValue;
    double highVal = double.MinValue;
    if (Model.ScaleID > 0)
    {
        Sensor sens = Sensor.Load(Model.ScaleID);
        label = ZeroToTenVolts.GetLabel(Model.ScaleID);
        lowVal = ZeroToTenVolts.GetLowValue(Model.ScaleID);
        highVal = ZeroToTenVolts.GetHighValue(Model.ScaleID);

    }
    else
    {
        lowVal = 0.0;
        highVal = 10.0;

    }

    double revertedVal = Model.CompareValue.ToDouble().LinearInterpolation(0, lowVal, 10, highVal, 2);
    revertedVal = revertedVal < 0 ? 0 : revertedVal;

%>

<div class="reading-tag1">

    <div class="hidden-xs ruleDevice__icon">
    </div>
    <div class="tag-title"><span><%= Html.TranslateTag("Condition") %></span> </div>

    <div class="triggerDevice__name">
        <strong style="margin-top: 10px;"><%: Html.TranslateTag("When the reading is")%>:
            <br />
        </strong>
        <span class="reading-tag-condition"><%=Html.TranslateTag(MonnitSession.NotificationInProgress.CompareType.ToString().Replace("_"," ")) %> <%=revertedVal %> <%= label %>
        </span>
    </div>

</div>

