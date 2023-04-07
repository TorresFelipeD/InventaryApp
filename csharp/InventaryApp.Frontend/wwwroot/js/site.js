// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const swalCustom = Swal.mixin({
    customClass: {
        confirmButton: 'btn btn-dark mr-3',
        cancelButton: 'btn btn-outline-secondary'
    },
    allowOutsideClick: false,
    buttonsStyling: false,
    showCloseButton: true
})

$(function () {
    $('[data-toggle="popover"]').popover()
    $('[data-toggle="tooltip"]').tooltip()
    $(".modal-dialog").draggable({
        handle: ".modal-header"
    });
})