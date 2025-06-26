




$(function () {
    $("#loginButton").on("click", function (e) {
        e.preventDefault();

        const data = {
            username: $("#loginUsername").val(),
            password: $("#loginPassword").val(),
            rememberMe: $("#loginRememberme").prop("checked")
        };
        $("#loginJson").val(JSON.stringify(data));
        $("#loginForm").submit();
    });

    $("#registerButton").on("click", function (e) {
        e.preventDefault();

        const data = {
            username: $("#registerUsername").val(),
            password: $("#registerPassword").val(),
            email: $("#registerEmail").val(),
            rememberMe: $("#registerRememberme").prop("checked")
        };
        console.log("Submitting:", JSON.stringify(data));
        $("#registerJson").val(JSON.stringify(data));
        $("#registerForm").submit();
    });

    // Sync login's remember me -> register's remember me
    $("#loginRememberme").on("click", function () {
        const isChecked = $("#loginRememberme").prop("checked");
        console.log(isChecked);
        $("#registerRememberme").prop("checked", isChecked);
    });

    // Sync register's remember me -> login's remember me
    $("#registerRememberme").on("click", function () {
        const isChecked = $("#registerRememberme").prop("checked");
        console.log(isChecked);
        $("#loginRememberme").prop("checked", isChecked);
    });

});


function toggleModal(showId, hideId) {
    document.getElementById(showId).style.display = 'block';
    document.getElementById(hideId).style.display = 'none';
}