<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<script type="text/javascript">
    var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
</script>
<form class="form-horizontal form-label-left" action="/Overview/SensorEdit/<%:Model.SensorID %>" id="simpleEdit_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false)%>
    <input type="hidden" value="/Overview/SensorEdit/<%:Model.SensorID %>" name="returns" id="returns" />
    <% 
        //If specific monnit application edit view exists use that page
        Account acc = Account.Load(Model.AccountID);
        if (acc.CurrentSubscription.AccountSubscriptionType.Can("sensor_advanced_edit"))
        {
            string ViewToFind = string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Edit", Model.ApplicationID.ToString("D3"));
            if (MonnitViewEngine.CheckPartialViewExists(Request, ViewToFind, "Sensor", MonnitSession.CurrentTheme.Theme))
            {
                ViewBag.returnConfirmationURL = ViewToFind;
                Html.RenderPartial("~\\Views\\Sensor\\" + ViewToFind + ".ascx", Model);
            }
            else
            {
                Html.RenderPartial("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default\\Edit.ascx", Model);
            }
        }
        else //If they don't have permissions to view advanced partials try to load simple edit
        {
            Html.RenderPartial("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default\\Edit.ascx", Model);
        }
    %>
</form>