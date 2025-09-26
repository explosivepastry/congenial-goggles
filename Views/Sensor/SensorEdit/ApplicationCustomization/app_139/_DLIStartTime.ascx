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

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization|139-DLI-StartTime-Title","DLI Reset Time")%>
    </div>
    <div class="col sensorEditFormInput">
        <select class="form-select ms-0" <%=Model.CanUpdate ? "" : "disabled" %> name="DLIStartTime" id="DLIStartTime">
            <%for(int h = 0; h<=23; h++) {
                for(int m = 0; m<=3; m++) { %>
            <option value="<%:HourArray[h] + MinuteArray[m] %>" <%=(UTCStartTimeHour*4 == HourArray[h] && UTCStartTimeMinute == MinuteArray[m]) ? "selected='selected'" : "" %>><%: String.Format("{0,2}:{1,2:00} {2}",h==0?12:h>12?h-12:h,(m*15),h<12?"AM":"PM")%></option>
            <%  } 
            } %>
        </select>
        
        <%: Html.ValidationMessage("DLIStartTime")%>
    </div>
</div>
