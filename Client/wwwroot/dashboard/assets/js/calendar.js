const baseUrl = "https://localhost:60107/api/transactionevent/";
var modalDetail = new bootstrap.Modal(document.getElementById('modal-detail'));
document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        height: 650,
        initialView: 'dayGridMonth',
        events: 'https://localhost:7074/events/get',
        eventClick: function (info) {
            let id = info.event._def.publicId;
            console.log(id);
            $.ajax({
                url: baseUrl + "detailByGuid/" + id,
                type: "GET",
                headers: {
                    'Content-Type': 'application/json'
                }
            }).done((result) => {
                const street = result.data.street;
                const subDistrict = result.data.subDistrict;
                const district = result.data.district;
                const city = result.data.city;
                const province = result.data.province;
                const inputEventDate = new Date(result.data.eventDate).toLocaleDateString('en-CA');

                $('#wInvoice2').val(result.data.invoice);
                $('#wFullName2').val(result.data.firstName + " " + result.data.lastName);
                $('#wEmail2').val(result.data.email);
                $('#wLocation2').val(`${street}, ${subDistrict}, ${district}, Kota ${city}, Provinsi ${province}.`);
                $('#wPackage2').val(result.data.package);
                $('#wEventDate2').val(inputEventDate);

            }).fail((error) => {
                console.log(error);
            })
            modalDetail.show()
        }
    });
    calendar.render();
});
