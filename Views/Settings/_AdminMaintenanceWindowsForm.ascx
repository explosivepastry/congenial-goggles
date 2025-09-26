<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.MaintenanceWindow>" %>

<%
    string translateDisplayDate = Html.TranslateTag("Settings/_AdminMaintenanceWindowsForm|Start sending notifications", "Start sending notifications");
    string translateStartDate = Html.TranslateTag("Settings/_AdminMaintenanceWindowsForm|Start of Maintenance", "Start of Maintenance");
    string translateEndDate = Html.TranslateTag("Settings/_AdminMaintenanceWindowsForm|End of Maintenance", "End of Maintenance");

    DateTime utcDisplayDate = Model.DisplayDate == DateTime.MinValue ? DateTime.UtcNow.AddDays(1) : Model.DisplayDate;
    DateTime utcStartDate = Model.StartDate == DateTime.MinValue ? DateTime.UtcNow.AddDays(2) : Model.StartDate;
    DateTime utcEndDate = Model.HideDate == DateTime.MinValue ? DateTime.UtcNow.AddDays(3) : Model.HideDate;

    // truncate seconds
    utcDisplayDate = new DateTime(utcDisplayDate.Year, utcDisplayDate.Month, utcDisplayDate.Day, utcDisplayDate.Hour, utcDisplayDate.Minute, 0);
    utcStartDate = new DateTime(utcStartDate.Year, utcStartDate.Month, utcStartDate.Day, utcStartDate.Hour, utcStartDate.Minute, 0);
    utcEndDate = new DateTime(utcEndDate.Year, utcEndDate.Month, utcEndDate.Day, utcEndDate.Hour, utcEndDate.Minute, 0);

    Monnit.TimeZone userTimeZone = Monnit.TimeZone.Load(MonnitSession.CurrentCustomer.Account.TimeZoneID);
    DateTime localizedDisplayDate = Monnit.TimeZone.GetLocalTime(utcDisplayDate, userTimeZone.Info);
    DateTime localizedStartDate = Monnit.TimeZone.GetLocalTime(utcStartDate, userTimeZone.Info);
    DateTime localizedEndDate = Monnit.TimeZone.GetLocalTime(utcEndDate, userTimeZone.Info);

    TimeZoneInfo estTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
    DateTime estDisplayDate = Monnit.TimeZone.GetLocalTime(utcDisplayDate, estTimeZoneInfo);
    DateTime estStartDate = Monnit.TimeZone.GetLocalTime(utcStartDate, estTimeZoneInfo);
    DateTime estEndDate = Monnit.TimeZone.GetLocalTime(utcEndDate, estTimeZoneInfo);

    TimeSpan localDiffToUTC = (utcDisplayDate - localizedDisplayDate);
    TimeSpan localDiffToEST = (estDisplayDate - localizedDisplayDate);

    if (MonnitSession.IsCurrentCustomerMonnitAdmin)
    {%>
<div class="switchFlexDirectionOnSM" style="display: flex; justify-content: space-evenly; margin: 2rem 0;">
    <div class="bold col-sm-2 col-12" style="margin-top: 1rem;">
        <%: translateDisplayDate %>
        <input id="displayDate" name="displayDate" class="form-control datePicker timeInputStyle" style="padding: .25rem" value="<%= localizedDisplayDate.ToString("M/d/yyyy h:mm tt")%>" />
        <div>
            UTC:
            <input id="displayDateUTC" name="displayDateUTC" class="timeInputStyle" value="<%: utcDisplayDate.ToString("M/d/yyyy h:mm tt") %>" />
        </div>
        <div>
            EST:
            <input id="displayDateEST" name="displayDateEST" class="timeInputStyle" value="<%: estDisplayDate.ToString("M/d/yyyy h:mm tt") %>" />
        </div>
    </div>
    <div class="bold col-sm-2 col-12" style="margin-top: 1rem;">
        <%: translateStartDate %>
        <input id="startDate" name="startDate" class="form-control datePicker timeInputStyle" style="padding: .25rem" value="<%= localizedStartDate.ToString("M/d/yyyy h:mm tt")%>" />
        <div>
            UTC:
            <input id="startDateUTC" name="startDateUTC" class="timeInputStyle" value="<%: utcStartDate.ToString("M/d/yyyy h:mm tt") %>" />
        </div>
        <div>
            EST:
            <input id="startDateEST" name="startDateEST" class="timeInputStyle" value="<%: estStartDate.ToString("M/d/yyyy h:mm tt") %>" />
        </div>
    </div>
    <div class="bold col-sm-2 col-12" style="margin-top: 1rem;">
        <%: translateEndDate %>
        <input id="endDate" name="endDate" class="form-control datePicker timeInputStyle" style="padding: .25rem" value="<%= localizedEndDate.ToString("M/d/yyyy h:mm tt")%>" />
        <div>
            UTC:
            <input id="endDateUTC" name="endDateUTC" class="timeInputStyle" value="<%: utcEndDate.ToString("M/d/yyyy h:mm tt") %>" />
        </div>
        <div>
            EST:
            <input id="endDateEST" name="endDateEST" class="timeInputStyle" value="<%: estEndDate.ToString("M/d/yyyy h:mm tt") %>" />
        </div>
    </div>
</div>

<%: Html.TranslateTag("Settings/_AdminMaintenanceWindowsForm|Severity Level","Severity Level")%>

<% var strings = SeverityLevel.LoadAll();%>
<select id="SeverityLevel" name="SeverityLevel" class="form-select">
    <%foreach (SeverityLevel severityLevel in SeverityLevel.LoadAll())
        { %>
    <option value="<%=severityLevel.SeverityLevelNumber%>" <%= Model.SeverityLevel == severityLevel.SeverityLevelNumber ? "selected='selected'" : "" %>>
        <%= Html.TranslateTag("Settings/_AdminMaintenanceWindowsForm|" + severityLevel.SeverityLevelLabel, severityLevel.SeverityLevelLabel) %>
    </option>
    <%} %>
</select>


<%} %>
<br />
<div>
    <div>
        <div style="display: flex;">
            <div class="bold aSettings__title" style="padding-left: 0; justify-content: center; width: 100%;">
                <%: Html.TranslateTag("Settings/_AdminMaintenanceWindowsForm|In app modal description (Default 8000 Characters)","In app modal description (Default 8000 Characters)")%>:
            </div>
            <div class="bold aSettings__title hideOnSmScreenz" style="padding-left: 0; justify-content: center; width: 100%;">
                <%: Html.TranslateTag("Settings/_AdminMaintenanceWindowsForm|Preview","Preview")%>:
            </div>
        </div>
        <div class="switchFlexDirectionOnSM" style="display: flex; gap: 0.5rem;">
            <textarea style="width: 100%; border-radius: 5px; min-height: 200px;"
                id="DescriptionHtml" class="card-text" onmouseup="updatePreviewOnTypeHandler(event,'DescriptionOutput')" onkeyup="updatePreviewOnTypeHandler(event,'DescriptionOutput')" contenteditable="true" placeholder="<%: Html.TranslateTag("Write HTML Source Code here...","Write HTML Source Code here")%>..."><%:Model.Description %></textarea>
            <div id="maintNotification" class="backdrop-note">

                <div class='maint-notify'>
                    <div class="maintIcon">
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
                            <path d="M78.6 5C69.1-2.4 55.6-1.5 47 7L7 47c-8.5 8.5-9.4 22-2.1 31.6l80 104c4.5 5.9 11.6 9.4 19 9.4h54.1l109 109c-14.7 29-10 65.4 14.3 89.6l112 112c12.5 12.5 32.8 12.5 45.3 0l64-64c12.5-12.5 12.5-32.8 0-45.3l-112-112c-24.2-24.2-60.6-29-89.6-14.3l-109-109V104c0-7.5-3.5-14.5-9.4-19L78.6 5zM19.9 396.1C7.2 408.8 0 426.1 0 444.1C0 481.6 30.4 512 67.9 512c18 0 35.3-7.2 48-19.9L233.7 374.3c-7.8-20.9-9-43.6-3.6-65.1l-61.7-61.7L19.9 396.1zM512 144c0-10.5-1.1-20.7-3.2-30.5c-2.4-11.2-16.1-14.1-24.2-6l-63.9 63.9c-3 3-7.1 4.7-11.3 4.7H352c-8.8 0-16-7.2-16-16V102.6c0-4.2 1.7-8.3 4.7-11.3l63.9-63.9c8.1-8.1 5.2-21.8-6-24.2C388.7 1.1 378.5 0 368 0C288.5 0 224 64.5 224 144l0 .8 85.3 85.3c36-9.1 75.8 .5 104 28.7L429 274.5c49-23 83-72.8 83-130.5zM104 432c0 13.3-10.7 24-24 24s-24-10.7-24-24s10.7-24 24-24s24 10.7 24 24z" />
                        </svg>
                    </div>
                    <div>
                        <div id="DescriptionOutput" class="content-text-maintenance">
                            <%:Html.Raw(Model.Description) %>
                        </div>
                    </div>
                    <div type="button" class="btn btn-primary">Acknowledge</div>
                </div>
            </div>
        </div>
        <br />
        <div class="prevreset" style="display: flex; justify-content: center; gap: 1rem;">
            <input type="button" class="previewHtml btn btn-sm btn-primary active" data-id="Description" value="<%: Html.TranslateTag("Preview","Preview")%>" title="<%: Html.TranslateTag("HTML Viewer","HTML Viewer")%>" />
            <input type="button" class="resetHtml btn btn-sm btn-secondary" data-id="Description" value="<%: Html.TranslateTag("Reset","Reset")%>" title="<%: Html.TranslateTag("Reset to Default HTML values from DB","Reset to Default HTML values from DB")%>" />
            <input type="button" class="clearHtml btn btn-sm btn-info" data-id="Description" value="<%: Html.TranslateTag("Clear","Clear")%>" title="<%: Html.TranslateTag("Clear Html Source Code","Clear Html Source Code")%>" />
        </div>

    </div>

    <%: Html.HiddenFor(model => model.Description)%>
</div>

<div>
    <div>
        <div style="display: flex;">
            <div class="bold aSettings__title" style="padding-left: 0; margin-top: 2rem; justify-content: center; width: 100%">
                <%: Html.TranslateTag("Settings/_AdminMaintenanceWindowsForm|Email Body (Default 8000 Characters)","Email Body (Default 8000 Characters)")%>:
            </div>
            <div class="bold aSettings__title hideOnSmScreenz" style="padding-left: 0; margin-top: 2rem; justify-content: center; width: 100%;">
                <%: Html.TranslateTag("Settings/_AdminMaintenanceWindowsForm|Preview","Preview")%>:
            </div>
        </div>
        <div class="switchFlexDirectionOnSM" style="display: flex; gap: 0.5rem;">
            <textarea style="width: 100%; min-height: 200px; border-radius: 5px;"
                id="EmailBodyHtml" class="card-text" onmouseup="updatePreviewOnTypeHandler(event,'EmailBodyOutput')" onkeyup="updatePreviewOnTypeHandler(event,'EmailBodyOutput')" contenteditable="true" placeholder="<%: Html.TranslateTag("Write HTML Source Code here...","Write HTML Source Code here")%>..."><%:Model.EmailBody %></textarea>
            <div id="" class="backdrop-note">
                <div class='emailPreviewWrapperAB' style="margin: 1rem;">
                    <div class="blueBarAB">
                    </div>
                    <div style="background: white; min-height: 150px">
                        <div style="display: flex; justify-content: center;">
                            <img style="padding: 2rem;" class="siteLogo2" src="/Overview/Logo">
                        </div>

                        <div id="EmailBodyOutput" style="padding: 2rem;" class=""><%:Html.Raw(Model.EmailBody) %></div>
                        <div id="emailFooter">
                            <div>
                                <img style="padding: 1rem; width: 150px;"
                                    class="siteLogo2" src="https://monnit.blob.core.windows.net/site/images/logos/logo-email-footer.png" alt="monnit footer logo">
                                <p style="margin-bottom: 0rem;">© All trademarks are property of Monnit</p>
                                <p style="margin-bottom: 0rem;">2009 - 2024 Monnit Corp.</p>
                                <p style="margin-bottom: 0rem;">All Rights Reserved.</p>
                                <br />
                                <p style="margin-bottom: 0rem;">
                                    Top opt out of future notifications click <span style="color: var(--primary-color); text-decoration: underline">here</span>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="prevreset" style="display: flex; justify-content: center; gap: 1rem;">
            <input type="button" class="previewHtml btn btn-sm btn-primary active" data-id="EmailBody" value="<%: Html.TranslateTag("Preview","Preview")%>" title="<%: Html.TranslateTag("HTML Viewer","HTML Viewer")%>" />
            <input type="button" class="resetHtml btn btn-sm btn-secondary" data-id="EmailBody" value="<%: Html.TranslateTag("Reset","Reset")%>" title="<%: Html.TranslateTag("Reset to Default HTML values from DB","Reset to Default HTML values from DB")%>" />
            <input type="button" class="clearHtml btn btn-sm btn-info" data-id="EmailBody" value="<%: Html.TranslateTag("Clear","Clear")%>" title="<%: Html.TranslateTag("Clear Html Source Code","Clear Html Source Code")%>" />
        </div>
        <%: Html.HiddenFor(model => model.EmailBody)%>
    </div>
</div>


<div class="clearfix"></div>
<div style="margin: 3rem; display: flex; justify-content: center; gap: 2rem;">
    <a style="width: 200px; margin-top: 1.5rem" href="/Settings/AdminMaintenanceWindows/" class="btn btn-light Cancel"><%: Html.TranslateTag("Cancel","Cancel")%></a>
    <button id="prefSave" type="submit" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary me-2" style="width: 200px; margin-top: 1.5rem">
        <%: Html.TranslateTag("Save","Save")%>
    </button>
</div>


<link href="/suneditor/suneditor.min.css" rel="stylesheet" />
<script type="text/javascript" src="/suneditor/suneditor.min.js"></script>


<style>
    .content-text-maintenance {
        padding: 1.25rem;
        text-align: justify;
    }

    .switchFlexDirectionOnSM {
        flex-direction: row;
    }

    #emailFooter div {
        display: flex;
        justify-content: center;
        background: #182C39;
        color: white;
        flex-direction: column;
        align-items: center;
        font-size: 0.65rem
    }

    .backdrop-note {
        background-color: #000000ab;
        display: flex;
        justify-content: center;
        align-items: center;
        min-height: 200px;
        width: 100%;
        border-radius: 5px;
    }

    .maint-notify {
        padding-bottom: 9px;
        width: clamp(305px, 58vw, 471px);
        border-radius: 5px;
        text-align: center;
        background-color: white;
        margin: 1rem;
    }

    .maintIcon {
        background: #EBEBF0;
        border-radius: 5px 5px 0px 0px;
        margin-bottom: 5px;
    }

        .maintIcon svg {
            fill: #e91820;
            width: 30px;
            height: 30px;
            margin: 10px;
        }

    .blueBarAB {
        height: 25px;
        background: var(--primary-color);
        border-radius: 5px 5px 0px 0px;
    }

    .emailPreviewWrapperAB {
        background: var(--primary-color);
        width: clamp(305px, 58vw, 471px);
    }

    .timeInputStyle {
        border: none;
        min-width: 200px;
        padding: .25rem 0rem;
    }

    .hideOnSmScreenz {
        display: flex;
    }

    @media screen and (max-width:730px) {
        .switchFlexDirectionOnSM {
            flex-direction: column;
        }

        .hideOnSmScreenz {
            display: none;
        }
    }
</style>


<script type="text/javascript">

    const updatePreviewOnTypeHandler = (event, valueToUpdate) => {
        if (!valueToUpdate) {
            console.error("updatePreviewOnTypeHandler takes two arguments. You forgot the second, valueToUpdate");
            return;
        }
        const textAreaToChange = document.querySelector(`#${valueToUpdate}`)
        textAreaToChange.innerHTML = event.target.value;
    }

    $('form').submit(function (e) {
        var descriptionHtml = $('#DescriptionHtml').val();
        var emailBodyHtml = $('#EmailBodyHtml').val();
        $('#Description').val(descriptionHtml);
        $('#EmailBody').val(emailBodyHtml);
    });

    var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';

    $(document).ready(function () {
        $('#displayDate').mobiscroll().datepicker({
            controls: ['calendar', 'time'],
            theme: "ios",
            select: 'date',
            display: popLocation,
            headerText: '<%=translateDisplayDate%>',

            onInit: function (event, inst) {
                inst.setVal(new Date('<%=localizedDisplayDate%>'));
            },

            onChange: function (event, inst) {
                var utcTime = new Date(inst.value);
                utcTime = new Date(utcTime.setMinutes(utcTime.getMinutes() + <%:localDiffToUTC.TotalMinutes%>));
                utcTime = utcTime.toLocaleString().replace(',', '');
                var estTime = new Date(inst.value);
                estTime = new Date(estTime.setMinutes(estTime.getMinutes() + <%:localDiffToEST.TotalMinutes%>));
                estTime = estTime.toLocaleString().replace(',', '');

                $('#displayDateUTC').val(utcTime);
                $('#displayDateEST').val(estTime);
            }
        });
        $('#startDate').mobiscroll().datepicker({
            controls: ['calendar', 'time'],
            theme: "ios",
            select: 'date',
            display: popLocation,
            headerText: '<%=translateStartDate%>',

            onInit: function (event, inst) {
                inst.setVal(new Date('<%=localizedStartDate%>'));
            },

            onChange: function (event, inst) {
                var utcTime = new Date(inst.value);
                utcTime = new Date(utcTime.setMinutes(utcTime.getMinutes() + <%:localDiffToUTC.TotalMinutes%>));
                utcTime = utcTime.toLocaleString().replace(',', '');
                var estTime = new Date(inst.value);
                estTime = new Date(estTime.setMinutes(estTime.getMinutes() + <%:localDiffToEST.TotalMinutes%>));
                estTime = estTime.toLocaleString().replace(',', '');

                $('#startDateUTC').val(utcTime);
                $('#startDateEST').val(estTime);
            }
        });
        $('#endDate').mobiscroll().datepicker({
            controls: ['calendar', 'time'],
            theme: "ios",
            select: 'date',
            display: popLocation,
            headerText: '<%=translateEndDate%>',

            onInit: function (event, inst) {
                inst.setVal(new Date('<%=localizedEndDate%>'));
            },

            onChange: function (event, inst) {
                var utcTime = new Date(inst.value);
                utcTime = new Date(utcTime.setMinutes(utcTime.getMinutes() + <%:localDiffToUTC.TotalMinutes%>));
                utcTime = utcTime.toLocaleString().replace(',', '');
                var estTime = new Date(inst.value);
                estTime = new Date(estTime.setMinutes(estTime.getMinutes() + <%:localDiffToEST.TotalMinutes%>));
                estTime = estTime.toLocaleString().replace(',', '');

                $('#endDateUTC').val(utcTime);
                $('#endDateEST').val(estTime);
            }
        });

        $('.sf-with-ul').removeClass('currentPage');
        $('#MenuMaint').addClass('currentPage');

        $('.previewHtml').click(function () {
            var id = $(this).attr('data-id');
            var input = $('#' + id + 'Html');
            var obj = $('#' + id + 'Output');
            obj.html(input.val());
        });
        $('.resetHtml').click(function () {
            var id = $(this).attr('data-id');
            var oldValue = $('#' + id).val();
            var obj = $('#' + id + 'Html');
            var output = $('#' + id + 'Output');
            obj.val(oldValue);
            output.html(oldValue);
        });
        $('.clearHtml').click(function () {
            var id = $(this).attr('data-id');
            var obj = $('#' + id + 'Html');
            var output = $('#' + id + 'Output');
            obj.val('');
            output.html('');
        });
    });
</script>
