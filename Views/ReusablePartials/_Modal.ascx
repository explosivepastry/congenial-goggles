<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div class="modal">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-body">
        <h4 id="modalMessage"></h4> <!-- Body Message -->
      </div>
      <div class="modal-footer">
        <button type="button" class="modal_confirm modalBtn" data-bs-dismiss="modal" id="modalConfirm"><%: Html.TranslateTag("OK","OK")%></button>
        <button type="button" class="modal_cancel modalBtn" id="modalCancel"><%: Html.TranslateTag("Cancel","Cancel")%></button>
      </div>
    </div>
  </div>
</div>

<script type="text/javascript">

    $('#modalCancel').click(function () {
        $('.modal').remove();
    });

    //$('#modalConfirm').click(function () {
    //    console.log("confirm");
    //})
</script>

<style type="text/css">
    .modal {
        position: fixed;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        display: block;
    }

    .modalBtn {
        color: white;
        border-radius: 6px;
        padding: 15px 20px;
        font-size: 16px;
        border: none;
        display: flex;
        align-items: center;
    }

    .modal_cancel {
        background: grey;
        margin-left: 10px;
    }

    .modal_cancel:hover {
        transition: .2s ease;
        background: #555;
    }

    .modal_confirm {
        background: #0067ab;
    }

    .modal_confirm:hover {
        transition: .2s ease;
        background: #054c7b;
    }

    .modal-footer {
        display: flex;
        justify-content: flex-end;
    }

</style>