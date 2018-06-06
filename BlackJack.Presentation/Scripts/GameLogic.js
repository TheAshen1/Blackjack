$(document).ready(function () {


    $("#startGame").click(function(event) {
        event.preventDefault();

        $.ajax({
            url: '/api/GameLogic/',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                $('#gameHead').hide();

                LoadPlayers();
            },
            error: function (x, y, z, ) {
                alert(x.status + '\n' + x.responseJSON + '\n' + z);
            }
        });
    });

});