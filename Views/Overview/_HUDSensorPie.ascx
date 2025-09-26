<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.PreAggregatePageModel>" %>

<%
    int totalAware = 0;
    int totalCount = 0;
    string splitValue = "";
    if (Model.PreAggregateList.Count > 0)
    {
        splitValue = Model.PreAggregateList[0].SplitValue;
        totalAware = Model.PreAggregateList.Where(m => m.SplitValue == splitValue).Sum(m => m.AwareStateCounts).ToInt();
        totalCount = Model.PreAggregateList.Where(m => m.SplitValue == splitValue).Sum(m => m.SensorMessageCounts).ToInt();

    }
%>
<div class="x_title">
    <h2 style="overflow: unset; text-overflow: unset;"><%: Html.TranslateTag("Overview/SensorHud|Aware vs Not Aware","Aware vs Not Aware")%></h2>
    <div class="clearfix"></div>
</div>
<div class="x_content col-12 col-lg-12" style="align-content: center !important;">
    <%--    <div class="col-12 col-lg-12"></div>--%>

    <div class="" id="awarePieDiv" style="height: 150px; width: 250px; position: absolute;">
    </div>

</div>



<script type="text/javascript">

    $(document).ready(function () {

        var awareString = '<%: Html.TranslateTag("Aware","Aware")%>';
        var notAwareString = '<%: Html.TranslateTag("Not Aware","Not Aware")%>';

        var echartBar = echarts.init(document.getElementById('awarePieDiv'));

        echartBar.setOption({
            tooltip: {
                trigger: 'item',
                formatter: "{b} : {c} ({d}%)"
            },
            series: [
                {
                    type: 'pie',
                    startAngle: 45,
                    labelLine: {
                        normal: {
                            length: 3,
                            length2: 3,
                            show: '',
                        }
                    },
                    radius: '60%',
                    center: ['50%', '50%'],
                    selectedMode: 'single',
                    data: [
                        { value: <%=totalAware%>, name:awareString  },
                        { value: <%=totalCount - totalAware%>, name: notAwareString }
                    ],
                    itemStyle: {
                        emphasis: {
                            shadowBlur: 10,
                            shadowOffsetX: 0,
                            shadowColor: 'rgba(0, 0, 0, 0.5)'
                        }
                    },
                }
            
         
            ],
            //media: [
            //        {
            //            query: {
            //                maxWidth: 500               // when container width is smaller than 500
            //            },
            //            option: {
            //                series: [                   // top and bottom layout of two pie charts
            //                    {
            //                        radius: [20, '50%'],
            //                        center: ['50%', '30%']
            //                    },
            //                ]
            //            }
            //        }
            //]

        });



    });








</script>


