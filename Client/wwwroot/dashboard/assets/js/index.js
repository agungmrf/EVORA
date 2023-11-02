
const baseUrl = "https://localhost:60107/api/";

$(document).ready(() => {

    $.ajax({
        url: baseUrl + "employee",
        type: "GET",
        headers: {
            'Content-Type': 'application/json'
        }
    }).done((result) => {
        const total = result.data.length;
        $("#total-employee").html(total);

    }).fail((error) => {
        console.log(error);
    });

    $.ajax({
        url: baseUrl + "customer",
        type: "GET",
        headers: {
            'Content-Type': 'application/json'
        }
    }).done((result) => {
        const total = result.data.length;
        $("#total-customer").html(total);

    }).fail((error) => {
        console.log(error);
    });

    $.ajax({
        url: baseUrl + "packageevent",
        type: "GET",
        headers: {
            'Content-Type': 'application/json'
        }
    }).done((result) => {
        const total = result.data.length;
        $("#total-package").html(total);

    }).fail((error) => {
        console.log(error);
    });


    $.ajax({
        url: baseUrl + "TransactionEvent/detail",
        type: "GET",
        headers: {
            'Content-Type': 'application/json'
        }
    }).done((result) => {
        const total = result.data.length;
        $("#total-transaction").html(total);

        const getMonthNow = new Date().getMonth() + 1;
        let totalPrice = 0;
        let tableRow = "";
        $.each(result.data, function (index, data) {
            const eventDate = new Date(data.eventDate).getMonth() + 1;
            if ((getMonthNow == eventDate) && (data.status == 1)) {
                totalPrice += data.price
            }
            console.log(index);
            if (index < 6) {
                const options = { day: '2-digit', month: '2-digit', year: 'numeric' };
                let badge = printBadge(data.status)
                let date = new Date(data.eventDate).toLocaleDateString('en-GB', options);
                if (index < 6) {
                    tableRow += `
                <tr>
                    <td class="border-bottom-0">
                        <h6 class="fw-semibold mb-0">${index + 1}</h6>
                    </td>
                    <td class="border-bottom-0">
                        <h6 class="fw-normal mb-0">${data.email}</h6>
                    </td>
                    <td class="border-bottom-0">
                        <h6 class="fw-normal mb-0">${data.invoice}</h6>
                    </td>
                    <td class="border-bottom-0">
                        <h6 class="fw-normal mb-0">${data.package}</h6>
                    </td>
                    <td class="border-bottom-0">
                        <h6 class="fw-normal mb-0">${date}</h6>
                    </td>
                    <td class="border-bottom-0">
                        <h6 class="fw-normal mb-0">${badge}</h6>
                    </td>
               </tr>
            `;
                }
                
            }
            
        });
        $("#table-transaction tbody").html(tableRow)
        //$("#total-price").html("Rp. " + totalPrice.toLocaleString('id-ID'));

        

    }).fail((error) => {
        console.log(error);
    });

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

});