const url = "/api/BourseAPI";
const connection = new signalR.HubConnectionBuilder().withUrl("/BourseHub").build();
connection.start()
    .catch(function (err) { return console.error(err.tostring()) })

document.getElementById("createbt").addEventListener("click", function (event) {
    var nom = document.getElementById("nom").value;
    var valeur = document.getElementById("valeur").value;
    var variation = document.getElementById("variation").value;
    const bourse = {
        id: 0, nom: nom, valeur: valeur, variation: variation
    };

    console.log(bourse);

    fetch(url, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(bourse)
    })
        .then(response => response.json())
        .then(() => {
            connection.invoke("SendMessage").catch(function (err) {
                return console.error(err.toString());
            });
        })
        .catch(error => alert("Erreur API"));
    event.preventDefault();
});