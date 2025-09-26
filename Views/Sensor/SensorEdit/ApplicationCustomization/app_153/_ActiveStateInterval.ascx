<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<input class="form-control user-dets" type="hidden" <%=Model.CanUpdate ? "" : "disabled"  %> name="ActiveStateInterval" id="ActiveStateInterval" value="<%=Model.ActiveStateInterval %>" />

<script>
    const inputToCopy = document.querySelector("#ReportInterval");
    const inputToCopyTo = document.querySelector("#ActiveStateInterval");

    inputToCopy.addEventListener("change", () => {
        inputToCopyTo.value = inputToCopy.value;
    });

</script>
