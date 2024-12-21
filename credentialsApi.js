const apiBaseUrl = 'https://localhost:7090'; // cambia la porta ciula

document.addEventListener('DOMContentLoaded', () => {
    const dbConnectionForm = document.querySelector('#dbConnectionForm');

    dbConnectionForm.addEventListener('submit', async (event) => {
        event.preventDefault();

        const formData = new FormData(event.target);
        const credentials = {
            hostname: formData.get('hostname'),
            username: formData.get('username'),
            password: formData.get('password'),
            databaseName: formData.get('database'),
            port: formData.get('port'),
        };

        try {
            const resp = await fetch(`${apiBaseUrl}/api/Credentials`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(credentials)
            });

            if (!resp.ok) {
                const errorData = await resp.json();
                throw new Error(errorData.message || 'Errore durante la connessione al database.');
            }

            window.location.href = './query.html';
        } catch (error) {
            console.error('Errore durante la connessione:', error);
            alert(`Errore: ${error.message}`);
        }
    });
});