<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<% Account acc = Account.Load(Model.AccountID);
    if (acc.CurrentSubscription.AccountSubscriptionType.Can("sensor_advanced_edit") && MonnitSession.CustomerCan("Sensor_Advanced_Configuration"))
    {%>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Overview/SensorScale/Help|Type of Bridge / Unit","Type of Bridge / Unit")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Overview/SensorScale/Help|Allows you to choose a bridge type. Based on the selected type, different units of measure will be available to choose from.","Allows you to choose a bridge type. Based on the selected type, different units of measure will be available to choose from.")%>
        <br />
        <br />
        <b><%:Html.TranslateTag("Sensor/ApplicationCustomization/Help|Available Options:", "Available Options:")%></b>
        <br />
        <%: Html.TranslateTag("Overview/SensorScale/Help|Load cell (gf, kgf, tf, N, kN, lbs, oz)","Load cell (gf, kgf, tf, N, kN, lbs, oz)")%>
        <br />
        <%: Html.TranslateTag("Overview/SensorScale/Help|Pressure (Bar, kpa, Mpa, kg/cm², kg/m², psi, Torr)","Pressure (Bar, kpa, Mpa, kg/cm², kg/m², psi, Torr)")%>
        <br />
        <%: Html.TranslateTag("Overview/SensorScale/Help|Displacement (mm, cm, m, in, ft)","Displacement (mm, cm, m, in, ft)")%>
        <br />
        <%: Html.TranslateTag("Overview/SensorScale/Help|Torque · (N·m, kgf·m, kgf·cm, ft·lb, in·lb) ","Torque (N·m, kgf·m, kgf·cm, ft·lb, in·lb) ")%>
        <br />
        <%: Html.TranslateTag("Overview/SensorScale/Help|Inclinometer (degree, rad, grade)","Inclinometer (degree, rad, grade)")%>
        <br />
        <%: Html.TranslateTag("Overview/SensorScale/Help|Strain (με) ","Strain (με) ")%>
        <br />
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Overview/SensorScale/Help|Capacity","Capacity")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Overview/SensorScale/Help|Maximum formatted value produced by the sensor.","Maximum formatted value produced by the sensor.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Overview/SensorScale/Help|Rated Output (mV/V)","Rated Output (mV/V)")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Overview/SensorScale/Help|Raw value produced by the bridge at Capacity.","Raw value produced by the bridge at Capacity.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Overview/SensorScale/Help|Precision","Precision")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Overview/SensorScale/Help|Number of decimals to display.","Number of decimals to display.")%>
        <hr />
    </div>
</div>
<%} %>


<div class="clearfix"></div>
