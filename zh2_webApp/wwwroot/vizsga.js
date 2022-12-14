fetch("/api/minden").then(r => r.json()).then(data => kezel(data));

function kezel(adat) {
    console.log(adat.length);

    for (var i = 0; i < adat.length; i++) {
        kirak(adat[i]);
    }
}
function kirak(a) {
    let ide = document.getElementById("ide");
    let elem = document.createElement("div");
    if (a.work == null) {
        elem.innerHTML = "Tanuló neve: " + a.name + "; Születési dátuma: " + a.birthdate.substring(0,10);
    }
    else {
        elem.innerHTML = "Tanuló neve: " + a.name + "; Születési dátuma: " + a.birthdate.substring(0, 10) + "; Munka típusa: " + a.work;
    }

    ide.appendChild(elem);
}

function torol(mit) {
    let elem = document.getElementById(mit);
    while (elem.firstChild) {
        elem.removeChild(elem.firstChild);
    }
}

function szurById() {
    torol("ide")
    let textbox = document.getElementById("byId").value;
    fetch("/api/1/" + textbox).then(r => r.json()).then(d => kirak(d));
}
function szurByName() {
    torol("ide")
    let textbox = document.getElementById("byName").value;
    fetch("/api/2/" + textbox).then(r => r.json()).then(d => kirak(d));
}
