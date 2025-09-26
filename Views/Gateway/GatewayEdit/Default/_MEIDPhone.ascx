<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_MEIDPhone|MEID","MEID")%>
    </div>
    <div class="col sensorEditFormInput" style="font-size:1.1em;">
        <%try
          {%>
        <%: Convert.ToInt64(Model.MacAddress.Split('|')[0]).ToString("X")%>
        <%}
          catch { }%>
    </div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_MEIDPhone|Phone","Phone")%>
    </div>
    <div class="col sensorEditFormInput" style="font-size:1.1em;">
        <%try
          {%>
        <%: Model.MacAddress.Split('|')[1].Insert(6, "-").Insert(3, ") ").Insert(0, "(")%>
        <%}
          catch { }%>
    </div>
</div>

