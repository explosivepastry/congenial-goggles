<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%     DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();
    
    CalibrationFacility calFacility = CalibrationFacility.Load(Model.CalibrationFacilityID);
%>

<div class="formtitle">
    Calibration Certificate
</div>

<div class="formBody">
    <% if (calFacility != null){ %>
    <div>
        This sensor has been pre-calibrated and certified by <%: calFacility.Name %>  <%:  (CalibrationCertificationValidUntil != DateTime.MinValue && CalibrationCertificationValidUntil < new DateTime(2099,1,1)) && !string.IsNullOrWhiteSpace(CalibrationCertificationValidUntil.ToShortDateString())?"and is valid until "+CalibrationCertificationValidUntil.ToShortDateString():"" %>.
    </div>
    
    <br />
    <% if (!string.IsNullOrWhiteSpace(calFacility.CertificationPath)){ %>
    <% if (calFacility.CertificationPath.Contains("74.93.64.170"))//callabco certification numbers are  all numeric digits
           { %>
    <div>
        <a target="_blank" href="<%: string.Format(calFacility.CertificationPath,  new Regex("[^0-9-]").Replace(Model.CalibrationCertification, "")) %>">View Calibration Certificate</a>
    </div>

    <%}
      else
      {%>
    <div>
        <a target="_blank" href="<%: string.Format(calFacility.CertificationPath, Model.CalibrationCertification) %>">View Calibration Certificate</a>
    </div>
    
    <%}
      }
          } 
          else { %>
    <div>
        This sensor has been pre-calibrated and certified.
    </div>
    <%} %>
</div>
