<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>


<div class="btn_container">
    <div class="btn-1">
      
    </div>
    <div class="btn-2">
        <button type="submit" value="<%: Html.TranslateTag("Next", "Next")%>" class="btn btn-primary btn-next"><%: Html.TranslateTag("Next", "Next")%></button>
    </div>
</div>

<%--<div class="clearfix"></div>
<div class="row">
    <div class="col-12">
        <div class="buttons text-end">
          
            <button type="submit" value="<%: Html.TranslateTag("Next", "Next")%>" class="btn btn-primary">Next</button>
            <div style="clear: both;"></div>
        </div>
    </div>
</div>--%>
