﻿// Copyright (c) Service Stack LLC. All Rights Reserved.
// License: https://raw.github.com/ServiceStack/ServiceStack/master/license.txt

using System;
using System.Collections.Generic;
using System.Net;
using System.Xml.Serialization;

namespace ServiceStack.AzureSQL.Types
{
    //public enum DatabasePerformanceLevel
    //{
    //    [Obsolete("Retiring Sept. 2015")] Web,
    //    [Obsolete("Retiring Sept. 2015")] Business,
    //    Basic,
    //    Standard_S0,
    //    Standard_S1,
    //    Standard_S2,
    //    Standard_S3, 
    //    Premium_P1,
    //    Premium_P2,
    //    Premium_P3,
    //}

    public enum DataCenter
    {
        [XmlEnum(Name = "East Asia")]
        EastAsia,
        [XmlEnum(Name = "Southeast Asia")]
        SoutheastAsia,
        [XmlEnum(Name = "Japan West")]
        JapanWest,
        [XmlEnum(Name = "Japan East")]
        JapanEast,
        [XmlEnum(Name = "Central US")]
        CentralUS,
        [XmlEnum(Name = "North Central US")]
        NorthCentralUS,
        [XmlEnum(Name = "South Central US")]
        SouthCentralUS,
        [XmlEnum(Name = "West US")]
        WestUS,
        [XmlEnum(Name = "East US")]
        EastUS,
        [XmlEnum(Name = "East US 2")]
        EastUS2,
        [XmlEnum(Name = "North Europe")]
        NorthEurope,
        [XmlEnum(Name = "West Europe")]
        WestEurope,
        [XmlEnum(Name = "Brazil South")]
        BrazilSouth
    }



    //public static class Extensions
    //{
    //    public static string ToEdition(this DatabasePerformanceLevel level)
    //    {
    //        switch (level)
    //        {
    //            case DatabasePerformanceLevel.Standard_S0:
    //            case DatabasePerformanceLevel.Standard_S1:
    //            case DatabasePerformanceLevel.Standard_S2:
    //            case DatabasePerformanceLevel.Standard_S3:
    //                return "Standard";
    //            case DatabasePerformanceLevel.Premium_P1:
    //            case DatabasePerformanceLevel.Premium_P2:
    //            case DatabasePerformanceLevel.Premium_P3:
    //                return "Premium";
    //            case DatabasePerformanceLevel.Web:
    //                return "Web";
    //            case DatabasePerformanceLevel.Business:
    //                return "Business";
    //            default:
    //                return "Basic";
    //        }
    //    }

    //    const Guid b_guid = Guid.Parse("dd6d99bb-f193-4ec1-86f2-43d3bccbc49c");
    //    const Guid s0_guid = Guid.Parse("f1173c43-91bd-4aaa-973c-54e79e15235b");
    //    const Guid s1_guid = Guid.Parse("1b1ebd4d-d903-4baa-97f9-4ea675f5e928");
    //    const Guid s2_guid = Guid.Parse("455330e1-00cd-488b-b5fa-177c226f28b7");
    //    const Guid s3_guid = Guid.Parse("789681b8-ca10-4eb0-bdf2-e0b050601b40");
    //    const Guid p1_guid = Guid.Parse("7203483a-c4fb-4304-9e9f-17c71c904f5d");
    //    const Guid p2_guid = Guid.Parse("a7d1b92d-c987-4375-b54d-2b1d0e0f5bb0");
    //    const Guid p3_guid = Guid.Parse("a7c4c615-cfb1-464b-b252-925be0a19446");

    //    public static Guid ToGuid(this DatabasePerformanceLevel level)
    //    {
    //        switch (level)
    //        {
    //            case DatabasePerformanceLevel.Basic:
    //                return b_guid;
    //            case DatabasePerformanceLevel.Standard_S0:
    //                return s0_guid;
    //            case DatabasePerformanceLevel.Standard_S1:
    //                return s1_guid;
    //            case DatabasePerformanceLevel.Standard_S2:
    //                return s2_guid;
    //            case DatabasePerformanceLevel.Standard_S3:
    //                return s3_guid;
    //            case DatabasePerformanceLevel.Premium_P1:
    //                return p1_guid;
    //            case DatabasePerformanceLevel.Premium_P2:
    //                return p2_guid;
    //            case DatabasePerformanceLevel.Premium_P3:
    //                return p3_guid;
    //            default:
    //                return Guid.Empty;
    //        }
    //    }
    //}


    public class AzureErrors
    {
        public AzureError Error { get; set; }
    }

    public class AzureError
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public string Param { get; set; }
    }

    public class AzureException : Exception
    {
        public AzureException(AzureError error)
            : base(error.Message)
        {
            Code = error.Code;
            Param = error.Param;
        }

        public string Code { get; set; }
        public string Param { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }

}

