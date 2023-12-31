
const baseUrl = "https://localhost:60107/api/Employee/";

$(document).ready(() => {
    const table = $('#employee-table').DataTable({
        ordering: false,
        ajax: {
            url: baseUrl,
            dataSrc: 'data',
            'error': function (jqXHR, textStatus, errorThrown) {
                $('#employee-table').DataTable().clear().draw();
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
                data: 'nik',
            },
            {
                render: (data, type, row) => {
                    return `${row.firstName}  ${row.lastName}`
                }
            },
            {
                data: 'gender',
                render: function (data, type, row) {
                    if (data === 0) {
                        return "Female";
                    }
                    return "Male";

                }
            },
            {
                data: 'email',
            },
            {
                data: 'phoneNumber',
            },
            {
                render: (data, type, row) => {
                    return `
                    <button type="button" class="btn btn-sm btn-primary" onclick="edit('${row.guid}')"  data-bs-toggle="modal" data-bs-target="#modal-employee">
                       <span>
                            <i class="ti ti-pencil"></i>
                       </span>
                    </button>
                    <button type="button" class="btn  btn-sm btn-danger" onclick="remove('${row.accountGuid}')">
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
        $('#form-employee')[0].reset();
        $("#modal-employee .modal-title").html("Add New Employee");
        var today = new Date().toISOString().split('T')[0];
        $("#wHiringDate2").val(today);

        $("#modal-employee button[type=submit]").removeClass("btn-edit");
        $("#form-password").show();
    });

    getRole = function (id) {
        $.ajax({
            url: "https://localhost:60107/api/Role?guid=" + id,
            type: "GET",
            headers: {
                'Content-Type': 'application/json'
            }
        }).done((result) => {
            $.each(result.data, function (index, value) {
                console.log(value.guid, 'xx', id, 'xxx', value.name)
                if (value.guid === id) {
                    $("#wRole2").val(value.name).change();
                }
            });
        }).fail((error) => {
            console.log(error);
        })
    }

    getAccountRole = function (id) {
        $.ajax({
            url: "https://localhost:60107/api/AccountRole?guid=" + id,
            type: "GET",
            headers: {
                'Content-Type': 'application/json'
            }
        }).done((result) => {
            $.each(result.data, function (index, value) {
                if (value.accountGuid === id) return getRole(value.roleGuid);
            });
        }).fail((error) => {
            console.log(error);
        })
    }

    edit = function (id) {
        console.log(id);
        $.ajax({
            url: baseUrl + id,
            type: "GET",
            headers: {
                'Content-Type': 'application/json'
            }
        }).done((result) => {
            console.log(result, 'edit');
            const inputBirthDate = new Date(result.data.birthDate).toLocaleDateString('en-CA');

            const inputHiringDate = new Date(result.data.hiringDate).toLocaleDateString('en-CA');

            $("#guid").val(result.data.guid);
            $("#nik").val(result.data.nik);
            $("#wfirstName2").val(result.data.firstName);
            $("#wlastName2").val(result.data.lastName);
            $("#wemailAddress2").val(result.data.email);
            $("#wphoneNumber2").val(result.data.phoneNumber);
            $("#wgender2").val(result.data.gender).change();
            $("#wBirthDate2").val(inputBirthDate);
            $("#wHiringDate2").val(inputHiringDate);

            $("#wRole2").val(result.data.role).change();

            $("#modal-employee .modal-title").html("Edit Employee");

            getAccountRole(result.data.accountGuid);

            $("#form-password").hide();

            $("#modal-employee button[type=submit]").addClass("btn-edit");
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
                    url: "https://localhost:60107/api/Account?guid=" + id,
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

    const form = document.querySelector('#form-employee');
    form.addEventListener("submit", (e) => {
        e.preventDefault();
        const lastElement = e.srcElement[e.srcElement.length - 1];


        const data = {};
        data.firstName = $("#wfirstName2").val();
        data.lastName = $("#wlastName2").val();
        data.email = $("#wemailAddress2").val();
        data.phoneNumber = $("#wphoneNumber2").val();
        data.gender = parseInt($("#wgender2").val());
        data.birthDate = $("#wBirthDate2").val();
        data.hiringDate = $("#wHiringDate2").val();
        data.Role = $("#wRole2").val();

        console.log(data);

        if ($(lastElement).hasClass('btn-edit')) {
            data.guid = $("#guid").val();
            data.nik = $("#nik").val();
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
                $('#form-employee')[0].reset();
                $('#modal-employee').modal('hide');
                messageSuccess("Data berhasil Diupdate");

                table.ajax.reload();

            }).fail((error) => {
                messageFail("Data gagal diupdate");
                console.log(error, 'ini error');
            });

        } else {
            data.password = $("#wPassword2").val();
            data.confirmPassword = $("#wConfirmPassword2").val();

            console.log(data)
            const stringData = JSON.stringify(data);
            $.ajax({
                url: "https://localhost:60107/api/Account/register-employee",
                type: "POST",
                headers: {
                    'Content-Type': 'application/json'
                },
                data: stringData
            }).done((result) => {
                console.log(result);
                $('#form-employee')[0].reset();
                $('#modal-employee').modal('hide');
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

    var today = new Date().toISOString().split('T')[0];
    $("#wHiringDate2").val(today);

});