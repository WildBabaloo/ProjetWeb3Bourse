const url = "/api/BourseAPI"
let $bourses = $("#bourses")
getBourses();

function getBourses() {
    fetch(url)
        .then(response => response.json())
        .then(data => data.forEach(bourse => {
            let template = `<tr>
                                <td>${bourse.nom}</td>
                                <td>${bourse.valeur}</td>
                                <td>${bourse.variation}</td>
                                <td>
                                <a href = "/Bourses/Edit/${bourse.id}">Edit</a> |
                                <a href = "/Bourses/Delete/${bourse.id}">Delete</a>
                                </td>
                             </tr>`;
            $bourses.append($(template));
        }
        ))
        .catch(error => alert("Erreur API"));
}

const connection = new signalR.HubConnectionBuilder().withUrl("/BourseHub").build();
connection.start()
    .catch(function (err) { return console.error(err.tostring()) })

connection.on("BourseChange", function () {
    $bourses.empty();
    getBourses();
    console.log(getBourses.length)
})