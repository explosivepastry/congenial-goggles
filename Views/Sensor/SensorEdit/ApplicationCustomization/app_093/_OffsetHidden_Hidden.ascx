<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
        string label = "";
        double offset = unchecked((sbyte)(CurrentZeroToOneFiftyAmp.GetHystThirdByte(Model))) / 100d;
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);        
     
%>

    <input hidden type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Offset_Hidden" id="Offset_Hidden" value="<%=offset %>" />
