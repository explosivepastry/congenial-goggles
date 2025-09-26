<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<%
    bool SensorIsInStudyMode = (Model.MinimumThreshold & 0x01) == 0x01;
    bool StudyModeChangePending = (Model.Hysteresis & 0x03) > 0;

    bool SensorIsInPriority = (Model.MinimumThreshold & 0x04) == 0x04;
    bool PriorityChangePending = (Model.Hysteresis & 0x0C) > 0;
    DataMessage.CacheLastByNetwork(MonnitSession.CurrentCustomer.AccountID, new TimeSpan(0, 1, 0));
    int PriorityCount = Sensor.LoadByCsNetID(Model.CSNetID).Where(s =>
        s.ApplicationID == 60
        && s.LastDataMessage != null
        && Model.LastDataMessage != null
        && s.LastDataMessage.GatewayID == Model.LastDataMessage.GatewayID
        && (s.MinimumThreshold & 0x04) == 0x04).Count();

    bool ClearBacklogPending = (Model.Hysteresis & 0x10) > 0;
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|Behavior Mode","Behavior Mode")%>
    </div>
    <div class="col sensorEditFormInput">
        <%if (SensorIsInStudyMode)
          { %>
                    <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|Study Mode","Study Mode")%>
                    <%if (StudyModeChangePending)
                      {%>
                        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|(Pending Mode Change)","(Pending Mode Change)")%>
                    <%}
                      else
                      {%>
        <a class="actionControlCommand btn btn-primary" data-command="205" ><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|Change to Standby Mode","Change to Standby Mode")%></a>
        <%} %>
        <%}
          else
          {%>
                    <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|Standby Mode","Standby Mode")%>
                    <%if (StudyModeChangePending)
                      {%>
                        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|(Pending Mode Change)","(Pending Mode Change)")%>
                    <%}
                      else
                      {%>
        <a class="actionControlCommand btn btn-primary" data-command="204" ><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|Change to Study Mode","Change to Study Mode")%></a>
        <%    }
          } %>
    </div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|Backlog Delivery","Backlog Delivery")%>
    </div>
    <div class="col sensorEditFormInput">
        <%if (SensorIsInPriority)
          { %>
                    <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|Priority High","Priority High")%> 
                <%}
          else
          {%>
                    <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|Priority Standard","Priority Standard")%>
                <%} %>


        <%if (PriorityChangePending)
          { %>
                    <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|(Pending Priority Change)","(Pending Priority Change)")%>
                <%}
          else
          {%>
                    (<%:PriorityCount %> <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|devices with Priority High)","devices with Priority High)")%>
                    <%if (SensorIsInPriority)
                      { %>
        <a class="actionControlCommand btn btn-primary" data-command="203" ><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|Change Priority to Low","Change Priority to Low")%></a>
        <%}
                      else if (SensorIsInStudyMode)
                      { %>
                        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|change Standby Mode to enable","change Standby Mode to enable")%>
                    <%}
                      else if (PriorityCount < 5)
                      {%>
        <a class="actionControlCommand btn btn-primary" data-command="202" ><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|Change Priority to High","Change Priority to High")%></a>
        <%}
                      else
                      {%>
                        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|Only 5 devices per gateway are permitted to be prioritize backlog delivery simultaneously.","Only 5 devices per gateway are permitted to be prioritize backlog delivery simultaneously.")%>
                    <%}
          } %>
    </div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|Backlog Data","Backlog Data")%>
    </div>
    <div class="col sensorEditFormInput">
        <%
            int BacklogCount = 0;
            if (Model.LastDataMessage != null) { BacklogCount = ((WeightedActivity)Model.LastDataMessage.AppBase).DLCount; }
        %>

        <%:BacklogCount%> <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|backlog messages","backlog messages")%>

                <%if (ClearBacklogPending)
                  { %>
                    <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|(Clear Backlog Pending)","(Clear Backlog Pending)")%>
                <%}
                  else
                  {%>
        <a class="actionControlCommand btn btn-primary" data-command="201" ><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|Clear Backlog","Clear Backlog")%></a>
        <%} %>
    </div>
</div>


<script type="text/javascript">

   var studyMode = "<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|Entering Study Mode will erase all backlog data. Press OK to continue.")%>";
   var eraseBacklog = "<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|This will erase all backlog data. Press OK to continue.")%>";

    $('.actionControlCommand').on("click", function (e) {
        e.preventDefault();

        var sensorID = <%:Model.SensorID%>;
                var command = $(this).data("command");
                
            //Set Study Mode Confirmation
                if(command == 204 && !confirm(studyMode))
                    return;
            //Clear Data Confirmation
                if(command == 201 && !confirm(eraseBacklog))
                    return;
                
                $.post("/Sensor/Calibrate",{ id: sensorID, command: command },function (result) {
                    if(result.length < 100)
                    {
                        window.location.reload();
                    }
                });

        });
</script>

