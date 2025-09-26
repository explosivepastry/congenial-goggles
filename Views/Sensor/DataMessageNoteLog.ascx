<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Monnit.DataMessageNote>>" %>



<div id="divNote">
	<div class="formtitle">
		Note Log for Data Message 
	</div>
		
	<div style="padding: 20px;">
		<form style="width: 100%;" action="/Sensor/DataMessageNoteLog" method="post">
			<table>   
				<%if (Model.Count() > 0) {  %>                            
				<tr>
					<th style="width: 150px;">User
					</th>
					<th style="width: 150px;">Date
					</th>
					<th style="width: 500px;">Note
					</th>
				</tr>
				<%} %>
				<%foreach (var item in Model)
				  {%>
				<tr>
					<td style="width: inherit;">
						<%: String.Format("{0:g}", Customer.Load(item.CustomerID).FullName)%>                        
					</td>
					<td style="width: inherit;">
						<%: Monnit.TimeZone.GetLocalTimeById(item.NoteDate,MonnitSession.CurrentCustomer.Account.TimeZoneID).ToShortDateString() %>
						<%: Monnit.TimeZone.GetLocalTimeById(item.NoteDate,MonnitSession.CurrentCustomer.Account.TimeZoneID).ToShortTimeString() %>
					</td>
					<td style="width: inherit;">
						<%: item.Note%>                      
					</td>
					<td style="width: inherit;">
						<div style="float: right;">
							<%if (MonnitSession.IsCurrentCustomerMonnitAdmin)
							  { %>
							<a href="/Sensor/DeleteNoteHistoryLog/<%:item.DataMessageNoteID %>" class="delete">
								<img src="<%:Html.GetThemedContent("/images/notification/trash.png")%>" height="20" width="30" class="deleteIcon" alt="delete"></a>
							<%} %>
						</div>
					</td>
				</tr>
				<%} %>
			</table>
			<div>
				<textarea id="dataMessageNote" wrap="soft" name="dataMessageNote" style="width: 850px; height: 100px;"></textarea>
			</div>
			<div>
				<input id="dataMessageNoteSave" type="button" class="bluebutton" value="Save Note" />
                <input id="dataMessageNoteCancel" type="button" class="greybutton" value="Cancel/Exit" />
				<div style="clear: both;"></div>
			</div>
		</form>
	</div>
</div>

<script type="text/javascript">
	$(function () {
	    $(".note").on("mouseenter", function () {
			var cell = $(this);
			cell.find('.myhover').height(cell.height()).width(cell.width()).fadeIn(700);
		}).on("mouseleave", function () {
			$(this).find('.myhover').stop().fadeOut(100);
		})

		$('#dataMessageNoteSave').click(function () {		   
		    var dataNote = encodeURIComponent($('#dataMessageNote').val());            		    
		    $.post("/Sensor/DataMessageNoteLog/<%: Request.RequestContext.RouteData.Values["id"]%>", "note=" + dataNote, function (data) {			        		
			if (data == "Success") {
				//$('#divHistory').show();
			    var tabContainter = $('.tabContainer').tabs();			   
				var active = tabContainter.tabs('option', 'active'); 
				tabContainter.tabs('load', active);
				//alert("Note Saved Successful"); //Put a message on the page somewhere to inform the user that the note was saved successfully
			}
			else {
				$('#divNote').show();				
			}
		}, "text");
	});

		$('#dataMessageNoteCancel').click(function () {
			$('#divNote').hide();
			$('#divHistory').show();
		});

		$('.delete').click(function (e) {
			e.preventDefault();
			var lnk = $(this).attr("href");

			$.get(lnk, function (data) {
				if (data == "Success") {
					var tabContainer = $('.tabContainer').tabs();
					var active = tabContainer.tabs('option', 'active');
					tabContainer.tabs('load', active);
				}
				else {
                    showSimpleMessageModal("<%=Html.TranslateTag("Failed to delete note")%>");
				}

			});
			e.stopImmediatePropagation();
		});
	});

   
	if($('#dataMessageNote'.text(null)))
		$("#divNote").find('th').hide()

</script>