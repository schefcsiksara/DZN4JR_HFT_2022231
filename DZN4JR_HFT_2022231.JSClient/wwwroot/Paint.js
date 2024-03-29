﻿let data = [];
let brands = [];
let connection = null;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:49044/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("PaintCreated", (user, message) => {
        getdata();
    });

    connection.on("PaintDeleted", (user, message) => {
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
    await fetch('http://localhost:49044/paint/')
        .then(x => x.json())
        .then(y => {
            data = y;
            display();
        });

    await fetch('http://localhost:49044/brand/')
        .then(x => x.json())
        .then(y => {
            brands = y;
            displayDropdown();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    data.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td>"
            + "<td>" + t.color + "</td>"
            + `<td><button type="button" onclick="remove(${t.id})">Delete</button>`
            + `<td><button type="button" onclick="selectForUpdate(${t.id})">Update</button>`
            + "</td></tr>";
    });
}

function displayDropdown() {
    var select = document.getElementById('brand');
    select.innerHTML = "";
    brands.forEach(t => {
        var option = document.createElement("option");
        option.value = t.id;
        option.innerHTML = t.name;
        select.appendChild(option);
    });
}

function selectForUpdate(id) {
    fetch('http://localhost:49044/paint/' + id, {
        method: 'GET',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(x => x.json())
        .then(data => {
            console.log(data);
            document.getElementById('id').value = data.id;
            document.getElementById('color').value = data.color;
            document.getElementById('type').value = data.type;
            document.getElementById('baseprice').value = data.basePrice;
            document.getElementById('volume').value = data.volume;
            document.getElementById('brand').value = data.brandId;
        })
        .catch((error) => { console.error('Error:', error); });
}

function remove(id) {
    fetch('http://localhost:49044/paint/' + id, {
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
    let color = document.getElementById('color').value;
    let type = document.getElementById('type').value;
    let baseprice = document.getElementById('baseprice').value;
    let volume = document.getElementById('volume').value;
    let brand = document.getElementById('brand').value;

    fetch('http://localhost:49044/paint/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                Id: id,
                Color: color,
                Type: type,
                BasePrice: baseprice,
                Volume: volume,
                BrandId: brand,
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
    let color = document.getElementById('color').value;
    let type = document.getElementById('type').value;
    let baseprice = document.getElementById('baseprice').value;
    let volume = document.getElementById('volume').value;
    let brand = document.getElementById('brand').value;

    fetch('http://localhost:49044/paint/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                Id: id,
                Color: color,
                Type: type,
                BasePrice: baseprice,
                Volume: volume,
                BrandId: brand,
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}