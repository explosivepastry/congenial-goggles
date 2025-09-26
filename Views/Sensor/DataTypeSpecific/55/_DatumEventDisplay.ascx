<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 55 - DifferentialPressureData-->


<%
    double valcompare = 0.0;

    if (!String.IsNullOrEmpty(Model.Scale))
    {
        Sensor sensor = Sensor.Load(Model.SensorID);
        List<UnitConversion> listOfConversions = Monnit.MonnitApplicationBase.GetScales(sensor, eDatumType.DifferentialPressureData);
        UnitConversion currentUnitOfMeasure = listOfConversions.Where(conversion => conversion.UnitLabel == Model.Scale).FirstOrDefault();
        if (currentUnitOfMeasure == null)
        {
            currentUnitOfMeasure = listOfConversions[0];
        }
        valcompare = (Model.CompareValue.ToDouble() - currentUnitOfMeasure.Intercept) / currentUnitOfMeasure.Slope;
    }
%>


<div class="reading-tag1">

    <div class="hidden-xs ruleDevice__icon">
    </div>
    <div class="tag-title"><span><%= Html.TranslateTag("Condition") %></span> </div>

    <div class="triggerDevice__name">
        <strong style="margin-top: 10px;"><%: Html.TranslateTag("When the Differential Pressure is")%>:
            <br />
        </strong>
        <span class="reading-tag-condition"><%=Html.TranslateTag(MonnitSession.NotificationInProgress.CompareType.ToString().Replace("_"," ")) %> <%=valcompare %> <%=Model.Scale %>
        </span>
    </div>

</div>

