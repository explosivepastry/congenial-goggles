<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<TranslationSearchModel>>" %>

<div class="x_content">

    <%Language lang = Language.Load((long)ViewBag.LanguageID);
      if (lang == null)
          lang = Language.Load(Language.EnglishID);%>

    <div class="gridPanel">
        <div class="row">
            <div class="bold col-md-4 col-sm-4 col-xs-12">
                <span style="font-weight: bold; font-size: 1.2em;">English</span>
            </div>
            <div class="bold col-md-8 col-sm-8 col-xs-12 lgbox">
                <span style="font-weight: bold; font-size: 1.2em;">(<%=lang.Name %>) Translation</span>
            </div>
        </div>
        <div class="clearfix"></div>
        <br />
    </div>
    <%if (Model.Count < 1)
      {
    %>

    <div class="gridPanel">
        <div class="row">
            <div class="bold col-md-2 col-sm-2 col-xs-12">
            </div>
            <div class="bold col-md-10 col-sm-10 col-xs-12">
                                        <%: Html.TranslateTag("Settings/AdminTranslate|No translations found","No translations found")%>.
            </div>
        </div>
        <div class="clearfix"></div>
        <br />
    </div>
    <%}
      else
      { %>

    <%
          string location = "";
          string toBeTranslated = "";
          foreach (TranslationSearchModel link in Model)
          {
              if (link.TagIDName.Split('|').Length < 2)
              {
                  toBeTranslated = link.TagIDName;
                  location = "Single Word";
                  //Exception Ex = new Exception();
                  // Ex.Log("Incorrect Translation format: ID" + link.TranslationLanguageLinkID + " Text: " + link.TagIDName);
              }
              else
              {
                  location = link.TagIDName.Split('|')[0];
                  toBeTranslated = link.TagIDName.Split('|')[1];
              }
    %>

    <div class="gridPanel col-12">
        <div class="row">
            <div class="col-md-6 col-sm-4 col-12" style="border-bottom:1px dotted gray;">
                <span title="<%=location %>"><%=toBeTranslated %></span>
            </div>
            <div class="col-md-6 col-sm-8 col-12 lgbox d-flex mb-2">
                <div class="input-group">
                    <input type="text" class="form-control" name="textValue_<%=link.TranslationLanguageLinkID %>" id="textValue_<%=link.TranslationLanguageLinkID %>" value="<%=link.Text %>" />
                    <input id="entry_<%=link.TranslationLanguageLinkID %>" class="updateEntry btn btn-secondary" type="button" value="Update Entry" />
                </div>
                <span style="color: red;" class="message" id="message_<%=link.TranslationLanguageLinkID %>"></span>
            </div>
        </div>
        <div class="clearfix"></div>
        <br />
    </div>


    <%  } %>

    <%} %>
</div>

<script type="text/javascript">
    $(document).ready(function () {


        $('.updateEntry').click(function (e) {
            e.preventDefault();
            var tllID = $(this).attr("id").split('_')[1]
            var updatedText = $('#textValue_' + tllID).val();

            $.post("/Settings/UpdateTranslationString", { id: tllID, text: updatedText }, function (data) {
                $('#message_' + tllID).html(data);
                setTimeout(clearMessage, 5000);
            });

        });


    });


    function clearMessage() {

        $('.message').html('');
    }

</script>
