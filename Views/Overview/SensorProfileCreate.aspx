<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<SensorProfile>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SensorProfileCreate
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>SensorProfileCreate</h2>
    <div>
        <button id="water">Create Water</button>
        <button id="button">Create Button</button>
        <button id="temperature">Create Temperature</button>
        <button id="humidity">Create Humidity</button>
        <button id="cardetect">Create CarDetect</button>
    </div>
    <br />
    
    <div id="results">
        <div id="object">
            <h4>Object Datems</h4>
            <div id="datem"></div>
        </div>

        <div id="serialized">
            <h4>Serialized</h4>
            <div id="serializedResults"></div>
        </div>

        <div id="deserialized">
            <h4>Deserialized</h4>
            <div id="deserializedResults"></div>
        </div>    
    </div>


<script>

    $(document).ready(function () {
        $('#results').hide();
    });

    $('#water').click(function () {
        var key = "020100"        
        $.post('/Overview/SensorProfileCreate/?id=2&inputData=' + key, function (data) {
            var result = getResults(data);         
        });
    });
    $('#button').click(function () {
        var key = "000000"        
        $.post('/Overview/SensorProfileCreate/?id=1&inputData=' + key, function (data) {
            var result = getResults(data);
        });
        var key = "020100"        
        $.post('/Overview/SensorProfileCreate/?id=1&inputData=' + key, function (data) {
            var result = getResults(data);
        });
    });
    $('#temperature').click(function () {
        var key = "100401"
        $.post('/Overview/SensorProfileCreate/?id=3&inputData=' + key, function (data) {
            var result = getResults(data);
        });
    });
    $('#humidity').click(function () {
        var key = "021B003700"
        $.post('/Overview/SensorProfileCreate/?id=4&inputData=' + key, function (data) {
            var result = getResults(data);
        });
    });

    $('#cardetect').click(function () {
        var key = "01000000"
        $.post('/Overview/SensorProfileCreate/?id=119&inputData=' + key, function (data) {
            var result = getResults(data);
        });
    });

    function getResults(data) {
        $('#results').show();

        var o;
        var s;
        var d;

        if (!data.includes("Failed")) {

            var splitData = data.split("~$~");
            var object = splitData[0];
            var serialized = splitData[1];
            var deserialized = splitData[2];

            o = $('#datem').html(object);
            s = $('#serializedResults').html(serialized);
            d = $('#deserializedResults').html(deserialized);

        } else {
            o = $('#datem').html("Error");
            s = $('#serializedResults').html("Error");
            d = $('#deserializedResults').html("Error");         
        }

        return [o, s, d];
    }

</script>

    </asp:Content>
