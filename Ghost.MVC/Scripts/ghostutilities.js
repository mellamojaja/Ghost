$(document).ready(function () {
    var url = 'http://localhost:50519/api/Analyse';
    
    $.post(url, { Word: "hola" }, function (data) {
        console.log(data);
    })
});