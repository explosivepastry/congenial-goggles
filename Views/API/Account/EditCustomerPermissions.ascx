<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
	<b>Method: EditCustomerPermissions</b><br />
	Edit Customer Permissions<br />

	<h4>Parameters</h4>
	<table>
		<tr>
			<td>custID:</td>
			<td style="width: 100px;">long</td>
			<td>Customer Number</td>
		</tr>

		<% foreach (var item in CustomerPermissionType.LoadAll().Where(cata => { return cata.Category == "Administration" || cata.Category == "Navigation" || cata.Category == "Networks"; }).ToList())
			{ %>
		<% if (item.Name != "Network_View")
			{ %>
		<tr>
			<td class="permission" data-name="<%=item.Name%>"><%: Html.Label(item.Name) %>:</td>
			<td>bool (optional)</td>
			<td><%: Html.Label(item.Description) %></td>
		</tr>
		<%}
			else
			{ %>

		<tr>
			<td class="permission" data-name="<%=item.Name%>"><%: Html.Label(item.Name+"_Net_{networkID}") %>:</td>
			<td>bool (optional)</td>
			<td><%: Html.Label(item.Description) %>. NetworkID is the ID of the network you want the user to either be able to view or not view. example (Network_View_Net_123=off). you can add or remove multiple networks at the same time by using different networkIDs.</td>
		</tr>

		<%}
			}%>
	</table>

	<h4>Example</h4>
	<%-- <a href="/xml/EditCustomerPermissions/Z3Vlc3Q6cGFzc3dvcmQ=?custID=1&Customer_Create=on&Account_Edit=off" target="_blank">https://<%:Request.Url.Host %>/xml/EditCustomerPermissions/Z3Vlc3Q6cGFzc3dvcmQ=?custID=1&amp;Customer_Create=on&amp;Account_Edit=off</a>--%>
	<input type="button" id="btn_TryAPI_EditCustomerPermissions" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_EditCustomerPermissions').click(function () {

				var permissionparams = {};

				permissionparams["custID"] = new Parameter("custID", "Integer", "", false);

				$('.permission').each(function (index, inp) {

					permissionparams[$(this).data("name")] = new Parameter($(this).data("name"), "Boolean", "", true);

				})

				var json =
				{
					"auth": true,
					"api": "EditCustomerPermissions",
					"params":
						permissionparams
				};

				APITest(json);
			});

			function Parameter(name, type, description, optional) {
				this.name = name;
				this.type = type;
				this.description = description;
				this.optional = optional;
			}
		});



	</script>

	<h4>Example Outputs</h4>
	<pre> style="border: solid 1px black; background-color: #DDEEFF; padding: 5px; overflow: auto;max-width:835px;">
    &lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;EditCustomerPermissions&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Successfully Saved&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
   </pre>
</div>
