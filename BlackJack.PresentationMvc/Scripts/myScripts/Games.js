
$(document).ready(function () {

    getAllGames();

    $("#editGame").click(function (event) {
        event.preventDefault();
        editGame();
    });

    $("#createGame").click(function (event) {
        event.preventDefault();
        createGame();
    });

});

function getAllGames() {

    $("#createBlock").css('display', 'block');
    $("#editBlock").css('display', 'none');
    $.ajax({
        url: 'http://localhost:57060/api/GameValues',
        type: 'GET',
        dataType: 'json',
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        crossDomain: true,
        success: function (data) {
            writeResponse(data);
        },
        error: function (x, y, z, ) {
            alert(x.status + '\n' + x.responseJSON + '\n' + z);
        }
    });
}

function createGame() {
    var game = {
        Start: $('#createStart').val()
    };
    $.ajax({
        url: 'http://localhost:57060/api/GameValues/',
        type: 'POST',
        data: JSON.stringify(game),        
        contentType: "application/json;charset=utf-8",
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        crossDomain: true,
        success: function () {
            getAllGames();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function deleteGame(id) {
    $.ajax({
        url: 'http://localhost:57060/api/GameValues/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        crossDomain: true,
        success: function (data) {
            getAllGames();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function editGame() {
    var id = $('#editId').val();
    // получаем новые значения для редактируемого объекта
    var game = {
        Id: $('#editId').val(),
        Start: $('#editStart').val(),
        End: $('#editEnd').val()
    };
    $.ajax({
        url: 'http://localhost:57060/api/GameValues/' + id,
        type: 'PUT',
        data: JSON.stringify(game),
        contentType: "application/json;charset=utf-8",
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        crossDomain: true,
        success: function (data) {
            getAllGames();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function writeResponse(games) {
    var strResult = "<table><th>ID</th><th>Start</th><th>End</th>";
    $.each(games, function (index, game) {
        strResult += "<tr><td>" + game.Id + "</td><td> " + game.Start + "</td><td>" + game.End +
            "</td><td><a id=editItem' data-item='" + game.Id + "' onclick='editItem(this)'>Edit</a></td>" +
            "<td><a id='deleteItem' data-item='" + game.Id + "' onclick='deleteItem(this)'>Delete</a></td></tr>";
    });
    strResult += "</table>";
    $("#tableBlock").html(strResult);

}

function deleteItem(el) {

    var id = $(el).attr('data-item');
    deleteGame(id);
}

function editItem(el) {
    var id = $(el).attr('data-item');
    getGame(id);
}

function showGame(game) {
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

function getGame(id) {
    $.ajax({
        url: 'http://localhost:57060/api/GameValues/' + id,
        type: 'GET',
        dataType: 'json',
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        crossDomain: true,
        success: function (data) {

            if (data.Id != getEmptyGuid()) {
                showGame(data);
                console.log('No entity with such Id was found!');
            }
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function getEmptyGuid() {
    return "00000000-0000-0000-0000-000000000000";
} 