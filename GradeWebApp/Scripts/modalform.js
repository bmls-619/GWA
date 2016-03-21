$(function () {

    $.ajaxSetup({ cache: false });

    $("a[data-modal]").on("click", function (e) {

        // hide dropdown if any
        $(e.target).closest('.btn-group').children('.dropdown-toggle').dropdown('toggle');

        
        $('#myModalContent').load(this.href, function () {
            

            $('#myModal').modal({
                //Disabling modal popup closing if user click in background
                backdrop: 'static',

                //Disabling modal popup closing if user tap ESC button
                keyboard: false
            }, 'show');

            //to make autofocus to work in the modal popup, Every time a modal is shown, if it has an autofocus element, focus on it.
            $('.modal').on('shown.bs.modal', function () {
                $(this).find('[autofocus]').focus();
            });

            bindForm(this);
        });

        return false;
    });


});

function bindForm(dialog) {
    
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    //Refresh
                    location.reload();
                } else {
                    $('#myModalContent').html(result);
                    bindForm();
                }
            }
        });
        return false;
    });
}