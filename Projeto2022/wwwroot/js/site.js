// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function Sucesso(data) {

    Swal.fire(
        'Sucesso',
        data.msg,
        'success'
    );
}

function Falha() {
    Swal.fire(
        'Falha',
        'Favor verificar todos os campos',
        'error'
    );
}