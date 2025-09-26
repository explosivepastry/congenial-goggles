<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<style>
    /* TODO: Temporary styles, doesn't look like we have styles defined for .btn-active/inactive, fix before releasing */
    .btn a, .btn a i {
        color: white;
    }

    .Tab-Btns1 {
        background: #0067AB;
        color: #FFFFFF;
    }

        .Tab-Btns1:hover {
            transition: .2s ease;
            color: #FFFFFF;
            background: #2699FB;
            border-color: #2699FB;
        }

    .btn-inactive {
        background: #6C757D;
        color: white;
    }

        .btn-inactive:hover {
            transition: .2s ease;
            color: white;
            background: #5B9BD5;
            border-color: #5B9BD5;
        }
</style>

<br />
<br />


<div class="clearfix"></div>

<!-- Tab Section -->
<div id="deviceInfoTabs" class="row" data-id="<%= Model.GatewayID %>">
    <ul class="nav nav-tabs " role="tablist" style="margin: 10px 0; justify-content: space-evenly; align-items: center;">
        <li class="nav-item" style="margin: 10px 0;">
            <div class="btn-group">
                <button type="button" class=" Tab-Btns1 TabGtwHist innerTabToggle btn btn-active" data-id="gatewayHistoryTab" data-toggleclass="TabGtwHist innerTabToggle" data-togglepane="innerTab.tab-pane">
                    <a class="active" id="gatewayHistoryTab-tab" data-toggle="tab" role="tab" aria-controls="gatewayHistoryTab" aria-selected="true" title="Gateway History">Gateway History &nbsp; 
                        <i class="fa fa-history" aria-hidden="true"></i>
                    </a>
                </button>
            </div>
        </li>

        <li class="nav-item">
            <div class="btn-group">
                <button type="button" class="Tab-Btns1 TabGtwEdit innerTabToggle btn btn-inactive" data-id="gatewayEditTab" data-toggleclass="TabGtwEdit innerTabToggle" data-togglepane="innerTab.tab-pane">
                    <a class="active" id="gatewayEditTab-tab" data-toggle="tab" role="tab" aria-controls="gatewayEditTab" aria-selected="false" title="Edit Gateway">Edit &nbsp; 
                        <i class="fa fa-pencil-square-o" aria-hidden="true"></i>
                    </a>
                </button>
            </div>
        </li>

        <li class="nav-item">
            <div class="btn-group">
                <%if (Model.GatewayTypeID == 30)
                    { %>
                <button type="button" class="Tab-Btns1 TabGtwLocGPS innerTabToggle btn btn-inactive" data-id="gatewayLocGPSTab" data-toggleclass="TabGtwLocGPS innerTabToggle" data-togglepane="innerTab.tab-pane">
                    <a class="active" id="gatewayLocGPSTab-tab" data-toggle="tab" role="tab" aria-controls="gatewayLocGPSTab" aria-selected="false" title="GPS Location">GPS Location &nbsp;<%=Html.GetThemedSVG("gateway") %></a>
                </button>
                <%} %>
            </div>
        </li>
    </ul>

    <div class="tab-content">
        <div class="TabGtwHist innerTab tab-pane fade show active" id="gatewayHistoryTab" role="tabpanel" aria-labelledby="gatewayHistoryTab-tab">
        </div>
        <div class="TabGtwEdit innerTab tab-pane fade" id="gatewayEditTab" role="tabpanel" aria-labelledby="gatewayEditTab-tab">
        </div>
        <div class="TabGtwLocGPS innerTab tab-pane fade" id="gatewayLocGPSTab" role="tabpanel" aria-labelledby="gatewayLocGPSTab-tab">
        </div>

    </div>
</div>

<script>
    <%= ExtensionMethods.LabelPartialIfDebug("Testing_LoadGatewayDetails.aspx") %>

    function refreshDeviceStart() {
        $('#deviceInfoTabs .tab-content').hide();
        $('#deviceDetailsLoading').show();
    }

    function refreshDeviceEnd() {
        $('#deviceInfoTabs .tab-content').show();
        $('#deviceDetailsLoading').hide();
    }

    //var regreshGatewayHistoryTimeout;
    function refreshGatewayHistory(gatewayID) {

        //clearTimeout(regreshGatewayHistoryTimeout);

        $.get('/Testing/LoadGatewayHistory/' + gatewayID, function (data) {
            $('#gatewayHistoryTab').html(data);
            updateGatewayHistory(gatewayID);
            //if ($('#gatewayHistoryTab').is('.active.show')) {
            //    updateGatewayHistory(gatewayID);
                ////Upon 'refreshGatewayHistory()' being called once, will start to auto-refresh at 5 seconds
                //regreshGatewayHistoryTimeout = setTimeout(function () {
                //    refreshGatewayHistory(gatewayID)
                //}, 5 * 1000);
            //}
        });
    }

    var updateGatewayHistoryTimeout;
    function updateGatewayHistory(gatewayID) {

        clearTimeout(updateGatewayHistoryTimeout);

        var timestamp = $('#testingGatewayHistoryTable .testingHistoryRow')
            .filter(function () { return $(this).data('iconstring') == 'gateway' })
            .first()
            .attr('data-timestamp');
        
        if ($('#gatewayHistoryTab').is('.active.show') && timestamp) {
            
            $.get('/Testing/UpdateGatewayHistory/',
                {
                    id: gatewayID,
                    timestamp
                },
                function (data) {
                    $.each(data, (idx, row) => {
                        var r = $(row);
                        $('#testingGatewayHistoryTable').prepend(r);
                        flash(r);
                        $('#testingHistoryRecordCount').text(parseInt($('#testingHistoryRecordCount').text()) + 1)
                    });

                    //Upon 'updateGatewayHistory()' being called once, will start to auto-update at 5 seconds
                    updateGatewayHistoryTimeout = setTimeout(function () {
                        updateGatewayHistory(gatewayID)
                    }, 5 * 1000);
                }
            );
        } else {
            updateGatewayHistoryTimeout = setTimeout(function () {
                updateGatewayHistory(gatewayID)
            }, 5 * 1000);
        }
    }

    function flash(elem) {
        //var elem = $(elem);
        //let bgc = elem.css('background-color');
        //elem.css('background-color', 'rgba(123, 333, 111, 0.2)');
        elem.toggleClass('testingHistoryRowFade');

        setTimeout(() => {
            elem.toggleClass('testingHistoryRowFade');
        }, 1000);
    }

    function refreshGatewayEdit(gatewayID) {
        refreshDeviceStart();
        $.get('/Testing/LoadGatewayEdit/' + gatewayID, function (data) {
            $('#gatewayEditTab').html(data);
            refreshDeviceEnd();
        });
    }

    function refreshGatewayLocGPS(gatewayID) {
        refreshDeviceStart();
        $.get('/Gateway/Details/' + gatewayID, function (data) {
            $('#gatewayLocGPSTab').html(data);
            refreshDeviceEnd();
        });
    }

</script>
