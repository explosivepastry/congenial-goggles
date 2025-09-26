<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Monnit.Sensor>>" %>
<%="" %>
<%
    Notification notification = ViewBag.Notification;
  %>            
            
    <table border="0">
    <%foreach (Sensor item in Model)
        {

            if (item.ApplicationID == 13)
            {
                NotificationRecipient notifierRecipient = notification.NotificationRecipients.FirstOrDefault(m => { return m.DeviceToNotifyID == item.SensorID; });
                bool LED = false;
                bool Buzzer = false;
                bool AutoScroll = false;
                bool BackLight = false;
                string DeviceName = item.SensorName.ToStringSafe();
                if (notifierRecipient != null)
                {
                    Attention.ParseSerializedRecipientProperties(notifierRecipient.SerializedRecipientProperties, out LED, out Buzzer, out AutoScroll, out BackLight, out DeviceName);
                }
                else
                {
                    LED = true;
                    Buzzer = true;
                    AutoScroll = true;
                    BackLight = true;
                }
    %>
        <tr>
            <td>
                <a href="Remove" onclick="toggleDevice(<%:item.SensorID %>, 'Notifier', false); return false;" class="nrd<%:item.SensorID %> Notifier" style="display:<%:notifierRecipient != null ? "inline" : "none"%>;">
                    <img src="/Content/images/notification/attention-on.png" class="deviceIcon" alt="Attention-on">
                </a>
                <a href="Add" onclick="toggleDevice(<%:item.SensorID %>, 'Notifier', true); return false;" class="nrd<%:item.SensorID %> Notifier" style="display:<%:notifierRecipient == null ? "inline" : "none"%>;">
                    <img src="/Content/images/notification/attention-off.png" class="deviceIcon" alt="Attention-off">
                </a>
            </td>
            <td><%:item.SensorName %></td>
            <td>
                <div class="nrd<%:item.SensorID %> Notifier" style="display:<%:notifierRecipient != null ? "inline" : "none"%>;">
                    <a href="LED_Off" title="LED Active" onclick="deviceState(<%:item.SensorID %>, 'Notifier', 'led', 'Off'); return false;" style="display:<%:LED ? "inline" : "none"%>;"><img src="/Content/images/notification/LED-on.png" alt="LED Active"></a>
                    <a href="LED_On" title="LED Inactive"  onclick="deviceState(<%:item.SensorID %>, 'Notifier', 'led', 'On'); return false;" style="display:<%:!LED ? "inline" : "none"%>;"><img src="/Content/images/notification/LED-off.png" alt="LED Inactive"></a>
                </div>
            </td>
            <td>
                <div class="nrd<%:item.SensorID %> Notifier" style="display:<%:notifierRecipient != null ? "inline" : "none"%>;">
                    <a href="Buzzer_Off" title="Buzzer Active" onclick="deviceState(<%:item.SensorID %>, 'Notifier', 'buzzer', 'Off'); return false;" style="display:<%:Buzzer ? "inline" : "none"%>;"><img src="/Content/images/notification/Buzzer-on.png" alt="Buzzer Active"></a>
                    <a href="Buzzer_On" title="Buzzer Inactive"  onclick="deviceState(<%:item.SensorID %>, 'Notifier', 'buzzer', 'On'); return false;" style="display:<%:!Buzzer ? "inline" : "none"%>;"><img src="/Content/images/notification/Buzzer-off.png" alt="Buzzer Inactive"></a>
                </div>
            </td>
            <td>
                <div class="nrd<%:item.SensorID %> Notifier" style="display:<%:notifierRecipient != null ? "inline" : "none"%>;">
                    <a href="Screen_Off" title="Scroll on and backlight on" onclick="deviceState(<%:item.SensorID %>, 'Notifier', 'screen', 'Off'); return false;" style="display:<%:AutoScroll && BackLight ? "inline" : "none"%>;"><img src="/Content/images/notification/ScreenBacklight.png" alt="Scroll on and backlight on"></a>
                    <a href="Backlight_On" title="Scroll on and backlight off"  onclick="deviceState(<%:item.SensorID %>, 'Notifier', 'screen', 'On'); return false;" style="display:<%:AutoScroll && !BackLight ? "inline" : "none"%>;"><img src="/Content/images/notification/ScreenScroll.png" alt="Scroll on and backlight off"></a>
                    <a href="Scroll_On" title="Screen off"  onclick="deviceState(<%:item.SensorID %>, 'Notifier', 'screen', 'Scroll'); return false;" style="display:<%:!AutoScroll ? "inline" : "none"%>;"><img src="/Content/images/notification/Screen.png" alt="Screen off"></a>
                </div>
            </td>
        </tr>
        <tr>
        	<td colspan="5"><hr class="recipientDivider"></td>
        </tr>

    <% } %>

    <!-- Control Relay 1 -->

    <%  if (item.ApplicationID == 12)
        {
            NotificationRecipient controlRecipient = null;
            int State1 = 0;
            int State2 = 0;
            ushort Time1 = 0;
            ushort Time2 = 0;
            foreach (NotificationRecipient nr in notification.NotificationRecipients.Where(m => { return m.DeviceToNotifyID == item.SensorID; }))
            {
                Control_1.ParseSerializedRecipientProperties(nr.SerializedRecipientProperties, out State1, out State2, out Time1, out Time2);
                if (State1 > 0)
                {
                    controlRecipient = nr;
                    break;
                }
            }
    %>
        <tr>
            <td>
                <a href="Remove" onclick="toggleDevice(<%:item.SensorID %>, 'Control1', false); return false;" class="nrd<%:item.SensorID %> Control1" style="display:<%:controlRecipient != null && State1 > 0 ? "inline" : "none"%>;">
                    <img src="/Content/images/notification/control-on.png" class="deviceIcon" alt="Control">
                </a>
                <a href="Add" onclick="toggleDevice(<%:item.SensorID %>, 'Control1', true); return false;" class="nrd<%:item.SensorID %> Control1" style="display:<%:controlRecipient == null || State1 == 0 ? "inline" : "none"%>;">
                    <img src="/Content/images/notification/control-off.png" class="deviceIcon" alt="Control">
                </a>
            </td>
            <td>
                <%: item.SensorName%><br />
                <%:Control_1.Relay1Name(item.SensorID) %>
            </td>
            <td>
                <div class="nrd<%:item.SensorID %> Control1" style="display:<%:controlRecipient != null && State1 > 0 ? "inline" : "none"%>;">
                    <a href="Relay_On" title="Relay Off" onclick="deviceState(<%:item.SensorID %>, 'Control1', 'Relay1', 'On'); return false;" style="display:<%:State1 == 1 || State1 == 0 ? "inline" : "none"%>;"><img src="/Content/images/notification/relay-off.png" alt="Relay Off"></a>
                    <a href="Relay_Toggle" title="Relay On" onclick="deviceState(<%:item.SensorID %>, 'Control1', 'Relay1', 'Toggle'); return false;" style="display:<%:State1 == 2 ? "inline" : "none"%>;"><img src="/Content/images/notification/relay-on.png" alt="Relay On"></a>
                    <a href="Relay_Off" title="Relay Toggle" onclick="deviceState(<%:item.SensorID %>, 'Control1', 'Relay1', 'Off'); return false;" style="display:<%:State1 == 3 ? "inline" : "none"%>;"><img src="/Content/images/notification/relay-toggle.png" alt="Toggle"></a>
                </div>
            </td>
            <td>
                <div class="nrd<%:item.SensorID %> Control1" style="display:<%:controlRecipient != null && State1 > 0 ? "inline" : "none"%>;">
                    <a href="Timer_Off" title="Timer On" onclick="deviceState(<%:item.SensorID %>, 'Control1', 'Time1', '0'); return false;" style="display:<%:Time1 > 0 ? "inline" : "none"%>;"><img src="/Content/images/notification/timer-on.png" alt="Timer On"></a>
                    <a href="Timer_On" title="Timer Off" onclick="deviceState(<%:item.SensorID %>, 'Control1', 'Time1', '60'); return false;" style="display:<%:Time1 == 0 ? "inline" : "none"%>;"><img src="/Content/images/notification/timer-off.png" alt="Timer Off"></a>
                </div>
            </td>
            <td>
                <div class="nrd<%:item.SensorID %> Control1" style="display:<%:controlRecipient != null && State1 > 0 ? "inline" : "none"%>;">
                    <div style="display:<%:Time1 > 0 ? "inline" : "none"%>;">
                        <table>
                            <tr>
                                <td>
                                     <input type="text" class="shortTimer minutes" onchange="AddMinutes($(this),<%:item.SensorID %>, 'Control1', 'Time1'); return false;"" value="<%:Time1/60 %>" />
                                
                                     <input type="text" class="shortTimer seconds" onchange="AddSeconds($(this),<%:item.SensorID %>, 'Control1', 'Time1'); return false;"" value="<%:Time1%60 %>" /> 
                                </td>
                           </tr>
                           <tr>
                                <td>
                                     <div  style="text-align: start; " class="timeSec">&nbsp Minutes &nbsp&nbsp Seconds</div>
                               </td>
                           </tr>
                       </table>
                    </div>
                </div>
            </td>
        </tr>
        

        <tr>
        	<td colspan="5"><hr class="recipientDivider"></td>
        </tr>

        <!-- Control Relay 2 -->
        <%  controlRecipient = null;
            foreach (NotificationRecipient nr in notification.NotificationRecipients.Where(m => { return m.DeviceToNotifyID == item.SensorID; }))
            {
                Control_1.ParseSerializedRecipientProperties(nr.SerializedRecipientProperties, out State1, out State2, out Time1, out Time2);
                if (State2 > 0)
                {
                    controlRecipient = nr;
                    break;
                }
            }
        %>
        <tr>
            <td>
                <a href="Remove" onclick="toggleDevice(<%:item.SensorID %>, 'Control2', false); return false;" class="nrd<%:item.SensorID %> Control2" style="display:<%:controlRecipient != null && State2 > 0 ? "inline" : "none"%>;">
                    <img src="/Content/images/notification/control-on.png" class="deviceIcon" alt="Control">
                </a>
                <a href="Add" onclick="toggleDevice(<%:item.SensorID %>, 'Control2', true); return false;" class="nrd<%:item.SensorID %> Control2" style="display:<%:controlRecipient == null || State2 == 0 ? "inline" : "none"%>;">
                    <img src="/Content/images/notification/control-off.png" class="deviceIcon" alt="Control">
                </a>
            </td>
            <td>
                <%: item.SensorName%><br />
                <%:Control_1.Relay2Name(item.SensorID) %>
            </td>
            <td>
                <div class="nrd<%:item.SensorID %> Control2" style="display:<%:controlRecipient != null && State2 > 0 ? "inline" : "none"%>;">
                    <a href="Relay_On" title="Relay Off" onclick="deviceState(<%:item.SensorID %>, 'Control2', 'Relay2', 'On'); return false;" style="display:<%:State2 == 1 || State2 == 0 ? "inline" : "none"%>;"><img src="/Content/images/notification/relay-off.png" alt="Relay Off"></a>
                    <a href="Relay_Toggle" title="Relay On" onclick="deviceState(<%:item.SensorID %>, 'Control2', 'Relay2', 'Toggle'); return false;" style="display:<%:State2 == 2 ? "inline" : "none"%>;"><img src="/Content/images/notification/relay-on.png" alt="Relay On"></a>
                    <a href="Relay_Off" title="Relay Toggle" onclick="deviceState(<%:item.SensorID %>, 'Control2', 'Relay2', 'Off'); return false;" style="display:<%:State2 == 3 ? "inline" : "none"%>;"><img src="/Content/images/notification/relay-toggle.png" alt="Toggle"></a>
                </div>
            </td>
            <td>
                <div class="nrd<%:item.SensorID %> Control2" style="display:<%:controlRecipient != null && State2 > 0 ? "inline" : "none"%>;">
                    <a href="Timer_Off" title="Timer On" onclick="deviceState(<%:item.SensorID %>, 'Control2', 'Time2', '0'); return false;" style="display:<%:Time2 > 0 ? "inline" : "none"%>;"><img src="/Content/images/notification/timer-on.png" alt="Timer On"></a>                   
                    <a href="Timer_On" title="Timer Off" onclick="deviceState(<%:item.SensorID %>, 'Control2', 'Time2', '60'); return false;" style="display:<%:Time2 == 0 ? "inline" : "none"%>;"><img src="/Content/images/notification/timer-off.png" alt="Timer Off"></a>                   
                </div>
            </td>
            <td>
                <div class="nrd<%:item.SensorID %> Control2" style="display:<%:controlRecipient != null && State2 > 0 ? "inline" : "none"%>;">
                   <div style="display:<%:Time2 > 0 ? "inline" : "none"%>;">
                               <table>
                            <tr>
                                <td>
                                    <input type="text" class="shortTimer minutes" onchange="AddMinutes($(this),<%:item.SensorID %>, 'Control2','Time2'); return false;"" value="<%:Time2/60 %>" />
                                
                                    <input type="text" class="shortTimer seconds" onchange="AddSeconds($(this),<%:item.SensorID %>, 'Control2', 'Time2'); return false;"" value="<%:Time2%60 %>" />                                                                         
                                </td>
                           </tr>
                           <tr>
                                <td>
                                     <div  style="text-align: start; " class="timeSec">&nbsp Minutes &nbsp&nbsp Seconds</div>
                               </td>
                           </tr>
                       </table>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
        	<td colspan="5"><hr class="recipientDivider"></td>
        </tr>
    <% } %>
       
    <!-- Basic Control -->  
    <%  if (item.ApplicationID == 76)
        {
            NotificationRecipient controlRecipient = null;
            int State1 = 60;
            ushort Time1 = 0;
            foreach (NotificationRecipient nr in notification.NotificationRecipients.Where(m => { return m.DeviceToNotifyID == item.SensorID; }))
            {
                BasicControl.ParseSerializedRecipientProperties(nr.SerializedRecipientProperties, out State1, out Time1);
                if (State1 > 0)
                {
                    controlRecipient = nr;
                    break;
                }
            }
    %>
        <tr>
            <td>
                <a href="Remove" onclick="toggleDevice(<%:item.SensorID %>, 'BasicControl', false); return false;" class="nrd<%:item.SensorID %> BasicControl" style="display:<%:controlRecipient != null && State1 > 0 ? "inline" : "none"%>;">
                    <img src="/Content/images/notification/control-on.png" class="deviceIcon" alt="Control">
                </a>
                <a href="Add" onclick="toggleDevice(<%:item.SensorID %>, 'BasicControl', true); return false;" class="nrd<%:item.SensorID %> BasicControl" style="display:<%:controlRecipient == null || State1 == 0 ? "inline" : "none"%>;">
                    <img src="/Content/images/notification/control-off.png" class="deviceIcon" alt="Control">
                </a>
            </td>
            <td>
                <%: item.SensorName%><br />
                <%:BasicControl.Relay1Name(item.SensorID) %>
            </td>
            <td>
                <div class="nrd<%:item.SensorID %> BasicControl" style="display:<%:controlRecipient != null && State1 > 0 ? "inline" : "none"%>;">
                    <a href="Relay_On" title="Relay Off" onclick="deviceState(<%:item.SensorID %>, 'BasicControl', 'Relay1', 'On'); return false;" style="display:<%:State1 == 1 || State1 == 0 ? "inline" : "none"%>;"><img src="/Content/images/notification/relay-off.png" alt="Relay Off"></a>
                    <a href="Relay_Toggle" title="Relay On" onclick="deviceState(<%:item.SensorID %>, 'BasicControl', 'Relay1', 'Toggle'); return false;" style="display:<%:State1 == 2 ? "inline" : "none"%>;"><img src="/Content/images/notification/relay-on.png" alt="Relay On"></a>
                    <a href="Relay_Off" title="Relay Toggle" onclick="deviceState(<%:item.SensorID %>, 'BasicControl', 'Relay1', 'Off'); return false;" style="display:<%:State1 == 3 ? "inline" : "none"%>;"><img src="/Content/images/notification/relay-toggle.png" alt="Toggle"></a>
                </div>
            </td>
            <td>
                <div class="nrd<%:item.SensorID %> BasicControl" style="display:<%:controlRecipient != null && State1 > 0 ? "inline" : "none"%>;">
                    <a href="Timer_Off" title="Timer On" onclick="deviceState(<%:item.SensorID %>, 'BasicControl', 'Time1', '0'); return false;" style="display:<%:Time1 > 0 ? "inline" : "none"%>;"><img src="/Content/images/notification/timer-on.png" alt="Timer On"></a>
                    <a href="Timer_On" title="Timer Off" onclick="deviceState(<%:item.SensorID %>, 'BasicControl', 'Time1', '60'); return false;" style="display:<%:Time1 == 0 ? "inline" : "none"%>;"><img src="/Content/images/notification/timer-off.png" alt="Timer Off"></a>
                </div>
            </td>
            <td>
                <div class="nrd<%:item.SensorID %> BasicControl" style="display:<%:controlRecipient != null && State1 > 0 ? "inline" : "none"%>;">
                    <div style="display:<%:Time1 > 0 ? "inline" : "none"%>;">              
                        <table>
                            <tr>
                                <td>
                                    
                                     <input type="text" class="shortTimer minutes" onchange="AddMinutes($(this),<%:item.SensorID %>, 'BasicControl', 'Time1'); return false;"" value="<%:Time1/60 %>" />
                                
                                     <input type="text" class="shortTimer seconds" onchange="AddSeconds($(this),<%:item.SensorID %>, 'BasicControl', 'Time1'); return false;"" value="<%:Time1%60 %>" /> 
                                </td>
                           </tr>
                           <tr>
                                <td>
                                     <div  style="text-align: start; " class="timeSec">&nbsp Minutes &nbsp&nbsp Seconds</div>
                               </td>
                           </tr>
                       </table>
                    </div>
                </div>
            </td>
        </tr>
        
        <%
            controlRecipient = null;
            foreach (NotificationRecipient nr in notification.NotificationRecipients.Where(m => { return m.DeviceToNotifyID == item.SensorID; }))
            {
                BasicControl.ParseSerializedRecipientProperties(nr.SerializedRecipientProperties, out State1, out Time1);
                if (State1 > 0)
                {
                    controlRecipient = nr;
                    break;
                }
            }
        %>
       
        <tr>
        	<td colspan="5"><hr class="recipientDivider"></td>
        </tr>
    <% } %>   
     
    <!-- Thermostat -->
    <%  if (item.ApplicationID == 97 || item.ApplicationID == 125)
        {
            NotificationRecipient controlRecipient = null;
            int Occupany = 1;
            ushort Duration = 30;
            foreach (NotificationRecipient nr in notification.NotificationRecipients.Where(m => { return m.DeviceToNotifyID == item.SensorID; }))
            {
                if (item.ApplicationID == 97)
                {
                    Thermostat.ParseSerializedRecipientProperties(nr.SerializedRecipientProperties, out Occupany, out Duration);
                }

                if (item.ApplicationID == 125)
                {
                    MultiStageThermostat.ParseSerializedRecipientProperties(nr.SerializedRecipientProperties, out Occupany, out Duration);
                }

                controlRecipient = nr;
                break;
            }
        %>
            <tr>
                <td>
                    <a href="Remove" title="Thermostat On" onclick="toggleDevice(<%:item.SensorID %>, 'Thermostat', false); return false;" class="nrd<%:item.SensorID %> Thermostat" style="display:<%:controlRecipient != null ? "inline" : "none"%>;">
                        <img src="/Content/images/notification/thermostat-on.png" class="deviceIcon" alt="Control">
                    </a>
                    <a href="Add" title="Thermostat Off" onclick="toggleDevice(<%:item.SensorID %>, 'Thermostat', true); return false;" class="nrd<%:item.SensorID %> Thermostat" style="display:<%:controlRecipient == null ? "inline" : "none"%>;">
                        <img src="/Content/images/notification/thermostat-off.png" class="deviceIcon" alt="Control">
                    </a>
                </td>
                <td>
                    <%: item.SensorName%>
                </td>
                <td>
                    <div class="nrd<%:item.SensorID %> Thermostat" style="display:<%:controlRecipient != null ? "inline" : "none"%>;">
                        <a href="Occupancy_On" title="Occupancy Off" onclick="deviceState(<%:item.SensorID %>, 'Thermostat', 'Occupancy', 'On'); return false;" style="display:<%:Occupany == 0 ? "inline" : "none"%>;"><img src="/Content/images/notification/occupied-off.png" alt="Occupancy Off"></a>                        
                        <a href="Occupancy_Off" title="Occupancy On" onclick="deviceState(<%:item.SensorID %>, 'Thermostat', 'Occupancy', 'Off'); return false;" style="display:<%:Occupany == 1 ? "inline" : "none"%>;"><img src="/Content/images/notification/occupied-on.png" alt="Occupancy On"></a>
                    </div>
                </td>
                <td>
                    <div class="nrd<%:item.SensorID %> Thermostat" style="display:<%:controlRecipient != null ? "inline" : "none"%>;">
                        <a title="Duration"  style="display:inline"><img src="/Content/images/notification/timer-on.png" alt="Timer On"></a>
                    </div>
                </td>
                <td>
                    <div class="nrd<%:item.SensorID %> Thermostat" style="display:<%:controlRecipient != null ? "inline" : "none"%>;">
                        <div style="display:inline">              
                            <table>
                                <tr>
                                    <td>
                                    
                                         <input type="text" class="shortTimer minutes" onchange="deviceState(<%:item.SensorID %>, 'Thermostat', 'Duration', $(this).val()); return false;"" value="<%:Duration %>" />
                                         <input type="hidden" class="shortTimer seconds" value="0" />
                                                                        
                                    </td>
                               </tr>
                               <tr>
                                    <td>
                                         <div  style="text-align: start; " class="timeSec">&nbsp Minutes</div>
                                   </td>
                               </tr>
                           </table>
                        </div>
                    </div>
                </td>
            </tr>
        
            <%
                controlRecipient = null;
                foreach (NotificationRecipient nr in notification.NotificationRecipients.Where(m => { return m.DeviceToNotifyID == item.SensorID; }))
                {
                    Thermostat.ParseSerializedRecipientProperties(nr.SerializedRecipientProperties, out Occupany, out Duration);
                    if (Occupany != 0)
                    {
                        controlRecipient = nr;
                        break;
                    }
                }
            %>
       
            <tr>
        	    <td colspan="5"><hr class="recipientDivider"></td>
            </tr>
        <% }
            } %>       
    </table>
