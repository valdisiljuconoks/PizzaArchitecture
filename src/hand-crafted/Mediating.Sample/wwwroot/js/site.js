$(function() {
    var redirect = function(data) {
        data = JSON.parse(data);
        if (data.redirect) {
            window.location = data.redirect;
        } else {
            window.scrollTo(0, 0);
            window.location.reload();
        }
    };
    var showException = function(data) {
        alert(data.responseJSON.Error);
    };
    var highlightErrors = function(response) {
        var data = response.responseJSON;
        $.each(Object.keys(data),
            function(ix, el) {
                var errors = data[el].Errors,
                    fieldId = el.replace('.', '_');
                if (errors.length != 0) {
                    var $field = $('#' + fieldId);
                    $field.closest('.form-group').addClass('has-error');
                    $field.next('.field-validation-valid').text(errors[0].ErrorMessage);
                }
            });
    };
    var $form = $('form.ajax-form[method=post]');
    $form.on('submit',
        function() {
            var $submitButton = $(this).find('input[type=submit]');
            $submitButton.attr('disabled', true);
            $(window).unbind();
            $.ajax({
                url: $form.attr('action'),
                type: 'post',
                headers: {
                    'X-Ajax-Form': true
                },
                data: new FormData(this),
                cache: false,
                processData: false,
                contentType: false,
                dataType: 'json',
                statusCode: {
                    200: redirect,
                    400: highlightErrors,
                    500: showException
                },
                complete: function() {
                    $submitButton.attr('disabled', false);
                }
            });
            return false;
        });
});