<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<CertificationAcknowledgementModel>>" %>

<%if(Model.Count > 0)
    {
        foreach (CertificationAcknowledgementModel item in Model)
        {
%>
<a  href="/overview/SensorCertificate/<%=item.SensorID %>" class="small-list-card" style="cursor: pointer;font-size:inherit!important">
    <div class=" triggerDevice__container w-100">
        <div class="hidden-xs triggerDevice__icon">
            <%=Html.GetThemedSVG("app" + item.ApplicationID) %>
        </div>
        <div class="triggerDevice__name" >
            <strong><%:System.Web.HttpUtility.HtmlDecode(item.SensorName) %></strong>
            <br />
            Certificate Expires: <%=item.CertificationValidUntil.ToShortDateString() %>
        </div>
            <%=Html.GetThemedSVG("certificate") %>
    </div>
</a>
<%}
    }%>


<script type="text/javascript">


</script>
