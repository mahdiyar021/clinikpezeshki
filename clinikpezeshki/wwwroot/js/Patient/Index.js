﻿$(document).ready(function (){$(".sampleTable").fancyTable({pagination:true,pagination:'btn btn-primary',inputPlaceholder:'جستجو',globalSearch:true});});var toastElList =[].slice.call(document.querySelectorAll('.toast'));var toastList=toastElList.map(function(toastEl){return new bootstrap.Toast(toastEl,option);});