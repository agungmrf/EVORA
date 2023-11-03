document.addEventListener('DOMContentLoaded', function () {
    const today = new Date();
    const minDate = new Date(today);
    minDate.setDate(minDate.getDate() + 2); // Minimal 2 hari dari hari ini

    const eventDateInput = document.querySelector('#EventDate');
    eventDateInput.min = today.toISOString().split('T')[0];
    eventDateInput.addEventListener('change', function () {
        const selectedDate = new Date(this.value);
        if (selectedDate < minDate) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Minimal pemesanan adalah 2 hari sebelum hari H.!',
            })
            this.value = ''; // Hapus tanggal yang tidak valid
        }
    });

    $.ajax({
        url: "https://localhost:60107/api/province/",
    }).done((result) => {
        console.log(result);
        optionProvince = `<option value="" disabled selected>Choose..</li>`;
        result.data.forEach(data => {
            optionProvince += `<option value="${data.guid}">${data.name}</li>`;
        });
        $('#province').html(optionProvince);
    });

    $("#province").change(function () {
        var selectedProvinsi = $(this).val();
        console.log(selectedProvinsi);
        if(selectedProvinsi != ""){
            enableSelect();
            $.ajax({
                url: "https://localhost:60107/api/City/province/" + selectedProvinsi,
            }).done((result) => {
                if (result.data != null) {
                    console.log(result);
                    optionCity = `<option value="" disabled selected>Choose..</li>`;
                    result.data.forEach(data => {
                        optionCity += `<option value="${data.guid}">${data.name}</li>`;
                    });
                    $('#city').html(optionCity);
                }
            });
        };
    });

    function disableSelect() {
        document.getElementById("city").disabled = true;
    }

    function enableSelect() {
        document.getElementById("city").disabled = false;
    }
});
