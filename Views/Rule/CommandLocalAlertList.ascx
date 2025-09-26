<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Sensor>>" %>

<div class="cool-grid-col-2">
    <form id="sendCommandPageForm">

        <div class="choose-task">
            <div class="control-unit_container w-100">
                <div class="card_container__top ccu-custom">
                    <div class="card_container__top__title">
                        <%: Html.TranslateTag("Command Local Alert")%>
                    </div>
                </div>

                <div class="cla-box" id="localAlertText">
                    <div class="">
                        <div class="form-group" >
                            <div class="rule-title msg-title-alert" >
                                <label for="message"><%: Html.TranslateTag("Rule/SendNotification|Display Message (63 max)")%>: </label>
                                     </div>
                            <div class="local-alert-form">
                                
                                <%if (MonnitSession.AccountCan("text_override"))
                                    {%>
                                <a class="helpIco" data-bs-toggle="modal" data-bs-target=".overrideValues" title="<%: Html.TranslateTag("Rule/SendNotification|Merge Fields", "Merge Fields")%>" style="cursor: pointer; max-width:400px; justify-content: end; display:flex;">
                                    <p class="merge-btn">Merge Fields</p>
                                </a>
                           
                                <%} %>
                                <textarea maxlength="63" placeholder="<%: Html.TranslateTag("Rule/SendNotification|Local Alert message text")%>" id="LocalAlertText" class="form-control alert-txt user-dets" name="LocalAlertText" style="min-height:6rem;"><%=ViewBag.LocalAlertText%></textarea>

                            <a href="#" class="overridePreview alert-preview" data-type="Text" data-bs-toggle="modal" data-bs-target=".previewOverride"><strong><%= Html.TranslateTag("Message Preview") %></strong></a>
                            </div>

                        </div>
                    </div>
                    <div class="col-12 col-md-6">
                        <div id="textAreaError" class="form-group" style="margin-top: 1em; font-weight: bold; color: green;">
                        </div>
                    </div>
                </div>

                <!-- Help Button Override Values -->
                <div class="modal fade overrideValues" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered tValues_modal">
                        <%=Html.Partial("_MergeFieldModalBody") %>
                    </div>
                </div>

                <!-- Help Button Override Preview -->
                <div class="modal fade previewOverride" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered tValues_modal">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="modal"><%: Html.TranslateTag("Rule/SendNotification|Message Preview","Message Preview")%></h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body " id="previewOverrideBody"></div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-bs-dismiss="modal"><%: Html.TranslateTag("Close", "Close")%></button>
                            </div>

                        </div>
                    </div>
                </div>



                <div class="command-thermo_container " >
                    <div class="control-search-header">
               

                        <div class="container111">
                            <input class="nosubmit form-control user-dets" id="filter" type="search" placeholder="<%: Html.TranslateTag("Rule/SendCommand|Device Name","Device Name")%>">
                        </div>
                    </div>

                    <div class="control-unit_grid" id="deviceList">

                        <%  foreach (var device in Model)
                            {%>
                        <div filter="<%: device.SensorName.ToLower() %>" class="deviceHolder" id="localAlertDevice_<%=device.SensorID %>">
                            <%=Html.Partial("CommandLocalAlertDetails",device) %>
                        </div>
                        <%} %>
                    </div>
                   
                </div>
                 <div class="ccu-button">
                     <button id="go-back" type="button" class=" user-dets btn btn-secondary" " style="cursor: pointer;" title="<%=Html.TranslateTag("Back") %>"><%: Html.TranslateTag("Back")%></button>
                        <button type="button" class=" user-dets btn btn-primary" id="saveLocalAlertTextBtn"><%: Html.TranslateTag("Save")%></button>
                    </div>
            </div>
        </div>
    </form>
</div>
 <input type="hidden" id="scroller" />

<script type="text/javascript">

    var notAllowedString = '<%: Html.TranslateTag("Character not allowed")%>';
    var sensorID;

    $(document).ready(function () {

        $('#filter').keyup(function (e) {
            e.preventDefault();
            $('.deviceHolder').hide();
            let query = $('#filter').val().toLowerCase();
            if (query.length > 0) {
                $('.deviceHolder[filter*=' + query + ']').show();
            } else {
                $('.deviceHolder').show();
            }
        });


        document.getElementById("LocalAlertText").addEventListener('keypress', function (e) {
            if (e.keyCode < 32 || //Anything less than 32 is invalid
                (e.keyCode > 90 && e.keyCode < 97) || //Anything between 90-97 is invalid ([, \, ], ^, _, and `)
                (e.keyCode > 122 && e.keyCode != 167 && e.keyCode != 248)// anything larger than 122 (with exception of 167 : § and 248 : ø) is invalid
            ) {
                $('#textAreaError').html("");
                $('#textAreaError').html(notAllowedString + ": " + e.key);
                e.preventDefault();
            }

        });

        $('.overridePreview').click(function (e) {
            e.preventDefault();
            var msg = $("#LocalAlertText").val();;
            var obj = $('#previewOverrideBody');
            obj.html("content/css/myloader.ascx");
            $.post('/Rule/NotifyBodyMsgPreview/', { msg: msg }, function (data) {
                obj.html(data);
            });
        });

    });

    function showDelay(sensID) {
        sensorID = Number(sensID);
    }

    function setLocalAlertVal(divToCheck, id) {
        if ($('#' + divToCheck + id).prop('checked')) {
            $('#' + divToCheck + id).removeAttr('checked');
            $('#' + divToCheck + id).val(false);
        } else {
            $('#' + divToCheck + id).attr('checked', 'checked');
            $('#' + divToCheck + id).val(true);
        }
    }


    function toggleLocalAlertDiv(deviceID, doHide) {

        if (doHide) {
            $('#caretClose_' + deviceID).css("display", "none");
            $('#caretOpen_' + deviceID).css("display", "block");
            $('#card_' + deviceID).css("height", "57px");
        } else {
            $('#caretOpen_' + deviceID).css("display", "none");
            $('#caretClose_' + deviceID).css("display", "block");
            $('#card_' + deviceID).css("height", "auto");
        }
    }

</script>
