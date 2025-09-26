<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

  <%  eCOGasDisplay Display = eCOGasDisplay.Concentration;
      if (ViewData["Display"] != null)
      {
          Display = (eCOGasDisplay)ViewData["Display"];
      }
      else
      {
          Display = Gas_CO.GetDisplay(Model.SensorID);
      }
    %>
        <tr>
            <td style="width: 250px;">Display Mode</td>
            <td>
                <%:Html.DropDownList<eCOGasDisplay>("Display",Display) %>
                <script>
                    $('#Display').addClass('editField editFieldMedium');
                    $(function () {
                        $('#Display').change(function () {
                            $('.Thres34').hide();
                            $('.Thres34_' + $('#Display').val()).show();
                        });

                        $('.Thres34').hide();
                        $('.Thres34_' + $('#Display').val()).show();
                    });
                </script>
            </td>
            <td>
                <img alt="help" class="helpIcon" title="Sets the display mode of the data.<br /> Ability to set thresholds available only for the displayed values" src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
        </tr>