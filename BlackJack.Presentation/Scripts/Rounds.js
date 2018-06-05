
$(document).ready(function () {

    GetAllRounds();
    GetAllGames();
    GetAllPlayers();

    $("#editRound").click(function (event) {
        event.preventDefault();
        EditRound();
    });

    $("#createRound").click(function (event) {
        event.preventDefault();
        CreateRound();
    });

});

function GetAllRounds() {

    $("#createBlock").css('display', 'block');
    $("#editBlock").css('display', 'none');
    $.ajax({
        url: '/api/RoundValues/',
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

function CreateRound() {
    var round = {
        GameId: $('#createGameId').val(),
        WinnerId: $('#createWinnerId').val()
    };
    $.ajax({
        url: '/api/RoundValues/',
        type: 'POST',
        data: JSON.stringify(round),
        contentType: "application/json;charset=utf-8",
        success: function () {
            GetAllRounds();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function DeleteRound(id) {
    $.ajax({
        url: '/api/RoundValues/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllRounds();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function EditRound() {
    var id = $('#editId').val()
    // получаем новые значения для редактируемого объекта
    var round = {
        Id: $('#editId').val(),
        GameId: $('#editGameId').val(),
        WinnerId: $('#editWinnerId').val(),
    };
    $.ajax({
        url: '/api/RoundValues/' + id,
        type: 'PUT',
        data: JSON.stringify(round),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllRounds();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function WriteResponse(rounds) {
    var strResult = "<table><th>ID</th><th>GameId</th><th>WinnerId</th>";
    $.each(rounds, function (index, round) {
        strResult += "<tr><td>" + round.Id + "</td><td> " + round.GameId + "</td><td>" + round.WinnerId +
            "</td><td><a id=editItem' data-item='" + round.Id + "' onclick='EditItem(this)'>Edit</a></td>" +
            "<td><a id='deleteItem' data-item='" + round.Id + "' onclick='DeleteItem(this)'>Delete</a></td></tr>";
    });
    strResult += "</table>";
    $("#tableBlock").html(strResult);

}

function DeleteItem(el) {

    var id = $(el).attr('data-item');
    DeleteRound(id);
}

function EditItem(el) {
    var id = $(el).attr('data-item');
    GetRound(id);
}

function ShowRound(round) {
    if (round != null) {
        $("#createBlock").css('display', 'none');
        $("#editBlock").css('display', 'block');
        $("#editId").val(round.Id);
        $("#editGameId").val(round.GameId);
        $("#editWinnerId").val(round.WinnerId);
    }
    else {
        alert("Such player does not exist!");
    }
}

function GetRound(id) {
    $.ajax({
        url: '/api/RoundValues/' + id,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            ShowRound(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function GetAllGames() {

    $("#createBlock").css('display', 'block');
    $("#editBlock").css('display', 'none');
    $.ajax({
        url: '/api/GameValues/',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            FillGameSelect(data);
        },
        error: function (x, y, z, ) {
            alert(x.status + '\n' + x.responseJSON + '\n' + z);
        }
    });
}

function FillGameSelect(games) {
    var strResult = "";
    $.each(games, function (index, game) {
        strResult += "<option value='" + game.Id + "'>" + game.Id + "</option>";
    });
    $("#editGameId").html(strResult);
    $("#createGameId").html(strResult);
}

function GetAllPlayers() {

    $("#createBlock").css('display', 'block');
    $("#editBlock").css('display', 'none');
    $.ajax({
        url: '/api/PlayerValues/',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            FillPlayerSelect(data);
        },
        error: function (x, y, z, ) {
            alert(x.status + '\n' + x.responseJSON + '\n' + z);
        }
    });
}

function FillPlayerSelect(players) {
    var strResult = "";
    $.each(players, function (index, player) {
        strResult += "<option value='" + player.Id + "'>" + player.Id + "</option>";
    });
    $("#editWinnerId").html(strResult);
    $("#createWinnerId").html(strResult);
}