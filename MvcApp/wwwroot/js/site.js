// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {
    const toggleButton = document.getElementById("darkModeToggle");
    const icon = document.getElementById("darkModeIcon");
    const body = document.body;
    const DURATION = 200; // keep in sync with CSS (150ms + buffer)

    function setIcon(isDark) {
        if (!icon) return;
        icon.classList.remove("bi-moon", "bi-sun");
        icon.classList.add(isDark ? "bi-sun" : "bi-moon");
    }

    function applyDarkMode(isDark, { animate } = { animate: false }) {
        if (animate) {
            body.classList.add("theme-transition");
            // Next frame: toggle the class to trigger transition
            requestAnimationFrame(() => {
                body.classList.toggle("dark-mode", isDark);
                setIcon(isDark);
                localStorage.setItem("darkMode", isDark ? "enabled" : "disabled");

                // Remove the gate after transition completes
                setTimeout(() => body.classList.remove("theme-transition"), DURATION);
            });
        } else {
            // No animation on initial load
            body.classList.toggle("dark-mode", isDark);
            setIcon(isDark);
            localStorage.setItem("darkMode", isDark ? "enabled" : "disabled");
        }
    }

    // Load preference without animation
    const isDark = localStorage.getItem("darkMode") === "enabled";
    applyDarkMode(isDark, { animate: false });

    toggleButton?.addEventListener("click", function () {
        const next = !body.classList.contains("dark-mode");
        applyDarkMode(next, { animate: true });
    });
});
