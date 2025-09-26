<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string MinFrequency = "";
    string MaxFrequency = "";
    string SampleRate = "";
    string frequencyScale = "";
    double resolutionValue = 1.0;
    bool isHertz = true;

    MinFrequency = AdvancedVibration2.GetMinFrequency(Model).ToString();
    MaxFrequency = AdvancedVibration2.GetMaxFrequency(Model).ToString();
    SampleRate = AdvancedVibration2.GetSampleRate(Model).ToString();
    resolutionValue = AdvancedVibration2.getResolutionValue(SampleRate.ToInt());
    isHertz = AdvancedVibration2.IsHertz(Model.SensorID);
    frequencyScale = isHertz ? "Hz" : "rpm";


%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_111|Sample Rate","Sample Rate")%>
    </div>
    <div class="col sensorEditFormInput">
       <select  <%=Model.CanUpdate ? "" : "disabled" %> id="SampleRate_Manual" name="SampleRate_Manual" class="form-select" <%:ViewData["disabled"].ToBool() ? "disabled" : ""%>>
         <%--   <option <%: SampleRate == "15"? "selected":"" %> value="15">25600 Hz</option>--%>
            <option <%: SampleRate == "14"? "selected":"" %> value="14">12800 Hz</option>
            <option <%: SampleRate == "13"? "selected":"" %> value="13">6400 Hz</option>
            <option <%: SampleRate == "12"? "selected":"" %> value="12">3200 Hz</option>
            <option <%: SampleRate == "11"? "selected":"" %> value="11">1600 Hz</option>
            <option <%: SampleRate == "10"? "selected":"" %> value="10">800 Hz</option>
            <option <%: SampleRate == "9"? "selected":"" %> value="9">400 Hz</option>
            <option <%: SampleRate == "8"? "selected":"" %> value="8">200 Hz</option>
            <option <%: SampleRate == "7"? "selected":"" %> value="7">100 Hz</option>
            <option <%: SampleRate == "6"? "selected":"" %> value="6">50 Hz</option>
            <option <%: SampleRate == "5"? "selected":"" %> value="5">25 Hz</option>
        </select>
    </div>
</div>



<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_111|Frequency Range Minimum","Frequency Range Minimum")%> (<%=frequencyScale%>)
    </div>
    <div class="col sensorEditFormInput" id="MinFrequency_Manual_Div">
       <select  <%=Model.CanUpdate ? "" : "disabled" %> id="MinFrequency_Manual" name="MinFrequency_Manual" class="form-select" <%:ViewData["disabled"].ToBool() ? "disabled" : ""%>>
           <%for (int i = 2; i <= 128; i++){ %> 
           <option <%:(SampleRate.ToInt() == 5 && i > 125) ? "disabled" : ""%> <%: MinFrequency == i.ToString() ? "selected" : "" %> value="<%=i %>"><%= isHertz ? (Math.Round( i * resolutionValue ,1)).ToString("0.0") : (Math.Round( i * resolutionValue ,1)).ToRPM().ToString("0.0") %> <%=frequencyScale %></option>
           <%} %>
        </select>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_111|Frequency Range Maximum","Frequency Range Maximum")%> (<%=frequencyScale%>)
    </div>
    <div class="col sensorEditFormInput" id="MaxFrequency_Manual_Div">
       <select  <%=Model.CanUpdate ? "" : "disabled" %> id="MaxFrequency_Manual" name="MaxFrequency_Manual" class="form-select" <%:ViewData["disabled"].ToBool() ? "disabled" : ""%>>
           <%for (int i = 2; i <= 128; i++){ %> 
           <option  <%:(SampleRate.ToInt() == 5 && i > 125) ? "disabled" : ""%> <%: MaxFrequency == i.ToString() ? "selected" : "" %> value="<%=i %>"><%= isHertz ? (Math.Round( i * resolutionValue ,1)).ToString("0.0") : (Math.Round( i * resolutionValue ,1)).ToRPM().ToString("0.0") %> <%=frequencyScale %></option>
           <%} %>
        </select>
    </div>
</div>


<script type="text/javascript">

    var MinUIVal = 2;
    var MaxUIVal = 128;
    var UIStep = 0;
    var frequencyScale = '<%=frequencyScale%>';
    var isHertz = <%=isHertz.ToString().ToLower()%>;

    $(document).ready(function () {

        $("#MinFrequency_Manual").change(function () {
            if (isANumber(Number($("#MinFrequency_Manual").val()))) {
                if (parseFloat($("#MinFrequency_Manual").val()) < MinUIVal) {
                    $("#MinFrequency_Manual").val(MinUIVal);
                }
                if (parseFloat($("#MinFrequency_Manual").val()) > (MaxUIVal - 3)) {
                    $("#MinFrequency_Manual").val(MaxUIVal - 3);
                }

                if (parseFloat($("#MinFrequency_Manual").val()) + 3.0 > parseFloat($("#MaxFrequency_Manual").val())) {
                    $("#MinFrequency_Manual").val(parseFloat($("#MaxFrequency_Manual").val()) - 3.0);
                }
            }
            else {
                $("#MinFrequency_Manual").val(parseFloat(<%: MinFrequency%>));
            }
        });

        $("#MaxFrequency_Manual").change(function () {
            if (isANumber(Number($("#MaxFrequency_Manual").val()))) {
                if (parseFloat($("#MaxFrequency_Manual").val()) < (MinUIVal + 3)) {
                    $("#MaxFrequency_Manual").val(MinUIVal + 3);
                }

                if (parseFloat($("#MaxFrequency_Manual").val()) > MaxUIVal) {
                    $("#MaxFrequency_Manual").val(MaxUIVal);
                }

                if (parseFloat($("#MaxFrequency_Manual").val()) - 3.0 < $("#MinFrequency_Manual").val()) {
                    $("#MaxFrequency_Manual").val(parseFloat($("#MinFrequency_Manual").val()) + 3.0);
                }
            }
            else {
                $("#MaxFrequency_Manual").val(<%: MaxFrequency%>);
            }
        });
        
        $('#SampleRate_Manual').change(function (e) { 
            e.preventDefault();
            setFreqRanges(Number($('#SampleRate_Manual').val()));
        });
    });



    function setFreqRanges(sampleRate) {
        var MinDefaultVal = 2;
        var mode = $('#VibrationMode_Manual').val();
        switch (mode) {
            case '0': // Velocity RMS
                MinDefaultVal = 6;
                break;
            case '1': //Acceleration RMS
            case '2': //Acceleration Peak
                MinDefaultVal = 4;
                break;
            case '4': //Displacement
                MinDefaultVal = 8;
                break;
            default:
                break;
        }
        setFreqRangeSelect(Number(sampleRate));

        $("#MinFrequency_Manual").val(MinDefaultVal);
        $("#MaxFrequency_Manual").val(85);
    }

    function setFreqRangeSelect(sampleRate) {     
        $("#MinFrequency_Manual > option").each(function () {
            if (isHertz) {
                this.text = (this.value * getResolutionValue(Number(sampleRate))).toFixed(1) + ' Hz';
            } else {
                this.text = ((this.value * getResolutionValue(Number(sampleRate))) * 60.0).toFixed(1) + ' rpm'; 
            }

            if (this.value > 125) {
                if (sampleRate == 5) {
                    $(this).prop("disabled", true);
                    this.text = "";
                } else {
                    $(this).prop("disabled", false);
                }
            }      
        });

        $("#MaxFrequency_Manual > option").each(function () {
            if (isHertz) {
                this.text = (this.value * getResolutionValue(Number(sampleRate))).toFixed(1) + ' Hz';
            } else {
                this.text = ((this.value * getResolutionValue(Number(sampleRate))) * 60.0).toFixed(1) + ' rpm';
            }
          
            if (this.value > 125) {
                if (sampleRate == 5) {
                    $(this).prop("disabled", true);
                    this.text = "";
                } else {
                    $(this).prop("disabled", false);
                }
            }
        });
    }

    function getResolutionValue(sampleRate) {
        switch (sampleRate) {
            case 5:
                return 0.1;
            case 6:
                return 0.2;
            case 7:
                return 0.4;
            case 8:
                return 0.8;
            case 9:
                return 1.6;
            case 11:
                return 6.3;
            case 12:
                return 12.5;
            case 13:
                return 25;
            case 14:
                return 50;
            case 10:
            default:
                return 3.1;
        }
    }

</script>
