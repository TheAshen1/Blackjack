$(document).ready(function () {

    $('#gameBody').hide();
    $('#roundSummary').hide();


    $("#startGame").click(function(event) {
        event.preventDefault();

        $.ajax({
            url: '/api/GameLogic/', 
            type: 'GET',
            data: {
                playerName: $('#PlayerName').val(),
                numberOfBots: $('#NumberOfBots').val()
            },
            success: function (data) {
               

                InitGameBody(data);

                LoadPlayers(data);
                StartTheGame(data);

            },
            error: function (x, y, z, ) {
                alert(x.status + '\n' + x.responseJSON + '\n' + z);
            }
        });
    });

    $('#quitGame').click(function () {
        event.preventDefault();
    });

    $('#startNextRound').click(function () {
        event.preventDefault();
    });
});

function InitGameBody(data) {
    $('#gameBody').attr('data-item', data.GameId);
    $('#roundBody').attr('data-item', data.CurrentRoundId);

    $('#gameHead').hide();
    $('#gameBody').show();
}

function LoadPlayers(data) {

    var strResult = "<table><th>PlayerId</th><th>Name</th><th>IsBot</th><th>Cards</th>";

    $.each(data.Players, function (index, player) {
        strResult += "<tr id='" + player.CurrentRoundPlayerId + "' class ='player'> <td>"
            + player.Id + "</td><td>"
            + player.Name + "</td><td>"
            + player.IsBot + "</td>" +
            "<td><ul class='cardList'></ul></td>";

        if (player.IsBot === false) {
            strResult += "<td> <a class='user-input' onclick='PlayerDrawCard(this)'>Draw</a> </td>";
            strResult += "<td> <a class='user-input' onclick='FinishRound()'>Stand</a> </td></tr>";
        }
        else {
            strResult += "</tr>";
        }
    });
    strResult += "</table>";

    $("#roundBody").html(strResult);
}


function DrawCard(roundPlayerId){

    $.ajax({
        url: '/api/GameLogic/',
        type: 'GET',
        data: {
            roundPlayerId : roundPlayerId
        },
        success: function (data) {
            $('#' + roundPlayerId + ' .cardList').append('<li>' + data +'</li>');

        },
        error: function (x, y, z, ) {
            alert(x + '\n' + y + '\n' + z);
        },
        async: false
    });
}

function DrawTwoCardsEach(data) {
    $.each(data.Players, function (index, player) {
        DrawCard(player.CurrentRoundPlayerId);
        DrawCard(player.CurrentRoundPlayerId);
    });
}
function PlayerDrawCard(el) {
    var id = $(el).parent().parent().attr('id');
    DrawCard(id);

    if (CountScore(id) == 21) {
        $('#' + id).css('background-color', 'green');
    }
    if (CountScore(id) > 21) {
        $('#' + id).css('background-color', 'red');
        $('.user-input').hide();
    }
}

function StartTheGame(data) {
    
    DrawTwoCardsEach(data);

    $.each(data.Players, function (index, player) {

        if (player.IsBot) {
            while (CountScore(player.CurrentRoundPlayerId) < 17) {
                DrawCard(player.CurrentRoundPlayerId);
            }

            if (CountScore(player.CurrentRoundPlayerId) == 21) {
                $('#' + player.CurrentRoundPlayerId).css('background-color', 'green');
            }
            if (CountScore(player.CurrentRoundPlayerId) > 21) {
                $('#' + player.CurrentRoundPlayerId).css('background-color', 'red');
            }
        }
    });
}

function FinishRound() {

    var rows = $('.player').at;
    var playerIds = [];
    $.each(rows, function (index, row) {
        playerIds.push(row.getAttribute('data-item'));
        console.log(row.getAttribute('data-item'));
    });

    var winners = [];
    var maxScore = 0;

    $.each(players, function (index, player) {
        var playerScore = CountScore(player.CurrentRoundPlayerId);

        if (playerScore > maxScore) {
            winners = [];
            winners.push(player.Name);
            maxScore = playerScore;
        }
        else if (playerScore === maxScore) {
            winners.push(player);
        }
    });
    if (winners.length === 1)
        alert("The winner is " + winners);
    else
        alert("Winners are: " + winners);
}

function CountScore(roundPlayerId) {
    var cardValues = {
        ACE: 1,
        TWO: 2,
        THREE: 3,
        FOUR: 4,
        FIVE: 5,
        SIX: 6,
        SEVEN: 7,
        EIGHT: 8,
        NINE: 9,
        TEN: 10,
        JACK: 10,
        QUEEN: 10,
        KING: 10
    };

    var Score = 0;
    var aces = 0;
    var cardList = $('#' + roundPlayerId + ' .cardList').children();
    $.each(cardList, function (index, cardListElement) {
        var cardString = cardListElement.innerText;
        var cardData = cardString.split(" ");
        var val = cardValues[cardData[0]];


        if (val != 1) {
            Score += val;
        }
        else {
            aces++;
        }
    });

    for (var i = 0; i < aces; i++) {
        if (Score <= 21 - (aces + Score)) {
            Score += 11;
        }
        else {
            Score++;
        }
    }
    console.log(Score);
    return Score;
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

/////

function dialog(message, yesCallback, noCallback) {
    $('.title').html(message);
    var dialog = $('#dialog').dialog();

    $('#btnDraw').click(function () {
        dialog.dialog('close');
        yesCallback();
    });
    $('#btnStand').click(function () {
        dialog.dialog('close');
        noCallback();
    });
}