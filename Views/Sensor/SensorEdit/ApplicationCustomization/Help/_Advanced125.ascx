<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|System Type","System Type")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Sets the HVAC system type and what items are controlled. The HVAC system must be defined prior to use. If undefined, none of the HVAC controls will turn on as a safety feature.","Sets the HVAC system type and what items are controlled. The HVAC system must be defined prior to use. If undefined, none of the HVAC controls will turn on as a safety feature.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Heat/Cool Mode","Heat/Cool Mode")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Sets whether the thermostat controls heating, cooling, emergency heat, or heating/cooling automatically. Emergency heat is an option on select HVAC systems and bypasses the primary heating system to only use the emergency/aux heating system as the primary.","Sets whether the thermostat controls heating, cooling, emergency heat, or heating/cooling automatically. Emergency heat is an option on select HVAC systems and bypasses the primary heating system to only use the emergency/aux heating system as the primary.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Temperature Buffer","Temperature Buffer")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Used in conjunction with the Setpoints to determine at what temperature the heating or cooling systems are activated.","Used in conjunction with the Setpoints to determine at what temperature the heating or cooling systems are activated.")%>
    </div>
    <div class="word-def">
        <span class="word-subheader"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Example: ","Example: ")%></span>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|If the setpoint is 70 F and buffer is set to 5 degrees, the heat will turn on at 65 F and off at 70 F. The thermostat does not turn off at the exact setpoint, it overshoots by 0.25 C to improve comfort and system efficiency.","If the setpoint is 70 F and buffer is set to 5 degrees, the heat will turn on at 65 F and off at 70 F. The thermostat does not turn off at the exact setpoint, it overshoots by 0.25 C to improve comfort and system efficiency.")%>
    </div>
    <hr />
</div>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Occupied Setpoint","Occupied Setpoint")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Temperature at which the heating and cooling systems turn off after they have been activated. The thermostat doesn’t turn off at this exact setpoint, it overshoots by 0.25 C to improve comfort and system efficiency.","Temperature at which the heating and cooling systems turn off after they have been activated. The thermostat doesn’t turn off at this exact setpoint, it overshoots by 0.25 C to improve comfort and system efficiency.")%>
        <hr />
    </div>
</div>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Occupied Timeout","Occupied Timeout")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|The amount of time after the occupied state has been triggered that the system will revert back to the unoccuped temperature settings.","The amount of time after the occupied state has been triggered that the system will revert back to the unoccuped temperature settings.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Unoccupied Heating and Cooling setpoints"," Unoccupied Heating and Cooling setpoints")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Temperature at which the heating and cooling systems turn off after they have been activated. The thermostat doesn’t turn off at this exact setpoint, it overshoots by 0.25 C to improve comfort and system efficiency.","Temperature at which the heating and cooling systems turn off after they have been activated. The thermostat doesn’t turn off at this exact setpoint, it overshoots by 0.25 C to improve comfort and system efficiency.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Heating and Cooling Activation Time","Heating and Cooling Activation Time")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Amount of time the 1st stage is allowed to run before activating the stage associated with this configuration. There will be a slight delay","Amount of time the 1st stage is allowed to run before activating the stage associated with this configuration. There will be a slight delay")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Heating and Cooling Activation Threshold","Heating and Cooling Activation Threshold")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|If the temperature goes beyond the setpoint by this amount the stage associated with this configuration will be activated immediately","If the temperature goes beyond the setpoint by this amount the stage associated with this configuration will be activated immediately")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Stage Load Balancing","Stage Load Balancing")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|When ON, balances the load between the 1st and 2nd stage heating or cooling ensuring they run approximately the same time overall. Only applies when the System Type has 2 or more stages for heating or cooling. When turning the heater or cooling on, this checks the time tally for each stage and will turn on whichever stage has the lowest total runtime first.","When ON, balances the load between the 1st and 2nd stage heating or cooling ensuring they run approximately the same time overall. Only applies when the System Type has 2 or more stages for heating or cooling. When turning the heater or cooling on, this checks the time tally for each stage and will turn on whichever stage has the lowest total runtime first.")%>
        <br />
        <br />
        <span style="font-style: italic"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Note: Should only be enabled when the 1st and 2nd stages have close to the same heating or cooling capacity. If unsure about this setting leave it OFF.","Note: Should only be enabled when the 1st and 2nd stages have close to the same heating or cooling capacity. If unsure about this setting leave it OFF.")%></span>
    </div>
    <hr />
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Reversing Valve Control","Reversing Valve Control")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Defines whether the reversing valve is energized to cool or energized to heat. If Undefined, the heating/cooling system will not activate.","Defines whether the reversing valve is energized to cool or energized to heat. If Undefined, the heating/cooling system will not activate.")%>
        <hr />
    </div>
</div>
<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Show Advanced Settings","Show Advanced Settings")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Allows for more advanced editing of thermostat if set to show.","Allows for more advanced editing of thermostat if set to show.")%>
        <hr />
    </div>
</div>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware when Occupied","Aware when Occupied")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|If turned on, the thermostat will report aware when motion is detected. \"The  Occupied Timeout\" configuration acts as a rearm period for reporting motion. ","If turned on, the thermostat will report aware when motion is detected. \"The  Occupied Timeout\" configuration acts as a rearm period for reporting motion. ")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware on State Change","Aware on State Change")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|If On, when the Thermostat reports a state change it will mark that data message as aware.","If On, when the Thermostat reports a state change it will mark that data message as aware.")%>
    </div>
    <div class="word-def" style="font-style: italic">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Note:","Note:")%>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|The thermostat reports all state changes no matter what. If marked Aware, the message will force the gateway to send that data to the server immediately if the gateway is also set to transmit Aware messages immediately.","The thermostat reports all state changes no matter what. If marked Aware, the message will force the gateway to send that data to the server immediately if the gateway is also set to transmit Aware messages immediately.")%>
    </div>
    <hr />
</div>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan Control Mode","FanControl Mode")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Various control states for the fan.","Various control states for the fan.")%><div></div>
        <div class="word-def">
            <span class="word-subheader"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Auto: ","Auto: ")%></span>
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|The HVAC system, not the thermostat, controls the fan.","The HVAC system, not the thermostat, controls the fan.")%>
        </div>
        <div class="word-def">
            <span class="word-subheader"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Auto+Periodic: ","Auto+Periodic: ")%></span>
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Enable configuration to turn the fan on for a period at the beginning of a longer period. It is meant to be used with Fan Period and Fan Interval configurations. If a cooling or heating event occurs, the Periodic Fan event time is reset.","Enable configuration to turn the fan on for a period at the beginning of a longer period. It is meant to be used with Fan Period and Fan Interval configurations. If a cooling or heating event occurs, the Periodic Fan event time is reset.")%>
        </div>
        <div class="word-def">
            <span class="word-subheader"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|On: ","On: ")%></span>
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|The fan is always on with this setting.","The fan is always on with this setting.")%>
        </div>
        <div class="word-def">
            <span class="word-subheader"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Active Fan Control: ","Active Fan Control: ")%></span>
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|The thermostat actively controls when the fan turns on/off when the heating/cooling is started and stopped. It is meant to be used with the various Fan Start Time configurations.","The thermostat actively controls when the fan turns on/off when the heating/cooling is started and stopped. It is meant to be used with the various Fan Start Time configurations.")%>
        </div>
        <div class="word-def">
            <span class="word-subheader"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Off: ","Off: ")%></span>
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|The HVAC system controls the fan during heating and cooling, not the thermostat.","The HVAC system controls the fan during heating and cooling, not the thermostat.")%>
        </div>
        <hr />
    </div>
</div>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan On Period","Fan On Period")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Amount of time the fan is forced on during the Fan Interval." ,"Amount of time the fan is forced on during the Fan Interval.")%>
        <br />
        <div class="word-def">
            <span class="word-subheader"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Example: ","Example: ")%></span>
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan On Period = 10 minutes, Fan Interval = 120 minutes. At the beginning of the 120 minute Fan Interval, the fan will be forced on for 10 minutes. After 10 minutes the fan will turn off. 120 minutes after the fan was forced on, the fan will be forced on again. Fan On Period must be less than Fan Auto Period.","Fan On Period = 10 minutes, Fan Interval = 120 minutes. At the beginning of the 120 minute Fan Interval, the fan will be forced on for 10 minutes. After 10 minutes the fan will turn off. 120 minutes after the fan was forced on, the fan will be forced on again. Fan On Period must be less than Fan Auto Period.")%>
        </div>
        <hr />
    </div>
</div>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan Interval","Fan Interval")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Interval, in which, Fan On Period operates. Fan Interval must be greater than Fan On Period.","Interval in which Fan On Period operates. Fan Interval must be greater than Fan On Period.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan Start Time For Heater","Fan Start Time For Heater")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan starts this amount of time before(-)/after(+) the heater starts.","Fan starts this amount of time before(-)/after(+) the heater starts.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan Stop Time For Heater","Fan Stop Time For Heater")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan stops this amount of time before(-)/after(+) the heater stops.","Fan stops this amount of time before(-)/after(+) the heater stops.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan Start Time For Cooler","Fan Start Delay For Cooler")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan starts this amount of time before(-)/after(+) the cooler starts.","Fan starts this amount of time before(-)/after(+) the cooler starts.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan Stop Time For Cooler","Fan Stop Delay For Cooler")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan stops this amount of time before(-)/after(+) the cooler stops.","Fan stops this amount of time before(-)/after(+) the cooler stops.")%>
        <hr />
    </div>
</div>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Calibration/Controls","Calibration/Controls")%>
    </div>
    <div class="word-def">
        <span class="word-subheader"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan Override: ","Fan Override: ")%></span>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Turns the fan on for a set period.","Turns the fan on for a set period.")%>
    </div>
    <div class="word-def">
        <span class="word-subheader"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Occupancy Override: ","Occupancy Override: ")%></span>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|This command will force the thermostat to enter the occupied state and use the Occupied Temperature settings for a selected duration.","This command will force the thermostat to enter the occupied state and use the Occupied Temperature settings for a selected duration.")%>
    </div>
    <div class="word-def">
        <span class="word-subheader"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Unoccupied Override: ","Unoccupied Override: ")%></span>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|This command forces the thermostat to enter the unoccupied state and use the Unoccupied Temperature settings for the selected duration.","This command forces the thermostat to enter the unoccupied state and use the Unoccupied Temperature settings for the selected duration.")%>
    </div>
    <div class="word-def">
        <span class="word-subheader"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Temperature Offset: ","Temperature Offset: ")%></span>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Adjusts temperature readings by the Offset Value. Example: An offset of 2°C will change a 20°C reading to a 22°C.","Adjusts temperature readings by the Offset Value. Example: An offset of 2°C will change a 20°C reading to a 22°C.")%>
    </div>
    <div class="word-def">
        <span class="word-subheader"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Humidity Offset: ","Humidity Offset: ")%></span>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Adjusts humidity readings by the Offset Value. Example: An offset of 2% will change a 20% reading to a 22%. It has no effect if the humidity hardware option isn't installed.","Adjusts humidity readings by the Offset Value. Example: An offset of 2% will change a 20% reading to a 22%. It has no effect if the humidity hardware option isn't installed.")%>
    </div>
    <hr />
</div>

