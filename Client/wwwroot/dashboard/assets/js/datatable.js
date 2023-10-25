
$('#example').dataTable({
<<<<<<< Updated upstream
    dom: "<'row'<'col-sm-12 col-md-9'B><'col-sm-12 col-md-3'f>>" +
        "<'row'<'col-sm-12'tr>>" +
        "<'row'<'col-sm-12 col-md-6'i><'col-sm-12 col-md-6'p>>",
=======
    dom: "<'row'<'col-sm-12 col-md-6'B><'col-sm-12 col-md-6'f>>" +
        "<'row'<'col-sm-12'tr>>" +
        "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>",
>>>>>>> Stashed changes
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
