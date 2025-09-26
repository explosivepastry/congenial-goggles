<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<% NotificationByTime timedNotification = Model.NotificationByTime;%>

<tr>
    <td>Send Notification at</td>
    <td>
        <select id="ScheduledHour" name="ScheduledHour" style="width: 50px;">
            <option value="0" <%= timedNotification.ScheduledHour % 12 == 0 ? "selected":""%>>12</option>
            <option value="1" <%= timedNotification.ScheduledHour % 12 == 1 ? "selected":""%>>1</option>
            <option value="2" <%= timedNotification.ScheduledHour % 12 == 2 ? "selected":""%>>2</option>
            <option value="3" <%= timedNotification.ScheduledHour % 12 == 3 ? "selected":""%>>3</option>
            <option value="4" <%= timedNotification.ScheduledHour % 12 == 4 ? "selected":""%>>4</option>
            <option value="5" <%= timedNotification.ScheduledHour % 12 == 5 ? "selected":""%>>5</option>
            <option value="6" <%= timedNotification.ScheduledHour % 12 == 6 ? "selected":""%>>6</option>
            <option value="7" <%= timedNotification.ScheduledHour % 12 == 7 ? "selected":""%>>7</option>
            <option value="8" <%= timedNotification.ScheduledHour % 12 == 8 ? "selected":""%>>8</option>
            <option value="9" <%= timedNotification.ScheduledHour % 12 == 9 ? "selected":""%>>9</option>
            <option value="10" <%= timedNotification.ScheduledHour % 12 == 10 ? "selected":""%>>10</option>
            <option value="11" <%= timedNotification.ScheduledHour % 12 == 11 ? "selected":""%>>11</option>
        </select>
        :
              <select id="ScheduledMinute" name="ScheduledMinute" style="width: 50px;">
                  <option value="00" <%= timedNotification.ScheduledMinute == 00 ? "selected":""%>>00</option>
                  <option value="15" <%= timedNotification.ScheduledMinute == 15 ? "selected":""%>>15</option>
                  <option value="30" <%= timedNotification.ScheduledMinute == 30 ? "selected":""%>>30</option>
                  <option value="45" <%= timedNotification.ScheduledMinute == 45 ? "selected":""%>>45</option>
              </select>
        <select id="AMorPM" name="AMorPM" style="width: 50px;">
                  <option value="0"  <%= timedNotification.ScheduledHour < 12 ? "selected":""%>>am</option>
                  <option value="12"  <%= timedNotification.ScheduledHour > 11 ? "selected":""%>>pm</option>
              </select>
    </td>
    <td></td>
</tr>
<tr>
    <td></td>
  <td><%= Html.ValidationMessageFor(model => model.NotificationByTime.ScheduledHour)%> <%= Html.ValidationMessageFor(model => model.NotificationByTime.ScheduledMinute)%></td>
    <td></td>
</tr>
