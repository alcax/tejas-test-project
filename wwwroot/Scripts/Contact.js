let contactTable = {};
$(document).ready(function () {
    var current_fs, next_fs, previous_fs; //fieldsets
    var opacity;
    var current = 1;
    var steps = $("fieldset").length;
    setProgressBar(current);
    $(".next").click(function () {

        let isvalid = $("#msform").valid();
        if (isvalid) {
            current_fs = $(this).parent();
            next_fs = $(this).parent().next();
            //Add Class Active
            $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");
            //show the next fieldset
            next_fs.show();
            //hide the current fieldset with style
            current_fs.animate({ opacity: 0 }, {
                step: function (now) {
                    // for making fielset appear animation
                    opacity = 1 - now;
                    current_fs.css({
                        'display': 'none',
                        'position': 'relative'
                    });
                    next_fs.css({ 'opacity': opacity });
                },
                duration: 500
            });
            setProgressBar(++current);
        }

    });

    $(".final").click(function () {
        let isvalid = $("#msform").valid();
        current_fs = $(this).parent();
        next_fs = $(this).parent().next();

        if (isvalid) {
            $.post("/home/addcontact", $("#msform").serialize(), function (result) {
                if (result.status == 1) {
                    setTimeout(() => {
                        location.reload();
                    }, 1000);
                    $('#msform')[0].reset();

                    $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");
                    next_fs.show();
                    //hide the current fieldset with style
                    current_fs.animate({ opacity: 0 }, {
                        step: function (now) {
                            // for making fielset appear animation
                            opacity = 1 - now;
                            current_fs.css({
                                'display': 'none',
                                'position': 'relative'
                            });
                            next_fs.css({ 'opacity': opacity });
                        },
                        duration: 500
                    });
                    setProgressBar(++current);
                    // refreshing the table
                    refreshTable();
               
                }
                else {
                    alert(result.message);
                }
            });
        }
    });

    $(".previous").click(function () {
        current_fs = $(this).parent();
        previous_fs = $(this).parent().prev();
        //Remove class active
        $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");
        //show the previous fieldset
        previous_fs.show();
        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;
                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                previous_fs.css({ 'opacity': opacity });
            },
            duration: 500
        });
        setProgressBar(--current);
    });
    function setProgressBar(curStep) {
        var percent = parseFloat(100 / steps) * curStep;
        percent = percent.toFixed();
        $(".progress-bar")
            .css("width", percent + "%")
    }
    $(".submit").click(function () {
        return false;
    })


    // Initializing the datatable 
    datatableinstance = $("#contacttable").DataTable({
        "order": [[0, 'desc']],
        "processing": true,
        "serverSide": true,
        "filter": true,

        "ajax": {
            "url": "/home/GetContacts",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            {
                "data": "firstName",
                "name": "firstName",
                "autoWidth": true,
                "render": function (data, type, row) {
                    console.log(row);
                    return row.firstName + " " + row.lastName;
                }
            },
            {
                "data": "gender",
                "name": "Gender",
                "autoWidth": true,
                "render": function (data, type, row) {
                    return row.gender?.substring(0, 1).toUpperCase();
                }
            },

            { "data": "email", "name": "Email", "autoWidth": true },
            { "data": "reason", "name": "Reason", "autoWidth": true },
            {
                "data": "Id",
                "name": "Id",
                "autoWidth": true,
                "render": function (data, type, row) { return "<a href='javascript:void(0);' class='btn btn-primary'  onclick=getContactdetailsById('" + row.id + "'); >Info</a>"; }
            },
        ]
    });

    $('#btnnewcontact').click(function () {
        current = 1;
        $('#addcontactmodal').modal("show");
        $('#msform')[0].reset();
    })

    $('#dismismodaladdContact').click(function () {
        window.location.reload();
    })

    $('#dismismodal').click(function () {
        $('.contactdetailsModal').modal("hide");
    });
});
// Refresh table 
function refreshTable() {
    contactTable.ajax.reload();
}
function getContactdetailsById(id) {

    $.get("/home/getcontactdetailsbyid?id=" + id, function (result) {
        $('.contactdetailsModal').modal("show");
        for (const key in result) {
            if (result.hasOwnProperty(key)) {
                $('#' + key).text(result[key]);
            }
        }
    });
}
