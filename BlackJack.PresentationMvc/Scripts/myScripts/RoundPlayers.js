
$(document).ready(function ()
{

    getAllRoundPlayers();
    getAllRounds();
    getAllPlayers();

    $("#editRoundPlayer").click(function (event)
    {
        event.preventDefault();
        editRoundPlayer();
    });

    $("#createRoundPlayer").click(function (event)
    {
        event.preventDefault();
        createRoundPlayer();
    });


    $("#createRoundId").change(function ()
    {
        getAllPlayers();
    });
    $("#editRoundId").change(function ()
    {
        getAllPlayers();
    });


});

function getAllRoundPlayers()
{

    $("#createBlock").css('display', 'block');
    $("#editBlock").css('display', 'none');
    $.ajax({
        url: 'http://localhost:57060/api/RoundPlayerValues/',
        type: 'GET',
        dataType: 'json',
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        crossDomain: true,
        success: function (data)
        {
            writeResponse(data);
        },
        error: function (x, y, z, )
        {
            alert(x.status + '\n' + x.responseJSON + '\n' + z);
        }
    });
}

function createRoundPlayer()
{
    var roundPlayer = {
        RoundId: $('#createRoundId').val(),
        PlayerId: $('#createPlayerId').val(),
        Cards: $('#createCards').val()
    };
    $.ajax({
        url: 'http://localhost:57060/api/RoundPlayerValues/',
        type: 'POST',
        data: JSON.stringify(roundPlayer),
        contentType: "application/json;charset=utf-8",
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        crossDomain: true,
        success: function ()
        {
            getAllRoundPlayers();
        },
        error: function (x, y, z)
        {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function deleteRoundPlayer(id)
{
    $.ajax({
        url: 'http://localhost:57060/api/RoundPlayerValues/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        crossDomain: true,
        success: function (data)
        {
            getAllRoundPlayers();
        },
        error: function (x, y, z)
        {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function editRoundPlayer()
{
    var id = $('#editId').val();

    var roundPlayer = {
        Id: $('#editId').val(),
        RoundId: $('#editRoundId').val(),
        PlayerId: $('#editPlayerId').val(),
        Cards: $('#editCards').val()
    };
    $.ajax({
        url: 'http://localhost:57060/api/RoundPlayerValues/' + id,
        type: 'PUT',
        data: JSON.stringify(roundPlayer),
        contentType: "application/json;charset=utf-8",
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        crossDomain: true,
        success: function (data)
        {
            getAllRoundPlayers();
        },
        error: function (x, y, z)
        {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function writeResponse(roundplayers)
{
    var cardValues = {
        TWO: 2,
        THREE: 3,
        FOUR: 4,
        FIVE: 5,
        SIX: 6,
        SEVEN: 7,
        EIGHT: 8,
        NINE: 9,
        TEN: 10,
        JACK: 'J',
        QUEEN: 'Q',
        KING: 'K',
        ACE: 'A'
    };

    var strResult = `<table><th>ID</th><th>RoundId</th><th>PlayerId</th><th>Cards</th>`;

    $.each(roundplayers, function (index, roundplayer)
    {

        let cards = JSON.parse(roundplayer.Cards);
        strResult += `
            <tr>
                <td>${roundplayer.Id}</td>
                <td>${roundplayer.RoundId}</td>
                <td>${roundplayer.PlayerId}</td>
                <td>`;


        $.each(cards, (index, card) =>
        {
            let picName = cardValues[card.value] + card.suit.charAt(0);
            strResult += `  <div class='card' data-value='${card.value}' data-suit='${card.suit}'>
                                <img src='Content/Pictures/Cards/${picName}.png' />
                            </div>`;
        });


        strResult += `</td>
                <td><a id='editItem' data-item='${roundplayer.Id}' onclick='editItem(this)'>Edit</a></td>
                <td><a id='deleteItem' data-item='${roundplayer.Id}' onclick='deleteItem(this)'>Delete</a></td>
            </tr>`;
    });
    strResult += "</table>";
    $("#tableBlock").html(strResult);

}

function deleteItem(el)
{

    var id = $(el).attr('data-item');
    deleteRoundPlayer(id);
}

function editItem(el)
{
    var id = $(el).attr('data-item');
    getRoundPlayer(id);
}

function showRoundPlayer(roundPlayer)
{
    if (roundPlayer !== null)
    {
        $("#createBlock").css('display', 'none');
        $("#editBlock").css('display', 'block');
        $("#editId").val(roundPlayer.Id);
        $("#editRoundId").val(roundPlayer.RoundId);
        $("#editPlayerId").val(roundPlayer.PlayerId);
        $("#editCards").val(roundPlayer.Cards);
    }
    else
    {
        alert("Such player does not exist!");
    }
}

function getRoundPlayer(id)
{
    $.ajax({
        url: 'http://localhost:57060/api/RoundPlayerValues/' + id,
        type: 'GET',
        dataType: 'json',
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        crossDomain: true,
        success: function (data)
        {

            if (data.Id != getEmptyGuid())
            {
                showRoundPlayer(data);
                console.log('No entity with such Id was found!');
            }

        },
        error: function (x, y, z)
        {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

///////////

function getAllRounds()
{

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
        success: function (data)
        {
            fillRoundSelect(data);
        },
        error: function (x, y, z, )
        {
            alert(x.status + '\n' + x.responseJSON + '\n' + z);
        }
    });
}

function fillRoundSelect(rounds)
{
    var strResult = "";
    $.each(rounds, function (index, round)
    {
        strResult += "<option value='" + round.Id + "' data-item='" + round.GameId + "' >" + round.Id + "</option>";
    });
    $("#editRoundId").html(strResult);
    $("#createRoundId").html(strResult);
}

function getAllPlayers()
{

    $("#createBlock").show();
    $("#editBlock").hide();
    $.ajax({
        url: 'http://localhost:57060/api/PlayerValues/',
        type: 'GET',
        dataType: 'json',
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        crossDomain: true,
        success: function (data)
        {
            fillPlayerSelect(data);
        },
        error: function (x, y, z, )
        {
            alert(x.status + '\n' + x.responseJSON + '\n' + z);
        }
    });
}

function fillPlayerSelect(players)
{
    var select = $('#createRoundId');
    var selectedRoundGameId = $('option:selected', select).attr('data-item');

    var strResult = "";
    $.each(players, function (index, player)
    {
        if (player.GameId === selectedRoundGameId)
            strResult += "<option value='" + player.Id + "'>" + player.Name + "</option>";
    });

    $("#editPlayerId").html(strResult);
    $("#createPlayerId").html(strResult);
}

function getEmptyGuid()
{
    return "00000000-0000-0000-0000-000000000000";
} 