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
    /// <summary> A tool data package. </summary>
    public class OpToolData
    {
        /// <summary> Gets or sets the tool number. </summary>
        public int Number { get; set; }

        /// <summary> Gets or sets the slot number of tool in the tool list. </summary>
        public int Slot { get; set; }

        /// <summary> Gets or sets the tool description name. </summary>
        public string Description { get; set; }

        /// <summary> Gets or sets the tool diameter. </summary>
        public double Diameter { get; set; }

        /// <summary> Gets or sets the tool length. </summary>
        public double Length { get; set; }

        /// <summary> Gets or sets the tool cutting length. </summary>
        public double CuttingLength { get; set; }

        /// <summary> Gets or sets the tool corner radius. </summary>
        public double CornerRadius { get; set; }

        /// <summary> Gets or sets the Holder ID (name). </summary>
        public string HolderId { get; set; }

        /// <summary> Gets or sets the Manufacturer's Tool Code. </summary>
        public string MfgToolCode { get; set; }

        /// <summary> Gets or sets the ID of the operation that references this tool. </summary>
        public int OpID { get; set; }

        /// <summary> Gets or sets the ID of the toolpath group the operation is in. </summary>
        public int GroupID { get; set; }

        /// <summary> Gets or sets the name of the Toolpath group. </summary>
        public string ToolpathGroupName { get; set; }

        /// <summary> Gets or set the Diameter Offset Number. </summary>
        public int DiameterOffset { get; set; }

        /// <summary> Gets or set the ext length. </summary>
        public double StickOut { get; set; }

        

    }
}