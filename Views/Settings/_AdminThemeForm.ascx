<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<AccountThemeStyle>>" %>

<%
    string accountthemeid = Request.Path.ToString().Split('/').LastOrDefault();
    bool isGradient = false;
    //Set Default Primary and Secondary Colors
    AccountThemeStyle primary = null;
    AccountThemeStyle secondary = null;

    foreach (var style in Model)
    {
        if (style.Type.Property == "PrimaryColor")
        {
            primary = style;
            if (secondary == null)
            {
                secondary = style;
            }
        }
        if (style.Type.Property == "SecondaryColor")
        {
            secondary = style;
        }
    }
    if (primary != null)//Secondary assigned to primary if not defined separately
    {
        isGradient = primary.Value != secondary.Value;
    }
	


	foreach (AccountThemeStyleType styleType in AccountThemeStyleType.LoadAll().OrderBy(t => t.DisplayOrder))
    {
		if (MonnitSession.CurrentTheme != null && !MonnitSession.CurrentTheme.AllowPWA && (styleType.Property == "MobileAppLogo" || styleType.Property == "MobileAppName"))
			continue;

        var style = Model.Where(s => s.AccountThemeStyleTypeID == styleType.AccountThemeStyleTypeID).FirstOrDefault();
        if (style == null)
        {
            style = new AccountThemeStyle()
            {
                AccountThemeStyleTypeID = styleType.AccountThemeStyleTypeID,
                BinaryValue = styleType.DefaultBinaryValue,
                Value = styleType.DefaultValue
            };
        }

        switch (styleType.DataType.ToLower().Trim())
        {
            //Color Picker
            case "color":%>
				<div class="x_content col-12">
					<div class="bold aSettings__title  col-sm-3 col-12">
						<%:styleType.Name%>
					</div>			
					<div class="col-sm-9 col-12" style="padding-top: 10px; margin-left: -17px;">
						<input name="<%: styleType.Property%>" class="form-control a_modalInputs jscolor { hash:true, backgroundColor:'#E7E2E4', closable:true, closeText:'Set Color'}" data-typeid="<%=style.AccountThemeStyleTypeID %>" style="width: 100px; margin-left: 15px;" value="<%: style.Value %>" />
					</div>
					<div class="clearfix"></div>
				</div>
				<%break;
			//Text Input
			case "text":%>
				<div class="x_content col-12">
					<div class="bold aSettings__title col-sm-3 col-12">
						<%:styleType.Name%>
					</div>
					<div class="col-sm-9 col-12" style="padding-top: 10px;">
						<input name="<%: styleType.Property%>" class="form-control a_modalInputs" value="<%: style.Value %>" />
					</div>
					<div class="clearfix"></div>
				</div>
			<%break;
		   //Image Input
			case "image":
			case "logo":
			case "appicon":%>
				<div class="x_content col-12">
					<div class="bold aSettings__title  col-sm-3 col-12">
						<%:styleType.Name%>
					</div>
					<div class="col-sm-9 col-12" style="padding-top: 10px;">
						<img style="max-height: 40px;" src='data:image/jpeg;base64, <%: System.Convert.ToBase64String(style.BinaryValue) %>' /><br /><br />
						<input type="button" value="Upload Image" onclick="showUpload(<%: styleType.AccountThemeStyleTypeID%>);"/>
					</div>
					<div class="clearfix"></div>
				</div>
				<%break;
		   //icon Input
			case "ico":%>
				<div class="x_content col-12">
					<div class="bold aSettings__title  col-sm-3 col-12">
						<%:styleType.Name%>
					</div>
					<div class="col-sm-9 col-12" style="padding-top: 10px;">
						<img style="height:16px;" src='data:image/jpeg;base64, <%: System.Convert.ToBase64String(style.BinaryValue) %>' /><br /><br />
						<input type="button" value="Upload Icon" onclick="showUpload(<%: styleType.AccountThemeStyleTypeID%>);"/>
					</div>
					<div class="clearfix"></div>
				</div>
				<%break;

			default:
			  break;
	}%>
<%} %>

<div class="clearfix"></div>
<div class="text-end">
	<a href="/Settings/AdminAccountThemeEdit/<%: accountthemeid %>" class="btn btn-light me-2"><%: Html.TranslateTag("Cancel","Cancel")%></a>
	<button id="prefSave" type="submit" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary">
		<%: Html.TranslateTag("Save","Save")%>
        &nbsp;
		<svg xmlns="http://www.w3.org/2000/svg" width="13" height="13" viewBox="0 0 18 18">
			<path id="ic_save_24px" d="M17,3H5A2,2,0,0,0,3,5V19a2,2,0,0,0,2,2H19a2.006,2.006,0,0,0,2-2V7ZM12,19a3,3,0,1,1,3-3A3,3,0,0,1,12,19ZM15,9H5V5H15Z" transform="translate(-3 -3)" fill="#fff" />
		</svg>
	</button>
</div>

<script src="/Scripts/bootstrap-toggle.min.js"></script>
<script src="/Scripts/jscolor.js"></script>
<script type="text/javascript">
	$(document).ready(function () {
		$('.bootTooggle').bootstrapToggle();
				
		gradientToggle();
		$('#gradientToggle').change(gradientToggle);
	});

	$('.jscolor').keyup(function () {
		if ($(this).val() == '') {
			var id = $(this).data('typeid');
			setDefaultValue(id, this);			
		}

	});

	//function gradientToggle() {
	//	if ($('#gradientToggle').is(":checked"))
	//		$('#secondaryToggle').show();
	//	else {
	//		var primarycolor = $('#primarycolor').val();			
	//		$('#secondarycolor').val(primarycolor);
	//		$('#secondaryToggle').hide();
	//	}
	//}

	function setDefaultValue(id, element) {
		$.get('/Settings/GetDefaultValue/' + id, function (data) {			
			$(element).val(data);
		});		
	}

</script>
