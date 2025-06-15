// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('.close-alert').click(function () {
    $('.alert').hide('hide')
});

$(document).ready(function () {
    startTable('#myTable');
    startTable('#usersTable');
});

document.querySelectorAll('.btn-total-contatos').forEach(function (btn) {
    btn.addEventListener('click', function () {
        const userId = this.getAttribute('usuario-id');

        fetch("/Users/ListarContatosUserId/" + userId)
            .then(function (response) {
                if (!response.ok) {
                    throw new Error('Erro ao carregar os contatos.');
                }
                return response.text();
            })
            .then(function (html) {
                document.getElementById("listaContatosUser").innerHTML = html;

                const myModal = new bootstrap.Modal(document.getElementById('modalContatosUser'));
                myModal.show();
            })
            .catch(function (error) {
                console.error(error);
                alert("Erro ao buscar contatos.");
            });
    });
});

document.addEventListener('DOMContentLoaded', function () {

    const ctx = document.getElementById('graficoDados');
    if (!ctx) return; 


    const perfil = document.body.dataset.perfilUsuario || 'Normal';
    const totalUsuarios = parseInt(document.body.dataset.totalUsuarios) || 0;
    const totalContatos = parseInt(document.body.dataset.totalContatos) || 0;

    const mostrarGrafico = (perfil === 'Admin' && totalUsuarios > 0) || totalContatos > 0;

    if (!mostrarGrafico) {
        ctx.parentElement.style.display = 'none';
        return;
    }

    let labels = [];
    let dados = [];
    let cores = [];

    if (perfil === 'Admin') {
        labels = ['Usuários', 'Contatos'];
        dados = [totalUsuarios, totalContatos];
        cores = ['rgba(255, 165, 0, 0.7)', 'rgba(54, 162, 235, 0.7)'];
    } else {
        labels = ['Meus Contatos'];
        dados = [totalContatos];
        cores = ['rgba(54, 162, 235, 0.7)'];
    }

    new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: labels,
            datasets: [{
                label: 'Total',
                data: dados,
                backgroundColor: cores,
                borderColor: 'rgba(0, 0, 0, 1)',
                borderWidth: 2
            }]
        },
        options: {
            devicePixelRatio: 2,
            plugins: {
                legend: { display: true, position: 'bottom' },
                title: { display: true, text: 'Resumo do Sistema' }
            }
        }
    });
});

function startTable(id) {
    $(id).DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "oLanguage": {
            "sEmptyTable": "Nenhum registro encontrado",
            "sInfo": "Mostrar _START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrar 0 até 0 de 0 Registros",
            "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Mostrar _MENU_ registros por pagina",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Proximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Ultimo"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });

}
