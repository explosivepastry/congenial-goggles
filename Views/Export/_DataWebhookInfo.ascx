<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.ExternalDataSubscription>" %>

<div class="container-fluid" >
    <div class="col-12">
        <div class="rule-card_container w-100">
            <div class="card_container__top accordion-item webhook-card">
                <div class="card_container__top__title d-flex justify-content-between accordion-header"  style="border-bottom:none;">
                   <div  style="margin-left:10px;"><%:Html.TranslateTag("Export/DataWebhook|Data Webhook Information","Data Webhook Information")%></div>
                  <div>  <button class="accordion-button <%:Model.ExternalDataSubscriptionID > 0 ? "collapsed" : "" %>" type="button" data-bs-toggle="collapse" data-bs-target="#info-toggle" aria-expanded="false"style="background-color:transparent!important;border-bottom: none;" /></div>
                </div>

                <div class="clearfix"></div>
            </div>

            <div id="info-toggle" class="col-12 accordion-collapse collapse <%:Model.ExternalDataSubscriptionID < 0 ? "show" : "" %>">
            <div class="accordion-body" style="max-height: 55vh;" >
                <div class="x_content">
                    <div class="col-xs-12">
                        <h2><%:Html.TranslateTag("Export/DataWebhook|Overview","Overview")%></h2>
                        <span style="font-size: 1.1em; align-content: center;"><%:Html.TranslateTag("Export/DataWebhook|A Data Push sends data to your end point when data is received at the server. You can configure the destination and query parameters used to route the request. Data is compiled as a JSON body and sent via HTTP POST. There are Four (4) endpoints available now. Webhook, Watson, Amazon AWS, and Azure IoT Hub. Only one (1) data push allowed per account. To switch data push types, first stop your existing data push, only then will the Create button appear.","A Data Push sends data to your end point when data is received at the server. You can configure the destination and query parameters used to route the request. Data is compiled as a JSON body and sent via HTTP POST. Four (4) endpoints available now. Webhook, Watson, Amazon AWS, and Azure IoT Hub. Only one (1) data push allowed per account. To switch data push types, first stop your existing data push, only then will the Create button appear.")%></span>
                        <hr />
                    </div>

                    <div class="col-xs-12">
                        <h2><%:Html.TranslateTag("Export/DataWebhook|Attempts & Retries","Attempts & Retries")%></h2>
                        <span style="font-size: 1.1em; align-content: center;"><%: Html.TranslateTag("Export/DataWebhook|Each individual message is attempted up to 4 times unless the total consecutive failure count for all attempts hits 20.  After 20 consecutive failures each message is only attempted 1 time up to 100 total consecutive failures.  After 100 consecutive failures the webhook is suspended until it is manualy reset. First attempt queued immediately (typically delivered within a few seconds) Second attempt queued after 2 minutes Third attempt queued after 15 minutes Fourth attempt queued after 60 minutes Subsequent attempts only sent after manually being manually re-queued. There is a second limit of 10 total attempts that can be sent. After 10 failed attempts of a particular message (4 auto attempts + 6 manual resend commands) you will need to contact support to release that message.","Each individual message is attempted up to 4 times unless the total consecutive failure count for all attempts hits 20.  After 20 consecutive failures each message is only attempted 1 time up to 100 total consecutive failures.  After 100 consecutive failures the webhook is suspended until it is manualy reset. First attempt queued immediately (typically delivered within a few seconds) Second attempt queued after 2 minutes Third attempt queued after 15 minutes Fourth attempt queued after 60 minutes Subsequent attempts only sent after manually being manually re-queued. There is a second limit of 10 total attempts that can be sent. After 10 failed attempts of a particular message (4 auto attempts + 6 manual resend commands) you will need to contact support to release that message.")%></span>
                        <hr />
                    </div>

                <div class="col-xs-12" style="width:100%;">
                <h2><%: Html.TranslateTag("Export/DataWebhook|General Output Format","General Output Format")%></h2>
              
               <pre style="border: solid 1px black; background-color: #DDEEFF; padding: 5px; overflow: auto;">
                        {Gateway Message},
                        SensorMessages:[{Sensor 1 Message}{Sensor 2 Message}{Sensor n Message}],
                        LocationMessages:[{Location 1 Message}{Location 2 Message}{Location n Message}]
                    </pre>
                </div>

                <div style="clear: both;"></div>
            </div>

            <div class="col-12" style="align-content: center;">
                <div class="col-12 col-lg-6">
                    <h2><%: Html.TranslateTag("Export/DataWebhook|Gateway Contents","Gateway Contents")%></h2>
                
                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|gatewayID","gatewayID")%>
                        </div>
                    
                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|A unique numeric identifier for the gateway.","A unique numeric identifier for the gatewa")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|gatewayName","gatewayName")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                          <%: Html.TranslateTag("Export/DataWebhook|User designated gateway identifier.","User designated gateway identifier.")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|networkID","networkID")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                           <%: Html.TranslateTag("Export/DataWebhook|A unique numeric identifier for the network.","A unique numeric identifier for the network.")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|messageType","messageType")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                           <%: Html.TranslateTag("Export/DataWebhook|Numerical value assigned to message.","Numerical value assigned to message.")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|power","power")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                           <%: Html.TranslateTag("Export/DataWebhook|A numeric value indicating type of power.","A numeric value indicating type of power.")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                           <%: Html.TranslateTag("Export/DataWebhook|batteryLevel","batteryLevel")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                           <%: Html.TranslateTag("Export/DataWebhook|A numeric value indicating battery power level.","A numeric value indicating battery power level.")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|date","date")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|Time stamp of data message.","Time stamp of data message.")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|count","count")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|The number of sensors.","The number of sensors")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|signalStrength","signalStrength")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|Strength of radio signal (0-100).","Strength of radio signal (0-100).")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|pendingChange","pendingChange")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|Has pending configuration transaction to take place.","Has pending configuration transaction to take place.")%>
                        </div>
                    </div>
                </div>

                <div class="col-12 col-lg-6">
                    <h2><%: Html.TranslateTag("Export/DataWebhook|Sensor Contents","Sensor Contents")%></h2>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|sensorID","sensorID")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|A unique numeric identifier for the sensor.","A unique numeric identifier for the sensor")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|sensorName","sensorName")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|User designated sensor identifier.","User designated sensor identifier.")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                           <%: Html.TranslateTag("Export/DataWebhook|applicationID","applicationID")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|Coded Sensor type (2= Temperature, 4= Water, etc.).","Coded Sensor type (2= Temperature, 4= Water, etc.).")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                           <%: Html.TranslateTag("Export/DataWebhook|networkID","networkID")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                           <%: Html.TranslateTag("Export/DataWebhook|A unique numeric identifier for the network.","A unique numeric identifier for the network.")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|dataMessageGUID","dataMessageGUID")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|A unique identifier for the message.","A unique identifier for the message.")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                          <%: Html.TranslateTag("Export/DataWebhook|state","state")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|Encoded state data (in general 0= Normal, 2= Aware State).","Encoded state data (in general 0= Normal, 2= Aware State)")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|messageDate","messageDate")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|Date message was delivered.","Date message was delivered.")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|rawData","rawData")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                          <%: Html.TranslateTag("Export/DataWebhook|Raw sensor data.","Raw sensor data.")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|dataType","dataType")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|Data discription (TemperatureData).","Data discription (TemperatureData).")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|dataValue","dataValue")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                           <%: Html.TranslateTag("Export/DataWebhook|Parsed sensor data.","Parsed sensor data.")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|plotValues","plotValues")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|Parsed sensor data plot values","Parsed sensor data plot values")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|plotLabels","plotLabe")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                          <%: Html.TranslateTag("Export/DataWebhook|Sensor data plot label. (Fahrenheit,Celsius etc.)","Sensor data plot label. (Fahrenheit,Celsius etc.)")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                           <%: Html.TranslateTag("Export/DataWebhook|batteryLevel","batteryLevel")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|Approximate percentage remaining on battery.","Approximate percentage remaining on battery")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|signalStrength","signalStrength")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|Strength of radio signal (0-100).","Strength of radio signal (0-100).")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|pendingChange","pendingChange")%>
                        </div>
                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|Has pending configuration transaction to take place.","Has pending configuration transaction to take place.")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|voltage","voltage")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|A numeric value indicating voltage level.","A numeric value indicating voltage level")%>
                        </div>
                    </div>
                </div>

                <div class="col-12 col-lg-6">
                    <h2><%: Html.TranslateTag("Export/DataWebhook|Location Contents","Location Contents")%></h2>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|deviceID","deviceID")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|A unique numeric identifier for the device.","A unique numeric identifier for the device.")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|locationMessageGUID","locationMessageGUI")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|A unique identifier for the location message.","A unique identifier for the location message.")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|locationDate","locationDate")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|Date location was recorded.","Date location was recorded")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|state","state")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|Encoded state data.","Encoded state data.")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|latitude","latitude")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|Latitude at time of recording.","Latitude at time of recording.")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                           <%: Html.TranslateTag("Export/DataWebhook|longitude","longitude")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|Longitude at time of recording.","|Longitude at time of recording.")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|altitude","altitude")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|Altitude at time of recording.","Altitude at time of recording.")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|speed","speed")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|Speed m/s traveling at time of recording.","Speed m/s traveling at time of recording.")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|course","course")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|Compass course traveling at time of recording.","Compass course traveling at time of recording")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                           <%: Html.TranslateTag("Export/DataWebhook|fixTime","fixTime")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|Number of seconds to aquire coordinates.","Number of seconds to aquire coordinates")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|sateliteCount","sateliteCount")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                           <%: Html.TranslateTag("Export/DataWebhook|Number of satelites used to generate reading.","Number of satelites used to generate reading.")%>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="bold col-sm-3 col-12">
                            <%: Html.TranslateTag("Export/DataWebhook|uncertainty","uncertainty")%>
                        </div>

                        <div class="col-sm-9 col-12 lgBox">
                            <%: Html.TranslateTag("Export/DataWebhook|Expected error in Meters.","Expected error in Meters.")%>
                        </div>
                    </div>
                </div>

<div class="col-12 col-lg-6">
<h2><%: Html.TranslateTag("Export/DataWebhook|Data Example JSON","Data Example JSON")%></h2>

<pre style="border: solid 1px black; background-color: #DDEEFF; padding: 5px; overflow: auto;">
    {
           "gatewayMessage":{
           "gatewayID":"10000" ,
           "gatewayName":"ExampleGateway" ,
           "accountID":"xxxx" ,
           "networkID":"xxxx" ,
           "messageType":"0" ,
           "power":"0",
           "batteryLevel": "101" ,
           "date": 2022-4-28 21:47:01",
           "count":"3",
           "signalStrength": "29",
           "pendingChange": "False" 
       },
       "sensorMessages":[
          {
              "sensorID":"10001" ,
              "sensorName":"Temp1" ,
              "applicationID":"2" ,
              "networkID":"xxxx" ,
              "dataMessageGUID":"78642056-CBD8-43B0-9A4B-247E58D3B6CB",
              "state": "0" ,
              "messageDate": 2022-4-28 21:48:01",
              "rawData":"23.7",
              "dataType": "TemperatureData",
              "dataValue": "23.7",
              "plotValues": "74.66",
              "plotLabels": "Fahrenheit",                           
              "batteryLevel": "100",
              "signalStrength": "100",
              "pendingChange": "False",
              "voltage": "3.24" 
          }
        ],
        "locationMessages":[
          {
              "deviceID":"10000" ,
              "locationMessageGUID":"5badfa3d-f110-4058-81cd-1ade90983adb",
              "locationDate":"2022-4-28 21:47:11",
              "state":"0",
              "latitude":"40.6977500915527",
              "longitude": "-111.894149780273",
              "altitude": "1302",
              "speed":"0",
              "course": "0",
              "fixTime": "24",
              "sateliteCount": "6",
              "uncertainty": "1"
          }
        ]
     }
     </pre>
</div>

        <div style="clear: both;"></div>
        </div>

        <div style="clear: both;"></div>
        </div>

         <div class="form-group row">
        <div class="bold col-md-2 col-sm-2 col-12">
        </div>

         <div class="col-md-6 col-sm-6 col-12 lgBox">
           <div class="editor-error">
           </div>
        </div>

            <div style="clear: both;"></div>
        </div>
    </div>
</div>
</div>
</div>