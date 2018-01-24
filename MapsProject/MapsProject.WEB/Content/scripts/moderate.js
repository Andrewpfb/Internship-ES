/**
    * Скрипт для отрисовки таблицы неподтвержденных объектов.
    * Содержит методы для удаления и подтверждения объектов.
*/

$(document).ready(function () {
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
            { data: 'Status' } //где добавить?
        ]
    });
}); 

$('confirmAction').click(function (event) {
    var id = $(el).attr('data-item-id');
    deletePlace(id);
});

function getData() {
    $.getJSON('/api/moderate', function (response) {
        $('#moderateTable').dataTable(
            {
                processing: true,
                data: JSON.parse(response.data)
            });
        window.someGlobalOrWhatever = response.balance
    });
}

// Функция для получения данных из сервера.
//function getData() {
//    $('#moderateTable > tbody').empty();
//    $.getJSON('/api/moderate/', function (data) {
//        $.each(data, function (i, item) {
//            $('#moderateTable > tbody:last-child').append(
//                '<tr>'
//                + '<th>' + item.Id + '</th>'
//                + '<th>' + item.ObjectName + '</th>'
//                + '<th>' + item.Tags + '</th>'
//                + '<th>' + item.GeoLat + '</th>'
//                + '<th>' + item.GeoLong + '</th>'
//                + '<th>' + item.Status + '</th>'
//                + '<th>' + '<a id="deletePlaceLink" data-item-id="' + item.Id + '"onclick="delPlace(this)">Delete</a></th>'
//                + '<th>' + '<a id="approvedPlaceLink" data-item-id="' + item.Id + '"onclick="appPlace(this)">Approve</a></th>'
//                + '</tr > '
//            );
//        });
//    });
//}

function delPlace(el) {
    $("#myModalBox").modal('show');
    //var id = $(el).attr('data-item-id');
    //deletePlace(id);
}

function appPlace(el) {
    var id = $(el).attr('data-item-id');
    approvedPlace(id);
}

function deletePlace(id) {
    $.ajax({
        url: '/api/values/' + id,
        type: 'DELETE',
        contentType: 'application/json;charset=utf-8',
        success: function (data) {
            getData();
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
            getData();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
