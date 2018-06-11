﻿$(document).ready(function () {

    GetAllRounds();
    GetAllGames();

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
        GameId: $('#createGameId').val()
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
    var id = $('#editId').val();

    var round = {
        Id: $('#editId').val(),
        GameId: $('#editGameId').val()
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
    var strResult = "<table><th>ID</th><th>GameId</th>";
    $.each(rounds, function (index, round) {
        strResult += "<tr><td>" + round.Id + "</td><td> " + round.GameId + "</td><td> " +
            "</td><td><a id='editItem' data-item='" + round.Id + "' onclick='EditItem(this)'>Edit</a></td>" +
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
///////////
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