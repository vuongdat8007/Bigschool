$(document).ready(function () {
    //fetchCBNVs();
    // Init datatable
    /*fetchCBNVs().then(function () {
        // Initialize the DataTable after the fetchCBNVs() function has completed
        $('#cbnvTable').DataTable({
            dom: 'Bfrtip',
            responsive: true,
            fixedHeader: true,
            colReorder: true,
            //autoFill: true,
            scroller: true,
            //searchPanes: true,
            buttons: [
                'copy', 'print', 'excel', 'pdf', 'csv'
            ],
            });
    }).catch(function () {
            console.error("Error initializing DataTable after fetching CBNVs");
    });*/

    $(window).scroll(function () {
        var scrollTop = $(window).scrollTop();
        var imgPos = scrollTop / 2 + 'px';
        $('.background').css('background-position', 'center ' + imgPos);
    });

    $.when(fetchCBNVs()).done(function () {
        $('#cbnvTable').DataTable({
            dom: 'Bfrtip',
            responsive: true,
            fixedHeader: true,
            colReorder: true,
            scroller: true,
            buttons: [
                'copy', 'print', 'excel', 'pdf', 'csv'
            ],
        });
    }).fail(function () {
        console.error("Error initializing DataTable after fetching CBNVs");
    });

    $("#addNewCBNV").on("click", function () {
        clearCBNVForm();
        $("#cbnvModalLabel").text("Add New CBNV");
    });

    $("#saveCBNV").on("click", function () {
        var isValid = $("#cbnvForm").valid();
        if (isValid) {
            saveCBNV();
        }
    });

    $("#cbnvTable").on("click", ".editCBNV", function () {
        var id = $(this).data("id");
        getCBNVById(id);
        $("#cbnvModalLabel").text("Edit CBNV");
    });

    $("#cbnvTable").on("click", ".deleteCBNV", function () {
        var id = $(this).data("id");
        deleteCBNV(id);
    });
    $("#saveCBNV").on("click", function () {
        var isValid = $("#cbnvForm").valid();
        if (isValid) {
            saveCBNV();
        }
    });

    $("#cbnvTable").on("click", ".editCBNV", function () {
        var id = $(this).data("id");
        getCBNVById(id);
        $("#cbnvModalLabel").text("Edit CBNV");
    });

    $("#cbnvTable").on("click", ".deleteCBNV", function () {
        var id = $(this).data("id");
        deleteCBNV(id);
    });
});

/*function fetchCBNVs() {
    $.ajax({
        url: "/api/CBNV",
        type: "GET",
        dataType: "json",
        success: function (data) {
            var tbody = $("#cbnvTable tbody");
            tbody.empty();
            for (var i = 0; i < data.length; i++) {
                var cbnv = data[i];
                var row = createCBNVRow(cbnv);
                tbody.append(row);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error fetching CBNVs:", status, error);
        }
    });
};*/

/*function fetchCBNVs() {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: "/api/CBNV",
            type: "GET",
            dataType: "json",
            success: function (data) {
                var tbody = $("#cbnvTable tbody");
                tbody.empty();
                for (var i = 0; i < data.length; i++) {
                    var cbnv = data[i];
                    var row = createCBNVRow(cbnv);
                    tbody.append(row);
                }
                resolve(); // Resolve the promise after the data has been fetched and processed
            },
            error: function (xhr, status, error) {
                console.error("Error fetching CBNVs:", status, error);
                reject(); // Reject the promise if an error occurs during the AJAX request
            }
        });
    });
}*/

function fetchCBNVs() {
    return $.ajax({
        url: "/api/CBNV",
        type: "GET",
        dataType: "json",
        success: function (data) {
            var tbody = $("#cbnvTable tbody");
            tbody.empty();
            for (var i = 0; i < data.length; i++) {
                var cbnv = data[i];
                var row = createCBNVRow(cbnv);
                tbody.append(row);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error fetching CBNVs:", status, error);
        }
    });
};

function createCBNVRow(cbnv) {
    var row = $("<tr></tr>");
    row.append($("<td></td>").text(cbnv.MaCBNV));
    row.append($("<td></td>").text(cbnv.HoTen));

    // Create a new Date object from the date string
    var ngaySinh = new Date(cbnv.NgaySinh);
    // Convert the date to a string in the format "DD-MM-YYYY"
    var ngaySinhString = formatDateToDDMMYYYY(ngaySinh);
    row.append($("<td></td>").text(ngaySinhString));

    row.append($("<td></td>").text(cbnv.NoiSinh));
    row.append($("<td></td>").text(cbnv.GioiTinh));
    row.append($("<td></td>").text(cbnv.QueQuan));
    row.append($("<td></td>").text(cbnv.SoCMND));
    row.append($("<td></td>").text(cbnv.HoKhau));
    row.append($("<td></td>").text(cbnv.DiaChi));
    row.append($("<td></td>").text(cbnv.QuocTich));
    row.append($("<td></td>").text(cbnv.DanToc));
    row.append($("<td></td>").text(cbnv.TonGiao));
    row.append($("<td></td>").text(cbnv.TrinhDoVanHoa));
    row.append($("<td></td>").text(cbnv.DienThoai));
    row.append($("<td></td>").text(cbnv.Email));

    // Create a new Date object from the date string
    var ngayVaoTruong = new Date(cbnv.NgayVaoTruong);
    // Convert the date to a string in the format "DD-MM-YYYY"
    var ngayVaoTruongString = formatDateToDDMMYYYY(ngayVaoTruong);
    row.append($("<td></td>").text(ngayVaoTruongString));

    row.append($("<td></td>").text(cbnv.ThamNienCongTac));
    // Add other fields as needed

    var actions = $("<td></td>");
    actions.append(createEditButton(cbnv.MaCBNV));
    actions.append(createDeleteButton(cbnv.MaCBNV));
    row.append(actions);

    return row;
};

function createEditButton(id) {
    var editBtn = $("<button></button>")
        .addClass("btn btn-warning btn-sm editCBNV")
        .data("id", id)
        .text("Edit");
    return editBtn;
};

function createDeleteButton(id) {
    var deleteBtn = $("<button></button>")
        .addClass("btn btn-danger btn-sm deleteCBNV")
        .data("id", id)
        .text("Delete");
    return deleteBtn;
};

function clearCBNVForm() {
    $("#cbnvId").val("");
    $("#hoTen").val("");
    $("#ngaySinh").val("");
    $("#noiSinh").val("");
    $("#gioiTinh").val("");
    $("#queQuan").val("");
    $("#soCMND").val("");
    $("#hoKhau").val("");
    $("#diaChi").val("");
    $("#quocTich").val("");
    $("#danToc").val("");
    $("#tonGiao").val("");
    $("#trinhDoVanHoa").val("");
    $("#dienThoai").val("");
    $("#email").val("");
    $("#ngayVaoTruong").val("");
    $("#thamNienCongTac").val("");
    // Clear other fields as needed
};

function getCBNVById(id) {
    $.ajax({
        url: "/api/CBNV/" + id,
        type: "GET",
        dataType: "json",
        success: function (data) {
            $("#cbnvId").val(data.MaCBNV);
            $("#hoTen").val(data.HoTen);

            // Create a new Date object from the date string
            var ngaySinh = new Date(data.NgaySinh);

            // Adjust the date by the timezone offset
            ngaySinh.setMinutes(ngaySinh.getMinutes() - ngaySinh.getTimezoneOffset());

            // Convert the adjusted date to a string in the format "YYYY-MM-DD"
            var ngaySinhString = ngaySinh.toISOString().slice(0, 10);
            
            //console.log("aaaaa " + data.NgaySinh);
            $("#ngaySinh").val(ngaySinhString);
            $("#noiSinh").val(data.NoiSinh);
            $("#gioiTinh").val(data.GioiTinh);
            $("#queQuan").val(data.QueQuan);
            $("#soCMND").val(data.SoCMND);
            $("#hoKhau").val(data.HoKhau);
            $("#diaChi").val(data.DiaChi);
            $("#quocTich").val(data.QuocTich);
            $("#danToc").val(data.DanToc);
            $("#tonGiao").val(data.TonGiao);
            $("#trinhDoVanHoa").val(data.TrinhDoVanHoa);
            $("#dienThoai").val(data.DienThoai);
            $("#email").val(data.Email);
            $("#ngayVaoTruong").val(data.NgayVaoTruong);
            $("#thamNienCongTac").val(data.ThamNienCongTac);
            // Set other fields as needed
            $("#cbnvModal").modal("show");
        },
        error: function (xhr, status, error) {
            console.error("Error fetching CBNV:", status, error);
        }
    });
};

function saveCBNV() {
    var cbnv = {
        MaCBNV: $("#cbnvId").val(),
        HoTen: $("#hoTen").val(),
        NgaySinh: $("#ngaySinh").val(),
        NoiSinh: $("#noiSinh").val(),
        GioiTinh: $("#gioiTinh").val(),
        QueQuan: $("#queQuan").val(),
        SoCMND: $("#soCMND").val(),
        HoKhau: $("#hoKhau").val(),
        DiaChi: $("#diaChi").val(),
        QuocTich: $("#quocTich").val(),
        DanToc: $("#danToc").val(),
        TonGiao: $("#tonGiao").val(),
        TrinhDoVanHoa: $("#trinhDoVanHoa").val(),
        DienThoai: $("#dienThoai").val(),
        Email: $("#email").val(),
        NgayVaoTruong: $("#ngayVaoTruong").val(),
        ThamNienCongTac: $("#thamNienCongTac").val(),
        // Set other fields as needed
    };

    if (cbnv.MaCBNV) {
        // Update existing CBNV
        $.ajax({
            url: "/api/CBNV/" + cbnv.MaCBNV,
            type: "PUT",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(cbnv),
            success: function (data) {
                fetchCBNVs();
                $("#cbnvModal").modal("hide");
            },
            error: function (xhr, status, error) {
                console.error("Error updating CBNV:", status, error);
            }
        });
    } else {
        // Add new CBNV
        $.ajax({
            url: "/api/CBNV",
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(cbnv),
            success: function (data) {
                fetchCBNVs();
                $("#cbnvModal").modal("hide");
            },
            error: function (xhr, status, error) {
                console.error("Error adding CBNV:", status, error);
            }
        });
    }
};

function deleteCBNV(id) {
    if (confirm("Are you sure you want to delete this CBNV?")) {
        $.ajax({
            url: "/api/CBNV/" + id,
            type: "DELETE",
            dataType: "json",
            success: function (data) {
                fetchCBNVs();
            },
            error: function (xhr, status, error) {
                console.error("Error deleting CBNV:", status, error);
            }
        });
    }
};

function parseDateWithoutTimezone(dateString) {
    var dateComponents = dateString.split(/[-T]/);
    return new Date(dateComponents[0], dateComponents[1] - 1, dateComponents[2]);
};

function formatDateToDDMMYYYY(date) {
    var day = ('0' + date.getDate()).slice(-2);
    var month = ('0' + (date.getMonth() + 1)).slice(-2);
    var year = date.getFullYear();

    return day + '/' + month + '/' + year;
};