<%@ Page Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.ChangeLog>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%  
        Customer cust = MonnitSession.CurrentCustomer;
        string accountTheme = (ViewBag.themeID > 0) ? ViewBag.themeID.ToString() : "";
        string[] prefArray = cust.Preferences.Values.ToArray();
        string prefDate = prefArray[0].ToLower();

    %>
    <div class="container-fluid">
        <a href="/settings/ReleaseNotesIndex" class="btn btn-primary mt-3">
            <%=Html.GetThemedSVG("cardList") %>
            <span class="ms1"><%: Html.TranslateTag("Release Notes","Release Notes")%></span>
        </a>
        <div class="x_panel col-md-12 shadow-sm rounded mb-4 mt-2">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <%: Html.TranslateTag("Add Release Note","Add Release Note")%>
                </div>
            </div>

            <form id="ChangeLogForm">
                <input type="hidden" name="ChangeLogID" value="<%:Model != null ? Model.ChangeLogID : long.MinValue %>" />

                <div class="row sensorEditForm">
                    <div class="col-12 col-md-3">
                        <%: Html.TranslateTag("Release Date","Release Date")%>
                    </div>
                    <div class="col sensorEditFormInput">
                        <input onchange="$('#date').text($(this).val())" id="datePickMobi" name="ReleaseDate" class="form-control" value="<%:Model != null ? Model.ReleaseDate : DateTime.Today %>" />
                    </div>
                </div>

                <div class="row sensorEditForm">
                    <div class="col-12 col-md-3">
                        <%: Html.TranslateTag("Version","Version")%>
                    </div>
                    <div class="col sensorEditFormInput">
                        <input required onkeyup="$('#version').text($(this).val())" class="form-control" id="Version" name="Version" value="<%:Model != null ? Model.Version : "" %>" />
                    </div>
                </div>

                <div class="row sensorEditForm">
                    <div class="col-12 col-md-3">
                        <%: Html.TranslateTag("Published","Published")%>
                    </div>
                    <div class="col sensorEditFormInput">
                        <div class="form-check form-switch d-flex align-items-center ps-0">
                            <input class="form-check-input my-0 ms-0" type="checkbox" id="isPublished" <%--onclick="publishNote(<%:Model.ChangeLogID %>)"--%> name="isPublished" <%:Model.isPublished ? "checked" : "" %> />
                        </div>
                    </div>
                </div>

                <div class="text-end">
                    <button type="submit" class="btn btn-primary"><%:Model.ChangeLogID > 0 ? "Save" : "Add Release"%></button>
                </div>
            </form>

            <% if (Model.ChangeLogID > 0)
                     { %>
            <hr />
            <form id="ChangeLogItemForm">
                <div class="row sensorEditForm">
                    <div class="col-12 col-md-3">
                        <%: Html.TranslateTag("Note Type", "Note Type")%>
                    </div>
                    <div class="col sensorEditFormInput">
                        <select required class="form-select" id="Type" name="Type">
                            <option disabled selected value=""><%: Html.TranslateTag("Settings/ReleaseNotesEdit|-- select an option --","-- select an option --")%></option>
                            <option><%: Html.TranslateTag("Settings/ReleaseNotesEdit|Fix","Fix")%></option>
                            <option><%: Html.TranslateTag("Settings/ReleaseNotesEdit|Improved","Improved")%></option>
                            <option><%: Html.TranslateTag("Settings/ReleaseNotesEdit|New","New")%></option>
                            <option><%: Html.TranslateTag("Settings/ReleaseNotesEdit|Updated","Updated")%></option>
                        </select>
                    </div>
                </div>

                <div class="row sensorEditForm">
                    <div class="col-12 col-md-3">
                        <%: Html.TranslateTag("Heading Type", "Heading Type")%>
                    </div>
                    <div class="col sensorEditFormInput">
                        <select required class="form-select" id="Heading" name="Heading">
                            <option disabled selected value=""><%: Html.TranslateTag("Settings/ReleaseNotesEdit|-- select an option --","-- select an option --")%></option>
                            <option><%: Html.TranslateTag("Settings/ReleaseNotesEdit|iMonnit","iMonnit")%></option>
                            <option><%: Html.TranslateTag("Settings/ReleaseNotesEdit|Sensors","Sensors")%></option>
                            <option><%: Html.TranslateTag("Settings/ReleaseNotesEdit|Gateways","Gateways")%></option>
                        </select>
                    </div>
                </div>

                <input hidden id="ChangeLogItemID" type="text" name="ChangeLogItemID" value="" />
                <input hidden id="ChangeLogID" type="text" name="id" value="<%:ViewBag.ChangeLogID %>" />

                <div class="row sensorEditForm">
                    <div class="col-12 col-md-3">
                        <%: Html.TranslateTag("Note", "Note")%>
                    </div>
                    <div class="col" style="max-width: 600px;">
                        <textarea id="editor"></textarea>
                    </div>
                </div>
                <div class="row sensorEditForm" id="errorAlert" style="display: none;">
                    <div class="col-12 col-md-3">
                    </div>
                    <div class="col" style="max-width: 600px;">
                        <div class="alert alert-danger" role="alert">
                            <div id="error"></div>
                        </div>
                    </div>
                </div>

                <div class="text-end">
                    <button type="submit" class="btn btn-primary"><%: Html.TranslateTag("Settings/ReleaseNotesEdit|Add Note","Add Note")%></button>
                </div>
            </form>
            <% } %>
        </div>

        <div class="mt-4" id="noteList">
            <%Html.RenderPartial("_ChangeLogsList", Model); %>
        </div>
    </div>

    
    <link href="/suneditor/suneditor.min.css" rel="stylesheet" />
    <script type="text/javascript" src="/suneditor/suneditor.min.js"></script>

    
    <script type="text/javascript">
        var sunObjEditor = null;
        var prefDate = '<%= prefDate %>';
        var dFormat = prefDate;
        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';

        $('#datePickMobi').mobiscroll().datepicker({
            theme: 'ios',
            controls: ['calendar'],
            display: popLocation,
            dateFormat: dFormat.toUpperCase(),
        });

        <% if (Model.ChangeLogID > 0) { %>
            sunObjEditor = createSunEditor('editor', true);
 
        <% } %>

        const editNote = (id, heading) => {
            $('#ChangeLogItemID').val(id);
            $('#Type').val($(`#type_${id}`).text());
            $('#Heading').val(heading);
            sunObjEditor.setContents($(`#details_${id}`).html());
        }

        $("#ChangeLogForm").submit(function (event) {
            event.preventDefault();
            let form = $('#ChangeLogForm').serialize();

            $.ajax({
                type: 'POST',
                url: '/Settings/ReleaseNotesForm',
                data: form,
                success: function (returnVal) {
                    if (returnVal.includes("failed")) {
                        $('#errorAlert').show();
                        $('#error').text(returnVal);
                    }
                    else {
                        window.location.href = `/Settings/ReleaseNotesEdit/${returnVal}`;
                    }
                }
            });
        });

        $("#ChangeLogItemForm").submit(function (event) {
            event.preventDefault();
            let form = $('#ChangeLogItemForm').serialize();

            $.ajax({
                type: 'POST',
                url: '/Settings/ReleaseNoteItemForm',
                data: `${form}&Details=${sunObjEditor.getContents()}`,
                success: function (returnVal) {
                    if (returnVal.includes("failed")) {
                        console.log('here');
                        $('#errorAlert').show();
                        $('#error').text(returnVal);
                    }
                    else {
                        sunObjEditor.setContents("");
                        $('#noteList').html(returnVal);
                        $('#Type').val('');
                        $('#Heading').val('');
                        $('#ChangeLogItemID').val("");
                        $('#ChangeLogID').val('<%:ViewBag.ChangeLogID%>');
                        $('#errorAlert').hide();
                        $('#error').text();
                    }
                }
            });
        });

        const removeNote = (id) => {
            let values = {};
            values.url = `/Settings/RemoveReleaseNoteItem/${id}`;
            values.text = 'Are you sure you want to remove this note?';
            values.partialTag = $('#noteList')
            openConfirm(values);
        }


    </script>

    <style type="text/css">
        #svg_cardList g {
            stroke: #fff;
        }
    </style>

</asp:Content>
