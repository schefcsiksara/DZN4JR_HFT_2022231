let data = [];
let paints = [];
let connection = null;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:49044/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("CustomerCreated", (user, message) => {
        getdata();
    });

    connection.on("CustomerDeleted", (user, message) => {
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
    await fetch('http://localhost:49044/customer/')
        .then(x => x.json())
        .then(y => {
            data = y;
            display();
        });

    await fetch('http://localhost:49044/paint/')
        .then(x => x.json())
        .then(y => {
            paints = y;
            displayDropdown();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    data.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td>"
            + "<td>" + t.customerName + "</td>"
            + `<td><button type="button" onclick="remove(${t.id})">Delete</button>`
            + `<td><button type="button" onclick="selectForUpdate(${t.id})">Update</button>`
            + "</td></tr>";
    });
}

function displayDropdown() {
    var select = document.getElementById('paint');
    select.innerHTML = "";
    paints.forEach(t => {
        var option = document.createElement("option");
        option.value = t.id;
        option.innerHTML = t.color;
        select.appendChild(option);
    });
}

function selectForUpdate(id) {
    fetch('http://localhost:49044/customer/' + id, {
        method: 'GET',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(x => x.json())
        .then(data => {
            console.log(data);
            document.getElementById('id').value = data.id;
            document.getElementById('name').value = data.customerName;
            document.getElementById('address').value = data.adress;
            document.getElementById('email').value = data.email;
            document.getElementById('regular').value = data.regularCustomer? "yes" : "no";
            document.getElementById('paint').value = data.favoritePaintId;
        })
        .catch((error) => { console.error('Error:', error); });
}

function remove(id) {
    fetch('http://localhost:49044/customer/' + id, {
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
    let address = document.getElementById('address').value;
    let email = document.getElementById('email').value;
    let regular = document.getElementById('regular').value == "yes";
    let paint = document.getElementById('paint').value;

    fetch('http://localhost:49044/customer/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                Id: id,
                CustomerName: name,
                Adress: address,
                Email: email,
                RegularCustomer: regular,
                FavoritePaintId: paint,
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
    let address = document.getElementById('address').value;
    let email = document.getElementById('email').value;
    let regular = document.getElementById('regular').value == "yes";
    let paint = document.getElementById('paint').value;

    fetch('http://localhost:49044/customer/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                Id: id,
                CustomerName: name,
                Adress: address,
                Email: email,
                RegularCustomer: regular,
                FavoritePaintId: paint,
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}