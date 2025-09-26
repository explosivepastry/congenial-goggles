<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
  
<% Sensor sens = ViewBag.Sensor as Sensor;
        var datumType = MonnitSession.NotificationInProgress.eDatumType;
        if (sens != null)
        {
    %>
    <div class="choose-task" id="existingRuleDiv" <%= (datumType == eDatumType.Error || Model.Count == 0 ) ? "style=\"display:none;\"" : ""%>>
        <div class="rule-card_container" style="width:100%">
            <div class="card_container__top">
                <div class="rule-title" style="margin-bottom: 10px; border-bottom: 1px solid #ccc; width:100%; ">
                    <%: Html.TranslateTag("Or use an existing rule.")%>
                </div>

                <br />
            </div>

            <div class="hasScroll-rule" id="existingRulesList">
                <div id="rulesLoading" class="text-center ">
                    <div class="spinner-border text-primary my-3" role="status">
                        <span class="visually-hidden"><%= Html.TranslateTag("Loading") %>...</span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%} %>
