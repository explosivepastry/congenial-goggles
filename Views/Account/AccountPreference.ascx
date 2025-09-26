<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<PreferenceType>>" %>

<div id="fullForm" style="width: 100%;">
    <div class="right_col" role="main" style="margin-left: 30px">
        <div class="">
            <div class="clearfix"></div>
            <div class="row">
                <div class="col-md-6 col-12">
                    <div class="x_panel">
                        <div class="x_title">
                            <h1>Account Preferences</h1>
                            <div class="clearfix"></div>
                            <hr />
                            <form id="prefForm" class="form-horizontal form-label-left" method="post">
                                <% foreach (var item in Model)
                                   {
                                       string val = "";
                                       Preference pref = Preference.LoadByPreferenceTypeIDandAccountID(item.PreferenceTypeID, MonnitSession.CurrentCustomer.AccountID);
                                       if (pref != null) val = pref.Value;%>
                                <div class="x_content">

                                    <h4><%: item.Name %></h4>
                                    <div class="col-md-3 col-sm-3 col-6">
                                        <select id="<%:item.Name %>" name="<%:item.Name %>">
                                            <%foreach (PreferenceTypeOption opt in PreferenceTypeOption.LoadByPreferenceTypeID(item.PreferenceTypeID))
                                              { %>
                                            <option value="<%=opt.Value == null ? item.DefaultValue : opt.Value %>" <%= opt.Value == val ?"selected='selected'":"" %>><%=opt.Name %></option>
                                            <%} %>
                                        </select>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div style="clear: both;"></div>
                                <%} %>

                                  <a onclick="goback();" style="float:left;" class="greybutton">Cancel</a>
                                <button id="prefSave" style="float:left;margin-left:100px;" type="submit" value="Save" class="gen-btn">Save</button>
                            </form>

                        </div>
                        <div style="clear: both;"></div>
                    </div>


                </div>
            </div>
        </div>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {

        $('#prefSave').click(function (e) {
            $.post('/Account/EditPreference/<%=ViewBag.accountID%>', $("#prefForm").serialize(), function (data) {
                showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                    window.location.reload();

                });

            });


    });

    function goback() {

        window.history.back();
    }

</script>
