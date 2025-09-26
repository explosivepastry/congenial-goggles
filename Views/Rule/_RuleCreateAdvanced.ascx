<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Notification>" %>

<% 
    //Dictionary<string, object> dic = new Dictionary<string, object>();
    AdvancedNotification Advanced = ViewData["AdvancedNotification"] as AdvancedNotification;

%>

<div class="adv-rule-condition-tag">
  
        <div class="card_container__top">
            <div class="card_container__top__title">
                <%: Html.TranslateTag("Choose a Condition","Choose a Condition")%>
            </div>

          
        </div>
        <div class="clearfix"></div>
        <div class="card_container__body">
            <div class="col-12">
                <%if (!string.IsNullOrEmpty(Advanced.HelpText))
                    { %>
                <div class="editor-help">
                    <br />
                    <%: Html.Raw(Advanced.HelpText)%>
                </div>
                <%} %>

                <%
                    List<AdvancedNotificationParameter> paramList = (ViewData["Params"] as List<AdvancedNotificationParameter>).OrderBy(p => p.DisplayOrder).ToList();
                    string Settings = "";
                    if (paramList == null || paramList.Count == 0)
                    {%>
                     <div class="row sensorEditForm">
                    <div class="col-12 col-md-4">
                        <%: Html.TranslateTag("Events/CreateBatteryTrigger|This Advanced rule has no condition parameters, Click Save Button to Continue")%>
                    </div>
                    <div class="col sensorEditFormInput">
             
                    </div>
                </div>
                    <%}
                    else
                    {
                        foreach (AdvancedNotificationParameter param in paramList)
                        {
                            if (param.ParameterName.ToLower() == "sensorid") continue;

                            string value = "";
                            Dictionary<string, string> dic = Session["AdvancedNotificationParams"] as Dictionary<string, string>;
                            if (dic != null && dic.ContainsKey(param.AdvancedNotificationParameterID.ToString()))
                            {
                                value = dic[param.AdvancedNotificationParameterID.ToString()];
                            }

                            Settings += string.Format("'ParamID_{0}=' + $('#ParamID_{0}').val() + '&' +", param.AdvancedNotificationParameterID);

                                %>
                <div class="">
                    <div class="innerCard__title-small" style="margin-top: 10px;">
                        <%:param.ParameterDisplayName %>
                        <%:param.Required ? "" : "(" + Html.TranslateTag("Optional") +")" %>
                    </div>
                </div>
                <div class="col-sm-6 col-12 triggerCondition_set">
                    <%switch (param.Type.ToString())
                        {
                            case "System.DateTime":%>
                    <%: Html.TextBox("ParamID_" + param.AdvancedNotificationParameterID.ToString(), value, new { @class = "DateTime form-control", id = "ParamID_" + param.AdvancedNotificationParameterID.ToString() })%>
                    <%break;
                        case "System.Boolean":
                        case "System.SByte":%>
                    <select class="form-select" id="ParamID_<%:param.AdvancedNotificationParameterID.ToString() %>" name="ParamID_<%:param.AdvancedNotificationParameterID.ToString() %>">
                        <option value="True" <% if (value == "True") Response.Write("selected='selected'"); %>><%: Html.TranslateTag("True", "True")%></option>
                        <option value="False" <% if (value == "False") Response.Write("selected='selected'"); %>><%: Html.TranslateTag("False", "False")%></option>
                    </select>
                    <%break;
                        case "System.TimeSpan":
                            double Temp;
                            DateTime date;

                            if (!double.TryParse(value, out Temp) && DateTime.TryParse(value, out date))
                                value = date.Subtract(new DateTime(1900, 1, 1)).TotalMinutes.ToString();
                                          %>

                    <%if (param.ParameterType.ToString().ToLower() != "option")
                        {%><%: Html.TextBox("ParamID_" + param.AdvancedNotificationParameterID.ToString(), value, new { @class = "form-control", id = "ParamID_" + param.AdvancedNotificationParameterID.ToString() })%><%}%>
                    <%else
                        {%><%: Html.TextBox("ParamID_" + param.AdvancedNotificationParameterID.ToString(), value, new { @class = "form-control me-1", id = "ParamID_" + param.AdvancedNotificationParameterID.ToString() })%><%}%>
                    <%: Html.Label(param.Units.ToString())%>

                    <%break;
                        case "Monnit.eCompareType":%>
                    <%: Html.DropDownList("ParamID_" + param.AdvancedNotificationParameterID.ToString(), !string.IsNullOrEmpty(value) ? (Monnit.eCompareType)Enum.Parse(typeof(Monnit.eCompareType), value) : Monnit.eCompareType.Equal)%>
                    <%break;
                        case "Monnit.AdvancedNotificationParameterOption":%>
                    <%: Html.DropDownList("ParamID_" + param.AdvancedNotificationParameterID.ToString(), param.Options, "Display", "Value", value, "")%>
                    <%break;
                        case "System.String":
                        case "System.UInt32":
                        case "System.Int32":
                        case "System.UInt64":
                        case "System.Int64":
                        case "System.Double":
                        default:%>

                    <%: Html.TextBox("ParamID_" + param.AdvancedNotificationParameterID.ToString(), value, new { @class = "form-control", id = "ParamID_" + param.AdvancedNotificationParameterID.ToString() })%>

                    <%break;
                        }%>
                </div>
                <div class="clearfix"></div>
                <% }
                    }%>
            </div>

            <div class=" save-me2">
                <button type="button" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary" onclick="$(this).hide();$('#saving').show();createTrigger(this);">
                    <%: Html.TranslateTag("Save","Save")%>
                                &nbsp;
                    <%=Html.GetThemedSVG("save") %>
                               
    
                </button>
                <button class="btn btn-primary" id="saving" style="display: none;" type="button" disabled>
                    <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                    Saving...
                </button>
            </div>
            <div class="clearfix"></div>
            <div id="result"></div>

            <script type="text/javascript">

                $('select').addClass('form-select');

                function createTrigger(btn) {
                    btn = $(btn);
                    btn.hide();
                    var beginAjaxTime = Date.now();

                    var settings = <%=Settings%> 'advancedNotificationID=' + <%:Model.AdvancedNotificationID%>;
                    settings += '&CompareType=Equal&CompareValue=Advanced';

                    $.post("/Rule/AddRuleConditions", settings, function (data) {
                        //Show loader for at leat 500ms
                        if (data == "Success") {
                            window.location.href = "/Rule/ChooseTask";
                        }
                    });
                }
                        </script>


        </div>

    
</div>
