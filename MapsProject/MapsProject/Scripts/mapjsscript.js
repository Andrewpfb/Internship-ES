var map;
var idValueIsEmpty;

$(document).ready(function () {
    initMap();
    getMapDataByServer();
    getCategories();
});

$("#savePlace").click(function (event) {
    event.preventDefault();
    savePlace();
});

function initMap() {
    google.maps.visualRefresh = true;

    var Minsk = new google.maps.LatLng(53.887895, 27.538710);

    var mapOptions = {
        zoom: 15,
        center: Minsk
    };

    map = new google.maps.Map(document.getElementById("canvas"), mapOptions);

    map.addListener('click', function (e) {
        placeMarkerAndPanTo(e.latLng, map);
    });
}

function getMapDataByServer() {
    $.getJSON('/api/values/', function (data) {
        $.each(data, function (i, item) {
            var marker = new google.maps.Marker({
                'position': new google.maps.LatLng(item.GeoLat, item.GeoLong),
                'map': map,
                'title': item.ObjectName
            });

            //TODO: Сделать прилично. Обертки jquery.
            var infowindow = new google.maps.InfoWindow({
                content: "<div class='objectInfo'><h2>Place: " + item.ObjectName + "</h2>"
                + "<div><h4>Category: " + item.Category + "</h4></div>"
                + "</div>"
                //+ "<div><img src=/Content/images/" + item.PlaceImg + " style=height:150px;width:250px;></src></div>"
            });

            google.maps.event.addListener(marker, 'click', function () {
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

function placeMarkerAndPanTo(latLng, map) {
    var marker = new google.maps.Marker({
        position: latLng,
        map: map
    });
    $("#savePlaceName").val("Enter name");
    $("#savePlaceCategory").val("Enter category");
    $("#savePlaceLatitude").val(latLng.lat());
    $("#savePlaceLongitude").val(latLng.lng());
    map.panTo(latLng);
}

function savePlace() {
    var place = {
        ObjectName: $('#savePlaceName').val(),
        Category: $('#savePlaceCategory').val(),
        GeoLat: $('#savePlaceLatitude').val(),
        GeoLong: $("#savePlaceLongitude").val()
    };
    debugger;
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
        success: showSuccess(),
        error: function (x, y, z) {
            showError(x, y, z)
        }
    });
}

function getCategories() {
    var dynamicSelect = $('#categories');
    dynamicSelect.empty();
    debugger;
    $.ajax({
        url: 'api/category',
        type: 'GET',
        success: function (data) {
            dynamicSelect.append(data);
        },
        error: function (x, y, z) {
            alert(x + " " + y + " " + z)
        }
    });
}

function showError(x, y, z) {
    $("#savePlaceInfo").attr('class', 'alert alert-danger');
    $("#savePlaceInfo").text(x + " " + y + " " + z);
    $("#savePlaceInfo").show('slow');
    setTimeout(function () { $("#savePlaceInfo").hide('slow'); }, 2000);
}

function showSuccess() {
    $("#savePlaceInfo").attr('class', 'alert alert-success');
    if (idValueIsEmpty) {
        $("#savePlaceInfo").text('Place added for moderation.');
    } else {
        $("#savePlaceInfo").text('Changes added for moderation.');
    }
    $("#savePlaceInfo").show('slow');
    setTimeout(function () { $("#savePlaceInfo").hide('slow'); }, 2000);
    getMapDataByServer();
}