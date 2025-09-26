<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.DateRangeModel>" %>


    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        <%
           DateTime LocalStart = MonnitSession.MakeLocal(Model.StartDate);
           DateTime LocalEnd = MonnitSession.MakeLocal(Model.EndDate); 
        %>
            <div class="editor-label">
                <%: Html.LabelFor(model => model.StartDate) %>
            </div>
            <div class="editor-field">
                <input type="text" class="datepicker" id="StartDate" name="StartDate" value="<%:LocalStart.ToShortDateString() %>" />
            </div>
            <%if (Model.ShowTime)
              { %>
                <div class="editor-label">
                    
                </div>
                <div class="editor-field">
                    <%: Html.DropDownList("StartTimeHour", (SelectList)Model.Hours(LocalStart), new { })%>:
                    <%: Html.DropDownList("StartTimeMinute", (SelectList)Model.Minutes(LocalStart), new { })%>
                    <%: Html.DropDownList("StartTimeAM", (SelectList)Model.AM(LocalStart), new { })%>
                </div>
            <%} %>
            <div class="editor-error">
                <%: Html.ValidationMessageFor(model => model.StartDate) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EndDate) %>
            </div>
            <div class="editor-field">
                <input type="text" class="datepicker" id="EndDate" name="EndDate" value="<%:LocalEnd.ToShortDateString() %>" />
            </div>
            <%if (Model.ShowTime)
              { %>
                <div class="editor-label">
                    
                </div>
                <div class="editor-field">
                    <%: Html.DropDownList("EndTimeHour", (SelectList)Model.Hours(LocalEnd), new { })%>:
                    <%: Html.DropDownList("EndTimeMinute", (SelectList)Model.Minutes(LocalEnd), new { })%>
                    <%: Html.DropDownList("EndTimeAM", (SelectList)Model.AM(LocalEnd), new { })%>
                </div>
            <%} %>
            <div class="editor-error">
                <%: Html.ValidationMessageFor(model => model.EndDate) %>
            </div>
            
    <div style="clear:both;"></div> 
    <div class="buttons" style="margin: 10px -10px -10px -10px;">
        <input type="button" onclick="postMain();" value="Submit" class="bluebutton" />
        <div style="clear:both;"></div>
    </div>

    <% } %>


<script type="text/javascript">
    $(document).ready(function () {

        $('#StartDate').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        $('#StartTimeHour').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        $('#StartTimeMinute').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        $('#StartTimeAM').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        $('#EndDate').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        $('#EndTimeMinute').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        $('#EndTimeHour').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        $('#EndTimeAM').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });


        //$(window).keydown(function (event) {
        //    if ($("*:focus").attr("id") != "savebtn") {
        //        if (event.keyCode == 13) {
        //            event.preventDefault();
        //            return false;
        //        }
        //    }
        //});

        $('form').submit(function (e) {
            e.preventDefault();
            postMain();
        });

        $(".datepicker").datepicker();
        $('#ui-datepicker-div').css('display', 'none');
    });
</script>