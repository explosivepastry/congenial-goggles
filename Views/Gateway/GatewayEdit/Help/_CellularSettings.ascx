<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_CellularSettings|Cellular Settings","Cellular Settings")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_CellularSettings|IMSI","IMSI")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_CellularSettings|The Global System for Mobile Communications utilizes a 15-digit IMSI (International Mobile Subscriber Identity) number as the primary mode to identify the country, mobile network, and subscriber. It is formatted as MCC-MNC-MSIN. MCC is the Mobile Country Code. MNC is the Mobile Network Code attached to the cellular network. MSIN is a serial number making the IMSI unique to the subscriber.", "The Global System for Mobile Communications utilizes a 15-digit IMSI (International Mobile Subscriber Identity) number as the primary mode to identify the country, mobile network, and subscriber. It is formatted as MCC-MNC-MSIN. MCC is the Mobile Country Code. MNC is the Mobile Network Code attached to the cellular network. MSIN is a serial number making the IMSI unique to the subscriber.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_CellularSettings|ICCID","ICCID")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_CellularSettings|The 19-digit unique identification number corresponding to the cellular SIM card.", "The 19-digit unique identification number corresponding to the cellular SIM card.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_CellularSettings|IMEI","IMEI")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_CellularSettings|IMEI (International Mobile Equipment Identify) is a number exclusive to your IoT Gateway to identify the gateway to the cell tower. The Global System for the Mobile Communications network stores the IMEI numbers in their database (EIR - Equipment Identify Register) containing all valid cellular equipment.", "IMEI (International Mobile Equipment Identify) is a number exclusive to your IoT Gateway to identify the gateway to the cell tower. The Global System for the Mobile Communications network stores the IMEI numbers in their database (EIR - Equipment Identify Register) containing all valid cellular equipment.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_CellularSettings|Carrier Preference","Carrier Preference")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_CellularSettings|Permits the selection of Auto (default) or Manual. Auto permits the gateway to use standard gateway SIM identification rules to automatically preconfigures the gateway's cellular service. Manual is useful if that gateway does not reliably and in a timely manner connect to a tower, or a non-supported carrier SIM is used.", "Permits the selection of Auto (default) or Manual. Auto permits the gateway to use standard gateway SIM identification rules to automatically preconfigures the gateway's cellular service. Manual is useful if that gateway does not reliably and in a timely manner connect to a tower, or a non-supported carrier SIM is used.")%>
        <hr />
    </div>

</div>



