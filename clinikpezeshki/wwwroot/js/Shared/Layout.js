var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
  return new bootstrap.Tooltip(tooltipTriggerEl)
})
$(document).ready(function () {
    let url = window.location.href;
    const array = url.split("/");
    if ("OnlineReserve" == array[array.length - 1]) $("#hor").addClass("active");
    if ("AboutUs" == array[array.length - 1]) $("#hau").addClass("active");
    if ("Login" == array[array.length - 1]) $("#li").addClass("active");
    if ("Questions" == array[array.length - 1]) $("#hq").addClass("active");
})