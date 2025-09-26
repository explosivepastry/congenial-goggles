<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Monnit.Sensor>>" %>

<div class="SelectSensorsDiv">
        <%foreach (Monnit.Sensor s in Model)
          {%>
            <div class="checkSensor <%:s.SensorID%>" style="width: 250px; float: left; padding-top: 5px; border-bottom: dotted 1px grey;">
            <% if (s.CanUpdate)
               {%>
            <input type="checkbox" id="<%:s.SensorID%>" checked="checked" style="margin-right: 10px;" /><label for="<%: (s.SensorID)%>"><%: (s.SensorName) %></label>
            <%}
               else
               {%>
            <span style="margin-left: 20px; position: relative; top: 3px;"><%: (s.SensorName) %></span>
            <%}%>
        </div>
        <%}%>

