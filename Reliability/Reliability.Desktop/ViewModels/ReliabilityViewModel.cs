using Reliability.Desktop.Commands;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows;
using System;
using System.Linq;
using System.Diagnostics;

namespace Reliability.Desktop.ViewModels
{
    internal class ReliabilityViewModel : AbstractViewModel
    {
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

        public AsyncCommand ApproximateDenialCommand { get; set; }

        public ReliabilityViewModel()
        {
            ApproximateDenialCommand = new AsyncCommand(o => ApproximateDenial(), c => CanApproximateDenial());
        }

        private bool CanApproximateDenial()
        {
            return !IsInProgress;
        }

        private async Task ApproximateDenial()
        {
            //IsInProgress = true;
            //Status = "Upscaling...:";

            //await Task.Run(async () =>
            //{
            //    try
            //    {
            //        await ImageUpscalerFacade.RunImageUpscaler(
            //            FilenameInput,
            //            Algorithm,
            //            Scale,
            //            IsToDownscale,
            //            FilenameTemporaryOutput);
            //    }
            //    catch (Exception ex)
            //    {
            //        Status = ex.Message;
            //        IsInProgress = false;

            //        MessageBox.Show(
            //            "Image Upscaler failed to execute!",
            //            "Image Upscaler",
            //            MessageBoxButton.OK,
            //            MessageBoxImage.Error,
            //            MessageBoxResult.Yes);

            //        return;
            //    }

            //    Status = "Image was scaled!";
            //    Output = FilenameTemporaryOutput;
            //    IsInProgress = false;
            //});
        }
    }
}
