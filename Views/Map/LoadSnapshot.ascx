<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Monnit.VisualMapSensor>>" %>

<% foreach (var item in Model)
   { %>

<div id="sid<%: item.SensorID%>" class="tooltipDiv_<%:item.SensorID%>" style="position: absolute;">
    <% %><div class="tiptip_content" href="/Sensor/DetailsSmallOneview/<%: item.SensorID%>">
        <%--<%Html.RenderPartial("../Sensor/DetailsSmall", item); %>--%>
        <%--We Changed this because by calling the url it will adapt to sensor specific and theme overriden pages--%>
    </div>
</div>

<% } %>
<script>
    $(document).ready(function () {        
            <%foreach (var item in Model)
              {%>
        setPosition(<%:item.SensorID%>);
            <%}%>
    });

    function setPosition(id) {
        var position = $('#' + id + ':visible').position();
        if (position) {
            var tipx = position.left;
            var tipy = position.top;
            var tip = $('.tooltipDiv_' + id);

            $('.tooltipDiv_' + id).html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);

            tip.css({ top: tipy + 18, left: tipx + 6, position: 'absolute' });
            tip.show().css({ opacity: 1 });

            $.get("/Sensor/DetailsSmallOneview/" + id, function (data) {
                $('.tooltipDiv_' + id).html(data);
                $('.tooltipDiv_' + id).addClass('tip');
            });
        }

        $('.tiptip_content').each(function (i, e) {
            var toolTip = $(this);
            $('#overlayTooltip').show();
            toolTip.hide();           
        });
    }
</script>

