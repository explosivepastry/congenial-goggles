<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<% 
    AdvancedNotification Advanced = AdvancedNotification.Load(Model.AdvancedNotificationID);
    List<AdvancedNotificationParameter> AdvancedParams = AdvancedNotificationParameter.LoadByAdvancedNotificationID(Model.AdvancedNotificationID);
    Dictionary<string, string> coll = new Dictionary<string, string>();

    List<AdvancedNotificationParameterValue> ParamValues = AdvancedNotificationParameterValue.LoadByNotificationID(Model.NotificationID);
    foreach (AdvancedNotificationParameter p in AdvancedNotificationParameter.LoadByAdvancedNotificationID(Model.AdvancedNotificationID))
    {
        if (p.ParameterName.ToLower() == "sensorid")
            continue;

        try
        {
            string key = p.AdvancedNotificationParameterID.ToString();
            coll.Add(key, ParamValues.Find(v => v.AdvancedNotificationParameterID == p.AdvancedNotificationParameterID).ParameterValue);
        }
        catch
        {
            //Don't throw error if there happens to be a new parameter for which there is no saved value (or optional parameter)
        }
    }

%>


<div class="col-12">
    <%if (!string.IsNullOrEmpty(Advanced.HelpText))
        { %>
    <div class="editor-help">

        <%: Html.Raw(Advanced.HelpText)%>
    </div>
    <%} %>

    <%
        string Settings = "";
        foreach (AdvancedNotificationParameter param in AdvancedParams.OrderBy(p => p.DisplayOrder))
        {
            if (param.ParameterName.ToLower() == "sensorid") continue;
                    
            string value = "";
            Dictionary<string, string> dic = coll;
            if (dic != null && dic.ContainsKey(param.AdvancedNotificationParameterID.ToString()))
            {
                value = dic[param.AdvancedNotificationParameterID.ToString()];
            }

            Settings += string.Format("'ParamID_{0}=' + $('#ParamID_{0}').val() + '&' +", param.AdvancedNotificationParameterID);
          
        %>
        <div class="col-sm-4 col-12">
	        <%:param.ParameterDisplayName %> 
            <%:param.Required ? "" : "(Optional)" %> 
        </div> 
        <div class="col-sm-6 col-xs-12">
            <%switch (param.Type.ToString()) {
                case "System.DateTime":%>
                    <%: Html.TextBox("ParamID_" + param.AdvancedNotificationParameterID.ToString(), value, new { @class = "DateTime", id = "ParamID_" + param.AdvancedNotificationParameterID.ToString() })%>
                <%break;
                case "System.Boolean":
                case "System.SByte":%>
                    <select class="aSettings__input_input tzSelect" id="ParamID_<%:param.AdvancedNotificationParameterID.ToString() %>" name="ParamID_<%:param.AdvancedNotificationParameterID.ToString() %>">
                        <option value="True" <% if(value == "True") Response.Write("selected='selected'"); %>><%: Html.TranslateTag("True","True")%></option>
                        <option value="False" <% if(value == "False") Response.Write("selected='selected'"); %>><%: Html.TranslateTag("False","False")%></option>
                    </select>
                <%break;
                case "System.TimeSpan":
                    double Temp;
                    DateTime date;

                    if (!double.TryParse(value, out Temp) && DateTime.TryParse(value, out date))
                        value = date.Subtract(new DateTime(1900, 1, 1)).TotalMinutes.ToString();
                    %>
              
                <%if (param.ParameterType.ToString().ToLower() != "option")
                    {%><%: Html.TextBox("ParamID_" + param.AdvancedNotificationParameterID.ToString(), value, new {@class="short", id = "ParamID_" + param.AdvancedNotificationParameterID.ToString()})%><%}%>
                    <%else 
                    {%><%: Html.TextBox("ParamID_" + param.AdvancedNotificationParameterID.ToString(), value, new { id = "ParamID_" + param.AdvancedNotificationParameterID.ToString() })%><%}%>
                    <%: Html.Label(param.Units.ToString())%>
                    
                <%break;
                case "Monnit.eCompareType":%>
                    <%: Html.DropDownList("ParamID_" + param.AdvancedNotificationParameterID.ToString(), !string.IsNullOrEmpty(value) ? (Monnit.eCompareType)Enum.Parse(typeof(Monnit.eCompareType), value) : Monnit.eCompareType.Equal)%>
                <%break;
                case "Monnit.AdvancedNotificationParameterOption":%>
                    <%: Html.DropDownList("ParamID_" + param.AdvancedNotificationParameterID.ToString(), param.Options, "Display","Value", value, "")%>
                <%break;
                case "System.String":
                case "System.UInt32":
                case "System.Int32":
                case "System.UInt64":
                case "System.Int64":
                case "System.Double":
                default:%>
                
                    <%: Html.TextBox("ParamID_" + param.AdvancedNotificationParameterID.ToString(), value, new { id = "ParamID_" + param.AdvancedNotificationParameterID.ToString() })%>
                    
                <%break;
            }%>    
        </div>
        <div class="col-sm-2 hidden-xs">
	        <%if(!string.IsNullOrEmpty(param.Description)){ %>
                    <i class="fa fa-question-circle-o" title="<%:param.Description %>" ></i>
                <% } %>
        </div> 
        <div class="clearfix"></div>
    <% } %>

</div>

<script type="text/javascript">
    function triggerSettings() {
        var settings = <%=Settings%> 'AdvancedNotificationID=' + <%:Model.AdvancedNotificationID%>;
        settings += '&CompareType=Equal&CompareValue=Advanced';

        return settings;
    }
    function triggerUrl() {
        return "/Rule/EditAdvancedTrigger/<%:Model.NotificationID%>";
    }
</script>
