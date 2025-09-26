<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    int UTCStartTime = LightSensor_PPFD.GetDLIStartTime(Model);
    
    int UTCStartTimeHour = UTCStartTime / 4;
    int UTCStartTimeMinute = UTCStartTime % 4;
    
    DateTime UTC = DateTime.UtcNow;
    UTC = new DateTime(UTC.Year, UTC.Month, UTC.Day, UTCStartTimeHour, UTCStartTimeMinute,0,DateTimeKind.Utc);
    DateTime AccountTime = Monnit.TimeZone.GetLocalTimeById(UTC, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    
    int HourDifference = AccountTime.Hour - UTC.Hour;
    if(HourDifference < 0) HourDifference +=24;
    if(HourDifference > 24) HourDifference -= 24;
    int MinuteDifference = AccountTime.Minute - UTC.Minute;

    
    int[] HourArray = new int[24];
    for(int i = 0; i<=23; i++ )
    {
        //i = their local hour   
        //[i] = UTC Equivalent hour
        HourArray[i] = i * 4 - HourDifference*4; 
        if(HourArray[i] < 0)
            HourArray[i] += 96;
        if(HourArray[i] >= 96)
            HourArray[i] -= 96;
    }

    int[] MinuteArray = new int[4];
    for(int i = 0; i<=3; i++ )
    {
        //i = their local minute/15
        //[i] = UTC equivalent minute
        MinuteArray[i] = i - MinuteDifference/15; 
        if(MinuteArray[i] < 0)
            MinuteArray[i] += 4;
        if(MinuteArray[i] > 3)
            MinuteArray[i] -= 4;  
    }

%>

<input type="hidden" name="DLIStartTime" id="DLIStartTime" value="<%:HourArray[23] + MinuteArray[2] %>" />
