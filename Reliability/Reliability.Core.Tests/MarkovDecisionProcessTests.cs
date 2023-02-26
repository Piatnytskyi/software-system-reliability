using QuikGraph;
using QuikGraph.Algorithms;
using QuikGraph.Graphviz;
using QuikGraph.Graphviz.Dot;
using QuikGraph.Serialization;
using Reliability.Core.Enums;
using Reliability.Core.Models;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace Reliability.Core.Tests
{
    public class MarkovDecisionProcessTests
    {
        private BidirectionalGraph<SystemState, TaggedEdge<SystemState, ChangeTag>> _systemStates;

        [SetUp]
        public void Setup()
        {
            ModuleState[] initialModulesStates = new[] {
                new ModuleState(ModuleType.SoftwareHardware, 1, ModuleStatus.Functional),
                new ModuleState(ModuleType.Hardware, ModuleStatus.Functional),
                new ModuleState(ModuleType.SoftwareHardware, 1, ModuleStatus.Functional),
                new ModuleState(ModuleType.Hardware, ModuleStatus.Functional),
                new ModuleState(ModuleType.Hardware, ModuleStatus.Functional)
            };

            Predicate<ModuleState[]> isFunctional = (ModuleState[] moduleStates) =>
            {
                return (initialModulesStates[0].Status == ModuleStatus.Functional
                        || initialModulesStates[2].Status == ModuleStatus.Functional)
                    && (initialModulesStates[1].Status == ModuleStatus.Functional
                        || initialModulesStates[3].Status == ModuleStatus.Functional)
                    && initialModulesStates[4].Status == ModuleStatus.Functional;
            };

            //Predicate<IEnumerable<ModuleState>> isRestorable = (IEnumerable<ModuleState> moduleStates) =>
            //{
            //    return ((initialModulesStates.ElementAt(0).Status == ModuleStatus.Functional
            //                || initialModulesStates.ElementAt(0).InfinitelyRestorable
            //                    || initialModulesStates.ElementAt(0).AvailableRestoresCount > 0)
            //            || (initialModulesStates.ElementAt(2).Status == ModuleStatus.Functional
            //                || initialModulesStates.ElementAt(2).InfinitelyRestorable
            //                    || initialModulesStates.ElementAt(2).AvailableRestoresCount > 0))
            //        && ((initialModulesStates.ElementAt(1).Status == ModuleStatus.Functional
            //                || initialModulesStates.ElementAt(1).InfinitelyRestorable)
            //            || (initialModulesStates.ElementAt(3).Status == ModuleStatus.Functional
            //                || initialModulesStates.ElementAt(3).InfinitelyRestorable))
            //        && (initialModulesStates.ElementAt(4).Status == ModuleStatus.Functional
            //            || initialModulesStates.ElementAt(3).InfinitelyRestorable);
            //};

            SystemState initialSystemState = new SystemState(initialModulesStates, isFunctional);

            MarkovDecisionProcess markovDecisionProcess = new MarkovDecisionProcess(initialSystemState);
            _systemStates = markovDecisionProcess.BuildStates();
        }

        [Test]
        public void Test1()
        {
            File.WriteAllText("output.dot", _systemStates.ToGraphviz(algorithm =>
            {
                // Custom init example
                algorithm.CommonVertexFormat.Shape = GraphvizVertexShape.Circle;
                algorithm.CommonEdgeFormat.ToolTip = "Edge tooltip";
                algorithm.FormatVertex += (sender, args) =>
                {
                    args.VertexFormat.FillColor = args.Vertex.IsFunctional ? GraphvizColor.White : GraphvizColor.Red;
                    args.VertexFormat.Label = $"{args.Vertex}";
                };
                algorithm.FormatEdge += (sender, args) =>
                {
                    args.EdgeFormat.Label.Value = $"{args.Edge.Tag}";
                };
            }));

            Assert.Pass();
        }
    }
}