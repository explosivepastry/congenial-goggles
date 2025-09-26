<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    double ActiveCurrentDelta = SootBlower2.GetActiveCurrentDelta(Model);
%>

<div class="row sensorEditForm">
  <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Active Mode Current Delta","Active Mode Current Delta")%> (<%: Html.Label("Amps") %>)
    </div>
    <div class="col sensorEditFormInput">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="ActiveCurrentDelta" id="ActiveCurrentDelta" value="<%=ActiveCurrentDelta %>" />
        <a id="currentNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<script type="text/javascript">
    $('#ActiveCurrentDelta').addClass("editField editFieldMedium");

    $(function () {

          <% if (Model.CanUpdate)
    { %>

        createSpinnerModal("currentNum", "Amps", "ActiveCurrentDelta", [1, 2, 3, 4, 5], null, [".00", ".10", ".20", ".30", ".40", ".50", ".60", ".70", ".80", ".90"]);

      <%}%>

        $('#ActiveCurrentDelta').change(function () {
            if (isANumber($("#ActiveCurrentDelta").val())) {
                if ($('#ActiveCurrentDelta').val() < 0.01)
                    $('#ActiveCurrentDelta').val(0.01);

                if ($('#ActiveCurrentDelta').val() > 5)
                    $('#ActiveCurrentDelta').val(5);
            }
            else {
                $('#ActiveCurrentDelta').val(<%: ActiveCurrentDelta%>);
            }
        });
    });
</script>
