<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<% DateTime killDate = MonnitSession.EnterpriseKillDate();

   string message = string.Empty;
   string sensorCount = string.Empty;%>
<%if (MonnitSession.CurrentCustomer != null)
  {
      eProgramLevel level = MonnitSession.ProgramLevel();
      int count = ThemeController.SensorCount();
      if (count > level.ToInt())
          sensorCount = "Only " + level.ToInt() + " sensors allowed for this installation.";

      if (level == eProgramLevel.EnterpriseTrial)
      {
          message = "This is a trial installation";
      }


      if (level != eProgramLevel.EnterpriseTrial && killDate.AddDays(-15) < DateTime.UtcNow)
      {
          message = "This installation will expire soon, contact support to update subscription.";
      }

      if (killDate < DateTime.UtcNow)
      {
          message = "This installation is expired, contact support to update subscription.";
      }
  }%>


<div id="expirationMessage" class="vw-100 text-center">
    <%:message %> <%:sensorCount %>
</div>
<!-- Enterprise expiration days left Message -->

<script>

</script>
