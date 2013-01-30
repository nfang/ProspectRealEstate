function initMap() {
    var geolocation = new google.maps.LatLng(-37.90459, 144.9999873);
    var mapContainer = document.getElementById("map");
    if (mapContainer) {
        var options = {
            zoom: 16,
            center: geolocation,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById("map"), options);
        var marker = new google.maps.Marker({
            position: geolocation,
            map: map,
            title: "Prospect Real Estate"
        });
    }
}

function loadMapScript() {
    var script = document.createElement("script");
    script.type = "text/javascript";
    script.src = "http://maps.googleapis.com/maps/api/js?key=AIzaSyAPERoFb_MbIObrqPRU3vSK-7jg_4yjGv8&sensor=false&callback=initMap";
    document.body.appendChild(script);
}

window.onload = loadMapScript;