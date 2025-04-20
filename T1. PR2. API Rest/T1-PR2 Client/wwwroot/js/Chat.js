const chatConnection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7003/xat")
    .withAutomaticReconnect()
    .build();

function sendMessage() {
    const user = document.getElementById("user").value;
    const messageInput = document.getElementById("message");
    const message = messageInput.value.trim();
    if (!message) return;

    connection
        .invoke("SendMessage", user, message)
        .then(() => {
            messageInput.value = "";
            messageInput.focus();
        })
        .catch((err) => console.error(err.toString()));
}

// Recibir mensajes
connection.on("ReceiveMessage", (user, message) => {
    const currentUser = document.getElementById("user").value;
    const msgList = document.getElementById("messages");

    const li = document.createElement("li");
    li.classList.add("list-group-item");
    if (user === currentUser) {
        li.classList.add("user-message");
    } else {
        li.classList.add("other-message");
    }
    li.classList.add("animate-message");
    li.textContent = message;

    msgList.appendChild(li);
    msgList.scrollTop = msgList.scrollHeight;

    // Quitar clase de animación al finalizar
    li.addEventListener("animationend", () => {
        li.classList.remove("animate-message");
    });
});

// Iniciar la conexión
connection.start().catch((err) => console.error(err.toString()));

document.addEventListener('keydown', (event) => {
    if (event.key === 'Enter') {
        event.preventDefault();
    }
});