"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/Hub").build();

connection.on("countUpdated", (count) => {

     console.log("Expense count updated to " + count);
     document.getElementById("serverCounter").value = count;

});

connection.start()
    .catch(function (err) {
    return console.error(err.toString());
});
