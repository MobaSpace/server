/*
 * Modify nav bar style
 */
$(document).ready(function () {
    var Elements = document.getElementsByName('menu')
    Elements.forEach((Element) => {
        var currentPage = document.getElementById('currentPage').value ;
        if (Element.textContent == document.getElementById('currentPage').value ){
            Element.classList.add('font-weight-bold');
            Element.classList.add('underline');
        }
    })
});