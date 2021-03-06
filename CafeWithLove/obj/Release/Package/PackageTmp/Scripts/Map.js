﻿var map;
var cafeMarker;
var currentLocation;
var inputLocation;
var currentCafeLocation;
var markersArray = [];
var object;

$(function () {
    if (window.location.pathname.match('CafeDetails/Details')) {
        google.maps.event.addDomListener(window, 'load', initialDetailGoogleMap);
    }
});

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

function getThereClicked(value)
{
    if (cafeMarker != undefined && cafeMarker != '') {
        cafeMarker.setMap(null);
        cafeMarker = '';
    }

    inputLocation = value;
    displayRoute();
}

function displayRoute() {
    clearOverlays();
    var start
    var end
    if (inputLocation == "") {
        start = currentLocation;
        end = currentCafeLocation;
    }
    else
    {
        start = inputLocation;
        end = currentCafeLocation;
    }

    var directionsDisplay = new google.maps.DirectionsRenderer();// also, constructor can get "DirectionsRendererOptions" object
    directionsDisplay.setMap(map); // map should be already initialized.

    var request = {
        origin: start,
        destination: end,
        travelMode: google.maps.TravelMode.DRIVING
    };
    var directionsService = new google.maps.DirectionsService();
    directionsService.route(request, function (response, status) {
        if (status == google.maps.DirectionsStatus.OK) {
            directionsDisplay.setDirections(response);
        }
    });
    markersArray.push(directionsDisplay);
    //markersArray.push(start);
    //markersArray.push(end);
}

function setupLocationMarker(map) {
    // a sample list of JSON encoded data of places to visit in Tunisia
    // you can either make up a JSON list server side, or call it from a controller using JSONResult
    var data = [
              { "Id": 1, "Name": "Cafe With Love", "GeoLong": "1.4229369", "GeoLat": "103.8460359" },
              { "Id": 2, "Name": "49 Seater ", "GeoLong": "1.423867", "GeoLat": "103.8451212" },
              { "Id": 3, "Name": "Fat Cat", "GeoLong": "1.424749", "GeoLat": "103.8449983" },
              { "Id": 4, "Name": "CafeII", "GeoLong": "1.4256178", "GeoLat": "103.8445531" }
    ];

    $.each(data, function (i, item) {
        //Set list of Markers - jquery each selector to iteration through json list
        var icon = {
            url: "/Content/Images/cafemarkers.png", // url
            scaledSize: new google.maps.Size(30, 30), // scaled size
            origin: new google.maps.Point(0, 0), // origin
            anchor: new google.maps.Point(0, 0) // anchor
        };

        var marker = new google.maps.Marker({
            position: new google.maps.LatLng(item.GeoLong, item.GeoLat),
            map: map,
            title: item.Name,
            icon: icon
        });

        // InfoWindow content
        var contentString = '<div id="iw-container">' +
                          '<div class="iw-title">Boufe Boutique Cafe</div>' +
                          '<div class="iw-content">' +
                            '<div class="iw-subTitle">History</div>' +
                            '<img src="~/images/Cafe/fat_cat.jpg" alt="Porcelain Factory of Vista Alegre" height="115" width="83">' +
                            '<p>Located in Phoenix Park, blending simplicity and serenity is what Boufe does best – whether its fusing minimalistic architecture with well thought about food elements, or blending strong flavors into fresh modern menu items.   Attracting a fusion of personalities, from coffee lovers, holidaymakers, fashionistas, locals and families with children and more, the Boufe Boutique Cafe concept certainly proves their sincerity by bringing you an intimate, yet memorable cafe setting away from the bustle. .</p>' +
                            '<div class="iw-subTitle">Contacts</div>' +
                            '<p>VISTA ALEGRE ATLANTIS, SA<br>3830-292 Ílhavo - Portugal<br>' +
                            '<br>Phone. +351 234 320 600<br>e-mail: geral@vaa.pt<br>www: www.myvistaalegre.com</p>' +
                          '</div>' +
                          '<div class="iw-bottom-gradient"></div>' +
                        '</div>';

        //var contentString =
        //	'<div id="infowindow_content">' +
        //    '<p><strong>Heading</strong><br>' +
        //    'this is an example of a line with information<br>' +
        //    'second line of infotext<br>' +
        //    'third line of infotext</p>' +
        //    '<a href="http://www.tutsme-webdesign.info" target="_blank">This is a link</a>' +
        //    '</div>';

        // put in some information about each json object - in this case, the opening hours.
        var infowindow = new google.maps.InfoWindow({
            //content: "<div class='infoDiv'><h2>" + item.Name + "</div></div>"
            //content: "<div class='popup'><h1 class='popup-title'>" + item.Id + "</h1><div id='popup-body'><p><span class='popup-label'>plate:</span> " + item.Name + "</p><p><span class='popup-label'>type:</span> " + item.Name + "</p><p><span class='popup-label'>driver:</span> " + item.Name + "</p><p><span class='popup-label'>lat/lng:</span> " + item.GeoLong + "</p><p><span class='popup-label'>location:</span> " + item.GeoLat + "</p></div></div>"
            //content: "<div class='modal fade' tabindex='-1' role='dialog'><div class='modal-dialog'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button><h4 class='modal-title'>Modal title</h4></div><div class='modal-body'><p>One fine body&hellip;</p></div><div class='modal-footer'><button type='button' class='btn btn-default' data-dismiss='modal'>Close</button><button type='button' class='btn btn-primary'>Save changes</button></div></div></div></div>"
            content: contentString,

            // Assign a maximum value for the width of the infowindow allows
            // greater control over the various content elements
            maxWidth: 350
        });

        // finally hook up an "OnClick" listener to the map so it pops up out info-window when the marker-pin is clicked!
        google.maps.event.addListener(marker, 'click', function () {
            infowindow.open(map, marker);
        });

        // *
        // START INFOWINDOW CUSTOMIZE.
        // The google.maps.event.addListener() event expects
        // the creation of the infowindow HTML structure 'domready'
        // and before the opening of the infowindow, defined styles are applied.
        // *
        google.maps.event.addListener(infowindow, 'domready', function () {

            // Reference to the DIV that wraps the bottom of infowindow
            var iwOuter = $('.gm-style-iw');

            /* Since this div is in a position prior to .gm-div style-iw.
             * We use jQuery and create a iwBackground variable,
             * and took advantage of the existing reference .gm-style-iw for the previous div with .prev().
            */
            var iwBackground = iwOuter.prev();

            // Removes background shadow DIV
            iwBackground.children(':nth-child(2)').css({ 'display': 'none' });

            // Removes white background DIV
            iwBackground.children(':nth-child(4)').css({ 'display': 'none' });

            // Moves the infowindow 115px to the right.
            iwOuter.parent().parent().css({ left: '115px' });

            // Moves the shadow of the arrow 76px to the left margin.
            iwBackground.children(':nth-child(1)').attr('style', function (i, s) { return s + 'left: 76px !important;' });

            // Moves the arrow 76px to the left margin.
            iwBackground.children(':nth-child(3)').attr('style', function (i, s) { return s + 'left: 76px !important;' });

            // Changes the desired tail shadow color.
            iwBackground.children(':nth-child(3)').find('div').children().css({ 'box-shadow': 'rgba(72, 181, 233, 0.6) 0px 1px 6px', 'z-index': '1' });

            // Reference to the div that groups the close button elements.
            var iwCloseBtn = iwOuter.next();

            // Apply the desired effect to the close button
            iwCloseBtn.css({ right: '38px', top: '3px', border: '1px solid #48b5e9', 'border-radius': '13px', 'box-shadow': '0 0 5px #3990B9' });

            // If the content of infowindow not exceed the set maximum height, then the gradient is removed.
            if ($('.iw-content').height() < 140) {
                $('.iw-bottom-gradient').css({ display: 'none' });
            }

            // The API automatically applies 0.7 opacity to the button after the mouseout event. This function reverses this event to the desired value.
            iwCloseBtn.mouseout(function () {
                $(this).css({ opacity: '1' });
            });
        });

    })
}

function getCurrentLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        x.innerHTML = "Geolocation is not supported by this browser.";
    }
}

function showPosition(position) {
    currentLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
    createMarker(currentLocation);
}











/*SEARCH PAGE - INDEX*/
var var_infowindow;

//map Initialization for Search Cafe
function map_init(var_postalcode, var_cafename, var_contentstring) {
    var location = new google.maps.LatLng(1.3614221, 103.8238732); //center map to coordinates trying
    var location2 = new google.maps.LatLng(1.3285714, 103.9283453); //FATCAT LOCATION
    var mapOptions = {
        center: location, //center map to coordinates
        zoom: 11, //Initial zoom level
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    map = new google.maps.Map(document.getElementById("map-container"), mapOptions);
    var_infowindow = new google.maps.InfoWindow({ content: var_contentstring });

    // Create the DIV to hold the control and call the CenterControl()
    // constructor passing in this DIV.
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
    //alert(result + "1");
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
//testing
//Create Map Marker of Search Cafe Location
function createMarker(latlng) {
    var test = map.getDiv();
    var test = test.id;
    
    //Check which marker icon image to be set for the markers
    if (test == "map-container" && whatMarker == "0") {
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
    //markersArray.push(cafeMarker);
}

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


$(document).on("click", ".openmodal", function () {
    var cafeName = $(this).data('cafename');
    var cafeDesc = $(this).data('cafedesc');
    var cafeLogo = $(this).data('cafelogo');
    var cafePostalCode = $(this).data('cafepostalcode');
    var cafeAddress = $(this).data('cafeaddress');
    var cafeWebsite = $(this).data('cafewebsite');
    var cafeContact = $(this).data('cafecontact');

    //testingpls
    object = $(this).data('object');

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
    //object[i].l

    for (var i = 0; i < object.length; i++) {
        var position = new google.maps.LatLng(object[i].lat, object[i].lon)
        object[i].distance = google.maps.geometry.spherical.computeDistanceBetween(currentCafeLocation, position) / 1000;

        var R = 15; // radius of earth in km
        if (object[i].distance < R)
        {
            whatMarker = "1";
            closest.push(object[i]);
        }
    }
    closest.sort(sortByDist);
    return closest.splice(0, 3);
}

function sortByDist(a, b) {
    return (a.distance - b.distance)
}












//Dummy List of Carparks
function setupLocationMarker2(map) {
    // a sample list of JSON encoded data of places to visit in Tunisia
    // you can either make up a JSON list server side, or call it from a controller using JSONResult
    var data = [
              { "Id": 1, "Name": "Cafe With Love", "GeoLong": "1.312367", "GeoLat": "103.797141" },
              { "Id": 2, "Name": "49 Seater ", "GeoLong": "1.314511", "GeoLat": "103.919353" }
    ];

    $.each(data, function (i, item) {
        //Set list of Markers - jquery each selector to iteration through json list
        var icon = {
            url: "/Content/Images/carparkmarker.png", // url
            scaledSize: new google.maps.Size(30, 30), // scaled size
            origin: new google.maps.Point(0, 0), // origin
            anchor: new google.maps.Point(0, 0) // anchor
        };

        var marker = new google.maps.Marker({
            position: new google.maps.LatLng(item.GeoLong, item.GeoLat),
            map: map,
            title: item.Name,
            icon: icon
        });
    })
}