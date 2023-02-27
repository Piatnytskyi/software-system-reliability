using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.OdeSolvers;

namespace Reliability.Core
{
    public class SoftwareHardwareSystem7
    {
        public Vector<double> GetDenialApproximations(int start, int end,
            double lambda1, double lambda2, double lambda3, double lambda4, double lambda5,
            double eta1, double eta2, double eta3, double eta4, double eta5)
        {
            double[] initialY0 = new double[37];
            initialY0[0] = 1;
            Vector<double> y0 = Vector<double>.Build.Dense(initialY0);

            Vector<double>[] approximations = RungeKutta.FourthOrder(
                y0,
                start,
                end,
                25,
                (t, P) =>
                    Vector<double>.Build.Dense(new double[]
                    {
                        -(lambda1+lambda2+lambda3+2*lambda4)*P[1]+eta1*P[2]+eta2*P[3]+eta3*P[4]+eta4*P[5],          //1
                        -(eta1+lambda2+lambda3+2*lambda4)*P[2]+eta2*P[7]+eta3*P[8]+eta4*P[9]+lambda1*P[1]           //2
                        -(eta2+lambda1+lambda3+2*lambda4)*P[3]+eta1*P[7]+eta3*P[11]+eta4*P[12]+lambda2*P[1]         //3
                        -(eta3+lambda1+lambda2+2*lambda4)*P[4]+eta1*P[8]+eta2*P[11]+eta4*P[14]+lambda3*P[1]         //4
                        -eta4*P[5]+lambda4*P[1]                                       //5
                        -eta4*P[6]+lambda4*P[1]                                       //6
                        -(eta1+eta2+lambda3+2*lambda4)*P[7]+eta3*P[16]+eta4*P[17]+lambda1*P[3]+lambda2*P[2]         //7
                        -(eta1+eta3+lambda2+2*lambda4)*P[8]+eta2*P[16]+eta4*P[19]+lambda1*P[4]+lambda3*P[2]         //8
                        -eta4*P[9]+lambda4*P[2]                                       //9
                        -eta4*P[10]+lambda4*P[2]                                     //10
                        -(eta2+eta3+lambda1+2*lambda4)*P[11]+eta1*P[16]+eta4*P[21]+lambda2*P[4]+lambda3*P[3]       //11
                        -eta4*P[12]+lambda4*P[3]                                     //12
                        -eta4*P[13]+lambda4*P[3]                                     //13
                        -eta4*P[14]+lambda4*P[4]                                     //14
                        -eta4*P[15]+lambda4*P[4]                                     //15
                        -(eta1+eta2+eta3)*P[16]+lambda1*P[11]+lambda2*P[8]+lambda3*P[7]                  //16
                        -eta4*P[17]+lambda4*P[7]                                     //17
                        -eta4*P[18]+lambda4*P[7]                                     //18
                        -eta4*P[19]+lambda4*P[8]                                     //19
                        -eta4*P[20]+lambda4*P[8]                                     //20
                        -eta4*P[21]+lambda4*P[11]                                    //21
                        -eta4*P[22]+lambda4*P[11]                                    //22
                        -(lambda1+lambda2+lambda3+lambda4)*P[23]+eta1*P[24]+eta2*P[25]+eta3*P[26]+eta4*P[27]+eta4*P[6] //23
                        -(eta1+lambda2+lambda3+lambda4)*P[24]+eta2*P[28]+eta3*P[29]+eta4*P[30]+lambda1*P[23]+eta4*P[20]//24
                        -(eta2+lambda1+lambda3+lambda4)*P[25]+eta1*P[28]+eta3*P[31]+eta4*P[32]+lambda2*P[23]+eta4*P[13]//25
                        -(eta3+lambda1+lambda2+lambda4)*P[26]+eta1*P[29]+eta2*P[31]+eta4*P[33]+lambda3*P[23]+eta4*P[15]//26
                        -eta4*P[27]+lambda4*P[23]                                    //27
                        -(eta1+eta2+lambda3+lambda4)*P[28]+eta3*P[34]+eta4*P[35]+lambda1*P[25]+lambda2*P[24]+eta4*P[18]//28
                        -(eta1+eta3+lambda2+lambda4)*P[29]+eta2*P[34]+eta4*P[36]+lambda1*P[26]+lambda3*P[24]+eta4*P[20]//29
                        -eta4*P[30]+lambda4*P[24]                                    //30
                        -(eta2+eta3+lambda1+lambda4)*P[31]+eta1*P[34]+eta4*P[37]+lambda2*P[26]+lambda3*P[25]+eta4*P[22]//31
                        -eta4*P[32]+lambda4*P[25]                                    //32
                        -eta4*P[33]+lambda4*P[26]                                    //33
                        -(eta1+eta2+eta3)*P[34]+lambda1*P[31]+lambda2*P[29]+lambda3*P[28]                //34
                        -eta4*P[35]+lambda4*P[28]                                    //35
                        -eta4*P[36]+lambda4*P[29]                                    //36
                        -eta4*P[37]+lambda4*P[31]                                    //37
                    }
                ));

            return approximations.Last();
        } 
    }
}