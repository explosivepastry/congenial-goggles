<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>

<%="" %>

<%  long SensorID = Model.SensorID;
    int acc = 0;
    bool Relay1Active = false;

    string Relay1delayLabel = "Min";
    int Relay1delayMinutes = 0;
    string relay1DisplayValue = "";

    bool Relay2Active = false;
    string Relay2delayLabel = "Min";
    int Relay2delayMinutes = 0;
    string relay2DisplayValue = "";

    List<NotificationRecipient> notificationRecipients = ((List<NotificationRecipient>)ViewBag.notificationRecipients).Where(m => { return m.DeviceToNotifyID == Model.SensorID; }).ToList();
    if (notificationRecipients.Count > 0)
    {
        NotificationRecipient recipient = notificationRecipients.FirstOrDefault();


        if (Model.ApplicationID == 93)
        {
            CurrentZeroToTwentyAmp.ParseSerializedRecipientProperties(recipient.SerializedRecipientProperties, out acc);
            Relay1Active = true;
        }
        if (Model.ApplicationID == 94)
        {
            CurrentZeroToOneFiftyAmp.ParseSerializedRecipientProperties(recipient.SerializedRecipientProperties, out acc);

            Relay1Active = true;
        }
        if (Model.ApplicationID == 120)
        {
            CurrentZeroTo500Amp.ParseSerializedRecipientProperties(recipient.SerializedRecipientProperties, out acc);

            Relay1Active = true;
        }

        //Pulse Counters
        if (Model.ApplicationID == 73)
        {
            FilteredPulseCounter.ParseSerializedRecipientProperties(recipient.SerializedRecipientProperties, out acc);

            Relay1Active = true;

        }

        if (Model.ApplicationID == 90)
        {
            FilteredPulseCounter64.ParseSerializedRecipientProperties(recipient.SerializedRecipientProperties, out acc);

            Relay1Active = true;
        }

        if (Model.ApplicationID == 153)
        {
            foreach (NotificationRecipient recip in notificationRecipients)
            {

                int state1Temp = 0;
                int state2Temp = 0;

                TwoInputPulseCounter.ParseSerializedRecipientProperties(recip.SerializedRecipientProperties, out state1Temp, out state2Temp, out acc);
                if (state1Temp > 0)
                {
                    Relay1Active = true;
                    Relay1delayMinutes = recip.DelayMinutes;
                    if (Relay1delayMinutes > 1 && Relay1delayMinutes < 60)
                    {
                        Relay1delayLabel = Html.TranslateTag("minutes after condition met");
                        relay1DisplayValue = Relay1delayMinutes.ToString();
                    }

                    if (Relay1delayMinutes >= 60)
                    {

                        Relay1delayLabel = Html.TranslateTag("hours after condition met");
                        relay1DisplayValue = (Relay1delayMinutes / 60).ToString();
                    }
                }
                if (state2Temp > 0)
                {
                    Relay2Active = true;

                    Relay2delayMinutes = recip.DelayMinutes;

                    if (Relay2delayMinutes > 1 && Relay2delayMinutes < 60)
                    {
                        Relay2delayLabel = Html.TranslateTag("minutes after condition met");
                        relay2DisplayValue = Relay2delayMinutes.ToString();
                    }

                    if (Relay2delayMinutes >= 60)
                    {

                        Relay2delayLabel = Html.TranslateTag("hours after condition met");
                        relay2DisplayValue = (Relay1delayMinutes / 60).ToString();
                    }
                }
            }
        }
        else
        {
            if (Relay1Active)
            {
                Relay1delayMinutes = recipient.DelayMinutes;
                if (Relay1delayMinutes > 1 && Relay1delayMinutes < 60)
                {
                    Relay1delayLabel = Html.TranslateTag("minutes after condition met");
                    relay1DisplayValue = Relay1delayMinutes.ToString();
                }

                if (Relay1delayMinutes >= 60)
                {

                    Relay1delayLabel = Html.TranslateTag("hours after condition met");
                    relay1DisplayValue = (Relay1delayMinutes / 60).ToString();
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
                <div class="command-icon"><%=Html.GetThemedSVG("resetAcc") %></div>

                &nbsp;<%=Model.SensorName %>
            </div>
        </div>

        <input type="hidden" id="relay1State_<%:SensorID %>" name="relay1State_<%:SensorID %>" value="<%:Relay1Active %>" />

        <%--  1   RELAY  OFF--%>

        <%if (Model.ApplicationID == 93)
            {%>
        <div id="addRelay1_<%:SensorID %>" class="relay-tag alert-line nrd<%:SensorID %> Control1" onclick="toggleResetAccDevice(<%:SensorID %>, 'CurrentZeroToTwentyAmp', true,'1'); return false;" style="display: <%:Relay1Active ? "none" : "flex"%>">
            <div title="<%: Html.TranslateTag("Add","Add")%>" class=" drag-icon2 nrd<%:SensorID %> Control1 dfac">
                <%=Html.GetThemedSVG("add") %>
            </div>
            <div class="relay-off">
                <a href="Add" class="relay-off-name nrd<%:SensorID %> dfac">&nbsp;<%:Html.TranslateTag("Reset Accumulator") %>
                </a>
            </div>
        </div>
        <%}%>

        <%if (Model.ApplicationID == 94)
            {%>
        <div id="addRelay1_<%:SensorID %>" class="relay-tag alert-line nrd<%:SensorID %> Control1" onclick="toggleResetAccDevice(<%:SensorID %>, 'CurrentZeroToOneFiftyAmp', true,'1'); return false;" style="display: <%:Relay1Active ? "none" : "flex"%>">
            <div title="<%: Html.TranslateTag("Add","Add")%>" class=" drag-icon2 nrd<%:SensorID %> Control1 dfac">
                <%=Html.GetThemedSVG("add") %>
            </div>
            <div class="relay-off">
                <a href="Add" class="relay-off-name nrd<%:SensorID %> dfac">&nbsp;<%:Html.TranslateTag("Reset Accumulator") %>
                </a>
            </div>
        </div>
        <%}%>

        <%if (Model.ApplicationID == 120)
            {%>
        <div id="addRelay1_<%:SensorID %>" class="relay-tag alert-line nrd<%:SensorID %> Control1" onclick="toggleResetAccDevice(<%:SensorID %>, 'CurrentZeroTo500Amp', true,'1'); return false;" style="display: <%:Relay1Active ? "none" : "flex"%>">
            <div title="<%: Html.TranslateTag("Add","Add")%>" class=" drag-icon2 nrd<%:SensorID %> Control1 dfac">
                <%=Html.GetThemedSVG("add") %>
            </div>
            <div class="relay-off">
                <a href="Add" class="relay-off-name nrd<%:SensorID %> dfac">&nbsp;<%:Html.TranslateTag("Reset Accumulator") %>
                </a>
            </div>
        </div>
        <%}%>

        <%-- Pulse Counter Section--%>

        <%if (Model.ApplicationID == 73)
            {%>
        <div id="addRelay1_<%:SensorID %>" class="relay-tag alert-line nrd<%:SensorID %> Control1" onclick="toggleResetAccDevice(<%:SensorID %>, 'FilteredPulse', true,'1'); return false;" style="display: <%:Relay1Active ? "none" : "flex"%>">
            <div title="<%: Html.TranslateTag("Add","Add")%>" class=" drag-icon2 nrd<%:SensorID %> Control1 dfac">
                <%=Html.GetThemedSVG("add") %>
            </div>
            <div class="relay-off">
                <a href="Add" class="relay-off-name nrd<%:SensorID %> dfac">&nbsp;<%:Html.TranslateTag("Reset Accumulator") %>
                </a>
            </div>
        </div>
        <%}%>

        <%if (Model.ApplicationID == 90)
            {%>
        <div id="addRelay1_<%:SensorID %>" class="relay-tag alert-line nrd<%:SensorID %> Control1" onclick="toggleResetAccDevice(<%:SensorID %>, 'FilteredPulse64', true,'1'); return false;" style="display: <%:Relay1Active ? "none" : "flex"%>">
            <div title="<%: Html.TranslateTag("Add","Add")%>" class=" drag-icon2 nrd<%:SensorID %> Control1 dfac">
                <%=Html.GetThemedSVG("add") %>
            </div>
            <div class="relay-off">
                <a href="Add" class="relay-off-name nrd<%:SensorID %> dfac">&nbsp;<%:Html.TranslateTag("Reset Accumulator") %>
                </a>
            </div>
        </div>
        <%}%>

        <%if (Model.ApplicationID == 153)
            {%>
        <div id="addRelay1_<%:SensorID %>" class="relay-tag alert-line nrd<%:SensorID %> Control1" onclick="toggleResetAccDevice(<%:SensorID %>, 'TwoInputPulseRelay1', true,'1'); return false;" style="display: <%:Relay1Active ? "none" : "flex"%>">
            <div title="<%: Html.TranslateTag("Add","Add")%>" class=" drag-icon2 nrd<%:SensorID %> Control1 dfac">
                <%=Html.GetThemedSVG("add") %>
            </div>
            <div class="relay-off">
                <a href="Add" class="relay-off-name nrd<%:SensorID %> dfac">&nbsp;<%:Html.TranslateTag("Relay 1") %>
                </a>
            </div>
        </div>
        <%}%>

        <%------- RELAY 1 ON--%>
        <div class="edit-relay" id="editRelay1_<%:SensorID %>" style="display: <%= Relay1Active ? "block" : "none"%>; overflow: hidden;">
            <div class="relay-head">


                <%-- Current Meter Section--%>
                <%if (Model.ApplicationID == 93)
                    {%>
                <div title="<%: Html.TranslateTag("Remove","Remove")%>" onclick="toggleResetAccDevice(<%:SensorID %>,'CurrentZeroToTwentyAmp', false,'1'); return false;" class=" trash-icon  <%:SensorID %> Control1 ">
                    <%=Html.GetThemedSVG("delete") %>
                </div>
                <%} %>

                <%if (Model.ApplicationID == 94)
                    {%>
                <div title="<%: Html.TranslateTag("Remove","Remove")%>" onclick="toggleResetAccDevice(<%:SensorID %>,'CurrentZeroToOneFiftyAmp', false,'1'); return false;" class=" trash-icon  <%:SensorID %> Control1 ">
                    <%=Html.GetThemedSVG("delete") %>
                </div>
                <%} %>

                <%if (Model.ApplicationID == 120)
                    {%>
                <div title="<%: Html.TranslateTag("Remove","Remove")%>" onclick="toggleResetAccDevice(<%:SensorID %>, 'CurrentZeroTo500Amp', false,'1'); return false;" class=" trash-icon  <%:SensorID %> Control1 ">
                    <%=Html.GetThemedSVG("delete") %>
                </div>
                <%} %>

                <%-- End of Current Meter--%>


                <%-- Pulse Counter Section--%>

                <%if (Model.ApplicationID == 73)
                    {%>
                <div title="<%: Html.TranslateTag("Remove","Remove")%>" onclick="toggleResetAccDevice(<%:SensorID %>,'FilteredPulse', false,'1'); return false;" class=" trash-icon  <%:SensorID %> Control1 ">
                    <%=Html.GetThemedSVG("delete") %>
                </div>
                <%} %>

                <%if (Model.ApplicationID == 90)
                    {%>
                <div title="<%: Html.TranslateTag("Remove","Remove")%>" onclick="toggleResetAccDevice(<%:SensorID %>,'FilteredPulse64', false,'1'); return false;" class=" trash-icon  <%:SensorID %> Control1 ">
                    <%=Html.GetThemedSVG("delete") %>
                </div>
                <%} %>

                <%if (Model.ApplicationID == 153)
                    {%>
                <div title="<%: Html.TranslateTag("Remove", "Remove")%>" onclick="toggleResetAccDevice(<%:SensorID %>, 'TwoInputPulseRelay1', false,'1'); return false;" class=" trash-icon  <%:SensorID %> Control1 ">
                    <%=Html.GetThemedSVG("delete") %>
                </div>
                <%} %>

                <%-- End of Pulse Counter Section--%>


                <%if (Model.ApplicationID == 153)
                    {%>
                <div class="relay-name"><%:Html.TranslateTag("Relay 1") %></div>
                <%}
                    else
                    {%>
                <div class="relay-name"><%:Html.TranslateTag("Reset Accumulator") %></div>
                <%}%>

                <div id="caretClose_<%:SensorID %>_1" style="margin-left: 110px; display: block;" onclick="toggleResetAccDiv(<%:SensorID %>,'1',true);">
                    <div class="drag-icon2"><%=Html.GetThemedSVG("caret-down") %></div>
                </div>

                <div id="caretOpen_<%:SensorID %>_1" style="margin-left: 110px; display: none;" onclick="toggleResetAccDiv(<%:SensorID %>,'1',false);">
                    <div class="drag-icon2-close"><%=Html.GetThemedSVG("caret-down") %></div>
                </div>
            </div>

            <div class=" ccu-relay">
                <div class="nrd<%:SensorID %> Control1 unit-7">
                    <span class="ccu-sensor-active"><%: Html.TranslateTag("Send reset accumulator command","Send reset accumulator command")%> &nbsp;
                    </span>
                </div>

                <div id="nrd1_<%:SensorID %>" class="nrd<%:SensorID %> Control1" style="display: <%:Relay1Active  ? "flex" : "none"%>;">
                    <div class="switch">
                        <input value="<%:Relay1Active %>" <%=Relay1Active ? "checked=\"checked\"" : "" %> type="checkbox" id="relay1Toggle_<%:SensorID %>" name="relay1Toggle_<%:SensorID %>">
                        <div class="slider round" id="relay1Toggler_<%:SensorID %>" onclick="toggleResetAccRelayState('relay1Toggle_<%:SensorID %>')">
                            <span class="on"><%= Html.TranslateTag("ON","ON") %></span>
                            <span class="off"><%=  Html.TranslateTag("OFF","OFF")%></span>
                        </div>
                    </div>
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

                </div>
                <input type="hidden" id="relay1delayTime_<%=Model.SensorID %>" value="<%=Relay1Active ? Relay1delayMinutes : 0 %>" name="relay1delayMinutes_<%:Model.SensorID %>" />
            </div>

        </div>



        <%--  Relay 2 Delay Minutes--%>

        <%if (Model.ApplicationID == 153)
            {%>

        <%--   2 RELAY OFF--%>
        <div id="addRelay2_<%:SensorID %>" class="relay-tag alert-line nrd<%:SensorID %> Control2" onclick="toggleResetAccDevice(<%:SensorID %>, 'TwoInputPulseRelay2', true,'2'); return false;" style="display: <%:Relay2Active ? "none" : "flex"%>;">
            <div title="<%: Html.TranslateTag("Add","Add")%>" class=" drag-icon2 nrd<%:SensorID %> Control2">
                <%=Html.GetThemedSVG("add") %>
            </div>

            <div class="relay-off">
                <a href="Add" class="relay-off-name nrd<%:SensorID %> dfac">&nbsp;<%:Html.TranslateTag("Relay 2") %>
                </a>
            </div>
        </div>

        <%------- RELAY 2 ON--%>
        <div class="edit-relay" id="editRelay2_<%:SensorID %>" style="display: <%:Relay2Active ? "block" : "none"%>; overflow: hidden;">

            <div class="relay-head">
                <div title="<%: Html.TranslateTag("Remove","Remove") %>" onclick="toggleResetAccDevice(<%:SensorID %>, 'TwoInputPulseRelay2', false,'2'); return false;" class="trash-icon  <%:SensorID %> Control2 ">
                    <%=Html.GetThemedSVG("delete") %>
                </div>

                <div class="relay-name"><%:Html.TranslateTag("Relay 2") %></div>
                <div id="caretClose_<%:SensorID %>_2" style="margin-left: 110px; display: block;" onclick="toggleResetAccDiv(<%:SensorID %>,'2',true);">
                    <div class="drag-icon2"><%=Html.GetThemedSVG("caret-down") %></div>
                </div>

                <div id="caretOpen_<%:SensorID %>_2" style="margin-left: 110px; display: none;" onclick="toggleResetAccDiv(<%:SensorID %>,'2',false);">
                    <div class="drag-icon2-close"><%=Html.GetThemedSVG("caret-down") %></div>
                </div>
            </div>

            <div class=" ccu-relay">
                <div class="nrd<%:SensorID %> Control2 unit-7">
                    <span class="ccu-sensor-active"><%: Html.TranslateTag("Send reset accumulator command","Send reset accumulator command")%> &nbsp;
                    </span>
                </div>

                <div id="nrd2_<%:SensorID %>" class="nrd<%:SensorID %> Control2" style="display: <%:Relay2Active ? "flex" : "none"%>;">
                    <div class="switch">
                        <input value="<%:Relay2Active %>" <%=Relay2Active ? "checked=\"checked\"" : "" %> type="checkbox" id="relay2Toggle_<%:SensorID %>" name="relay2Toggle_<%:SensorID %>">
                        <div class="slider round" id="relay2Toggler_<%:SensorID %>" onclick="toggleResetAccRelayState('relay2Toggle_<%:SensorID %>')">
                            <span class="on"><%: Html.TranslateTag("ON","ON")%></span>
                            <span class="off"><%: Html.TranslateTag("OFF","OFF")%></span>
                        </div>
                    </div>
                </div>
            </div>

            <%-- Relay 2 Delay Minutes--%>

            <div class="alert-line nrd<%:Model.SensorID %> Notifier notifyDevice" style="cursor: pointer; display: <%:Relay2Active != null ? "flex" : "none"%>;" data-id="<%=Model.SensorID %>">
                <div class="ccu-send-btn" type="button" id="openNumberSpinnerModalFor_<%:Model.SensorID %>_2" onclick="showDelay(<%=Model.SensorID %>,2);" style="display: <%:Relay2Active != null ? "flex" : "none"%>;">
                    <%=Html.GetThemedSVG("send") %>
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

        <%}%>
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
