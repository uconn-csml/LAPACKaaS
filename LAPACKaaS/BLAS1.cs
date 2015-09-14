using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System.Diagnostics;

namespace LAPACKaaS
{
    public class BLAS1: Hub
    {
        public double Dot(String mat1, String mat2)
        {
            //Deserialize (JSON --> C# Object) using Newtonsoft.Json namespace  http://www.newtonsoft.com/json
            var A = JsonConvert.DeserializeObject<MatObject>(mat1);  // see below for MatObject class
            var B = JsonConvert.DeserializeObject<MatObject>(mat2);  // see below for MatObject class
            //Dot product: C = C + A*B
            int m = A.size[1]; // the number of rows of Matrix 
            double C = 0; // the dot product 
            for (int i = 0; i < m; i++)
            {
                C = C + A.data[0,i] * B.data[i,0];
            }
            String strC = C.ToString();

            // Call the displayProduct (client) method to update client.
            Clients.All.DisplayProduct(strC);

            return C;
        }
    }

    // This class can be generated using http://json2csharp.com/
    // Enter {"matrixType":"DenseMatrix", "data": [[1,2], [3,4], [5,6]], "size": [3, 2]}
    public class MatObject
    {
        public string matrixType { get; set; }
        public int[,] data { get; set; }
        public int[] size { get; set; }
    }
}
