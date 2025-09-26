<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Monnit.SVGIcon>>"%>


<%if (Model.Count < 1)
  { 
%>
No Icons Matching Filter
<%}
  else
  {
      foreach (SVGIcon item in Model)
      {
          %>

<div class="gridPanel col-12 px-3">
    <table width="100%">
        <tr class="viewSensorDetails">
            <td width="70">
                <div class="divCellCenter holder holderInactive">
                    <a data-id="<%:item.SVGIconID%>" >
                        <div class="sensor sensorIcon sensorStatusInactive">
                            <%--<%=Html.GetThemedSVG(item.ImageKey) %>--%>
                            <%=item.HTMLCode%>
                        </div>
                    </a>
                </div>
            </td>

            <td valign="middle" style="padding: 0px;">
                <a href="/Admin/SystemIconEdit/<%:item.SVGIconID%>">
                    <div class="glance-text">
                        <div class="glance-name"><%=item.Name%></div>
                        <div class="glance-reading" style="font-size: small;"><%:item.Category%> | <%:item.ImageKey%></div>
                    </div>
                </a>

            </td>
            <td width="90" style="text-align: center;">
                <div class="gatewaySignal sigIcon" style="text-align: center; fill:#999;">
                    <%=item.HTMLCode%>
                </div>
            </td>

        </tr>
    </table>
</div>

<%}
  } %>

<script type="text/javascript">

    $(document).ready(function () {

        $('#totalIcons').html('<%:ViewBag.TotalIcons%>');
        $('#filteredIcons').html('<%:Model.Count%>');
        
        $('.delete').click(function (e) {
            e.preventDefault();
            var clickedRow = $(this);
            var id = $(this).attr('data-deletereport');
            if (confirm('Are you sure you want to delete this report?')) {
                $.get("/Export/Delete", { "id": id }, function (data) {
                    if (data == "Success")
                        window.location.href = '/Export/ReportIndex';
                });
            }
            e.stopImmediatePropagation();
        });
    });

    function toggleEventStatus(anchor) {
        debug;
        var div = $(anchor).children('div.corp-status');
        if (div.hasClass("sensorStatusOK")) {
            $.get("/Export/SetActive", { "id": $(anchor).data("id"), "active": false }, function (data) {
                if (data == "Success") {
                    div.addClass("sensorStatusInactive");
                    div.removeClass("sensorStatusOK");
                }
            });
        }
        else {
            $.get("/Export/SetActive", { "id": $(anchor).data("id"), "active": true }, function (data) {
                if (data == "Success") {
                    div.addClass("sensorStatusOK");
                    div.removeClass("sensorStatusInactive");
                }
            });
        }
    }

</script>

<style>
    .viewSensorDetails svg, #svg_delete {
        height: 60px;
        width: 60px;
        fill: #666!important;
        padding: 5px;
    }
</style>
