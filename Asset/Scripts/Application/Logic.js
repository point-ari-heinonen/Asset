/// <reference path="../typings/jquery/jquery.d.ts" />
var AssignLocationModel = (function () {
    function AssignLocationModel() {
    }
    return AssignLocationModel;
})();
var LocationModel = (function () {
    function LocationModel() {
    }
    return LocationModel;
})();
function initAssetAssignment() {
    $("#AssignAssetButton").click(function () {
        var locationCode = $("#LocationCode").val();
        var assetCode = $("#AssetCode").val();
        alert("L: " + locationCode + ", A: " + assetCode);
        var data = new AssignLocationModel();
        data.LocationCode = locationCode;
        data.AssetCode = assetCode;
        $.ajax({
            type: "POST",
            url: "/Asset/AssignLocation",
            data: JSON.stringify(data),
            contentType: "application/json",
            success: function (data) {
                if (data.success == true) {
                    alert("Asset successfully assigned.");
                }
                else {
                    alert("There was an error: " + data.error);
                }
            },
            dataType: "json"
        });
    });
}
function initLocation() {
    $("#LocationButton").click(function () {
        var newLocationCode = $("#NewLocationCode").val();
        var newLocationAddress = $("#NewLocationAddress").val();
        var newLocationName = $("#NewLocationName").val();
        alert(newLocationCode + newLocationName + newLocationAddress);
        var data = new LocationModel();
        data.LocationAddress = newLocationAddress;
        data.LocationCode = newLocationCode;
        data.LocationName = newLocationName;
        $.ajax({
            type: "POST",
            url: "/Asset/Location",
            data: JSON.stringify(data),
            contentType: "application/json",
            success: function (data) {
                if (data.success == true) {
                    alert("Asset successfully assigned.");
                }
                else {
                    alert("There was an error: " + data.error);
                }
            },
            dataType: "json"
        });
    });
}
//# sourceMappingURL=Logic.js.map