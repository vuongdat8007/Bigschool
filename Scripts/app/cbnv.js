var cbnvTable, cbnvList;
$(document).ready(function () {
    $.validator.addMethod("phoneVN", function (value, element) {
        // Vietnamese phone number pattern
        var pattern = /((09|03|07|08|05)+([0-9]{8})\b)/g;
        return this.optional(element) || pattern.test(value);
    }, "Please enter a valid VN phone number");

    $.validator.addMethod("emailDomain", function (value, element) {
        // Regular expression to match email with specific domain
        var emailRegEx = /^[\w-]+(\.[\w-]+)*@[A-Za-z0-9-]+(\.[A-Za-z0-9]+)*(\.[A-Za-z]{2,})$/;
        return this.optional(element) || emailRegEx.test(value);
    }, "Please enter a valid email address with the correct domain.");

    // Initialize the validation
    var validator = $("#cbnvForm").validate({
        onfocusout: function (element) {
            this.element(element);
        },
        errorClass: "invalid-feedback",
        errorElement: "div",
        rules: {
            email: {
                required: true,
                email: true,
                emailDomain: true
            },
            dienThoai: {
                required: true,
                phoneVN: true
            },
            soCMND: {
                required: true,
                minlength: 9,
                maxlength: 12,
                number: true
            }
        },
        messages: {
            email: {
                required: "Please enter an email address",
                email: "Please enter a valid email address",
                emailDomain: "Please enter valid domain name of your email hosting"
            },
            dienThoai: {
                required: "Please enter a phone number",
                phoneVN: "Please enter a valid VN phone number"
            },
            soCMND: {
                required: "Please enter an ID number",
                minlength: "ID number must be between 9 and 12 characters",
                maxlength: "ID number must be between 9 and 12 characters",
                number: "Please enter a valid ID number"
            }
        },
        highlight: function (element, errorClass) {
            $(element).addClass("is-invalid").removeClass("is-valid");
        },
        unhighlight: function (element, errorClass) {
            $(element).addClass("is-valid").removeClass("is-invalid");
        },
        errorPlacement: function (error, element) {
            /*error.addClass("invalid-feedback");
            element.closest(".form-group").append(error);*/
            error.addClass("invalid-feedback");
            if (element.is("select")) {
                error.insertAfter(element.next("label"));
            } else {
                error.insertAfter(element);
            }
        },
        submitHandler: function (form, event) {
            event.preventDefault(); // Prevent the default form submission behavior
            saveCBNV(); // Call the saveCBNV function when the form is valid
        }
    });

    $(window).scroll(function () {
        var scrollTop = $(window).scrollTop();
        var imgPos = scrollTop / 2 + 'px';
        $('.background').css('background-position', 'center ' + imgPos);
    });

    /*$.when(fetchCBNVs(), fetchChuyenNganhs()).done(function () {
        cbnvTable = $('#cbnvTable').DataTable({
            dom: 'Blfrtip',
            buttons: [
                'copy', 'csv', 'excel', 'pdf', 'print'
            ],
            responsive: true,
            deferRender: true,
        });
    }).fail(function () {
        console.error("Error initializing DataTable after fetching CBNVs");
    });*/

    $.when(fetchCBNVs(), fetchChuyenNganhs()).done(function () {
        //console.log(cbnvList);
        cbnvTable = $('#cbnvTable').DataTable({
            dom: 'Blfrtip',
            buttons: [
                'copy', 'csv', 'excel', 'pdf', 'print'
            ],
            responsive: true,
            deferRender: true,
            data: cbnvList, // Add the data source
            columns: [
                { data: 'CBNV.MaCBNV' },
                { data: 'CBNV.HoTen' },
                { data: 'CBNV.NgaySinh' },
                { data: 'CBNV.NoiSinh' },
                { data: 'CBNV.GioiTinh' },
                { data: 'CBNV.QueQuan' },
                { data: 'CBNV.SoCMND' },
                { data: 'CBNV.HoKhau' },
                { data: 'CBNV.DiaChi' },
                { data: 'CBNV.QuocTich' },
                { data: 'CBNV.DanToc' },
                { data: 'CBNV.TonGiao' },
                { data: 'CBNV.TrinhDoVanHoa' },
                {
                    data: 'ChuyenNganhs',
                    render: function (data, type, row) {
                        var chuyenNganhs = data;
                        var chuyenNganhText;
                        if (!Array.isArray(chuyenNganhs) || chuyenNganhs.length === 0) {
                            chuyenNganhText = "Không"; // Default text when there are no ChuyenNganhs
                        } else {
                            chuyenNganhText = chuyenNganhs.map(function (chuyenNganh) {
                                return chuyenNganh.TenChuyenNganh;
                            }).join(', ');
                        }
                        return chuyenNganhText;
                    }
                },
                { data: 'CBNV.DienThoai' },
                { data: 'CBNV.Email' },
                { data: 'CBNV.NgayVaoTruong' },
                { data: 'CBNV.ThamNienCongTac' },
                // Add other fields as needed
                {
                    data: 'CBNV.BankingInfo',
                    render: function (data, type, row) {
                        //console.log(data);
                        if (data) {
                            if ((data.PaymentMethod + "").match(/^\d+$/)) {
                                //it's all digits
                                return getPaymentMethodString(data.PaymentMethod);
                            } else {
                                return data.PaymentMethod;
                            }
                        } else {
                            return '-';
                        }
                        //return data ? getPaymentMethodString(data.PaymentMethod) : '-';
                    }
                },
                {
                    data: 'BankingInfo',
                    render: function (data, type, row) {
                        return data ? data.BankName : '-';
                    }
                },
                {
                    data: 'BankingInfo',
                    render: function (data, type, row) {
                        return data ? data.AccountNumber : '-';
                    }
                },
                {
                    data: 'BankingInfo',
                    render: function (data, type, row) {
                        return data ? data.AccountHolderName : '-';
                    }
                },
                {
                    data: 'BankingInfo',
                    render: function (data, type, row) {
                        return data ? data.Branch : '-';
                    }
                },
                {
                    data: 'BankingInfo',
                    render: function (data, type, row) {
                        return data ? data.SwiftCode : '-';
                    }
                },
                {
                    data: 'CBNV',
                    render: function (data, type, row) {
                        var id = data.MaCBNV;
                        return '<button class="btn btn-warning btn-sm editCBNV" data-id="' + id + '">Edit</button> ' +
                            '<button class="btn btn-danger btn-sm deleteCBNV" data-id="' + id + '">Delete</button>' +
                            "<br class='d-md-none' />" +
                            "<button class='btn btn-primary btn-sm ml-2' onclick='openBangCapModal(`${id}`)'>Q.Lý Bằng Cấp</button>";
                    }
                }
                /*{ data: 'CBNV.BankingInfos[0].PaymentMethod', defaultContent: '-' },
                { data: 'CBNV.BankingInfos[0].BankName', defaultContent: '-' },
                { data: 'CBNV.BankingInfos[0].AccountNumber', defaultContent: '-' },
                { data: 'CBNV.BankingInfos[0].AccountHolderName', defaultContent: '-' },
                { data: 'CBNV.BankingInfos[0].Branch', defaultContent: '-' },
                { data: 'CBNV.BankingInfos[0].SwiftCode', defaultContent: '-' },*/
            ],
            createdRow: function (row, data, dataIndex) {
                // Set the ID attribute of the row element using the MaCBNV value from the data source
                row.id = data.CBNV.MaCBNV;
            }
        });
    }).fail(function () {
        console.error("Error initializing DataTable after fetching CBNVs");
    });

    $("#addNewCBNV").on("click", function () {
        clearCBNVForm();
        $("#cbnvModalLabel").text("Add New CBNV");
    });

    $('#cbnvTable').on('click', '.editCBNV', function () {
        var tr = $(this).closest('tr');
        var row = cbnvTable.row(tr);
        var rowData = row.data();
        
        //console.log(rowData); // Log the rowData to see its structure

        // If rowData is still undefined, try accessing the hidden row data
        if (rowData === undefined) {
            rowData = cbnvTable.row(tr.prev()).data();
            //console.log("aaa" + rowData); // Log the rowData again to see its structure
        }
        //var id = rowData[0]; // Because of responsive mode, data stored in an array
        var id = rowData.CBNV.MaCBNV; 
        getCBNVById(id);
        $("#cbnvModalLabel").text("Edit CBNV");
    });

    $("#cbnvTable").on("click", ".deleteCBNV", function () {
        var tr = $(this).closest('tr');
        var row = cbnvTable.row(tr);
        var rowData = row.data();

        //console.log(rowData); // Log the rowData to see its structure

        // If rowData is still undefined, try accessing the hidden row data
        if (rowData === undefined) {
            rowData = cbnvTable.row(tr.prev()).data();
            //console.log("aaa" + rowData); // Log the rowData again to see its structure
        }
        //var id = rowData[0]; // Because of responsive mode, data stored in an array
        var id = $(this).data('id');
        // Set the data-id attribute of the confirm delete button
        $('#confirmDelete').data({ 'id': id, 'row': row });
        // Show the delete confirmation modal
        $('#deleteConfirmModal').modal('show');
    });

    /*validateCBNVForm();
    $("#cbnvForm").on("submit", function (e) {
        e.preventDefault(); // Prevent the default form submission
        if ($(this).valid()) {
            saveCBNV();
        }
    });*/

    // Save button click handler
    /*$("#saveCBNV").on("click", function () {
        var isValid = validator.form(); // Check if the form is valid
        if (isValid) {
            saveCBNV(); // Call the saveCBNV function when the form is valid
        }
    });*/

    // Form submit handler
    $("#cbnvForm").off("submit").on("submit", function (event) {
        event.preventDefault(); // Prevent the form from submitting by default

        var isValid = validator.form(); // Check if the form is valid
        if (isValid) {
            saveCBNV(); // Call the saveCBNV function when the form is valid
        }
    });

    // Save button click handler
    $("#saveCBNV").off("click").on("click", function (event) {
        event.preventDefault(); // Prevent the button from submitting the form by default

        // Trigger the form's submit event
        $("#cbnvForm").submit();
    });

    $('#confirmDelete').on('click', function () {
        var id = $(this).data('id');
        deleteCBNV(id);
    });

   /* $("[name='quocTich']").select2({
        placeholder: "Chọn quốc gia...",
        width: '100%',
        templateResult: function (country) {
            if (!country.id) {
                return country.text;
            }
            return $(
                "<span><i class=\"fi fi-" + country.id.toLowerCase() + "\"></i> " + country.text + "</span>"
            );
        },
        templateSelection: function (country) {
            return $(
                "<span><i class=\"fi fi-" + country.id.toLowerCase() + "\"></i> " + country.text + "</span>"
            );
        },
        data: countriesDataSource,
    });*/

    $('#cbnvModal').on('shown.bs.modal', function () {
        $('#quocTich').select2({
            // Your Select2 options
            placeholder: "Chọn quốc gia...",
            width: '100%',
            templateResult: function (country) {
                if (!country.id) {
                    return country.text;
                }
                return $(
                    "<span><i class=\"fi fi-" + country.id.toLowerCase() + "\"></i> " + country.text + "</span>"
                );
            },
            templateSelection: function (country) {
                return $(
                    "<span><i class=\"fi fi-" + country.id.toLowerCase() + "\"></i> " + country.text + "</span>"
                );
            },
            data: countriesDataSource,
        }).trigger('change'); // Trigger the change event to show the selected value
    });

    // Destroy Select2 when the modal is hidden
    $('#cbnvModal').on('hidden.bs.modal', function () {
        $('#quocTich').select2('destroy');
    });

    $('select').on('select2-opening', function () {
        var container = $(this).select2('container')
        var position = $(this).select2('container').offset().top
        var avail_height = $(window).height() - container.offset().top - container.outerHeight()

        // The 50 is a magic number here. I think this is the search box + other UI
        // chrome from select2?
        $('ul.select2-results').css('max-height', (avail_height - 50) + px)
    })

    $("[name='bankName']").select2({
        placeholder: "Chọn ngân hàng...",
        data: vnBankNames,
    });

});

function openBangCapModal(cbnvId) {
    // Fetch the list of BangCapCBNVChuyenNganh records for the given CBNV
    $.ajax({
        url: '/api/BangCapCBNVChuyenNganh/' + cbnvId,
        type: "GET",
        success: function (data) {
            // Add your logic to render the list of BangCapCBNVChuyenNganh records in the modal or inline form
            console.log(data);
        },
        error: function (error) {
            console.error("Error fetching data:", error);
        }
    });

    // Show the modal or inline form
    $("#bangCapModal").modal("show");
};

function fetchCertificates(cbnvId) {
    $.ajax({
        url: '/api/BangCapCBNVChuyenNganh/' + cbnvId,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            // Clear the table and populate it with the fetched data
        },
        error: function (error) {
            console.error('Error fetching certificates:', error);
        }
    });
};

function addCertificate(certificate) {
    $.ajax({
        url: '/api/BangCapCBNVChuyenNganh',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(certificate),
        success: function (data) {
            // Update the table with the new data
        },
        error: function (error) {
            console.error('Error adding certificate:', error);
        }
    });
};

function updateCertificate(certificate) {
    $.ajax({
        url: '/api/BangCapCBNVChuyenNganh/' + certificate.Id,
        type: 'PUT',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(certificate),
        success: function (data) {
            // Update the table with the updated data
        },
        error: function (error) {
            console.error('Error updating certificate:', error);
        }
    });
};

function deleteCertificate(id) {
    $.ajax({
        url: '/api/BangCapCBNVChuyenNganh/' + id,
        type: 'DELETE',
        success: function (data) {
            // Remove the deleted record from the table
        },
        error: function (error) {
            console.error('Error deleting certificate:', error);
        }
    });
};

var countriesDataSource = [
    { id: 'CA', text: 'Canada' },
    { id: 'VN', text: 'Việt Nam' },
    { id: 'LA', text: 'Lào' },
    { id: 'CM', text: 'Campuchia' },
    { id: 'US', text: 'United States' },
    { id: 'JP', text: 'Japan' }
];

function fetchCBNVs() {
    return $.ajax({
        url: "/api/CBNV",
        type: "GET",
        dataType: "json",
        success: function (data) {
            //console.log("Received data:", data); // Add this line to inspect the data

            for (var i = 0; i < data.length; i++) {
                data[i].CBNV.QuocTich = $.fn.getTextFromCountryID(data[i].CBNV.QuocTich);
            }
            cbnvList = data;
            /*var tbody = $("#cbnvTable tbody");
            tbody.empty();
            for (var i = 0; i < data.length; i++) {
                var cbnv = data[i];
                //console.log("Received cbnv:", cbnv);
                var row = createCBNVRow(cbnv);
                tbody.append(row);
            }*/
        },
        error: function (xhr, status, error) {
            console.error("Error fetching CBNVs:", status, error);
        }
    });
};

function fetchChuyenNganhs() {
    return $.ajax({
        url: "/api/ChuyenNganh",
        type: "GET",
        dataType: "json",
        success: function (data) {
            var select = $("#chuyenNganh");
            select.empty();
            for (var i = 0; i < data.length; i++) {
                var chuyenNganh = data[i];
                var option = $("<option></option>")
                    .val(chuyenNganh.MaChuyenNganh)
                    .text(chuyenNganh.TenChuyenNganh);
                select.append(option);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error fetching Chuyen Nganhs:", status, error);
        }
    });
}

function createCBNVRow(data) {
    var row = $("<tr></tr>");
    var cbnv = data.CBNV;
    if (cbnv.MaCBNV !== null && cbnv.MaCBNV !== undefined) {
        row.attr("id", cbnv.MaCBNV);
    }
    row.append($("<td></td>").text(cbnv.MaCBNV));
    row.append($("<td></td>").text(cbnv.HoTen));

    /*// Create a new Date object from the date string
    var ngaySinh = new Date(cbnv.NgaySinh);
    // Convert the date to a string in the format "DD-MM-YYYY"
    var ngaySinhString = formatDateToDDMMYYYY(ngaySinh);
    row.append($("<td></td>").text(ngaySinhString));*/
    // Use moment.js to parse and format the date string
    var ngaySinhString = moment(cbnv.NgaySinh).format('DD-MM-YYYY');
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
    var chuyenNganhs = data.ChuyenNganhs;
    var chuyenNganhText;
    if (chuyenNganhs.length === 0) {
        chuyenNganhText = "Không"; // Default text when there are no ChuyenNganhs
    } else {
        chuyenNganhText = chuyenNganhs.map(function (chuyenNganh) {
            return chuyenNganh.TenChuyenNganh;
        }).join(', ');
    }
    row.append($("<td></td>").text(chuyenNganhText));
    row.append($("<td></td>").text(cbnv.DienThoai));
    row.append($("<td></td>").text(cbnv.Email));

    /*// Create a new Date object from the date string
    var ngayVaoTruong = new Date(cbnv.NgayVaoTruong);
    // Convert the date to a string in the format "DD-MM-YYYY"
    var ngayVaoTruongString = formatDateToDDMMYYYY(ngayVaoTruong);
    row.append($("<td></td>").text(ngayVaoTruongString));*/

    // Use moment.js to parse and format the date string
    var ngayVaoTruongString = moment(cbnv.NgayVaoTruong).format('DD-MM-YYYY');
    row.append($("<td></td>").text(ngayVaoTruongString));
    row.append($("<td></td>").text(cbnv.ThamNienCongTac));
    if (cbnv.BankingInfo) {
        row.append($("<td></td>").text(getPaymentMethodString(cbnv.BankingInfo.PaymentMethod)));
        row.append($("<td></td>").text(cbnv.BankingInfo.BankName));
        row.append($("<td></td>").text(cbnv.BankingInfo.AccountNumber));
        row.append($("<td></td>").text(cbnv.BankingInfo.AccountHolderName));
        row.append($("<td></td>").text(cbnv.BankingInfo.Branch));
        row.append($("<td></td>").text(cbnv.BankingInfo.SwiftCode));
    } else {
        row.append($("<td></td>").text("-"));
        row.append($("<td></td>").text("-"));
        row.append($("<td></td>").text("-"));
        row.append($("<td></td>").text("-"));
        row.append($("<td></td>").text("-"));
        row.append($("<td></td>").text("-"));
    }
    
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
    $('#bankName').val('');
    $('#accountNumber').val('');
    $('#accountHolderName').val('');
    $('#branch').val('');
    $('#swiftCode').val('');
    $('#paymentMethod').val('Cash');
    // Clear other fields as needed
};

function getCBNVById(id) {
    $.ajax({
        url: "/api/CBNV/" + id,
        type: "GET",
        dataType: "json",
        success: function (dataraw) {
            data = dataraw.CBNV;
            
            $("#cbnvId").val(data.MaCBNV);
            $("#hoTen").val(data.HoTen);

            // Create a Moment.js object from the date string
            var ngaySinh = moment(data.NgaySinh);

            // Convert the adjusted date to a string in the format "DD-MM-YYYY"
            var ngaySinhString = ngaySinh.format("YYYY-MM-DD");
            
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
            var chuyenNganhIds = data.CBNVChuyenNganhs.map(function (cbnvChuyenNganh) {
                return cbnvChuyenNganh.MaChuyenNganh;
            });
            $("#chuyenNganh").val(chuyenNganhIds);
            $("#dienThoai").val(data.DienThoai);
            $("#email").val(data.Email);

            // Create a Moment.js object from the date string
            var ngayVaoTruong = moment(data.NgayVaoTruong);

            // Convert the adjusted date to a string in the format "DD-MM-YYYY"
            var ngayVaoTruongString = ngayVaoTruong.format("DD-MM-YYYY");
            //console.log("data received: " + ngayVaoTruongString);
            $("#ngayVaoTruong").val(ngayVaoTruongString);
            $("#thamNienCongTac").val(data.ThamNienCongTac);

            // Populate the banking information fields
            if (data.BankingInfo) {
                $('#bankName').val(data.BankingInfo.BankName);
                $('#accountNumber').val(data.BankingInfo.AccountNumber);
                $('#accountHolderName').val(data.BankingInfo.AccountHolderName);
                $('#branch').val(data.BankingInfo.Branch);
                $('#swiftCode').val(data.BankingInfo.SwiftCode);
                $('#paymentMethod').val(getPaymentMethodString(data.BankingInfo.PaymentMethod));
            } else {
                $('#bankName').val('');
                $('#accountNumber').val('');
                $('#accountHolderName').val('');
                $('#branch').val('');
                $('#swiftCode').val('');
                $('#paymentMethod').val('Cash');
            }
            // Set other fields as needed
            $("#cbnvModal").modal("show");
        },
        error: function (xhr, status, error) {
            console.error("Error fetching CBNV:", status, error);
        }
    });
};

function saveCBNV() {
    var chuyenNganhList = [];
    $('#chuyenNganh option:selected').each(function (index, option) {
        chuyenNganhList.push({ 'MaChuyenNganh': option.value, 'TenChuyenNganh': option.text });
    });

    // Use moment.js to parse and format the NgayVaoTruong date string
    var ngayVaoTruong = $("#ngayVaoTruong").val();
    var ngayVaoTruongFormatted = moment(ngayVaoTruong, "DD-MM-YYYY").format("YYYY-MM-DD");

    var bankingInfos = {
            PaymentMethod: $("#paymentMethod").val(),
            BankName: $("#bankName").val(),
            AccountNumber: $("#accountNumber").val(),
            AccountHolderName: $("#accountHolderName").val(),
            Branch: $("#branch").val(),
            SwiftCode: $("#swiftCode").val(),
        };
    var cbnv = {
        CBNV: {
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
            NgayVaoTruong: ngayVaoTruongFormatted,
            ThamNienCongTac: $("#thamNienCongTac").val(),
            BankingInfo: bankingInfos,
            ChuyenNganhs: chuyenNganhList
            // Set other fields as needed
            },
            ChuyenNganhs: chuyenNganhList,
    };
    //cbnv.CBNV.BankingInfo = bankingInfos; // Send the bankingInfo object directly
    cbnv.BankingInfo = bankingInfos; // Send the bankingInfo object directly

    // Check that cbnv object has all required properties
    /*if (!cbnv.MaCBNV || !cbnv.HoTen || !cbnv.NgaySinh || !cbnv.ChuyenNganhs) {
        console.error("Error saving CBNV: Required properties are missing.");
        return;
    }*/

    if (cbnv.CBNV.MaCBNV) {
        // Update existing CBNV
        $.ajax({
            url: "/api/CBNV/" + cbnv.CBNV.MaCBNV,
            type: "PUT",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(cbnv),
            success: function (data) {
                // Redraw the DataTable with the updated data
                /*fetchCBNVs().then(function () {
                    cbnvTable.draw();
                });

                $("#cbnvModal").modal("hide");*/
                //console.log("data: " + data.CBNV.MaCBNV);
                // Remove the existing row from the DataTable
                //var rowData = cbnvTable.row("#" + cbnv.CBNV.MaCBNV).data();
                cbnvTable.rows("#" + cbnv.CBNV.MaCBNV, "#" + cbnv.CBNV.MaCBNV + ' .child').remove().draw();

                // Add the updated data as a new row and redraw the DataTable
                //cbnvTable.row.add(data.CBNV).draw();
                //var rowData = createCBNVRow(cbnv);
                //console.log(cbnv);
                cbnvTable.row.add(cbnv).draw();
               
                $("#cbnvModal").modal("hide");
            },
            error: function (xhr, status, error) {
                console.error("Error updating CBNV:", xhr.responseText, status, error);
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
                // Redraw the DataTable with the updated data
                /*fetchCBNVs().then(function () {
                    cbnvTable.draw();;
                });*/
                cbnvTable.row.add(data).draw();
                $("#cbnvModal").modal("hide");
            },
            error: function (xhr, status, error) {
                console.error("Error adding CBNV:", status, error);
            }
        });
    }
};

function deleteCBNV(id) {
    //if (confirm("Are you sure you want to delete this CBNV?")) {  // Already have modal to confirm the delete action
        $.ajax({
            url: "/api/CBNV/" + id,
            type: "DELETE",
            dataType: "json",
            success: function (data) {
                // Destroy the existing DataTable
                //$('#cbnvTable').destroy();

                // Fetch the updated data and re-create the DataTable
                fetchCBNVs().done(function (data) {
                    $('#deleteConfirmModal').modal('hide');
                    //$('#cbnvTable').dataTable().fnDraw();
                    //var row = $('#' + id);
                    //var subRow = row.next();
                    //cbnvTable.row($('#' + id)).remove().draw(); // remove single row doesnt work??
                    //cbnvTable.row($('#' + id).next()).remove().draw(); // remove single row doesnt work??
                    cbnvTable.rows(['#' + id, '#' + id + ' > .child']).remove().draw();
                }).fail(function () {
                    console.error("Error initializing DataTable after fetching CBNVs");
                });

                //$('#deleteConfirmModal').modal('hide');
                //$('#cbnvTable').dataTable().fnDeleteRow(row); // doesn't work
                
            },
            error: function (xhr, status, error) {
                console.error("Error deleting CBNV:", status, error);
            }
        });
    //}
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

function validateCBNVForm() {
    $("#cbnvForm").validate({
        onfocusout: function (element) {
            this.element(element);
        },
        errorClass: "invalid-feedback",
        errorElement: "div",
        rules: {
            email: {
                required: true,
                email: true
            },
            dienThoai: {
                required: true,
                phoneVN: true
            },
            soCMND: {
                required: true,
                minlength: 9,
                maxlength: 12,
                number: true
            }
        },
        messages: {
            email: {
                required: "Please enter an email address",
                email: "Please enter a valid email address"
            },
            dienThoai: {
                required: "Please enter a phone number",
                phoneVN: "Please enter a valid VN phone number"
            },
            soCMND: {
                required: "Please enter an ID number",
                minlength: "ID number must be between 9 and 12 characters",
                maxlength: "ID number must be between 9 and 12 characters",
                number: "Please enter a valid ID number"
            }
        },
        highlight: function (element, errorClass) {
            $(element).addClass("is-invalid").removeClass("is-valid");
        },
        unhighlight: function (element, errorClass) {
            $(element).addClass("is-valid").removeClass("is-invalid");
        },
        errorPlacement: function (error, element) {
            error.addClass("invalid-feedback");
            element.closest(".form-group").append(error);
        }
    });
};

function getPaymentMethodString(paymentMethodInt) {
    switch (paymentMethodInt) {
        case 0:
            return "BankWire";
        case 1:
            return "Cash";
        default:
            return "Unknown";
    }
}

$.fn.getTextFromCountryID = function (id) {
    var text = '';
    $.each(countriesDataSource, function (index, country) {
        if (country.id === id) {
            text = country.text;
            return false; // Break the loop
        }
    });
    return text;
};

var vnBankNames = [
    { id: "0", text: "......................................................."}, {id: "(CBBANK) Ngân hàng TMCP Xây Dựng", text: "(CBBANK) Ngân hàng TMCP Xây Dựng" }, { id: "(GPBANK) Ngân hàng TMCP Dầu Khí Toàn Cầu", text: "(GPBANK) Ngân hàng TMCP Dầu Khí Toàn Cầu" }, { id: "(OCEANBANK) Ngân hàng TMCP Đại Dương", text: "(OCEANBANK) Ngân hàng TMCP Đại Dương" }, { id: "(AGRIBANK) Ngân hàng TMCP Nông Nghiệp và Phát triển nông thôn Việt Nam", text: "(AGRIBANK) Ngân hàng TMCP Nông Nghiệp và Phát triển nông thôn Việt Nam" }, { id: "(Hong Leong Bank Vietnam) Ngân hàng 100% vốn nước ngoài", text: "(Hong Leong Bank Vietnam) Ngân hàng 100% vốn nước ngoài" }, { id: "(Public Bank) Ngân hàng 100% vốn nước ngoài", text: "(Public Bank) Ngân hàng 100% vốn nước ngoài" }, { id: "(ANZ) Ngân hàng ANZ Việt Nam", text: "(ANZ) Ngân hàng ANZ Việt Nam" }, { id: "(Hong Leong Bank) Hong Leong Bank Vietnam", text: "(Hong Leong Bank) Hong Leong Bank Vietnam" }, { id: "(Standard Chartered Bank) Standard Chartered Việt Nam", text: "(Standard Chartered Bank) Standard Chartered Việt Nam" }, { id: "(SHINHANBANK) Shinhan Bank Vietnam Limited – SHBVN", text: "(SHINHANBANK) Shinhan Bank Vietnam Limited – SHBVN" }, { id: "(HSBC) Hongkong-Shanghai Bank", text: "(HSBC) Hongkong-Shanghai Bank" }, { id: "(COOP BANK) Ngân hàng Hợp tác xã Việt Nam", text: "(COOP BANK) Ngân hàng Hợp tác xã Việt Nam" }, { id: "(VRB) Ngân hàng liên doanh Việt – Nga", text: "(VRB) Ngân hàng liên doanh Việt – Nga" }, { id: "(Indovina Bank) Ngân hàng TNHH Indovina", text: "(Indovina Bank) Ngân hàng TNHH Indovina" }, { id: "(VIETBANK) Ngân hàng TMCP Viet Nam Thương Tín", text: "(VIETBANK) Ngân hàng TMCP Viet Nam Thương Tín" }, { id: "(NCB) Ngân hàng TMCP Quốc Dân", text: "(NCB) Ngân hàng TMCP Quốc Dân" }, { id: "(PGBANK) Ngân hàng TMCP Xăng dầu Petrolimex", text: "(PGBANK) Ngân hàng TMCP Xăng dầu Petrolimex" }, { id: "(SAIGONBANK) Ngân hàng TMCP Sài Gòn Công Thương", text: "(SAIGONBANK) Ngân hàng TMCP Sài Gòn Công Thương" }, { id: "(BAOVIET BANK) Ngân hàng TMCP Bảo Việt", text: "(BAOVIET BANK) Ngân hàng TMCP Bảo Việt" }, { id: "(VIETCAPITAL) Ngân hàng TMCP Bản Việt", text: "(VIETCAPITAL) Ngân hàng TMCP Bản Việt" }, { id: "(KIENLONGBANK) Ngân hàng TMCP Kiên Long", text: "(KIENLONGBANK) Ngân hàng TMCP Kiên Long" }, { id: "(NAMABANK) Ngân hàng TMCP Nam Á", text: "(NAMABANK) Ngân hàng TMCP Nam Á" }, { id: "(VIETABANK) Ngân hàng TMCP Việt Á", text: "(VIETABANK) Ngân hàng TMCP Việt Á" }, { id: "(DONGABANK) Ngân hàng TMCP Đông Á", text: "(DONGABANK) Ngân hàng TMCP Đông Á" }, { id: "(BAC A BANK) Ngân hàng TMCP Bắc Á", text: "(BAC A BANK) Ngân hàng TMCP Bắc Á" }, { id: "(SEABANK) Ngân hàng TMCP Đông Nam Á", text: "(SEABANK) Ngân hàng TMCP Đông Nam Á" }, { id: "(ABBANK) Ngân hàng TMCP An Bình", text: "(ABBANK) Ngân hàng TMCP An Bình" }, { id: "(Lienviet Post Bank) Ngân hàng TMCP Liên Việt", text: "(Lienviet Post Bank) Ngân hàng TMCP Liên Việt" }, { id: "(OCB) Ngân hàng TMCP Phương Đông", text: "(OCB) Ngân hàng TMCP Phương Đông" }, { id: "(TPBANK) Ngân hàng TMCP Tiên Phong", text: "(TPBANK) Ngân hàng TMCP Tiên Phong" }, { id: "(TECHCOMBANK) Ngân hàng TMCP Kỹ Thương", text: "(TECHCOMBANK) Ngân hàng TMCP Kỹ Thương" }, { id: "(PVcomBank) Ngân hàng TMCP PVCombank", text: "(PVcomBank) Ngân hàng TMCP PVCombank" }, { id: "(VIB) Ngân hàng TMCP Quốc Tế", text: "(VIB) Ngân hàng TMCP Quốc Tế" }, { id: "(MSB) Ngân hàng TMCP Hàng Hải", text: "(MSB) Ngân hàng TMCP Hàng Hải" }, { id: "(HDBANK) Ngân hàng TMCP Phát Triển TP HCM", text: "(HDBANK) Ngân hàng TMCP Phát Triển TP HCM" }, { id: "(SHB) Ngân hàng TMCP Sài Gòn Hà Nội", text: "(SHB) Ngân hàng TMCP Sài Gòn Hà Nội" }, { id: "(EXIMBANK) Ngân hàng TMCP Xuất Nhập Khẩu", text: "(EXIMBANK) Ngân hàng TMCP Xuất Nhập Khẩu" }, { id: "(ACB) Ngân hàng TMCP Á Châu", text: "(ACB) Ngân hàng TMCP Á Châu" }, { id: "(SCB) Ngân hàng TMCP Sài Gòn", text: "(SCB) Ngân hàng TMCP Sài Gòn" }, { id: "(VPBANK) Ngân hàng TMCP Việt Nam Thịnh Vượng", text: "(VPBANK) Ngân hàng TMCP Việt Nam Thịnh Vượng" }, { id: "(MBBANK) Ngân hàng TMCP Quân Đội", text: "(MBBANK) Ngân hàng TMCP Quân Đội" }, { id: "(SACOMBANK) Ngân hàng TMCP Sài Gòn Thương Tín", text: "(SACOMBANK) Ngân hàng TMCP Sài Gòn Thương Tín" }, { id: "(VIETCOMBANK) Ngân hàng TMCP Ngoại thương", text: "(VIETCOMBANK) Ngân hàng TMCP Ngoại thương" }, { id: "(BIDV) Ngân hàng TMCP Đầu Tư Phát Triển Việt Nam", text: "(BIDV) Ngân hàng TMCP Đầu Tư Phát Triển Việt Nam" }, { id: "(VIETINBANK) Ngân hàng TMCP Công Thương", text: "(VIETINBANK) Ngân hàng TMCP Công Thương" }
];