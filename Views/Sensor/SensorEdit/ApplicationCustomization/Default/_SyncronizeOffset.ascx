<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%if (!Model.IsWiFiSensor)
    {%>

<%if (Model.MeasurementsPerTransmission <= 1)
    {%>
<div class="111 row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Synchronize","Synchronize")%>
    </div>

    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label id="par" class="form-check-label " style="display: none;"><%: Html.TranslateTag("Off","Off")%></label>
            <label id="par2" class="form-check-label " style="display: none;"><%: Html.TranslateTag("On","On")%></label>
            <input value="<%=Model.TransmitOffset > 0 ? "checked" : "" %> " onclick="onOffToggle()" class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="TransmitOffsetChk" id="TransmitOffsetChk" <%=Model.TransmitOffset > 0 ? "checked" : "" %>>
        </div>

        <div style="display: none;"><%: Html.TextBoxFor(model => model.TransmitOffset, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
    </div>
</div>
<%}%>


<script type="text/javascript">

    let toggle = document.getElementById("TransmitOffsetChk");
    let off = document.getElementById("par");
    let on = document.getElementById("par2");
    let tog = document.getElementById("TransmitOffset");

    $(document).ready(function () {
        $('#TransmitOffsetChk').change(function () {
            if ($('#TransmitOffsetChk').prop('checked')) {
                $('#TransmitOffset').val(7);
            } else {
                $('#TransmitOffset').val(0);
            }
        });
    });

    function onOffToggle() {
        if (toggle.checked == true) {
            off.style.display = "none";
            on.style.display = "block";
        } else {
            on.style.display = "none";
            off.style.display = "block";
        }
    };
    onOffToggle()

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