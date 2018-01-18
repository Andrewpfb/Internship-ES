$(document).ready(function () {
    GetMap();

    $("#savePlace").click(function (event) {
        event.preventDefault();
        AddPlace();
    });
});


function GetMap() {

    google.maps.visualRefresh = true;

    var Minsk = new google.maps.LatLng(53.887895, 27.538710);

    var mapOptions = {
        zoom: 15,
        center: Minsk
    };

   
    var map = new google.maps.Map(document.getElementById("canvas"), mapOptions);

    $.getJSON('/api/values/', function (data) {
        $.each(data, function (i, item) {
            var marker = new google.maps.Marker({
                'position': new google.maps.LatLng(item.GeoLat,item.GeoLong),
                'map': map,
                'title': item.ObjectName
            });

            //TODO: Сделать прилично. Обертки jquery.
            var infowindow = new google.maps.InfoWindow({
                content: "<div class='objectInfo'><h2>Place: " + item.ObjectName + "</h2>"
                + "<div><h4>Category: " + item.Category + "</h4></div>"
                + "<div><img src=/Content/images/" + item.PlaceImg + " style=height:150px;width:250px;></src></div>"
                + "</div>"
            });

            google.maps.event.addListener(marker, 'click', function () {
                infowindow.open(map, marker);
                $("#addPlaceName").prop('value', item.ObjectName);
                $("#addPlaceCategory").prop('value', item.Category);
                $("#addPlaceLatitude").prop('value', marker.getPosition().lat());
                $("#addPlaceLongitude").prop('value', marker.getPosition().lng());
            });
        })
    });

    map.addListener('click', function (e) {
        placeMarkerAndPanTo(e.latLng, map);
    });

    function placeMarkerAndPanTo(latLng, map) {
        var marker = new google.maps.Marker({
            position: latLng,
            map: map
        });
        $("#addPlaceLatitude").prop('value', latLng.lat());
        $("#addPlaceLongitude").prop('value', latLng.lng());
        map.panTo(latLng);
    }    
}

function AddPlace() {
    // получаем значения для добавляемой книги
    debugger;
    var place = {
        ObjectName: $('addPlaceName').val(),
        Category: $('#addPlaceCategory').val(),
        GeoLat: $('#addPlaceLatitude').val(),
        GeoLong: $("#addPlaceLongitude").val()
    };
    debugger;
    $.ajax({
        url: '/api/values/',
        type: 'POST',
        data: JSON.stringify(place),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetMap();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}