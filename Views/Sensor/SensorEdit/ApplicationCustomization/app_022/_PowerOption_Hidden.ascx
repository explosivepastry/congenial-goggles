<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>

<%
    bool viewable = ZeroToTwentyMilliamp.ShowAdvCal(Model.SensorID);

    if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Support_Advanced"))
                {
            
%>
        <input hidden type="checkbox" name="showAdvCal" id="showAdvCal" <%= viewable ? "checked='checked'" : ""  %> data-toggle="toggle" />

        <%if (Model.GenerationType.ToUpper().Contains("GEN2"))
          {%>

        <select hidden name="power" id="power" class="powerdrop" <%=Model.CanUpdate ? "" : "disabled" %>>
            <option value="0" <%=Model.Calibration3 == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("None","None")%></option>
            <option value="1" <%=Model.Calibration3 == 1 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Digital High","Digital High")%></option>
            <option value="2" <%=Model.Calibration3 == 2 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Digital Low","Digital Low")%></option>
        </select>

        <%}

          else
          { %>

        <select hidden name="power" id="Select1" class="powerdrop" <%=Model.CanUpdate ? "" : "disabled" %>>
            <option value="0" <%=Model.Calibration3 == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("None","None")%></option>
            <option value="1" <%=Model.Calibration3 == 1 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Digital High","Digital High")%></option>
            <option value="2" <%=Model.Calibration3 == 2 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Digital Low","Digital Low")%></option>
            <option value="3" <%=Model.Calibration3 == 3 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Amplifier SP9 Active Low","Amplifier SP9 Active Low")%></option>
        </select>

        <%}%>

        <input hidden type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="delay" id="delay" class="delaymil" value="<%=Model.Calibration4 %>" />

<%}%>
