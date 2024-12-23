const setDarkTheme = () => {
    document.body.classList.add('dark-theme');
    document.body.classList.remove('light-theme');

    document.querySelector('#lightIcon').style.display = 'none';
    document.querySelector('#darkIcon').style.display = 'inline';

    document.querySelector('#toggleThemeBtn').classList.remove('btn-primary');
    document.querySelector('#toggleThemeBtn').classList.add('btn-secondary');
}

const setLightTheme = () => {
    document.body.classList.add('light-theme');
    document.body.classList.remove('dark-theme');

    document.querySelector('#lightIcon').style.display = 'inline';
    document.querySelector('#darkIcon').style.display = 'none';

    document.querySelector('#toggleThemeBtn').classList.remove('btn-secondary');
    document.querySelector('#toggleThemeBtn').classList.add('btn-primary');
}

const checkStoredTheme = () => {
    const savedTheme = localStorage.getItem('theme');

    if (savedTheme === 'dark') {
        setDarkTheme();
    } else {
        setLightTheme();
    }
}

document.querySelector('#toggleThemeBtn').addEventListener('click', () => {
    if (document.body.classList.contains('dark-theme')) {
        setLightTheme();
        localStorage.setItem('theme', 'light');
    } else {
        setDarkTheme();
        localStorage.setItem('theme', 'dark');
    }
});

checkStoredTheme();
