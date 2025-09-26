<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<select hidden class="tzSelect"  name="Sensitivity" id="Sensitivity" <%=Model.CanUpdate ? "" : "disabled" %>>
    <option value="64" <%=Model.Calibration2 == 64 ? "selected" : "" %>>9 feet</option>
    <option value="40" <%=Model.Calibration2 == 40 ? "selected" : "" %>>12 feet</option>
    <option value="25" <%=Model.Calibration2 == 25 ? "selected" : "" %>>15 feet</option>
</select>

