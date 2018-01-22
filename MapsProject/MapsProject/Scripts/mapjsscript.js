/**
    * Скрипт для отрисовки карты, маркеров, действий с объектами.
    * Содержит методы инициализации карты, отрисовки маркеров, добавления и редактирования объектов,
    * отображения объектов по категориям, поиска объекта по адресу.
 */

var map;
var geocoder;
var markers = [];
var idValueIsEmpty;
var infowindow;
var marker;

$(document).ready(function () {
    initMap();
    getMapDataByServer("");
    getCategories();
});

$("#savePlace").click(function (event) {
    event.preventDefault();
    savePlace();
});
$('#searchPlacesByCategory').click(function (event) {
    event.preventDefault();
    showPlaceByCategory();
});
$('#searchPlaceByAdress').click(function (event) {
    event.preventDefault();
    getPlaceByAdress();
});

//Функция инициализация карты.
function initMap() {
    markers = [];
    geocoder = new google.maps.Geocoder();
    google.maps.visualRefresh = true;

    var Minsk = new google.maps.LatLng(53.887895, 27.538710);

    var mapOptions = {
        zoom: 15,
        center: Minsk
    };

    map = new google.maps.Map(document.getElementById("canvas"), mapOptions);

    map.addListener('click', function (e) {
        if (infowindow) {
            infowindow.close();
        }
        placeMarkerAndPanTo(e.latLng, map);
    });
}

// Функция получения данных от сервера.
function getMapDataByServer(category) {
    clearMapFromMarker();
    var url;
    //Если в функцию передана категория, то получим список объектов данной категории.
    if (category == "" || category == undefined) {
        url = '/api/values';
    } else {
        url = '/api/values?category=' + category;
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
                    content: "<div class='objectInfo'><h2>Place: " + item.ObjectName + "</h2>"
                    + "<div><h4>Category: " + item.Category + "</h4></div>"
                    + "</div>"
                });

                infowindow.open(map, marker);
                $('#savePlaceId').val(item.Id);
                $("#savePlaceName").val(item.ObjectName);
                $("#savePlaceCategory").val(item.Category);
                $("#savePlaceLatitude").val(marker.getPosition().lat());
                $("#savePlaceLongitude").val(marker.getPosition().lng());
            });
        });
    });
}

// Функция отрисовки маркеров.
function placeMarkerAndPanTo(latLng, map) {
    //Удаляем предыдущий маркер.
    if (marker) {
        var index = markers.indexOf(marker);
        markers.splice(index, 1);
        markers.length = markers.length - 1;
        marker.setMap(null);
    }
    marker = new google.maps.Marker({
        position: latLng,
        map: map
    });
    $('#savePlaceId').val("");
    $("#savePlaceName").val("Enter name");
    $("#savePlaceCategory").val("Enter category");
    $("#savePlaceLatitude").val(latLng.lat());
    $("#savePlaceLongitude").val(latLng.lng());
    map.panTo(latLng);
}

// Функция для сохранения или редактирования объекта.
function savePlace() {
    var place = {
        ObjectName: $('#savePlaceName').val(),
        Category: $('#savePlaceCategory').val(),
        GeoLat: $('#savePlaceLatitude').val(),
        GeoLong: $("#savePlaceLongitude").val(),
        Status: 'Need moderate'
    };
    var requestType;
    var url;
    var idValue = $('#savePlaceId').val();
    idValueIsEmpty = idValue == "" ? true : false;
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
        contentType: "application/json;charset=utf-8",
        success: showSuccessSaveOrChange(),
        error: function (x, y, z) {
            showErrorSaveOrChange(x, y, z)
        }
    });
    
}

//Функция для получения категорий.
function getCategories() {
    var dynamicSelect = $('#categories');
    dynamicSelect.empty();
    $.ajax({
        url: '/api/category',
        type: 'GET',
        success: function (data) {
            dynamicSelect.append(data);
        },
        error: function (x, y, z) {
            alert(x + " " + y + " " + z)
        }
    });
}

function showPlaceByCategory() {
    getMapDataByServer($('#searchPlaceCategory').val());
}

function getPlaceByAdress() {
    var address = $('#searchPlaceAdress').val();
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
    $("#savePlaceInfo").attr('class', 'alert alert-danger');
    $("#savePlaceInfo").text(x + " " + y + " " + z);
    $("#savePlaceInfo").show('slow');
    setTimeout(function () { $("#savePlaceInfo").hide('slow'); }, 2000);
}

function showSuccessSaveOrChange() {
    $("#savePlaceInfo").attr('class', 'alert alert-success');
    if (idValueIsEmpty) {
        $("#savePlaceInfo").text('Place added for moderation.');
    } else {
        $("#savePlaceInfo").text('Changes added for moderation.');
    }
    $("#savePlaceInfo").show('slow');
    setTimeout(function () { $("#savePlaceInfo").hide('slow'); }, 2000);
    getMapDataByServer("");
}