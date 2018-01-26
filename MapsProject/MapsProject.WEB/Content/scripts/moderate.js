/**
    * Script for rendering the table of disapproved objects.
    * Contains methods for deleting and confirming objects.
*/

$(document).ready(function () {
    loadTable();
});

function loadTable() {
    $("#moderateTable").dataTable({
        "ajax": {
            "url": '/api/moderate',
            "dataSrc": ''
        },
        "columns": [
            { data: 'Id' },
            { data: 'ObjectName' },
            { data: 'Tags' },
            { data: 'GeoLong' },
            { data: 'GeoLat' },
            { data: 'Status' },
            { data: 'DeleteLink' },
            { data: 'ApprovedLink' }
        ]
    });
}

function delPlace(el) {
    $('#confirmDelModalWin').modal('show');
    $('#deletePlace').click(function () {
        var id = $(el).attr('data-item-id');
        deletePlace(id);
        $('#confirmDelModalWin').modal('hide');
    });
}

function appPlace(el) {
    $('#confirmAppModalWin').modal('show');
    $('#approvePlace').click(function () {
        var id = $(el).attr('data-item-id');
        approvedPlace(id);
        $('#confirmAppModalWin').modal('hide');
    });
}

function deletePlace(id) {
    $.ajax({
        url: '/api/values/' + id,
        type: 'DELETE',
        contentType: 'application/json;charset=utf-8',
        success: function (data) {
            $("#moderateTable").dataTable().api().ajax.reload();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function approvedPlace(id) {
    var place = {
        Id: id,
        Status: 'Approved'
    };
    $.ajax({
        url: '/api/moderate/' + id,
        type: 'PUT',
        data: JSON.stringify(place),
        contentType: 'application/json;charset=utf-8',
        success: function (data) {
            $("#moderateTable").dataTable().api().ajax.reload();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
