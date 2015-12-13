using System;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

// Yong-Jun Shin
// UCONN CMSL, 2015

namespace LAPACKaaS
{
    public class BLAS: Hub
    {
        public double Blas1(String mat1, String mat2)
        {
            //Deserialize (JSON --> C# Object) using Newtonsoft.Json namespace  http://www.newtonsoft.com/json
            var A = JsonConvert.DeserializeObject<Matrix>(mat1);  // see below for MatObject class
            var B = JsonConvert.DeserializeObject<Matrix>(mat2);  // see below for MatObject class
            //dot product: C = C + A*B
            int N = A.size[1]; // the number of elements in vector A 
            double C = 0; // dot product C
            for (int i = 0; i < N; i++)
            {
                C = C + A.data[0, i] * B.data[i, 0];
            }
            String strC = C.ToString();

            // Call the displayBlas1  method on the client side.
            Clients.All.DisplayBlas1(strC);

            return C;
        }

        // This class can be generated using http://json2csharp.com/
        // Enter {"matrixType":"DenseMatrix", "data": [[1,2], [3,4], [5,6]], "size": [3, 2]}
        public class Matrix
        {
            public string matrixType { get; set; }
            public double [,] data { get; set; }
            public int [] size { get; set; }
        }
    }
}
