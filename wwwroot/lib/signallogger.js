const rowlimit = 100;

let connection = new signalR.HubConnectionBuilder()
    .withUrl("/logger")
    .build();

connection.start().then(function () {
    // connection.invoke("notify", "client started").then(function (s) {
    // });
});

connection.on("notify", function (xmlmessage) {
    parser = new DOMParser();
    console.log(xmlmessage);
    
    let xmlDoc = parser.parseFromString(xmlmessage.replace(/log4j:/g, ""), "text/xml");

    let message = xmlDoc.getElementsByTagName("message")[0].textContent;
    let msglevel = xmlDoc.documentElement.getAttribute("level");
    //let tRow = document.createElement("tr");

    let mylist = document.getElementById("loglist");
    let tRow = mylist.insertRow(1);
    tRow.className = msglevel + " log row";
    tRow.textContent = message;

    

    // if (mylist.childNodes.length >= rowlimit) {
    //     mylist.lastChild.remove();
    // }
    // mylist.insertRow(0);
    //mylist.firstChild.insertAdjacentElement(1, tRow);
    //let rowheader = document.getElementById("loglistRowHeader");
    //rowheader.insertAdjacentElement(tRow, 2); //, mylist.childNodes(1));
});

$(document).ready(function () {
    $("#btnTrace").click(function () {
        $("tr.trace, tr.TRACE").toggle();
    });
    $("#btnDebug").click(function () {
        $("tr.debug, tr.DEBUG").toggle();
    });
    $("#btnInfo").click(function () {
        $("tr.info, tr.INFO").toggle();
    });
    $("#btnError").click(function () {
        $("tr.error, tr.ERROR").toggle();
    });
    $("#btnFatal").click(function () {
        $("tr.fatal, tr.FATAL").toggle();
    });
});

$(window).on("load", function () {
    console.log("window loaded");
});
