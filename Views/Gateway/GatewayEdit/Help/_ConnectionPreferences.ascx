<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Connection Preferences","Connection Preferences")%>
    </div>
     <div class="word-def" >
        <%: Html.TranslateTag("Gateway/_ConnectionPreferences|Enables the selection of how the server sends messages to the Primary Server. Options are Ethernet Preferred (default). Ethernet Only, and Cellular Only. Ethernet Preferred is also 'Ethernet with Cellular Backup'. When either Ethernet Preferred or Cellular Only are selected, the location (GPS/GNSS) data generator capability are enabled. Ethernet Only will disable the location data generator. ","Enables the selection of how the server sends messages to the Primary Server. Options are Ethernet Preferred (default). Ethernet Only, and Cellular Only. Ethernet Preferred is also 'Ethernet with Cellular Backup'. When either Ethernet Preferred or Cellular Only are selected, the location (GPS/GNSS) data generator capability are enabled. Ethernet Only will disable the location data generator.")%>
        <hr />
    </div>
</div>

