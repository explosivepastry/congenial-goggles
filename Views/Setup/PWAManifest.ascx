<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.AccountTheme>" %>

<%
    string AppName = MonnitSession.CurrentStyle("MobileAppName");

    if (Model.AllowPWA && !string.IsNullOrEmpty(AppName) && AppName.Length > 0)//Default and web.config for now to make sure it doesn't pop up on white label portals
    { %>
{
   "name":"<%:AppName %>",
   "short_name":"<%:AppName.Substring(0, Math.Min(AppName.Length, 12)) %>",
   "id":"/",
   "start_url":"/Overview/",
   "display":"standalone",
   "prefer_related_applications":"true",
   "related_applications": [{
        "platform": "webapp",
        "url": "https://<%:Model.Domain %>/PWAManifest"
    }],
   "theme_color":"#000000",
   "background_color":"#ebebf0",
   "orientation": "portrait-primary",
   "description":"Sensor Portal",
   "lang":"en-US",
   "categories":[
      "Tools"
   ],
   "scope":"/",
   "icons":[
      {
         "src":"/PWAIcon/512x512",
         "type":"image/png",
         "sizes":"512x512",
         "purpose": "any maskable"
      },
      {
         "src":"/PWAIcon/192x192",
         "type":"image/png",
         "sizes":"192x192"
      },
      {
         "src":"/PWAIcon/144x144",
         "type":"image/png",
         "sizes":"144x144"
      },
      {
         "src":"/PWAIcon/96x96",
         "type":"image/png",
         "sizes":"96x96"
      },
      {
         "src":"/PWAIcon/72x72",
         "type":"image/png",
         "sizes":"72x72"
      },
      {
         "src":"/PWAIcon/48x48",
         "type":"image/png",
         "sizes":"48x48"
      },
      {
         "src":"/PWAIcon/1024x1024",
         "type":"image/png",
         "sizes":"1024x1024"
      },
      {
         "src":"/PWAIcon/180x180",
         "type":"image/png",
         "sizes":"180x180"
      },
      {
         "src":"/PWAIcon/152x152",
         "type":"image/png",
         "sizes":"152x152"
      },
      {
         "src":"/PWAIcon/120x120",
         "type":"image/png",
         "sizes":"120x120"
      },
      {
         "src":"/PWAIcon/76x76",
         "type":"image/png",
         "sizes":"76x76"
      },
      {
         "src":"/PWAIcon/114x114",
         "type":"image/png",
         "sizes":"114x114"
      },
      {
         "src":"/PWAIcon/58x58",
         "type":"image/png",
         "sizes":"58x58"
      },
      {
         "src":"/PWAIcon/57x57",
         "type":"image/png",
         "sizes":"57x57"
      },
      {
         "src":"/PWAIcon/750x1334",
         "type":"image/png",
         "sizes":"750x1334"
      },
      {
         "src":"/PWAIcon/1334x750",
         "type":"image/png",
         "sizes":"1334x750"
      },
      {
         "src":"/PWAIcon/1242x2208",
         "type":"image/png",
         "sizes":"1242x2208"
      },
      {
         "src":"/PWAIcon/2208x1242",
         "type":"image/png",
         "sizes":"2208x1242"
      },
      {
         "src":"/PWAIcon/640x960",
         "type":"image/png",
         "sizes":"640x960"
      },
      {
         "src":"/PWAIcon/640x1136",
         "type":"image/png",
         "sizes":"640x1136"
      },
      {
         "src":"/PWAIcon/1536x2048",
         "type":"image/png",
         "sizes":"1536x2048"
      },
      {
         "src":"/PWAIcon/2048x1536",
         "type":"image/png",
         "sizes":"2048x1536"
      },
      {
         "src":"/PWAIcon/768x1024",
         "type":"image/png",
         "sizes":"768x1024"
      },
      {
         "src":"/PWAIcon/1024x768",
         "type":"image/png",
         "sizes":"1024x768"
      }
   ]
}
<%} %>