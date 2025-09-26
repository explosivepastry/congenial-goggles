<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%  
    string datumName0 = Model.GetDatumName(0);
    string datumName1 = Model.GetDatumName(1);
    string datumName2 = Model.GetDatumName(2);
    string datumName3 = Model.GetDatumName(3);
    
     %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_092|Probe One Name","Probe One Name")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="text" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> id="datumName0" name="datumName0" value="<%= Model.GetDatumName(0)%>"  />
        <%: Html.ValidationMessageFor(model => datumName0)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_092|Probe Two Name","Probe Two Name")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="text" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> id="datumName1" name="datumName1" value="<%= Model.GetDatumName(1)%>"  />
        <%: Html.ValidationMessageFor(model => datumName1)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_092|Probe Three Name","Probe Three Name")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="text" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> id="datumName2" name="datumName2" value="<%= Model.GetDatumName(2)%>"  />
        <%: Html.ValidationMessageFor(model => datumName2)%>
    </div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_092|Probe Four Name","Probe Four Name")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="text" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> id="datumName3" name="datumName3" value="<%= Model.GetDatumName(3)%>"  />
        <%: Html.ValidationMessageFor(model => datumName3)%>
    </div>
</div>
