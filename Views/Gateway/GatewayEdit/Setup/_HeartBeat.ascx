<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>



<div class="gateway-row-1">
    <div class="item-1">
        <%: Html.TranslateTag("Gateway/_HeartBeat|Heartbeat: ")%>
    </div>
    <div class="hb-input">
    
     <input class="form-control" name="ReportInterval" id="ReportInterval" value="<%=Model.ReportInterval %>" />
     
    
         <%: Html.ValidationMessageFor(model => model.ReportInterval)%> 
    </div>
   <p style="margin: 10px 5px;"><%:Html.TranslateTag ("Minutes") %></p>
       <div class="circleQuestion" data-bs-toggle="modal" data-bs-target="#hrModal">
            <%=Html.GetThemedSVG("circleQuestion") %>
        </div>
</div>

 <%-------The Range Slider--%>
 
<div class="container123"> 
  <input type='range' id='my-slider' min="0" max='120'value="<%=Model.ReportInterval %>" oninput="slider()">   <%: Html.ValidationMessageFor(model => model.ReportInterval)%> </input>
  
</div>

<!-- Modal -->
<div class="modal fade" id="hrModal" tabindex="-1" aria-labelledby="hrModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-sm">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="hrModalLabel"><%:Html.TranslateTag ("Heartbeat") %></h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" style="color: blue"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="word-def">
                    <%:Html.TranslateTag ("The") %> <strong style="color: rgb(17,104,173);"><%:Html.TranslateTag ("Heartbeat") %> </strong><%:Html.TranslateTag ("configures the") %> <strong style="color: #C9222B;"><%:Html.TranslateTag ("time") %></strong> <%:Html.TranslateTag ("that the") %> <strong><%:Html.TranslateTag ("gateway") %></strong> <%:Html.TranslateTag ("will check in with the server.") %>.
                    <br />
                    <%:Html.TranslateTag ("The default is 5 minutes. So every 5 minutes your gateway will report to the server.") %>
                         </div>
                </div>
            </div>
        </div>
    </div>
</div>

<style>
   /*---------range slider CSS*/
   .container123 {
  
  display: flex;
  align-items: center;
  justify-content: space-around;
  padding: 0px 20px;
  border-radius: 5px;
}
input[type="range"]{
  position: relative;
  -webkit-appearance: none;
  -moz-appearance: none;
  display: block;
  width: 90%;
  height: 8px;
  background-color: #d5d5d5;
  border-radius: 20px;
}
input[type="range"]::-webkit-slider-runnable-track{
  -webkit-appearance: none;
  height: 8px;
}
input[type="range"]::-moz-track{
  -moz-appearance: none;
  height: 8px;
}
input[type="range"]::-ms-track{
  appearance: none; 
  height: 8px;
}
input[type="range"]::-webkit-slider-thumb{
  -webkit-appearance:none;
  height: 20px;
  width: 20px;
  background-color: rgb(13 79 131);
    border-radius: 50%;
  pointer-events: auto;
  cursor: pointer;
  margin-top: -6px;
  border: none; 
}
input[type="range"]::-moz-range-thumb{
   -webkit-appearance:none;
  height: 20px;
  width: 20px;
  background-color: rgb(17,104,173);
    border-radius: 50%;
  pointer-events: auto;
  cursor: pointer;
  margin-top: -6px;
  border: none; 
}
input[type="range"]::-ms-thumb{
   appearance:none;
  height: 20px;
  width: 20px;
  background-color: rgb(17,104,173);
    border-radius: 50%;
  pointer-events: auto;
  cursor: pointer;
  margin-top: -6px;
  border: none; 
}
input[type="range"]:active::-webkit-slider-thumb{
  background-color: #ffffff;
  border: 3px solid #C9222B;
}
#slider-value {
  width: 16%;
  position: relative;
  background-color: rgb(17,104,173);
  color: white;
  text-align: center;
  padding: 10px 0;
  border-radius:5px;
}


</style>
<script >

    $(function () {
        slider();

        $('#ReportInterval').change(function () {
            slider($(this).val());
        });
    });
    /*This is the new Range slider*/

    const mySlider = document.getElementById("my-slider");
    //const sliderValue = document.getElementById("slider-Value");

    function slider(value) {
        if ($.isNumeric(value)) {
            mySlider.value = value;
        }

        valPercent = (mySlider.value / mySlider.max) * 100;

        mySlider.style.background = `linear-gradient(to right, rgb(17,104,173,0.8) ${valPercent}%, #d5d5d5 ${valPercent}%)`;
        $('#ReportInterval').val(mySlider.value);
    }
    

</script>
