$(document).ready(function () {
    fetchCBNV();
    fetchHopDong();
    // Initialize the DataTable
    var cbnvHopDongTable = $('#cbnvHopDongTable').DataTable({
        ajax: {
            url: '/api/CBNVHopDongApi',
            dataSrc: ''
        },
        dom: 'Bflrtip',
        columns: [
            { data: 'ID' },
            { data: 'CBNV.HoTen' },
            { data: 'HopDong.LoaiHopDong' },
            {
                data: 'NgayKyHopDong',
                render: function (data, type, row) {
                    return moment(data).format("DD-MM-YYYY");
                }
            },
            { data: 'NgayKetThucHopDong' },
            { data: 'TinhTrangHopDong' },
            { data: 'GhiChu' },
            {
                data: 'ID',
                render: function (data, type, row) {
                    return '<button class="btn btn-primary btn-edit" data-id="' + data + '">Edit</button> ' +
                        '<button class="btn btn-danger btn-delete" data-id="' + data + '">Delete</button>';
                }
            }
        ]
    });

    // Add event listeners for Edit and Delete buttons
    $('#cbnvHopDongTable tbody').on('click', '.btn-edit', function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/api/CBNVHopDongApi/' + id,
            method: 'GET',
            success: function (data) {
                $('#Id').val(data.ID);
                $('#MaCBNV').val(data.MaCBNV);
                $('#MaHopDong').val(data.MaHopDong);
                $('#NgayKyHopDong').val(moment(data.NgayKyHopDong).format("YYYY-MM-DD"));
                $('#NgayKetThucHopDong').val(moment(data.NgayKetThucHopDong).format("YYYY-MM-DD"));
                $('#TinhTrangHopDong').val(data.TinhTrangHopDong);
                $('#GhiChu').val(data.GhiChu);
                $('#createEditModal').modal('show');
            }
        });
    });

    $('#cbnvHopDongTable tbody').on('click', '.btn-delete', function () {
        var id = $(this).data('id');
        $('#deleteId').val(id);
        $('#deleteModal').modal('show');
    });

    // Add event listeners for Create/Edit/Delete modals
    $('#createEditForm').on('submit', function (e) {
        e.preventDefault();
        var id = $('#Id').val();
        var url = id ? '/api/CBNVHopDongApi/' + id : '/api/CBNVHopDongApi';
        var method = id ? 'PUT' : 'POST';
        $.ajax({
            url: url,
            method: method,
            data: $(this).serialize(),
            success: function () {
                $('#createEditModal').modal('hide');
                cbnvHopDongTable.ajax.reload();
            }
        });
    });

    $('#btnConfirmDelete').on('click', function () {
        var id = $('#deleteId').val();
        $.ajax({
            url: '/api/CBNVHopDongApi/' + id,
            method: 'DELETE',
            success: function () {
                $('#deleteModal').modal('hide');
                cbnvHopDongTable.ajax.reload();
            }
        });
    });

    // Add event listener for the Add New button
    $('#btnAddNew').on('click', function () {
        $('#Id').val('');
        $('#createEditModal').modal('show');
    });
});

function fetchCBNV() {
    $.ajax({
        url: '/api/CBNV',
        type: 'GET',
        success: function (cbnvs) {
            cbnvs.forEach(function (cbnv) {
                $('#MaCBNV').append('<option value="' + cbnv.CBNV.MaCBNV + '">' + cbnv.CBNV.HoTen + '</option>');
            });
        }
    });
};

function fetchHopDong() {
    $.ajax({
        url: '/api/HopDongsApi',
        type: 'GET',
        success: function (hopDongs) {
            hopDongs.forEach(function (hopDong) {
                $('#MaHopDong').append('<option value="' + hopDong.MaHopDong + '">' + hopDong.LoaiHopDong + ' (' + hopDong.MaHopDong + ')' + '</option>');
            });
        }
    });
};