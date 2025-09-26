<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<% 
    string errorParseStr = Html.TranslateTag("Error in parsing");

    string[] simstrings = Model.MacAddress.Split('|');
    if (simstrings.Length < 3) simstrings = new string[] { "errorParseStr", "errorParseStr", "errorParseStr" }; %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("IMSI","IMSI")%>
    </div>
    <div class="col sensorEditFormInput" style="font-size:1.1em;">
        <%: simstrings[0] %>
    </div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("ICCID","ICCID")%>
    </div>
    <div class="col sensorEditFormInput" style="font-size:1.1em;">
        <%: simstrings[1] %>
    </div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("IMEI","IMEI")%>
    </div>
    <div class="col sensorEditFormInput" style="font-size:1.1em;">
        <%: simstrings[2] %>
    </div>
</div>

