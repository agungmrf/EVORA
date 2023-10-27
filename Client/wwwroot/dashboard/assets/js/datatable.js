
$('#example').dataTable({
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

let table2 = document.getElementById('example2');
table2.dataTable({
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