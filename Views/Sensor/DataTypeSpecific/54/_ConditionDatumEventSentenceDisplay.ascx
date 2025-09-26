<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 54 MoistureWeight-->
<%
	Type datumType = AppDatum.getType(Model.eDatumType);
	bool isBoolDatumType = (datumType == AppDatum.getType(eDatumType.BooleanData) || datumType.BaseType == AppDatum.getType(eDatumType.BooleanData));
	UnitConversion currentUnitOfMeasure = null;
	double valcompare = Model.CompareValue.ToDouble();

	Sensor sensor = Sensor.Load(Model.SensorID);
	List<UnitConversion> listOfConversions = Monnit.MonnitApplicationBase.GetScales(sensor, Model.eDatumType);
	currentUnitOfMeasure = listOfConversions.Where(conversion => conversion.UnitLabel == Model.Scale).FirstOrDefault();

	if (currentUnitOfMeasure == null)
	{
		currentUnitOfMeasure = listOfConversions[0];
	}

	valcompare = (Model.CompareValue.ToDouble() - currentUnitOfMeasure.Intercept) / currentUnitOfMeasure.Slope;
%>

<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Reading is ") + Model.CompareType.ToString().Replace("_"," ")%> <%= valcompare %> <%= !String.IsNullOrEmpty(Model.Scale) ? currentUnitOfMeasure.UnitFrom : "" %>

