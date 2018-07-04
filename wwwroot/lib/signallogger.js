const rowlimit = 100;

let connection = new signalR.HubConnectionBuilder()
    .withUrl("/logger")
    .build();

connection.start().then(function () {
    // connection.invoke("notify", "client started").then(function (s) {
    // });
});

connection.on("notify",  function(xmlmessage)  {
    parser = new DOMParser();
    let xmlDoc = parser.parseFromString(xmlmessage.replace(/log4j:/g, ""), "text/xml");

    let message = xmlDoc.getElementsByTagName("message")[0].textContent;
    let msglevel = xmlDoc.documentElement.getAttribute("level");
    let tdiv = document.createElement("div");
    tdiv.className = msglevel;
    const hr = document.createElement("hr");
    tdiv.textContent = message;

    let mylist = document.getElementById("loglist");
    
    if (mylist.childNodes.length >= rowlimit)
    {
        mylist.lastChild.remove();
    }
    tdiv.appendChild(hr);
    mylist.insertBefore(tdiv, mylist.firstChild);
});

