<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType Default -->

<% 
    Monnit.eCompareType adjustedCompareValueToShow = Model.CompareType;

    if (adjustedCompareValueToShow == eCompareType.Greater_Than_or_Equal)
    {
        adjustedCompareValueToShow = eCompareType.Greater_Than;
    }
    else if (adjustedCompareValueToShow == eCompareType.Less_Than_or_Equal)
    {
        adjustedCompareValueToShow = eCompareType.Less_Than;
    }

 %>

<%: Html.TranslateTag("Reading is") + adjustedCompareValueToShow.ToString().Replace("_"," ")%> <%:Model.CompareValue %> <%=Model.Scale %>