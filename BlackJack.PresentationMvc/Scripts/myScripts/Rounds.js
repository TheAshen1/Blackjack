$(document).ready(function () {

    getAllRounds();
    getAllGames();

    $("#editRound").click(function (event) {
        event.preventDefault();
        editRound();
    });

    $("#createRound").click(function (event) {
        event.preventDefault();
        createRound();
    });

});

function getAllRounds() {

    $("#createBlock").css('display', 'block');
    $("#editBlock").css('display', 'none');
    $.ajax({
        url: 'http://localhost:57060/api/RoundValues/',
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

function createRound() {
    var round = {
        GameId: $('#createGameId').val()
    };
    $.ajax({
        url: 'http://localhost:57060/api/RoundValues/',
        type: 'POST',
        data: JSON.stringify(round),
        contentType: "application/json;charset=utf-8",
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        crossDomain: true,
        success: function () {
            getAllRounds();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function deleteRound(id) {
    $.ajax({
        url: 'http://localhost:57060/api/RoundValues/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        crossDomain: true,
        success: function (data) {
            getAllRounds();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function editRound() {
    var id = $('#editId').val();

    var round = {
        Id: $('#editId').val(),
        GameId: $('#editGameId').val()
    };
    $.ajax({
        url: 'http://localhost:57060/api/RoundValues/' + id,
        type: 'PUT',
        data: JSON.stringify(round),
        contentType: "application/json;charset=utf-8",
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        crossDomain: true,
        success: function (data) {
            getAllRounds();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function writeResponse(rounds) {
    var strResult = "<table><th>ID</th><th>GameId</th>";
    $.each(rounds, function (index, round) {
        strResult += "<tr><td>" + round.Id + "</td><td> " + round.GameId + "</td><td> " +
            "</td><td><a id='editItem' data-item='" + round.Id + "' onclick='editItem(this)'>Edit</a></td>" +
            "<td><a id='deleteItem' data-item='" + round.Id + "' onclick='deleteItem(this)'>Delete</a></td></tr>";
    });
    strResult += "</table>";
    $("#tableBlock").html(strResult);

}

function deleteItem(el) {

    var id = $(el).attr('data-item');
    deleteRound(id);
}

function editItem(el) {
    var id = $(el).attr('data-item');
    getRound(id);
}

function showRound(round) {
    if (round !== null) {
        $("#createBlock").css('display', 'none');
        $("#editBlock").css('display', 'block');
        $("#editId").val(round.Id);
        $("#editGameId").val(round.GameId);
    }
    else {
        alert("Such player does not exist!");
    }
}

function getRound(id) {
    $.ajax({
        url: 'http://localhost:57060/api/RoundValues/' + id,
        type: 'GET',
        dataType: 'json',
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        crossDomain: true,
        success: function (data) {
            
            if (data.Id != getEmptyGuid()) {
                showRound(data);
                console.log('No entity with such Id was found!');
            }
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
///////////
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