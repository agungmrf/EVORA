
const baseUrl = "https://localhost:50969/api/city/";

$(document).ready(() => {

    const table = $('#city-table').DataTable({
        ordering: false,
        ajax: {
            url: baseUrl,
            dataSrc: 'data',
            'error': function (jqXHR, textStatus, errorThrown) {
                $('#city-table').DataTable().clear().draw();
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
                data: 'name',
            },
            {
                render: (data, type, row) => {
                    return `
                    <button type="button" class="btn btn-sm btn-primary" onclick="edit('${row.guid}')"  data-bs-toggle="modal" data-bs-target="#modal-city">
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

    $(".btn-input").on("click", function () {
        $('#form-city')[0].reset();
        $("#modal-city .modal-title").html("Add New City");

        $("#modal-city button[type=submit]").removeClass("btn-edit");
    });

    edit = function (id) {
        console.log(id);
        $.ajax({
            url: baseUrl + id,
            type: "GET",
            headers: {
                'Content-Type': 'application/json'
            }
        }).done((result) => {

            $("#guid").val(result.data.guid);
            $("#wName2").val(result.data.name);
            $("#wProvince2").val(result.data.provinceGuid).change();
            console.log(result.data.provinceGuid);

            $("#modal-city .modal-title").html("Edit City");

            $("#modal-city button[type=submit]").addClass("btn-edit");
        }).fail((error) => {
            console.log(error);
        })
    }

    remove = function (id) {
        Swal.fire({
            text: "Apakah anda yakin ingin meghapus ?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Iya'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: baseUrl + "?guid=" + id,
                    type: "DELETE",
                    headers: {
                        'Content-Type': 'application/json'
                    },
                }).done((result) => {
                    console.log(result);
                    table.ajax.reload();
                    messageSuccess("Data berhasil dihapus");
                }).fail((error) => {
                    messageFail("Data gagal dihapus");
                    console.log(error, 'ini error');
                });
            }
        })
    }

    const form = document.querySelector('#form-city');
    form.addEventListener("submit", (e) => {
        e.preventDefault();
        const lastElement = e.srcElement[e.srcElement.length - 1];

        const data = {};
        data.name = $("#wName2").val();
        data.provinceGuid = $("#wProvince2").val();
        console.log(data);

        if ($(lastElement).hasClass('btn-edit')) {
            data.guid = $("#guid").val();
            console.log(data, 'update');
            const stringData = JSON.stringify(data);
            $.ajax({
                url: baseUrl,
                type: "PUT",
                headers: {
                    'Content-Type': 'application/json'
                },
                data: stringData
            }).done((result) => {
                console.log(result);
                $('#form-city')[0].reset();
                $('#modal-city').modal('hide');
                messageSuccess("Data berhasil Diupdate");

                table.ajax.reload();

            }).fail((error) => {
                messageFail("Data gagal diupdate");
                console.log(error, 'ini error');
            });

        } else {

            const stringData = JSON.stringify(data);
            $.ajax({
                url: baseUrl,
                type: "POST",
                headers: {
                    'Content-Type': 'application/json'
                },
                data: stringData
            }).done((result) => {
                console.log(result);
                $('#form-city')[0].reset();
                $('#modal-city').modal('hide');
                table.ajax.reload();
                messageSuccess("Data berhasil disimpan");
            }).fail((error) => {
                messageFail("Data gagal disimpan");
                console.log(error, 'ini error');
            });
        }
    });

    messageSuccess = function (msg) {
        Swal.fire({
            position: 'top-end',
            icon: 'success',
            title: msg,
            showConfirmButton: false,
            timer: 1500
        })
    }

    messageFail = function (msg) {
        Swal.fire({
            position: 'top-end',
            icon: 'error',
            title: msg,
            showConfirmButton: false,
            timer: 1500
        })
    }

    $.ajax({
        url: "https://localhost:50969/api/province/",
    }).done((result) => {
        console.log(result);
        optionProvince = "";
        result.data.forEach(data => {
            optionProvince += `<option value="${data.guid}">${data.name}</li>`;
        });
        $('#wProvince2').html(optionProvince);
    });


    $('#wProvince2').select2({
        theme: 'bootstrap-5',
        multiple: false,
        dropdownParent: $("#modal-city"),
    });

});