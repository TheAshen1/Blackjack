
$(document).ready(function () {

    GetAllRoundPlayers();
    GetAllRounds();
    GetAllPlayers();

    $("#editRoundPlayer").click(function (event) {
        event.preventDefault();
        EditRoundPlayer();
    });

    $("#createRoundPlayer").click(function (event) {
        event.preventDefault();
        CreateRoundPlayer();
    });

});

function GetAllRoundPlayers() {

    $("#createBlock").css('display', 'block');
    $("#editBlock").css('display', 'none');
    $.ajax({
        url: '/api/RoundPlayerValues/',
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

function CreateRoundPlayer() {
    var roundPlayer = {
        RoundId: $('#createRoundId').val(),
        PlayerId: $('#createPlayerId').val(),
        Cards: $('#createCards').val()
    };
    $.ajax({
        url: '/api/RoundPlayerValues/',
        type: 'POST',
        data: JSON.stringify(roundPlayer),
        contentType: "application/json;charset=utf-8",
        success: function () {
            GetAllRoundPlayers();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function DeleteRoundPlayer(id) {
    $.ajax({
        url: '/api/RoundPlayerValues/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllRoundPlayers();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function EditRoundPlayer() {
    var id = $('#editId').val()

    var roundPlayer = {
        Id: $('#editId').val(),
        RoundId: $('#editRoundId').val(),
        PlayerId: $('#editPlayerId').val(),
        Cards: $('#editCards').val()
    };
    $.ajax({
        url: '/api/RoundPlayerValues/' + id,
        type: 'PUT',
        data: JSON.stringify(roundPlayer),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllRoundPlayers();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function WriteResponse(roundplayers) {
    var strResult = "<table><th>ID</th><th>RoundId</th><th>PlayerId</th><th>Cards</th>";
    $.each(roundplayers, function (index, roundplayer) {
        strResult += "<tr><td>" + roundplayer.Id + "</td><td> " + roundplayer.RoundId + "</td><td>" + roundplayer.PlayerId +
            "</td><td>" + roundplayer.Cards +
            "</td><td><a id=editItem' data-item='" + roundplayer.Id + "' onclick='EditItem(this)'>Edit</a></td>" +
            "<td><a id='deleteItem' data-item='" + roundplayer.Id + "' onclick='DeleteItem(this)'>Delete</a></td></tr>";
    });
    strResult += "</table>";
    $("#tableBlock").html(strResult);

}

function DeleteItem(el) {

    var id = $(el).attr('data-item');
    DeleteRoundPlayer(id);
}

function EditItem(el) {
    var id = $(el).attr('data-item');
    GetRoundPlayer(id);
}

function ShowRoundPlayer(roundPlayer) {
    if (roundPlayer != null) {
        $("#createBlock").css('display', 'none');
        $("#editBlock").css('display', 'block');
        $("#editId").val(roundPlayer.Id);
        $("#editRoundId").val(roundPlayer.RoundId);
        $("#editPlayerId").val(roundPlayer.PlayerId);
        $("#editCards").val(roundPlayer.Cards);
    }
    else {
        alert("Such player does not exist!");
    }
}

function GetRoundPlayer(id) {
    $.ajax({
        url: '/api/RoundPlayerValues/' + id,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            ShowRoundPlayer(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

///////////

function GetAllRounds() {

    $("#createBlock").css('display', 'block');
    $("#editBlock").css('display', 'none');
    $.ajax({
        url: '/api/RoundValues/',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            FillRoundSelect(data);
        },
        error: function (x, y, z, ) {
            alert(x.status + '\n' + x.responseJSON + '\n' + z);
        }
    });
}

function FillRoundSelect(rounds) {
    var strResult = "";
    $.each(rounds, function (index, round) {
        strResult += "<option value='" + round.Id + "'>" + round.Id + "</option>";
    });
    $("#editRoundId").html(strResult);
    $("#createRoundId").html(strResult);
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
        strResult += "<option value='" + player.Id + "'>" + player.Name + "</option>";
    });
    $("#editPlayerId").html(strResult);
    $("#createPlayerId").html(strResult);
}