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