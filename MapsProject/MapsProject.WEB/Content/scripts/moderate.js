/**
    * Script for rendering the table of disapproved objects.
    * Contains methods for deleting and confirming objects.
*/

var tokenKey = "tokenInfo";

$(document).ready(function () {
    if (sessionStorage.getItem(tokenKey) != undefined) {
        $('#loginDiv').hide();
        $('#TableDiv').show();
        loadTable(sessionStorage.getItem(tokenKey));
    }
});

$('#submit').click(function (e) {
    e.preventDefault();
    var loginData = {
        grant_type: 'password',
        username: $('#name').val(),
        password: $('#password').val()
    };

    $.ajax({
        type: 'POST',
        url: '/token',
        data: loginData
    }).success(function (data) {
        $('#loginDiv').hide();
        $('#TableDiv').show();
        loadTable(data.access_token);
        sessionStorage.setItem(tokenKey, data.access_token);
    }).fail(function (data) {
        $('#errorAuth').text("Auth failed");
        $('#errorAuth').show('slow');
        setTimeout(function () { $('#errorAuth').hide('slow'); }, 2000);
    });
});

$('#logOut').click(function (e) {
    e.preventDefault();
    sessionStorage.removeItem(tokenKey);
    location.reload();
});

function loadTable(token) {
    $("#moderateTable").dataTable({
        "ajax": {
            url: '/api/moderate',
            dataSrc: '',
            headers: { 'Authorization': 'Bearer ' + token }
        },
        "columns": [
            { data: 'Id' },
            { data: 'ObjectName' },
            { data: 'Tags' },
            { data: 'GeoLong' },
            { data: 'GeoLat' },
            { data: 'Status' },
            { data: 'DeleteLink' },
            { data: 'ApprovedLink' }
        ]
    });
}

function delPlace(el) {
    $('#confirmDelModalWin').modal('show');
    $('#deletePlace').click(function () {
        var id = $(el).attr('data-item-id');
        deletePlace(id);
        $('#confirmDelModalWin').modal('hide');
    });
}

function appPlace(el) {
    $('#confirmAppModalWin').modal('show');
    $('#approvePlace').click(function () {
        var id = $(el).attr('data-item-id');
        approvedPlace(id);
        $('#confirmAppModalWin').modal('hide');
    });
}

function deletePlace(id) {
    $.ajax({
        url: '/api/map/' + id,
        type: 'DELETE',
        contentType: 'application/json;charset=utf-8',
        success: function (data) {
            $("#moderateTable").dataTable().api().ajax.reload();
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
        url: '/api/moderate/' + id,
        type: 'PUT',
        headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem(tokenKey)},
        data: JSON.stringify(place),
        contentType: 'application/json;charset=utf-8',
        success: function (data) {
            $("#moderateTable").dataTable().api().ajax.reload();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
