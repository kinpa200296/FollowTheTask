$(function () {

    $('#showButton').click(function() {
        if ($(this).text() == 'Показать чат') {
            $(this).text('Скрыть чат');
            chat.server.connect($('#username').val());
        } else {
            $(this).text('Показать чат');
        }
    });

    var chat = $.connection.chatHub;

    chat.client.addMessage = function (name, message) {
        $('#chatRoom').append('<p class="row"><b>' + htmlEncode(name)+ '</b>: ' + htmlEncode(message) + '</p>');
    };

    chat.client.onConnected = function (id, userName, allUsers, allMessages) {

        $('#hdId').val(id);
        $('#username').val(userName);
        var i;
        for (i = 0; i < allUsers.length; i++) {
            AddUser(allUsers[i].ConnectionId, allUsers[i].Name);
        }

        for (i = 0; i < allMessages.length; i++) {
            chat.client.addMessage(allMessages[i].UserName, allMessages[i].Message);
        }
    }

    chat.client.onNewUserConnected = function (id, name) {
        AddUser(id, name);
    }

    chat.client.onUserDisconnected = function (id, userName) {
        $('#' + id).remove();
    }

    $.connection.hub.start().done(function () {

        $('#sendmessage').click(function () {
            chat.server.send($('#username').val(), $('#message').val());
            $('#message').val('');
        });
    });
});

function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}

function AddUser(id, name) {

    var userId = $('#hdId').val();

    if (userId != id) {
        $("#chatUsers").append('<p class="row" id="' + id + '"><b>' + name + '</b></p>');
    }
}