$(document).ready(function () {
    $('#motores').DataTable();
});

function mostrarAcoes(mId) {

    console.log('Clicou com id: ' + mId);
    var url = "MotorAux/ModalAcoes?id=" + mId;

    var settings = {
        "async": true,
        "crossDomain": true,
        "url": url,
        "method": "GET",
    };

    $.ajax(settings).done(function (response) {
        $('#modal').html(response);
        $('#myModal').modal('show');
    });
}