const togglePassword = document.querySelector("#togglePassword");
const passwords = document.querySelectorAll('input.password');

togglePassword.addEventListener("click", function () {

    passwords.forEach(function (element) {
        // toggle the type attribute
        const type = element.getAttribute("type") === "password" ? "text" : "password";
        element.setAttribute("type", type);
    })

    // toggle the eye icon
    this.classList.toggle('bi-eye-fill');
    this.classList.toggle('bi-eye-slash-fill');
});