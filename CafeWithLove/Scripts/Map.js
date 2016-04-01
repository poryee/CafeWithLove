var map;
var cafeMarker;
var currentLocation;
var inputLocation;
var currentCafeLocation;
var markersArray = [];
var carparkList;

/*SEARCH PAGE - DETAILS*/
$(function () {
    if (window.location.pathname.match('CafeDetails/Details')) {
        google.maps.event.addDomListener(window, 'load', initialDetailGoogleMap);
    }
});

//Details Setup Google Map
function initialDetailGoogleMap()
{
    var postalCode = document.getElementById("postalCode");
    var mapProp = {
        center: new google.maps.LatLng(1.3285714, 103.9283453),
        zoom: 11,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    }

    map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
    getCoordinate(postalCode.innerHTML, map);
}

//How to get there from current or specific location to cafe location
function getThereClicked(value)
{
    if (cafeMarker != undefined && cafeMarker != '') {
        cafeMarker.setMap(null);
        cafeMarker = '';
    }
    inputLocation = value;
    displayRoute();
}

// Displaying the route from current / specific location to cafe location
function displayRoute() {
    clearOverlays();
    var start
    var end
    if (inputLocation == "") {start = currentLocation; end = currentCafeLocation;}
    else{start = inputLocation; end = currentCafeLocation;}

    var directionsDisplay = new google.maps.DirectionsRenderer();// also, constructor can get "DirectionsRendererOptions" object
    directionsDisplay.setMap(map); // map should be already initialized.

    var request = {origin: start, destination: end, travelMode: google.maps.TravelMode.DRIVING};
    var directionsService = new google.maps.DirectionsService();
    directionsService.route(request, function (response, status) {
        if (status == google.maps.DirectionsStatus.OK) {
            directionsDisplay.setDirections(response);
        }
    });
    markersArray.push(directionsDisplay);
}

//Current location of device
function getCurrentLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        x.innerHTML = "Geolocation is not supported by this browser.";
    }
}

//Show Position of the location
function showPosition(position) {
    currentLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
    createMarker(currentLocation);
}





/*SEARCH PAGE - INDEX*/
var var_infowindow;

//map Initialization for Search Cafe
function map_init(var_postalcode, var_cafename, var_contentstring) {
    var location = new google.maps.LatLng(1.3614221, 103.8238732); //center map to coordinates trying
    var mapOptions = {
        center: location, //center map to coordinates
        zoom: 11, //Initial zoom level
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    map = new google.maps.Map(document.getElementById("map-container"), mapOptions);
    var_infowindow = new google.maps.InfoWindow({ content: var_contentstring });

    // Create the DIV to hold the control and call the CenterControl()
    var centerControlDiv = document.createElement('div');
    var centerControl = new CenterControl(centerControlDiv, map);

    centerControlDiv.index = 1;
    map.controls[google.maps.ControlPosition.BOTTOM_LEFT].push(centerControlDiv);

    getCoordinate(var_postalcode, map);

    $('#mapmodals').on('shown.bs.modal', function () {
        google.maps.event.trigger(map, "resize");
        map.setCenter(currentCafeLocation);
        map.setZoom(16);
    });

}

//Convert Cafe Postalcode to lat and lon coordinates
function getCoordinate(postalCode, map) {
    var geocoder = new google.maps.Geocoder();
    var result = "";
    geocoder.geocode({ 'address': String(postalCode) }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            currentCafeLocation = results[0].geometry.location;
            result = results[0].geometry.location;
            createMarker(result, map);
            setInfoWindow(map);
        } else {
            result = "Unable to find address: " + status;
        }
    });
    return result;
}

var whatMarker = "0";
//Create Map Marker of Search Cafe Location
function createMarker(latlng) {
    var divName = map.getDiv();
    var divName = divName.id;
    
    //Check which marker icon image to be set for the markers
    if (divName == "map-container" && whatMarker == "0") {
        var icon = {
            url: "/Images/currentLocation.png", // url
            scaledSize: new google.maps.Size(30, 30), // scaled size
            origin: new google.maps.Point(0, 0), // origin
            anchor: new google.maps.Point(0, 0) // anchor
        };
        cafeMarker = new google.maps.Marker({
            map: map,
            icon: icon,
            position: latlng
        });
    }
    else if (whatMarker == "1")
    {
        var icon = {
            url: "/Images/carparkLocation.png", // url
            scaledSize: new google.maps.Size(30, 30), // scaled size
            origin: new google.maps.Point(0, 0), // origin
            anchor: new google.maps.Point(0, 0) // anchor
        };
        cafeMarker = new google.maps.Marker({
            map: map,
            icon: icon,
            position: latlng
        });
    }
    else
    {
        cafeMarker = new google.maps.Marker({
            map: map,
            position: latlng
        });
    }
}

//Clear Markers
function clearOverlays() {
    for (var i = 0; i < markersArray.length; i++) {
        markersArray[i].setMap(null);
    }
    markersArray.length = 0;
}

//Setting Info Window for the Markers
function setInfoWindow(map)
{
    google.maps.event.addListener(cafeMarker, 'click', function () {
        var_infowindow.open(map, cafeMarker);
    });
    google.maps.event.addListener(var_infowindow, 'closeclick', function () {
        map.setCenter(currentCafeLocation);
    });
}

//Pop up function
$(document).on("click", ".openmodal", function () {
    var cafeName = $(this).data('cafename');
    var cafeDesc = $(this).data('cafedesc');
    var cafeLogo = $(this).data('cafelogo');
    var cafePostalCode = $(this).data('cafepostalcode');
    var cafeAddress = $(this).data('cafeaddress');
    var cafeWebsite = $(this).data('cafewebsite');
    var cafeContact = $(this).data('cafecontact');

    //Pass Data into javascript for carpark
    carparkList = $(this).data('object');

    map_init(cafePostalCode, "Click to visit the " + cafeName + "",
          '<div id="mapInfo">' +
          '<p><img class="img-rounded" src="/Images/' + cafeLogo + '" alt="' + cafeName + '" width="150px" /><br><br>' +
          cafeDesc + '<br>' +
          cafeAddress + '<br>' +
          cafePostalCode + '<br>' +
          cafeContact + '<br>' +
          '<a href="' + cafeWebsite + '" target="_blank">Plan your visit</a>' +
          '</div>');
    $(".modal-header #title_id").html(cafeName);
    $('#mapmodals').modal('show');
});

//GoogleMap Extra Buttons for LIST OF CARPARK
function CenterControl(controlDiv, map) {
    // Set CSS for the control border.
    var controlUI = document.createElement('div');
    controlUI.style.backgroundColor = '#fff';
    controlUI.style.border = '2px solid #fff';
    controlUI.style.borderRadius = '3px';
    controlUI.style.boxShadow = '0 2px 6px rgba(0,0,0,.3)';
    controlUI.style.cursor = 'pointer';
    controlUI.style.marginBottom = '22px';
    controlUI.style.textAlign = 'center';
    controlUI.title = 'Click to recenter the map';
    controlDiv.appendChild(controlUI);

    // Set CSS for the control interior.
    var controlText = document.createElement('div');
    controlText.style.color = 'rgb(25,25,25)';
    controlText.style.fontFamily = 'Roboto,Arial,sans-serif';
    controlText.style.fontSize = '16px';
    controlText.style.lineHeight = '38px';
    controlText.style.paddingLeft = '5px';
    controlText.style.paddingRight = '5px';
    controlText.innerHTML = 'List Carparks';
    controlUI.appendChild(controlText);

    // Setup the click event listeners: simply set the map to Chicago.
    controlUI.addEventListener('click', function () {
        closest = findClosestN();
        for (var i = 0; i < closest.length; i++) {
            var position = new google.maps.LatLng(closest[i].lat, closest[i].lon);
            createMarker(position);
        }
        whatMarker = "0";
    });

}

//Find closest carpark
function findClosestN() {
    // Multiple Markers
    var closest = [];

    for (var i = 0; i < carparkList.length; i++) {
        var position = new google.maps.LatLng(carparkList[i].lat, carparkList[i].lon)
        carparkList[i].distance = google.maps.geometry.spherical.computeDistanceBetween(currentCafeLocation, position) / 1000;

        var R = 15; // radius of earth in km
        if (carparkList[i].distance < R)
        {
            whatMarker = "1";
            closest.push(carparkList[i]);
        }
    }
    closest.sort(sortByDist);
    return closest.splice(0, 3);
}

//Sort Distance for nearest carpark list
function sortByDist(a, b) {
    return (a.distance - b.distance)
}