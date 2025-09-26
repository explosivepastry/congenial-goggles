<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div class="x_panel col-md-12 powertourhook shadow-sm rounded">
	<div class="card_container__top">
		<div id="explorer_top" class="card_container__top__title dfjcsb" style="overflow: unset; justify-content: space-between;">
			<span><%: Html.TranslateTag("API/_ExplorerResult|API Result","API Result")%></span>
			<input class="alignright" id="btn_copyResults" type="button" style="margin: -10px; font-weight: 400;" value="<%: Html.TranslateTag("API/_ExplorerResult|Copy Results","Copy Results")%>" />
		</div>
	</div>
	<div class="x_content">
		<div class="card__container__body">
			<pre id="pre_results" style="border: solid 1px black; background-color: #DDEEFF; padding: 5px; overflow: auto; max-width: 835px; white-space: pre-wrap"></pre>
		</div>
	</div>
</div>

<script type="text/javascript">

	$('#btn_copyResults').click(function () {		
		const copyText = document.getElementById("pre_results").textContent;
		const textArea = document.createElement('textarea');
        textArea.id = 'copied_text'
		textArea.textContent = copyText;
		document.body.append(textArea);
		textArea.select();
		document.execCommand("copy");
        $('#pre_results').text('Copied to clipboard');

		const btnTxt = $(this).val();
		$(this).val('Copied');
		$(this).prop('disabled', true);

		setTimeout(function (mybtn) {
			$('#pre_results').text($('#copied_text').text());
			$('#copied_text').remove();
            $(mybtn).val(btnTxt);
            $(mybtn).prop('disabled', false);
		},
			1000,
			this
		);
	});

</script>