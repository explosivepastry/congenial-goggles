<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string SelectedValue = "";
    switch (Model.Hysteresis)
    {
        case 2:
            SelectedValue = "State Change";
            break;
        case 0:
            SelectedValue = "No Motion";
            break;
        case 1:
            SelectedValue = "Motion";
            break;
    }

    SelectList select = new SelectList(new string[3] { "Motion", "No Motion", "State Change" }, SelectedValue);
       ((Dictionary<string,object>)ViewData["HtmlAttributes"]).Add("style", "margin-left:0px;");
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_101|Enter Aware State when there is","Enter Aware State when there is")%>
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.DropDownList("EnterAwareOn", select as IEnumerable<SelectListItem>, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>


<script type="text/javascript">

    //MobiScroll
    $(function () {
                <% if (Model.CanUpdate)
                   { %>

        //$('#EnterAwareOn').mobiscroll().select({
        //    theme: 'ios',
        //    display: popLocation,
        //    minWidth: 200
        //});

    <%}%>

    });

    $('#EnterAwareOn').addClass('form-select');
</script>
