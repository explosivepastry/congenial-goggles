<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<input type="hidden" name="signalTestDuration" value ="<%:Model.Calibration1 %>" />
<input type="hidden" name="signalReliabilityLevel" value ="<%:SiteSurvey.GetSignalReliabilityLevel(Model) %>" />
<input type="hidden" name="autoShutoffTime" value="<%:Model.Calibration2 %>" />
<input type="hidden" name="FullNotiString" value="<%: SiteSurvey.GetShowFullDataValue(Model.SensorID) %>" />