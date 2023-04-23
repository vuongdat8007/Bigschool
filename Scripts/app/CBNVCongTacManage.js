function fetchCBNVs() {
    $.ajax({
        url: "/api/CBNVCongTacApi",
        method: "GET",
        dataType: "json",
        success: function (data) {
            //console.log(data)
            var cbnvSelect = $("#maCBNV");
            (data.CBNVs).forEach(function (cbnvCongTac) {
                var option = $("<option></option>")
                    .attr("value", cbnvCongTac.MaCBNV)
                    .text(cbnvCongTac.HoTen);
                cbnvSelect.append(option);
            });

            var chucvuSelect = $('#maChucVu');
            (data.ChucVuAvails).forEach(function (data) {
                var option = $("<option></option>")
                    .attr("value", data.MaChucVu)
                    .text(data.TenChucVu);
                chucvuSelect.append(option);
            });
        },
        error: function (error) {
            console.error("Error fetching CBNVs:", error);
        }
    });
}

$(document).ready(function () {
    fetchCBNVs();
    let table = $('#cbnvCongTacTable').DataTable({
        ajax: {
            url: '/api/CBNVCongTacApi',
            dataSrc: 'CBNVCongTacs'
        },
        columns: [
            { data: 'MaCBNV' },
            { data: 'MaChucVu' },
            {
                data: 'NgayBatDauCongTac',
                render: function (data, type, row) {
                    // Convert the adjusted date to a string in the format "DD-MM-YYYY"
                    return moment(data).format("DD-MM-YYYY");
                }
            },
            {
                data: 'NgayKetThucCongTac',
                render: function (data, type, row) {
                    // Convert the adjusted date to a string in the format "DD-MM-YYYY"
                    return moment(data).format("DD-MM-YYYY");
                }
            },
            { data: 'TenTruong' },
            { data: 'GhiChu' },
            {
                data: 'CBNVCongTacId',
                render: function (data, type, row) {
                    return `
                        <button class="btn btn-sm btn-primary edit" data-id="${data}" id="editCBNVCongTac-${data}">Edit</button>
                        <button class="btn btn-sm btn-danger delete" data-id="${data}" id="deleteCBNVCongTac-${data}">Delete</button>
                    `;
                }
            }
        ]
    });

    $('#cbnvCongTacForm').validate({
        submitHandler: function (form, event) {
            event.preventDefault();

            let cbnvCongTacData = {
                maCBNV: $('#maCBNV').val(),
                maChucVu: $('#maChucVu').val(),
                ngayBatDauCongTac: $('#ngayBatDauCongTac').val(),
                ngayKetThucCongTac: $('#ngayKetThucCongTac').val(),
                tenTruong: $('#tenTruong').val(),
                ghiChu: $('#ghiChu').val()
            };

            let id = $('#cbnvCongTacId').val();

            if (id) {
                cbnvCongTacData.CBNVCongTacId = id;
                // Update the existing record
                $.ajax({
                    url: '/api/CBNVCongTacApi/' + id,
                    method: 'PUT',
                    data: cbnvCongTacData
                }).done(function () {
                    table.ajax.reload();
                    resetForm();
                }).fail(function (jqXHR) {
                    console.error('Error updating CBNVCongTac:', jqXHR.responseText);
                });
            } else {
                // Create a new record
                $.post('/api/CBNVCongTacApi', cbnvCongTacData)
                    .done(function () {
                        table.ajax.reload();
                        resetForm();
                    })
                    .fail(function (jqXHR) {
                        console.error('Error creating CBNVCongTac:', jqXHR.responseText);
                    });
            }
        }
    });

    $('#cbnvCongTacTable').on('click', '.edit', function () {
        let id = $(this).data('id');
        $.getJSON('/api/CBNVCongTacApi/' + id, function (data) {
            $('#cbnvCongTacId').val(data.CBNVCongTacId);
            $('#maCBNV').val(data.CBNVCongTac.MaCBNV);
            $('#maChucVu').val(data.CBNVCongTac.MaChucVu);
            $('#ngayBatDauCongTac').val(moment(data.CBNVCongTac.NgayBatDauCongTac).format("YYYY-MM-DD"));
            $('#ngayKetThucCongTac').val(moment(data.CBNVCongTac.NgayKetThucCongTac).format("YYYY-MM-DD"));
            $('#tenTruong').val(data.CBNVCongTac.TenTruong);
            $('#ghiChu').val(data.CBNVCongTac.GhiChu);
        });
    });

    $('#cbnvCongTacTable').on('click', '.delete', function () {
        let id = $(this).data('id');
        if (confirm('Are you sure you want to delete this record?')) {
            $.ajax({
                url: '/api/CBNVCongTacApi/' + id,
                method: 'DELETE'
            }).done(function () {
                table.ajax.reload();
            }).fail(function (jqXHR) {
                console.error('Error deleting CBNVCongTac:', jqXHR.responseText);
            });
        }
    });

    $('#resetForm').on('click', function () {
        resetForm();
    });

    /*$("#maCBNV").on("change", function () {
        var maCBNV = $(this).val();
        
        if (maCBNV) {
            $.ajax({
                url: "/api/CBNV/" + maCBNV,
                type: "GET",
                success: function (data) {
                    var chucVus = data.ChucVus;
                    var chucVuSelect = $("#maChucVu");
                    chucVuSelect.empty();

                    chucVus.forEach(function (chucVu) {
                        chucVuSelect.append($("<option></option>")
                            .attr("value", chucVu.MaChucVu)
                            .text(chucVu.TenChucVu));
                    });
                },
                error: function (xhr, status, error) {
                    console.error("Error fetching ChucVus:", error);
                }
            });
        }
    });*/

    function resetForm() {
        $('#cbnvCongTacId').val('');
        $('#maCBNV').val('');
        $('#maChucVu').val('');
        $('#ngayBatDauCongTac').val('');
        $('#ngayKetThucCongTac').val('');
        $('#tenTruong').val('');
        $('#ghiChu').val('');
    }
});