
const baseUrl = "https://localhost:60107/api/TransactionEvent/detail";
$(document).ready(() => {

    const table = $('#table-transaction').DataTable({
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
                data: 'package',
            },
            {
                data: 'eventDate',
                render: (data, type, row) => {
                    const options = { day: '2-digit', month: '2-digit', year: 'numeric' };

                    return new Date(data).toLocaleDateString('en-GB', options);
                }
            },
            {
                data: 'status',
                render: (data, type, row) => {
                    return printBadge(data);
                }
            },
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
                badge = `<span class="badge bg-light-danger text-danger rounded-pill">
        <span class="round-8 bg-danger rounded-circle d-inline-block me-1"></span>canceled
    </span>`;
                break;
            case 1:
                badge = `
                    <span class="badge bg-light-success text-success rounded-pill">
        <span class="round-8 bg-success rounded-circle d-inline-block me-1"></span>complete
    </span>
                `;
                break;
            case 2:
                badge = `
                <span class="badge bg-light-warning text-warning rounded-pill">
        <span class="round-8 bg-warning rounded-circle d-inline-block me-1"></span>pending
    </span>
                `;
                break;
            case 3:
                badge = `
                <span class="badge bg-light-success text-success rounded-pill">
        <span class="round-8 bg-success rounded-circle d-inline-block me-1"></span>approve
    </span>
                `;
        }
        return badge
    }
})