<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<% 
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();
%>

<table>
    <tr>
        <td valign="top">
            <table>
                <!-- Top Left Table -->
                <tr>
                    <td width="295px" valign="bottom" class="panel1_top panel_title">
                        Account Information
                    </td>
                </tr>
                <tr>
                    <td class="panel1_mid panel_content">
                        <% if (MonnitSession.CurrentCustomer != null) { Html.RenderPartial("../Account/DetailsSmall", MonnitSession.CurrentCustomer.Account); } %>
                    </td>
                </tr>
                <tr>
                    <td class="panel1_btm" height="18px" style="background-repeat: no-repeat">
                    </td>
                </tr>
            </table>
            <div id="social_nav">
                <a href="http://blog.monnit.com" title="Visit the Monnit Blog" target="_blank"><img src="/content/images/social/blogger.png" /></a>&nbsp;
                <a href="http://www.facebook.com/pages/Monnit/244290499233" title="Visit us on Facebook" target="_blank"><img src="/content/images/social/facebook.png" /></a>&nbsp; 
                <a href="https://twitter.com/monnitsensors" title="Follow us on Twitter" target="_blank"><img src="/content/images/social/twitter.png" /></a>&nbsp; 
                <a href="http://www.linkedin.com/shareArticle?mini=true&url=http://www.monnit.com&title=Monnit - The leader in low cost wireless sensors&summary=Monnit brings you the ability to communicate with the &#147;things&#148; around you. Introducing low cost, durable wireless sensors and monitoring service.&source=monnit.com" title="Share this page on LinkedIn" target="_blank"><img src="/content/images/social/linkedin.png" /></a>
            </div>
        </td>
        <td valign="top">
            <iframe src="https://www.monnit.com/news-feed.php" scrolling="no" frameborder="0" style="border:none; overflow:hidden; width:310px; height:274px;" allowTransparency="true"></iframe>
            <%--<table>
                <!-- Top Left Table -->
                <tr>
                    <td width="291px" valign="bottom" class="panel2_top panel_title">
                        News / Social
                    </td>
                </tr>
                <tr>
                    <td class="panel2_mid panel_content">
                        <div class="threepanel">
                            <iframe src="https://www.facebook.com/plugins/likebox.php?id=244290499233&amp;width=288&amp;connections=0&amp;stream=true&amp;header=false&amp;height=395" scrolling="no" frameborder="0" style="border:none; overflow:hidden; width:288px; height:395px;" allowTransparency="true"></iframe>
                        </div>
                        <!-- threepanel -->
                    </td>
                </tr>
                <tr>
                    <td class="panel2_btm" height="18px" style="background-repeat: no-repeat">
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="social_nav">
                            <a href="http://blog.monnit.com" title="Visit the Monnit Blog" target="_blank">
                                <img src="/content/images/social/blogger.png" /></a>&nbsp; <a href="http://www.facebook.com/pages/Monnit/244290499233"
                                    title="Visit us on Facebook" target="_blank">
                                    <img src="/content/images/social/facebook.png" /></a>&nbsp; <a href="https://twitter.com/monnitsensors"
                                        title="Follow us on Twitter" target="_blank">
                                        <img src="/content/images/social/twitter.png" /></a>&nbsp; <a href="http://www.linkedin.com/shareArticle?mini=true&url=http://www.monnit.com&title=Monnit - The leader in low cost wireless sensors&summary=Monnit brings you the ability to communicate with the &#147;things&#148; around you. Introducing low cost, durable wireless sensors and monitoring service.&source=monnit.com"
                                            title="Share this page on LinkedIn" target="_blank">
                                            <img src="/content/images/social/linkedin.png" /></a>
                        </div>
                    </td>
                </tr>
            </table>--%>
        </td>
        <td valign="top">
            <table>
                <!-- Top Left Table -->
                <tr>
                    <td width="290px" valign="bottom">
                        <%--<a href="https://www.monnit.com/shop/" target="_blank"><img src="https://www.monnit.com/images/msphere-special.jpg" style="margin-top:-7px; margin-left:-5px;" /></a>--%>
                        <a href="http://www.monnit.com/newsletter/" target="_blank"><img src="https://www.monnit.com/images/imonnit-newsletter.jpg" style="margin-top:-7px; margin-left:-5px;" /></a>
                    </td> 
                </tr>
            </table>
        </td>
    </tr>
</table>
