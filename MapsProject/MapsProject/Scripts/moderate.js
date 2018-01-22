$(document).ready(function () {
    getData();
});

function getData() {
    $.getJSON('/api/moderate/', function (data) {
        $.each(data, function (i, item) {
            $('#moderateTable > tbody:last-child').append(
                '<tr>'
                + '<th id="itemId">' + item.Id + '</th>'
                + '<th id="itemName">' + item.ObjectName + '</th>'
                + '<th id="itemCategory">' + item.Category + '</th>'
                + '<th id="itemLat">' + item.GeoLat + '</th>'
                + '<th id="itemLong">' + item.GeoLong + '</th>'
                + '<th id="itemStatus">' + item.Status + '</th>'
                + '<th><a id="approvedPlaceLink" data-item="' + item.Id + '"onclick="appPlace(this)">Approved</a></th>'
                + '<th><a id="deletePlaceLink" data-item="' + item.Id + '"onclick="delPlace(this)">Delete</a></th>'
                + '</tr > '
            );
        });
    });
}

//function appPlace(el){
//    var id = $(el).attr('data-item');
//    approvedPlace(id);
//}

function delPlace(el) {
    var id = $(el).attr('data-item');
    deletePlace(id);
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

//function approvedPlace(id) {
//    var place = {
//        Id: id,
//        ObjectName: $('#itemName').val(),
//        Category: $('#itemCategory').val(),
//        GeoLat: $('#itemLat').val(),
//        GeoLong: $('#itemLong').val(),
//        Status: 'Approved'
//    }

//    var requestType = 'PUT';
//    url = '/api/values/' + id;
//    $.ajax({
//        url: url,
//        type: requestType,
//        data: JSON.stringify(place),
//        contentType: "application/json;charset=utf-8",
//        success: function () {
//            alert('Approved')
//        },
//        error: function (x, y, z) {
//            showErrorSaveOrChange(x, y, z)
//        }
//    });
//}