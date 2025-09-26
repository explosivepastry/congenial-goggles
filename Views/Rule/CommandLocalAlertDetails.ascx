<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>

<% 
    bool LED = false;
    bool Buzzer = false;
    bool AutoScroll = false;
    bool BackLight = false;
    string DeviceName = Model.SensorName.ToStringSafe();
    List<NotificationRecipient> notificationRecipients = (List<NotificationRecipient>)ViewBag.notificationRecipients;
    NotificationRecipient notifierRecipient = null;
    if (notificationRecipients != null)
    {
        notifierRecipient = notificationRecipients.FirstOrDefault(m => { return m.DeviceToNotifyID == Model.SensorID; });
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
    }
    bool isInProgress = notifierRecipient != null;
    string delayLabel = "Min";
    int delayMinutes = -1; ;

    if (isInProgress)
    {
        delayMinutes = notifierRecipient.DelayMinutes;
        if (delayMinutes > 1 && delayMinutes < 60)
        {
            delayLabel = Html.TranslateTag("minutes after condition met");
        }

        if (delayMinutes == 60)
        {
            delayLabel = Html.TranslateTag("hour after condition met");
            delayMinutes = 1;
        }

        if (delayMinutes > 60)
        {
            delayLabel = Html.TranslateTag("hours after condition met");
            delayMinutes = (delayMinutes / 60);
        }

    }
%>

<div class="local-alert_container">
    <div class="local-alert_card" id="card_<%=Model.SensorID %>">

        <%--Sensor name--%>
        <div class="local-alert-heading" style="display: <%:notifierRecipient != null ? "none" : "flex"%>;">
            <input type="hidden" name="Notifier_<%=Model.SensorID %>" id="Notifier_<%=Model.SensorID %>" value="<%=(notifierRecipient != null).ToString().ToLower() %>" />
            <%--    Active--%>
            <a href="add" onclick="toggleLocalAlertDevice(<%:Model.SensorID %>, true); return false;" class="alert-head-active nrd<%:Model.SensorID %> Notifier">
                <div class="addme"><%=Html.GetThemedSVG("add") %></div>
            </a>
            <div class="command-icon"><%=Html.GetThemedSVG("alert") %></div>
            <span style="width: clamp(36rem, 30vw, 100%);">&nbsp;<%=Model.SensorName %></span>

        </div>

        <div class="local-alert-heading" style="display: <%:notifierRecipient != null ? "flex" : "none"%>;">
            <input type="hidden" name="Notifier_<%=Model.SensorID %>" id="Notifier_<%=Model.SensorID %>" value="<%=(notifierRecipient != null).ToString().ToLower() %>" />
            <%--    Active--%>
            <a href="Remove" onclick="toggleLocalAlertDevice(<%:Model.SensorID %>, false); return false;" class="alert-head-active nrd<%:Model.SensorID %> Notifier">
                <div class="deletesvg"><%=Html.GetThemedSVG("delete") %></div>
            </a>
 
            <div class="command-icon"><%=Html.GetThemedSVG("alert") %></div>
            <span style="width: clamp(36rem, 30vw, 100%);">&nbsp;<%=Model.SensorName %></span>

            <div id="caretClose_<%:Model.SensorID %>" style="display: block;" onclick="toggleLocalAlertDiv(<%:Model.SensorID %>,true);">
                <div class="white-carrot"><%=Html.GetThemedSVG("caret-down") %></div>
            </div>
            <div id="caretOpen_<%:Model.SensorID %>" style="display: none;" onclick="toggleLocalAlertDiv(<%:Model.SensorID %>,false);">
                <div class="white-carrot carrot-close"><%=Html.GetThemedSVG("caret-down") %></div>
            </div>

        </div>

        <%-- LED light--%>
        <div class=" alert-line nrd<%:Model.SensorID %> Notifier" style="display: <%:notifierRecipient != null ? "flex" : "none"%>;">
            <div class="alert-icon">
                <%=Html.GetThemedSVG("led-light") %>
            </div>
            <div class="local-title"><span><%= Html.TranslateTag("LED Light") %></span></div>
            <div class="switch">
                <input value="<%= LED %>" <%=LED ? "checked=\"checked\"" : "" %> type="checkbox" id="led_<%:Model.SensorID %>" name="led_<%:Model.SensorID %>">
                <div class="slider round" onclick="setLocalAlertVal('led_','<%:Model.SensorID %>');">
                    <span class="on"><%= Html.TranslateTag("ON") %></span>
                    <span class="off"><%= Html.TranslateTag("OFF") %></span>
                </div>
            </div>
        </div>


        <%--Buzzer--%>
        <div class="alert-line nrd<%:Model.SensorID %> Notifier" style="display: <%:notifierRecipient != null ? "flex" : "none"%>;">
            <div class="alert-icon"><%=Html.GetThemedSVG("buz") %></div>
            <div class="local-title"><span><%= Html.TranslateTag("Buzzer") %></span></div>
            <div class="switch">
                <input value="<%= Buzzer %>" <%=Buzzer ? "checked=\"checked\"" : "" %> type="checkbox" id="buzzer_<%:Model.SensorID %>" name="buzzer_<%:Model.SensorID %>">
                <div class="slider round" onclick="setLocalAlertVal('buzzer_','<%:Model.SensorID %>');">
                    <span class="on"><%= Html.TranslateTag("ON") %></span>
                    <span class="off"><%= Html.TranslateTag("OFF") %></span>
                </div>
            </div>
        </div>


        <%----backlight--%>
        <div class="alert-line nrd<%:Model.SensorID %> Notifier" style="display: <%:notifierRecipient != null ? "flex" : "none"%>;">
            <div class="alert-icon"><%=Html.GetThemedSVG("backlight") %></div>
            <div class="local-title"><span><%= Html.TranslateTag("Screen Backlight") %></span></div>
            <div class="switch">
                <input value="<%= BackLight %>" <%=BackLight ? "checked=\"checked\"" : "" %> type="checkbox" id="backLight_<%:Model.SensorID %>" name="backLight_<%:Model.SensorID %>">
                <div class="slider round" onclick="setLocalAlertVal('backLight_','<%:Model.SensorID %>');">
                    <span class="on"><%= Html.TranslateTag("ON") %></span>
                    <span class="off"><%= Html.TranslateTag("OFF") %></span>
                </div>
            </div>
        </div>

        <%-- Delay Minutes--%>
        <div class="alert-line nrd<%:Model.SensorID %> Notifier notifyDevice"  style="cursor: pointer; display: <%:notifierRecipient != null ? "flex" : "none"%>;" data-id="<%=Model.SensorID %>">
            <button id="openSpinnerModalFor_<%:Model.SensorID %>" class="ccu-send-btn" type="button" onclick="showDelay(<%:Model.SensorID %>);" style="display: <%:notifierRecipient != null ? "flex" : "none"%>;">
                       <%=Html.GetThemedSVG("send") %>
                <div class=""><span><%= Html.TranslateTag("Send command") %></span>
                    <span id="delayValue_<%=Model.SensorID %>"><%=(isInProgress && notifierRecipient.DelayMinutes <= 0 ? Html.TranslateTag("when condition met") : "<b>" + delayMinutes + "</b>  " + delayLabel) %></span>
                </div>
            </button>
              <input type="hidden" id="delayTime_<%=Model.SensorID %>" value="<%=isInProgress ? notifierRecipient.DelayMinutes : -1 %>" name="delayMinutes_<%:Model.SensorID %>" />   
        </div>
    </div>
</div>
<script>
        var valuesArrayInStrings_<%:Model.SensorID %> = ["<%: Html.TranslateTag("No Delay")%>", "<%: Html.TranslateTag("2 Minutes")%>", "<%: Html.TranslateTag("5 Minutes")%>", "<%: Html.TranslateTag("10 Minutes")%>", "<%: Html.TranslateTag("15 Minutes")%>", "<%: Html.TranslateTag("30 Minutes")%>", "<%: Html.TranslateTag("45 Minutes")%>", "<%: Html.TranslateTag("1 Hour")%>", "<%: Html.TranslateTag("2 Hours")%>", "<%: Html.TranslateTag("4 Hours")%>", "<%: Html.TranslateTag("6 Hours")%>", "<%: Html.TranslateTag("8 Hours")%>", "<%: Html.TranslateTag("10 Hours")%>", "<%: Html.TranslateTag("12 Hours")%>", "<%: Html.TranslateTag("16 Hours")%>", "<%: Html.TranslateTag("20 Hours")%>", "<%: Html.TranslateTag("24 Hours")%>"]
        var valuesArrayInNumbers_<%:Model.SensorID %> = [0, 2, 5, 10, 15, 30, 45, 60, 120, 240, 360, 480, 600, 720, 960, 1200, 1440];

    createSpinnerModal("openSpinnerModalFor_<%:Model.SensorID %>", "Time To Delay", "delayTime_<%=Model.SensorID %>", valuesArrayInStrings_<%:Model.SensorID %>,0);

    var inputElement<%=Model.SensorID %> = document.getElementById('delayTime_<%=Model.SensorID %>');
    var spanElement<%=Model.SensorID %> = document.getElementById('delayValue_<%=Model.SensorID %>');

    var observer<%=Model.SensorID %> = new MutationObserver(function (mutationsList, observer) {

        for (let mutation of mutationsList) {

            if (mutation.type === 'attributes' && mutation.attributeName === 'value') {
                observer<%=Model.SensorID %>.disconnect();

                if (inputElement<%=Model.SensorID %>.value !== "No Delay") {
                    spanElement<%=Model.SensorID %>.textContent = `${inputElement<%=Model.SensorID %>.value} <%: Html.TranslateTag(" after condition met ")%>`;
                } else {
                    spanElement<%=Model.SensorID %>.textContent = `<%: Html.TranslateTag(" after condition met ")%>`;
                }
                inputElement<%=Model.SensorID %>.value = StringToNumber(inputElement<%=Model.SensorID %>.value)
                observer<%=Model.SensorID %>.observe(inputElement<%=Model.SensorID %>, { attributes: true });
        }
    }
});

    observer<%=Model.SensorID %>.observe(inputElement<%=Model.SensorID %>, { attributes: true });

    function StringToNumber(string) {
        var matchingIndex = valuesArrayInStrings_<%:Model.SensorID %>.indexOf(string);
        return valuesArrayInNumbers_<%:Model.SensorID %>[matchingIndex];
        }

</script>