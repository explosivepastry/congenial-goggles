<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    NameRule
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <%=Html.Partial("_CreateNewRuleProgressBar") %>


    <div class="name-rule_box">

        <div class="card_container__top__title justify-content-between">
            <%: Html.TranslateTag("Name the Rule","Name the Rule")%>
            <!-- help button   namerule-->
            <a class="helpIco" style="cursor: pointer!important;" data-bs-toggle="modal" title="<%: Html.TranslateTag("Overview/SensorEdit|Rule Help","Rule Help") %>" data-bs-target=".ruleHelp">
                <div class="help-hover"><%=Html.GetThemedSVG("circleQuestion") %></div>
            </a>
        </div>

        <div class="name-txt">
            <p><%= Html.TranslateTag("Rule/NameRule|Please provide a descriptive name for your rule. This will help you identify it later.") %></p>
        </div>

        <input id="ruleName" style="width: auto;" type="text" class="form-control user-dets" value="<%= string.IsNullOrWhiteSpace(MonnitSession.NotificationInProgress.Name) ? "" : MonnitSession.NotificationInProgress.Name%>" />

        <h5 id="saveRuleNameMsg" style="color: red;"></h5>


        <%--  <button type="button" id="saveRuleName" class="btn btn-primary" style="position: relative; margin-top: 20px;" onclick="$(this).hide();$('#saving').show();">

                <%: Html.TranslateTag("Save", "Save")%>
            </button>--%>

        <div class="save-me " style="width:100%">
            <button type="button" id="saveRuleName" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary user-dets" onclick="checkName();">
                <%: Html.TranslateTag("Save","Save")%>
            </button>
            <button
                class="btn btn-primary"
                id="saving"
                style="display: none;"
                type="button"
                disabled>
                <span
                    class="spinner-border spinner-border-sm"
                    role="status"
                    aria-hidden="true"></span>
                Saving...
            </button>
            <%--     <button class="btn btn-primary" id="saving" style="display: none;" type="button" disabled>
                    <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                    <%= Html.TranslateTag("Saving") %>...
                </button>--%>
        </div>
    </div>


    <div class="modal fade ruleHelp" style="z-index: 2000!important; background: rgba(0,0,0,0.5);" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="pageHelp"><%: Html.TranslateTag("Rule/NameRule|Rule Name")%></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">

                    <div class="container">
                        <p>
                            <%= Html.TranslateTag("Rule/NameRule|We recommend adding the Rule Type and Condition you’re monitoring to the Rule Name. The best Rule Names have memorable terms unique to your remote monitoring use case or application.") %>
                        </p>


                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-bs-dismiss="modal"><%: Html.TranslateTag("Close","Close")%></button>
                </div>
            </div>
        </div>
    </div>

    <script>
        var notAllowedString = '<%: Html.TranslateTag("Character not allowed")%>';
        $(function () {
            document.getElementById("ruleName").addEventListener("beforeinput", (event) => {
                if (event.data != null && (event.data.includes('>') || event.data.includes('<'))) {
                    $('#saveRuleNameMsg').html("");
                    $('#saveRuleNameMsg').html(notAllowedString + ": < or >");
                    event.preventDefault();
                }

            });


            $('#saveRuleName').click(function (e) {
                e.preventDefault();
                sessionStorage.clear();

                $('#saveRuleNameMsg').html("");
                var name = $('#ruleName').val();

                if (name.length == 0) {
                    return
                }

                $.post('/Rule/NameRule/', { name: name }, function (data) {
                    if (data.includes('Success')) {
                        var dataArray = data.split("|");
                        var notiID = dataArray[1];
                        window.location.href = "/Rule/RuleComplete/" + notiID;
                        
                    }
                    else {
                        $('#saveRuleNameMsg').html(data);

                    }
                });
            });


        });

        function checkName() {

            $('#saveRuleNameMsg').html("");
            var name = $('#ruleName').val();

            if (name.length == 0) {
                return
            } else {

                $("#saveRuleName").hide(); $('#saving').show();

            }

        }

    </script>

</asp:Content>
