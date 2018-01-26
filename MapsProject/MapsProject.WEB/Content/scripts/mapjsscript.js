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
        preventDefault();
    });
}

function getMapDataByServer(tags) {
    clearMapFromMarker();
    var url;
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
    $('#savePlaceName').val('Enter name');
    $('#savePlaceTags').val('Enter tags');
    $('#savePlaceLatitude').val(latLng.lat());
    $('#savePlaceLongitude').val(latLng.lng());
    map.panTo(latLng);
}

// Save or edit object.
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

    var input = $('#searchPlaceTags');

    // Создаем общий блок с классом
    var val_cont = document.createElement('div');
    $(val_cont).addClass("dropdown");

    // Создаем кнопку открытия списка и поле для записи значений
    $(val_cont).append("<a href='javascript:void(0);'><span class='open'>Select tags</span><span class='value'></span></a>");


    // Создаем выпадающий список и вкладываем в общий блок
    var ul = document.createElement('ul');

    for (elem in val_data) {
        $(ul).append("<li><input type='checkbox' value='" + val_data[elem] + "' id='" + val_data[elem] + "'><label for='" + val_data[elem] + "'>" + val_data[elem] + "</label></li>");
    }
    $(ul).appendTo(val_cont);
    $(ul).hide();

    // Размещаем общий блок после нужного input-а
    $(input).after(val_cont);

    // Скрываем/открываем выпадающий список
    $(".dropdown a").on('click', function () {
        $(".dropdown ul").slideToggle('fast');
        $(val_cont).toggleClass("checkbox-list");
    });

    $('.dropdown ul input[type="checkbox"]').on('click', function () {
        var inputValue, innerObj = {};

        /* проверяем value текстового инпута. это необходимо для очистки
           от лишних запятых при удалении всех элементов и накликивания
           чекбоксов заново. если эту проверку не делать, то пустой инпут
           добавляется как пустой элемент массива */
        if (input.val()) {
            /* если инпут не пустой, то закидываем данные из него в массив
               по разделителю "; " */
            inputValue = input.val().split(';')
        } else {
            inputValue = []; // если пустой - присваиваем переменно пустой массив
        };

        /* промежуточный объект нам необходим для составления массива
           только с уникальными элементами */
        inputValue.forEach(function (item) {
            innerObj[item] = true;
        });

        /* если чекбокс активен — добавляем его value как ключ к объекту, 
           а если нет — удаляем этот ключ */
        if ($(this).is(':checked')) {
            innerObj[$(this).val()] = true;
        } else {
            delete innerObj[$(this).val()];
        }

        inputValue = Object.keys(innerObj); // преобразуем ключи объекта в массив
        input.val(inputValue.join(';')); // преобразуем массив в строку, разделяя элементы "; " и записываем в value инпута
    });

    $('.check').click(function () {
        var valuesArray = input.val().split(';'), // собираем данные из инпута в массив, разделитель "; "
            $checkboxes = $(ul).find('li input').removeClass('protected'); // удаляем со всех инпутов класс

        $.each(valuesArray, function (index, value) { // проходимся циклом по собранному массиву из инпутов
            $checkboxes.each(function () { // для каждого значение запускаем цикл по всем чекбоксам
                if ($(this).val() === value) { // и если value инпута равно элементу из собранного массива 
                    $(this).prop('checked', true).addClass('protected'); // "чекаем" чекбокс и добавляем ему класс, чтобы на следующем условии чекбокс не стал обратно не выделенным

                    return true; // уходим на следующую итерацию
                } else if (!$(this).hasClass('protected')) { // если у чекбокса нет класса protected
                    $(this).prop('checked', false); // то снимаем выделение с чекбокса
                }
            });
        });
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
}

function showSuccessSaveOrChange() {
    $('#savePlaceInfo').attr('class', 'alert alert-success');
    $('#savePlaceInfo').text('Place added for moderation.');
    $('#savePlaceInfo').show('slow');
    setTimeout(function () { $('#savePlaceInfo').hide('slow'); }, 2000);
    getMapDataByServer("");
}