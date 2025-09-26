<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.AdvancedNotification>" %>
    <%foreach (AdvancedNotificationParameter param in (ViewData["Params"] as List<AdvancedNotificationParameter>).OrderBy(p=>p.DisplayOrder))
      {
          if (param.ParameterName.ToLower() == "sensorid") continue;
                    
          string value = "";
          Dictionary<string, string> dic = Session["AdvancedNotificationParams"] as Dictionary<string, string>;
          if (dic != null && dic.ContainsKey(param.AdvancedNotificationParameterID.ToString()))
          {
              value = dic[param.AdvancedNotificationParameterID.ToString()];
          }
          
        %>
        <tr>
            <td>
                <%:param.ParameterDisplayName %> 
                <%:param.Required ? "" : "(Optional)" %> 
            </td>
            <td>

            <%switch (param.Type.ToString()) {
                case "System.DateTime":%>
                    <%: Html.TextBox(param.AdvancedNotificationParameterID.ToString(), value, new { @class = "DateTime", id = param.AdvancedNotificationParameterID.ToString() })%>
                <%break;
                case "System.Boolean":
                case "System.SByte":%>
                    <select id="<%:param.AdvancedNotificationParameterID.ToString() %>" name="<%:param.AdvancedNotificationParameterID.ToString() %>">
                        <option value="True" <% if(value == "True") Response.Write("selected='selected'"); %>>True</option>
                        <option value="False" <% if(value == "False") Response.Write("selected='selected'"); %>>False</option>
                    </select>
                <%break;
                case "System.TimeSpan":
                  double Temp;
                  DateTime date;

                  if (!double.TryParse(value, out Temp) && DateTime.TryParse(value, out date))
                      value = date.Subtract(new DateTime(1900, 1, 1)).TotalMinutes.ToString();
                  %>
              
                <%if (param.ParameterType.ToString().ToLower() != "option")
                  {%><%: Html.TextBox(param.AdvancedNotificationParameterID.ToString(), value, new {@class="short"})%><%}%>
                    <%else 
                  {%><%: Html.TextBox(param.AdvancedNotificationParameterID.ToString(), value)%><%}%>
                    <%: Html.Label(param.Units.ToString())%></td>
                    
                <%break;
                case "Monnit.eCompareType":%>
                    <%: Html.DropDownList(param.AdvancedNotificationParameterID.ToString(), !string.IsNullOrEmpty(value) ? (Monnit.eCompareType)Enum.Parse(typeof(Monnit.eCompareType), value) : Monnit.eCompareType.Equal)%>
                <%break;
                case "Monnit.AdvancedNotificationParameterOption":%>
                    <%: Html.DropDownList(param.AdvancedNotificationParameterID.ToString(), param.Options, "Display","Value", value, "")%>
                <%break;
                case "System.String":
                case "System.UInt32":
                case "System.Int32":
                case "System.UInt64":
                case "System.Int64":
                case "System.Double":
                default:%>
                
                    <%: Html.TextBox(param.AdvancedNotificationParameterID.ToString(), value)%>
                    
                <%break;
            }%>                    
            </td> <!-- Don't remove this </td> tag-->
            <td>
                <%if(!string.IsNullOrEmpty(param.Description)){ %>
                    <img alt="help" class="helpIcon" title="<%:param.Description %>" src="<%:Html.GetThemedContent("/images/help.png")%>" />
                <% } %>
            </td>
        </tr>

    <% } %>