using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.OdeSolvers;
using QuikGraph;
using Reliability.Core.Enums;
using Reliability.Core.Models;
using System;
using System.Xml.Linq;

namespace Reliability.Core
{
    public class MarkovDecisionProcess
    {
        private readonly SystemState _systemState;

        public MarkovDecisionProcess(SystemState systemState)
        {
            _systemState = systemState;
        }

        public BidirectionalGraph<SystemState, TaggedEdge<SystemState, ChangeTag>> BuildStates()
        {
            BidirectionalGraph<SystemState, TaggedEdge<SystemState, ChangeTag>> systemStates =
                new BidirectionalGraph<SystemState, TaggedEdge<SystemState, ChangeTag>>();

            SystemState currentSystemState = (SystemState)_systemState.Clone();
            systemStates.AddVertex(currentSystemState);

            for (int i = 0; i < systemStates.VertexCount; i++)
            {
                currentSystemState = systemStates.Vertices.ElementAt(i);
                for (int j = 0; j < _systemState.Modules.Count(); j++)
                {
                    if (currentSystemState.Modules[j].Status == ModuleStatus.Failed
                        && currentSystemState.Modules[j].AvailableRestoresCount > 0)
                    {
                        SystemState nextSystemState = (SystemState)currentSystemState.Clone();
                        ModuleState currentModuleState = nextSystemState.Modules[j];
                        currentModuleState.SetModuleStatus(ModuleStatus.Functional);
                        nextSystemState.Modules[j] = currentModuleState;

                        systemStates.AddVertex(nextSystemState);
                        systemStates.AddEdge(
                            new TaggedEdge<SystemState, ChangeTag>(
                                currentSystemState,
                                nextSystemState,
                                new ChangeTag(ChangeType.Restore, 1)));
                    }

                    if (currentSystemState.Modules[j].Status == ModuleStatus.Functional)
                    {
                        SystemState nextSystemState = (SystemState)currentSystemState.Clone();
                        ModuleState currentModuleState = nextSystemState.Modules[j];
                        currentModuleState.SetModuleStatus(ModuleStatus.Failed);
                        nextSystemState.Modules[j] = currentModuleState;

                        if (!systemStates.Vertices.Any(v => v.Equals(nextSystemState)))
                            systemStates.AddVertex(nextSystemState);

                        systemStates.AddEdge(
                            new TaggedEdge<SystemState, ChangeTag>(
                                currentSystemState,
                                nextSystemState,
                                new ChangeTag(ChangeType.Failure, 1)));
                    }
                }
            }

            return systemStates;
        }

        //public Vector<double> GetDenialApproximations(int start, int end, double lambda1, double lambda2, double lambda3, double lambda4, double lambda5)
        //{
        //    double[] initialY0 = new double[24];
        //    initialY0[0] = 1;
        //    Vector<double> y0 = Vector<double>.Build.Dense(initialY0);

        //    Vector<double>[] approximations = RungeKutta.FourthOrder(
        //        y0,
        //        start,
        //        end,
        //        25,
        //        (t, P) =>
        //            Vector<double>.Build.Dense(new double[]
        //            {
        //                - ( lambda1 + lambda2 + lambda3 + lambda4 + lambda5) * P[0],        //1
        //                lambda1 * P[0] - ( lambda2 + lambda3 + lambda4 + lambda5 ) * P[1],  //2
        //                lambda2 * P[0] - ( lambda1 + lambda3 + lambda4 + lambda5 ) * P[2],  //3
        //                lambda3 * P[0] - ( lambda1 + lambda2 + lambda4 + lambda5 ) * P[3],  //4
        //                lambda4 * P[0] - ( lambda1 + lambda2 + lambda3 + lambda5 ) * P[4],  //5
        //                lambda5 * P[0],                                                     //6
        //                lambda2 * P[1] + lambda1 * P[2] - ( lambda3 + lambda4 + lambda5 ) * P[6],  //7
        //                lambda3 * P[1] + lambda1 * P[3],                                    //8
        //                lambda4 * P[1] + lambda1 * P[4] - (lambda2 + lambda3 + lambda5) * P[8],    //9
        //                lambda5 * P[1],                                                     //10
        //                lambda3 * P[2] + lambda2 * P[3] - (lambda1 + lambda4 + lambda5) * P[10],  //11
        //                lambda4 * P[2] + lambda2 * P[4],                                     //12
        //                lambda5 * P[2],                                                     //13
        //                lambda3 * P[4] + lambda4 * P[3] - (lambda1 + lambda2 + lambda5) * P[13],  //14
        //                lambda5 * P[3],                                                     //15
        //                lambda5 * P[4],                                                     //16
        //                lambda3 * P[6] + lambda1 * P[10],                                   //17
        //                lambda4 * P[6] + lambda2 * P[8],                                    //18
        //                lambda5 * P[6],                                                     //19
        //                lambda3 * P[8] + lambda1 * P[13],                                   //20
        //                lambda5 * P[8],                                                     //21
        //                lambda4 * P[10] + lambda5 * P[13],                                  //22
        //                lambda5 * P[10],                                                    //23
        //                lambda5 * P[13]                                                     //24
        //            }
        //        ));

        //    return approximations.Last();
        //} 
    }
}