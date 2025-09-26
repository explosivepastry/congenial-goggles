<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Notification>>" %>




<div class="choose-task" id="existingRuleDiv" <%= Model.Count == 0 ? "style=\"display:none;\"" : ""%>>
    <div class="rule-card_container"style="width:100%">
        <div class="card_container__top">
            <div class="rule-title" style="margin-bottom: 10px;border-bottom: 1px solid #ccc; width:100%; ">
                <%: Html.TranslateTag("Or use an existing rule.")%>
            </div>

            <br />
        </div>

        <div class="hasScroll-rule" id="existingRulesList">
            <%foreach (Notification item in Model)
                { %>
            <div class="toggleRule super_small_card" onclick="goToRule(<%:item.NotificationID%>)" >
                <div class=" ">
                    <div class="triggerDevice__name">
                        <strong style="font-size:.9rem;"><%:System.Web.HttpUtility.HtmlDecode(item.Name) %></strong>
                    </div>

                    <div class="col-1" style="text-align: center;">
                        <div>
                            <div class="dropleft" style="width: 50px;">
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            <%} %>
        </div>
    </div>
</div>

<script>

    function goToRule(notiID) {
        window.location.href = "/Rule/ChooseTaskToEdit/" + notiID
    }

</script>