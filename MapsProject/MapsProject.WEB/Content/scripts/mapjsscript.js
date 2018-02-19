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

    //var polygonCoords = [
    //    new google.maps.LatLng(-58.5717277526855, 6.75720977783203),
    //    new google.maps.LatLng(-58.5143966674805, 6.7439661026001),
    //    new google.maps.LatLng(-58.536449432373, 6.73580884933472),
    //    new google.maps.LatLng(-58.5831718444824, 6.72692441940308),
    //    new google.maps.LatLng(-58.6032447814941, 6.44053840637207),
    //    new google.maps.LatLng(-52.5602645874023, 5.29054975509644),
    //    new google.maps.LatLng(-52.7203712463379, 5.28919172286987),
    //    new google.maps.LatLng(-52.5695495605469, 5.28308486938477),
    //    new google.maps.LatLng(-52.7073707580566, 5.28301048278809 ),
    //    new google.maps.LatLng(-52.7248916625977, 5.27624607086182),
    //    new google.maps.LatLng(-52.5549964904785, 5.27327871322632),
    //    new google.maps.LatLng(-52.3008117675781, 4.95884084701538),
    //    new google.maps.LatLng(-52.317626953125, 4.95810031890869),
    //    new google.maps.LatLng(-52.2659454345703, 4.94687938690186),
    //    new google.maps.LatLng(-52.261604309082, 4.94159841537476),
    //    new google.maps.LatLng(-52.165885925293, 4.89228534698486),
    //    new google.maps.LatLng(-52.1708526611328, 4.88959312438965 ),
    //    new google.maps.LatLng(-50.2029151916504, 0.616409301757813),
    //    new google.maps.LatLng(-58.5717277526855, 6.75720977783203)
    //];
    var polygonCoords = [
        new google.maps.LatLng(6.75720977783203,-58.5717277526855),
        new google.maps.LatLng(6.7439661026001,-58.5143966674805),
        new google.maps.LatLng(6.73580884933472,-58.536449432373),
        new google.maps.LatLng(6.72692441940308,-58.5831718444824),
        new google.maps.LatLng(6.44053840637207, -58.6032447814941),
        new google.maps.LatLng(6.75720977783203, -58.5717277526855)  //del
        //new google.maps.LatLng(5.29054975509644,-52.5602645874023),
        //new google.maps.LatLng(5.28919172286987,-52.7203712463379),
        //new google.maps.LatLng(5.28308486938477,-52.5695495605469),
        //new google.maps.LatLng(5.28301048278809,-52.7073707580566),
        //new google.maps.LatLng(5.27624607086182,-52.7248916625977),
        //new google.maps.LatLng(5.27327871322632,-52.5549964904785),
        //new google.maps.LatLng(4.95884084701538,-52.3008117675781),
        //new google.maps.LatLng(4.95810031890869,-52.317626953125),
        //new google.maps.LatLng(4.94687938690186,-52.2659454345703),
        //new google.maps.LatLng(4.94159841537476,-52.261604309082),
        //new google.maps.LatLng(4.89228534698486,-52.165885925293),
        //new google.maps.LatLng(4.88959312438965,-52.1708526611328),
        //new google.maps.LatLng(0.616409301757813,-50.2029151916504),
        //new google.maps.LatLng(6.75720977783203,-58.5717277526855)
    ];
    var polygon = new google.maps.Polyline({
        path: polygonCoords, // Координаты
        strokeColor: "#FF0000", // Цвет обводки
        strokeOpacity: 0.8, // Прозрачность обводки
        strokeWeight: 2, // Ширина обводки
        fillColor: "#FF0000", // Цвет заливки
        fillOpacity: 0.35 // Прозрачность заливки
    });

    polygon.setMap(map);
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