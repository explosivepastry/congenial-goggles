<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<SensorGroup>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ChartIndex
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <!-- Add/Help Buttons -->
    <div class="formtitle">
        <div id="MainTitle" style="display: none;"></div>
        <div style="height: 20px">
            <div class="col-xs-6" id="Div1" style="margin-top: 5px;">
                <a href="/Chart/ChartEdit/" id="list" class="btn btn-default btn-md" style="font-size: 1.0em; color: #515356; background-color: #f7f7f7; border: 0px; padding: 0px; margin: 0px; margin-left: 10px; margin-bottom: 5px;"><i class="fa fa-plus-square" style="font-size: 16px; color: #515356;"></i>&nbsp;<%: Html.TranslateTag("New Chart","New Chart")%></a>
            </div>
        </div>
        <!-- End Main Refresh -->

    </div>

    <%if (Model.Count == 0)
      {%>

    <div class="col-lg-12">
        <h2><%: Html.TranslateTag("Chart/ChartIndex|No charts currently created. Click \"New Chart \" to get started.","No charts currently created. Click \"New Chart\" to get started.")%></h2>
    </div>
    <%  }
      else
      {

          foreach (SensorGroup item in Model)
          { %>

    <div class="gridPanel chipHover">
        <table width="100%">
            <tr>
                <td width="50">
                    <div class="divCellCenter holder holderInactive">
                        <div class="sensor sensorIcon sensorStatusOK">
                            <i class="fa fa-area-chart" style="font-size: 1.8em;"></i>
                        </div>
                    </div>
                </td>

                <td valign="middle" style="padding: 0px;" class="chartName" onclick="location.href='/Chart/EditChart/<%=item.SensorGroupID %>">
                    <div class="glance-text">
                        <div class="glance-name"><%:item.Name%></div>
                    </div>
                </td>

                <td width="90" class="extra" style="text-align: center;">
                    <a href="/Chart/ChartEdit/<%=item.SensorGroupID %>">
                        <div class="gatewaySignal sigIcon" style="text-align: center;">
                            <i class="fa fa-edit" style="font-size: 1.8em; float: right;"></i>
                        </div>
                    </a>
                </td>
            </tr>
        </table>
    </div>
    <%}
      }%>
</asp:Content>
