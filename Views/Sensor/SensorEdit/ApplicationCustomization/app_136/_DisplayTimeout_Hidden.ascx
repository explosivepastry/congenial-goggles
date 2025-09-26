<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    int displayTimeOut = LCD_Temperature.GetDisplayTimeout(Model);

%>

    <select hidden id="DisplayTimeOut_Manual" name="DisplayTimeOut_Manual" class="editField editFieldMedium" <%=Model.CanUpdate ? "" : "disabled"  %>>
        <option value="5" <%: displayTimeOut == 5 ? "selected":"" %>>5</option>
        <option value="10" <%: displayTimeOut == 10 ? "selected":"" %>>10</option>
        <option value="15" <%: displayTimeOut == 15 ? "selected":"" %>>15</option>
        <option value="20" <%: displayTimeOut == 20 ? "selected":"" %>>20</option>
        <option value="25" <%: displayTimeOut == 25 ? "selected":"" %>>25</option>
        <option value="30" <%: displayTimeOut == 30 ? "selected":"" %>>30</option>
        <option value="65535" <%: displayTimeOut == 65535 ? "selected":"" %>>Always On</option>
    </select>