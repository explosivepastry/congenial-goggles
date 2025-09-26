<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 17 - PPM-->

<%string label = "";

    if (Model.DatumIndex == 1)
    {
        label = "TWA";
    }
    else
    {
        label = "PPM";
    }

%>

<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Reading is ") + Model.CompareType.ToString().Replace("_"," ")%> <%=Model.CompareValue %>   <%: label%> 


