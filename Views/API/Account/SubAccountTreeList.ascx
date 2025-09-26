<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SubAccountTreeList</b><br />
    Returns the list of account trees that are sub accounts to the calling users account.<br />
    
    <h4>Parameters</h4>
    <table>
           <tr>
            <td>accountID: </td>
            <td>Integer</td>
            <td>Unique identifier of SubAccount.</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/SubAccountTreeList/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=3" target="_blank">https://<%:Request.Url.Host %>/xml/SubAccountTreeList/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=3</a>--%>
    <input type="button" id="btn_TryAPI_SubAccountTreeList" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_SubAccountTreeList').click(function () {
				var json =
				{
					"auth": true,					
					"api": "SubAccountTreeList",
					"params": [
						{ "name": "accountID", "type": "Integer", "description": "Unique identifier of SubAccount.", "optional": false }						
					]
				};								
				APITest(json);
			});
		});
	</script>
                
    <h4>Example Outputs</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;SubAccountTreeList&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&lt;APIAccountTreeList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIAccountTree  AccountID="3" AccountName="Account3" ParentID="12" AccountIDTree="|1|3|"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIAccountTree  AccountID="5" AccountName="Account5" ParentID="3" AccountIDTree="|1|3|5|" /&gt;
&nbsp;&nbsp;&lt;APIAccountTreeList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
    </pre>
</div>