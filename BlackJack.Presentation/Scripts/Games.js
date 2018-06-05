
$(document).ready(function () {

    GetAllGames();

    $("#editGame").click(function (event) {
        event.preventDefault();
        EditGame();
    });

    $("#createGame").click(function (event) {
        event.preventDefault();
        CreateGame();
    });

});

function GetAllGames() {

    $("#createBlock").css('display', 'block');
    $("#editBlock").css('display', 'none');
    $.ajax({
        url: '/api/GameValues/',
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

function CreateGame() {
    var game = {
        Start: $('#createStart').val()
    };
    $.ajax({
        url: '/api/GameValues/',
        type: 'POST',
        data: JSON.stringify(game),
        contentType: "application/json;charset=utf-8",
        success: function () {
            GetAllGames();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function DeleteGame(id) {
    $.ajax({
        url: '/api/GameValues/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllGames();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function EditGame() {
    var id = $('#editId').val()
    // получаем новые значения для редактируемого объекта
    var game = {
        Id: $('#editId').val(),
        Start: $('#editStart').val(),
        End: $('#editEnd').val(),
    };
    $.ajax({
        url: '/api/GameValues/' + id,
        type: 'PUT',
        data: JSON.stringify(game),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllGames();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function WriteResponse(games) {
    var strResult = "<table><th>ID</th><th>Start</th><th>End</th>";
    $.each(games, function (index, game) {
        strResult += "<tr><td>" + game.Id + "</td><td> " + game.Start + "</td><td>" + game.End +
            "</td><td><a id=editItem' data-item='" + game.Id + "' onclick='EditItem(this)'>Edit</a></td>" +
            "<td><a id='deleteItem' data-item='" + game.Id + "' onclick='DeleteItem(this)'>Delete</a></td></tr>";
    });
    strResult += "</table>";
    $("#tableBlock").html(strResult);

}

function DeleteItem(el) {

    var id = $(el).attr('data-item');
    DeleteGame(id);
}

function EditItem(el) {
    var id = $(el).attr('data-item');
    GetGame(id);
}

function ShowGame(game) {
    if (game != null) {
        $("#createBlock").css('display', 'none');
        $("#editBlock").css('display', 'block');
        $("#editId").val(game.Id);
        $("#editStart").val(game.Start);
        $("#editEnd").val(game.End);
    }
    else {
        alert("Such player does not exist!");
    }
}

function GetGame(id) {
    $.ajax({
        url: '/api/GameValues/' + id,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            ShowGame(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}