
const baseUrl = "https://localhost:60107/api/TransactionEvent/detail/";
$(document).ready(() => {

    const table = $('#transaction-table').DataTable({
        ordering: false,
        ajax: {
            url: baseUrl,
            dataSrc: 'data',
            'error': function (jqXHR, textStatus, errorThrown) {
                $('#transaction-table').DataTable().clear().draw();
            }
        },
        columns: [
            {
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            {
                data: 'email',
            },
            {
                data: 'invoice',
            },
            {
                data: 'eventDate',
                render: (data, type, row) => {
                    const options = { day: '2-digit', month: '2-digit', year: 'numeric' };

                    return new Date(data).toLocaleDateString('en-GB', options);
                }
            },
            {
                data: 'city',
            },
            {
                data: 'package',
            },
            {
                data: 'status',
                render: (data, type, row) => {
                    return printBadge(data);
                }
            },
            {
                render: (data, type, row) => {
                    return `
                    <button type="button" class="btn btn-sm btn-primary" onclick="edit('${row.guid}')"  data-bs-toggle="modal" data-bs-target="#transaction-modal">
                       <span>
                            <i class="ti ti-pencil"></i>
                       </span>
                    </button>
                    <button type="button" class="btn  btn-sm btn-danger" onclick="remove('${row.guid}')">
                        <span>
                            <i class="ti ti-trash"></i>
                        </span>
                    </button>`
                }
            }
        ],
        dom: "<'row'<'col-sm-12 col-md-9'B><'col-sm-12 col-md-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-12 col-md-6'i><'col-sm-12 col-md-6'p>>",

        buttons: [
            {
                extend: 'excelHtml5',
                attr: {
                    id: 'excel-btn'
                },
                className: 'btn btn-success'
            },
            {
                extend: 'pdfHtml5',
                attr: {
                    id: 'pdf-btn'
                },
                className: 'btn btn-danger',
            }, {
                extend: 'colvis',
                text: 'Visibility',
                attr: {
                    id: 'colvis-btn'
                },
                className: 'btn btn-info'
            }
        ]
    });

    let element = document.getElementById('excel-btn');
    element.classList.remove('dt-button');
    let element2 = document.getElementById('pdf-btn');
    element2.classList.remove('dt-button', 'buttons-pdf', 'buttons-html5');
    let element3 = document.getElementById('colvis-btn');
    element3.classList.remove('dt-button', 'buttons-colvis');

    printBadge = function (id) {
        let badge = "";
        switch (id) {
            case 0:
                badge = '<span class="badge bg-danger">Canceled</span>';
                break;
            case 1:
                badge = '<span class="badge bg-success">Complete</span>';
                break;
            case 2:
                badge = '<span class="badge bg-warning text-dark">OnGoing</span>';
                break;
        }
        return badge
    }
})