/*
 * Created by Yong-Jun Shin (UCONN CSML) on 2015-12-13.
 */

    $(function () {

        $.connection.hub.url = "http://lapack.azurewebsites.net/signalr/";

        // Declare a proxy to reference the hub.
        var blas = $.connection.bLAS;

        // Start the connection.
        $.connection.hub.start().done(function () {
            alert('Now connected, connection ID=' + $.connection.hub.id)
            $('#send').click(function () {
                var mat1JSON = $('#mat1').val();
                var mat2JSON = $('#mat2').val();
                // Call the matrix multiplication method on the hub.
                blas.server.blas1(mat1JSON, mat2JSON);
            });
        });
        // Create a function that the hub can call to display the product.
        blas.client.displayBlas1 = function (product) {
            //Parse JSON using JSON.parse  http://www.w3schools.com/json/json_eval.asp
            var productObj = JSON.parse(product);
            //Display the product in Javascript multidimensional array format
            /*var numRows = productObj.data.length;
             var displayProduct = '[';
             for (i = 0; i < numRows; i++){
             displayProduct += '[' + productObj.data[i] + ']';
             if (i != numRows - 1) {
             displayProduct += ','
             } else displayProduct += ']'
             }
             document.getElementById("matrixProduct").innerHTML = 'The product matrix is ' + displayProduct;
             */
            document.getElementById("Product").innerHTML = 'The dot product is ' + productObj;
        };
    });