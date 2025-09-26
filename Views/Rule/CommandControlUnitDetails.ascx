<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>

<%="" %>

<% 
    List<NotificationRecipient> notificationRecipients = (List<NotificationRecipient>)ViewBag.notificationRecipients;

    if (Model.ApplicationID == 12)
    {
        long SensorID = Model.SensorID;
        bool Relay1Active = false;
        bool Relay1TimerActive = false;
        int State1 = 0;
        ushort Time1 = 0;

        bool Relay2Active = false;
        bool Relay2TimerActive = false;
        int State2 = 0;
        ushort Time2 = 0;
        string Relay1delayLabel = "Min";
        int Relay1delayMinutes = -1;
        string Relay2delayLabel = "Min";
        int Relay2delayMinutes = -1;
        string relay1DisplayValue = "";
        string relay2DisplayValue = "";
     
        if (notificationRecipients != null)
        {
            foreach (NotificationRecipient nr in notificationRecipients.Where(m => { return m.DeviceToNotifyID == SensorID; }))
            {
                int state1Temp = 0;
                int state2Temp = 0;
                ushort time1Temp = 0;
                ushort time2Temp = 0;

                Control_1.ParseSerializedRecipientProperties(nr.SerializedRecipientProperties, out state1Temp, out state2Temp, out time1Temp, out time2Temp);

                if (state1Temp > 0)
                {
                    State1 = state1Temp;
                    Time1 = time1Temp;

                    Relay1Active = true;
                    Relay1TimerActive = Time1 > 0;

                    Relay1delayMinutes = nr.DelayMinutes;
                    if (Relay1delayMinutes > 1 && Relay1delayMinutes < 60)
                    {
                        Relay1delayLabel = Html.TranslateTag("minutes after condition met");
                        relay1DisplayValue = Relay1delayMinutes.ToString();
                    }

                    if (Relay1delayMinutes == 60)
                    {
                        Relay1delayLabel = Html.TranslateTag("hour after condition met");
                        relay1DisplayValue = "1";
                    }

                    if (Relay1delayMinutes > 60)
                    {
                        Relay1delayLabel = Html.TranslateTag("hours after condition met");
                        relay1DisplayValue = (Relay1delayMinutes / 60).ToString();
                    }

                }
                if (state2Temp > 0)
                {
                    State2 = state2Temp;
                    Time2 = time2Temp;

                    Relay2Active = true;
                    Relay2TimerActive = Time2 > 0;

                    Relay2delayMinutes = nr.DelayMinutes;
                    if (Relay2delayMinutes > 1 && Relay2delayMinutes < 60)
                    {
                        Relay2delayLabel = Html.TranslateTag("minutes after condition met");
                        relay2DisplayValue = Relay1delayMinutes.ToString();
                    }

                    if (Relay2delayMinutes == 60)
                    {
                        Relay2delayLabel = Html.TranslateTag("hour after condition met");
                       relay2DisplayValue = "1";
                    }

                    if (Relay2delayMinutes > 60)
                    {
                        Relay2delayLabel = Html.TranslateTag("hours after condition met");
                        relay2DisplayValue = (Relay1delayMinutes / 60).ToString();
                    }
                }
            }
        }

%>

<div class="local-alert_container">
    <div class="local-alert_card" id="toggle-container">
        <%--Sensor name--%>
        <div class="control-unit-heading" id="head-btn">
            <!-- Active -->
            <div title="<%: Html.TranslateTag("Remove","Remove")%>" class="relay-unit-title active-relay nrd<%:SensorID %> Control1">
                <div style="display: flex;">
                    <%--<div class="icon-det"><%=Html.GetThemedSVG("delete") %></div>--%>
                </div>
                <div class="command-icon"><%=Html.GetThemedSVG("alert") %></div>

                &nbsp;<%=Model.SensorName %>
            </div>
        </div>

        <input type="hidden" id="relay1State_<%:SensorID %>" name="relay1State_<%:SensorID %>" value="<%:Relay1Active %>" />

        <%--  1   RELAY  OFF--%>
        <div id="addRelay1_<%:SensorID %>" class="relay-tag alert-line nrd<%:SensorID %> Control1" onclick="toggleControlUnitDevice(<%:SensorID %>, 'Control1', true,'1'); return false;" style="display: <%:Relay1Active ? "none" : "flex"%>">
            <div title="<%: Html.TranslateTag("Add","Add")%>" class=" drag-icon2 nrd<%:SensorID %> Control1 dfac">
                <%=Html.GetThemedSVG("add") %>
            </div>
            <div class="relay-off">
                <a href="Add" class="relay-off-name nrd<%:SensorID %> dfac">&nbsp;<%=Control_1.Relay1Name(SensorID)%>
                </a>
            </div>
        </div>

        <%------- RELAY 1 ON--%>
        <div class="edit-relay" id="editRelay1_<%:SensorID %>" style="display: <%= Relay1Active ? "block" : "none"%>; overflow: hidden;">
            <div class="relay-head">
                <div title="<%: Html.TranslateTag("Remove","Remove")%>" onclick="toggleControlUnitDevice(<%:SensorID %>, 'Control1', false,'1'); return false;" class=" trash-icon  <%:SensorID %> Control1 ">
                    <%=Html.GetThemedSVG("delete") %>
                </div>

                <div class="relay-name"><%=Control_1.Relay1Name(SensorID)%></div>
                <div id="caretClose_<%:SensorID %>_1" style="margin-left: 110px; display: block;" onclick="toggleRelayDiv(<%:SensorID %>,'1',true);">
                    <div class="drag-icon2"><%=Html.GetThemedSVG("caret-down") %></div>
                </div>

                <div id="caretOpen_<%:SensorID %>_1" style="margin-left: 110px; display: none;" onclick="toggleRelayDiv(<%:SensorID %>,'1',false);">
                    <div class="drag-icon2-close"><%=Html.GetThemedSVG("caret-down") %></div>
                </div>
            </div>

            <div class=" ccu-relay">
                <div class="nrd<%:SensorID %> Control1 unit-7">
                    <span class="ccu-sensor-active"><%: Html.TranslateTag("Send command to turn","Send command to turn")%> &nbsp;
                    </span>
                </div>

                <div class="nrd<%:SensorID %> Control1 " style="margin-right: 10px;">
                    <div class="switch">
                        <input value="<%:Relay1Active %>" <%=Relay1Active && State1 == 2 ? "checked=\"checked\"" : "" %> type="checkbox" id="relay1Toggle_<%:SensorID %>" name="relay1Toggle_<%:SensorID %>">
                        <div class="slider round" id="relay1Toggler_<%:SensorID %>" onclick="toggleControlUnitRelayState('relay1Toggle_<%:SensorID %>')">
                            <span class="on"><%= Html.TranslateTag("ON","ON") %></span>
                            <span class="off"><%=  Html.TranslateTag("OFF","OFF")%></span>
                        </div>
                    </div>
                </div>
            </div>

            <%--    Command -timmer--%>
            <div class="ccu-relay-2">
                <div class="ccu-relay">
                    <div class="ccu-sensor-active"><%: Html.TranslateTag("Set command timer","Set command timer")%></div>
                    <div class=" switch-it ">
                        <div class="switch">
                            <input type="checkbox" id="Indefinite1_<%:SensorID %>" <%=Relay1TimerActive ? "checked=\"checked\"" : "" %>>
                            <div class="slider round" onclick="toggleTimer(<%:SensorID %>,1); return false;">
                                <span class="on"><%= Html.TranslateTag("ON","ON") %></span>
                                <span class="off"><%= Html.TranslateTag("OFF","OFF") %></span>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="timerHolder" style="display: <%:Relay1TimerActive ? "flex" : "none"%>;">
                    <table>
                        <tr>
                            <td style="display: flex; gap: 5px;">
                                <input type="text" class="user-dets form-control shortTimer minutes" id="relay1Minute_<%:SensorID%>" name="relay1Minute_<%:SensorID%>" value="<%:Time1/60 %>" style="width: 45px" />
                                <input type="text" class="user-dets form-control shortTimer seconds" id="relay1Second_<%:SensorID%>" name="relay1Second_<%:SensorID%>" value="<%:Time1%60 %>" style="width: 45px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="font-size: .9rem; padding: 0 8px; text-align: start;" class="timeSec">
                                    <%: Html.TranslateTag("min","min")%> &nbsp&nbsp
                                                <%: Html.TranslateTag("sec","sec")%>
                                </div>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="col-4 timerHolder" style="display: <%:Relay1TimerActive ? "none" : "flex"%>;">
                </div>
            </div>

            <%-- Delay Minutes--%>
            <div id="openNumberSpinnerModalFor_<%:Model.SensorID %>_1" class=" alert-line nrd<%:Model.SensorID %> Notifier notifyDevice" style="cursor: pointer; display: <%:Relay1Active != null ? "flex" : "none"%>;" data-id="<%=Model.SensorID %>">

                <div class="ccu-send-btn" type="button" onclick="showDelay(<%:Model.SensorID%>,1);" style="display: <%:Relay1Active != null ? "flex" : "none"%>;">

                    <%=Html.GetThemedSVG("send") %>

                    <div class="">
                        <span><%= Html.TranslateTag("Send command") %></span> 
                        <span id="relay1delayValue_<%=Model.SensorID %>"><%=(Relay1delayMinutes <= 0 ? Html.TranslateTag("when condition met") :   relay1DisplayValue + " " + Relay1delayLabel) %></span>
                    </div>

<%--                    <div class="">
                        <span><%= Html.TranslateTag("Send command") %></span>
                        <span id="relay1delayValue_<%=Model.SensorID %>"><%=(Relay1delayMinutes <= 0 ? Html.TranslateTag("when condition met") :   Relay1delayMinutes + " " + Relay1delayLabel) %> </span>

                    </div>--%>
                </div>
                <input type="hidden" id="relay1delayTime_<%=Model.SensorID %>" value="<%=Relay1Active ? Relay1delayMinutes : -1 %>" name="relay1delayMinutes_<%:Model.SensorID %>" />
            </div>

        </div>

        <input type="hidden" id="relay2State_<%:SensorID %>" name="relay2State_<%:SensorID %>" value="<%:Relay2Active %>" />

        <%--   2 RELAY OFF--%>
        <div id="addRelay2_<%:SensorID %>" class="relay-tag alert-line nrd<%:SensorID %> Control2" onclick="toggleControlUnitDevice(<%:SensorID %>, 'Control2', true,'2'); return false;" style="display: <%:Relay2Active ? "none" : "flex"%>;">
            <div title="<%: Html.TranslateTag("Add","Add")%>" class=" drag-icon2 nrd<%:SensorID %> Control2">
                <%=Html.GetThemedSVG("add") %>
            </div>

            <div class="relay-off">
                <a href="Add" class="relay-off-name nrd<%:SensorID %> dfac">&nbsp;<%=Control_1.Relay2Name(SensorID)%>
                </a>
            </div>
        </div>

        <%------- RELAY 2 ON--%>
        <div class="edit-relay" id="editRelay2_<%:SensorID %>" style="display: <%:Relay2Active ? "block" : "none"%>; overflow: hidden;">
            <div class="relay-head">
                <div title="<%: Html.TranslateTag("Remove","Remove") %>" onclick="toggleControlUnitDevice(<%:SensorID %>, 'Control2', false,'2'); return false;" class="trash-icon  <%:SensorID %> Control2 ">
                    <%=Html.GetThemedSVG("delete") %>
                </div>

                <div class="relay-name"><%=Control_1.Relay2Name(SensorID)%></div>
                <div id="caretClose_<%:SensorID %>_2" style="margin-left: 110px; display: block;" onclick="toggleRelayDiv(<%:SensorID %>,'2',true);">
                    <div class="drag-icon2"><%=Html.GetThemedSVG("caret-down") %></div>
                </div>

                <div id="caretOpen_<%:SensorID %>_2" style="margin-left: 110px; display: none;" onclick="toggleRelayDiv(<%:SensorID %>,'2',false);">
                    <div class="drag-icon2-close"><%=Html.GetThemedSVG("caret-down") %></div>
                </div>
            </div>

            <div class=" ccu-relay">
                <div class="nrd<%:SensorID %> Control2 unit-7">
                    <span class="ccu-sensor-active"><%: Html.TranslateTag("Send command to turn","Send command to turn")%>&nbsp;
                    </span>
                </div>

                <div class="nrd<%:SensorID %> Control2 " style="margin-right: 10px;">
                    <div class="switch">
                        <input value="<%:Relay2Active %>" <%=Relay2Active && State2 == 2 ? "checked=\"checked\"" : "" %> type="checkbox" id="relay2Toggle_<%:SensorID %>" name="relay2Toggle_<%:SensorID %>">
                        <div class="slider round" id="relay2Toggler_<%:SensorID %>" onclick="toggleControlUnitRelayState('relay2Toggle_<%:SensorID %>')">
                            <span class="on"><%: Html.TranslateTag("ON","ON")%></span>
                            <span class="off"><%: Html.TranslateTag("OFF","OFF")%></span>
                        </div>
                    </div>
                </div>
            </div>

            <%-- command timer relay 2--%>

            <div class="ccu-relay-2">
                <div class="ccu-relay">
                    <div class="ccu-sensor-active"><%: Html.TranslateTag("Set command timer","Set command timer")%></div>
                    <div class="switch-it">
                        <div class="switch">
                            <input type="checkbox" id="Indefinite2_<%:SensorID %>" <%=Relay2TimerActive ? "checked=\"checked\"" : "" %>>
                            <div class="slider round" onclick="toggleTimer(<%:SensorID %>,2); return false;">
                                <span class="on"><%= Html.TranslateTag("ON","ON") %></span>
                                <span class="off"><%= Html.TranslateTag("OFF","OFF") %></span>
                            </div>
                        </div>
                    </div>
                </div>

                <div class=" timerHolder" style="display: <%:Relay2TimerActive?"flex":"none"%>;">
                    <table>
                        <tr>
                            <td style="display: flex; gap: 5px;">
                                <input type="text" class="user-dets form-control shortTimer minutes" id="relay2Minute_<%:SensorID%>" name="relay2Minute_<%:SensorID%>" value="<%:Time2/60 %>" style="width: 45px" />
                                <input type="text" class="user-dets form-control shortTimer seconds" id="relay2Second_<%:SensorID%>" name="relay2Second_<%:SensorID%>" value="<%:Time2%60 %>" style="width: 45px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="font-size: .9rem; padding: 0 8px; text-align: start;" class="timeSec">
                                    <%: Html.TranslateTag("min","min")%> &nbsp&nbsp
                                                <%: Html.TranslateTag("sec","sec")%>
                                </div>
                            </td>
                        </tr>
                    </table>

                </div>

                <div class="col-4 timerHolder" style="display: <%:Relay2TimerActive?"none":"flex"%>;">
                </div>
            </div>

            <%-- Delay Minutes--%>

            <div class="alert-line nrd<%:Model.SensorID %> Notifier notifyDevice" style="cursor: pointer; display: <%:Relay2Active != null ? "flex" : "none"%>;" data-id="<%=Model.SensorID %>">
                <div class="ccu-send-btn" type="button" id="openNumberSpinnerModalFor_<%:Model.SensorID %>_2" onclick="showDelay(<%=Model.SensorID %>,2);" style="display: <%:Relay2Active != null ? "flex" : "none"%>;">                    <%=Html.GetThemedSVG("send") %>
                    <div class="">
                        <span><%= Html.TranslateTag("Send command") %></span>
                        <span id="relay2delayValue_<%=Model.SensorID %>">
                            <%=(Relay2delayMinutes <= 0 ? Html.TranslateTag("when condition met") :   relay2DisplayValue  + " " + Relay2delayLabel) %>
                        </span>
                    </div>
                </div>
                <input type="hidden" id="relay2delayTime_<%=Model.SensorID %>" value="<%=Relay2Active ? Relay2delayMinutes : -1 %>" name="relay2delayMinutes_<%:Model.SensorID %>" />
            </div>
        </div>
    </div>
</div>




<script>
    const valuesArrayInStrings_<%:Model.SensorID %> = ["<%: Html.TranslateTag("No Delay")%>", "<%: Html.TranslateTag("2 Minutes")%>", "<%: Html.TranslateTag("5 Minutes")%>", "<%: Html.TranslateTag("10 Minutes")%>", "<%: Html.TranslateTag("15 Minutes")%>", "<%: Html.TranslateTag("30 Minutes")%>", "<%: Html.TranslateTag("45 Minutes")%>", "<%: Html.TranslateTag("1 Hour")%>", "<%: Html.TranslateTag("2 Hours")%>", "<%: Html.TranslateTag("4 Hours")%>", "<%: Html.TranslateTag("6 Hours")%>", "<%: Html.TranslateTag("8 Hours")%>", "<%: Html.TranslateTag("10 Hours")%>", "<%: Html.TranslateTag("12 Hours")%>", "<%: Html.TranslateTag("16 Hours")%>", "<%: Html.TranslateTag("20 Hours")%>", "<%: Html.TranslateTag("24 Hours")%>"]
    const valuesArrayInNumbers_<%:Model.SensorID %> = [0, 2, 5, 10, 15, 30, 45, 60, 120, 240, 360, 480, 600, 720, 960, 1200, 1440];

    createSpinnerModal("openNumberSpinnerModalFor_<%:Model.SensorID %>_1", "Time To Delay", "relay1delayTime_<%=Model.SensorID %>", valuesArrayInStrings_<%:Model.SensorID %>, 0);
    createSpinnerModal("openNumberSpinnerModalFor_<%:Model.SensorID %>_2", "Time To Delay", "relay2delayTime_<%=Model.SensorID %>", valuesArrayInStrings_<%:Model.SensorID %>, 0);

    const inputElementRelay1<%=Model.SensorID %> = document.getElementById('relay1delayTime_<%=Model.SensorID %>');
    const spanElementRelay1<%=Model.SensorID %> = document.getElementById('relay1delayValue_<%=Model.SensorID %>');

    const observerRelay1<%=Model.SensorID %> = new MutationObserver(function (mutationsList, observer) {

        for (let mutation of mutationsList) {

            if (mutation.type === 'attributes' && mutation.attributeName === 'value') {
                observerRelay1<%=Model.SensorID %>.disconnect();

                if (inputElementRelay1<%=Model.SensorID %>.value !== "No Delay") {
                    spanElementRelay1<%=Model.SensorID %>.textContent = `${inputElementRelay1<%=Model.SensorID %>.value} <%: Html.TranslateTag(" after condition met ")%>`;
                } else {
                    spanElementRelay1<%=Model.SensorID %>.textContent = `<%: Html.TranslateTag(" after condition met ")%>`;
                }
                inputElementRelay1<%=Model.SensorID %>.value = StringToNumber(inputElementRelay1<%=Model.SensorID %>.value)
                observerRelay1<%=Model.SensorID %>.observe(inputElementRelay1<%=Model.SensorID %>, { attributes: true });
        }
    }
});
    observerRelay1<%=Model.SensorID %>.observe(inputElementRelay1<%=Model.SensorID %>, { attributes: true });

    const inputElementRelay2<%=Model.SensorID %> = document.getElementById('relay2delayTime_<%=Model.SensorID %>');
    const spanElementRelay2<%=Model.SensorID %> = document.getElementById('relay2delayValue_<%=Model.SensorID %>');

    const observerRelay2<%=Model.SensorID %> = new MutationObserver(function (mutationsList, observer) {

        for (let mutation of mutationsList) {
            if (mutation.type === 'attributes' && mutation.attributeName === 'value') {
                observerRelay2<%=Model.SensorID %>.disconnect();

                if (inputElementRelay2<%=Model.SensorID %>.value !== "No Delay") {
                    spanElementRelay2<%=Model.SensorID %>.textContent = `${inputElementRelay2<%=Model.SensorID %>.value} <%: Html.TranslateTag(" after condition met ")%>`;
                } else {
                    spanElementRelay2<%=Model.SensorID %>.textContent = `<%: Html.TranslateTag(" after condition met ")%>`;
                }

                inputElementRelay2<%=Model.SensorID %>.value = StringToNumber(inputElementRelay2<%=Model.SensorID %>.value)

                observerRelay2<%=Model.SensorID %>.observe(inputElementRelay2<%=Model.SensorID %>, { attributes: true });
            }
        }
    });

    observerRelay2<%=Model.SensorID %>.observe(inputElementRelay2<%=Model.SensorID %>, { attributes: true });

    function StringToNumber(string) {
        const matchingIndex = valuesArrayInStrings_<%:Model.SensorID %>.indexOf(string);
        return valuesArrayInNumbers_<%:Model.SensorID %>[matchingIndex];
    }
</script>
<%}%>