<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Monnit.SensorMessageAudit.SensorMessageAuditDataModel>>" %>

           <table class="inboundPacket" width="100%">
                <thead>
                    <tr>
                        <th width="20"></th>

                        <th>SensorID
                        </th>

                        <th>Gateway ID
                        </th>

                        <th>Message Event
                        </th>

                        <th>Message Date
                        </th>


                           <th>Success
                        </th>
                        <th width="20"></th>
                    </tr>
                </thead>
                <tbody>
                    <%if (Model != null)
                      { %>
                    <%foreach (SensorMessageAudit.SensorMessageAuditDataModel item in Model)
                      {
                          %>
                    <tr class="Details">
                        <td width="20"></td>

                        <td >
                          <%: item.SensorID%>
                        </td>
                        
                        <td><%: item.GatewayID%>
                        </td>

                        <td><%: item.MessageEvent%>
                        </td>

                       <td><%:  item.MessageDate.ToLocalTime().ToString("MM-dd-yyyy HH:mm:ss")%>
                        </td>

                        <td><%:item.Success %></td>
                         <td width="20"></td>
                    </tr>
                 <%}
                      } %>   
                    </tbody>
               </table>    