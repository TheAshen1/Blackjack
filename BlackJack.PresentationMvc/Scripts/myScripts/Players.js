
$(document).ready(function () {

    getAllPlayers();
    getAllGames();

    $("#editPlayer").click(function (event) {
        event.preventDefault();
        editPlayer();
    });

    $("#createPlayer").click(function (event) {
        event.preventDefault();
        createPlayer();
    });

});

function getAllPlayers() {

    $("#createBlock").css('display', 'block');
    $("#editBlock").css('display', 'none');
    $.ajax({
        url: 'http://localhost:57060/api/PlayerValues/',
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

function createPlayer() {
    var player = {
        Name: $('#createName').val(),
        IsBot: $('#createIsBot').val()
    };
    $.ajax({
        url: 'http://localhost:57060/api/PlayerValues/',
        type: 'POST',
        data: JSON.stringify(player),
        contentType: "application/json;charset=utf-8",
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        crossDomain: true,
        success: function (data) {
            getAllPlayers();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function deletePlayer(id) {
    $.ajax({
        url: '/api/PlayerValues/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        crossDomain: true,
        success: function (data) {
            getAllPlayers();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function editPlayer() {
    var id = $('#editId').val();
    // получаем новые значения для редактируемого объекта
    var player = {
        Id: $('#editId').val(),
        Name: $('#editName').val(),
        IsBot: $('#editIsBot').val()
    };
    $.ajax({
        url: 'http://localhost:57060/api/PlayerValues/' + id,
        type: 'PUT',
        data: JSON.stringify(player),
        contentType: "application/json;charset=utf-8",
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        crossDomain: true,
        success: function (data) {
            getAllPlayers();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function writeResponse(players) {
    var strResult = "<table><th>ID</th><th>Name</th><th>IsBot</th>";
    $.each(players, function (index, player) {
        strResult += "<tr><td>" + player.Id + "</td><td> " + player.Name + "</td><td>" + player.IsBot +
            "</td><td><a id='editItem' data-item='" + player.Id + "' onclick='editItem(this)'>Edit</a></td>" +
            "<td><a id='deleteItem' data-item='" + player.Id + "' onclick='deleteItem(this)'>Delete</a></td></tr>";
    });
    strResult += "</table>";
    $("#tableBlock").html(strResult);

}

function deleteItem(el) {

    var id = $(el).attr('data-item');
    deletePlayer(id);
}

function editItem(el) {
    var id = $(el).attr('data-item');
    getPlayer(id);
}

function showPlayer(player) {
    if (player !== null) {
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

function getPlayer(id) {
    $.ajax({
        url: 'http://localhost:57060/api/PlayerValues/' + id,
        type: 'GET',
        dataType: 'json',
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        crossDomain: true,
        success: function (data) {

            if (data.Id != getEmptyGuid()) {
                showPlayer(data);
                console.log('No entity with such Id was found!');
            }
          
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
////////

function getAllGames() {

    $("#createBlock").css('display', 'block');
    $("#editBlock").css('display', 'none');
    $.ajax({
        url: 'http://localhost:57060/api/GameValues/',
        type: 'GET',
        dataType: 'json',
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        crossDomain: true,
        success: function (data) {
            fillGameSelect(data);
        },
        error: function (x, y, z, ) {
            alert(x.status + '\n' + x.responseJSON + '\n' + z);
        }
    });
}

function fillGameSelect(games) {
    var strResult = "";
    $.each(games, function (index, game) {
        strResult += "<option value='" + game.Id + "'>" + game.Id + "</option>";
    });
    $("#editGameId").html(strResult);
    $("#createGameId").html(strResult);
}

function getEmptyGuid() {
    return "00000000-0000-0000-0000-000000000000";
} 