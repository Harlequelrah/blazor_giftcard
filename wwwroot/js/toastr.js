$(document).ready(function() {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": true,
        "progressBar": true,
        "positionClass": "toast-container",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    const originalToastr = {
        error: toastr.error,
        success: toastr.success,
        warning: toastr.warning,
        info: toastr.info
    };

    toastr.error = function(message, title, options) {
        options = options || {};
        options.iconClass = 'toast alert-danger'; // Bootstrap class for error
        return originalToastr.error.call(this, message, title, options);
    };

    toastr.success = function(message, title, options) {
        options = options || {};
        options.iconClass = 'toast alert-success'; // Bootstrap class for success
        return originalToastr.success.call(this, message, title, options);
    };

    toastr.warning = function(message, title, options) {
        options = options || {};
        options.iconClass = 'toast alert-warning'; // Bootstrap class for warning
        return originalToastr.warning.call(this, message, title, options);
    };

    toastr.info = function(message, title, options) {
        options = options || {};
        options.iconClass = 'toast alert-info'; // Bootstrap class for info
        return originalToastr.info.call(this, message, title, options);
    };

    window.showModal = function (modalId) {
        $('#' + modalId).show();
    };

    window.hideModal = function (modalId) {
        $('#' + modalId).hide();
    };
});
