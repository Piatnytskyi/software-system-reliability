using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.OdeSolvers;

namespace Reliability.Core
{
    public class SoftwareHardwareSystem7
    {
        public Vector<double> GetDenialApproximations(int start, int end,
            double lambda1, double lambda2, double lambda3, double lambda4,
            double eta1, double eta2, double eta3, double eta4)
        {
            double[] initialY0 = new double[37];
            initialY0[0] = 1;
            Vector<double> y0 = Vector<double>.Build.Dense(initialY0);

            Vector<double>[] approximations = RungeKutta.FourthOrder(
                y0,
                start,
                end,
                38,
                (t, P) =>
                    Vector<double>.Build.Dense(new double[]
                    {
                        -(lambda1+lambda2+lambda3+2*lambda4)*P[0]+eta1*P[1]+eta2*P[2]+eta3*P[3]+eta4*P[4],          //1
                        -(eta1+lambda2+lambda3+2*lambda4)*P[1]+eta2*P[6]+eta3*P[7]+eta4*P[8]+lambda1*P[0],           //2
                        -(eta2+lambda1+lambda3+2*lambda4)*P[2]+eta1*P[6]+eta3*P[10]+eta4*P[11]+lambda2*P[0],         //3
                        -(eta3+lambda1+lambda2+2*lambda4)*P[3]+eta1*P[7]+eta2*P[10]+eta4*P[13]+lambda3*P[0],         //4
                        -eta4*P[4]+lambda4*P[0],                                       //5
                        -eta4*P[5]+lambda4*P[0],                                       //6
                        -(eta1+eta2+lambda3+2*lambda4)*P[6]+eta3*P[15]+eta4*P[16]+lambda1*P[2]+lambda2*P[1],         //7
                        -(eta1+eta3+lambda2+2*lambda4)*P[7]+eta2*P[15]+eta4*P[18]+lambda1*P[3]+lambda3*P[1],         //8
                        -eta4*P[8]+lambda4*P[1],                                       //9
                        -eta4*P[9]+lambda4*P[1],                                     //10
                        -(eta2+eta3+lambda1+2*lambda4)*P[10]+eta1*P[15]+eta4*P[20]+lambda2*P[3]+lambda3*P[2],       //11
                        -eta4*P[11]+lambda4*P[2],                                     //12
                        -eta4*P[12]+lambda4*P[2],                                     //13
                        -eta4*P[13]+lambda4*P[3],                                     //14
                        -eta4*P[14]+lambda4*P[3],                                     //15
                        -(eta1+eta2+eta3)*P[15]+lambda1*P[10]+lambda2*P[7]+lambda3*P[6],                  //16
                        -eta4*P[16]+lambda4*P[6],                                     //17
                        -eta4*P[17]+lambda4*P[6],                                     //18
                        -eta4*P[18]+lambda4*P[7],                                     //19
                        -eta4*P[19]+lambda4*P[7],                                     //20
                        -eta4*P[20]+lambda4*P[10],                                    //21
                        -eta4*P[21]+lambda4*P[10],                                   //22
                        -(lambda1+lambda2+lambda3+lambda4)*P[22]+eta1*P[23]+eta2*P[24]+eta3*P[25]+eta4*P[26]+eta4*P[5], //23
                        -(eta1+lambda2+lambda3+lambda4)*P[23]+eta2*P[27]+eta3*P[28]+eta4*P[29]+lambda1*P[22]+eta4*P[19],//24
                        -(eta2+lambda1+lambda3+lambda4)*P[24]+eta1*P[27]+eta3*P[30]+eta4*P[31]+lambda2*P[22]+eta4*P[12],//25
                        -(eta3+lambda1+lambda2+lambda4)*P[25]+eta1*P[28]+eta2*P[30]+eta4*P[32]+lambda3*P[22]+eta4*P[14],//26
                        -eta4*P[26]+lambda4*P[22],                                    //27
                        -(eta1+eta2+lambda3+lambda4)*P[27]+eta3*P[33]+eta4*P[34]+lambda1*P[24]+lambda2*P[23]+eta4*P[17],//28
                        -(eta1+eta3+lambda2+lambda4)*P[28]+eta2*P[33]+eta4*P[35]+lambda1*P[25]+lambda3*P[23]+eta4*P[19],//29
                        -eta4*P[29]+lambda4*P[23],                                 //30
                        -(eta2+eta3+lambda1+lambda4)*P[30]+eta1*P[33]+eta4*P[36]+lambda2*P[25]+lambda3*P[24]+eta4*P[21],//31
                        -eta4*P[31]+lambda4*P[24],                                    //32
                        -eta4*P[32]+lambda4*P[25],                                    //33
                        -(eta1+eta2+eta3)*P[33]+lambda1*P[30]+lambda2*P[28]+lambda3*P[27],                //34
                        -eta4*P[34]+lambda4*P[27],                                    //35
                        -eta4*P[35]+lambda4*P[28],                                    //36
                        -eta4*P[36]+lambda4*P[30]                                    //37
                    }
                ));

            return approximations.Last();
        } 
    }
}