// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {
    const toggleButton = document.getElementById("darkModeToggle");
    const icon = document.getElementById("darkModeIcon");
    const body = document.body;

    function applyDarkMode(isDark) {
        body.classList.toggle("dark-mode", isDark);
        icon.classList.remove("bi-moon", "bi-sun");
        icon.classList.add(isDark ? "bi-sun" : "bi-moon");
        localStorage.setItem("darkMode", isDark ? "enabled" : "disabled");
    }

    // Load preference
    const isDark = localStorage.getItem("darkMode") === "enabled";
    applyDarkMode(isDark);

    toggleButton?.addEventListener("click", function () {
        applyDarkMode(!body.classList.contains("dark-mode"));
    });
});
