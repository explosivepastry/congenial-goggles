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
%>

    <%: Html.DropDownList("EnterAwareOn", select as IEnumerable<SelectListItem>, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
    <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
