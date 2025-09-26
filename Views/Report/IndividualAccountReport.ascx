<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Data.DataTable>" %>


<%using (Html.BeginForm()) {%>

<table>
    <thead>
         <tr>
        
             <th>
                 SensorID
             </th>
            
             <th>
                 Sensor Name
             </th>
             <th>
                 Sensor Type
             </th>
             <th>
                 Network Name
             </th>
              <th>
                 Sensor Code
             </th>
        </tr>
    </thead>
    <tbody>
        <% foreach(System.Data.DataRow a in Model.Rows)
           {%>
            <tr>
               <td>
                   <%:a["SensorID"].ToStringSafe() %>
               </td>        
              <td>
                   <%:a["Sensor Name"].ToStringSafe()  %>
               </td>  
                <td>
                   <%:a["Sensor Type"].ToStringSafe() %>
               </td>  
                <td>
                   <%:a["Network Name"].ToStringSafe()  %>
               </td>  
                <td>
                    IM<%:MonnitUtil.CheckDigit(a["SensorID"].ToLong()) %>
                </td> 
            </tr>       
        <% } %>
    </tbody>
</table>
 <% } %>