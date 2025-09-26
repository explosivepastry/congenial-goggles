<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%bool FullNotiString = CurrentZeroToOneFiftyAmp.GetShowFullDataValue(Model.SensorID);%>

<%if (!Model.IsWiFiSensor)
  {%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Show Temperature","Show Temperature")%>
    </div>
    <div class="col sensorEditFormInput">

        <div class="form-check form-switch d-flex align-items-center ps-0">
                        <label id="off" class="form-check-label " style="display: none;"> <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Off","Off")%></label>
            <label id="on" class="form-check-label " style="display: none;"> <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|On","On")%></label>


            <input value="" onclick="onOffToggle2()" class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="fullChk" id="fullChk" <%=CurrentZeroToOneFiftyAmp.GetShowFullDataValue(Model.SensorID) ? "checked" : "" %>      >





        </div>
        <div style="display: none;"><%: Html.TextBoxFor(model => FullNotiString, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>

        </div>
    </div>
</div>

<script type="text/javascript">

    $(document).ready(function () {

    //setTimeout("$('#fullChk').iButton();", 500);
    $('#fullChk').change(function () {
        if ($('#fullChk').prop('checked')) {
            $('#FullNotiString').val(1);
        } else {
            $('#FullNotiString').val(0);
        }
    });

    });
    let toggleit = document.getElementById("fullChk");
    let off1 = document.getElementById("off");
    let on1 = document.getElementById("on");


 
    function onOffToggle2() {
        if (toggleit.checked == true) {
            off1.style.display = "none";
            on1.style.display = "block";
        } else {
            on1.style.display = "none";
            off1.style.display = "block";
        }
        console.log('value', toggleit.checked);
    };
    onOffToggle2()

</script>
<style type="text/css">

    .switch {
        position: relative;
        display: inline-block;
        width: 60px;
        height: 34px;
    }

    .switch input {
         opacity: 0;
         width: 0;
         height: 0;
        }

    .slider {
        position: absolute;
        cursor: pointer;
        top: 0;
        left: 0;
        right: 0;
        bottom: 0;
        background-color: #ccc;
        -webkit-transition: .4s;
        transition: .4s;
    }

    .slider:before {
        position: absolute;
        content: "";
        height: 26px;
        width: 26px;
        left: 4px;
        bottom: 4px;
        background-color: white;
        -webkit-transition: .4s;
        transition: .4s;
    }

    input:checked + .slider {
        background-color: #2196F3;
    }

    input:focus + .slider {
        box-shadow: 0 0 1px #2196F3;
    }

    input:checked + .slider:before {
        -webkit-transform: translateX(26px);
        -ms-transform: translateX(26px);
        transform: translateX(26px);
    }

    /* Rounded sliders */
    .slider.round {
        border-radius: 34px;
    }

    .slider.round:before {
        border-radius: 50%;
    }

</style>
<%}%>