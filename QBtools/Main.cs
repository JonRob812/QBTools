// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Main.cs" company="CNC Software, Inc.">
//   Copyright (c) 2019 CNC Software, Inc.
// </copyright>
// <summary>
//  If this project is helpful please take a short survey at ->
//  http://ux.mastercam.com/Surveys/APISDKSupport 
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace QBtools
{
    using System;
    using Mastercam.App.Exceptions;
    using Mastercam.IO;
    using Mastercam.IO.Types;
    using System.Windows.Forms;
    using Mastercam.App;
    using Mastercam.App.Types;
    using Mastercam.Support.UI;
    using System.Reflection;
    using Services;
    using Properties;
    using Cnc.Tool.Interop;
    using Mastercam.Support;
    using System.Collections.Generic;
    using System.Net;
    using System.Linq;




    public class Main : Mastercam.App.NetHook3App
    {
        public static class Globals
        {
            public static string csvtools = "";
            

        }

        #region Public Override Methods

        /// <summary> Initialize anything we need for the NET-Hook here. </summary>
        ///
        /// <param name="param"> System parameter. </param>
        ///
        /// <returns> A <c>MCamReturn</c> return type representing the outcome of your NetHook application. </returns>
        public override MCamReturn Init(int param)
        {
            // Wire up handler for any global exceptions not handled by the app
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                this.HandleUnhandledException(args.ExceptionObject as Exception);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender, args) => this.HandleUnhandledException(args.Exception);

            if (Settings.Default.FirstTimeRunning)
            {
                var msg = ResourceReaderService.GetString("FirstTimeRunning");
                var assembly = Assembly.GetExecutingAssembly().FullName;
                EventManager.LogEvent(MessageSeverityType.InformationalMessage, assembly, msg.IsSuccess ? msg.Value : msg.Error);

                Settings.Default.FirstTimeRunning = false;
                Settings.Default.Save();
            }


          
            return base.Init(param);
        }
                

        /// <summary> The main entry point for your NethookPlay. </summary>
        ///
        /// <param name="param"> System parameter. </param>
        ///
        /// <returns> A <c>MCamReturn</c> return type representing the outcome of your NetHook application. </returns>
        public override MCamReturn Run(int param)
        {

            // Create our view
            var winView = new MainView { TopLevel = true };

            // Set the dialog as modeless to Mastercam, always on top
            var handle = Control.FromHandle(MastercamWindow.GetHandle().Handle);
            _ = new ModelessDialogTabsHandler(winView);

            winView.StartPosition = FormStartPosition.CenterScreen;
            winView.Show(handle);
              //.SetupId.Text = Mastercam.Support.GroupManager.GetCurrentGroupName();

            this.GetToolData();
            

            return MCamReturn.NoErrors;
        }

        /// <summary> Gets (some) data about the tools used by the toolpath operations. </summary>
        public void GetToolData()
        {
            // Get all tools in the file
            var ts = TlToolFunctions.GetToolSystem();

            // Using functionality in the GetToolDataInterop.DLL - all ops in file
            // Get the IDs (and some more data) for all of the tools that are referenced in an operation.
            var allops = Mastercam.Database.Interop.OperationManager.GetAllOperationData();
            var onlyMillops = SearchManager.GetOperations(true);

            // create empty list
            var toolDataList = new List<OpToolData>();

            // loop through lathe ops, for tool data list 
            foreach (var op in allops)
            {
                
                if (op.IsLatheOperation)
                {
                    var latheTool = new TlToolLathe();
                    if (ts.Find(op.ToolSlot, ref latheTool))
                    {
                        var toolData = new OpToolData
                        {
                            Number = latheTool.ToolNumber,
                            DiameterOffset = latheTool.ToolOffsetNum,
                            MfgToolCode = latheTool.MfgToolCode,
                            StickOut = latheTool.GaugeLength,
                        };
                        toolDataList.Add(toolData);
                    }
                }

            }
            // Loop through mill ops, for tool data list
            foreach (var op in onlyMillops)
            {  
                var toolAssembly = new TlAssembly();
                // The unique slot# of the tool in the local tool list.
                var slot = op.OperationTool.Slot;
                if (ts.Find(slot, ref toolAssembly))
                {
                    var toolData = new OpToolData
                    {
                        Number = toolAssembly.ToolNumber,
                        DiameterOffset = op.DiameterOffset, // use offset that is used in the NC code 
                        MfgToolCode = toolAssembly.MainTool.MfgToolCode,
                        StickOut = toolAssembly.Stickout,
                    };
                        
                    toolDataList.Add(toolData);
                }
            }
            
            this.ShowToolData(toolDataList);
        }


        /// <summary> Displays some data to the user.. </summary>
        ///
        /// <param name ="toolDataList"> The tool data from the file. </param>
        private void ShowToolData(List<OpToolData> toolDataList)
        {
            // build string for csv message
            // filter duplicate tools
            var sb = new System.Text.StringBuilder();
            var uniqueTools = toolDataList.GroupBy(x => x.Number).Select(x => x.First());  
            
            foreach (var entry in uniqueTools)
            {
               
                sb.AppendFormat($"{entry.Number},{entry.DiameterOffset},{entry.MfgToolCode},{entry.StickOut},setupid{Environment.NewLine}");
                
            }

            Globals.csvtools = sb.ToString();    
        }

               
        #endregion

        #region Private Methods

        /// <summary> Log exceptions and show a message. </summary>
        ///
        /// <param name="e"> The exception. </param>
        private void HandleUnhandledException(Exception e)
        {
            // Show the user
            DialogManager.Exception(new MastercamException(e.Message, e.InnerException));

            // Write to the event log
            var msg = e.InnerException != null ? e.InnerException.Message : e.Message;
            var assembly = Assembly.GetExecutingAssembly().FullName;
            EventManager.LogEvent(MessageSeverityType.ErrorMessage, assembly, msg);
        }

        #endregion
    }
}

