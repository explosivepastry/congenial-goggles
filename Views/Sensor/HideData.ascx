<%@ Control Language="C#" %>
<% 
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();

    
%>

<div id="PrintArea" style="background-color: White;">
    <div class="formtitle">
        HX Message Credits Consumed
        
    </div>

    <div class="formBody divNoDataContainer" >
        You have used all your HX Message Credits
    </div>

    <div class="buttons">
        <div class="clear"></div>
    </div>
</div>
