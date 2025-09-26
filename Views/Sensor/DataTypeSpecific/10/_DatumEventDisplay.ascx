<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 10 - Distance-->

<% 
    string valcompare = "0";
    string displayLabel = "";

    switch (Model.Scale)
    {
        case "cm":
            valcompare = Monnit.Application_Classes.DataTypeClasses.Distance.InchesToCentimeter(Model.CompareValue.ToDouble()).ToString();
            displayLabel = "Centimeters";
            break;
        case "Meters":
            valcompare = Monnit.Application_Classes.DataTypeClasses.Distance.InchesToMeter(Model.CompareValue.ToDouble()).ToString();
            displayLabel = "Meters";
            break;
        case "mm":
            valcompare = Monnit.Application_Classes.DataTypeClasses.Distance.InchesToMillimeters(Model.CompareValue.ToDouble()).ToString();
            displayLabel = "Millimeters";
            break;
        case "Feet":
            valcompare = Monnit.Application_Classes.DataTypeClasses.Distance.InchesToFeet(Model.CompareValue.ToDouble()).ToString();
            displayLabel = "Feet";
            break;
        case "Yards":
            valcompare = Monnit.Application_Classes.DataTypeClasses.Distance.InchesToYards(Model.CompareValue.ToDouble()).ToString();
            displayLabel = "Yards";
            break;
        default:
        case "Inches":
            valcompare = (Model.CompareValue.ToDouble()).ToString();
            displayLabel = "Inches";
            break;

    }

    if (valcompare.ToDouble() < 0) valcompare = "0";
%>

<div class="reading-tag1">

    <div class="hidden-xs ruleDevice__icon">
    </div>
    <div class="tag-title"><span><%= Html.TranslateTag("Condition") %></span> </div>

    <div class="triggerDevice__name">
        <strong style="margin-top: 10px;"><%= Html.TranslateTag("When distance reading is") %>
            <br />
        </strong>
        <span class="reading-tag-condition"><%=Html.TranslateTag(MonnitSession.NotificationInProgress.CompareType.ToString().Replace("_"," ")) %> <%:valcompare %> <%= Html.TranslateTag(displayLabel) %>
        </span>
    </div>

</div>

