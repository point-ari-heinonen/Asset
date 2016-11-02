/// <reference path="../typings/jquery/jquery.d.ts" />
class AssignLocationModel {
    public AssetCode: string;
    public LocationCode: string;
}
class LocationModel {
    public LocationCode: string;
    public LocationName: string;
    public LocationAddress: string;
}


function initAssetAssignment() {
    $("#AssignAssetButton").click(function () {
        var locationCode: string = $("#LocationCode").val();
        var assetCode: string = $("#AssetCode").val();

        alert("L: " + locationCode + ", A: " + assetCode);
        var data: AssignLocationModel = new AssignLocationModel();
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
        var newLocationCode: string = $("#NewLocationCode").val();
        var newLocationAddress: string = $("#NewLocationAddress").val();
        var newLocationName: string = $("#NewLocationName").val();
        alert(newLocationCode + newLocationName + newLocationAddress);
        var data: LocationModel = new LocationModel();
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

    })
}