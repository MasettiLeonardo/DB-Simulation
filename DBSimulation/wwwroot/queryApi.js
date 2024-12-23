const apiBaseUrl = 'https://localhost:7090'; // cambia la porta ciula

document.addEventListener('DOMContentLoaded', () => {
    document.querySelector('#queryForm').addEventListener('submit', async (event) => {
        event.preventDefault();

        const queryInput = document.getElementById('queryInput').value;
        const queryResults = document.getElementById('queryResults');

        try {
            const response = await fetch(`${apiBaseUrl}/api/Query`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ Content: queryInput })
            });

            if (!response.ok)
                throw new Error('Failed to execute query');


            const data = await response.json();

            console.log(data); // debug

            queryResults.innerHTML = '';  // refresh dei risultati precedenti

            if (data && Array.isArray(data) && data.length > 0) {
                const table = document.createElement('table');
                table.className = 'table table-bordered table-striped';

                const thead = document.createElement('thead');
                const headerRow = document.createElement('tr');

                // questa parte di codice mi ha fatto bestemmiare non poco
                Object.keys(data[0]).forEach(key => {
                    const th = document.createElement('th');
                    th.textContent = key;
                    headerRow.appendChild(th);
                });

                thead.appendChild(headerRow);
                table.appendChild(thead);

                const tbody = document.createElement('tbody');

                data.forEach(row => {
                    const tr = document.createElement('tr');

                    Object.values(row).forEach(value => {
                        const td = document.createElement('td');
                        td.textContent = value;
                        tr.appendChild(td);
                    });

                    tbody.appendChild(tr);
                });

                table.appendChild(tbody);
                queryResults.appendChild(table);
            } else {
                queryResults.innerHTML = '<p class="text-danger text-center">Nessun risultato trovato.</p>';
            }
        } catch (error) {
            console.error('Error executing query:', error);
            queryResults.innerHTML = '<p class="text-danger text-center">Si e\' verificato un errore durante l\'esecuzione della query.</p>';
        }
    });
}); 