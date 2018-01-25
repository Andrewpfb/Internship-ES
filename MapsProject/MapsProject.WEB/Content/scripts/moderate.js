/**
    * Скрипт для отрисовки таблицы неподтвержденных объектов.
    * Содержит методы для удаления и подтверждения объектов.
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
    if (confirm("Do you want to delete this place?")) {
        var id = $(el).attr('data-item-id');
        deletePlace(id);
    }

}

function appPlace(el) {
    if (confirm("Do you want to approve this place?")) {
        var id = $(el).attr('data-item-id');
        approvedPlace(id);
    }
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
