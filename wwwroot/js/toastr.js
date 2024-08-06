$(document).ready(function() {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": true,
        "progressBar": true,
        "positionClass": "toast-container",
        "preventDuplicates": false,
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
    toastr.error = function(message, title, options) {
        options = options || {};
        options.iconClass = 'alert alert-danger';
        toastr.error(message, title, options);
    };

    toastr.success = function(message, title, options) {
        options = options || {};
        options.iconClass = 'alert alert-success';
        toastr.success(message, title, options);
    };

    toastr.warning = function(message, title, options) {
        options = options || {};
        options.iconClass = 'alert alert-warning';
        toastr.warning(message, title, options);
    };

    toastr.info = function(message, title, options) {
        options = options || {};
        options.iconClass = 'alert alert-info';
        toastr.info(message, title, options);
    };
    window.showModal = function (modalId) {
        $('#' + modalId).modal('show');
    };

    window.hideModal = function (modalId) {
        $('#' + modalId).modal('hide');
    };
});
