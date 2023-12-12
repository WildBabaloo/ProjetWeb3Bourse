const url = "/api/BourseAPI";
const connection = new signalR.HubConnectionBuilder().withUrl("/BourseHub").build();
connection.start()
    .catch(function (err) { return console.error(err.tostring()) })

document.getElementById("savebt").addEventListener("click", function (event) {
    var id = document.getElementById("id").value;
    var nom = document.getElementById("nom").value;
    var valeur = document.getElementById("valeur").value;
    var variation = document.getElementById("variation").value;

    const bourse = {
        id: id, nom: nom, valeur: valeur, variation: variation
    };

    fetch(url + "/" + id, {
        method: "PUT",
        headers: {
            'Accept': "application/json",
            'Content-Type': "application/json"
        },
        body: JSON.stringify(bourse)
    })
        .then(response => response.json())
        .then(() => {
            connection.invoke("SendMessage").catch(function (err) {
                return console.error(err.tostring());
            });
        })
        .catch(error => alert("Erreur d'API"));
    event.preventDefault();
});
