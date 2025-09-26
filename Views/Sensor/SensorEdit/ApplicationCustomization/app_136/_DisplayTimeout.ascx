<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    int displayTimeOut = LCD_Temperature.GetDisplayTimeout(Model);

%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|LCD Display Timeout","LCD Display Timeout")%> (<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Seconds","Seconds")%>)
    </div>
    <div class="col sensorEditFormInput">
        <select id="DisplayTimeOut_Manual" name="DisplayTimeOut_Manual" class="form-select ms-0" <%=Model.CanUpdate ? "" : "disabled"  %>>
            <option value="5" <%: displayTimeOut == 5 ? "selected":"" %>>5</option>
            <option value="10" <%: displayTimeOut == 10 ? "selected":"" %>>10</option>
            <option value="15" <%: displayTimeOut == 15 ? "selected":"" %>>15</option>
            <option value="20" <%: displayTimeOut == 20 ? "selected":"" %>>20</option>
            <option value="25" <%: displayTimeOut == 25 ? "selected":"" %>>25</option>
            <option value="30" <%: displayTimeOut == 30 ? "selected":"" %>>30</option>
            <option value="65535" <%: displayTimeOut == 65535 ? "selected":"" %>>Always On</option>
        </select>
         <!--<a id="DisplayTimeOutNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>-->
    </div>
</div>

<script type="text/javascript">

    var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
    //MobiScroll
    $(function () {
      
        //$('#DisplayTimeOut_Manual').mobiscroll().select({
        //    theme: 'ios',
        //    display: popLocation,
        //    //showLabel: true,
        //    minWidth: 200,
        //    headerText: 'Display Timeout (Seconds)',
        //    onInit: function (event, inst) {
        //    <%if(!Model.CanUpdate) {%>
        //    inst.settings.disabled = true;
        //   <%}%>
        //    }
        //});

        var inst = $('#DisplayTimeOut_Manual').mobiscroll('getInst');


        $("#DisplayTimeOutNum").click(function () {
            inst.show();
        });
       
    });



</script>
