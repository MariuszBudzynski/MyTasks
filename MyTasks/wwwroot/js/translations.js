//as the translation are small, no need to prep.the full resex architecture

const translations = {
    LogoutButton: { en: "Logout", pl: "Wyloguj" },
};

function applyTranslations() {
    const culture = navigator.language.startsWith('pl') ? 'pl' : 'en';
    document.querySelectorAll("[data-i18n]").forEach(el => {
        const key = el.getAttribute("data-i18n");

        if (translations[key]) {
            el.textContent = translations[key][culture];
        }
    });
}

//we run the script after the full page is loaded
document.addEventListener("DOMContentLoaded", applyTranslations);
