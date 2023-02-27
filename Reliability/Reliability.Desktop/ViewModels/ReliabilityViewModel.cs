using Reliability.Desktop.Commands;
using System.Threading.Tasks;
using System;
using Reliability.Core;
using MathNet.Numerics.LinearAlgebra;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Reliability.Desktop.ViewModels
{
    internal class ReliabilityViewModel : AbstractViewModel
    {
        private double _lambda1 = 5 * Math.Pow(10, -4);

        public double Lambda1
        {
            get => _lambda1;
            set
            {
                if (_lambda1.Equals(value))
                {
                    return;
                }
                _lambda1 = value;
                RaisePropertyChanged(nameof(Lambda1));
            }
        }

        private double _lambda2 = 4 * Math.Pow(10, -4);

        public double Lambda2
        {
            get => _lambda2;
            set
            {
                if (_lambda2.Equals(value))
                {
                    return;
                }
                _lambda2 = value;
                RaisePropertyChanged(nameof(Lambda2));
            }
        }

        private double _lambda3 = 3 * Math.Pow(10, -4);

        public double Lambda3
        {
            get => _lambda3;
            set
            {
                if (_lambda3.Equals(value))
                {
                    return;
                }
                _lambda3 = value;
                RaisePropertyChanged(nameof(Lambda3));
            }
        }

        private double _lambda4 = 2.5 * Math.Pow(10, -4);

        public double Lambda4
        {
            get => _lambda4;
            set
            {
                if (_lambda4.Equals(value))
                {
                    return;
                }
                _lambda4 = value;
                RaisePropertyChanged(nameof(Lambda4));
            }
        }

        private double _lambda5 = 5 * Math.Pow(10, -4);

        public double Lambda5
        {
            get => _lambda5;
            set
            {
                if (_lambda5.Equals(value))
                {
                    return;
                }
                _lambda5 = value;
                RaisePropertyChanged(nameof(Lambda5));
            }
        }

        private double _eta1 = 5 * Math.Pow(10, -4);

        public double Eta1
        {
            get => _eta1;
            set
            {
                if (_eta1.Equals(value))
                {
                    return;
                }
                _eta1 = value;
                RaisePropertyChanged(nameof(Eta1));
            }
        }

        private double _eta2 = 4 * Math.Pow(10, -4);

        public double Eta2
        {
            get => _eta2;
            set
            {
                if (_eta2.Equals(value))
                {
                    return;
                }
                _eta2 = value;
                RaisePropertyChanged(nameof(Eta2));
            }
        }

        private double _eta3 = 3 * Math.Pow(10, -4);

        public double Eta3
        {
            get => _eta3;
            set
            {
                if (_eta3.Equals(value))
                {
                    return;
                }
                _eta3 = value;
                RaisePropertyChanged(nameof(Eta3));
            }
        }

        private double _eta4 = 2.5 * Math.Pow(10, -4);

        public double Eta4
        {
            get => _eta4;
            set
            {
                if (_eta4.Equals(value))
                {
                    return;
                }
                _eta4 = value;
                RaisePropertyChanged(nameof(Eta4));
            }
        }

        private int _integrationLimitsStart = 0;

        public int IntegrationLimitsStart
        {
            get => _integrationLimitsStart;
            set
            {
                if (_integrationLimitsStart.Equals(value))
                {
                    return;
                }
                _integrationLimitsStart = value;
                RaisePropertyChanged(nameof(IntegrationLimitsStart));
            }
        }

        private int _integrationLimitsEnd = 100;

        public int IntegrationLimitsEnd
        {
            get => _integrationLimitsEnd;
            set
            {
                if (_integrationLimitsEnd.Equals(value))
                {
                    return;
                }
                _integrationLimitsEnd = value;
                RaisePropertyChanged(nameof(IntegrationLimitsEnd));
            }
        }

        private ObservableCollection<KeyValuePair<int, double>> _chartData;

        public ObservableCollection<KeyValuePair<int, double>> ChartData
        {
            get => _chartData;
            set
            {
                if (_chartData.Equals(value))
                {
                    return;
                }
                _chartData = value;
                RaisePropertyChanged(nameof(ChartData));
            }
        }

        private string _status = string.Empty;

        public string Status
        {
            get => _status;
            set
            {
                if (_status.Equals(value))
                {
                    return;
                }
                _status = value;
                RaisePropertyChanged(nameof(Status));
            }
        }

        private string _output = string.Empty;

        public string Output
        {
            get => _output;
            set
            {
                if (_output.Equals(value))
                {
                    return;
                }
                _output = value;
                RaisePropertyChanged(nameof(Output));
            }
        }

        private bool _isInProgress;

        public bool IsInProgress
        {
            get => _isInProgress;
            set
            {
                if (_isInProgress.Equals(value))
                {
                    return;
                }
                _isInProgress = value;
                RaisePropertyChanged(nameof(IsInProgress));
            }
        }

        public AsyncCommand ApproximateDenial2Command { get; set; }
        public AsyncCommand ApproximateDenial7Command { get; set; }

        public ReliabilityViewModel()
        {
            ApproximateDenial2Command = new AsyncCommand(o => ApproximateDenial2(), c => CanApproximateDenial2());
            ApproximateDenial7Command = new AsyncCommand(o => ApproximateDenial7(), c => CanApproximateDenial7());
        }

        private bool CanApproximateDenial2()
        {
            return !IsInProgress;
        }

        private async Task ApproximateDenial2()
        {
            IsInProgress = true;
            Status = "Approximating Denials...:";

            await Task.Run(() =>
            {
                var softwareHardwareSystem2 = new SoftwareHardwareSystem2();
                Vector<double> denialApproximations = softwareHardwareSystem2.GetDenialApproximations(
                    IntegrationLimitsStart,
                    IntegrationLimitsEnd,
                    Lambda1,
                    Lambda2,
                    Lambda3,
                    Lambda4,
                    Lambda5);

                Output = string.Empty;
                for (int index = 0; index < 24; ++index)
                {
                    Output += "P[" + (index + 1) + "] = " + denialApproximations[index].ToString() + "\r\n";
                    //ChartData.Add(new KeyValuePair<int, double>(index + 1, denialApproximations[index]));
                }

                double denialApproximationsProbabilitySum = denialApproximations.Sum();
                double successProbability = denialApproximations[0]
                    + denialApproximations[1]
                    + denialApproximations[2]
                    + denialApproximations[3]
                    + denialApproximations[4]
                    + denialApproximations[6]
                    + denialApproximations[8]
                    + denialApproximations[10]
                    + denialApproximations[13];
                double failureProbability = denialApproximationsProbabilitySum - successProbability;
                Output += "Success Probability: " + successProbability + "\r\n";
                Output += "FailureProbability: " + failureProbability + "\r\n";
                Output += "Denial approximations probability sum: " + denialApproximationsProbabilitySum + "\r\n";

                Status = "Denial was approximated!";
                IsInProgress = false;
            });
        }

        private bool CanApproximateDenial7()
        {
            return !IsInProgress;
        }

        private async Task ApproximateDenial7()
        {
            IsInProgress = true;
            Status = "Approximating Denials...:";

            await Task.Run(() =>
            {
                var softwareHardwareSystem7 = new SoftwareHardwareSystem7();
                Vector<double> denialApproximations = softwareHardwareSystem7.GetDenialApproximations(
                    IntegrationLimitsStart,
                    IntegrationLimitsEnd,
                    Lambda1,
                    Lambda2,
                    Lambda3,
                    Lambda4,
                    Eta1,
                    Eta2,
                    Eta3,
                    Eta4);

                Output = string.Empty;
                for (int index = 0; index < 37; ++index)
                {
                    Output += "P[" + (index + 1) + "] = " + denialApproximations[index].ToString() + "\r\n";
                    //ChartData.Add(new KeyValuePair<int, double>(index + 1, denialApproximations[index]));
                }

                double denialApproximationsProbabilitySum = denialApproximations.Sum();
                double successProbability = denialApproximations[0]
                    + denialApproximations[1]
                    + denialApproximations[2]
                    + denialApproximations[3]
                    + denialApproximations[6]
                    + denialApproximations[7]
                    + denialApproximations[10]
                    + denialApproximations[22]
                    + denialApproximations[23]
                    + denialApproximations[24]
                    + denialApproximations[25]
                    + denialApproximations[27]
                    + denialApproximations[28]
                    + denialApproximations[30];
                double failureProbability = denialApproximationsProbabilitySum - successProbability;
                Output += "Success Probability: " + successProbability + "\r\n";
                Output += "FailureProbability: " + failureProbability + "\r\n";
                Output += "Denial approximations probability sum: " + denialApproximationsProbabilitySum + "\r\n";

                Status = "Denial was approximated!";
                IsInProgress = false;
            });
        }
    }
}
