'use strict';
app.controller('main',
[
    '$scope',
    '$http',
    function (
        $scope,
        $http) {


        $scope.allowToMakeRequest = true;
        $scope.requestModel = {};
        $scope.responseModel = {};
        $scope.convert = {};

        $scope.convertToBtc = function(isValid) {
            if (isValid) {
                $http({
                    method: 'GET',
                    url: '/api/jeeb/rialtobtc?value=' + $scope.convert.irr,

                }).then(function(result) {
                    if (result.data) {
                        $scope.convert.btc = result.data;
                    }

                });
            }
        };

        var formPost = function (path, params, method) {

            //console.log(params);
            //debugger;
            method = method || "post";

            var form = document.createElement("form");
            form.setAttribute("method", method);
            form.setAttribute("action", path);

            for (var key in params) {
                if (params.hasOwnProperty(key)) {
                    if (params[key] !== null) {
                        var hiddenField = document.createElement("input");
                        hiddenField.setAttribute("type", "hidden");
                        hiddenField.setAttribute("name", key);
                        hiddenField.setAttribute("value", params[key]);
                        form.appendChild(hiddenField);
                    }
                }
            }

            document.body.appendChild(form);
            form.submit();
        }

        $scope.submit = function (isValid) {
            if (isValid) {
                formPost('/jeeb/createinvoice',
                    {
                        requestAmount: $scope.requestModel.requestAmount,
                        orderNo: $scope.requestModel.orderNo
                    });
            }

        }
        

    }
]);