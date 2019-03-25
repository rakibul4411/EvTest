var myApp = angular.module("myApp", []);
myApp.controller("mainController", function ($scope, $http) {
    $scope.PersonVM = {};
    $scope.model = {};
    function GetAll() {
        $http({
            method: "GET",
            url: "Person/GetAll"
        }).then(
            function (response) {
                $scope.PersonVM = response.data;
            },
            function () {
                alert("Failed.");
                console.log(response.data);
            });
    }
    $scope.btnSubmit = "Create";
    function Create(model) {
        var json = model;
        $http({
            method: "POST",
            url: "Person/Add",
            data: json
        }).then(
            function (response) {
                $scope.model = {};
                GetAll();
            },
            function () {
                alert("Failed.");
                console.log(response.data);
            });
    }
    $scope.btnUpdate = function (ps) {
        var editData = { "id": ps };
        $http({
            method: "GET",
            url: "Person/Update" + "/" + ps.id
        }).then(
            function (response) { $scope.model = response.data; },
            function () { alert("Failed."); }
        );
        $scope.btnSubmit = "Update";
    };

    function Update(model) {
        var json = model;
        $http({
            method: "PUT",
            url: "Person/Update",
            data: json
        }).then(
            function (response) {
                $scope.model = {};
                GetAll();
            },
            function () {
                alert("Failed.");
                console.log(response.data);
            });
    }
    $scope.btnDelete = Delete;
    GetAll();
    function Delete(model) {
        var json = model;
        var c = confirm("Are you sure to delete this record?");
        if (c == true) {
            $http({
                method: "POST",
                url: "Person/Delete",
                data: json
            }).then(
                function (response) {
                    GetAll();
                    alert("Deleted!!!");
                },
                function () {
                    alert("Failed.");
                    console.log(response.data);
                });
        }
    }
    $scope.Click = function (model) {
        if ($("#btnSubmit").val() == "Create") {
            Create(model);
        }
        if ($("#btnSubmit").val() == "Update") {
            Update(model);            
        }
    }
});