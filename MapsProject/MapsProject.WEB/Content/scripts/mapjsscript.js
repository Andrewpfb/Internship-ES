/**
    * Script for drawing a map, markers, actions with objects.
    * Contains methods for initializing the map, drawing markers, adding and editing objects,
    * displaying objects by category, searching for an object at the address.
 */



var map;
var markers = [];

// Глобальные потому что используются в разных участках кода. Например, перед отрисовкой нового маркера 
// убрать старый, перед отрисовкой infowindow убрать старое и т.д.
var infowindow;
var marker;

$(document).ready(function () {
    initMap();
    getMapDataByServer('');
    getTags();
});

$('#savePlace').click(function () {
    savePlace();
});
$('#searchPlacesByTags').click(function () {
    showPlaceByTags();
});
$('#searchPlaceByAdress').click(function () {
    getPlaceByAdress();
});

function initMap() {
    markers = [];

    google.maps.visualRefresh = true;

    var Minsk = new google.maps.LatLng(53.887895, 27.538710);

    var mapOptions = {
        zoom: 15,
        center: Minsk
    };

    map = new google.maps.Map(document.getElementById('canvas'), mapOptions);

    map.addListener('dblclick', function (e) {
        if (infowindow) {
            infowindow.close();
        }
        placeMarkerAndPanTo(e.latLng, map);
        $('#savePlaceModalWin').modal('show');
        e.preventDefault();
    });
}

function getMapDataByServer(tags) {
    clearMapFromMarker();
    var url;
    if (tags == '' || tags == undefined) {
        url = '/api/map';
    } else {
        url = '/api/map?tags=' + tags;
    }
    $.getJSON(url, function (data) {
        $.each(data, function (i, item) {
            var marker = new google.maps.Marker({
                'position': new google.maps.LatLng(item.GeoLat, item.GeoLong),
                'map': map,
                'title': item.ObjectName
            });
            markers.push(marker);

            google.maps.event.addListener(marker, 'click', function () {
                if (infowindow) {
                    infowindow.close();
                }

                infowindow = new google.maps.InfoWindow({
                    content: '<div class="objectInfo"><h2>Place: ' + item.ObjectName + '</h2>'
                    + '<div><h4>Tags: ' + item.Tags + '</h4></div>'
                    + '</div>'
                });

                infowindow.open(map, marker);
                $('#savePlaceId').val(item.Id);
                $('#savePlaceName').val(item.ObjectName);
                $('#savePlaceTags').val(item.Tags);
                $('#savePlaceLatitude').val(marker.getPosition().lat());
                $('#savePlaceLongitude').val(marker.getPosition().lng());
            });
            google.maps.event.addListener(marker, 'dblclick', function () {
                $('#savePlaceId').val(item.Id);
                $('#savePlaceName').val(item.ObjectName);
                $('#savePlaceTags').val(item.Tags);
                $('#savePlaceLatitude').val(marker.getPosition().lat());
                $('#savePlaceLongitude').val(marker.getPosition().lng());
                $('#savePlaceModalWin').modal('show');
            });
        });
    });
}

function placeMarkerAndPanTo(latLng, map) {
    //Remove old marker.
    if (marker) {
        marker.setMap(null);
    }
    marker = new google.maps.Marker({
        position: latLng,
        map: map
    });
    $('#savePlaceId').val('');
    $('#savePlaceName').val('');
    $('#savePlaceLatitude').val(latLng.lat());
    $('#savePlaceLongitude').val(latLng.lng());
    map.panTo(latLng);
}

// Save or edit object.
function savePlace() {
    var place = {
        ObjectName: $('#savePlaceName').val(),
        Tags: getTagsFromSelect('#saveEditSelectTags'),
        GeoLat: $('#savePlaceLatitude').val(),
        GeoLong: $('#savePlaceLongitude').val(),
        Status: 'Need moderate'
    };
    var requestType;
    var url;
    var idValue = $('#savePlaceId').val();
    var idValueIsEmpty = idValue == "" ? true : false;
    if (idValueIsEmpty) {
        requestType = 'POST';
        url = '/api/map/';
    } else {
        requestType = 'PUT';
        place.Id = idValue;
        url = '/api/map/' + idValue;
    }
    $.ajax({
        url: url,
        type: requestType,
        data: JSON.stringify(place),
        contentType: 'application/json;charset=utf-8',
        success: function () {
            $('#savePlaceModalWin').modal('hide');
            showSuccessSaveOrChange();
        },
        error: function (x, y, z) {
            showErrorSaveOrChange(x, y, z);
        }
    });

}

function getTags() {
    $.ajax({
        url: '/api/tags',
        type: 'GET',
        success: function (data) {
            setTagList(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z)
        }
    });
}

function setTagList(val_data) {

    for (elem in val_data) {
        $('.js-example-basic-multiple').append('<option>' + val_data[elem].TagName + '</option>');
    }

    $('.js-example-basic-multiple').select2({
        placeholder: "Select the tags",
        tags: true
    });
}

function showPlaceByTags() {
    clearMapFromMarker();
    getMapDataByServer(getTagsFromSelect('#searchSelectTags'));
}

function getTagsFromSelect(elementId) {
    var tags = $(elementId).find(':selected');
    var requestTagString = '';
    for (tag in tags) {
        if (typeof (tags[tag].nodeName) == 'undefined') {
            requestTagString = requestTagString.slice(0, -2);
            break;
        }
        requestTagString += tags[tag].innerText + "; ";
    }
    return requestTagString;
}

function getPlaceByAdress() {
    clearMapFromMarker();
    var address = $('#searchPlaceAdress').val();
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == 'OK') {
            map.setCenter(results[0].geometry.location);
            markers.push(marker);
            placeMarkerAndPanTo(results[0].geometry.location, map);
        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}

function clearMapFromMarker() {
    for (var i = 0; i < markers.length; i++) {
        if (markers[i]) {
            markers[i].setMap(null);
        }
    }
    markers = [];
}

function showErrorSaveOrChange(x, y, z) {
    $('#savePlaceInfo').attr('class', 'alert alert-danger');
    $('#savePlaceInfo').text(x + " " + y + " " + z);
    $('#savePlaceInfo').show('slow');
    setTimeout(function () { $('#savePlaceInfo').hide('slow'); }, 2000);
}

function showSuccessSaveOrChange() {
    $('#savePlaceInfo').attr('class', 'alert alert-success');
    $('#savePlaceInfo').text('Place added for moderation.');
    $('#savePlaceInfo').show('slow');
    setTimeout(function () { $('#savePlaceInfo').hide('slow'); }, 2000);
    getMapDataByServer("");
}