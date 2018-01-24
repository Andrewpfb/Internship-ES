/**
    * Скрипт для отрисовки карты, маркеров, действий с объектами.
    * Содержит методы инициализации карты, отрисовки маркеров, добавления и редактирования объектов,
    * отображения объектов по категориям, поиска объекта по адресу.
 */

var map;
var markers = [];
var infowindow;
var marker;

$(document).ready(function () {
    initMap();
    getMapDataByServer('');
    getCategories();
});

$('#savePlace').click(function (event) {
    event.preventDefault();
    savePlace();
});
$('#searchPlacesByTags').click(function (event) {
    event.preventDefault();
    showPlaceByTags();
});
$('#searchPlaceByAdress').click(function (event) {
    event.preventDefault();
    getPlaceByAdress();
});

//Функция инициализация карты.
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
        $('#PlaceSaveBlock').attr('class', 'visible');
        preventDefault();
    });
}

// Функция получения данных от сервера.
function getMapDataByServer(tags) {
    clearMapFromMarker();
    var url;
    //Если в функцию переданы тэги, то получим список объектов с такими тэгами.
    if (tags == '' || tags == undefined) {
        url = '/api/values';
    } else {
        url = '/api/values?tags=' + tags;
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
                $('#PlaceSaveBlock').attr('class', 'visible');
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
        });
    });
}

// Функция отрисовки маркеров.
function placeMarkerAndPanTo(latLng, map) {
    //Удаляем предыдущий маркер.
    if (marker) {
        marker.setMap(null);
    }
    marker = new google.maps.Marker({
        position: latLng,
        map: map
    });
    $('#savePlaceId').val('');
    $('#savePlaceName').val('Enter name');
    $('#savePlaceTags').val('Enter tags');
    $('#savePlaceLatitude').val(latLng.lat());
    $('#savePlaceLongitude').val(latLng.lng());
    map.panTo(latLng);
}

// Функция для сохранения или редактирования объекта.
function savePlace() {
    var place = {
        ObjectName: $('#savePlaceName').val(),
        Tags: $('#savePlaceTags').val(),
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
        url = '/api/values/';
    } else {
        requestType = 'PUT';
        place.Id = idValue;
        url = '/api/values/' + idValue;
    }
    $.ajax({
        url: url,
        type: requestType,
        data: JSON.stringify(place),
        contentType: 'application/json;charset=utf-8',
        success: showSuccessSaveOrChange(),
        error: function (x, y, z) {
            showErrorSaveOrChange(x, y, z)
        }
    });

}

//Функция для получения категорий.
function getCategories() {
    var dynamicSelect = $('#tags');
    dynamicSelect.empty();
    $.ajax({
        url: '/api/tags',
        type: 'GET',
        success: function (data) {
            dynamicSelect.append(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z)
        }
    });
}

function showPlaceByTags() {
    clearMapFromMarker();
    getMapDataByServer($('#searchPlaceTags').val());
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
    $('#PlaceSaveBlock').attr('class', 'invisible');
}

function showSuccessSaveOrChange() {
    $('#PlaceSaveBlock').attr('class', 'invisible');
    $('#savePlaceInfo').attr('class', 'alert alert-success');
    $('#savePlaceInfo').text('Place added for moderation.');
    $('#savePlaceInfo').show('slow');
    setTimeout(function () { $('#savePlaceInfo').hide('slow'); }, 2000);
    $('#PlaceSaveBlock').attr('class', 'invisible');
    getMapDataByServer("");
}