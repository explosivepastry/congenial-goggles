<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="">
	<%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Type_030/_IMSI.ascx", Model); %>
    <input type="hidden" name="M1BandMask" id="M1BandMask" value="<%= Model.M1BandMask%>">
	<input type="hidden" name="NB1BandMask" id="NB1BandMask" value="<%= Model.NB1BandMask%>">

	<div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Carrier Preference","Carrier Preference")%>
        </div>

        <div class="col sensorEditFormInput">
            <select id="UMNOProf" name ="UMNOProf" class="form-select">
                <option value="0" <%= Model.UMNOProf == 0 ? "selected=''" : ""%>><%: Html.TranslateTag("Auto","Auto")%></option>
                <option value="100" <%= Model.UMNOProf == 100 ? "selected=''" : ""%>"><%: Html.TranslateTag("Manual","Manual")%></option>
            </select>

            <%: Html.ValidationMessageFor(model => model.UMNOProf)%>
        </div>
    </div>

    <div class="row sensorEditForm prefReq">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Carrier APN","Carrier APN")%>
        </div>

        <div class="col sensorEditFormInput">
            <input type="text" class="form-control" id="CellAPNName" name="CellAPNName" value="<%= Model.CellAPNName%>" style="width: 50%;" />
            <%: Html.ValidationMessageFor(model => model.CellAPNName)%>
        </div>
    </div>

    <div class="row sensorEditForm prefReq">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("SIM Authentication Type","SIM Authentication Type")%>
        </div>
        <div class="col sensorEditFormInput">
            <select class="form-control" id="SIMAuthType" name ="SIMAuthType">
                <option value="0" <%= Model.SIMAuthType == 0 ? "selected=''" : ""%>"><%: Html.TranslateTag("None","None")%></option>
                <option value="1" <%= Model.SIMAuthType == 1 ? "selected=''" : ""%>"><%: Html.TranslateTag("PAP","PAP")%></option>
                <option value="2" <%= Model.SIMAuthType == 2 ? "selected=''" : ""%>"><%: Html.TranslateTag("CHAP","CHAP")%></option>
            </select>
            <%: Html.ValidationMessageFor(model => model.SIMAuthType)%>
        </div>
    </div>

    <div class="row sensorEditForm authReq">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Username","Username")%>
        </div>

        <div class="col sensorEditFormInput">
            <input type="text" class="form-control" id="Username" name="Username" oninvalid="alert('Improper Characters detected');" pattern="[a-zA-Z][a-zA-Z0-9-_.]{0,32}"  value="<%= Model.Username%>" style="width: 50%;" />
            <%: Html.ValidationMessageFor(model => model.Username)%>
        </div>
    </div>

    <div class="row sensorEditForm authReq">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Password","Password")%>
        </div>

        <div class="col sensorEditFormInput">
            <%
                string tempPass = string.Empty;
                try { tempPass = Model.Password.Decrypt(); }
                catch { tempPass = Model.Password; }
            %>
            <input type="password" class="form-control" id="Password" name="Password" oninvalid="alert('Improper Characters detected');" pattern="[a-zA-Z][a-zA-Z0-9-_.]{0,32}"  value="<%= tempPass%>" style="width: 50%;" />
            <%: Html.ValidationMessageFor(model => model.Password)%>
        </div>
    </div>

    <div class="row sensorEditForm prefReq">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Active Bands","Active Bands")%>
        </div>

        <div class="col sensorEditFormInput">
            <table style="min-width: 250px;">
			    <tr><td></td><td><%: Html.TranslateTag("M Enabled","M Enabled")%></td><td><%: Html.TranslateTag("NB Enabled","NB Enabled")%></td></tr>
			    <tr><td><%: Html.TranslateTag("Band 1","Band 1")%> </td><td><input type="checkbox" class="M1Chk" value="1"       id="m1" ></td> <td><input type="checkbox" class="NB1Chk" value="1"       id="n1" ></td></tr>
			    <tr><td><%: Html.TranslateTag("Band 2","Band 2")%> </td><td><input type="checkbox" class="M1Chk" value="2"       id="m2" ></td> <td><input type="checkbox" class="NB1Chk" value="2"       id="n2" ></td></tr>
			    <tr><td><%: Html.TranslateTag("Band 3","Band 3")%> </td><td><input type="checkbox" class="M1Chk" value="4"       id="m3" ></td> <td><input type="checkbox" class="NB1Chk" value="4"       id="n3" ></td></tr>
			    <tr><td><%: Html.TranslateTag("Band 4","Band 4")%></td><td><input type="checkbox" class="M1Chk" value="8"       id="m4" ></td> <td><input type="checkbox" class="NB1Chk" value="8"       id="n4" ></td></tr>
			    <tr><td><%: Html.TranslateTag("Band 5","Band 5")%> </td><td><input type="checkbox" class="M1Chk" value="16"      id="m5" ></td> <td><input type="checkbox" class="NB1Chk" value="16"      id="n5" ></td></tr>
			    <tr><td><%: Html.TranslateTag("Band 8","Band 8")%> </td><td><input type="checkbox" class="M1Chk" value="32"      id="m8" ></td> <td><input type="checkbox" class="NB1Chk" value="32"      id="n8" ></td></tr>
                                                                                                                                    
			    <tr><td><%: Html.TranslateTag("Band 12","Band 12")%></td><td><input type="checkbox" class="M1Chk" value="64"      id="m12"></td> <td><input type="checkbox" class="NB1Chk" value="64"      id="n12"></td></tr>
			    <tr><td><%: Html.TranslateTag("Band 13","Band 13")%></td><td><input type="checkbox" class="M1Chk" value="128"     id="m13"></td> <td><input type="checkbox" class="NB1Chk" value="128"     id="n13"></td></tr>
                <% if (Model.GatewayTypeID == 36)
                    {
                %>
                <tr>
                    <td><%: Html.TranslateTag("Band 17","Band 17") %></td>
                    <td>N/A</td>
                    <td>
                        <input type="checkbox" class="M1Chk" value="256" id="m14"></td>
                </tr>
                <%
                    }
                    else
                    {
                %>
                <tr>
                    <td><%: Html.TranslateTag("Band 14","Band 14") %></td>
                    <td>
                        <input type="checkbox" class="M1Chk" value="256" id="m14"></td>
                    <td>N/A</td>
                </tr>
                <%
                    }
                %>
			    <tr><td><%: Html.TranslateTag("Band 18","band 18")%></td><td><input type="checkbox" class="M1Chk" value="512"     id="m18"></td> <td><input type="checkbox" class="NB1Chk" value="512"     id="n18"></td></tr>
			    <tr><td><%: Html.TranslateTag("Band 19","Band 19")%></td><td><input type="checkbox" class="M1Chk" value="1024"    id="m19"></td> <td><input type="checkbox" class="NB1Chk" value="1024"    id="n19"></td></tr>
			    <tr><td><%: Html.TranslateTag("Band 20","Band 20")%></td><td><input type="checkbox" class="M1Chk" value="2048"    id="m20"></td> <td><input type="checkbox" class="NB1Chk" value="2048"    id="n20"></td></tr>
                                                                                                                                                                      
			    <tr><td><%: Html.TranslateTag("Band 25","Band 25")%></td><td><input type="checkbox" class="M1Chk" value="4096"    id="m25"></td> <td><input type="checkbox" class="NB1Chk" value="4096"    id="n25"></td></tr>
                <% if (Model.GatewayTypeID == 36)
                    {
                %>
                <tr>
                    <td><%: Html.TranslateTag("Band 26","Band 26")%></td>
                    <td>
                        <input type="checkbox" class="M1Chk" value="8192" id="m26"></td>
                    <td>N/A</td>
                </tr>

                <%
                    }
                    else
                    {
                %>
                <tr>
                    <td><%: Html.TranslateTag("Band 26","Band 26")%></td>
                    <td>
                        <input type="checkbox" class="M1Chk" value="8192" id="m26"></td>
                    <td>
                        <input type="checkbox" class="NB1Chk" value="8192" id="n26"></td>
                </tr>

                <%
                    }
                %>
			    <tr><td><%: Html.TranslateTag("Band 27","band 27")%></td><td><input type="checkbox" class="M1Chk" value="16384"   id="m27"></td> <td>N/A</td></tr>
			    <tr><td><%: Html.TranslateTag("Band 28","Band 28")%></td><td><input type="checkbox" class="M1Chk" value="32768"   id="m28"></td> <td><input type="checkbox" class="NB1Chk" value="32768"   id="n28"></td></tr>


                <% if (Model.GatewayTypeID == 36)
                    {
                %>

                <%
                    }
                    else
                    {
                %>
                <tr>
                    <td><%: Html.TranslateTag("Band 31","Band 31")%></td>
                    <td>
                        <input type="checkbox" class="M1Chk" value="65536" id="m31"></td>
                    <td>
                        <input type="checkbox" class="NB1Chk" value="65536" id="n31"></td>
                </tr>

                <%
                            }
                %>
		        <tr><td><%: Html.TranslateTag("Band 66","band 66")%></td><td><input type="checkbox" class="M1Chk" value="131072"  id="m66"></td> <td><input type="checkbox" class="NB1Chk" value="131072"  id="n66"></td></tr>
		                        <% if (Model.GatewayTypeID == 36)
                    {
                %>

                <%
                    }
                    else
                    {
                %>
                <tr><td><%: Html.TranslateTag("Band 71","Band 71")%></td><td>N/A</td> <td><input type="checkbox" class="NB1Chk" value="262144"  id="n71"></td></tr>
                <tr><td><%: Html.TranslateTag("Band 72","Band 72")%></td><td><input type="checkbox" class="M1Chk" value="524288"  id="m72"></td> <td><input type="checkbox" class="NB1Chk" value="524288"  id="n72"></td></tr>
		        <tr><td><%: Html.TranslateTag("Band 73","Band 73")%></td><td><input type="checkbox" class="M1Chk" value="1048576" id="m73"></td> <td><input type="checkbox" class="NB1Chk" value="1048576" id="n73"></td></tr>
		        <tr><td><%: Html.TranslateTag("Band 85","Band 85")%></td><td><input type="checkbox" class="M1Chk" value="2097152" id="m85"></td> <td><input type="checkbox" class="NB1Chk" value="2097152" id="n85"></td></tr>
                <%
                            }
                %>
		    </table>
        </div>
    </div>
</div>

<script type="text/javascript">

    function SetAuthType() {
        if ($('#SIMAuthType').val() == "0") {
            $('.authReq').hide();
        }
        else {
            $('.authReq').show();
        }
    }
    SetAuthType();
    $('#SIMAuthType').change(SetAuthType);

    function SetUMNOProf() {
        if ($('#UMNOProf').val() == "100") {
            $('.prefReq').show();
        }
        else {
            $('.prefReq').hide();
        }
    }
    SetUMNOProf();
    $('#UMNOProf').change(SetUMNOProf);

    $('.M1Chk').each(function () { // iterate through each element.
        if ((parseInt($("#M1BandMask").val()) & parseInt($(this).val())) > 0)
            $(this).prop('checked', true);
    });
    $('.M1Chk').change(function () {
        var total = 0;
        $('.M1Chk:checked').each(function () { // iterate through each checked element.
            total += isNaN(parseInt($(this).val())) ? 0 : parseInt($(this).val());
        });
        $("#M1BandMask").val(total);
    });

    $('.NB1Chk').each(function () { // iterate through each element.
        if ((parseInt($("#NB1BandMask").val()) & parseInt($(this).val())) > 0)
            $(this).prop('checked', true);
    });

    $('.NB1Chk').change(function () {
        var total = 0;
        $('.NB1Chk:checked').each(function () { // iterate through each checked element.
            total += isNaN(parseInt($(this).val())) ? 0 : parseInt($(this).val());
        });
        $("#NB1BandMask").val(total);
    });

</script>
