$(document).ready(function () {
    getData();
});

function getData() {
    debugger;
    $("#moderateTable > tbody").empty();
    $.getJSON('/api/moderate/', function (data) {
        $.each(data, function (i, item) {
            $('#moderateTable > tbody:last-child').append(
                '<tr>'
                + '<th>' + item.Id + '</th>'
                + '<th>' + item.ObjectName + '</th>'
                + '<th>' + item.Category + '</th>'
                + '<th>' + item.GeoLat + '</th>'
                + '<th>' + item.GeoLong + '</th>'
                + '<th>' + item.Status + '</th>'
                + '<th>' + '<a id="deletePlaceLink" data-item-id="' + item.Id + '"onclick="delPlace(this)">Delete</a></th>'
                + '<th>' + '<a id="approvedPlaceLink" data-item-id="' + item.Id + '"onclick="appPlace(this)">Approve</a></th>'
                + '</tr > '
            );
        });
    });
}

function delPlace(el) {
    var id = $(el).attr('data-item-id');
    deletePlace(id);
}

function appPlace(el) {
    var id = $(el).attr('data-item-id');
    approvedPlace(id);
}

function deletePlace(id) {
    $.ajax({
        url: '/api/values/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
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
        url: '/api/values/' + id,
        type: 'PUT',
        data: JSON.stringify(place),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            getData();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
