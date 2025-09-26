<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<form action="/Sensor/Default/<%:Model.SensorID %>" id="Default_<%:Model.SensorID %>" method="post">

<div class="buttons">
<% Html.Label("Sets sensor back to original manufacture settings:"); %>
<input class="bluebutton" type="button" onclick="postMain()" value="Save" />
</div>
    <script type="text/javascript">
        $(document).ready(function () {
            
        });
</script>
</form>

