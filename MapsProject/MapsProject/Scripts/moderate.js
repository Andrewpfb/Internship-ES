$(document).ready(function () {
    getData();
});

function getData() {
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
                + '<th>' + '<a id="deletePlaceLink" data-item="' + item.Id + '"onclick="delPlace(this)">Delete</a></th>'
                + '</tr > '
            );
        });
    });
}

function delPlace(el) {
    var id = $(el).attr('data-item');
    deletePlace(id);
}

function deletePlace(id) {
    debugger;
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