<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Cookie Policy
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div style="padding:25px;">

        <center><h1><%: Html.TranslateTag("SENSORCERT COOKIE POLICY") %></h1>

        <p>In operating this Site, we use a technology called "cookies." </p></center>

        <h2>What are cookies?</h2>

        <p style="margin-top:-15px;">A cookie is a piece of information saved in your computer by the server of the site you are visiting. They allow websites to store memory associated to the user such as preferences and information. Without cookies, the server will not recognize you when you return to a website.  Cookies set by the website owner (in this case, Monnit) are called “first party cookies.” Cookies set by parties other than the website owner are called “third party cookies.”</p>
        
        <h2>How does SensorCert use cookies?</h2>

        <p style="margin-top:-10px;">
            SensorCert uses first party cookies to help provide personalized functionality on the site.  SensorCert uses third party cookies in aggregate that allow us to analyze site usage and enable us to enhance key functionality of the site. In all cases in which we use cookies, we will not collect personal data through the use of such technology.
        </p>
        <br/>

        <h2>What cookies are saved in SensorCert's Website and Platform?</h2>

        <p style="margin-top:-15px;">
            SensorCert uses cookies with different duration settings to help our site enhance your user experience.  Types used are:
        </p>
            <p style="margin-top:-10px;">
                <b>Persistent cookies</b> - 
                Helps the site to remember your preferences and settings when you return to our site in the future. This results in a faster and convenient access.  These cookies are stored in the browser and persist between visits to the site.
            </p>
            <p style="margin-top:-10px;">
                <b>Session cookies</b> -
                Enables our site to keep track of your context from one page to another quickly without having to reprocess data. Session cookies are cleared when you or close your browser or shut down your computer.
            </p>
        <br/>

        <h2>When does SensorCert use cookies?</h2>
        
        <p style="margin-top:-10px;">
            <b>Registration</b> -
            We use cookies to remember your preferences when you use applications from the site. These saved items remain as your default settings when you return to our website. If you have not selected "Remember me?", during login, your cookies will be deleted at the end of the session.
        </p>
        <p style="margin-top:-10px;">
            <b>Site Performance</b> - When you access the site, cookies are generated that let help maintain session level context. SensorCert uses these cookies to cache recently accessed information and to increase speed of responses on subsequent requests.
        </p>
        <p style="margin-top:-10px;">
            <b>Analyze Site usage</b> - We use cookies to analyze the usage of our websites and mobile applications. This helps us improve user experience by learning how you interact with our content and identifying access errors.
        </p>
        <br/>

        <h2>Cookie Management</h2>
            <p style="margin-top:-10px;">
                On most web browsers, you will find a “help” section on the toolbar. Please refer to this section for information on how to receive notification when you are receiving a new cookie and how you may turn cookies off. We recommend that you leave cookies turned on because they allow you to take advantage of some of the site’s features.
            </p>

            <b>Cookie settings</b>:
        <p style="margin-top:-10px;">
            <br/>
                    <a href="https://support.microsoft.com/en-us/help/17442/windows-internet-explorer-delete-manage-cookies#ie=ie-10">Internet Explorer</a>
            <br/>
                    <a href="https://support.google.com/chrome/answer/95647?hl=en&ref_topic=14666">Google Chrome</a>
            <br/>
                    <a href="https://support.mozilla.com/en-US/kb/cookies-information-websites-store-on-your-computer?redirectlocale=en-US&redirectslug=Cookies">Mozilla Firefox</a>
            <br/>
                    <a href="https://support.apple.com/kb/PH17191?locale=en_US">Safari</a>
            <br/>
                    <a href="https://support.apple.com/en-us/HT201265">iOS Device</a>
            <br/>
                    <a href="https://support.google.com/nexus/answer/54068?visit_id=1-636598556453354517-3959018307&hl=en&rd=1">Android Device</a>
        </p>
        <br/>

        <h2>SensorCert Terms of Use</h2>
        <p style="margin-top:-10px;">
            <a href="/Account/Legal" target="_blank">Terms of Use</a>
        </p>
        <h2>Effective Date: May 25, 2018</h2>
    </div>



</asp:Content>
