let data = [];
let connection = null;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:49044/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("BrandCreated", (user, message) => {
        getdata();
    });

    connection.on("BrandDeleted", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();


}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    await fetch('http://localhost:49044/brand/')
        .then(x => x.json())
        .then(y => {
            data = y;
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    data.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td>"
            + "<td>" + t.name + "</td>"
            + `<td><button type="button" onclick="remove(${t.id})">Delete</button>`
            + `<td><button type="button" onclick="selectForUpdate(${t.id})">Update</button>`
            + "</td></tr>";
    });
}

function selectForUpdate(id) {
    fetch('http://localhost:49044/brand/' + id, {
        method: 'GET',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(x => x.json())
        .then(data => {
            console.log(data);
            document.getElementById('id').value = data.id;
            document.getElementById('name').value = data.name;
            document.getElementById('wholesalername').value = data.wholeSalerName;
            document.getElementById('country').value = data.country;
            document.getElementById('address').value = data.address;
            document.getElementById('rating').value = data.rating;
        })
        .catch((error) => { console.error('Error:', error); });
}

function remove(id) {
    fetch('http://localhost:49044/brand/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function update() {
    let id = document.getElementById('id').value;
    let name = document.getElementById('name').value;
    let wholesalername = document.getElementById('wholesalername').value;
    let country = document.getElementById('country').value;
    let address = document.getElementById('address').value;
    let rating = document.getElementById('rating').value;

    fetch('http://localhost:49044/brand/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                Id: id,
                Name: name,
                WholeSalerName: wholesalername,
                Country: country,
                Address: address,
                Rating: rating,
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function create() {
    let id = document.getElementById('id').value;
    let name = document.getElementById('name').value;
    let wholesalername = document.getElementById('wholesalername').value;
    let country = document.getElementById('country').value;
    let address = document.getElementById('address').value;
    let rating = document.getElementById('rating').value;

    fetch('http://localhost:49044/brand/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                Id: id,
                Name: name,
                WholeSalerName: wholesalername,
                Country: country,
                Address: address,
                Rating: rating,
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}