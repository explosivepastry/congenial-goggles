<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    List<eDatumType> datumTypes = Model.GetDatumTypes();
    if(datumTypes.Count > 1) { %>
        <tr><th><h2>Data Names</h2></th></tr>
<%
        for(int i=0; i<datumTypes.Count; i++) {
%>

<tr>
    <td>
        <%:Model.GetDatumDefaultName(i)%>
    </td>
    <td>
        <%: Html.TextBox("Datum"+i,Model.GetDatumName(i)) %>
    </td>
</tr>

<%      }
    } %>






