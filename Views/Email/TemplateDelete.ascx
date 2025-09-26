<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.EmailTemplate>" %>

    <h3>Are you sure you want to delete this?</h3>
    
        <div class="display-label"><%: Html.LabelFor(model => model.Name) %></div>
        <div class="display-field"><%: Model.Name %></div>
        
        <div class="display-label">Email Text</div>
        <div style="clear:both; width:585px; overflow:auto;"><%: Model.Template %></div>
        
        
    <% using (Html.BeginForm()) { %>

 <div class="buttons" style="margin: 10px -10px -10px -10px;">
	<a href="#" onclick="getMain(); return false;" class="greybutton">Cancel</a> 
    <input type="button" onclick="postMain();" value="Delete" class="bluebutton" />
    <div style="clear:both;"></div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        //$(window).keydown(function (event) {
        //    if ($("*:focus").attr("id") != "savebtn") {
        //        if (event.keyCode == 13) {
        //            event.preventDefault();
        //            return false;
        //        }
        //    }
        //});

        $('#Name').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        $('#Template').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

    });
</script>
    <% } %>


