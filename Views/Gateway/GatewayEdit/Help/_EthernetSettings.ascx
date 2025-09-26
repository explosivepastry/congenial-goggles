<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_EthernetSettings|Ethernet/Network Settings","Ethernet/Network Settings")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_EthernetSettings|MAC Address","MAC Address")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_EthernetSettings|This is the media access control address of your gateway to exclusively identify the device to a Network Interface Controller.", "This is the media access control address of your gateway to exclusively identify the device to a Network Interface Controller.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_EthernetSettings|Static IP","Static IP")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_EthernetSettings|A static IP Address is a numerical sequence assigned to a computer by a network administrator. This is different from a Dynamic IP Address. A Static IP doesn't periodically change and remains constant.", "A static IP Address is a numerical sequence assigned to a computer by a network administrator. This is different from a Dynamic IP Address. A Static IP doesn't periodically change and remains constant.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_EthernetSettings|Network Mask","Network Mask")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_EthernetSettings|Also known as a 'subnet mask', this number hides the network half of an IP Address.", "Also known as a 'subnet mask', this number hides the network half of an IP Address.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_EthernetSettings|Default Gateway","Default Gateway")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_EthernetSettings|This is the forwarding host the gateway uses to relay data to the Internet, typically your router IP Address.", "This is the forwarding host the gateway uses to relay data to the Internet, typically your router IP Address.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_EthernetSettings|Default DNS Server","Default DNS Server")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_EthernetSettings|DNS Servers take alphanumerical data (like a URL address) and returns the IP Address for the server containing the information you need.", "DNS Servers take alphanumerical data (like a URL address) and returns the IP Address for the server containing the information you need.")%>
        <hr />
    </div>

</div>



