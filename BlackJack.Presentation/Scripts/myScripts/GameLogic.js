$(document).ready(function () {

    $('#gameBody').hide();

    $("#startGame").click(function(event) {
        event.preventDefault();

        //var playerName = $('#PlayerName').val();
        //var numberOfBots = $('#NumberOfBots').val();

        $.ajax({
            url: '/api/GameLogic/', 
            type: 'GET',
            data: {
                playerName: $('#PlayerName').val(),
                numberOfBots: $('#NumberOfBots').val()
            },
            success: function (data) {
                $('#gameHead').hide();
                $('#gameBody').show();

                InitGameBody(data);

                LoadPlayers(data);
                //GiveCards(data);
            },
            error: function (x, y, z, ) {
                alert(x.status + '\n' + x.responseJSON + '\n' + z);
            }
        });
    });
});

function InitGameBody(data) {
    $('#gameBody').attr('data-item', data.GameId);
    $('#roundBody').attr('data-item', data.CurrentRoundId);
}

function LoadPlayers(data) {

    var strResult = "<table class='players'><th>PlayerId</th><th>Name</th><th>IsBot</th><th>Cards</th>";
    $.each(data.Players, function (index, player) {
        strResult += "<tr><td>" + player.Id + "</td><td> " + player.Name + "</td><td>" + player.IsBot + "</td><td>";
        var cardList = "<ul>";

        $.each(player.Cards, function (index, card) {
            cardList += "<li>" + card + "</li>";
        });
        cardList += "</ul>";

        strResult += cardList + "</td></tr>";
    });
    strResult += "</table>";
    $("#roundBody").html(strResult);
}


function DrawCard(playerId){

    $.ajax({
        url: '/api/GameLogic/' + playerId,
        type: 'GET',
        success: function (data) {
            LoadPlayers();
        },
        error: function (x, y, z, ) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}



//////
function Exit() { /// idk mb later
    EndTheGame();
    document.body.onunload = "";
    document.body.onbeforeunload = "";
}

function EndTheGame() { /// idk mb later
    var gameData = {
            GameId : $('#gameBody').attr('data-item')
            //RoundId =  $('#roundBody').attr('data-item')
    };

    console.log(GameId);
    $.ajax({
        url: '/api/GameLogic/',
        type: 'POST',
        //data: gameData,
        data: JSON.stringify(gameData),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log("game was successfully saved");
        },
        error: function (x, y, z, ) {
            alert(x.status + '\n' + x.responseJSON + '\n' + z);
        }
    });
}