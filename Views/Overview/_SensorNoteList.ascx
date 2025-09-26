<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartModel>" %>


<%if (!MonnitSession.CurrentCustomer.Account.HideData)
    { %>
<div class="rule-card_container" id="pointComment" style="width:100%">
        
      


    <div class="card_container__top__title">
        <%=Html.GetThemedSVG("list") %>
                        &nbsp;
                    <%: Html.TranslateTag("Notes", "Notes")%>
          <!-- help button  sensornotelist-->
        <a class="helpIcon help-hover" data-bs-toggle="modal" title="<%: Html.TranslateTag("Overview/SensorEdit|Notes Help","Notes Help") %>" data-bs-target=".pageHelp" style="margin-left:auto; cursor:pointer; overflow-x:hidden;">
          <div class="help-hover">  <%=Html.GetThemedSVG("circleQuestion") %></div>
        </a>
    </div>
    <div class="x_body verticalScroll" style="max-height: 360px; min-height: 160px; overflow-y: scroll;">
        <div id="dataList" class="dataList" style="padding-top: 10px;">

            <%
                DateTime lastNotesMessageDate = new DateTime();
                List<DataMessageNote> dm = DataMessageNote.LoadBySensorAndDateRange(Model.Sensor.SensorID, Model.FromDate, Model.ToDate);
                foreach (DataMessageNote item in dm)
                {
                    if (item.MessageDate != lastNotesMessageDate)
                    {
                        if (lastNotesMessageDate != new DateTime())
                        {
            %>
            <hr />
            <%}
                DataMessage dataMsg = DataMessage.Load(item.DataMessageGUID);
                lastNotesMessageDate = item.MessageDate;
            %>

            <!-- History -->
            <div class="row message_<%=item.DataMessageGUID %>" style="font-size: 1.4em; cursor: pointer;" title="<%=item.DataMessageGUID %>" onclick="location.href='/Overview/SensorNote/<%=item.DataMessageGUID %>';">

                <div class="col-lg-3 col-md-3 col-sm-3" style="font-size: 0.7em; padding-left: 20px; overflow: scroll;">
                    <%=item.MessageDate.OVToLocalDateTimeShort() %>
                    <br />
                    <%=dataMsg.DisplayData %>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8" style="font-size: 0.7em; overflow: scroll;">
                    <strong><%=Customer.Load(item.CustomerID).FullName %> - </strong>(<%:item.NoteDate.OVToLocalDateTimeShort() %>)
                    <br />
                    - <%:item.Note %>
                </div>
                <div class="col-lg-1 col-md-1 col-sm-1" style=""></div>
                <div class="col-lg-2 col-md-2 col-sm-2" style="font-size: 0.8em"></div>
            </div>

            <%}
                else
                {%>
            <div class="row message_<%=item.DataMessageGUID %>" style="font-size: 1.4em; cursor: pointer;" title="<%=item.DataMessageGUID %>" onclick="location.href='/Overview/SensorNote/<%=item.DataMessageGUID %>';">
                <div class="col-lg-3 col-md-3 col-sm-3" style="font-size: 0.5em; padding-left: 10px;">&nbsp;</div>
                <div class="col-lg-8 col-md-8 col-sm-8" style="font-size: 0.7em; overflow: scroll;">
                    <strong><%=Customer.Load(item.CustomerID).FullName %> - </strong>(<%:item.NoteDate.OVToLocalDateTimeShort() %>)<br />
                    - <%:item.Note %>
                </div>
                <div class="col-lg-1 col-md-1 col-sm-1" style=""></div>
                <div class="col-lg-2 col-md-2 col-sm-2" style="font-size: 0.8em"></div>
            </div>

            <%}
                } %>
        </div>
    </div>
</div>
<%} %>



<div class="modal fade pageHelp" tabindex="-1" role="dialog" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
            <%--            <div class="modal-header">
                <h5 class="modal-title">
                    <%: Html.TranslateTag("Overview/SensorHome|Sensor Edit Settings","Sensor Edit Settings")%>
                </h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>--%>

            <div class="modal-body">

                <div class="row">
                    <div class="word-def" >
                        <%: Html.TranslateTag("To create a note, visit the \"Readings\" page and select your choosen reading.")%>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
        
            </div>
        </div>
    </div>
</div>
<!-- End help button modal -->


