"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/Hub").build();

connection.on("countUpdate", function (count) {
    console.log(count);
    document.getElementById("serverCounter").value = count;
});

connection.start()
    .catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("countButton").addEventListener("click", function (event) {
    fetch('api/count/inc')
        .then()
        .catch((err) => {
            console.log(err.toString());
        });
    event.preventDefault();
});