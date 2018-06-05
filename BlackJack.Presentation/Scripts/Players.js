
$(document).ready(function () {

    GetAllPlayers();

    $("#editPlayer").click(function (event) {
        event.preventDefault();
        EditPlayer();
    });

    $("#createPlayer").click(function (event) {
        event.preventDefault();
        CreatePlayer();
    });

});

function GetAllPlayers() {

    $("#createBlock").css('display', 'block');
    $("#editBlock").css('display', 'none');
    $.ajax({
        url: '/api/PlayerValues/',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            WriteResponse(data);
        },
        error: function (x, y, z, ) {
            alert(x.status + '\n' + x.responseJSON + '\n' + z);
        }
    });
}

function CreatePlayer() {
    var player = {
        Name: $('#createName').val(),
        IsBot: $('#createIsBot').val()
    };
    $.ajax({
        url: '/api/PlayerValues/',
        type: 'POST',
        data: JSON.stringify(player),
        contentType: "application/json;charset=utf-8",
        success: function () {
            //alert("yay!");
            GetAllPlayers();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function DeletePlayer(id) {
    $.ajax({
        url: '/api/PlayerValues/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllPlayers();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function EditPlayer() {
    var id = $('#editId').val()
    // получаем новые значения для редактируемого объекта
    var player = {
        Id: $('#editId').val(),
        Name: $('#editName').val(),
        IsBot: $('#editIsBot').val(),
    };
    $.ajax({
        url: '/api/PlayerValues/' + id,
        type: 'PUT',
        data: JSON.stringify(player),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllPlayers();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function WriteResponse(players) {
    var strResult = "<table><th>ID</th><th>Name</th><th>IsBot</th>";
    $.each(players, function (index, player) {
        strResult += "<tr><td>" + player.Id + "</td><td> " + player.Name + "</td><td>" + player.IsBot +
            "</td><td><a id=editItem' data-item='" + player.Id + "' onclick='EditItem(this)'>Edit</a></td>" +
            "<td><a id='deleteItem' data-item='" + player.Id + "' onclick='DeleteItem(this)'>Delete</a></td></tr>";
    });
    strResult += "</table>";
    $("#tableBlock").html(strResult);

}

function DeleteItem(el) {

    var id = $(el).attr('data-item');
    DeletePlayer(id);
}

function EditItem(el) {
    var id = $(el).attr('data-item');
    GetPlayer(id);
}

function ShowPlayer(player) {
    if (player != null) {
        $("#createBlock").css('display', 'none');
        $("#editBlock").css('display', 'block');
        $("#editId").val(player.Id);
        $("#editName").val(player.Name);
        $("#editIsBot").val(player.IsBot);
    }
    else {
        alert("Such player does not exist!");
    }
}

function GetPlayer(id) {
    $.ajax({
        url: '/api/PlayerValues/' + id,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            ShowPlayer(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}