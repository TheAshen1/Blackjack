$(document).ready(function ()
{
    $('#gameBody').hide();
    $('#roundSummary').hide();

    $("#startGame").click(function (event)
    {
        event.preventDefault();

        $.ajax({
            url: 'http://localhost:50001/api/GameLogic/',
            type: 'GET',
            dataType: 'json',
            data: {
                playerName: $('#PlayerName').val(),
                numberOfBots: $('#NumberOfBots').val()
            },
            headers: {
                'Access-Control-Allow-Origin': '*'
            },
            crossDomain: true,
            success: function (data)
            {
                if (data.GameId == getEmptyGuid())
                {
                    alert('Unable to start a game!');
                    return;
                }

                initGameBody(data);

                loadPlayers(data);
                startTheGame(data);

            },
            error: function (x, y, z, )
            {
                alert(x.status + '\n' + x.responseJSON + '\n' + z);
            }
        });
    });

    $('#quitGame').click(function (event)
    {
        event.preventDefault();
        finishGame();
    });

    $('#startNextRound').click(function (event)
    {
        event.preventDefault();
        moveToTheNextRound();
    });
});

function initGameBody(data)
{
    $('#gameBody').attr('data-item', data.GameId);
    $('#roundBody').attr('data-item', data.CurrentRoundId);

    $('#gameHead').hide();
    $('#gameBody').show();
}

function loadPlayers(data)
{
    var strResult = "";
    $.each(data.Players, function (index, player)
    {
        strResult += "<div id='" + player.CurrentRoundPlayerId + "' class ='player'>"
            + "<div class='playerName'>" + player.Name + "</div>"
            + "<div class='cardList'></div>";

        if (player.IsBot === false)
        {
            strResult += "<div id='user-input'> <div class='btn btn-primary' onclick='playerDrawCard(this)'>Draw</div>";
            strResult += "<div class='btn btn-primary' onclick='finishRound()'>Stand</div> </div>";
        }
        strResult += "</div>";
    });

    $("#roundBody").html(strResult);

    var radius = 250;
    var fields = $('.player'),
        container = $('#roundBody'),
        width = container.width(),
        height = container.height(),
        angle = 0,
        step = 2 * Math.PI / fields.length;
    fields.each(function ()
    {
        var x = Math.round(width / 2 + radius * Math.cos(angle) - $(this).width() / 2),
            y = Math.round(height / 2 + radius * Math.sin(angle) - $(this).height() / 2);
        $(this).css({
            left: x + 'px',
            top: y - this.clientHeight + 'px'
        });
        angle += step;
    });
}

function drawCard(roundPlayerId)
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

    $.ajax({
        url: 'http://localhost:50001/api/GameLogic/',
        type: 'GET',
        data: {
            roundPlayerId: roundPlayerId
        },
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        crossDomain: true,
        success: function (data)
        {
            let card = JSON.parse(data);
            if (card == null)
            {
                alert('Unable to retrieve card!');
                return;
            }
            let picName = cardValues[card.value] + card.suit.charAt(0);

            $('#' + roundPlayerId + ' .cardList').append('<div class="card" data-value="' + card.value + '" data-suit="' + card.suit + '">'
                + '<img src="Content/Pictures/Cards/' + picName + '.png" />'
                + '</div>');

            var displayedCards = $('#' + roundPlayerId + ' .cardList').children();
            if (displayedCards.length > 1)
                displayedCards[displayedCards.length - 2].style = "margin-right:-40px;"; 

        },
        error: function (x, y, z, )
        {
            alert(x + '\n' + y + '\n' + z);
        },
        async: false
    });
}

function drawTwoCardsEach(data)
{
    $.each(data.Players, function (index, player)
    {
        drawCard(player.CurrentRoundPlayerId);
        drawCard(player.CurrentRoundPlayerId);
    });
}

function playerDrawCard(el)
{
    var id = $(el).parent().parent().attr('id');
    drawCard(id);

    if (countScore(id) >= 21)
    {
        finishRound();
    }
}

function startTheGame(data)
{

    drawTwoCardsEach(data);

    $.each(data.Players, function (index, player)
    {

        if (!player.IsBot)
        {
            countScore(player.CurrentRoundPlayerId);
            return;
        }

        while (countScore(player.CurrentRoundPlayerId) < 17)
        {
            drawCard(player.CurrentRoundPlayerId);
        }
    });
}

function countScore(roundPlayerId)
{
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

    var score = 0;
    var aces = 0;
    var cardList = $('#' + roundPlayerId + ' .cardList').children();
    $.each(cardList, function (index, cardListElement)
    {
        var cardValueString = cardListElement.getAttribute('data-value');
        var val = cardValues[cardValueString];

        if (val != 1)
        {
            score += val;
        }
        else
        {
            aces++;
        }
    });

    for (var i = 0; i < aces; i++)
    {
        if (score <= 21 - (aces + score))
        {
            score += 11;
        }
        else
        {
            score++;
        }
    }
    console.log(score);

    if (score == 21)
    {
        $('#' + roundPlayerId).css('background-color', 'green');
    }
    if (score > 21)
    {
        $('#' + roundPlayerId).css('background-color', 'red');
    }

    return score;
}

function finishRound()
{

    $('#user-input').hide();
    var rows = $('.player');
    var players = [];

    $.each(rows, function (index, row)
    {
        players.push({
            roundPlayerId: row.getAttribute('id'),
            Name: $('#' + row.getAttribute('id')).find(".playerName").text()
        });
    });

    var winners = [];
    var maxScore = 0;

    for (var i = 0; i < players.length; i++)
    {
        var playerScore = countScore(players[i].roundPlayerId);

        if (playerScore > maxScore && playerScore <= 21)
        {
            winners = [];
            winners.push(players[i].Name);
            maxScore = playerScore;
        }
        else if (playerScore === maxScore && playerScore <= 21)
        {
            winners.push(players[i].Name);
        }
    }

    var message = "";
    if (winners.length === 1)
        message = "The winner is " + winners;
    else
        message = "Winners are: " + winners;
    alert(message);

    $('#roundSummary .message').text(message);
    $('#roundSummary').show();
}

function moveToTheNextRound()
{

    $('#roundSummary .message').text('');
    $.ajax({
        url: 'http://localhost:50001/api/GameLogic/',
        type: 'GET',
        dataType: 'json',
        data: {
            currentGameId: $('#gameBody').attr('data-item')
        },
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        crossDomain: true,
        success: function (data)
        {
            if (data.GameId == getEmptyGuid())
            {
                alert('Unable to continue a game!');
                return;
            }

            initGameBody(data);

            loadPlayers(data);
            startTheGame(data);
        },
        error: function (x, y, z, )
        {
            alert(x.status + '\n' + x.responseJSON + '\n' + z);
        }
    });
}

function finishGame()
{
    $.ajax({
        url: 'http://localhost:50001/api/GameLogic/',
        type: 'GET',
        data: {
            GameId: $('#gameBody').attr('data-item')
        },
        contentType: "application/json;charset=utf-8",
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        crossDomain: true,
        success: function (data)
        {
            console.log("game was successfully saved");

            $("#roundBody").empty();
            $('#gameBody').hide();
            $('#roundSummary').hide();

            $('#gameBody').attr('data-item', null);
            $('#roundBody').attr('data-item', null);

            $('#gameHead').show();
        },
        error: function (x, y, z, )
        {
            alert(x.status + '\n' + x.responseJSON + '\n' + z);
        }
    });
}

function getEmptyGuid()
{
    return "00000000-0000-0000-0000-000000000000";
} 
