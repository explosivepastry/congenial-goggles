<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_CommandSettings|Command Settings","Command Settings")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_CommandSettings|Data Expiration","Data Expiration")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_CommandSettings|After this time has elapsed, the data pulled for Modbus and SNMP will be zero-ed out.", "After this time has elapsed, the data pulled for Modbus and SNMP will be zero-ed out.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_CommandSettings|Auto Reset","Auto Reset")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_CommandSettings|The amount of time in hours that the Local Interface will automatically reboot. Settings this to 0 will disable the feature. The maximum settings is 8760 hours.", "The amount of time in hours that the Local Interface will automatically reboot. Settings this to 0 will disable the feature. The maximum settings is 8760 hours.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_CommandSettings|Reform Network","Reform Network")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_CommandSettings|This command will trigger the gateway to remove all sensors from the internal whitelist, and then request a new sensor list from the server. This command will force all sensors to reinitalize their connection with the gateway.", "This command will trigger the gateway to remove all sensors from the internal whitelist, and then request a new sensor list from the server. This command will force all sensors to reinitalize their connection with the gateway.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_CommandSettings|Update Gateway Firmware","Update Gateway Firmware")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_CommandSettings|If updates are avaliable, this option will install the latest firmware.", "If updates are avaliable, this option will install the latest firmware.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_CommandSettings|Reset Gateway to Factory Defaults","Reset Gateway to Factory Defaults")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_CommandSettings|Will erase all of your unique settings and return the gateway to factory default settings.", "Will erase all of your unique settings and return the gateway to factory default settings.")%>
        <hr />
    </div>

</div>



