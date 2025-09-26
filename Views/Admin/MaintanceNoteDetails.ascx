<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Monnit.AccountThemeMaintenanceLink>>" %>

<script type="text/javascript">
    $(document).ready(function () {
        $(".datepicker").datepicker();

        $('.sf-with-ul').removeClass('currentPage');
        $('#MenuMaint').addClass('currentPage');

        sunEmailObjEditor = null;
        sunObjEditor = null;
    });
    var sunObjEditor;
    var sunEmailObjEditor;
    function Dictionary() {
        var dictionary = {};

        this.setData = function (key, val) { dictionary[key] = val; }
        this.getData = function (key) { return dictionary[key]; }
    }
    var objEditorDic = new Dictionary();
    var objEmailDic = new Dictionary();

    function submittingForm(form, id) {
        $('#overridennote' + id).val(objEditorDic.getData(id).getContents());
        $('#overriddenEmailBody' + id).val(objEmailDic.getData(id).getContents());

        postForm(form);
    }

    function DeleteLink(MaintenanceID, AccountThemeID) {
        $.post("/Admin/DeleteMaintenanceLink", { maintenanceID: MaintenanceID, accountthemeID: AccountThemeID }, function (data) {
            showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
            location.reload();
        });
    }


</script>


<%foreach (var item in Model)
{ %>
    <form action="/Admin/MaintanceNoteDetails" method="post">
    <% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin || (item.AccountThemeID == MonnitSession.CurrentTheme.AccountThemeID /*&& MonnitSession.IsCurrentCustomerReseller*/))
        {
            AccountTheme ItemTheme = Monnit.AccountTheme.Load(item.AccountThemeID);
        %>
        <div style="border: 1px solid black;">
            <div class="formtitle">
                <%: Html.Label(ItemTheme.Theme, new { @style = "font-size:20pt;" })%>
                <%: ItemTheme.Domain%>
            </div>
            <div class="formbody">
                <%: Html.Hidden("maintanceid",item.MaintenanceWindowID)%>
                <%: Html.Hidden("accountthemeid",item.AccountThemeID)%>
                <div class="editor-label" style="margin-left: 32px;">
                    <textarea id="editor<%: item.AccountThemeID%>"><%:item.OverriddenNote %></textarea>
                    <input type="hidden" name="overridennote" value="<%:item.OverriddenNote%>" id="overridennote<%: item.AccountThemeID%>" />
                </div>

                <div class="editor-label" style="margin-left: 32px;">
                    <textarea id="emailBody<%: item.AccountThemeID%>"><%:item.OverriddenEmail %></textarea>
                    <input type="hidden" name="overriddenEmailBody" value="<%:item.OverriddenEmail%>" id="overriddenEmailBody<%: item.AccountThemeID%>" />
                </div>

     
            </div>
            <div class="buttons">
                <input type="button" value="Save" style="margin-right: 306px;" onclick="submittingForm($(this).closest('form'),<%: item.AccountThemeID%>)" title="Save" class="bluebutton submitted " />
                <input type="button" value="Delete" class="greybutton deleted" onclick="DeleteLink(<%: item.MaintenanceWindowID%>,<%: item.AccountThemeID%>)" />
                <div style="clear: both"></div>
            </div>
        </div>
     <%} %>

        <script>
            $(function () {
                var id = <%: item.AccountThemeID%>;
                $('#editor' + id).before("<span>Description (Default 8000 Characters): </span>");
                $('#emailBody' + id).before("<span>Email Body (Default 8000 Characters): </span>");
                $('#smsEditor' + id).html("<span>SMS Description (Default 120 Characters): </span>");


                objEditorDic.setData(id, createSunEditor('editor' + id));
                objEmailDic.setData(id, createSunEditor('emailBody' + id));
            });
        </script>
    </form>
<%} %>